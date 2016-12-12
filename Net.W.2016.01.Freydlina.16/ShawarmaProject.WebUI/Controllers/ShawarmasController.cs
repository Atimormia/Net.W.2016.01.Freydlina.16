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
    public class ShawarmasController : Controller
    {
        private ShawarmaDBEntities db = new ShawarmaDBEntities();

        // GET: Shawarmas
        public async Task<ActionResult> Index()
        {
            return View(await db.Shawarmas.ToListAsync());
        }

        // GET: Shawarmas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Shawarma shawarma = await db.Shawarmas.FindAsync(id);
            if (shawarma == null)
            {
                return HttpNotFound();
            }
            return View(shawarma);
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
