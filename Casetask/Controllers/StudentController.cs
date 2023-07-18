using Casetask.Common.Dtos.StudentDTOs;
using Casetask.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Casetask.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private IStudentService StudentService { get; }

    public StudentController(IStudentService studentService)
    {
        StudentService = studentService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateStudent(StudentCreate studentCreate)
    {
        int id = await StudentService.CreateStudentAsync(studentCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateStudent(StudentUpdate studentUpdate)
    {
        await StudentService.UpdateStudentAsync(studentUpdate);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await StudentService.GetStudentAsync(id);
        return Ok(student);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetStudents()
    {
        var students = await StudentService.GetStudentsAsync();
        return Ok(students);
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteStudent(StudentDelete studentDelete)
    {
        await StudentService.DeleteStudentAsync(studentDelete);
        return Ok();
    }
}
