namespace congestion.calculator
{
    public interface IVehicle
    {
        public string vehicleName { get; set; }
        public int vehicleType { get; set; }
        public bool isTollFreeVehicle { get; }
    }
}