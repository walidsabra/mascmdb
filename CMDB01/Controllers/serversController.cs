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

        private void GetSFStatusList()
        {
            //Get List of DatasourceStatus ----------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName == "ServerFarmStatus").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.ServerFarmStatus = listSelectListItems;
            //--------------------------------------------------------------------
        }

        //Get List of Offering ----------------------------------------------
        private void GetPurpose()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName == "Purpose").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.Purpose = listSelectListItems;
            //--------------------------------------------------------------------
        }
        
        // GET: servers
        public ActionResult Index(string SearchValue, string dc, string dv, string rl, string acc, string StartWith, string sfST, string Options)
        {
            List<serverFarms> lsDC = null, lsDV = null, lsACC = null, lsST = null, lsSW = null, lsSV = null, lsPRP = null;
            List<serverFarms> lsServers = new List<serverFarms>();
            if (!string.IsNullOrEmpty(Options))
            {
                string[] ops = Options.Split(',');
                if (!string.IsNullOrEmpty(ops.First()))
                {
                    foreach (string opt in ops)
                    {
                        if (opt.StartsWith("dc"))
                        {
                            if (!string.IsNullOrEmpty(dc))
                            {
                                dc = dc + ',' + opt.Substring(3, opt.Length - 3);
                            }
                            else
                            {
                                dc = opt.Substring(3, opt.Length - 3);
                            }


                        }
                        if (opt.StartsWith("dv"))
                        {
                            if (!string.IsNullOrEmpty(dv))
                            {
                                dv = dv + ',' + opt.Substring(3, opt.Length - 3);
                            }
                            else
                            {
                                dv = opt.Substring(3, opt.Length - 3);
                            }
                        }
                        if (opt.StartsWith("acc"))
                        {
                            if (!string.IsNullOrEmpty(acc))
                            {
                                acc = acc + ',' + opt.Substring(4, opt.Length - 4);
                            }
                            else
                            {
                                acc = opt.Substring(4, opt.Length - 4);
                            }
                        }
                        if (opt.StartsWith("sfST"))
                        {
                            if (!string.IsNullOrEmpty(sfST))
                            {
                                sfST = sfST + ',' + opt.Substring(5, opt.Length - 5);
                            }
                            else
                            {
                                sfST = opt.Substring(5, opt.Length - 5);
                            }
                        }
                        if (opt.StartsWith("rl"))
                        {
                            if (!string.IsNullOrEmpty(rl))
                            {
                                rl = rl + ',' + opt.Substring(3, opt.Length - 3);
                            }
                            else
                            {
                                rl = opt.Substring(3, opt.Length - 3);
                            }
                        }
                    }
                }

            }
            if (!string.IsNullOrEmpty(SearchValue))
            {
                lsSW = db.serverFarms.Where(x=>x.Name.Contains(SearchValue)).ToList();
            }
            if (!string.IsNullOrEmpty(dc))
            {
                string[] dcLst = dc.Split(',');
                if (dcLst.Count() > 1)
                {
                    lsDC = db.serverFarms.Where(a => dcLst.Contains(a.DataCenter)).ToList();
                }
                else
                {
                    if (dc == "null")
                    {
                        lsDC = db.serverFarms.Where(a => a.DataCenter.Equals(null)).ToList();
                    }
                    else
                    {
                        lsDC = db.serverFarms.Where(a => a.DataCenter.Equals(dc)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(dv))
            {
                string[] dvLst = dv.Split(',');

                if (dvLst.Count() > 1)
                {
                    lsDV = db.serverFarms.Where(a => dvLst.Contains(a.DeployedVersion)).ToList();
                }
                else
                {
                    if (dv == "null")
                    {
                        lsDV = db.serverFarms.Where(a => a.DeployedVersion.Equals(null)).ToList();
                    }
                    else
                    {
                        lsDV = db.serverFarms.Where(a => a.DeployedVersion.Equals(dv)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(sfST))
            {
                string[] stLst = sfST.Split(',');

                if (sfST.Count() > 1)
                {
                    lsST = db.serverFarms.Where(a => stLst.Contains(a.Status)).ToList();
                }
                else
                {
                    if (sfST == "null")
                    {
                        lsST = db.serverFarms.Where(a => a.Status.Equals(null)).ToList();
                    }
                    else
                    {
                        lsST = db.serverFarms.Where(a => a.Status.Equals(sfST)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(rl))
            {
                string[] prpLst = rl.Split(',');

                if (rl.Count() > 1)
                {
                    lsPRP = db.serverFarms.Where(a => prpLst.Contains(a.Purpose)).ToList();
                }
                else
                {
                    if (rl == "null")
                    {
                        lsPRP = db.serverFarms.Where(a => a.Purpose.Equals(null)).ToList();
                    }
                    else
                    {
                        lsPRP = db.serverFarms.Where(a => a.Purpose.Equals(rl)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(acc))
            {
                string[] accLst = acc.Split(',');

                if (accLst.Count() > 1)
                {
                    lsACC = db.serverFarms.Where(a => accLst.Contains(a.account.Name)).ToList();
                }
                else
                {
                    if (acc == "null")
                    {
                        lsACC = db.serverFarms.Where(a => a.account.Name.Equals(null)).ToList();
                    }
                    else
                    {
                        lsACC = db.serverFarms.Where(a => a.account.Name.Equals(acc)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(StartWith))
                {
                    lsSW = db.serverFarms.Where(x => x.Name.StartsWith(StartWith)).ToList();
                }
            if (string.IsNullOrEmpty(SearchValue) && string.IsNullOrEmpty(dc) && string.IsNullOrEmpty(dv) && string.IsNullOrEmpty(acc) && string.IsNullOrEmpty(StartWith) && string.IsNullOrEmpty(sfST) && string.IsNullOrEmpty(rl))
            {
                lsServers = db.serverFarms.ToList();
                return View(lsServers);
            }

            if (lsSW != null)
            {
                if (lsServers.Count > 0)
                {
                    lsServers = lsServers.Intersect(lsSW).ToList();
                }
                else
                {
                    lsServers = lsServers.Union(lsSW).ToList();
                }
            }
            if (lsSV != null)
            {
                if (lsServers.Count > 0)
                {
                    lsServers = lsServers.Intersect(lsSV).ToList();
                }
                else
                {
                    lsServers = lsServers.Union(lsSV).ToList();
                }
            }
            if (lsST != null)
            {
                if (lsServers.Count > 0)
                {
                    lsServers = lsServers.Intersect(lsST).ToList();
                }
                else
                {
                    lsServers = lsServers.Union(lsST).ToList();
                }
            }
            if (lsDV != null)
            {
                if (lsServers.Count > 0)
                {
                    lsServers = lsServers.Intersect(lsDV).ToList();
                }
                else
                {
                    lsServers = lsServers.Union(lsDV).ToList();
                }
            }
            if (lsDC != null)
            {
                if (lsServers.Count > 0)
                {
                    lsServers = lsServers.Intersect(lsDC).ToList();
                }
                else
                {
                    lsServers = lsServers.Union(lsDC).ToList();
                }
            }
            if (lsACC != null)
            {
                if (lsServers.Count > 0)
                {
                    lsServers = lsServers.Intersect(lsACC).ToList();
                }
                else
                {
                    lsServers = lsServers.Union(lsACC).ToList();
                }
            }
            if (lsPRP != null)
            {
                if (lsServers.Count > 0)
                {
                    lsServers = lsServers.Intersect(lsPRP).ToList();
                }
                else
                {
                    lsServers = lsServers.Union(lsPRP).ToList();
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
            GetPurpose();
            GetSFStatusList();
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
        public ActionResult Create([Bind(Include = "Id,Name,DataCenter,DeployedVersion,FQDN,Architecture,Purpose,Status")] serverFarms server, string hdContactsArray, int accountId)
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
            GetPurpose();
            GetSFStatusList();

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
        public ActionResult Edit([Bind(Include = "Id,Name,DataCenter,DeployedVersion,FQDN,Architecture,Purpose,Status")] serverFarms server, string hdContactsArray)
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
