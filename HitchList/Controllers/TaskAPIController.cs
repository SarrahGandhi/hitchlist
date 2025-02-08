using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using HitchList.Data;
using Microsoft.EntityFrameworkCore;
using HitchList.Models;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")] // Base route is "api/TaskAPI"
public class TaskAPIController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TaskAPIController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the list of all tasks.
    /// </summary>
    /// <returns>A list of all tasks.</returns>
    [HttpGet("Tasks")] // Combined route: "api/TaskAPI/Task"
    public async Task<ActionResult<IEnumerable<HitchList.Models.Task>>> GetTask()
    {
        // Fetch all tasks from the database
        List<HitchList.Models.Task> tasks = await _context.Task.ToListAsync();
        List<HitchList.Models.Task> taskList = new List<HitchList.Models.Task>();

        // Iterate through tasks and create a list of tasks
        foreach (var taskItem in tasks)
        {
            taskList.Add(new HitchList.Models.Task()
            {
                Id = taskItem.Id,
                TaskName = taskItem.TaskName,
                TaskDescription = taskItem.TaskDescription,
                TaskDueDate = taskItem.TaskDueDate,
                TaskCategory = taskItem.TaskCategory
            });
        }

        return Ok(taskList); // Return list of tasks
    }
}
