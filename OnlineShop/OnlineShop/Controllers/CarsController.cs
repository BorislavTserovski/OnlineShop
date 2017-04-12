using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using System.IO;

namespace OnlineShop.Controllers
{
    public class CarsController : Controller
    {
        private ShopDbContext db = new ShopDbContext();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.Buyer);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Cars/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Make,Model,Price,Year,DateAdded,Image,BuyerId")] Car car,
            HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                using (MemoryStream ms = new MemoryStream())
                    if (file!= null)
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        car.Image = array;
                    }
               
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuyerId = new SelectList(db.Users, "Id", "FirstName", car.BuyerId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "FirstName", car.BuyerId);
            return View(car);
        }

        // POST: Cars/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Model,Price,Year,DateAdded,Image,BuyerId")] Car car,
            HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file!=null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                        car.Image = array;
                    }
                }
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "FirstName", car.BuyerId);
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
