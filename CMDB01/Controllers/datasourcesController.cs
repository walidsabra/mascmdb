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
	public class datasourcesController : Controller
	{
		private CMDB db = new CMDB();

		// GET: datasources
		public ActionResult Index(string SearchValue, string dc, string dv, string acc, string StartWith)
		{
            IQueryable<datasource> lsDSs;

            if (!string.IsNullOrEmpty(SearchValue))
            {
                lsDSs = db.datasources.Where(x => x.Name.Contains(SearchValue));
            }
            else if (!string.IsNullOrEmpty(dc))
            {
                if (dc == "null")
                {
                    lsDSs = db.datasources.Where(a => a.server.DataCenter.Equals(null));
                }
                else
                {
                    lsDSs = db.datasources.Where(a => a.server.DataCenter.Equals(dc));
                }

            }
            else if (!string.IsNullOrEmpty(dv))
            {
                if (dv == "null")
                {
                    lsDSs = db.datasources.Where(a => a.server.DeployedVersion.Equals(null));
                }
                else
                {
                    lsDSs = db.datasources.Where(a => a.server.DeployedVersion.Equals(dv));
                }

            }
            else if (!string.IsNullOrEmpty(acc))
            {
                if (acc == "null")
                {
                    lsDSs = db.datasources.Where(a => a.server.account.Name.Equals(null));
                }
                else
                {
                    lsDSs = db.datasources.Where(a => a.server.account.Name.Equals(acc));
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(StartWith))
                {
                    lsDSs = db.datasources.Where(x => x.Name.StartsWith(StartWith)).AsQueryable();
                }
                else
                {
                    lsDSs = db.datasources;
                }
            }
            return View(lsDSs.ToList());
		}

		// GET: datasources/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			datasource datasource = db.datasources.Find(id);

			GetDataSourceContacts(datasource);
            //Get DS Comments --------------------------------------------------
            List<comment> cmlistSelectListItems = new List<comment>();
            foreach (comment cm in db.comments.Where(a => a.entity_Id == id && a.entity == "Datasource"))
            {
                cmlistSelectListItems.Add(cm);
            }
            ViewBag.DSCMs = cmlistSelectListItems;
            //-----------------------------------------------------------------------

            if (datasource == null)
			{
				return HttpNotFound();
			}
			return View(datasource);

				}

		// GET: datasources/Create
		public ActionResult Create()
		{
			GetServers();

			GetContacts();

			return View();
		}



		// POST: datasources/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,Description,GUID,BeingMonitored")] datasource datasource, int serverId,  List<int> contactId)
		{
			server server = db.servers.Where(x => x.Id == serverId).FirstOrDefault();
			datasource.server = server;

			if (contactId != null) { 
				List<contact> contacts = new List<contact>();
				foreach (int x in contactId)
				{
					contacts.Add(db.contacts.Where(ex => ex.Id == x).FirstOrDefault());
				}

				datasource.contacts = contacts;
			}

			if (ModelState.IsValid)
			{
				db.datasources.Add(datasource);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(datasource);
		}

		// GET: datasources/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			datasource datasource = db.datasources.Find(id);
			if (datasource == null)
			{
				return HttpNotFound();
			}
			return View(datasource);
		}

		// POST: datasources/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Name,Description,GUID,BeingMonitored")] datasource datasource)
		{
			if (ModelState.IsValid)
			{
				db.Entry(datasource).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(datasource);
		}

		// GET: datasources/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			datasource datasource = db.datasources.Find(id);
			if (datasource == null)
			{
				return HttpNotFound();
			}
			return View(datasource);
		}

		// POST: datasources/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			datasource datasource = db.datasources.Find(id);
			db.datasources.Remove(datasource);
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



		private void GetDataSourceContacts(datasource datasource)
		{
			try
			{
				//Get Contacts --------------------------------------------------
				List<SelectListItem> listSelectListItems = new List<SelectListItem>();
				foreach (contact contact in datasource.contacts)
				{
					SelectListItem selectList = new SelectListItem()
					{
						Text = contact.Name,
						Value = contact.Id.ToString(),
						Selected = false
					};
					listSelectListItems.Add(selectList);
				}
				ViewBag.contacts = listSelectListItems;
				//-----------------------------------------------------------------------
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void GetServers()
		{
			try
			{
				List<SelectListItem> SlistSelectListItems = new List<SelectListItem>();

				foreach (server server in db.servers)
				{
					SelectListItem selectList = new SelectListItem()
					{
						Text = server.Name,
						Value = server.Id.ToString(),
						Selected = false
					};
					SlistSelectListItems.Add(selectList);
				}
				ViewBag.servers = SlistSelectListItems;
				//--------------------------------------------------------------------
			}
			catch (Exception)
			{

				throw;
			}
		}

		private void GetContacts()
		{
			try
			{
				//Get List of Contacts ----------------------------------------------
				List<SelectListItem> listSelectListItems = new List<SelectListItem>();

				foreach (contact contact in db.contacts)
				{
					SelectListItem selectList = new SelectListItem()
					{
						Text = contact.Name,
						Value = contact.Id.ToString(),
						Selected = false
					};
					listSelectListItems.Add(selectList);
				}
				ViewBag.contacts = listSelectListItems;
				//--------------------------------------------------------------------
			}
			catch (Exception)
			{

				throw;
			}
		}
        [HttpPost]
        public JsonResult GetComments(int DSId)
        {
            if (DSId > 0)
            {
                var comments = db.comments
                               .Where(x => x.entity_Id == DSId && x.entity == "Datasource")
                               .ToList();

                return Json(new { ok = true, data = comments, message = "ok" });
            }
            else
            {
                return null;
            }

        }

    }
}
