using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mifinca.Models;

namespace mifinca.Controllers
{
    public class productoesController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: productoes
        public ActionResult Index()
        {
            var producto = db.producto.Include(p => p.bodega);
            return View(producto.ToList());
        }

        // GET: productoes/Details/5
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

        // GET: productoes/Create
        public ActionResult Create()
        {
            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega");
            return View();
        }

        // POST: productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase insert_foto_producto, [Bind(Include = "id_producto,id_bodega,foto_producto,nombre_producto,descripcion,cantidad")] producto producto)
        {
            if (ModelState.IsValid)
            {
                if (insert_foto_producto != null)
                {
                    string _FileNameInsertFotoProducto = Path.GetFileName(insert_foto_producto.FileName);
                    string _path = Path.Combine(Server.MapPath("~/imgs/productos"), _FileNameInsertFotoProducto);
                    insert_foto_producto.SaveAs(_path);

                    producto.foto_producto = _FileNameInsertFotoProducto;
                    db.producto.Add(producto);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }



                db.producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega", producto.id_bodega);
            return View(producto);
        }

        // GET: productoes/Edit/5
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

        // POST: productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase update_foto_producto, [Bind(Include = "id_producto,id_bodega,foto_producto,nombre_producto,descripcion,cantidad")] producto producto)
        {
            if (ModelState.IsValid)
            {
                if (update_foto_producto != null)
                {
                    string _FileNameUpdateFotoProducto = Path.GetFileName(update_foto_producto.FileName);
                    string _path = Path.Combine(Server.MapPath("~/imgs/productos"), _FileNameUpdateFotoProducto);
                    update_foto_producto.SaveAs(_path);

                    producto.foto_producto= _FileNameUpdateFotoProducto;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }



                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega", producto.id_bodega);
            return View(producto);
        }

        // GET: productoes/Delete/5
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

        // POST: productoes/Delete/5
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
