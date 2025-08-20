using Microsoft.AspNetCore.Mvc;
using ToDoApp.Api.DTOs.Api;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.DTOs.TaskItem;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entities;

namespace ToDoApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost(Name = "CreateTask")]
        public async Task<ActionResult<ToDoAppResponse<bool>>> Create([FromBody] TaskItemInput input)
        {
            try
            {
                await _taskService.AddTaskAsync(input);
                return new ToDoAppResponse<bool> { IsSuccess = true, Message = "Tarefa criada com sucesso" };
            }
            catch (Exception ex)
            {
                return BadRequest(new ToDoAppResponse<bool> { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpGet("{id:int}", Name = "GetTaskById")]
        public async Task<ActionResult<ToDoAppResponse<TaskItem>>> GetById(int id)
        {
            try
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                return new ToDoAppResponse<TaskItem> { IsSuccess = true, Data = task };
            }
            catch (Exception ex)
            {
                return NotFound(new ToDoAppResponse<TaskItem> { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPost("list", Name = "GetAllTasks")]
        public async Task<ActionResult<ToDoAppResponse<PagedResult<TaskItem>>>> GetAll([FromBody] TaskItemListInput input)
        {
            try
            {
                var pagedTasks = await _taskService.GetAllTasksAsync(input);
                return new ToDoAppResponse<PagedResult<TaskItem>> { IsSuccess = true, Data = pagedTasks };
            }
            catch (Exception ex)
            {
                return BadRequest(new ToDoAppResponse<PagedResult<TaskItem>> { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPut(Name = "UpdateTask")]
        public async Task<ActionResult<ToDoAppResponse<bool>>> Update([FromBody] TaskItemUpdateInput input)
        {
            try
            {
                await _taskService.UpdateTaskAsync(input);
                return new ToDoAppResponse<bool> { IsSuccess = true, Message = "Tarefa atualizada com sucesso" };
            }
            catch (Exception ex)
            {
                return BadRequest(new ToDoAppResponse<bool> { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteTask")]
        public async Task<ActionResult<ToDoAppResponse<bool>>> Delete(int id)
        {
            try
            {
                await _taskService.DeleteTaskAsync(id);
                return new ToDoAppResponse<bool> { IsSuccess = true, Message = "Tarefa deletada com sucesso" };
            }
            catch (Exception ex)
            {
                return BadRequest(new ToDoAppResponse<bool> { IsSuccess = false, Message = ex.Message });
            }
        }

        [HttpPatch("complete", Name = "UpdateTaskCompletion")]
        public async Task<ActionResult<ToDoAppResponse<bool>>> UpdateCompletion([FromBody] TaskCompletionInput input)
        {
            try
            {
                await _taskService.UpdateTaskCompletionAsync(input);
                return new ToDoAppResponse<bool>
                {
                    IsSuccess = true,
                    Message = input.IsCompleted ? "Tarefa marcada como completa" : "Tarefa marcada como incompleta"
                };
            }
            catch (Exception ex)
            {
                return BadRequest(new ToDoAppResponse<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                });
            }
        }

    }
}
