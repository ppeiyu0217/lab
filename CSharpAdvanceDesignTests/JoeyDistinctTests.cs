using ExpectedObjects;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyDistinctTests
    {
        [Test]
        public void distinct_numbers()
        {
            var numbers = new[] { 91, 3, 91, -1 };
            var actual = Distinct(numbers);

            var expected = new[] { 91, 3, -1 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void distinct_employee()
        {

            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var actual = JoeyDistinctWithEqualityComparer(employees, new JoeyEmployeeEqualityComparerWithOnlyName());

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<TSource> JoeyDistinctWithEqualityComparer<TSource>(IEnumerable<TSource> employees, IEqualityComparer<TSource> comparee)
        {
            var sourceEnumerator = employees.GetEnumerator();
            var result = new HashSet<TSource>(comparee);
            while (sourceEnumerator.MoveNext())
            {
                var item = sourceEnumerator.Current;
                if (result.Add(item))
                    yield return item;
            }
        }

        private IEnumerable<int> Distinct(IEnumerable<int> numbers)
        {
            // 方法1 HashSet 重複 hashCode 會略過
            //return new HashSet<int>(numbers);

            // 方法2
            //var sourceEnumerator = numbers.GetEnumerator();
            //var result = new HashSet<int>();
            //while (sourceEnumerator.MoveNext())
            //{
            //    var item = sourceEnumerator.Current;
            //    if (result.Add(item))
            //        yield return item;
            //}

            //方法3
            return JoeyDistinctWithEqualityComparer(numbers, EqualityComparer<int>.Default);
        }
    }
}