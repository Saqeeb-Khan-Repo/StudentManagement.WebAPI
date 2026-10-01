namespace StudentManagement.WebAPI.Models;

public class Student
{
    public Guid StudentID { get; set; }
    public string? Name { get; set; }
    public int? Age { get; set; }
    public string? Gender { get; set;}
    public string? Email { get; set; }

    public string? Course { get; set; }
    public decimal? Marks { get; set; }
    public string? CreatedAt { get; set; }


}
