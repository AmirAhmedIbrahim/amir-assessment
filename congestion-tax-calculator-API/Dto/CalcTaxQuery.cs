using System;

namespace congestion_tax_calculator_API.Dto
{
    public enum VehicleTypeEnum
    {
        Motorcycle = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5,
        Car = 6,
        Motorbike = 7,
        other = 8,
    }

    public class CalcTaxQuery
    {
        public string vehicleName { get; set; }
        public VehicleTypeEnum vehicleType { get; set; }
        public DateTime[] dates { get; set; }
    }
}