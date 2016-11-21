namespace UniversityIot.VitocontrolApi.Models
{
    public class Gateway
    {
        public Gateway(string serialNumber)
        {
            SerialNumber = serialNumber;
        }

        public string SerialNumber { get; set; }
    }
}