using StudentManagement.WebAPI.Models;

namespace StudentManagement.WebAPI.ServiceContracts.DTOs;

public class StudentResponse
{
    public Guid StudentID { get; set; }
    public string? Name { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }

    public string? Course { get; set; }
    public decimal? Marks { get; set; }
    public string? CreatedAt { get; set; }

}

/// <summary>
/// Extension Method  Convert  to StudentResponse
/// </summary>
public static class StudentExtensions
{
    public static StudentResponse ToStudentResponse(this Student student)
    {
        return new StudentResponse
        {
            StudentID = student.StudentID,
            Name = student.Name,
            Course = student.Course,
            Age = student.Age,
            Marks = student.Marks,
            CreatedAt = student.CreatedAt,
            Gender = student.Gender,
            Email = student.Email
        };
    }
}
