using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShawarmaProject
{
    public class DatabaseQueries
    {
        public static bool AddIngredient(string name, int weight, string categoryName = "")
        {
            using (var ctx = new ShawarmaDBEntities())
            {
                Ingradient ingradient = ctx.Ingradients.FirstOrDefault(ingr => ingr.IngradientName == name);
                if (ingradient != null)
                {
                    ingradient.TotalWeight += weight;
                }
                else
                {
                    IngradientCategory category =
                        ctx.IngradientCategories.FirstOrDefault(ingr => ingr.CategoryName == categoryName);
                    if (categoryName != "")
                    {
                        int categoryId;
                        if (category == null)
                        {
                            ctx.IngradientCategories.Add(new IngradientCategory {CategoryName = categoryName});
                            IngradientCategory category1 =
                                ctx.IngradientCategories.Local.First(cat => cat.CategoryName == categoryName);
                            categoryId = category1.CategoryId;
                        }
                        else
                        {
                            categoryId = category.CategoryId;
                        }
                        ctx.Ingradients.Add(new Ingradient
                        {
                            CategoryId = categoryId,
                            IngradientName = name,
                            TotalWeight = weight
                        });
                    }
                    else
                    {
                        ctx.Ingradients.Add(new Ingradient
                        {
                            IngradientName = name,
                            TotalWeight = weight
                        });
                    }
                }

                return Commit(ctx);
            }
        }

        public static bool AddRecipe(string shawarmaName, IngradientWeight[] ingradientWeightAccordings, int cookingTime = 0)
        {
            using (var ctx = new ShawarmaDBEntities())
            {
                Shawarma shawarma = ctx.Shawarmas.FirstOrDefault(sh => sh.ShawarmaName == shawarmaName) ?? new Shawarma
                {
                    ShawarmaName = shawarmaName,
                    CookingTime = cookingTime
                };
                ShawarmaRecipe recipe = new ShawarmaRecipe();
                recipe.Shawarma = shawarma;
                foreach (var ingradientWeightAccording in ingradientWeightAccordings)
                {
                    Ingradient ingradient =
                        ctx.Ingradients.FirstOrDefault(
                            ing => ing.IngradientName == ingradientWeightAccording.IngradientName);
                    if (ingradient == null)
                    {
                        AddIngredient(ingradientWeightAccording.IngradientName, 0);
                        ingradient =
                            ctx.Ingradients.FirstOrDefault(
                                ing => ing.IngradientName == ingradientWeightAccording.IngradientName);
                    }
                    recipe.IngradientId = ingradient.IngradientId;
                    recipe.Weight = ingradientWeightAccording.Weight;
                    ctx.ShawarmaRecipes.Add(recipe);
                }
                return Commit(ctx);
            }
        }

        private static bool Commit(DbContext ctx)
        {
            try
            {
                Debug.WriteLine("Saving changes success: "+ctx.SaveChanges());
                return true;
            }
            catch (DbUpdateException e)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"DbUpdateException error details - {e?.InnerException?.InnerException?.Message}");
                foreach (var eve in e.Entries)
                    sb.AppendLine($"Entity of type {eve.Entity.GetType().Name} in state {eve.State} could not be updated");
                Debug.Write(sb.ToString());
                return false;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                        Debug.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                return false;
            }
        }

    }

    public struct IngradientWeight
    {
        public string IngradientName { get; set; }
        public int Weight { get; set; }
    }
}
