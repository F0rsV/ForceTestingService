using System;
using System.Threading.Tasks;
using ForceTestingService.Infrastructure.Context;
using ForceTestingService.Infrastructure.Entities;
using ForceTestingService.Infrastructure.Interfaces;
using ForceTestingService.Infrastructure.Repositories;

namespace ForceTestingService.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        private IRepository<Answer> _answerRepository;
        public IRepository<Answer> AnswerRepository => _answerRepository ??= new GenericRepository<Answer>(_context);

        private IRepository<Question> _questionRepository;
        public IRepository<Question> QuestionRepository => _questionRepository ??= new GenericRepository<Question>(_context);

        private IRepository<Subject> _subjectRepository;
        public IRepository<Subject> SubjectRepository => _subjectRepository ??= new GenericRepository<Subject>(_context);

        private IRepository<TestBase> _testBaseRepository;
        public IRepository<TestBase> TestBaseRepository => _testBaseRepository ??= new GenericRepository<TestBase>(_context);

        private IRepository<TestResult> _testResultRepository;
        public IRepository<TestResult> TestResultRepository => _testResultRepository ??= new GenericRepository<TestResult>(_context);

        private IRepository<Topic> _topicRepository;
        public IRepository<Topic> TopicRepository => _topicRepository ??= new GenericRepository<Topic>(_context);

        private IRepository<User> _userRepository;
        public IRepository<User> UserRepository => _userRepository ??= new GenericRepository<User>(_context);


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
    }
}