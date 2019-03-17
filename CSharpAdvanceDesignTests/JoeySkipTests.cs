using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeySkipTests
    {
        [Test]
        public void skip_2_employees()
        {
            var employees = GetEmployees();

            var actual = employees.JoeySkip(2);

            var expected = new List<Employee>
            {
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void number_skip_3()
        {
            var numbers = new[] {12, 23, 34, 45};

            var actual = numbers.JoeySkip(3);

            var expected = new[] { 45 };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private static IEnumerable<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"},
                new Employee {FirstName = "Mike", LastName = "Chang"},
                new Employee {FirstName = "Joseph", LastName = "Yao"},
            };
        }
    }
}