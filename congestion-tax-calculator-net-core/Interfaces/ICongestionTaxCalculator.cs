using System;

namespace congestion.calculator
{
    public interface ICongestionTaxCalculator
    {
        public decimal GetTax(IVehicle vehicle, DateTime[] dates);

    }
}