using Microsoft.EntityFrameworkCore;
using MyTestTask.Data;
using MyTestTask.Models;
using MyTestTask.Services.Implementations;


namespace MyTestTask.Tests
{
    public class TaskServiceTests
    {

        [Fact]
        public async Task Get_ValidId_ReturnsContact()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new AppDbContext(options);
            dbContext.Contacts.RemoveRange(dbContext.Contacts);
            dbContext.SaveChanges();

            var taskService = new TaskService(dbContext);

            var contactId = 1;
            var contactEntity = new Contact
            {
                Id = contactId,
                Name = "Anton",
                MobilePhone = "+333",
                JobTitle = "Senior",
                BirthDate = DateTime.Parse("2023-11-14"),
                UserId= "user123"
            };

            dbContext.Contacts.Add(contactEntity);
            dbContext.SaveChanges();

            var result = await taskService.Get("user123", contactId);

            Assert.NotNull(result);
            Assert.Equal(contactId, result.Id);
            Assert.Equal("Anton", result.Name);
            Assert.Equal("+333", result.MobilePhone);
            Assert.Equal("Senior", result.JobTitle);
            Assert.Equal(DateTime.Parse("2023-11-14"), result.BirthDate);
        }


        [Fact]
        public async Task GetAll_ReturnsListOfContacts()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new AppDbContext(options);
            dbContext.Contacts.RemoveRange(dbContext.Contacts);
            dbContext.SaveChanges();

            var taskService = new TaskService(dbContext);

            var contacts = new List<Contact>
            {
                new Contact { Name = "John", MobilePhone = "+123", JobTitle = "Developer", BirthDate = DateTime.Parse("1999-01-14"),UserId= "user456" },
                new Contact { Name = "Jane", MobilePhone = "+456", JobTitle = "Manager", BirthDate = DateTime.Parse("1995-11-13"),UserId= "user456" }
            };

            dbContext.Contacts.AddRange(contacts);
            dbContext.SaveChanges();

            var result = await taskService.GetAll("user456");

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.Equal("John", result[0].Name);
            Assert.Equal("+123", result[0].MobilePhone);
            Assert.Equal("Developer", result[0].JobTitle);
            Assert.Equal(DateTime.Parse("1999-01-14"), result[0].BirthDate);

            Assert.Equal("Jane", result[1].Name);
            Assert.Equal("+456", result[1].MobilePhone);
            Assert.Equal("Manager", result[1].JobTitle);
            Assert.Equal(DateTime.Parse("1995-11-13"), result[1].BirthDate);
        }


        [Fact]
        public async Task Add_ValidContact_ReturnsMappedContact()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new AppDbContext(options);

            var taskService = new TaskService(dbContext);

            var contact = new Contact
            {
                Name = "John Doe",
                MobilePhone = "+1234567890",
                JobTitle = "Developer",
                BirthDate = DateTime.Parse("1990-01-01T00:00:00.000Z")
            };

            var result = await taskService.Add("user789", contact);

            Assert.NotNull(result);
            Assert.Equal(contact.Name, result.Name);
            Assert.Equal(contact.MobilePhone, result.MobilePhone);
            Assert.Equal(contact.JobTitle, result.JobTitle);
            Assert.Equal(contact.BirthDate, result.BirthDate);

        }


        [Fact]
        public async Task Delete_ExistingContact_DeletesContact()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new AppDbContext(options);
            dbContext.Contacts.RemoveRange(dbContext.Contacts);
            dbContext.SaveChanges();

            var taskService = new TaskService(dbContext);

            var contactIdToDelete = 1;
            var contactEntity = new Contact
            {
                Id = contactIdToDelete,
                Name = "ContactToDelete",
                MobilePhone = "+111",
                JobTitle = "ToBeDeleted",
                BirthDate = DateTime.Now,
                UserId= "user321"

            };

            dbContext.Contacts.Add(contactEntity);
            dbContext.SaveChanges();

            await taskService.Delete("user321", contactEntity.Id);

            var deletedContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == contactIdToDelete);
            Assert.Null(deletedContact);
        }


        [Fact]
        public async Task Update_ExistingContact_UpdatesContact()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var dbContext = new AppDbContext(options);
            dbContext.Contacts.RemoveRange(dbContext.Contacts);
            dbContext.SaveChanges();

            var taskService = new TaskService(dbContext);

            var contactIdToUpdate = 1;
            var existingContact = new Contact
            {
                Id = contactIdToUpdate,
                Name = "ExistingContact",
                MobilePhone = "+222",
                JobTitle = "OriginalJob",
                BirthDate = DateTime.Now,
                UserId= "user654"

            };

            dbContext.Contacts.Add(existingContact);
            dbContext.SaveChanges();

            var updatedContact = new Contact
            {
                Id = contactIdToUpdate,
                Name = "UpdatedContact",
                MobilePhone = "+333",
                JobTitle = "NewJob",
                BirthDate = DateTime.Parse("1990-01-01T00:00:00.000Z")
            };

            await taskService.Update("user654", contactIdToUpdate, updatedContact);

            var modifiedContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == contactIdToUpdate);
            Assert.NotNull(modifiedContact);
            Assert.Equal("UpdatedContact", modifiedContact.Name);
            Assert.Equal("+333", modifiedContact.MobilePhone);
            Assert.Equal("NewJob", modifiedContact.JobTitle);
            Assert.Equal(DateTime.Parse("1990-01-01T00:00:00.000Z"), modifiedContact.BirthDate);
        }
    }
}

