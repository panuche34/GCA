using System.Text.RegularExpressions;

/// <summary>
/// Extensões utilitárias para a classe String.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Capitaliza a string pra uma melhor apresentação.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string Capitalize(this String s)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
    }

    /// <summary>
    /// Only numbers.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string OnlyNumbers(this String s)
    {
        string ret = String.Empty;
        int i = 0;
        while (i < s.Length)
        {
            if (Regex.IsMatch(s[i].ToString(), "[0-9]"))
                ret += s[i];
            i++;
        }
        return ret;
    }

    /// <summary>
    /// Tenta converter uma string para int32
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Int32 ToInt32(this String s)
    {
        return Int32.MinValue.TryParse(s);
    }

    /// <summary>
    /// Converte string para int32
    /// </summary>
    /// <param name="s"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int ToInt32(this string s, int defaultValue = 0)
    {
        int i = 0;
        try
        {
            i = Int32.Parse(s);
        }
        catch
        {
            i = defaultValue;
        }

        return i;
    }

    /// <summary>
    /// Tenta converter uma string para int64
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static long ToInt64(this String s)
    {
        try
        {
            return Int64.Parse(s);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// Converte strin para int64
    /// </summary>
    /// <param name="s"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static long ToInt64(this string s, long defaultValue = 0)
    {
        try
        {
            return Int64.Parse(s);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// Tenta converter uma string para DateTime
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static DateTime ToDateTime(this String s)
    {
        return DateTime.MinValue.TryParse(s);
    }

    /// <summary>
    /// Verifica se a string é um número.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNumber(this String value)
    {
        foreach (Char c in value.ToCharArray())
            if (!Char.IsDigit(c))
                return false;

        return true;
    }
}