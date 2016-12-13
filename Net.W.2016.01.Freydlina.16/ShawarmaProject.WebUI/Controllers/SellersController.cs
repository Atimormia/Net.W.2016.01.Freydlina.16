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
    public class SellersController : Controller
    {
        private ShawarmaDBEntities db = new ShawarmaDBEntities();

        // GET: Sellers
        public async Task<ActionResult> Index()
        {
            var sellers = db.Sellers.Include(s => s.SellingPoint);
            return View(await sellers.ToListAsync());
        }

        // GET: Sellers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = await db.Sellers.FindAsync(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // GET: Sellers/Create
        public ActionResult Create()
        {
            ViewBag.SellingPointId = new SelectList(db.SellingPoints, "SellingPointId", "Address");
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "SellerId,SellerName,SellingPointId")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                db.Sellers.Add(seller);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.SellingPointId = new SelectList(db.SellingPoints, "SellingPointId", "Address", seller.SellingPointId);
            return View(seller);
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
