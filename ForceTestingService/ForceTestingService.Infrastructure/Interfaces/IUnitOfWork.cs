using System.Threading.Tasks;
using ForceTestingService.Infrastructure.Entities;

namespace ForceTestingService.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Answer> AnswerRepository { get; }
        IRepository<Question> QuestionRepository { get; }
        IRepository<Subject> SubjectRepository { get; }
        IRepository<TestBase> TestBaseRepository { get; }
        IRepository<TestResult> TestResultRepository { get; }
        IRepository<Topic> TopicRepository { get; }
        IRepository<User> UserRepository { get; }

        Task CommitAsync();
        Task RollbackAsync();
    }
}