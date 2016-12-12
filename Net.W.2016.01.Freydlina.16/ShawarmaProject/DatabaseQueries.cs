using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
                if (ingradient != null) //there is specified ingradient; increasing
                {
                    ingradient.TotalWeight += weight;
                }
                else
                {
                    if (categoryName != "")
                    {
                        IngradientCategory category =
                            ctx.IngradientCategories.FirstOrDefault(ingr => ingr.CategoryName == categoryName);
                        if (category == null)//there is not specified category; creating
                        {
                            ctx.IngradientCategories.Add(new IngradientCategory {CategoryName = categoryName});
                            category =
                                ctx.IngradientCategories.Local.First(cat => cat.CategoryName == categoryName);
                        }
                        ctx.Ingradients.Add(new Ingradient
                        {
                            CategoryId = category.CategoryId,
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

        public static void ShowIngradients()
        {
            Debug.WriteLine("---Ingradients---");
            using (var ctx = new ShawarmaDBEntities())
            {
                foreach (var ingradient in ctx.Ingradients)
                {
                    Debug.WriteLine(ingradient.IngradientName);
                }
            }
            Debug.WriteLine("---End---");
        }

        public static void ShowShawarmas()
        {
            Debug.WriteLine("---Shawarmas---");
            using (var ctx = new ShawarmaDBEntities())
            {
                foreach (var shawarma in ctx.Shawarmas)
                {
                    Debug.WriteLine(shawarma.ShawarmaName);
                }
            }
            Debug.WriteLine("---End---");
        }

        public static bool ShawarmaSeillng(string shawarmaName)
        {
            using (var ctx = new ShawarmaDBEntities())
            {
                Shawarma shawarma = ctx.Shawarmas.FirstOrDefault(sh => sh.ShawarmaName == shawarmaName);
                if (shawarma == null) throw new Exception("there is not specified shawarma");
                foreach (var recipe in ctx.ShawarmaRecipes.Where(sh => sh.ShawarmaId == shawarma.ShawarmaId))
                {
                    if (recipe.Ingradient.TotalWeight >= recipe.Weight)
                        recipe.Ingradient.TotalWeight -= recipe.Weight;
                    else
                        throw new Exception("not enough ingradient "+recipe.Ingradient.IngradientName);
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
                ShawarmaRecipe recipe = ctx.ShawarmaRecipes.Create();
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

        public static bool NewPrice(string sellingPointTitle, string shawarmaName, decimal newPrice, string comment)
        {
            using (var ctx = new ShawarmaDBEntities())
            {
                SellingPoint point = ctx.SellingPoints.FirstOrDefault(p => p.ShawarmaTitle == sellingPointTitle);
                if (point == null)
                {
                    AddSellingPoint(sellingPointTitle, "", "");
                    point = ctx.SellingPoints.FirstOrDefault(p => p.ShawarmaTitle == sellingPointTitle);
                }
                Shawarma shawarma = ctx.Shawarmas.FirstOrDefault(sh => sh.ShawarmaName == shawarmaName);
                if (shawarma == null) return false; //there is not specified shawarma
                ctx.PriceControllers.Add(new PriceController
                {
                    ShwarmaId = shawarma.ShawarmaId,
                    Price = newPrice,
                    SellingPointId = point.SellingPointId,
                    Comment = comment
                });
                return Commit(ctx);
            }
        }

        public static bool AddSellingPoint(string sellingPointTitle, string address, string categoryName)
        {
            using (var ctx = new ShawarmaDBEntities())
            {
                SellingPointCategory category =
                    ctx.SellingPointCategories.FirstOrDefault(cat => cat.SellingPointCategoryName == categoryName);
                if (category == null)//there is not specified category; creating
                {
                    ctx.SellingPointCategories.Add(new SellingPointCategory {SellingPointCategoryName = categoryName});
                    category =
                        ctx.SellingPointCategories.FirstOrDefault(cat => cat.SellingPointCategoryName == categoryName);
                }
                ctx.SellingPoints.Add(new SellingPoint
                {
                    Address = address,
                    SellingPointCategory = category.SellingPointCategoryId,
                    ShawarmaTitle = sellingPointTitle
                });
                return Commit(ctx);
            }
        }

        public static bool AddNewSeller(string sellerName, string sellingPointName)
        {
            using (var ctx = new ShawarmaDBEntities())
            {
                SellingPoint point = ctx.SellingPoints.FirstOrDefault(p => p.ShawarmaTitle == sellingPointName);
                if (point == null) return false;//there is not specified selling point
                ctx.Sellers.Add(new Seller
                {
                    SellerName = sellerName,
                    SellingPointId = point.SellingPointId
                });
                return Commit(ctx);
            }
        }

        public static decimal SellingPointRevenue(string sellingPointName, DateTime start, DateTime end)
        {
            using (var ctx = new ShawarmaDBEntities())
            {
                decimal result = 0;
                SellingPoint point = ctx.SellingPoints.FirstOrDefault(p => p.ShawarmaTitle == sellingPointName);
                if (point == null) throw new Exception("there is not specified selling point");
                foreach (var seller in ctx.Sellers.Where(p => p.SellingPointId == point.SellingPointId))
                {
                    foreach (
                        var order in
                            seller.OrderHeaders.Where(order => order.OrderDate >= start && order.OrderDate <= end))
                    {
                        foreach (var orderDetail in order.OrderDetails)
                        {
                            PriceController price =
                                orderDetail.Shawarma.PriceControllers.LastOrDefault(
                                    p => p.SellingPointId == point.SellingPointId);
                            if (price ==null) throw new Exception("Shawarma hasn't price");
                            result += price.Price*orderDetail.Quantity;
                        }
                    }
                }
                return result;
            }
        }

        public static decimal SellerSalary
            (string sellerName, DateTime startPeriod, DateTime endPeriod, decimal workingRate, decimal cookingRate)
        {
            using (var ctx = new ShawarmaDBEntities())
            {
                decimal result = 0;
                Seller seller = ctx.Sellers.FirstOrDefault(s => s.SellerName == sellerName);
                if(seller==null) throw new Exception("there is not specified seller");
                foreach (var timeController in seller.TimeControllers)
                {
                    DateTime start, end;
                    if(timeController.WorkStart <= endPeriod)
                        if (timeController.WorkStart <= startPeriod)
                        {
                            if (timeController.WorkEnd >= startPeriod)
                            {
                                start = startPeriod;
                                end = timeController.WorkEnd <= endPeriod ? timeController.WorkEnd : endPeriod;
                            }
                            else continue;
                        }
                        else
                        {
                            start = timeController.WorkStart;
                            end = timeController.WorkEnd <= endPeriod ? timeController.WorkEnd : endPeriod;
                        }
                    else continue;
                    TimeSpan period = end - start;
                    result += period.Minutes*workingRate;
                }
                //foreach (var orderHeader in seller.OrderHeaders)
                //    foreach (var orderDetail in orderHeader.OrderDetails)
                //        result += orderDetail.Shawarma.CookingTime*orderDetail.Quantity*cookingRate;
                result +=
                    seller.OrderHeaders.SelectMany(orderHeader => orderHeader.OrderDetails)
                        .Sum(orderDetail => orderDetail.Shawarma.CookingTime*cookingRate);
            return result;
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
                sb.AppendLine($"DbUpdateException error details - {e.InnerException?.InnerException?.Message}");
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
        public int IngradientId { get; set; }
        public int Weight { get; set; }
    }
}
