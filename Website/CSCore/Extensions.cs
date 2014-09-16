using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSCore.Extensions
{
    public static class Common
    {
        /// <summary>
        /// Combines elements in a list into a single object.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <typeparam name="TResult">The type of new object to be returned.</typeparam>
        /// <param name="list"></param>
        /// <param name="merge"></param>
        /// <returns></returns>
        public static TResult Combine<T, TResult>(this List<T> list, Func<T, TResult, TResult> merge)
        {
            if (list.Count == 0)
                return default(TResult);
            
            TResult current = merge(list[0], default(TResult));

            for (int i = 1; i < list.Count; i++)
            {
                current = merge(list[i], current);
            }

            return current;
        }

        public static TResult Combine<T, TResult>(this IList<T> list, Func<T, TResult, TResult> merge)
        {
            return Combine<T, TResult>(list, merge);
        }

        public static TResult Combine<T, TResult>(this IEnumerable<T> list, Func<T, TResult, TResult> merge)
        {
            return Combine<T, TResult>(list.ToList(), merge);
        }
    }
}
