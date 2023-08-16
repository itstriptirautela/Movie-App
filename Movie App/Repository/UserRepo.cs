using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Movie_Api;
using Movie_App.DTO;
using Movie_App.Helpers;
using Movie_App.Model;
using Movie_App.Service;
using System.Security.Cryptography;
using System.Text;

namespace Movie_App.Repository
{
    public class UserRepo: IUserRepo
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<Movies> _movieCollection;
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<Ticket> _ticketCollection;
        private readonly MongoClient _client;
        private readonly ITokenService _tokenService;
        public UserRepo(IConfiguration configuration,ITokenService tokenService)
        {
            _tokenService=tokenService;
            _configuration = configuration;
            _client = new MongoClient(_configuration.GetConnectionString("MovieConnectionString"));
            _movieCollection = _client.GetDatabase("MovieApp").GetCollection<Movies>("Movie");
            _usersCollection = _client.GetDatabase("MovieApp").GetCollection<User>("User");
            _ticketCollection = _client.GetDatabase("MovieApp").GetCollection<Ticket>("Ticket");
        }
        public List<User> GetAllUsers()
        {
            return _usersCollection.AsQueryable().Select(u => new User
            {
                LoginId = u.LoginId,
                firstName = u.firstName,
                lastName = u.lastName,
                email = u.email,
                contactNumber = u.contactNumber
            }).ToList();
        }
        public async Task<string> GetUserId(string loginId)
        {
            var user = await _usersCollection.Find(u => u.LoginId == loginId).FirstOrDefaultAsync();
            if (user != null)
            {
                return (user.LoginId).ToString();
            }
            return null;
        }

        public async Task<UserLoginDTO> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _usersCollection.Find(u => loginDto.LoginId == u.LoginId).FirstOrDefaultAsync();
               
                if (user != null && user.password == loginDto.password)
                {
                    //var token = GenerateToken(user);


                    return new UserLoginDTO
                    {
                        userID = user.Id,
                        LoginId = loginDto.LoginId,
                        firstName = user.firstName,
                        lastName = user.lastName,
                        email = user.email,
                        contactNumber = user.contactNumber,
                        role = user.Roles,
                        token = _tokenService.CreateToken(user)
                    };
                }
                else{
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
        
        
        }
        public async Task<User> Register(User registerDto)
        {
            using var hmac = new HMACSHA512();
            var user = new User
            {
                LoginId = registerDto.LoginId,
                firstName = registerDto.firstName,
                lastName = registerDto.lastName,
                contactNumber = registerDto.contactNumber,
                email = registerDto.email,
                password = registerDto.password,
                confirmPassword = registerDto.confirmPassword,
                Roles = registerDto.Roles
            };
            await _usersCollection.InsertOneAsync(user);

            return new User
            {
                Id = user.Id,
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                contactNumber = user.contactNumber,
               // token = _tokenService.CreateToken(user),
                LoginId = registerDto.LoginId,
                 Roles = registerDto.Roles
            };
        }
        public async Task<string> ForgotPassword(string LoginId, ForgotPasswordDto forgotPasswordDto)
        {

            if (forgotPasswordDto.confirmPassword == forgotPasswordDto.Password)
            {
                

                await _usersCollection.UpdateOneAsync(u => u.LoginId == LoginId,
                                                Builders<User>
                                                .Update
                                                .Set(u => u.password, forgotPasswordDto.Password)
                                                .Set(u => u.confirmPassword, forgotPasswordDto.Password)
                                                );
                return "Password updated";
            }
            return "Password does not match. Try again!!!";

        }

        public async Task<User> GetUserByExactUsername(string LoginId)
        {
            var user = await _usersCollection.Find(u => u.LoginId == LoginId).FirstOrDefaultAsync();

            var userSearchDto = new User
            {
                Id= user.Id,
                LoginId = user.LoginId,
                firstName = user.firstName,
                lastName = user.lastName,
                contactNumber = user.contactNumber,
                email = user.email
            };
            return userSearchDto;
        }
    }
}
