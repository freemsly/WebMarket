using System.Collections.Generic;
using System.Linq;

namespace WebMarket.Common
{
    public static class ExtensionMethods
    {
        public static bool ContainsKey<T>(this List<TypedItem> list, string key)
        {
            return list.Select(item => item.Key == key).FirstOrDefault();
        }

        public static T Get<T>(this List<TypedItem> list, string key) where T: TypedItem
        {
            return (T) list.FirstOrDefault(item => item.Key == key);
        }
    }
}
