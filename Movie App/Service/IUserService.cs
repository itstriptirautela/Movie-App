using Movie_App.DTO;
using Movie_App.Model;

namespace Movie_App.Service
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        Task<UserLoginDTO> Login(LoginDto loginDto);
        Task<User> Register(User registerDto);
        Task<string> ForgotPassword(string username, ForgotPasswordDto forgotPasswordDto);
    }
}
