namespace UniversityIot.VitocontrolApi.Models
{
    public class Gateway
    {
        public string SerialNumber { get; set; }
        public Status Status { get; set; }

        public Gateway(string serialNumber)
        {
            SerialNumber = serialNumber;
            Status = Status.Unregistered;
        }
    }

    public enum Status
    {
        Registered,
        Unregistered
    }
}