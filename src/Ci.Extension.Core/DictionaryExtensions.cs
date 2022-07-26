using System.Collections.Generic;

namespace Ci.Extension.Core
{
    public static class DictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictToAdd)
        {
            foreach (var dict in dictToAdd)
            {
                source.Add(dict);
            }
        }
    }
}
