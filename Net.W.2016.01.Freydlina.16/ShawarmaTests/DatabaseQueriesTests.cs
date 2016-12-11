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
