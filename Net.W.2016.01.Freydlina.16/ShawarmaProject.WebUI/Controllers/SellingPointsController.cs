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
    public class SellingPointsController : Controller
    {
        private ShawarmaDBEntities db = new ShawarmaDBEntities();

        // GET: SellingPoints
        public async Task<ActionResult> Index()
        {
            var sellingPoints = db.SellingPoints.Include(s => s.SellingPointCategory1);
            return View(await sellingPoints.ToListAsync());
        }

        // GET: SellingPoints/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SellingPoint sellingPoint = await db.SellingPoints.FindAsync(id);
            if (sellingPoint == null)
            {
                return HttpNotFound();
            }
            return View(sellingPoint);
        }

        // GET: SellingPoints/Create
        public ActionResult Create()
        {
            ViewBag.SellingPointCategory = new SelectList(db.SellingPointCategories, "SellingPointCategoryId", "SellingPointCategoryName");
            return View();
        }

        // POST: SellingPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SellingPointId,Address,SellingPointCategory,ShawarmaTitle")] SellingPoint sellingPoint)
        {
            if (ModelState.IsValid)
            {
                db.SellingPoints.Add(sellingPoint);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SellingPointCategory = new SelectList(db.SellingPointCategories, "SellingPointCategoryId", "SellingPointCategoryName", sellingPoint.SellingPointCategory);
            return View(sellingPoint);
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
