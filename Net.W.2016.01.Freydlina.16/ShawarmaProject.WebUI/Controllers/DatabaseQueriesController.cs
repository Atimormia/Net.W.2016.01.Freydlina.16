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
            return DatabaseQueries.AddIngredient(ingradient.IngradientName, ingradient.TotalWeight, Request.Form["CategoryName"]) ? "Success" : "Error";
        }

        //public ActionResult ShawarmaSeillng(string shawarmaName)
        //{
            
        //}

        //public ActionResult AddRecipe(string shawarmaName, IngradientWeight[] ingradientWeightAccordings, int cookingTime = 0)
        //{
            
        //}

        //public ActionResult NewPrice(string sellingPointTitle, string shawarmaName, decimal newPrice, string comment)
        //{
            
        //}

        //public ActionResult AddSellingPoint(string sellingPointTitle, string address, string categoryName)
        //{
            
        //}

        //public ActionResult AddNewSeller(string sellerName, string sellingPointName)
        //{
            
        //}

        //public ActionResult SellingPointRevenue(string sellingPointName, DateTime start, DateTime end)
        //{
            
        //}

        //public ActionResult SellerSalary
        //    (string sellerName, DateTime startPeriod, DateTime endPeriod, decimal workingRate, decimal cookingRate)
        //{
        //}
    }
}