using congestion.calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass()]
    public partial class CongestionTaxCalculatorTests
    {
        ICongestionTaxCalculator _congestionTaxCalculator;
        ICongestionTaxesConfiguration _congestionTaxesConfiguration;
        public CongestionTaxCalculatorTests()
        {
            _congestionTaxesConfiguration = new CongestionTaxesConfiguration();
            _congestionTaxCalculator = new CongestionTaxCalculator(_congestionTaxesConfiguration);
        }

        [TestMethod()]
        [DynamicData(nameof(TaxCalcTestSetup))]
        public void GetTaxTest(IVehicle vehicle, DateTime[] dates, decimal expectedResult)
        {
            //Act test
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);

            //Assert test
            Assert.AreEqual(expectedResult, result);
        }
    }
}