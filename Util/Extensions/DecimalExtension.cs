/// <summary>
/// Extensões utilitárias para a classe Decimal.
/// </summary>
public static class DecimalExtension
{        
    /// <summary>
    /// Tenta fazer o parse de uma string para decimal.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="obj"></param>
    /// <param name="valDefault"></param>
    /// <returns></returns>
    public static Decimal TryParse(this Decimal i, object obj, Decimal valDefault = 0)
    {        
        if (obj == null) return valDefault;

        string s = obj.ToString();
        try
        {
            i = Decimal.Parse(s);
        }
        catch
        {
            i = valDefault;
        }

        return i;
    }
}