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
    public class IngredientsController : Controller
    {
        private ShawarmaDBEntities db = new ShawarmaDBEntities();

        // GET: Ingradients
        public async Task<ActionResult> Index()
        {
            var ingradients = db.Ingradients.Include(i => i.IngradientCategory);
            return View(await ingradients.ToListAsync());
        }

        // GET: Ingradients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingradient ingradient = await db.Ingradients.FindAsync(id);
            if (ingradient == null)
            {
                return HttpNotFound();
            }
            return View(ingradient);
        }

        // GET: Ingradients/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.IngradientCategories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Ingradients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IngradientId,IngradientName,TotalWeight,CategoryId")] Ingradient ingradient)
        {
            if (ModelState.IsValid)
            {
                db.Ingradients.Add(ingradient);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.IngradientCategories, "CategoryId", "CategoryName", ingradient.CategoryId);
            return View(ingradient);
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
