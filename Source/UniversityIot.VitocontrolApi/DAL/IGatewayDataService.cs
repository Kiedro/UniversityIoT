using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.DAL
{
    public interface IGatewayDataService
    {
        void CreateGateway(Gateway gateway);
        bool GatewayExists(string serialNumber);
    };
}