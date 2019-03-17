using System;
using System.Collections.Generic;
using Lab.Entities;

namespace Lab
{
    public static class MyOwnLinq
    {
        public static IEnumerable<TSource> JoeyWhere<TSource>(this IEnumerable<TSource> source, Predicate<TSource> filter)
        {
            var sourceIEnumerable = source.GetEnumerator();
            while (sourceIEnumerable.MoveNext())
            {
                var item = sourceIEnumerable.Current;
                if (filter(item))
                    yield return item;
            }

            ////var result = new List<TSource>();
            //foreach (var itemProduct in source)
            //{
            //    if (filter(itemProduct))
            //        yield return itemProduct;
            //    //result.Add(itemProduct);
            //}

            ////return result;
        }

        public static List<TSource> JoeyWhere<TSource>(this List<TSource> source, Func<TSource, int, bool> filter)
        {
            var result = new List<TSource>();
            var index = 0;
            foreach (var itemProduct in source)
            {
                if (filter(itemProduct, index))
                    result.Add(itemProduct);
                index++;
            }

            return result;
        }

        public static IEnumerable<TResult> JoeySelect<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> mapper)
        {
            //var result = new List<TResult>();
            foreach (var itemSource in source)
            {
                yield return mapper(itemSource);
                //result.Add(mapper(itemSource));
            }

            //return result;
        }

        public static IEnumerable<TResult> JoeySelectWithIndex<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, int, TResult> mapper)
        {
            var result = new List<TResult>();
            var index = 0;
            foreach (var itemUrl in urls)
            {
                result.Add(mapper(itemUrl, index));
                index++;
            }

            return result;
        }

        public static IEnumerable<TEmployee> JoeyTake<TEmployee>(this IEnumerable<TEmployee> employees, int takeCount)
        {
            var employeesEnumerator = employees.GetEnumerator();
            var index = 0;
            while (employeesEnumerator.MoveNext())
            {
                var item = employeesEnumerator.Current;
                if (index < takeCount)
                    yield return item;
                else
                    yield break;
                index++;
            }
        }

        public static IEnumerable<TEmployee> JoeySkip<TEmployee>(this IEnumerable<TEmployee> employees, int skipCount)
        {
            var employeesEnumerator = employees.GetEnumerator();
            var index = 0;
            while (employeesEnumerator.MoveNext())
            {
                var item = employeesEnumerator.Current;
                if (index >= skipCount)
                    yield return item;
                index++;
            }
        }

        public static TSource JoeyFirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            var employeesIEnumerable = source.GetEnumerator();
            return employeesIEnumerable.MoveNext()
                ? employeesIEnumerable.Current
                : default(TSource);
        }

        public static TSource JoeyLastOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            var sourceEnumerator = source.GetEnumerator();
            var last = default(TSource);
            while (sourceEnumerator.MoveNext())
            {
                var item = sourceEnumerator.Current;
                last = item;
            }
            return last;
        }

        public static IEnumerable<TSuorce> JoeyReverse<TSuorce>(this IEnumerable<TSuorce> source)
        {
            //var sourceEnumerator = source.GetEnumerator();
            //var result = new Stack<TSuorce>();
            //while (sourceEnumerator.MoveNext())
            //{
            //    var item = sourceEnumerator.Current;
            //    result.Push(item);
            //}
            //return result;

            // Stack 內部已經有實作GetEnumerator
            return new Stack<TSuorce>(source);
        }

        public static IEnumerable<TResult> JoeyZip<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> selector)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();

            //寫法1
            //while (firstEnumerator.MoveNext())
            //{
            //    var firstItem = firstEnumerator.Current;
            //    if (secondEnumerator.MoveNext())
            //    {
            //        var secondItem = secondEnumerator.Current;
            //        yield return $"{firstItem.Name}-{secondItem.Owner}";
            //    }
            //    else
            //    {
            //        yield break;
            //    }
            //}

            //寫法2
            while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
            {
                var firstItem = firstEnumerator.Current;
                var secondItem = secondEnumerator.Current;
                yield return selector(firstItem, secondItem);
            }
        }
    }
}