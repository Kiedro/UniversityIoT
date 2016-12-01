using System;

namespace UniversityIot.VitocontrolApi.Exceptions
{
    public class GatewayExistsException : Exception
    {
        public string SerialNumber { get; set; }

        public GatewayExistsException(string serialNumber)
        {
            SerialNumber = serialNumber;
        }
    }
}