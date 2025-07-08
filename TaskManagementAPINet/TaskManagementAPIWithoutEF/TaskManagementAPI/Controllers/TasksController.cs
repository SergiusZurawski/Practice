// Controllers/TasksController.cs

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

[ApiController]
[ApiVersion("1.0")] // ✅ Declare supported API version
[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // [HttpGet]
    // public async Task<IActionResult> GetAll()
    // {
    //     var tasks = await _taskService.GetAllTasksAsync();
    //     return Ok(tasks);
    // }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
            return Unauthorized();

        int userId = int.Parse(userIdClaim); // parse or convert safely
        var tasks = await _taskService.GetTasksForUserAsync(userId); // new method
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        var newId = await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(Get), new { id = newId }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem task)
    {
        if (id != task.Id)
            return BadRequest("ID mismatch");

        var exists = await _taskService.GetTaskByIdAsync(id);
        if (exists == null)
            return NotFound();

        var success = await _taskService.UpdateTaskAsync(task);
        return success ? NoContent() : StatusCode(500);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _taskService.DeleteTaskAsync(id);
        return success ? NoContent() : NotFound();
    }
}
