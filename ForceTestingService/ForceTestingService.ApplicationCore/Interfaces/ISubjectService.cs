using System.Collections.Generic;
using System.Threading.Tasks;
using ForceTestingService.ApplicationCore.DTO;

namespace ForceTestingService.ApplicationCore.Interfaces
{
    public interface ISubjectService
    {
        Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync();
        Task<SubjectDto> GetSubjectByIdAsync(int subjectId);
        Task CreateSubjectAsync(SubjectDto subjectDto);
        Task UpdateSubjectAsync(SubjectDto subjectDto);
        Task DeleteSubjectByIdAsync(int subjectId);
    }
}