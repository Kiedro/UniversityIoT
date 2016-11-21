using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using UniversityIot.VitocontrolApi.DAL;
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
            
            gatewayServiceMock.Verify(x=>x.CreateGateway(It.Is<Gateway>(y=>y.SerialNumber==serialNumber)),Times.Once);

        }
    }
}
