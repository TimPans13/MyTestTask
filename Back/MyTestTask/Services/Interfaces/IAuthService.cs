using MyTestTask.Models;

namespace MyTestTask.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginViewModel model);
        Task<string> Register(RegisterViewModel model);
        Task Logout();
    }
}
