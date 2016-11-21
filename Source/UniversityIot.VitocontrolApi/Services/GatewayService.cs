using System;
using UniversityIot.VitocontrolApi.DAL;
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

        public void RegisterGateway(string serialNumber, User user)
        {
            if (!_gatewayDataService.GatewayExists(serialNumber))
            {
                var gateway = new Gateway(serialNumber);
                _gatewayDataService.CreateGateway(gateway);
            }
        }
    }
}