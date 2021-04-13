using ForceTestingService.ApplicationCore.Interfaces;
using ForceTestingService.ApplicationCore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ForceTestingService.ApplicationCore.Utils
{
    public static class BllServiceProvider
    {
        public static IServiceCollection RegisterBllServices(this IServiceCollection services)
        {
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<ITopicService, TopicService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IAnswerService, AnswerService>();
            services.AddTransient<ITestBaseService, TestBaseService>();
            services.AddTransient<IStudentTestService, StudentTestService>();
            services.AddTransient<ITestResultService, TestResultService>();
            
            return services;
        }
    }
}