/// <summary>
/// Extensão para o objeto Int32.
/// </summary>
public static class Int32Extension
{

    /// <summary>
    /// Tenta fazer o parse de um objeto.
    /// </summary>
    /// <param name="i"></param>
    /// <param name="obj"></param>
    /// <param name="valDefault"></param>
    /// <returns></returns>
    public static Int32 TryParse(this Int32 i, object obj, Int32 valDefault = 0)
    {
        if (obj == null) return valDefault;

        string s = obj.ToString();
        try
        {
            i = Int32.Parse(s);
        }
        catch
        {
            i = valDefault;
        }

        return i;
    }
}
