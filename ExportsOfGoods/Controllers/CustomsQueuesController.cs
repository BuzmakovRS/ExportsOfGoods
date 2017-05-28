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
    public class CustomsQueuesController : Controller
    {
        private ExportsContext db = new ExportsContext();

        // GET: CustomsQueues
        public async Task<ActionResult> Index()
        {
            var customsQueues = db.CustomsQueues.Include(c => c.Customs).Include(c => c.Parti);
            return View(await customsQueues.ToListAsync());
        }

        // GET: CustomsQueues/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomsQueue customsQueue = await db.CustomsQueues.FindAsync(id);
            customsQueue.Customs = await db.Customs.FindAsync(customsQueue.CustomsId);
            customsQueue.Parti = await db.Parties.FindAsync(customsQueue.PartiId);
            if (customsQueue == null)
            {
                return HttpNotFound();
            }
            return View(customsQueue);
        }

        // GET: CustomsQueues/Create
        public ActionResult Create()
        {
            ViewBag.CustomsId = new SelectList(db.Customs, "Id", "Name");
            ViewBag.PartiId = new SelectList(db.Parties, "Id", "Id");
            return View();
        }

        // POST: CustomsQueues/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomsId,PartiId")] CustomsQueue customsQueue, string timeBegInsp, string timeEndInsp)
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParseExact(timeBegInsp, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("timeBegInsp", "Формат даты: dd.MM.yyyy HH:mm");
            else
            {
                customsQueue.TimeBegInsp = dt;
            }
            if (!DateTime.TryParseExact(timeEndInsp, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("timeEndInsp", "Формат даты: dd.MM.yyyy HH:mm");
            else
            {
                customsQueue.TimeEndInsp = dt;
            }
            if (ModelState.IsValid)
            {
                db.CustomsQueues.Add(customsQueue);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CustomsId = new SelectList(db.Customs, "Id", "Name", customsQueue.CustomsId);
            ViewBag.PartiId = new SelectList(db.Parties, "Id", "Id", customsQueue.PartiId);
            return View(customsQueue);
        }

        // GET: CustomsQueues/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomsQueue customsQueue = await db.CustomsQueues.FindAsync(id);
            customsQueue.Customs = await db.Customs.FindAsync(customsQueue.CustomsId);
            customsQueue.Parti = await db.Parties.FindAsync(customsQueue.PartiId);
            if (customsQueue == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomsId = new SelectList(db.Customs, "Id", "Name", customsQueue.CustomsId);
            ViewBag.PartiId = new SelectList(db.Parties, "Id", "Id", customsQueue.PartiId);
            return View(customsQueue);
        }

        // POST: CustomsQueues/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomsId,PartiId")] CustomsQueue customsQueue, string timeBegIns, string timeEndIns)
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParseExact(timeBegIns, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("", "Укажите дату в формате dd.MM.yyyy HH:mm");
            else
            {
                customsQueue.TimeBegInsp = dt;
            }
            if (!DateTime.TryParseExact(timeEndIns, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
                ModelState.AddModelError("timeEndInsp", "Укажите дату в формате dd.MM.yyyy HH:mm");
            else
            {
                customsQueue.TimeEndInsp = dt;
            }
            if (ModelState.IsValid)
            {
                db.Entry(customsQueue).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CustomsId = new SelectList(db.Customs, "Id", "Name", customsQueue.CustomsId);
            ViewBag.PartiId = new SelectList(db.Parties, "Id", "Id", customsQueue.PartiId);
            return View(customsQueue);
        }

        // GET: CustomsQueues/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomsQueue customsQueue = await db.CustomsQueues.FindAsync(id);
            customsQueue.Customs = await db.Customs.FindAsync(customsQueue.CustomsId);
            customsQueue.Parti = await db.Parties.FindAsync(customsQueue.PartiId);
            if (customsQueue == null)
            {
                return HttpNotFound();
            }
            return View(customsQueue);
        }

        // POST: CustomsQueues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomsQueue customsQueue = await db.CustomsQueues.FindAsync(id);
            db.CustomsQueues.Remove(customsQueue);
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

        public async Task<string> GetInspTimeEnd(string tb, string pId)
        {
            DateTime dt = new DateTime();
            if (!DateTime.TryParseExact(tb, "dd.MM.yyyy HH:mm", new CultureInfo("ru-RU"), DateTimeStyles.None, out dt))
            {
                return "EROR";
            }
            else
            {
                Parti parti = await db.Parties.FindAsync(Convert.ToInt32(pId));
                dt = dt.AddHours(parti.InspectionTime.Value.Hour);
                dt = dt.AddMinutes(parti.InspectionTime.Value.Minute);
                return  dt.ToString("dd.MM.yyyy HH:mm");
            }

        }

    }
}
