using System;

namespace congestion.calculator
{
    public interface ICongestionTaxesConfiguration
    {
        public decimal GetTollFee(DateTime date);
        public bool IsTollFreeDate(DateTime date);        
    }
}