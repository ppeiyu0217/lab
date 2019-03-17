using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace Lab.Entities
{
    public class JoeyEmployeeEqualityComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName
                && x.LastName == y.LastName
                && x.Phone == y.Phone;
        }

        public int GetHashCode(Employee obj)
        {
            var firstHashCode = obj.FirstName;
            var lastHashCode = obj.LastName;
            return new { firstHashCode, lastHashCode }.GetHashCode();
        }
    }

    public class JoeyEmployeeEqualityComparerWithOnlyName : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            return x.FirstName == y.FirstName && x.LastName == y.LastName;
        }

        public int GetHashCode(Employee obj)
        {
            var firstHashCode = obj.FirstName;
            var lastHashCode = obj.LastName;
            return new { firstHashCode, lastHashCode }.GetHashCode();
        }
    }

    public class Employee
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public Role Role { get; set; }
    }
}