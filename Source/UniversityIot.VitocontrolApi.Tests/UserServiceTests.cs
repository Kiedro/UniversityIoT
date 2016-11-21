using System;
using Moq;
using NUnit.Framework;
using UniversityIot.VitocontrolApi.DAL;
using UniversityIot.VitocontrolApi.Models;
using UniversityIot.VitocontrolApi.Services;

namespace UniversityIot.VitocontrolApi.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        [Test]
        public void GetUser_ExistingUserName_UserObject()
        {
            var userName = "Kowalski";
            var userDataServiceMoq = new Mock<IUserDataService>();
            userDataServiceMoq.Setup(x => x.FindUser(userName)).Returns(new User { UserName = userName });
            var userService = new UserService(userDataServiceMoq.Object);

            var result = userService.GetUser(userName);

            Assert.NotNull(result);
            Assert.AreEqual(userName, result.UserName);
        }

        [Test]
        public void GetUser_NonExistingUserName_ThrowException()
        {
            var userName = "Nowak";
            IUserDataService userDataService = new Mock<IUserDataService>().Object;
            var userService = new UserService(userDataService);

            Assert.Throws(typeof(InvalidOperationException), () => userService.GetUser(userName));
        }

        [Test]
        public void CreateUser_NonExistingUserName_CreateUser()
        {
            var userName = "Nowak";
            var dataServiceMoq = new Mock<IUserDataService>();
            var userService = new UserService(dataServiceMoq.Object);

            var result = userService.CreateUser(userName);

            dataServiceMoq.Verify(m => m.AddUser(It.Is<User>(x => x.UserName == userName)));
            Assert.IsTrue(result);
        }

        [Test]
        public void CreateUser_ExistingUserName_False()
        {
            var userName = "Kowalski";
            var userDataServiceMoq = new Mock<IUserDataService>();
            userDataServiceMoq.Setup(x => x.FindUser(userName)).Returns(new User { UserName = userName });
            var userService = new UserService(userDataServiceMoq.Object);

            bool result = userService.CreateUser(userName);

            Assert.IsFalse(result);
            userDataServiceMoq.Verify(x=>x.AddUser(It.IsAny<User>()),Times.Never);
        }

        [Test]
        public void DeleteUser_ExistingUserName_CallDataServiceDeleteUser()
        {
            var userName = "Kowalski";
            var userDataServiceMoq = new Mock<IUserDataService>();
            userDataServiceMoq.Setup(x => x.FindUser(userName)).Returns(new User { UserName = userName });
            var userService = new UserService(userDataServiceMoq.Object);

            userService.DeleteUser(userName);

            userDataServiceMoq.Verify(m => m.RemoveUser(It.Is<string>(x => x == userName)));
        }

        [Test]
        public void DeleteUser_NonExistingUserName_Fails()
        {
            var userName = "Nowak";
            var dataServiceMoq = new Mock<IUserDataService>();
            var userService = new UserService(dataServiceMoq.Object);
           
            // todo create specific exception type
            Assert.Throws(typeof(InvalidOperationException), () => userService.DeleteUser(userName));

            dataServiceMoq.Verify(x=>x.RemoveUser(It.IsAny<string>()),Times.Never);
        }
    }
}
