/// <summary>
/// Extensões para objetos
/// </summary>
public static class ObjectExtension
{
    /// <summary>
    /// Extensões para objetos
    /// </summary>
    public static void CopyPropertiesTo(this object source, object target)
    {
        if (source == null)
            return;
        var customerType = target.GetType();
        foreach (var prop in source.GetType().GetProperties())
        {
            var propGetter = prop.GetGetMethod();
            var propTarget = customerType.GetProperty(prop.Name);
            if (propTarget == null)
                continue;
            var propSetter = propTarget.GetSetMethod();
            if (propSetter == null) // metodo sem set ou com set privado
                continue;
            var valueToSet = propGetter.Invoke(source, null);
            propSetter.Invoke(target, new[] { valueToSet });
        }
    }
}
