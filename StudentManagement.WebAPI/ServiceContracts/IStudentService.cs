using StudentManagement.WebAPI.Models;
using StudentManagement.WebAPI.ServiceContracts.DTOs;

namespace StudentManagement.WebAPI.ServiceContracts;

public interface IStudentService
{
    public Task<List<StudentResponse>> GetAllStudentsAsync();
    public Task<StudentResponse> GetStudentByIDAsync(Guid studentId);
    public Task<StudentResponse> CreateStudentAsync(StudentAddRequest studentRequest);
    public Task<StudentResponse?> UpdateStudentAsync(StudentAddRequest student , Guid studentID);
    public Task<bool> DeleteStudentAsync(Guid? studentID);

}
