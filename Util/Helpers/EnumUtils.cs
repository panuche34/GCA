using System.ComponentModel;
using System.Reflection;

namespace Util.Helpers
{
    public static class EnumUtils
    {

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string GetAttributeText(Enum val)
        {
            if (val == null)
                return null;

            var attrType = val.GetType().
                GetMember(val.ToString())?.
                FirstOrDefault()?.
                GetCustomAttribute<DescriptionAttribute>();

            if (attrType != null)
                return attrType.Description;
            else
                return "";
        }
    }
}
