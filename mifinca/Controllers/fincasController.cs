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
    public class fincasController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: fincas
        public ActionResult Index()
        {
            return View(db.finca.ToList());
        }

        // GET: fincas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            finca finca = db.finca.Find(id);
            if (finca == null)
            {
                return HttpNotFound();
            }
            return View(finca);
        }

        // GET: fincas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: fincas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_finca,id_bodega,foto_finca,nombre,extension,localizacion,tablones,desripcion")] finca finca)
        {
            if (ModelState.IsValid)
            {
                db.finca.Add(finca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(finca);
        }

        // GET: fincas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            finca finca = db.finca.Find(id);
            if (finca == null)
            {
                return HttpNotFound();
            }
            return View(finca);
        }

        // POST: fincas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_finca,id_bodega,foto_finca,nombre,extension,localizacion,tablones,desripcion")] finca finca)
        {
            if (ModelState.IsValid)
            {
                db.Entry(finca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(finca);
        }

        // GET: fincas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            finca finca = db.finca.Find(id);
            if (finca == null)
            {
                return HttpNotFound();
            }
            return View(finca);
        }

        // POST: fincas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            finca finca = db.finca.Find(id);
            db.finca.Remove(finca);
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
