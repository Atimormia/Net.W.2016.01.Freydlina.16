using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShawarmaProject.DB;

namespace ShawarmaProject.WebUI.Controllers
{
    public class DatabaseQueriesController : Controller
    {
        
        public ActionResult AddIngredient()
        {
            ViewBag.CategoryName = new SelectList(DatabaseRepresentation.GetIngredientCategories(), "CategoryName", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddIngredient([Bind(Include = "IngradientName,TotalWeight")] Ingradient ingradient)
        {
            return DatabaseQueries.AddIngredient(ingradient.IngradientName, ingradient.TotalWeight,
                Request.Form["CategoryName"])
                ? "Success"
                : "Error";
        }

        public ActionResult ShawarmaSelling()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string ShawarmaSelling([Bind(Include = "ShawarmaName")] Shawarma shawarma)
        {
            return DatabaseQueries.ShawarmaSelling(shawarma.ShawarmaName) ? "Success" : "Error";
        }

        public ActionResult AddRecipeShawarma()
        {
            if (Session["ShawarmaRecipes"] == null)
                Session["ShawarmaRecipes"] = new List<IngradientWeight>();
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddRecipeShawarma([Bind(Include = "ShawarmaName,CookingTime")] Shawarma shawarma)
        {
            return DatabaseQueries.AddRecipe(shawarma.ShawarmaName,
                (Session["ShawarmaRecipes"] as IEnumerable<IngradientWeight>)?.ToArray(), shawarma.CookingTime)
                ? "Success"
                : "Error";
        }

        public ActionResult AddRecipeItem()
        {
            ViewBag.IngradientName = new SelectList(DatabaseRepresentation.GetIngradients(), "IngradientName",
                "IngradientName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRecipeItem([Bind(Include = "Weight")] ShawarmaRecipe shawarmaRecipe)
        {
            if (Session["ShawarmaRecipes"] == null)
                Session["ShawarmaRecipes"] = new List<IngradientWeight>();
            if (Session["RecipesCounter"] == null)
                Session["RecipesCounter"] = 0;
            int recipeCounter = int.Parse(Session["RecipesCounter"].ToString());
            Session["RecipesCounter"] = recipeCounter+1;
            (Session["ShawarmaRecipes"] as List<IngradientWeight>)?.Add(new IngradientWeight
            {
                IngradientId = recipeCounter,
                IngradientName = Request.Form["IngradientName"],
                Weight = shawarmaRecipe.Weight
            });
            return RedirectToAction("AddRecipeShawarma");
        }

        public ActionResult AddRecipe()
        {
            return View(Session["ShawarmaRecipes"]);
        }

        public ActionResult DeleteRecipeItem(int id)
        {
            (Session["ShawarmaRecipes"] as List<IngradientWeight>)?.RemoveAt(id);
            return RedirectToAction("AddRecipeShawarma"); ;
        }

        //public string AddRecipe(string shawarmaName, IngradientWeight[] ingradientWeightAccordings, int cookingTime = 0)
        //{

        //}

        public ActionResult AddPrice()
        {
            ViewBag.SellingPointTitle = new SelectList(DatabaseRepresentation.GetSellingPoints(), "ShawarmaTitle", "ShawarmaTitle");
            ViewBag.ShawarmaName = new SelectList(DatabaseRepresentation.GetShawarmas(), "ShawarmaName", "ShawarmaName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddPrice([Bind(Include = "PriceControllerId,ShwarmaId,Price,SellingPointId,Comment")] PriceController priceController)
        {
            return DatabaseQueries.AddPrice(Request.Form["SellingPointTitle"], Request.Form["ShawarmaName"],
                priceController.Price, priceController.Comment)
                ? "Success"
                : "Error";
        }

        public ActionResult AddSellingPoint()
        {
            ViewBag.SellingPointCategory = new SelectList(DatabaseRepresentation.GetSellingPointCategories(),
                "SellingPointCategoryName", "SellingPointCategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddSellingPoint([Bind(Include = "Address,ShawarmaTitle")] SellingPoint sellingPoint)
        {
            return DatabaseQueries.AddSellingPoint(sellingPoint.ShawarmaTitle, sellingPoint.Address,
                Request.Form["SellingPointCategory"])
                ? "Success"
                : "Error";
        }

        public ActionResult AddSeller()
        {
            ViewBag.SellingPointTitle = new SelectList(DatabaseRepresentation.GetSellingPoints(), "ShawarmaTitle",
                "ShawarmaTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string AddSeller([Bind(Include = "SellerName")] Seller seller)
        {
            return DatabaseQueries.AddSeller(seller.SellerName,Request.Form["SellingPointTitle"])
               ? "Success"
               : "Error";
        }
        //public ActionResult SellingPointRevenue(string sellingPointName, DateTime start, DateTime end)
        //{

        //}

        //public ActionResult SellerSalary
        //    (string sellerName, DateTime startPeriod, DateTime endPeriod, decimal workingRate, decimal cookingRate)
        //{
        //}
    }
}