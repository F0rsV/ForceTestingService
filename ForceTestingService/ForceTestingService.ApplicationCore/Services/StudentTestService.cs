using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;
using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Mappers;
using ForceTestingService.ApplicationCore.Models;
using ForceTestingService.ApplicationCore.Utils;
using ForceTestingService.Infrastructure.Entities;
using ForceTestingService.Infrastructure.Interfaces;

namespace ForceTestingService.ApplicationCore.Services
{
    public class StudentTestService : IStudentTestService
    {
        private readonly IUnitOfWork _unitOfWork;

        private TestResultMapper _testResultMapper;
        public TestResultMapper TestResultMapper => _testResultMapper ??= new TestResultMapper();

        private TestBaseMapper _testBaseMapper;
        public TestBaseMapper TestBaseMapper => _testBaseMapper ??= new TestBaseMapper();

        private TopicMapper _topicMapper;
        public TopicMapper TopicMapper => _topicMapper ??= new TopicMapper();

        private QuestionMapper _questionMapper;
        public QuestionMapper QuestionMapper => _questionMapper ??= new QuestionMapper();

        private AnswerMapper _answerMapper;
        public AnswerMapper AnswerMapper => _answerMapper ??= new AnswerMapper();

        public StudentTestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentTest>> CreateStudentTestsListForUserAsync(User user)
        {
            var testResultsForThisStudent = await _unitOfWork.TestResultRepository
                .GetAllAsync(testResult => testResult.StudentUser == user);
            IEnumerable<StudentTest> studentTests = new List<StudentTest>();

            var allTestBases = await _unitOfWork.TestBaseRepository
                .GetAllAsync(null, null, new[] {"Topic", "Topic.Subject"});

            foreach (var testBase in allTestBases)
            {
                var testResultsForThisTest = testResultsForThisStudent
                    .Where(x => x.TestBaseId == testBase.Id);

                int numOfTriesLeft = testBase.NumOfTries - testResultsForThisTest.Count();
                var isPassed = testResultsForThisTest.Any();

                var studentTest = new StudentTest(testBase, numOfTriesLeft, isPassed);
                studentTests = studentTests.Append(studentTest);
            }

            return studentTests;
        }

        public async Task<IEnumerable<TestResultDto>> GetTestResultsForUserAsync(User user, int testBaseId)
        {
            var testResultsForThisStudent = await _unitOfWork.TestResultRepository
                .GetAllAsync(testResult => testResult.StudentUser == user);

            var testResultsForThisTest = testResultsForThisStudent
                .Where(x => x.TestBaseId == testBaseId);

            return TestResultMapper.Map(testResultsForThisTest);
        }

        public async Task<CraftedTest> CraftRandomTestFromTestBaseAsync(User user, int testBaseId)
        {
            var testBaseEntity = await _unitOfWork.TestBaseRepository.GetByIdAsync(testBaseId);

            var testResultsForThisStudent = await _unitOfWork.TestResultRepository
                .GetAllAsync(testResult => testResult.StudentUser == user);
            var testResultsForThisTest = testResultsForThisStudent
                .Where(x => x.TestBaseId == testBaseId);
            int numOfTriesLeft = testBaseEntity.NumOfTries - testResultsForThisTest.Count();

            if (numOfTriesLeft <= 0)
            {
                return null;
            }

            testBaseEntity.Topic = await _unitOfWork.TopicRepository.GetByIdAsync(testBaseEntity.TopicId);
            var testBaseDto = TestBaseMapper.Map(testBaseEntity);

            var craftedTest = new CraftedTest(user, testBaseDto);

            var topicEntity = await _unitOfWork.TopicRepository.GetByIdAsync(testBaseDto.TopicId);
            topicEntity.Subject = await _unitOfWork.SubjectRepository.GetByIdAsync(topicEntity.SubjectId);

            
            var topicDto = TopicMapper.Map(topicEntity);
            var questionsEntities = await _unitOfWork.QuestionRepository
                .GetAllAsync(question => question.TopicId == testBaseDto.TopicId, null, new[] { "Topic" });
            topicDto.Questions = (ICollection<QuestionDto>)QuestionMapper.Map(questionsEntities);
            foreach (var questionDto in topicDto.Questions)
            {
                var answersEntities = await _unitOfWork.AnswerRepository
                    .GetAllAsync(answer => answer.QuestionId == questionDto.Id);
                ;
                questionDto.Answers = (ICollection<AnswerDto>) AnswerMapper.Map(answersEntities);
                foreach (var answerDto in questionDto.Answers)
                    answerDto.IsCorrect = false;
            }
            
            var questionListForTopic = topicDto.Questions.ToList();
            questionListForTopic.Shuffle();

            var questionListIndex = 0;
            for (var numOfQuestionIndex = 0; numOfQuestionIndex < testBaseDto.NumOfQuestions; numOfQuestionIndex++)
            {
                if (numOfQuestionIndex >= questionListForTopic.Count)
                {
                    questionListIndex = 0;
                    questionListForTopic.Shuffle();
                }

                var question = new QuestionDto
                {
                    Id = questionListForTopic[questionListIndex].Id,
                    TaskInfo = questionListForTopic[questionListIndex].TaskInfo,
                    TopicId = questionListForTopic[questionListIndex].TopicId,
                    TopicName = questionListForTopic[questionListIndex].TopicName
                };
                var answers = questionListForTopic[questionListIndex].Answers.ToList();
                answers.Shuffle();
                question.Answers = answers;
                craftedTest.Questions = craftedTest.Questions.Append(question);

                questionListIndex++;
            }

            return craftedTest;
        }

        public async Task SubmitTestAsync(CraftedTest craftedTest)
        {
            var numOfQuestions = craftedTest.TestBaseDto.NumOfQuestions;
            var numOfCorrectAnswers = 0;

            foreach (var questionDto in craftedTest.Questions)
            {
                var currentQuestionIsAnsweredCorrect = true;
                foreach (var answerDto in questionDto.Answers)
                {
                    var correctAnswer = await _unitOfWork.AnswerRepository.GetByIdAsync(answerDto.Id);
                    if (answerDto.IsCorrect != correctAnswer.IsCorrect)
                    {
                        currentQuestionIsAnsweredCorrect = false;
                        break;
                    }
                }

                if(currentQuestionIsAnsweredCorrect)
                    numOfCorrectAnswers++;
            }
            var score = (float)numOfCorrectAnswers / (float)numOfQuestions * 100;
            score = (float) Math.Round(score, 2);

            var testResultDto = new TestResultDto
            {
                Date = craftedTest.Date,
                TestBaseId = craftedTest.TestBaseDto.Id,
                StudentUserId = craftedTest.User.Id,
                Score = score
            };

            var testResultEntity = TestResultMapper.Map(testResultDto);

            await _unitOfWork.TestResultRepository.InsertAsync(testResultEntity);
            await _unitOfWork.CommitAsync();
        }
    }
}