namespace MyWalletz.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class CurrencyList : IReadOnlyDictionary<string, string>
    {
        private static readonly Lazy<IDictionary<string, string>>
            LazyList = new Lazy<IDictionary<string, string>>(Load);

        public IEnumerable<string> Keys
        {
            get { return LazyList.Value.Keys; }
        }

        public IEnumerable<string> Values
        {
            get { return LazyList.Value.Values; }
        }

        public int Count
        {
            get { return LazyList.Value.Count; }
        }

        public string this[string key]
        {
            get { return LazyList.Value[key]; }
        }

        public bool ContainsKey(string key)
        {
            return LazyList.Value.ContainsKey(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return LazyList.Value.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return LazyList.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static IDictionary<string, string> Load()
        {
            var comparer = new StringKeyValuePairComparer();

            var currencies = CultureInfo.GetCultures(
                    CultureTypes.SpecificCultures)
                .Select(c => new RegionInfo(c.LCID))
                .Select(x => new KeyValuePair<string, string>(
                    x.ISOCurrencySymbol,
                    x.ISOCurrencySymbol + " - " + x.CurrencySymbol))
                .Distinct(comparer)
                .ToDictionary(
                    d => d.Key,
                    d => d.Value,
                    StringComparer.OrdinalIgnoreCase);

            return currencies;
        }

        private sealed class StringKeyValuePairComparer :
            IEqualityComparer<KeyValuePair<string, string>>
        {
            public bool Equals(KeyValuePair<string, string> x, KeyValuePair<string, string> y)
            {
                return x.Key.Equals(y.Key);
            }

            public int GetHashCode(KeyValuePair<string, string> obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}