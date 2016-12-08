using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ShawarmaProject;

namespace ShawarmaTests
{
    [TestFixture]
    public class DatabaseQueriesTests
    {
        public static IEnumerable<TestCaseData> TestCasesForAddIngradient
        {
            get
            {
                yield return new TestCaseData("potato", 100000, "Vegetables").Returns(true);
                yield return new TestCaseData("flour", 10000, "").Returns(true);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForAddIngradient))]
        public bool TestAddIngradient(string name, int weight, string categoryName)
        {
            return DatabaseQueries.AddIngredient(name, weight, categoryName);
        }

        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
