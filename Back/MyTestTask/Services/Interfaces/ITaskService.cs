using MyTestTask.Models;

namespace MyTestTask.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Contact> Get(int id);

        Task<List<Contact>> GetAll();

        Task<Contact> Add(Contact task);

        Task Delete(int taskId);

        Task Update(int taskId, Contact task);
    }
}
