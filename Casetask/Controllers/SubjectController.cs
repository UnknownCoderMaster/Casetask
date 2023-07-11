using Casetask.Common.Dtos.SubjectDtos;
using Casetask.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Casetask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectController : ControllerBase
{
    private ISubjectService SubjectService { get; }
    public SubjectController(ISubjectService subjectService)
    {
        SubjectService = subjectService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateSubject(SubjectCreate subjectCreate)
    {
        int id = await SubjectService.CreateSubjectAsync(subjectCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateSubject(SubjectUpdate subjectUpdate)
    {
        await SubjectService.UpdateSubjectAsync(subjectUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteSubject(SubjectDelete subjectDelete)
    {
        await SubjectService.DeleteSubjectAsync(subjectDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetSubject(int id)
    {
        var subject = await SubjectService.GetSubjectAsync(id);
        return Ok(subject);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetSubjects()
    {
        var subjects = await SubjectService.GetSubjectsAsync();
        return Ok(subjects);
    }
}
