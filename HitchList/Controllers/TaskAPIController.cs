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
    [HttpGet("Tasks")] // Combined route: "api/TaskAPI/Task"
    public async Task<ActionResult<IEnumerable<HitchList.Models.Task>>> GetTask()
    {
        List<HitchList.Models.Task> tasks = await _context.Task.ToListAsync();
        List<HitchList.Models.Task> taskList = new List<HitchList.Models.Task>();
        foreach (var taskItem in tasks)
        {
            taskList.Add(new HitchList.Models.Task()
            {
                Id = taskItem.Id,
                TaskName = taskItem.TaskName,
                TaskDescription = taskItem.TaskDescription,
                TaskDueDate = taskItem.TaskDueDate,
                TaskStatus = taskItem.TaskStatus,
                TaskCategory = taskItem.TaskCategory
                // Corrected property name from TaskName to TaskUsername
            });

        }
        return Ok(taskList); // Moved outside the loop
    }
    [HttpGet(template: "Task{id}")]
    public async Task<ActionResult<HitchList.Models.Task>> FindTask(int id)
    {
        var taskItem = await _context.Task.FindAsync(id);
        if (taskItem == null)
        {
            return NotFound();
        }
        HitchList.Models.Task tasks = new HitchList.Models.Task()
        {
            Id = taskItem.Id,
            TaskName = taskItem.TaskName,
            TaskDescription = taskItem.TaskDescription,
            TaskDueDate = taskItem.TaskDueDate,
            TaskStatus = taskItem.TaskStatus,
            TaskCategory = taskItem.TaskCategory

            // Corrected property name from TaskName to TaskUsername
        };
        return Ok(tasks);
    }
    [HttpPost(template: "AddTask")]
    public async Task<ActionResult<HitchList.Models.Task>> AddTask(TaskDto addTask)
    {
        HitchList.Models.Task taskItem = new HitchList.Models.Task()
        {
            TaskName = addTask.TaskName,
            TaskDescription = addTask.TaskDescription,
            TaskDueDate = addTask.TaskDate,
            TaskStatus = addTask.TaskStatus,
            TaskCategory = addTask.TaskCategory
        };
        _context.Task.Add(taskItem);
        await _context.SaveChangesAsync();
        TaskDto taskDto = new TaskDto()
        {

            TaskName = taskItem.TaskName,
            TaskDescription = taskItem.TaskDescription,
            TaskDate = taskItem.TaskDueDate != default(DateTime) ? taskItem.TaskDueDate : DateTime.Now,
            TaskStatus = taskItem.TaskStatus,
            TaskCategory = taskItem.TaskCategory
            // Corrected property name from TaskName to TaskUsername
        };
        return CreatedAtAction("FindTask", new { id = taskItem.Id }, taskDto);
    }
    [HttpPut("UpdateTask/{id}")]
    public async Task<IActionResult> UpdateTask(int id, HitchList.Models.Task updateTask)
    {
        var tasks = await _context.Task.FindAsync(id);
        if (tasks == null)
        {
            return NotFound();
        }
        tasks.Id = id;
        tasks.TaskName = updateTask.TaskName;
        tasks.TaskDescription = updateTask.TaskDescription;
        tasks.TaskDueDate = updateTask.TaskDueDate;
        tasks.TaskStatus = updateTask.TaskStatus;
        tasks.TaskCategory = updateTask.TaskCategory;

        _context.Entry(tasks).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();

        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var taskItem = await _context.Task.FindAsync(id);
        if (taskItem == null)
        {
            return NotFound();
        }
        _context.Task.Remove(taskItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool TaskExists(int id)
    {
        return _context.Task.Any(e => e.Id == id);
    }
    // FInd all tasks by event
    [HttpGet("Task/{eventid}")]
    public async Task<ActionResult<IEnumerable<HitchList.Models.Task>>> GetTaskByEvent(int eventid)
    {
        List<HitchList.Models.Task> tasks = await _context.Task.Where(t => t.EventId == eventid).ToListAsync();
        List<HitchList.Models.Task> taskList = new List<HitchList.Models.Task>();
        foreach (var taskItem in tasks)
        {
            taskList.Add(new HitchList.Models.Task()
            {
                Id = taskItem.Id,
                TaskName = taskItem.TaskName,
                TaskDescription = taskItem.TaskDescription,
                TaskDueDate = taskItem.TaskDueDate,
                TaskStatus = taskItem.TaskStatus,
                TaskCategory = taskItem.TaskCategory
                // Corrected property name from TaskName to TaskUsername
            });

        }
        return Ok(taskList); // Moved outside the loop
    }
}