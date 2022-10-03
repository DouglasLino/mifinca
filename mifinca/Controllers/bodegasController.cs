using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mifinca.Models;

namespace mifinca.Controllers
{
    public class bodegasController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: bodegas
        public ActionResult Index()
        {
            var bodega = db.bodega.Include(b => b.finca);
            return View(bodega.ToList());
        }

        // GET: bodegas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = db.bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // GET: bodegas/Create
        public ActionResult Create()
        {
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca");
            return View();
        }

        // POST: bodegas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_bodega,id_finca")] bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.bodega.Add(bodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca", bodega.id_finca);
            return View(bodega);
        }

        // GET: bodegas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = db.bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca", bodega.id_finca);
            return View(bodega);
        }

        // POST: bodegas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_bodega,id_finca")] bodega bodega)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bodega).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_finca = new SelectList(db.finca, "id_finca", "foto_finca", bodega.id_finca);
            return View(bodega);
        }

        // GET: bodegas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bodega bodega = db.bodega.Find(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // POST: bodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bodega bodega = db.bodega.Find(id);
            db.bodega.Remove(bodega);
            db.SaveChanges();
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
