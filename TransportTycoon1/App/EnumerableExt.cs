using System;
using System.Collections.Generic;

namespace App
{
    public static class EnumerableExt
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}