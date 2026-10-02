using StudentManagement.WebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.WebAPI.ServiceContracts.DTOs;

public class StudentAddRequest
{
    [Required(ErrorMessage = "Student Name is Required")]
    [StringLength(100)]
    public string? Name { get; set; }

    [Required]
    [Range(15,25)]
    public int? Age { get; set; }

    [Required]
    [StringLength(20)]
    public string? Gender { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }

    [Required]
    [StringLength(100)]
    public string? Course { get; set; }

    [Required]
    [Range(0,100)]
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
