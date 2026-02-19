namespace Util.Helpers
{
    public class CalcTimeHelper
    {
        public TimeSpan CalcTime(DateTime? startPeriod, DateTime? endPeriod)
        {
            if ((startPeriod == null) || (startPeriod == DateTime.MinValue))
                return TimeSpan.Zero;

            if ((endPeriod == null) || (endPeriod == DateTime.MinValue))
                return TimeSpan.Zero;

            var taktTime = new TimeSpan();
            if (startPeriod.Value.Date == endPeriod.Value.Date)
            {
                taktTime = endPeriod.Value.Subtract(startPeriod.Value);
            }
            else if (startPeriod.Value.Date < endPeriod.Value.Date)
            {

                taktTime = CalculateWorkingDaysDifference(startPeriod.Value, endPeriod.Value);
            }
            else
            {
                taktTime = CalculateWorkingDaysDifference(endPeriod.Value, startPeriod.Value);
            }
            return taktTime;
        }

        public TimeSpan CalculateWorkingDaysDifference(DateTime startPeriod, DateTime endPeriod)
        {
            TimeSpan hoursReturn = TimeSpan.Zero;
            var startDay = new DateTime(startPeriod.Year, startPeriod.Month, startPeriod.Day,
                startPeriod.Hour < 8 ? 8 : startPeriod.Hour, startPeriod.Hour < 8 ? 0 : startPeriod.Minute,
                startPeriod.Hour < 8 ? 0 : startPeriod.Second);
            while (startDay.Date <= endPeriod.Date)
            {
                if (startDay.DayOfWeek == DayOfWeek.Saturday || startDay.DayOfWeek == DayOfWeek.Sunday)
                {
                    startDay = startDay.AddDays(1);
                    continue;
                }

                var endDay = new DateTime(startDay.Year, startDay.Month, startDay.Day, 18, 0, 0);
                hoursReturn += endDay.Subtract(startDay);
                startDay = startDay.AddDays(1).Date.AddHours(8);
            }
            return hoursReturn;
        }
    }
}
