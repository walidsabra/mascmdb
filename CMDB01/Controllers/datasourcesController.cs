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
		public ActionResult Index(string SearchValue, string dc, string dv, string acc, string StartWith, string dsST)
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
                    lsDSs = db.datasources.Where(a => a.ServerFarm.DataCenter.Equals(null));
                }
                else
                {
                    lsDSs = db.datasources.Where(a => a.ServerFarm.DataCenter.Equals(dc));
                }

            }
            else if (!string.IsNullOrEmpty(dv))
            {
                if (dv == "null")
                {
                    lsDSs = db.datasources.Where(a => a.ServerFarm.DeployedVersion.Equals(null));
                }
                else
                {
                    lsDSs = db.datasources.Where(a => a.ServerFarm.DeployedVersion.Equals(dv));
                }

            }
            else if (!string.IsNullOrEmpty(acc))
            {
                if (acc == "null")
                {
                    lsDSs = db.datasources.Where(a => a.ServerFarm.account.Name.Equals(null));
                }
                else
                {
                    lsDSs = db.datasources.Where(a => a.ServerFarm.account.Name.Equals(acc));
                }

            }
            else if (!string.IsNullOrEmpty(dsST))
            {
                if (dsST == "null")
                {
                    lsDSs = db.datasources.Where(a => a.Status.Equals(null));
                }
                else
                {
                    lsDSs = db.datasources.Where(a => a.Status.Equals(dsST));
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
            GetDatasourceEntityTypes();
            GetContacts();
            GetDSStatusList();

            return View();
		}



		// POST: datasources/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Name,Description,GUID,Monitored,Status")] datasource datasource, int serverId, string hdContactsArray)
        {
            serverFarms server = db.serverFarms.Where(x => x.Id == serverId).FirstOrDefault();
            datasource.ServerFarm = server;

            //if (contactId != null) { 
            //	List<contact> contacts = new List<contact>();
            //	foreach (int x in contactId)
            //	{
            //		contacts.Add(db.contacts.Where(ex => ex.Id == x).FirstOrDefault());
            //	}

            //	datasource.dcontacts = contacts;
            //}
            

            if (ModelState.IsValid)
            {
                processcontacts(datasource, hdContactsArray, "Create");
                db.datasources.Add(datasource);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(datasource);
        }

        private void processcontacts(datasource datasource, string hdContactsArray, string mode)
        {
            if (!string.IsNullOrEmpty(hdContactsArray))
            {
                List<ContactLinks> contactLinks = new List<ContactLinks>();
                var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<contactRec>>(hdContactsArray);
                foreach (var item in items)
                {
                    if (item.isOutage)
                    {
                        ContactLinks con = new ContactLinks();
                        con.datasource = datasource;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Datasource";
                        con.entityCategory = "Outage";
                        contactLinks.Add(con);
                    }
                    if (item.isMajor)
                    {
                        ContactLinks con = new ContactLinks();
                        con.datasource = datasource;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Datasource";
                        con.entityCategory = "Major";
                        contactLinks.Add(con);
                    }
                    if (item.isMinor)
                    {
                        ContactLinks con = new ContactLinks();
                        con.datasource = datasource;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Datasource";
                        con.entityCategory = "Minor";
                        contactLinks.Add(con);
                    }
                    if (!string.IsNullOrEmpty(item.opt))
                    {
                        ContactLinks con = new ContactLinks();
                        con.datasource = datasource;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Datasource";
                        con.entityCategory = item.opt;
                        contactLinks.Add(con);
                    }
                }
                if (mode == "Create")
                {
                    datasource.DatasourceContacts = contactLinks;

                }
                if (mode == "Edit")
                {
                    db.contactlinks.AddRange(contactLinks);
                }
            }
        }

        // GET: datasources/Edit/5
        public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            GetContacts();
            GetDatasourceEntityTypes();
            GetDSStatusList();
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
		public ActionResult Edit([Bind(Include = "Id,Name,Description,GUID,Monitored,Status")] datasource datasource, string hdContactsArray)
		{
			if (ModelState.IsValid)
			{
                processcontacts(datasource, hdContactsArray, "Edit");
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
            db.contactlinks.RemoveRange(db.contactlinks.Where(a => a.datasource.Id == id));

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
				////Get Contacts --------------------------------------------------
				//List<SelectListItem> listSelectListItems = new List<SelectListItem>();
				//foreach (contact contact in datasource.contacts)
				//{
				//	SelectListItem selectList = new SelectListItem()
				//	{
				//		Text = contact.Name,
				//		Value = contact.Id.ToString(),
				//		Selected = false
				//	};
				//	listSelectListItems.Add(selectList);
				//}
				//ViewBag.contacts = listSelectListItems;
				////-----------------------------------------------------------------------


			}
			catch (Exception)
			{

				throw;
			}
		}
        private void GetDatasourceEntityTypes()
        {
            //Get List of Contacts ----------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName == "DatasourceEntityType").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.DatasourceEntityTypes = listSelectListItems;
            //--------------------------------------------------------------------
        }

        private void GetServers()
		{
			try
			{
				List<SelectListItem> SlistSelectListItems = new List<SelectListItem>();

				foreach (serverFarms server in db.serverFarms)
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

        private void GetDSStatusList()
        {
            //Get List of Contacts ----------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName == "DSStatus").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.DSStatusList = listSelectListItems;
            //--------------------------------------------------------------------
        }

        private void GetContacts()
		{
			try
			{
				//Get List of Contacts ----------------------------------------------
				List<SelectListItem> listSelectListItems = new List<SelectListItem>();

				foreach (contact contact in db.contacts.OrderBy(a=>a.Name))
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


        // GET: accounts/Delete/5
        public ActionResult DeleteDatasourceContact(int? id)
        {

            return View();
        }
        // POST: accounts/Delete/5
        [HttpPost, ActionName("DeleteDatasourceContact")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDatasourceContact(int dsId, int contactId)
        {
            try
            {
                db.contactlinks.RemoveRange(db.contactlinks.Where(a => a.datasource.Id == dsId && a.contact.Id == contactId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Edit", new { Id = dsId });
        }

    }
}
