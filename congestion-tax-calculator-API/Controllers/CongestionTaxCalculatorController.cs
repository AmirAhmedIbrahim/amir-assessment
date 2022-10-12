using congestion.calculator;
using congestion_tax_calculator_API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace congestion_tax_calculator_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CongestionTaxCalculatorController : ControllerBase
    {
        ICongestionTaxCalculator _congestionTaxCalculator;
        public CongestionTaxCalculatorController(ICongestionTaxCalculator congestionTaxCalculator)
        {
            _congestionTaxCalculator = congestionTaxCalculator;
        }

        /// <summary>
        /// Calculate Tax using default Calc Tax behaviour
        /// </summary>
        /// <param name="query">
        ///   VehicleTypeEnum
        ///     Motorcycle = 0,
        ///     Tractor = 1,
        ///     Emergency = 2,
        ///     Diplomat = 3,
        ///     Foreign = 4,
        ///     Military = 5,
        ///     Car = 6,
        ///     Motorbike = 7,
        ///     other = 8,
        /// 

        /// </param>        
        /// <returns>calulated fees</returns>
        /// <remarks>
        /// </remarks>
        [HttpPost("calculate-tax")]
        public decimal GetTax([FromBody] CalcTaxQuery query)
        {
            var vehicle = new Vehicle()
            {
                vehicleName = query.vehicleName,
                vehicleType = (int)query.vehicleType
            };

            return _congestionTaxCalculator.GetTax(vehicle, query.dates);
        }
    }
}
