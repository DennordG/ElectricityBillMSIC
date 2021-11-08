using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectricityBillMSIC.Extensions
{
    public static class CollectionExtensions
    {
        public static void Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static IEnumerable<T> DoLazy<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);

                yield return item;
            }
        }

        public static IEnumerable<T> DoLazy<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            foreach (var (item, idx) in source.Select((item, idx) => (item, idx)))
            {
                action(item, idx);

                yield return item;
            }
        }
    }
}
