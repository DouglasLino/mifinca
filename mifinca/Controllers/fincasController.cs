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
    public class fincasController : Controller
    {
        private mifincaEntities db = new mifincaEntities();

        // GET: fincas
        public ActionResult Index()
        {
            var finca = db.finca.Include(f => f.bodega);
            return View(finca.ToList());
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
            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega");
            return View();
        }

        // POST: fincas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_finca,id_bodega,foto_finca,nombre_finca,extension,planoCatastral,localizacionEntrada,tablones,desripcion,msnm_altura")] finca finca)
        {
            if (ModelState.IsValid)
            {
                db.finca.Add(finca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega", finca.id_bodega);
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
            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega", finca.id_bodega);
            return View(finca);
        }

        // POST: fincas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase update_foto_finca, HttpPostedFileBase update_planoCatastral , [Bind(Include = "id_finca,id_bodega,foto_finca,nombre_finca,extension,planoCatastral,localizacionEntrada,tablones,desripcion,msnm_altura")] finca finca )
        {
            if (ModelState.IsValid)
            {

                if (update_planoCatastral != null)
                {
                    string _FileNamePlanoCatastral = Path.GetFileName(update_planoCatastral.FileName);
                    string _path = Path.Combine(Server.MapPath("~/imgs/plano_catastral"), _FileNamePlanoCatastral);
                    update_planoCatastral.SaveAs(_path);

                    finca.planoCatastral = _FileNamePlanoCatastral;
                    db.Entry(finca).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                if (update_foto_finca != null)
                {
                    string _FileName = Path.GetFileName(update_foto_finca.FileName);
                    string _path = Path.Combine(Server.MapPath("~/imgs/fincas"), _FileName);
                    update_foto_finca.SaveAs(_path);

                    finca.foto_finca = _FileName;
                    db.Entry(finca).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                

                //valida si no carga  foto_finca
                if (finca.foto_finca == null || finca.planoCatastral == null)
                {
                    return RedirectToAction("Index");
                }


                db.Entry(finca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_bodega = new SelectList(db.bodega, "id_bodega", "id_bodega", finca.id_bodega);
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
