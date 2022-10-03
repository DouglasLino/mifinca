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
    public class productoController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: producto
        public ActionResult Index()
        {
            var producto = db.producto.Include(p => p.bodega);
            return View(producto.ToList());
        }

        // GET: producto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: producto/Create
        public ActionResult Create()
        {
            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega");
            return View();
        }

        // POST: producto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codigo_producto,id_bodega,foto_producto,nombre_prod,extension,localizacion")] producto producto)
        {
            if (ModelState.IsValid)
            {
                db.producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega", producto.id_bodega);
            return View(producto);
        }

        // GET: producto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega", producto.id_bodega);
            return View(producto);
        }

        // POST: producto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codigo_producto,id_bodega,foto_producto,nombre_prod,extension,localizacion")] producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega", producto.id_bodega);
            return View(producto);
        }

        // GET: producto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            producto producto = db.producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            producto producto = db.producto.Find(id);
            db.producto.Remove(producto);
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
