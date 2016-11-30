using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using UniversityIot.VitocontrolApi.Interfaces;
using UniversityIot.VitocontrolApi.Models;
using UniversityIot.VitocontrolApi.Services;
using Xunit;

namespace UniversityIot.VitocontrolApi.Tests
{
    public class GatewayServiceTests
    {
        [Fact]
        public void ShouldCreateGatewayWithNewSerialNumberIfItDoesNotExistYet()
        {
            //Arrange
            var user = new User("Jola");
            long serialNmb = 1234567897548785;

            var dataServiceMock = new Mock<IGatewayDataService>();
            dataServiceMock.Setup(s => s.DoesGatewayExist(serialNmb)).Returns(false);

            var gatewayService = new GatewayService(dataServiceMock.Object);

            //Act
            var gateway = gatewayService.RegisterGateway(user, serialNmb);

            //Assert
            Assert.Equal(gateway.SerialNmb, serialNmb);
        }

        [Fact]
        public void ShouldConnectUserWithGateway()
        {
            //Arrange
            var user = new User("Jola");
            long serialNmb = 1234567897548785;

            var dataServiceMock = new Mock<IGatewayDataService>();
            dataServiceMock.Setup(s => s.DoesGatewayExist(serialNmb)).Returns(false);

            var gatewayService = new GatewayService(dataServiceMock.Object);

            //Act
            var gateway = gatewayService.RegisterGateway(user, serialNmb);
            

            //Assert
            Assert.Equal(gateway.User, user);
        }

        [Fact]
        public void ShouldSetGatewayStatusToRegistered()
        {
            //Arrange
            var user = new User("Jola");
            long serialNmb = 1234567897548785;

            var dataServiceMock = new Mock<IGatewayDataService>();
            dataServiceMock.Setup(s => s.DoesGatewayExist(serialNmb)).Returns(false);
            
            var gatewayService = new GatewayService(dataServiceMock.Object);

            //Act
            var gateway = gatewayService.RegisterGateway(user, serialNmb);
           
            //Assert
            Assert.Equal(gateway.Status, GatewayStatus.Registered);
        }
    }
}
