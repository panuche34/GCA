public static class TimeSpanExtension
{
    public static string DisplayDaysInHours(this TimeSpan t)
    {
        var hours = 0;
        if (t.Days > 0)
        {
            hours = ((t.Days * 24) + t.Hours);
        }
        else
        {
            hours = t.Hours;
        }
        return $"{hours.ToString().PadLeft(2, '0')}:{t.Minutes.ToString().PadLeft(2, '0')}";
    }
}
