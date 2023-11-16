using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyTestTask.Models;
using MyTestTask.Services.Interfaces;

namespace MyTestTask.Controllers
{

    [Route("api/v1/contacts")]
    [ApiController]
    public class ContactControllers: ControllerBase
    {
        private readonly ITaskService taskService;
        private readonly IMapper _mapper;

        public ContactControllers(ITaskService taskService, IMapper mapper)
        {
            this.taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("Task-GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await this.taskService.GetAll();
                var mappedResult = _mapper.Map<List<ContactDTO>>(result);
                return this.Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to Get all contacts: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("Task-Get/{taskId}")]
        public async Task<IActionResult> Get(int taskId)
        {
            try
            {
                var result = await this.taskService.Get(taskId);
                var mappedResult = _mapper.Map<ContactDTO>(result);
                return this.Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to Get all contacts: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("Task-Add")]
        public async Task<IActionResult> Add([FromBody] Contact task)
        {
            try
            {
                var result = await this.taskService.Add(task);
                var mappedResult = _mapper.Map<ContactDTO>(result);
                return this.Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to add contact: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Task-Delete/{taskId}")]
        public async Task<IActionResult> Delete(int taskId)
        {
            try
            {
                await this.taskService.Delete(taskId);
                //return this.Ok($"Task with ID {taskId} deleted successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to delete contact: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Task-Update/{taskId}")]
        public async Task<IActionResult> Update(int taskId, [FromBody] Contact task)
        {
            try
            {
                await this.taskService.Update(taskId, task);
                var updatedTask = await this.taskService.Get(taskId);
                var mappedResult = _mapper.Map<ContactDTO>(updatedTask);
                return this.Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return this.BadRequest($"Failed to Update contact: {ex.Message}");
            }
        }
    }
}

