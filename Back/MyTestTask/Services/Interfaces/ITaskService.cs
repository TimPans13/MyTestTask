using MyTestTask.Models;

namespace MyTestTask.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Contact> Get(string userId, int id);

        Task<List<Contact>> GetAll(string userId);

        Task<Contact> Add(string userId, Contact task);

        Task Delete(string userId, int taskId);

        Task Update(string userId, int taskId, Contact task);
    }
}
