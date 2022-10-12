using System;
using congestion.calculator;
using System.Linq;

public partial class CongestionTaxCalculator : ICongestionTaxCalculator
{
    public ICongestionTaxesConfiguration _congestionTaxesConfiguration;
    public CongestionTaxCalculator(ICongestionTaxesConfiguration congestionTaxesConfiguration)
    {
        _congestionTaxesConfiguration = congestionTaxesConfiguration;
    }
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public decimal GetDayTax(IVehicle vehicle, DateTime[] dates)
    {
        if (dates.Select(d => d.DayOfYear).Distinct().Count() > 1)
            throw new Exception("Only one day allowed for this method");

        // Check if no dates, return 0.
        if (dates == null || dates.Length == 0)
            return 0;

        // Early check the vehicle Type 
        if (vehicle.isTollFreeVehicle)
            return 0;

        if (dates.Length == 1)
            return _congestionTaxesConfiguration.GetTollFee(dates[0]);

        // sort the dates array just in case it passed without sorting.
        //sorted dates so not to change the orignal array as it pass by ref.        
        var datesWithTollFees = dates.Select(d => new
        {
            TollDate = d,
            TollFee = _congestionTaxesConfiguration.GetTollFee(d)
        }).OrderBy(x => x.TollDate).ToList();

        decimal totalFee = 0;
        DateTime maxDate = DateTime.MinValue;

        for (int i = 0; i < datesWithTollFees.Count; i++)
        {
            var curriRecord = datesWithTollFees[i];

            if (curriRecord.TollDate <= maxDate)
                continue;

            maxDate = datesWithTollFees.Where(x => Math.Abs(x.TollDate.Subtract(curriRecord.TollDate).TotalMinutes) < 60)
                .Select(x => x.TollDate).Max();

            var maxFeeDuring60Minute = datesWithTollFees.Where(x => Math.Abs(x.TollDate.Subtract(curriRecord.TollDate).TotalMinutes) < 60)
                .Select(x => x.TollFee).Max();

            totalFee += maxFeeDuring60Minute;
        }

        if (totalFee <= 60)
            return totalFee;
        else
            return 60;
    }

    /// <summary>
    ///  Implemented to get multiple days 
    /// </summary>
    /// <param name="vehicle"></param>
    /// <param name="dates"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public decimal GetTax(IVehicle vehicle, DateTime[] dates)
    {
        var groupedByDay = (from date in dates
                            group date by date.DayOfYear into g
                            select new
                            {
                                DayOfYear = g.Key,
                                DateFees = GetDayTax(vehicle, g.ToArray())
                            }).ToList();


        return groupedByDay.Select(x => x.DateFees).Sum();
    }
}