using Microsoft.AspNetCore.Mvc;
using StudentManagement.WebAPI.ServiceContracts;
using StudentManagement.WebAPI.ServiceContracts.DTOs;

namespace StudentManagement.WebAPI.Controllers;

[ApiController]
[Route("api/students")]
public class HomeController : ControllerBase
{
    private readonly IStudentService _studentService;
    public HomeController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var Students = await _studentService.GetAllStudentsAsync();

        return Ok(Students);
    }

    [HttpGet("{studentID:guid}")]
    public async Task<IActionResult> GetStudentByID(Guid studentID)
    {
        try
        {
            var Students = await _studentService.GetStudentByIDAsync(studentID);

            return Ok(Students);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { ex.Message });
        }
    }

    [HttpDelete("{studentID:guid}")]
    public async Task<IActionResult> DeleteStudentByID(Guid studentID)
    {
        try
        {
            var deleted = await _studentService.DeleteStudentAsync(studentID);

            if (!deleted)
            {
                var errorMessage = "Id Not Found or Already Deleted";
                return NotFound(
                    new
                    {  
                        errorMessage
                    }
                    );
            }
            else
            {
                return Content("Deleted Successfully...");
            }
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                ex.Message
            });
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateStudents(StudentAddRequest student)
    {
        try
        {
            StudentResponse student1 = await _studentService.CreateStudentAsync(student);
            return Created("Successfully Created..", student1);
        }
        catch(InvalidOperationException ex)
        {
            return NotFound(new
            {
                ex.Message
            });
        }
          
    }
    [HttpPut("{studentID:guid}")]
    public async Task<IActionResult> UpdateStudent(StudentAddRequest student , Guid studentID)
    {
        try
        {
            StudentResponse? student1 = await _studentService.UpdateStudentAsync(student,studentID);
            return Ok(student1);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new
            {
                ex.Message
            });
        }

    }
}
