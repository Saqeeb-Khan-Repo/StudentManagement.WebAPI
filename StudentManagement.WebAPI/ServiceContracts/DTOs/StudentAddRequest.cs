using StudentManagement.WebAPI.Models;

namespace StudentManagement.WebAPI.ServiceContracts.DTOs;

public class StudentAddRequest
{ 
    public string? Name { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set; }
    public string? Email { get; set; }

    public string? Course { get; set; }
    public decimal? Marks { get; set; }
    public string? CreatedAt { get; set; }

    /// <summary>
    /// Method to Convert StudentRequest into Student Object
    /// </summary>
    /// <returns></returns>
    public Student ToStudent()
    {
        return new Student
        {
          Name = Name,
          Email = Email,
          Course = Course,
          Gender = Gender,
          Marks = Marks,
          CreatedAt = CreatedAt,
          Age = Age
        };
    }
}
