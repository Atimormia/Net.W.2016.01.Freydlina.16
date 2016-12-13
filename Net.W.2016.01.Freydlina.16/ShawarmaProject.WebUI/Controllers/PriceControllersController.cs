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
    public class PriceControllersController : Controller
    {
        private ShawarmaDBEntities db = new ShawarmaDBEntities();

        // GET: PriceControllers
        public async Task<ActionResult> Index()
        {
            var priceControllers = db.PriceControllers.Include(p => p.Shawarma).Include(p => p.SellingPoint);
            return View(await priceControllers.ToListAsync());
        }

        // GET: PriceControllers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PriceController priceController = await db.PriceControllers.FindAsync(id);
            if (priceController == null)
            {
                return HttpNotFound();
            }
            return View(priceController);
        }

        // GET: PriceControllers/Create
        public ActionResult Create()
        {
            ViewBag.ShwarmaId = new SelectList(db.Shawarmas, "ShawarmaId", "ShawarmaName");
            ViewBag.SellingPointId = new SelectList(db.SellingPoints, "SellingPointId", "Address");
            return View();
        }

        // POST: PriceControllers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "PriceControllerId,ShwarmaId,Price,SellingPointId,Comment")] PriceController priceController)
        {
            if (ModelState.IsValid)
            {
                db.PriceControllers.Add(priceController);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ShwarmaId = new SelectList(db.Shawarmas, "ShawarmaId", "ShawarmaName", priceController.ShwarmaId);
            ViewBag.SellingPointId = new SelectList(db.SellingPoints, "SellingPointId", "Address", priceController.SellingPointId);
            return View(priceController);
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
