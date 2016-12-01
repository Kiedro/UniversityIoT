using System.Linq;
using UniversityIot.VitocontrolApi.DAL;
using UniversityIot.VitocontrolApi.Exceptions;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Services
{
    public class GatewayService
    {
        private readonly IGatewayDataService _gatewayDataService;

        /// <summary></summary>
        public GatewayService(IGatewayDataService gatewayDataService)
        {
            _gatewayDataService = gatewayDataService;
        }

        public Gateway RegisterGateway(string serialNumber, User user)
        {
            if (!SerialNumberIsCorrect(serialNumber))
            {
                throw new IncorrectSerialNumberException();
            }
            if (!_gatewayDataService.GatewayExists(serialNumber))
            {
                var gateway = new Gateway(serialNumber);
                _gatewayDataService.CreateGateway(gateway);
                _gatewayDataService.ConnectUserToGateway(user, gateway);
                gateway.Status = Status.Registered;

                return gateway;
            }
            else
            {
                throw new GatewayExistsException(serialNumber);
            }
        }

        private bool SerialNumberIsCorrect(string serialNumber)
        {
            return serialNumber.Length == 16 
                && serialNumber.All(char.IsDigit);
        }
    }
}