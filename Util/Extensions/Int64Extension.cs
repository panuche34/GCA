/// <summary>
/// Extensões utilitárias para a classe Int64.
/// </summary>
public static class Int64Extension
{

    /// <summary>
    /// Tenta fazer o parse de um objeto.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="obj"></param>
    /// <param name="valDefault"></param>
    /// <returns></returns>
    public static Int64 TryParse(this Int64 i, object obj, Int64 valDefault = 0)
    {
        if (obj == null) return valDefault;

        string s = obj.ToString();
        try
        {
            i = Int64.Parse(s);
        }
        catch
        {
            i = valDefault;
        }

        return i;
    }
}
