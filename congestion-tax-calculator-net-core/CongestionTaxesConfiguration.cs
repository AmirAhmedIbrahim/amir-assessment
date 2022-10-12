using System;
using System.Linq;

namespace congestion.calculator
{
    public class CongestionTaxesConfiguration : ICongestionTaxesConfiguration
    {
        /*
           Time	        Amount
           06:00–06:29	SEK 8
           06:30–06:59	SEK 13
           07:00–07:59	SEK 18
           08:00–08:29	SEK 13
           08:30–14:59	SEK 8
           15:00–15:29	SEK 13
           15:30–16:59	SEK 18
           17:00–17:59	SEK 13
           18:00–18:29	SEK 8
           18:30–05:59	SEK 0
         */
        
        private class HoursCongestionTax
        {
            public TimeSpan startTime { get; set; }
            public TimeSpan endTime { get; set; }
            public decimal amount { get; set; }
        }

        private HoursCongestionTax[] hoursCongestionTaxesConfiguration = new HoursCongestionTax[]
        {
            new HoursCongestionTax() {startTime = new TimeSpan(6,0,0),endTime= new TimeSpan(6,29,0),amount=8},
            new HoursCongestionTax() {startTime = new TimeSpan(6,30,0),endTime= new TimeSpan(6,59,0),amount=13},
            new HoursCongestionTax() {startTime = new TimeSpan(7,0,0),endTime= new TimeSpan(7,59,0),amount=18},
            new HoursCongestionTax() {startTime = new TimeSpan(8,0,0),endTime= new TimeSpan(8,29,0),amount=13},
            new HoursCongestionTax() {startTime = new TimeSpan(8,30,0),endTime= new TimeSpan(14,59,0),amount=8},
            new HoursCongestionTax() {startTime = new TimeSpan(15,0,0),endTime= new TimeSpan(15,29,0),amount=13},
            new HoursCongestionTax() {startTime = new TimeSpan(15,30,0),endTime= new TimeSpan(16,59,0),amount=18},
            new HoursCongestionTax() {startTime = new TimeSpan(17,0,0),endTime= new TimeSpan(17,59,0),amount=13},
            new HoursCongestionTax() {startTime = new TimeSpan(18,0,0),endTime= new TimeSpan(18,29,0),amount=8},
            new HoursCongestionTax() {startTime = new TimeSpan(18,30,0),endTime= new TimeSpan(11,59,0),amount=0},
            new HoursCongestionTax() {startTime = new TimeSpan(0,0,0),endTime= new TimeSpan(5,59,0),amount=0},
        };

        public decimal GetTollFee(DateTime date)
        {
            if (IsTollFreeDate(date)) return 0;

            var tollFee =  (from hct in hoursCongestionTaxesConfiguration
                    where CheckTimeSpanInterval(hct.startTime, hct.endTime, date.TimeOfDay)
                    select hct.amount).FirstOrDefault();

            return tollFee;            
        }

        public bool IsTollFreeDate(DateTime date)
        {
            // Could be enhanced using dictionary or something like this https://github.com/martinjw/Holiday
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;

            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

            if (year == 2013)
            {
                if ((month == 1 && day == 1)
                    || (month == 3 && (day == 28 || day == 29))
                    || (month == 4 && (day == 1 || day == 30))
                    || (month == 5 && (day == 1 || day == 8 || day == 9))
                    || (month == 6 && (day == 5 || day == 6 || day == 21))
                    || (month == 7)
                    || (month == 11 && day == 1)
                    || (month == 12 && (day == 24 || day == 25 || day == 26 || day == 31)))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool CheckTimeSpanInterval(TimeSpan start, TimeSpan end, TimeSpan toCheck)
        {
            if (start < end)
                return toCheck >= start && toCheck <= end;
            else
                return toCheck >= start || toCheck <= end;
        }
    }
}