﻿using System;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CSharpAdvanceDesignTests
{

    [TestFixture]
    public class JoeyAllTests
    {
        [Test]
        public void girls_all_adult()
        {
            var girls = new List<Girl>
            {
                new Girl{Age = 20},
                new Girl{Age = 21},
                new Girl{Age = 17},
                new Girl{Age = 18},
                new Girl{Age = 30},
            };

            var actual = JoeyAll(girls, girl => girl.Age >= 18);
            Assert.IsFalse(actual);
        }

        private bool JoeyAll(IEnumerable<Girl> girls, Func<Girl, bool> filter)
        {
            var employeesEnumerator = girls.GetEnumerator();
            while (employeesEnumerator.MoveNext())
            {
                var girl = employeesEnumerator.Current;
                if (filter(girl) == false)
                    return false;
            }
            return true;
        }
    }
}