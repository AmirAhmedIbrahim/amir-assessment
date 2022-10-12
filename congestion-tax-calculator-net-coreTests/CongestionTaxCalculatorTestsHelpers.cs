using congestion.calculator;
using System;
using System.Collections.Generic;

namespace Tests
{
    public partial class CongestionTaxCalculatorTests
    {
        public static IEnumerable<object[]> TaxCalcTestSetup
        {
            get
            {
                return new[]
                {
                new object[]
                {
                    new Vehicle {vehicleName="car",vehicleType=6  },
                    new DateTime[]
                    {
                        new DateTime(2013,1,1,1,30,0),
                        new DateTime(2013,2,1,1,30,0)
                    },
                    (decimal)0.0
                },
                new object[]
                {
                    new Vehicle {vehicleName="car",vehicleType=6  },
                    new DateTime[]
                    {
                        new DateTime(2013,2,1,1,30,0),
                        new DateTime(2013,2,1,1,30,0),
                        new DateTime(2013,2,1,6,30,0)
                    },
                    (decimal)13.0
                }
                ,
                new object[]
                {
                    new Vehicle {vehicleName="Emergency",vehicleType=2  },
                    new DateTime[]
                    {
                        new DateTime(2013,2,1,6,15,0),
                        new DateTime(2013,2,1,6,30,0),
                        new DateTime(2013,2,1,6,45,0),
                        new DateTime(2013,2,1,7,00,0),
                        new DateTime(2013,2,1,7,15,0),
                        new DateTime(2013,2,1,7,30,0),
                        new DateTime(2013,2,1,9,30,0)
                    },
                    (decimal)0.0
                }
                 ,
                new object[]
                {
                    new Vehicle {vehicleName="Car",vehicleType=7  },
                    new DateTime[]
                    {
                        new DateTime(2013,2,1,6,15,0),
                        new DateTime(2013,2,1,6,30,0),
                        new DateTime(2013,2,1,6,45,0),
                        new DateTime(2013,2,1,7,00,0),
                        new DateTime(2013,2,1,7,15,0),
                        new DateTime(2013,2,1,7,30,0),
                        new DateTime(2013,2,1,9,30,0)
                    },
                    (decimal)44.0
                }
                ,
                new object[]
                {
                    new Vehicle{vehicleName="Car",vehicleType=10},
                    new DateTime[]
                    {
                        new DateTime(2013,01,14,21,00,00),
                        new DateTime(2013,01,15,21,00,00),
                        new DateTime(2013,02,07,06,23,27),
                        new DateTime(2013,02,07,15,27,00),
                        new DateTime(2013,02,08,06,27,00),
                        new DateTime(2013,02,08,06,20,27),
                        new DateTime(2013,02,08,14,35,00),
                        new DateTime(2013,02,08,15,29,00),
                        new DateTime(2013,02,08,15,47,00),
                        new DateTime(2013,02,08,16,01,00),
                        new DateTime(2013,02,08,16,48,00),
                        new DateTime(2013,02,08,17,49,00),
                        new DateTime(2013,02,08,18,29,00),
                        new DateTime(2013,02,08,18,35,00),
                        new DateTime(2013,03,26,14,25,00),
                        new DateTime(2013,03,28,14,07,27)
                    },
                    (decimal)89.0
                }
            };
            }
        }
    }
}