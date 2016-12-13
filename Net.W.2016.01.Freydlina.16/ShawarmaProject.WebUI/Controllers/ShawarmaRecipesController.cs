using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShawarmaProject.DB;

namespace ShawarmaProject.WebUI.Controllers
{
    public class ShawarmaRecipesController : Controller
    {
        private ShawarmaDBEntities db = new ShawarmaDBEntities();

        // GET: ShawarmaRecipes
        public async Task<ActionResult> Index()
        {
            var shawarmaRecipes = db.ShawarmaRecipes.Include(s => s.Ingradient).Include(s => s.Shawarma);
            return View(await shawarmaRecipes.ToListAsync());
        }

        // GET: ShawarmaRecipes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShawarmaRecipe shawarmaRecipe = await db.ShawarmaRecipes.FindAsync(id);
            if (shawarmaRecipe == null)
            {
                return HttpNotFound();
            }
            return View(shawarmaRecipe);
        }

        // GET: ShawarmaRecipes/Create
        public ActionResult Create()
        {
            ViewBag.IngradientId = new SelectList(db.Ingradients, "IngradientId", "IngradientName");
            ViewBag.ShawarmaId = new SelectList(db.Shawarmas, "ShawarmaId", "ShawarmaName");
            return View();
        }

        // POST: ShawarmaRecipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ShawarmaRecipeId,ShawarmaId,IngradientId,Weight")] ShawarmaRecipe shawarmaRecipe)
        {
            if (ModelState.IsValid)
            {
                db.ShawarmaRecipes.Add(shawarmaRecipe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IngradientId = new SelectList(db.Ingradients, "IngradientId", "IngradientName", shawarmaRecipe.IngradientId);
            ViewBag.ShawarmaId = new SelectList(db.Shawarmas, "ShawarmaId", "ShawarmaName", shawarmaRecipe.ShawarmaId);
            return View(shawarmaRecipe);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
