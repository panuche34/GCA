using System.ComponentModel;
using System.Reflection;

/// <summary>
/// Classe extensiva do enum.
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// Obtém o valor do enum
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetAttrValue(this Enum value)
    {
        string description = value.ToString();
        FieldInfo fieldInfo = value.GetType().GetField(description);
        DescriptionAttribute[] attributes =
           (DescriptionAttribute[])
         fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
        {
            description = attributes[0].Description;
        }
        return description;
    }

    /// <summary>
    /// Atribui um valor de string a Enum usando o atributo de descrição
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string GetDescription<T>(this T e) where T : IConvertible
    {
        if (e is Enum)
        {
            Type type = e.GetType();
            Array values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(System.Globalization.CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Atribuindo um valor de string a Enum usando o atributo xml
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="e"></param>
    /// <returns></returns>
    public static string GetXmlEnumAttribute<T>(this T e) where T : IConvertible
    {
        if (e is Enum)
        {
            Type type = e.GetType();
            Array values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(System.Globalization.CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(System.Xml.Serialization.XmlEnumAttribute), false)
                        .FirstOrDefault() as System.Xml.Serialization.XmlEnumAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Name;
                    }
                }
            }
        }

        return null;
    }

}
