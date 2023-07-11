using Casetask.Common.Dtos.Teacher;
using Casetask.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Casetask.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{
    private ITeacherService TeacherService { get; }
    public TeacherController(ITeacherService teacherService)
    {
        TeacherService = teacherService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateTeacher(TeacherCreate teacherCreate)
    {
        int id = await TeacherService.CreateTeacherAsync(teacherCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateTeacher(TeacherUpdate teacherUpdate)
    {
        await TeacherService.UpdateTeacherAsync(teacherUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteTeacher(TeacherDelete teacherDelete)
    {
        await TeacherService.DeleteTeacherAsync(teacherDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetTeacher(int id)
    {
        var teacher = await TeacherService.GetTeacherAsync(id);
        return Ok(teacher);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetTeachers()
    {
        var teachers = await TeacherService.GetTeachersAsync();
        return Ok(teachers);
    }
}