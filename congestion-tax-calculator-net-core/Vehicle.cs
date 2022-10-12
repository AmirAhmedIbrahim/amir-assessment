using System;
using static VehiclesEnums;

namespace congestion.calculator
{
    public class Vehicle : IVehicle
    {
        public string vehicleName { get; set; } = string.Empty;

        public int vehicleType { get; set; } = -1;
        public bool isTollFreeVehicle
        {
            get
            {
                return Enum.IsDefined(typeof(TollFreeVehicles), vehicleType);
            }
        }
    }
}