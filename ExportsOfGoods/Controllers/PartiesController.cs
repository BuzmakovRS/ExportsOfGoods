using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExportsOfGoods.Models;
using System.Globalization;

namespace ExportsOfGoods.Controllers
{
    public class PartiesController : Controller
    {
        private ExportsContext db = new ExportsContext();

        // GET: Parties
        [Authorize]
        public async Task<ActionResult> Index()
        {
            if (HttpContext.User.IsInRole("admin"))
            {
                ViewBag.isAdmin = true;
            }
            var parties = db.Parties.Include(p => p.Product).Include(p=>p.TypeOfInspection);
            return View(await parties.ToListAsync());
        }

        // GET: Parties/Details/5
        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parti parti = await db.Parties.FindAsync(id);
            parti.Product = await db.Products.FindAsync(parti.ProductId);
            parti.TypeOfInspection = await db.TypeOfInspection.FindAsync(parti.TypeOfInspectionId);

            if (parti == null)
            {
                return HttpNotFound();
            }
            return View(parti);
        }

        // GET: Parties/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "NameProducer");
            ViewBag.TypeOfInspectionId = new SelectList(db.TypeOfInspection, "Id", "Type");
            return View();
        }

        // POST: Parties/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ProductId,PartiSize,TypeOfInspectionId")] Parti parti)
        {
            if (ModelState.IsValid)
            {
                parti.InspectionTime = SetTimeInsp(parti);
                db.Parties.Add(parti);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "NameProducer", parti.ProductId);
            ViewBag.TypeOfInspectionId = new SelectList(db.TypeOfInspection, "Id", "Type", parti.TypeOfInspectionId);

            return View(parti);
        }
        
        // GET: Parties/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parti parti = await db.Parties.FindAsync(id);
            parti.Product = await db.Products.FindAsync(parti.ProductId);
            parti.TypeOfInspection = await db.TypeOfInspection.FindAsync(parti.TypeOfInspectionId);

            if (parti == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", parti.ProductId);
            ViewBag.TypeOfInspectionId = new SelectList(db.TypeOfInspection, "Id", "Type", parti.TypeOfInspectionId);

            return View(parti);
        }

        // POST: Parties/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ProductId,PartiSize,TypeOfInspectionId")] Parti parti)
        {

            if (ModelState.IsValid)
            {
                parti.InspectionTime = SetTimeInsp(parti);
                db.Entry(parti).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", parti.ProductId);
            ViewBag.TypeOfInspectionId = new SelectList(db.TypeOfInspection, "Id", "Type", parti.TypeOfInspectionId);

            return View(parti);
        }

        // GET: Parties/Delete/5
        [Authorize(Roles ="admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parti parti = await db.Parties.FindAsync(id);
            parti.Product = await db.Products.FindAsync(parti.ProductId);
            parti.TypeOfInspection = await db.TypeOfInspection.FindAsync(parti.TypeOfInspectionId);

            if (parti == null)
            {
                return HttpNotFound();
            }
            return View(parti);
        }

        // POST: Parties/Delete/5
        [Authorize(Roles ="admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parti parti = await db.Parties.FindAsync(id);
            db.Parties.Remove(parti);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private DateTime SetTimeInsp(Parti p)
        {
            DateTime dt = new DateTime(2000, 1, 1);
            p.TypeOfInspection = db.TypeOfInspection.Find(p.TypeOfInspectionId);
            int minInsp = (int)Math.Round(p.TypeOfInspection.Time * p.PartiSize / 30);
            if (minInsp == 0) minInsp++;
            int minR = minInsp * 30;
            dt = dt.AddMinutes(minR);
            p.InspectionTime = dt;
            return (DateTime)p.InspectionTime;
        }
    }
}
