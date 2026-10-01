using Dapper;
using StudentManagement.WebAPI.RepositoryContracts;
using StudentManagement.WebAPI.ServiceContracts;
using StudentManagement.WebAPI.ServiceContracts.DTOs;
using StudentManagement.WebAPI.SqlConnection1;
using System.Data;


namespace StudentManagement.WebAPI.Services;

public class StudentService : IStudentService
{
    //private fileds
    private  readonly IStudentRepo _studentRepo;
    private readonly SqlConnectionFactory _sqlConnection;

    //Constructor
    public StudentService(IStudentRepo studentRepo , SqlConnectionFactory connectionFactory)
    {
        _studentRepo = studentRepo;
        _sqlConnection = connectionFactory;
    }
    public async Task<List<StudentResponse>> GetAllStudentsAsync()
    {
        return await _studentRepo.GetAllStudentsRepo();
    }

    public async Task<StudentResponse> GetStudentByIDAsync(Guid studentId)
    {
        return await  _studentRepo.GetStudentByIDRepo(studentId);

    }
    public async Task<StudentResponse> CreateStudentAsync(StudentAddRequest studentRequest)
    {
        // Validate email
        var existingStudent =
            await GetStudentByEmailAsync(studentRequest.Email);

        if (existingStudent != null)
        {
            throw new InvalidOperationException(
                "A student with this email already exists.");
        }

        return await _studentRepo.CreateStudentRepo(studentRequest);
    }

    public async Task<bool> DeleteStudentAsync(Guid? studentID)
    {
        if (studentID == null)
        {
            throw new KeyNotFoundException(
         $"Product with ID '{studentID}' was not found.");
        }

        return await  _studentRepo.DeleteStudentRepo(studentID);
    }

    public async Task<StudentResponse?> UpdateStudentAsync(StudentAddRequest student, Guid studentID)
    {
        return await _studentRepo.UpdateStudentRepo(student,studentID);
    }

    // For Email Validation
    public async Task<StudentResponse?> GetStudentByEmailAsync(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required.", nameof(email));
        }

        using var connection = _sqlConnection.CreateConnection();

        StudentAddRequest? studentRequest = await connection.QueryFirstOrDefaultAsync<StudentAddRequest>(
            "CheckDuplicate",
            new { Email = email.Trim() },
            commandType: CommandType.StoredProcedure
        );

        //dto => student
        var student = studentRequest.ToStudent();
        
        //Student => Response
        return student?.ToStudentResponse();
    }
}
