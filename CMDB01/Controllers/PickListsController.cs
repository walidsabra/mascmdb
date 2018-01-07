using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMDB01.Models;

namespace CMDB01.Controllers
{
    public class PickListsController : Controller
    {
        private Models.CMDB01 db = new Models.CMDB01();

        // GET: PickLists
        public ActionResult Index()
        {
            return View(db.PickLists.ToList());
        }

        // GET: PickLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickList pickList = db.PickLists.Find(id);
            if (pickList == null)
            {
                return HttpNotFound();
            }
            return View(pickList);
        }

        // GET: PickLists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PickLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PickListName,PickListValue")] PickList pickList)
        {
            if (ModelState.IsValid)
            {
                db.PickLists.Add(pickList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pickList);
        }

        // GET: PickLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickList pickList = db.PickLists.Find(id);
            if (pickList == null)
            {
                return HttpNotFound();
            }
            return View(pickList);
        }

        // POST: PickLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PickListName,PickListValue")] PickList pickList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pickList);
        }

        // GET: PickLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PickList pickList = db.PickLists.Find(id);
            if (pickList == null)
            {
                return HttpNotFound();
            }
            return View(pickList);
        }

        // POST: PickLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PickList pickList = db.PickLists.Find(id);
            db.PickLists.Remove(pickList);
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
