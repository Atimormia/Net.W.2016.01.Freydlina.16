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
            bool result = DatabaseQueries.AddIngredient(name, weight, categoryName);
            DatabaseQueries.ShowIngradients();
            return result;
        }

        public static IEnumerable<TestCaseData> TestCasesForShawarmaSeillng
        {
            get
            {
                yield return new TestCaseData("Chicken").Returns(true);
                yield return new TestCaseData("Vegetarian").Returns(true);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForShawarmaSeillng))]
        public bool TestShawarmaSeillng(string shawarmaName)
        {
            DatabaseQueries.ShowShawarmas();
            return DatabaseQueries.ShawarmaSeillng(shawarmaName);
        }

        public static IEnumerable<TestCaseData> TestCasesForAddRecipe
        {
            get
            {
                IngradientWeight[] ingradientWeights = new[]
                {
                    new IngradientWeight {IngradientName = "seitan", Weight = 500},
                    new IngradientWeight {IngradientName = "lemon juice", Weight = 5},
                    new IngradientWeight {IngradientName = "garam masala", Weight = 5},
                    new IngradientWeight {IngradientName = "paprika", Weight = 2},
                    new IngradientWeight {IngradientName = "cumin", Weight = 1},
                    new IngradientWeight {IngradientName = "salt", Weight = 2},
                    new IngradientWeight {IngradientName = "soy yogurt", Weight = 100},
                    new IngradientWeight {IngradientName = "vinegar", Weight = 5},
                    new IngradientWeight {IngradientName = "garlic", Weight = 2},
                };
                yield return new TestCaseData("Vegetarian", ingradientWeights, 20).Returns(true);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForAddRecipe))]
        public bool TestAddRecipe(string shawarmaName, IngradientWeight[] ingradientWeightAccordings, int cookingTime)
        {
            bool result = DatabaseQueries.AddRecipe(shawarmaName, ingradientWeightAccordings, cookingTime);
            DatabaseQueries.ShowIngradients();
            return result;
        }

        public static IEnumerable<TestCaseData> TestCasesForNewPrice
        {
            get
            {
                yield return new TestCaseData("Shawarma stall 1", "Vegetarian", 4m,"").Returns(true);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForNewPrice))]
        public bool TestNewPrice(string sellingPointTitle, string shawarmaName, decimal newPrice, string comment)
        { 
            return DatabaseQueries.NewPrice(sellingPointTitle, shawarmaName, newPrice, comment);
        }

        public static IEnumerable<TestCaseData> TestCasesForSellingPoint
        {
            get
            {
                yield return new TestCaseData("Shawarma stall 3", "address4", "stall").Returns(true);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForSellingPoint))]
        public bool TestAddSellingPoint(string sellingPointTitle, string address, string categoryName)
        {
            return DatabaseQueries.AddSellingPoint(sellingPointTitle, address, categoryName);
        }

        public static IEnumerable<TestCaseData> TestCasesForAddNewSeller
        {
            get
            {
                yield return new TestCaseData("Ann","Shawarma stall 3").Returns(true);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForAddNewSeller))]
        public bool TestAddNewSeller(string sellerName, string sellingPointName)
        {
            return DatabaseQueries.AddNewSeller(sellerName, sellingPointName);
        }

        public static IEnumerable<TestCaseData> TestCasesForSellingPointRevenue
        {
            get
            {
                yield return new TestCaseData("Shawarma stall 2",new DateTime(2016,12,05),new DateTime(2016,12,08)).Returns(5m);
                yield return new TestCaseData("Shawarma restaurant", new DateTime(2016, 12, 03), new DateTime(2016, 12, 06)).Returns(86m);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForSellingPointRevenue))]
        public decimal TestSellingPointRevenue(string sellingPointName, DateTime start, DateTime end)
        {
            return DatabaseQueries.SellingPointRevenue(sellingPointName, start, end);
        }

        public static IEnumerable<TestCaseData> TestCasesForSellerSalary
        {
            get
            {
                yield return new TestCaseData("John", new DateTime(2016, 12, 05), new DateTime(2016, 12, 08), 0.1m, 0.5m).Returns(170m);
                yield return new TestCaseData("Mark", new DateTime(2016, 12, 03), new DateTime(2016, 12, 06), 0.1m, 0.5m).Returns(92.5m);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForSellerSalary))]
        public decimal TestSellerSalary
            (string sellerName, DateTime startPeriod, DateTime endPeriod, decimal workingRate, decimal cookingRate)
        {
            return DatabaseQueries.SellerSalary(sellerName, startPeriod, endPeriod, workingRate, cookingRate);
        }

        [Test]
        public void View()
        {
            DatabaseQueries.ShowIngradients();
        }

        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
