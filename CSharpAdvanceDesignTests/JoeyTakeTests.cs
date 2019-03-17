using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeyTakeTests
    {
        [Test]
        public void take_2_employees()
        {
            var employees = (IEnumerable<Employee>)new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };

            var actual = employees.JoeyTake(2);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void number_take_3()
        {
            var numbers = new[] { 12, 23, 34, 45 };

            var actual = numbers.JoeyTake(3);

            var expected = new[] { 12, 23, 34 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

    }
}