using System;
using System.Collections.Generic;
using Moq;
using UniversityIot.VitocontrolApi.Interfaces;
using UniversityIot.VitocontrolApi.Models;
using UniversityIot.VitocontrolApi.Services;
using Xunit;

namespace UniversityIot.VitocontrolApi.Tests
{
    public class UserServiceTests
    {
        string _userAlreadyExistsMessage = "User already exists";
        string _userDoesNotExistsMessage = "User does not exists";

        [Fact]
        public void ShouldReturnUserByNameWhenUserExists()
        {
            //Arrange
            string userName = "Jan";

            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s.GetUser(userName)).Returns(new User(userName));

            var userService = new UserService(dataServiceMock.Object);                    

            //Act
            var user = userService.GetUser(userName);

            //Assert
            Assert.Equal(userName, user.Name);
        }

        [Fact]
        public void ShouldFailWhenUserDoesNotExists()
        {
            //Arrange
            string userName = "Jula";
            
            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s
                .GetUser(userName))
                .Throws<UserNotFoundException>();
            var userService = new UserService(dataServiceMock.Object);

            //Act      
            Exception ex = Assert.Throws<UserNotFoundException>(() => userService.GetUser(userName));

            //Assert
            Assert.Equal(_userDoesNotExistsMessage, ex.Message);
        }

        [Fact]
        public void ShouldNotAllowCreateUserWithExistingName()
        {
            //Arrange
            string userName = "Jula";
            
            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s
                .AddUser(It.Is<User>(u => u.Name == userName)))
                .Throws<UserAlreadyExistsException>();
            var userService = new UserService(dataServiceMock.Object);

            //Act      
            Exception ex = Assert.Throws<UserAlreadyExistsException>(() => userService.CreateUser(userName));

            //Assert
            Assert.Equal(_userAlreadyExistsMessage, ex.Message);
        }


        [Fact]
        public void ShouldDeleteUserWhenUserExists()
        {
            //Arrange
            string userName = "Jula";

            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s.DeleteUser(userName));
            var userService = new UserService(dataServiceMock.Object);

            //Act      
            bool success = userService.DeleteUser(userName);

            //Assert
            Assert.Equal(success, true);
        }

        [Fact]
        public void ShouldFailWhenDeletingUserDoesNotExists()
        {
            //Arrange
            string userName = "Jula";
            
            var dataServiceMock = new Mock<IUserDataService>();
            dataServiceMock.Setup(s => s
                .DeleteUser(userName))
                .Throws<UserNotFoundException>();
            var userService = new UserService(dataServiceMock.Object);

            //Act      
            Exception ex = Assert.Throws<UserNotFoundException>(() => userService.DeleteUser(userName));

            //Assert
            Assert.Equal(_userDoesNotExistsMessage, ex.Message);
        }
    }
}
