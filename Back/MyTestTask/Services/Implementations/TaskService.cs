using MyTestTask.Data;
using MyTestTask.Models;
using MyTestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Threading.Tasks;

namespace MyTestTask.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _dbContext;

        public TaskService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contact> Get(int Id)
        {
            try
            {
                var lastAddedTask = await _dbContext.Contacts
                    .FirstOrDefaultAsync(u => u.Id == Id);

                return lastAddedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Get: {ex.Message}");
                throw;
            }
        }
        

        public async Task<List<Contact>> GetAll()
        {
            try
            {
                var tasks = await _dbContext.Contacts
                    .ToListAsync();

                return tasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in GetAll: {ex.Message}");
                throw;
            }
        }

        public async Task<Contact> Add(Contact task)
        {
            try
            {
                _dbContext.Contacts.Add(task);
                await _dbContext.SaveChangesAsync();

                var lastAddedTask = await _dbContext.Contacts
                   .OrderByDescending(u => u.Id)
                .FirstOrDefaultAsync();

                return lastAddedTask;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Add: {ex.Message}");
                throw;
            }
        }


        public async Task Delete(int taskId)
        {
            try
            {
                var taskToDelete = await _dbContext.Contacts.FirstOrDefaultAsync(t => t.Id == taskId);
                if (taskToDelete != null)
                {
                    _dbContext.Contacts.Remove(taskToDelete);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Delete: {ex.Message}");
                throw;
            }
        }



        public async Task Update(int taskId, Contact updatedContact)
        {
            try
            {
                var contactToUpdate = await _dbContext.Contacts.FirstOrDefaultAsync(t => t.Id == taskId);

                if (contactToUpdate != null)
                {
                    contactToUpdate.Name = updatedContact.Name;
                    contactToUpdate.MobilePhone = updatedContact.MobilePhone;
                    contactToUpdate.BirthDate = updatedContact.BirthDate;
                    contactToUpdate.JobTitle = updatedContact.JobTitle;

                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Update: {ex.Message}");
                throw;
            }
        }


    }
}

