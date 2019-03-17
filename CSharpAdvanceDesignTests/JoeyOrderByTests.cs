using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Collections;

namespace CSharpAdvanceDesignTests
{
    public class MyOrderEnumerable : IOrderedEnumerable<Employee>
    {
        public IOrderedEnumerable<Employee> CreateOrderedEnumerable<TKey>(Func<Employee, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Employee> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class CombineKeyComparer<TKey> : IComparer<Employee>
    {
        public CombineKeyComparer(Func<Employee, TKey> KeySelector, Comparer<TKey> KeyComparer)
        {
            this.KeySelector = KeySelector;
            this.KeyComparer = KeyComparer;
        }

        private Func<Employee, TKey> KeySelector { get; set; }
        private Comparer<TKey> KeyComparer { get; set; }

        public int Compare(Employee element, Employee minElement)
        {
            return KeyComparer.Compare(KeySelector(element), KeySelector(minElement));
        }
    }

    public class ComboCompare : IComparer<Employee>
    {
        public ComboCompare(IComparer<Employee> firstComparer, IComparer<Employee> secondComparer)
        {
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        private IComparer<Employee> FirstComparer { get; set; }
        private IComparer<Employee> SecondComparer { get; set; }

        public int Compare(Employee x, Employee y)
        {
            var firstCompareResult = FirstComparer.Compare(x, y);
            return firstCompareResult == 0 ? SecondComparer.Compare(x, y) : firstCompareResult;
        }
    }

    [TestFixture]
    public class JoeyOrderByTests
    {
        //[Test]
        //public void orderBy_lastName()
        //{
        //    var employees = new[]
        //    {
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //    };

        //    var actual = JoeyOrderByLastName(employees);

        //    var expected = new[]
        //    {
        //        new Employee {FirstName = "Joseph", LastName = "Chen"},
        //        new Employee {FirstName = "Joey", LastName = "Chen"},
        //        new Employee {FirstName = "Tom", LastName = "Li"},
        //        new Employee {FirstName = "Joey", LastName = "Wang"},
        //    };

        //    expected.ToExpectedObject().ShouldMatch(actual);
        //}

        [Test]
        public void orderBy_lastName_then_by_firstName()
        {
            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Joey", LastName = "Chen"},
            };

            var firstComparer =
                new CombineKeyComparer<string>(firstKeySelector => firstKeySelector.LastName, Comparer<string>.Default);
            var secondComparer =
                new CombineKeyComparer<string>(secondKeySelector => secondKeySelector.FirstName, Comparer<string>.Default);

            var actual = JoeyOrderBy(employees
                , new ComboCompare(firstComparer, secondComparer));


            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Joseph", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "Joey", LastName = "Wang"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void orderBy_lastName_then_by_firstName_than_by_Age()
        {

            var employees = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
            };

            //JoeyOrderByKeepComparer(employees,)
            var firstComparer =
                new CombineKeyComparer<string>(firstKeySelector => firstKeySelector.LastName, Comparer<string>.Default);
            var secondComparer =
                new CombineKeyComparer<string>(secondKeySelector => secondKeySelector.FirstName, Comparer<string>.Default);

            var firstCombo = new ComboCompare(firstComparer, secondComparer);

            var thirdComparer =
                new CombineKeyComparer<int>(thirdSelector => thirdSelector.Age, Comparer<int>.Default);

            var finalCombo = new ComboCompare(firstCombo, thirdComparer);

            var actual = JoeyOrderBy(employees, finalCombo);

            var expected = new[]
            {
                new Employee {FirstName = "Joey", LastName = "Chen", Age = 33},
                new Employee {FirstName = "Joseph", LastName = "Chen", Age = 32},
                new Employee {FirstName = "Tom", LastName = "Li", Age = 31},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 20},
                new Employee {FirstName = "Joey", LastName = "Wang", Age = 50},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Employee> JoeyOrderByKeepComparer(
            IEnumerable<Employee> employees,
            IComparer<Employee> comparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];
                    if (comparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }

                    //var firstCompareResult = comboCompare.FirstComparer.Compare(element, minElement);
                    //if (firstCompareResult < 0)
                    //{
                    //    minElement = element;
                    //    index = i;
                    //}
                    //else if (firstCompareResult == 0)
                    //{
                    //    var secondCompareResult = comboCompare.SecondComparer.Compare(element, minElement);
                    //    if (secondCompareResult < 0)
                    //    {
                    //        minElement = element;
                    //        index = i;
                    //    }
                    //}
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

        private IEnumerable<Employee> JoeyOrderBy(
            IEnumerable<Employee> employees,
            IComparer<Employee> comparer)
        {
            //bubble sort
            var elements = employees.ToList();
            while (elements.Any())
            {
                var minElement = elements[0];
                var index = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    var element = elements[i];
                    if (comparer.Compare(element, minElement) < 0)
                    {
                        minElement = element;
                        index = i;
                    }

                    //var firstCompareResult = comboCompare.FirstComparer.Compare(element, minElement);
                    //if (firstCompareResult < 0)
                    //{
                    //    minElement = element;
                    //    index = i;
                    //}
                    //else if (firstCompareResult == 0)
                    //{
                    //    var secondCompareResult = comboCompare.SecondComparer.Compare(element, minElement);
                    //    if (secondCompareResult < 0)
                    //    {
                    //        minElement = element;
                    //        index = i;
                    //    }
                    //}
                }

                elements.RemoveAt(index);
                yield return minElement;
            }
        }

    }
}