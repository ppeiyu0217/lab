using System;
using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using Lab;

namespace CSharpAdvanceDesignTests
{
    [TestFixture()]
    public class JoeySelectTests
    {
        [Test]
        public void replace_http_to_https()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(itemUrl => itemUrl.Replace("http://", "https://"));
            var expected = new List<string>
            {
                "https://tw.yahoo.com",
                "https://facebook.com",
                "https://twitter.com",
                "https://github.com",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void replace_http_to_https_and_append_Joey()
        {
            var urls = GetUrls();

            var actual = urls.JoeySelect(itemUrl => itemUrl.Replace("http://", "https://") + "/joey");
            var expected = new List<string>
            {
                "https://tw.yahoo.com/joey",
                "https://facebook.com/joey",
                "https://twitter.com/joey",
                "https://github.com/joey",
            };

            expected.ToExpectedObject().ShouldEqual(actual.ToList());
        }

        [Test]
        public void get_full_name()
        {
            var employees = GetEmployees();

            var expected = new[]
            {
                "Joey-Chen",
                "Tom-Li",
                "David-Chen",
            };

            //var actual = JoeySelectGetFullName(employees, e => $"{e.FirstName}-{e.LastName}");
            var actual = employees.JoeySelect(e => $"{e.FirstName}-{e.LastName}");

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void get_full_name_with_seqNo()
        {
            var employees = GetEmployees();

            var expected = new[]
            {
                "1.Joey-Chen",
                "2.Tom-Li",
                "3.David-Chen",
            };

            var actual = employees.JoeySelectWithIndex((e, index) => $"{index+1}.{e.FirstName}-{e.LastName}");

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        [Test]
        public void get_full_name_length()
        {
            var employees = GetEmployees();

            var expected = new[]
            {
                8,5,9
            };

            var actual = employees.JoeySelect(e => $"{e.FirstName}{e.LastName}".Length);

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<string> JoeySelectAppendJoey(IEnumerable<string> urls, Func<string, string> mapper)
        {
            var result = new List<string>();
            foreach (var itemUrl in urls)
            {
                var mappingResult = mapper(itemUrl);
                result.Add(mappingResult);
            }

            return result;
        }

        private IEnumerable<string> JoeySelectGetFullName(IEnumerable<Employee> urls, Func<Employee, string> mapper)
        {
            var result = new List<string>();
            foreach (var itemUrl in urls)
            {
                result.Add(mapper(itemUrl));
            }

            return result;
        }

        private static IEnumerable<string> GetUrls()
        {
            yield return "http://tw.yahoo.com";
            yield return "https://facebook.com";
            yield return "https://twitter.com";
            yield return "http://github.com";
        }

        private static List<Employee> GetEmployees()
        {
            return new List<Employee>
            {
                new Employee {FirstName = "Joey", LastName = "Chen"},
                new Employee {FirstName = "Tom", LastName = "Li"},
                new Employee {FirstName = "David", LastName = "Chen"}
            };
        }
    }
}