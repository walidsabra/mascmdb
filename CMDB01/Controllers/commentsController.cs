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
    public class commentsController : Controller
    {
        private Models.CMDB db = new Models.CMDB();

        //Get List of CommentTypes --------------------------------------------------------
        private void GetCommentTypes()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName == "CommentTypes").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.CommentTypes = listSelectListItems;
            //--------------------------------------------------------------------
        }

        // GET: comments
        public ActionResult Index()
        {
            return View(db.comments.ToList());
        }

        // GET: comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // GET: comments/Create
        public ActionResult Create(int entity_id, string entity)
        {
            GetCommentTypes();

            return View();
        }

        // POST: comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string user, string comment, int entity_id, string entity, bool featured, string type, string Link)
        {
            comment cm = new comment();
            cm.entity_Id = entity_id;
            cm.timestamp = DateTime.Now;
            cm.entity = entity;
            cm.user = user;
            cm.Comment = comment;
            cm.featured = featured;
            cm.Type = type;
            cm.Link = Link;

            db.comments.Add(cm);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        // GET: comments/Edit/5
        public ActionResult Edit(int? id)
        {
            GetCommentTypes();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,entity_Id,entity,user,timestamp,Comment,featured,type")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(comment);
        }

        // GET: comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            comment comment = db.comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
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
