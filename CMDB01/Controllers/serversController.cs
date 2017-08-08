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
    public class serversController : Controller
    {
        private CMDB db = new CMDB();


        // GET: servers
        public ActionResult Index(string SearchValue, string dc, string dv, string rl, string acc, string StartWith)
        {
            IQueryable<server> lsServers;

            if (!string.IsNullOrEmpty(SearchValue))
            {
                lsServers = db.servers.Where(x=>x.Name.Contains(SearchValue));
            }
            else if (!string.IsNullOrEmpty(dc))
            {
                if(dc == "null")
                {
                    lsServers = db.servers.Where(a => a.DataCenter.Equals(null));
                }
                else
                {
                    lsServers = db.servers.Where(a => a.DataCenter.Equals(dc));
                }
                
            }
            else if (!string.IsNullOrEmpty(dv))
            {
                if (dv == "null")
                {
                    lsServers = db.servers.Where(a => a.DeployedVersion.Equals(null));
                }
                else
                {
                    lsServers = db.servers.Where(a => a.DeployedVersion.Equals(dv));
                }
            }
            else if (!string.IsNullOrEmpty(rl))
            {
                if (rl == "null")
                {
                    lsServers = db.servers.Where(a => a.Role.Equals(null));
                }
                else
                {
                    lsServers = db.servers.Where(a => a.Role.Equals(rl));
                }
            }
            else if (!string.IsNullOrEmpty(acc))
            {
                if (acc == "null")
                {
                    lsServers = db.servers.Where(a => a.account.Name.Equals(null));
                }
                else
                {
                    lsServers = db.servers.Where(a => a.account.Name.Equals(acc));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(StartWith))
                {
                    lsServers = db.servers.Where(x => x.Name.StartsWith(StartWith)).AsQueryable();
                }
                else
                {
                    lsServers = db.servers;
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
            server server = db.servers.Find(id);

            //Get Server Contacts --------------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();
            foreach (contact contact in server.contacts)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = contact.Name,
                    Value = contact.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.ServerContacts = listSelectListItems;
            //-----------------------------------------------------------------------

            //Get Server Datasources --------------------------------------------------
            List<SelectListItem> dslistSelectListItems = new List<SelectListItem>();
            foreach (datasource ds in server.datasources)
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


            if (server == null)
            {
                return HttpNotFound();
            }
            return View(server);
        }

        // GET: servers/Create
        public ActionResult Create()
        {
            //Get Accounts
            List<SelectListItem> alistSelectListItems = new List<SelectListItem>();

            foreach (account account in db.accounts)
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

            return View();
        }

        // POST: servers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,DataCenter,DeployedVersion,FQDN,Role")] server server, List<int> contactId, int accountId)
        {
            if (ModelState.IsValid)
            {

                account account = db.accounts.Where(x => x.Id == accountId).FirstOrDefault();
                server.account = account;

                if (contactId != null) { 
                    List<contact> contacts = new List<contact>();
                    foreach(int x in contactId)
                    {
                        contacts.Add(db.contacts.Where(ex => ex.Id == x).FirstOrDefault());
                    }


                    server.contacts = contacts;
                }
                db.servers.Add(server);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(server);
        }

        // GET: servers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            server server = db.servers.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Name,DataCenter,DeployedVersion,FQDN,Role")] server server)
        {
            if (ModelState.IsValid)
            {
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
            server server = db.servers.Find(id);
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
            server server = db.servers.Find(id);
            db.servers.Remove(server);
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

        [HttpPost]
        public JsonResult GetComments(int ServerId)
        {
            if (ServerId > 0)
            {
                var comments = db.comments
                               .Where(x => x.entity_Id == ServerId && x.entity == "Server")
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
