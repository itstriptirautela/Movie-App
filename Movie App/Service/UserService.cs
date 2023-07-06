using Movie_App.Repository;

using Movie_App.Model;
using Movie_App.DTO;

namespace Movie_App.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepo _userRepository;

        public UserService(IUserRepo userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public async Task<UserLoginDTO> Login(LoginDto loginDto)
        {
            return await _userRepository.Login(loginDto);
        }
        public async Task<User> Register(User registerDto)
        {
            return await _userRepository.Register(registerDto);
        }
        public async Task<string> ForgotPassword(string username, ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _userRepository.GetUserByExactUsername(username);
            if (user == null)
            {
                return null;
            }
            return await _userRepository.ForgotPassword(username, forgotPasswordDto);
        }

    }
}
