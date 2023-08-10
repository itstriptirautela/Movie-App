using ConsumerMicroserviceTests;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using Movie_App.Model;
using Movie_App.Repository;
using Movie_App.Service;
using NUnit.Framework;

namespace Movie_App_Test
{
    [TestClass]
    public class MovieTest
    {
        private Mock<IMovieRepo> movieMock;
        private Mock<ITicketRepo> ticketMock;
        private Mock<IUserRepo> userMock;
        private SampleRepo sampleRepo;
        private UserService userService;

        [SetUp]
        public void Setup()
        {
            movieMock = new Mock<IMovieRepo>();
            ticketMock = new Mock<ITicketRepo>();
            userMock = new Mock<IUserRepo>();
            sampleRepo = new SampleRepo();
        }
        [TestMethod]
        public async Task Register_ValidUser_Success()
        {
            // Arrange
            var mockUsersCollection = new Mock<IMongoCollection<User>>();
            var mockTokenService = new Mock<ITokenService>();

            var registrationService = new RegistrationService(mockUsersCollection.Object, mockTokenService.Object);

            var registerDto = new User
            {
                LoginId = "testuser",
                FirstName = "John",
                LastName = "Doe",
                ContactNumber = "1234567890",
                Email = "john@example.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            // Act
            var result = await registrationService.Register(registerDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(registerDto.LoginId, result.LoginId);
            Assert.AreEqual(registerDto.FirstName, result.FirstName);
            Assert.AreEqual(registerDto.LastName, result.LastName);
            Assert.AreEqual(registerDto.Email, result.Email);
            Assert.AreEqual(registerDto.ContactNumber, result.ContactNumber);

            // Additional assertions or verification steps can be added as needed.
        }
    }
    has context menu
