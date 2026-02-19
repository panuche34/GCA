using System.Globalization;

/// <summary>
/// Extensão do objeto Datetime.
/// </summary>
public static class DateTimeExtension
{        
    /// <summary>
    /// Tenta fazer o parse de uma string para data.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime TryParse(this DateTime d, string s)
    {
        DateTime ret = DateTime.MinValue;
        CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        DateTime.TryParse(s, culture, DateTimeStyles.None, out ret);
        return ret;
    }

    /// <summary>
    /// Tenta fazer o parse de uma objeto para data.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime TryParse(this DateTime d, object s)
    {
        if (s != null)
            return TryParse(d, s.ToString());
        else
            return DateTime.MinValue;
    }

    /// <summary>
    /// Tenta fazer o parse de uma string para data.
    /// </summary>
    /// <param name="d"></param>
    /// <param name="fmt">formato dd - dia, MM - mês, yyyy - ano</param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime TryParse(this DateTime d, string s, string fmt)
    {
        try
        {
            return DateTime.ParseExact(s, fmt, null, DateTimeStyles.None);
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    /// <summary>
    /// Returns the first day of week with in the month.
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <param name="dow">What day of week to find the first one of in the month.</param>
    /// <returns>Returns DateTime object that represents the first day of week with in the month.</returns>
    public static DateTime GetFirstDayOfWeekInMonth(this DateTime obj, DayOfWeek dow)
    {
        DateTime firstDay = new DateTime(obj.Year, obj.Month, 1);
        int diff = firstDay.DayOfWeek - dow;
        if (diff > 0) diff -= 7;
        return firstDay.AddDays(diff * -1);
    }

    /// <summary>
    /// Returns the first weekday (Financial day) of the month
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <returns>Returns DateTime object that represents the first weekday (Financial day) of the month</returns>
    public static DateTime GetFirstWeekDayOfMonth(this DateTime obj)
    {
        DateTime firstDay = new DateTime(obj.Year, obj.Month, 1);
        for (int i = 0; i < 7; i++)
        {
            if (firstDay.AddDays(i).DayOfWeek != DayOfWeek.Saturday && firstDay.AddDays(i).DayOfWeek != DayOfWeek.Sunday)
                return firstDay.AddDays(i);
        }
        return firstDay;
    }

    /// <summary>
    /// Returns the first day of the month
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be performed</param>
    /// <returns>Returns DateTime object that represents the first day of the month</returns>
    public static DateTime GetFirstDayOfMonth(this DateTime obj)
    {
        return new DateTime(obj.Year, obj.Month, 1);
    }

    /// <summary>
    /// Returns the last day of week with in the month.
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <param name="dow">What day of week to find the last one of in the month.</param>
    /// <returns>Returns DateTime object that represents the last day of week with in the month.</returns>
    public static DateTime GetLastDayOfWeekInMonth(this DateTime obj, DayOfWeek dow)
    {
        DateTime lastDay = new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month));
        DayOfWeek lastDow = lastDay.DayOfWeek;

        int diff = dow - lastDow;
        if (diff > 0) diff -= 7;

        return lastDay.AddDays(diff);
    }

    /// <summary>
    /// Returns the last weekday (Financial day) of the month
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <returns>Returns DateTime object that represents the last weekday (Financial day) of the month</returns>
    public static DateTime GetLastWeekDayOfMonth(this DateTime obj)
    {
        DateTime lastDay = new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month));
        for (int i = 0; i < 7; i++)
        {
            if (lastDay.AddDays(i * -1).DayOfWeek != DayOfWeek.Saturday && lastDay.AddDays(i * -1).DayOfWeek != DayOfWeek.Sunday)
                return lastDay.AddDays(i * -1);
        }
        return lastDay;
    }

    /// <summary>
    /// Returns the last day of the month
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be performed</param>
    /// <returns>Returns DateTime object that represents the last day of the month</returns>
    public static DateTime GetLastDayOfMonth(this DateTime obj)
    {
        return new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month));
    }

    /// <summary>
    /// Returns the closest Weekday (Financial day) Date
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <returns>Returns the closest Weekday (Financial day) Date</returns>
    public static DateTime GetClosestWeekDay(this DateTime obj)
    {
        if (obj.DayOfWeek == DayOfWeek.Saturday)
            return obj.AddDays(-1);
        if (obj.DayOfWeek == DayOfWeek.Sunday)
            return obj.AddDays(1);
        else
            return obj;
    }

    /// <summary>
    /// Returns the very end of the given month (the last millisecond of the last hour for the given date)
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <returns>Returns the very end of the given month (the last millisecond of the last hour for the given date)</returns>
    public static DateTime GetEndOfMonth(this DateTime obj)
    {
        return new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month), 23, 59, 59, 999);
    }

    /// <summary>
    /// Returns the Start of the given month (the fist millisecond of the given date)
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <returns>Returns the Start of the given month (the fist millisecond of the given date)</returns>
    public static DateTime GetBeginningOfMonth(this DateTime obj)
    {
        return new DateTime(obj.Year, obj.Month, 1, 0, 0, 0, 0);
    }

    /// <summary>
    /// Returns a given datetime according to the week of year and the specified day within the week.
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <param name="week">A number of whole and fractional weeks. The value parameter can only be positive.</param>
    /// <param name="dayofweek">A DayOfWeek to find in the week</param>
    /// <returns>A DateTime whose value is the sum according to the week of year and the specified day within the week.</returns>
    public static DateTime GetDateByWeek(this DateTime obj, int week, DayOfWeek dayofweek)
    {
        if (week > 0 && week < 54)
        {
            DateTime FirstDayOfyear = new DateTime(obj.Year, 1, 1);
            int daysToFirstCorrectDay = (((int)dayofweek - (int)FirstDayOfyear.DayOfWeek) + 7) % 7;
            return FirstDayOfyear.AddDays(7 * (week - 1) + daysToFirstCorrectDay);
        }
        else
            return obj;
    }

    private static int Sub(DayOfWeek s, DayOfWeek e)
    {
        if ((s - e) > 0) return (s - e) - 7;
        if ((s - e) == 0) return -7;
        return (s - e);
    }

    /// <summary>
    /// Returns first next occurence of specified DayOfTheWeek
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <param name="day">A DayOfWeek to find the next occurence of</param>
    /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the enum value represented by the day.</returns>
    public static DateTime GetNextDayOfWeek(this DateTime obj, DayOfWeek day)
    {
        return obj.AddDays(Sub(obj.DayOfWeek, day) * -1);
    }

    /// <summary>
    /// Returns next "first" occurence of specified DayOfTheWeek
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <param name="day">A DayOfWeek to find the previous occurence of</param>
    /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the enum value represented by the day.</returns>
    public static DateTime GetPreviousDayOfWeek(this DateTime obj, DayOfWeek day)
    {
        return obj.AddDays(Sub(day, obj.DayOfWeek));
    }

    /// <summary>
    /// Adds the specified number of financials days to the value of this instance.
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <param name="days">A number of whole and fractional financial days. The value parameter can be negative or positive.</param>
    /// <returns>A DateTime whose value is the sum of the date and time represented by this instance and the number of financial days represented by days.</returns>
    public static DateTime AddFinancialDays(this DateTime obj, int days)
    {
        int addint = Math.Sign(days);
        for (int i = 0; i < (Math.Sign(days) * days); i++)
        {
            do { obj = obj.AddDays(addint); }
            while (obj.IsWeekend());
        }
        return obj;
    }

    /// <summary>
    /// Calculate Financial days between two dates.
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <param name="otherdate">End or start date to calculate to or from.</param>
    /// <returns>Amount of financial days between the two dates</returns>
    public static int CountFinancialDays(this DateTime obj, DateTime otherdate)
    {
        TimeSpan ts = (otherdate - obj);
        int addint = Math.Sign(ts.Days);
        int unsigneddays = (Math.Sign(ts.Days) * ts.Days);
        int businessdays = 0;
        for (int i = 0; i < unsigneddays; i++)
        {
            obj = obj.AddDays(addint);
            if (!obj.IsWeekend())
                businessdays++;
        }
        return businessdays;
    }

    /// <summary>
    /// Returns true if the day is Saturday or Sunday
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <returns>boolean value indicating if the date is a weekend</returns>
    public static bool IsWeekend(this DateTime obj)
    {
        return (obj.DayOfWeek == DayOfWeek.Saturday || obj.DayOfWeek == DayOfWeek.Sunday);
    }

    /// <summary>
    /// Get the quarter that the datetime is in.
    /// </summary>
    /// <param name="obj">DateTime Base, from where the calculation will be preformed.</param>
    /// <returns>Returns 1 to 4 that represenst the quarter that the datetime is in.</returns>
    public static int Quarter(this DateTime obj)
    {
        return ((obj.Month - 1) / 3) + 1;
    }

    /// <summary>
    /// Retorna uma string no formato UTC requerido pela NFe
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string FormatUTC(this DateTime obj)
    {
        return obj.ToString("yyyy-MM-ddTHH:mm:sszzzz");
    }

    public static string FormatXML(this DateTime obj)
    {
        return obj.ToString("yyyy-MM-ddTHH:mm:ss-03:00");
    }

    /// <summary>
    /// Tenta fazer o parse de um objeto para data
    /// objeto no formato yyyy-MM-ddTHH:mm:ss-03:00
    /// </summary>
    /// <param name="d"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime TryParseXML(this DateTime d, object s)
    {
        if (s == null)
            return DateTime.UtcNow;

        try
        {
            return DateTime.ParseExact(s.ToString(), "yyyy-MM-ddTHH:mm:ss-03:00", new CultureInfo("en-US", false));
        }
        catch
        {
            return DateTime.UtcNow;
        }
    }
}