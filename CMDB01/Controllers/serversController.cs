using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMDB01.Models;
using Newtonsoft.Json;

namespace CMDB01.Controllers
{
    public class serversController : Controller
    {
        private CMDB db = new CMDB();

        //Get List of Offering ----------------------------------------------
        private void GetOffering()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName == "Offering").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.Offering = listSelectListItems;
            //--------------------------------------------------------------------
        }

        // GET: servers
        public ActionResult Index(string SearchValue, string dc, string dv, string rl, string acc, string StartWith)
        {
            IQueryable<serverFarms> lsServers;

            if (!string.IsNullOrEmpty(SearchValue))
            {
                lsServers = db.serverFarms.Where(x=>x.Name.Contains(SearchValue));
            }
            else if (!string.IsNullOrEmpty(dc))
            {
                if(dc == "null")
                {
                    lsServers = db.serverFarms.Where(a => a.DataCenter.Equals(null));
                }
                else
                {
                    lsServers = db.serverFarms.Where(a => a.DataCenter.Equals(dc));
                }
                
            }
            else if (!string.IsNullOrEmpty(dv))
            {
                if (dv == "null")
                {
                    lsServers = db.serverFarms.Where(a => a.DeployedVersion.Equals(null));
                }
                else
                {
                    lsServers = db.serverFarms.Where(a => a.DeployedVersion.Equals(dv));
                }
            }
            else if (!string.IsNullOrEmpty(rl))
            {
                if (rl == "null")
                {
                    lsServers = db.serverFarms.Where(a => a.Role.Equals(null));
                }
                else
                {
                    lsServers = db.serverFarms.Where(a => a.Role.Equals(rl));
                }
            }
            else if (!string.IsNullOrEmpty(acc))
            {
                if (acc == "null")
                {
                    lsServers = db.serverFarms.Where(a => a.account.Name.Equals(null));
                }
                else
                {
                    lsServers = db.serverFarms.Where(a => a.account.Name.Equals(acc));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(StartWith))
                {
                    lsServers = db.serverFarms.Where(x => x.Name.StartsWith(StartWith)).AsQueryable();
                }
                else
                {
                    lsServers = db.serverFarms;
                }

            }
            return View(lsServers.ToList());
        }

        // GET: servers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serverFarms server = db.serverFarms.Find(id);

            //Get Server Datasources --------------------------------------------------
            List<SelectListItem> dslistSelectListItems = new List<SelectListItem>();
            foreach (datasource ds in server.datasources.OrderBy(a => a.Name))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = ds.Name,
                    Value = ds.Id.ToString(),
                    Selected = false
                };
                dslistSelectListItems.Add(selectList);
            }
            ViewBag.ServerDSs = dslistSelectListItems;
            //-----------------------------------------------------------------------

            //Get Server Comments --------------------------------------------------
            List<comment> cmlistSelectListItems = new List<comment>();
            foreach (comment cm in db.comments
                .Where(a=>a.entity_Id == id && a.entity.ToLower() == "server")
                .OrderByDescending(a=>a.featured== true).ThenByDescending(b=>b.timestamp))
            {
                cmlistSelectListItems.Add(cm);
            }
            ViewBag.ServerCMs = cmlistSelectListItems;
            //-----------------------------------------------------------------------

            if (server == null)
            {
                return HttpNotFound();
            }
            return View(server);
        }

        // GET: servers/Create
        public ActionResult Create()
        {
            GetAccounts();
            GetContacts();
            GetServerFarmEntityTypes();
            GetOffering();

            return View();
        }

        private void GetAccounts()
        {
            //Get Accounts
            List<SelectListItem> alistSelectListItems = new List<SelectListItem>();

            foreach (account account in db.accounts.OrderBy(a => a.Name))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = account.Name,
                    Value = account.Id.ToString(),
                    Selected = false
                };
                alistSelectListItems.Add(selectList);
            }
            ViewBag.accounts = alistSelectListItems;
            //--------------------------------------------------------------------
        }

        private void GetContacts()
        {
            //Get List of Contacts ----------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (contact contact in db.contacts.OrderBy(a => a.Name))
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

        private void GetServerFarmEntityTypes()
        {
            //Get List of Contacts ----------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName == "ServerFarmEntityType").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.ServerFarmEntityTypes = listSelectListItems;
            //--------------------------------------------------------------------
        }

        // POST: servers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DataCenter,DeployedVersion,FQDN,Role,Purpose,Offering")] serverFarms server, string hdContactsArray, int accountId)
        {
            if (ModelState.IsValid)
            {
                account account = db.accounts.Where(x => x.Id == accountId).FirstOrDefault();
                server.account = account;
                ProcessContacts(server, hdContactsArray,"Create");

                db.serverFarms.Add(server);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(server);
        }

        private void ProcessContacts(serverFarms server, string hdContactsArray, string mode)
        {
            if (!string.IsNullOrEmpty(hdContactsArray))
            {
                List<ContactLinks> contactLinks = new List<ContactLinks>();
                var items = JsonConvert.DeserializeObject<List<contactRec>>(hdContactsArray);
                foreach (var item in items)
                {
                    if (item.isOutage)
                    {
                        ContactLinks con = new ContactLinks();
                        con.server = server;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Server";
                        con.entityCategory = "Outage";
                        contactLinks.Add(con);
                    }
                    if (item.isChange)
                    {
                        ContactLinks con = new ContactLinks();
                        con.server = server;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Server";
                        con.entityCategory = "Change";
                        contactLinks.Add(con);
                    }
                    if (!string.IsNullOrEmpty(item.opt))
                    {
                        ContactLinks con = new ContactLinks();
                        con.server = server;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Server";
                        con.entityCategory = item.opt;
                        contactLinks.Add(con);
                    }
                }
                if (mode == "Create")
                {
                    server.ServerContacts = contactLinks;
                }
                if (mode == "Edit")
                {
                    db.contactlinks.AddRange(contactLinks);
                }

            }
        }

        // GET: servers/Edit/5
        public ActionResult Edit(int? id)
        {
            GetContacts();
            GetServerFarmEntityTypes();
            GetOffering();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serverFarms server = db.serverFarms.Find(id);
            if (server == null)
            {
                return HttpNotFound();
            }
            return View(server);
        }

        // POST: servers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,DataCenter,DeployedVersion,FQDN,Role,Purpose,Offering")] serverFarms server, string hdContactsArray)
        {
            if (ModelState.IsValid)
            {
                ProcessContacts(server, hdContactsArray, "Edit");
                db.Entry(server).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(server);
        }

        // GET: servers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            serverFarms server = db.serverFarms.Find(id);
            if (server == null)
            {
                return HttpNotFound();
            }
            return View(server);
        }

        // POST: servers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                db.contactlinks.RemoveRange(db.contactlinks.Where(a => a.server.Id == id));
                db.datasources.RemoveRange(db.datasources.Where(a => a.ServerFarm.Id == id));
                serverFarms server = db.serverFarms.Find(id);
                db.serverFarms.Remove(server);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
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


        // GET: accounts/Delete/5
        public ActionResult DeleteServerFarmContact(int? id)
        {

            return View();
        }
        // POST: accounts/Delete/5
        [HttpPost, ActionName("DeleteServerFarmContact")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteServerFarmContact(int srvId, int contactId)
        {
            try
            {
                db.contactlinks.RemoveRange(db.contactlinks.Where(a => a.server.Id == srvId && a.contact.Id == contactId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Edit", new { Id = srvId });
        }

        [HttpPost]
        public JsonResult GetComments(int ServerId)
        {
            if (ServerId > 0)
            {
                var comments = db.comments
                               .Where(x => x.entity_Id == ServerId && x.entity == "Server")
                               .OrderByDescending(a => a.featured == true).ThenBy(b => b.timestamp)
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
