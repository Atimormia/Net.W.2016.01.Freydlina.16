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

        public static IEnumerable<TestCaseData> TestCasesForAddRecipe
        {
            get
            {
                IngradientWeight[] ingradientWeights = new[]
                {
                    new IngradientWeight {IngradientName = "seitan", Weight = 500},
                    new IngradientWeight {IngradientName = "lemon juice", Weight = 5},
                    new IngradientWeight {IngradientName = "Garam Masala", Weight = 5},
                    new IngradientWeight {IngradientName = "Paprika", Weight = 2},
                    new IngradientWeight {IngradientName = "Cumin", Weight = 1},
                    new IngradientWeight {IngradientName = "Salt", Weight = 2},
                    new IngradientWeight {IngradientName = "soy yogurt", Weight = 100},
                    new IngradientWeight {IngradientName = "Malt Vinegar", Weight = 5},
                    new IngradientWeight {IngradientName = "Garlic", Weight = 2},
                };
                yield return new TestCaseData("Vegetarian", ingradientWeights, 20).Returns(true);
            }
        }
        [Test, TestCaseSource(nameof(TestCasesForAddRecipe))]
        public bool TestAddRecipe(string shawarmaName, IngradientWeight[] ingradientWeightAccordings, int cookingTime)
        {
            return DatabaseQueries.AddRecipe(shawarmaName,ingradientWeightAccordings,cookingTime);
        }

        public void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
