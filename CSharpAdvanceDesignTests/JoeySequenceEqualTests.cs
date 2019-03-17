using NUnit.Framework;
using System;
using System.Collections.Generic;
using Lab.Entities;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySequenceEqualTests
    {
        [Test]
        public void compare_two_numbers_equal()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_equal_false()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 3, 2 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal_false_ex1()
        {
            var first = new List<int> { 3, 2 };
            var second = new List<int> { 3, 2, 1 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal_false_ex2()
        {
            var first = new List<int> { 3, 2, 1 };
            var second = new List<int> { 1, 2, 3 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_numbers_equal_true_ex3()
        {
            var first = new List<int> { };
            var second = new List<int> { };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsTrue(actual);
        }

        [Test]
        public void compare_two_numbers_equal_false_ex4()
        {
            var first = new List<int> { 3, 2 };
            var second = new List<int> { 3, 2, 0 };

            var actual = JoeySequenceEqual(first, second);

            Assert.IsFalse(actual);
        }

        [Test]
        public void compare_two_employee_equal_true()
        {
            var first = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };

            var second = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };

            //var actual = JoeySequenceEqual(first, second); //物件預設的Equal會認為兩物件是不同的 =>結果是False
            // 傳入自定義的比較類型Equal
            var actual = JoeySequenceEqual(first, second, new JoeyEmployeeEqualityComparer());

            Assert.IsTrue(actual);
        }

        private bool JoeySequenceEqual<TFirst>(IEnumerable<TFirst> first, IEnumerable<TFirst> second)
        {
            return JoeySequenceEqual(first, second, EqualityComparer<TFirst>.Default);
        }

        private bool JoeySequenceEqual<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            while (true)
            {
                var firstHasNext = firstEnumerator.MoveNext();
                var secondHasNext = secondEnumerator.MoveNext();
                if (firstHasNext != secondHasNext)
                {
                    return false;
                }

                if (firstHasNext == false)
                {
                    return true;
                }

                var firstItem = firstEnumerator.Current;
                var secondItem = secondEnumerator.Current;
                if (!comparer.Equals(firstItem, secondItem))
                {
                    return false;
                }

            }
            //方法1
            //var firstEnumerator = first.GetEnumerator();
            //var secondEnumerator = second.GetEnumerator();
            //var firstHasNext = firstEnumerator.MoveNext();
            //var secondHasNext = secondEnumerator.MoveNext();

            //while (firstHasNext || secondHasNext)
            //{
            //    if (firstHasNext && secondHasNext)
            //    {
            //        var firstItem = firstEnumerator.Current;
            //        var secondItem = secondEnumerator.Current;
            //        if (firstItem != secondItem)
            //            return false;
            //    }
            //    else
            //    {
            //        return false;
            //    }

            //    firstHasNext = firstEnumerator.MoveNext();
            //    secondHasNext = secondEnumerator.MoveNext();
            //}

            //return true;
        }
    }
}