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

        [TestMethod()]
        public void GetTax_MaxPerDay_Equal60()
        {
            IVehicle vehicle = new Vehicle() { vehicleName = "Car", vehicleType = 10 };
            
            DateTime[] dates = new DateTime[]
            {
                new DateTime(2013,2,7,6,0,0),
                new DateTime(2013,2,7,6,30,0),
                new DateTime(2013,2,7,7,30,0),
                new DateTime(2013,2,7,8,0,0),
                new DateTime(2013,2,7,9,0,0),
                new DateTime(2013,2,7,10,0,0),
                new DateTime(2013,2,7,11,0,0),
                new DateTime(2013,2,7,12,0,0),
                new DateTime(2013,2,7,13,0,0),
                new DateTime(2013,2,7,14,0,0),
                new DateTime(2013,2,7,15,0,0),
                new DateTime(2013,2,7,16,0,0),
                new DateTime(2013,2,7,17,0,0),
                new DateTime(2013,2,7,18,0,0),
                new DateTime(2013,2,7,19,0,0),
                new DateTime(2013,2,7,20,0,0),
            };
            //Act test
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);

            //Assert test
            Assert.AreEqual(60, result);
        }

        [TestMethod()]
        public void GetTax_WeekEndAndPublicHolidays_Equal0()
        {
            IVehicle vehicle = new Vehicle() { vehicleName = "Car", vehicleType = 10 };

            //add every 30 min            
            DateTime[] dates = new DateTime[]
            {
                new DateTime(2013,2,9,7,30,0),
                new DateTime(2013,2,9,8,0,0),
                new DateTime(2013,2,10,6,0,0),
                new DateTime(2013,2,10,6,30,0),
                new DateTime(2013,12,24,6,0,0),
                new DateTime(2013,12,25,6,0,0),
            };
            //Act test
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);

            //Assert test
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void GetTax_FreeVehicle_Equal0()
        {
            IVehicle vehicle = new Vehicle() { vehicleName = "Foreign", vehicleType = 4 };

            DateTime[] dates = new DateTime[]
            {
                new DateTime(2013,2,7,6,0,0),
                new DateTime(2013,2,7,6,30,0),
                new DateTime(2013,2,7,7,30,0),
                new DateTime(2013,2,7,8,0,0),
                new DateTime(2013,2,7,9,0,0),
                new DateTime(2013,2,7,10,0,0),
                new DateTime(2013,2,7,11,0,0),
                new DateTime(2013,2,7,12,0,0),
                new DateTime(2013,2,7,13,0,0),
                new DateTime(2013,2,7,14,0,0),
                new DateTime(2013,2,7,15,0,0),
                new DateTime(2013,2,7,16,0,0),
                new DateTime(2013,2,7,17,0,0),
                new DateTime(2013,2,7,18,0,0),
                new DateTime(2013,2,7,19,0,0),
                new DateTime(2013,2,7,20,0,0),
            };
            //Act test
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);

            //Assert test
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void GetTax_SingleChargeRole_EqualMaxValue()
        {
            IVehicle vehicle = new Vehicle() { vehicleName = "Car", vehicleType = 10 };

            DateTime[] dates = new DateTime[]
            {
                /*
                        Time	        Amount
                        06:00–06:29	    SEK 8
                        06:30–06:59	    SEK 13
                        07:00–07:59	    SEK 18
                        08:00–08:29	    SEK 13
                        08:30–14:59	    SEK 8
                        15:00–15:29	    SEK 13
                        15:30–16:59	    SEK 18
                        17:00–17:59	    SEK 13
                        18:00–18:29	    SEK 8
                        18:30–05:59	    SEK 0
                */
                new DateTime(2013,2,7,6,0,0),   //8
                new DateTime(2013,2,7,6,30,0),  //13
                new DateTime(2013,2,7,6,45,0),  //13
                new DateTime(2013,2,7,6,59,0),   //13                
            };
            //Act test
            var result = _congestionTaxCalculator.GetTax(vehicle, dates);

            //Assert test
            Assert.AreEqual(13, result);
        }
    }
}