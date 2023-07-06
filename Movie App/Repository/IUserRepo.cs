using MongoDB.Driver.Linq;
using Movie_App.DTO;
using Movie_App.Model;

namespace Movie_App.Repository
{
    public interface IUserRepo
    {
        List<User> GetAllUsers();
        Task<User> Register(User registerDto);
        Task<UserLoginDTO> Login(LoginDto loginDto);
        Task<string> GetUserId(string LoginId);
        Task<string> ForgotPassword(string LoginId, ForgotPasswordDto forgotPasswordDto);
        Task<User> GetUserByExactUsername(string LoginId);
    }
}
