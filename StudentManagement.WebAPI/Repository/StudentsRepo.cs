using Dapper;
using StudentManagement.WebAPI.Models;
using StudentManagement.WebAPI.RepositoryContracts;
using StudentManagement.WebAPI.ServiceContracts.DTOs;
using StudentManagement.WebAPI.SqlConnection1;
using System.Data;

namespace StudentManagement.WebAPI.Repository;

public class StudentsRepo : IStudentRepo
{
    private SqlConnectionFactory _sqlConnection;
    public StudentsRepo(SqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnection = sqlConnectionFactory;
    }
    public async Task<StudentResponse> CreateStudentRepo(
     StudentAddRequest studentRequest)
    {
       
        // Convert DTO to Student
        var student = studentRequest.ToStudent();

        // Generate values
        student.StudentID = Guid.NewGuid();
        student.CreatedAt = DateTime.UtcNow.ToString("O");

        using var connection = _sqlConnection.CreateConnection();

        // Insert student
        await connection.ExecuteAsync(
            "CreateStudent",
            new
            {
                student.StudentID,
                student.Name,
                student.Email,
                student.Age,
                student.Gender,
                student.Course,
                student.Marks,
                student.CreatedAt
            },
            commandType: CommandType.StoredProcedure
        );

        // Convert Student -> Response DTO
        return student.ToStudentResponse();
    }

    public async Task<bool> DeleteStudentRepo(Guid? studentID)
    {
        using var connection = _sqlConnection.CreateConnection();

        var Deleted = await connection.QueryFirstOrDefaultAsync(
             "DeleteStudentByID",
             new { StudentID = studentID },
             commandType: CommandType.StoredProcedure
             );

        if (Deleted == null)
        {
            return false;
        }

        return true;
    }

    public async Task<List<StudentResponse>> GetAllStudentsRepo()
    {
        var connection = _sqlConnection.CreateConnection();

        var students = await connection.QueryAsync<Student>
            (
              "GetAllStudents",
               commandType: CommandType.StoredProcedure
            );

        var StudentsList = students.Select(e => e.ToStudentResponse()).ToList();

        return StudentsList;
    }

    public async Task<StudentResponse> GetStudentByIDRepo(Guid studentId)
    {
        using var connection = _sqlConnection.CreateConnection();

       var student  = await connection
            .QueryFirstOrDefaultAsync<Student>(
                "GetStudentByID",
                new { StudentID = studentId }
                , commandType: CommandType.StoredProcedure
            );

        if (student == null)
        {
            throw new KeyNotFoundException("StudentID doest't Exists in Current Database");
        }

        return student.ToStudentResponse();
    }

    public async Task<StudentResponse?> UpdateStudentRepo(StudentAddRequest student, Guid studentID)
    {
        using var connection = _sqlConnection.CreateConnection();
        //check if user is exists or not
        Student? existingUser = await  connection.QueryFirstOrDefaultAsync<Student>(
            "GetStudentByID",
             new {StudentID = studentID},
             commandType:CommandType.StoredProcedure
            );

        if(existingUser == null)
        {
            return null;
        }


        //after user exist update code
        if (studentID == Guid.Empty)
        {
            throw new ArgumentException("StudentID cannot be empty.");
        }

        var rowsAffected = await connection.ExecuteAsync(
            "UpdateStudent",
            new
            {
                StudentID = studentID,
                student.Name,
                student.Email,
                student.Age,
                student.Gender,
                student.Course,
                student.Marks
            },
            commandType: CommandType.StoredProcedure
        );

        if (rowsAffected == 0)
        {
            return null;
        }
        //Convert to Student
        var studentModel = student.ToStudent();
        studentModel.StudentID = studentID;

        return studentModel.ToStudentResponse();
    }
}
