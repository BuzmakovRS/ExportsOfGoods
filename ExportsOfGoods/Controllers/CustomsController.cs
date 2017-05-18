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

namespace ExportsOfGoods.Controllers
{
    public class CustomsController : Controller
    {
        private ExportsContext db = new ExportsContext();

        // GET: Customs
        public async Task<ActionResult> Index()
        {
            var customs = db.Customs.Include(c => c.CountryRec).Include(c => c.CountrySend);
            return View(await customs.ToListAsync());
        }

        // GET: Customs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customs customs = await db.Customs.FindAsync(id);
            customs.CountrySend = await db.Countries.FindAsync(customs.SenderId);
            customs.CountryRec = await db.Countries.FindAsync(customs.RecipientId);
            if (customs == null)
            {
                return HttpNotFound();
            }
            return View(customs);
        }

        // GET: Customs/Create
        public ActionResult Create()
        {
            ViewBag.RecipientId = new SelectList(db.Countries, "CountryId", "CountryName");
            ViewBag.SenderId = new SelectList(db.Countries, "CountryId", "CountryName");
            return View();
        }

        // POST: Customs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,SenderId,RecipientId")] Customs customs)
        {
            if (ModelState.IsValid)
            {
                db.Customs.Add(customs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RecipientId = new SelectList(db.Countries, "CountryId", "CountryName", customs.RecipientId);
            ViewBag.SenderId = new SelectList(db.Countries, "CountryId", "CountryName", customs.SenderId);
            return View(customs);
        }

        // GET: Customs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customs customs = await db.Customs.FindAsync(id);
            customs.CountrySend = await db.Countries.FindAsync(customs.SenderId);
            customs.CountryRec = await db.Countries.FindAsync(customs.RecipientId);
            if (customs == null)
            {
                return HttpNotFound();
            }
            ViewBag.RecipientId = new SelectList(db.Countries, "CountryId", "CountryName", customs.RecipientId);
            ViewBag.SenderId = new SelectList(db.Countries, "CountryId", "CountryName", customs.SenderId);
            return View(customs);
        }

        // POST: Customs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,SenderId,RecipientId")] Customs customs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RecipientId = new SelectList(db.Countries, "CountryId", "CountryName", customs.RecipientId);
            ViewBag.SenderId = new SelectList(db.Countries, "CountryId", "CountryName", customs.SenderId);
            return View(customs);
        }

        // GET: Customs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customs customs = await db.Customs.FindAsync(id);
            customs.CountrySend = await db.Countries.FindAsync(customs.SenderId);
            customs.CountryRec = await db.Countries.FindAsync(customs.RecipientId);
            if (customs == null)
            {
                return HttpNotFound();
            }
            return View(customs);
        }

        // POST: Customs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customs customs = await db.Customs.FindAsync(id);
            db.Customs.Remove(customs);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
