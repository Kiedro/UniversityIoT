using Moq;
using NUnit.Framework;
using UniversityIot.VitocontrolApi.DAL;
using UniversityIot.VitocontrolApi.Exceptions;
using UniversityIot.VitocontrolApi.Models;
using UniversityIot.VitocontrolApi.Services;

namespace UniversityIot.VitocontrolApi.Tests
{
    [TestFixture]
    public class GatewayServiceTests
    {
        [Test]
        public void RegisterdGateway_NewRoute_CallsCreateRoute()
        {
            var user = new User();
            var gatewayServiceMock = new Mock<IGatewayDataService>();
            gatewayServiceMock.Setup(x => x.GatewayExists(It.IsAny<string>())).Returns(false);
            var service = new GatewayService(gatewayServiceMock.Object);
            var serialNumber = "1234567812345678";
            service.RegisterGateway(serialNumber, user);

            gatewayServiceMock.Verify(x => x.CreateGateway(It.Is<Gateway>(y => y.SerialNumber == serialNumber)), Times.Once);
        }

        [Test]
        public void RegisterGateway_ExistingRoute_ThrowsExistingRouteException()
        {
            var user = new User();
            var gatewayServiceMock = new Mock<IGatewayDataService>();
            gatewayServiceMock.Setup(x => x.GatewayExists(It.IsAny<string>())).Returns(true);
            var service = new GatewayService(gatewayServiceMock.Object);
            var serialNumber = "1234567812345678";
            Assert.Throws(typeof(GatewayExistsException), () => service.RegisterGateway(serialNumber, user));
        }

        [Test]
        public void RegisterGateway_NewRoute_ConnectsUserToGateway()
        {
            var user = new User();
            var gatewayServiceMock = new Mock<IGatewayDataService>();
            gatewayServiceMock.Setup(x => x.GatewayExists(It.IsAny<string>())).Returns(false);
            var service = new GatewayService(gatewayServiceMock.Object);
            var serialNumber = "1234567812345678";
            service.RegisterGateway(serialNumber, user);

            gatewayServiceMock.Verify(x => x.ConnectUserToGateway(It.IsAny<User>(),
                It.Is<Gateway>(y => y.SerialNumber == serialNumber)), Times.Once);
        }

        [Test]
        public void RegisterGateway_NewRoute_GatewayRegisteredStatusEqualTrue()
        {
            var user = new User();
            var gatewayServiceMock = new Mock<IGatewayDataService>();
            gatewayServiceMock.Setup(x => x.GatewayExists(It.IsAny<string>())).Returns(false);
            var service = new GatewayService(gatewayServiceMock.Object);
            var serialNumber = "1234567812345678";
            var gateway = service.RegisterGateway(serialNumber, user);

            Assert.True(gateway.Status == Status.Registered);
        }

        [Test]
        public void RegisterGateway_CorrectSerialNumber_CreatesGateway()
        {
            var user = new User();
            var gatewayServiceMock = new Mock<IGatewayDataService>();
            var service = new GatewayService(gatewayServiceMock.Object);
            var serialNumber = "1234567812345678";
            var gateway = service.RegisterGateway(serialNumber, user);

            Assert.NotNull(gateway);
        }

        [Test]
        public void RegisterGateway_IncorrectSerialNumber_ThrowsIncorectSerialNumberEception()
        {
            var user = new User();
            var gatewayServiceMock = new Mock<IGatewayDataService>();
            var service = new GatewayService(gatewayServiceMock.Object);
            var serialNumber = "1234";

            Assert.Throws(typeof(IncorrectSerialNumberException), () => service.RegisterGateway(serialNumber, user));
        }

        [Test]
        public void RegisterGateway_SerialNumberWithLetter_ThrowsIncorectSerialNumberEception()
        {
            var user = new User();
            var gatewayServiceMock = new Mock<IGatewayDataService>();
            var service = new GatewayService(gatewayServiceMock.Object);
            var serialNumber = "a123456781234567";

            Assert.Throws(typeof(IncorrectSerialNumberException), () => service.RegisterGateway(serialNumber, user));
        }
    }
}
