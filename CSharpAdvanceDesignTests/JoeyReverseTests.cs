using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyReverseTests
    {
        [Test]
        public void reverse_employees()
        {
            var employees = new List<Employee>
            {
                new Employee(){FirstName = "Joey",LastName = "Chen"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "David",LastName = "Wang"},
            };

            var actual = employees.JoeyReverse();

            var expected = new List<Employee>
            {
                new Employee(){FirstName = "David",LastName = "Wang"},
                new Employee(){FirstName = "Tom",LastName = "Li"},
                new Employee(){FirstName = "Joey",LastName = "Chen"},
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }
    }
}