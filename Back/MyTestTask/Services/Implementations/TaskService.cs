//using MyTestTask.Data;
//using MyTestTask.Models;
//using MyTestTask.Services.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using AutoMapper;
//using System.Threading.Tasks;

//namespace MyTestTask.Services.Implementations
//{
//    public class TaskService : ITaskService
//    {
//        private readonly AppDbContext _dbContext;

//        public TaskService(AppDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<Contact> Get(string userId, int Id)
//        {
//            try
//            {

//                var lastAddedTask = await _dbContext.Contacts
//                      .Where(c => c.UserId == userId)
//                    .FirstOrDefaultAsync(u => u.Id == Id);

//                return lastAddedTask;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred in Get: {ex.Message}");
//                throw;
//            }
//        }


//        public async Task<List<Contact>> GetAll(string userId)
//        {
//            try
//            {

//                var tasks = await _dbContext.Contacts
//                    .ToListAsync();

//                return tasks;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred in GetAll: {ex.Message}");
//                throw;
//            }
//        }

//        public async Task<Contact> Add(string userId,Contact task)
//        {
//            try
//            {
//                task.UserId = userId;

//                _dbContext.Contacts.Add(task);
//                await _dbContext.SaveChangesAsync();

//                var lastAddedTask = await _dbContext.Contacts
//                   .OrderByDescending(u => u.Id)
//                .FirstOrDefaultAsync();

//                return lastAddedTask;

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred in Add: {ex.Message}");
//                throw;
//            }
//        }


//        public async Task Delete(string userId,int taskId)
//        {
//            try
//            {

//                var taskToDelete = await _dbContext.Contacts.Where(c => c.UserId == userId).FirstOrDefaultAsync(t => t.Id == taskId);
//                if (taskToDelete != null)
//                {
//                    _dbContext.Contacts.Remove(taskToDelete);
//                    await _dbContext.SaveChangesAsync();
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred in Delete: {ex.Message}");
//                throw;
//            }
//        }



//        public async Task Update(string userId,int taskId, Contact updatedContact)
//        {
//            try
//            {

//                var contactToUpdate = await _dbContext.Contacts.Where(c => c.UserId == userId).FirstOrDefaultAsync(t => t.Id == taskId);

//                if (contactToUpdate != null)
//                {
//                    contactToUpdate.Name = updatedContact.Name;
//                    contactToUpdate.MobilePhone = updatedContact.MobilePhone;
//                    contactToUpdate.BirthDate = updatedContact.BirthDate;
//                    contactToUpdate.JobTitle = updatedContact.JobTitle;

//                    await _dbContext.SaveChangesAsync();
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred in Update: {ex.Message}");
//                throw;
//            }
//        }


//    }
//}


using MyTestTask.Data;
using MyTestTask.Models;
using MyTestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Contact> Get(string userId, int Id)
        {
            try
            {
                var contact = await _dbContext.Contacts
                    .Where(c => c.UserId == userId && c.Id == Id)
                    .FirstOrDefaultAsync();

                return contact;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Get: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Contact>> GetAll(string userId)
        {
            try
            {
                var contacts = await _dbContext.Contacts
                    .Where(c => c.UserId == userId)
                    .ToListAsync();

                return contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in GetAll: {ex.Message}");
                throw;
            }
        }

        public async Task<Contact> Add(string userId, Contact task)
        {
            try
            {
                task.UserId = userId;

                _dbContext.Contacts.Add(task);
                await _dbContext.SaveChangesAsync();

                var lastAddedTask = await _dbContext.Contacts
                   .Where(c => c.UserId == userId)
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

        public async Task Delete(string userId, int taskId)
        {
            try
            {
                var taskToDelete = await _dbContext.Contacts
                    .Where(c => c.UserId == userId)
                    .FirstOrDefaultAsync(t => t.Id == taskId);

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

        public async Task Update(string userId, int taskId, Contact updatedContact)
        {
            try
            {
                var contactToUpdate = await _dbContext.Contacts
                    .Where(c => c.UserId == userId)
                    .FirstOrDefaultAsync(t => t.Id == taskId);

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


