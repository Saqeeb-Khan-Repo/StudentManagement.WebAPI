using StudentManagement.WebAPI.Models;
using StudentManagement.WebAPI.ServiceContracts.DTOs;

namespace StudentManagement.WebAPI.RepositoryContracts;

public interface IStudentRepo
{
    public Task<List<StudentResponse>> GetAllStudentsRepo();
    public Task<StudentResponse> GetStudentByIDRepo(Guid studentId);
    public Task<StudentResponse> CreateStudentRepo(StudentAddRequest studentRequest);
    public Task<StudentResponse?> UpdateStudentRepo(StudentAddRequest student, Guid studentID);
    public Task<bool> DeleteStudentRepo(Guid? studentID);
}
