﻿using System;
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
        private Models.CMDB db = new Models.CMDB();

        // GET: datasources
     
        public ActionResult Index(string SearchValue, string dc, string dv, string acc, string StartWith, string dsST, string Options)
        {

            List<datasource> lsDC = null, lsDV = null, lsACC = null, lsST = null, lsSW = null, lsSV = null;
            List<datasource> lsDSs = new List<datasource>();

            List<SelectListItem> mlist = new List<SelectListItem>();
            SelectListGroup DCg = new SelectListGroup { Name = "Data Center" };
            SelectListGroup STg = new SelectListGroup { Name = "Status" };
            SelectListGroup DVg = new SelectListGroup { Name = "Deployed Version" };
            SelectListGroup ACCg = new SelectListGroup { Name = "Account" };

            if (!string.IsNullOrEmpty(Options))
            {
                //filter the db context
            }
            else
            {
                Options = "";
            }

            string[] STs = db.datasources.Select(a => a.Status).Distinct().ToArray();
            string[] DCs = db.datasources.Select(a => a.ServerFarm.DataCenter).Distinct().ToArray();
            string[] DVs = db.datasources.Select(a => a.ServerFarm.DeployedVersion).Distinct().ToArray();
            string[] ACCs = db.datasources.Select(a => a.ServerFarm.account.Name).Distinct().ToArray();


            //Status             
            foreach (var st in STs)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key,
                    Text = key,
                    Selected = Sel,
                    Group = STg
                };
                mlist.Add(li);
            };

            //Data Center                 
            foreach (var st in DCs)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }
                SelectListItem li = new SelectListItem
                {
                    Value = key,
                    Text = key,
                    Selected = Sel,
                    Group = DCg
                };
                mlist.Add(li);
            };

            //Deployed Version
            foreach (var st in DVs)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }
                SelectListItem li = new SelectListItem
                {
                    Value = key,
                    Text = key,
                    Selected = Sel,
                    Group = DVg
                };
                mlist.Add(li);
            };

            //Account
            foreach (var st in ACCs)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }
                SelectListItem li = new SelectListItem
                {
                    Value = key,
                    Text = key,
                    Selected = Sel,
                    Group = ACCg
                };
                mlist.Add(li);
            };

            ViewBag.LinkedOptions = mlist.ToList();


            if (!string.IsNullOrEmpty(Options))
            {
                string[] ops = Options.Split(',');
                if (!string.IsNullOrEmpty(ops.First()))
                {
                    foreach (string opt in ops)
                    {
                        //Process the single Option here
                        if (DCs.Contains(opt))
                        {
                            if (!string.IsNullOrEmpty(dc))
                            {
                                dc = dc + "," + opt;
                            }
                            else
                            {
                                dc = opt;
                            }
                            continue;
                        }
                        if (DVs.Contains(opt))
                        {
                            if (!string.IsNullOrEmpty(dv))
                            {
                                dv = dv + "," + opt;
                            }
                            else
                            {
                                dv = opt;
                            }
                            continue;
                        }
                        if (STs.Contains(opt))
                        {
                            if (!string.IsNullOrEmpty(dsST))
                            {
                                dsST = dsST + "," + opt;
                            }
                            else
                            {
                                dsST = opt;
                            }
                            continue;
                        }
                        if (ACCs.Contains(opt))
                        {
                            if (!string.IsNullOrEmpty(acc))
                            {
                                acc = acc + "," + opt;
                            }
                            else
                            {
                                acc = opt;
                            }
                            continue;
                        }
                    }
                }

            }
            if (!string.IsNullOrEmpty(SearchValue))
            {
                lsSW = db.datasources.Where(x => x.Name.Contains(SearchValue)).ToList();
            }
            if (!string.IsNullOrEmpty(dc))
            {
                string[] dcLst = dc.Split(',');

                if (dcLst.Count() > 1)
                {
                    lsDC = db.datasources.Where(a => dcLst.Contains(a.ServerFarm.DataCenter)).ToList();
                }
                else
                {
                    if (dc == "null")
                    {
                        lsDC = db.datasources.Where(a => a.ServerFarm.DataCenter.Equals(null)).ToList();
                    }
                    else
                    {
                        lsDC = db.datasources.Where(a => a.ServerFarm.DataCenter.Equals(dc)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(dv))
            {
                string[] dvLst = dv.Split(',');

                if (dvLst.Count() > 1)
                {
                    lsDV = db.datasources.Where(a => dvLst.Contains(a.ServerFarm.DeployedVersion)).ToList();
                }
                else
                {
                    if (dv == "null")
                    {
                        lsDV = db.datasources.Where(a => a.ServerFarm.DeployedVersion.Equals(null)).ToList();
                    }
                    else
                    {
                        lsDV = db.datasources.Where(a => a.ServerFarm.DeployedVersion.Equals(dv)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(acc))
            {
                string[] accLst = acc.Split(',');

                if (accLst.Count() > 1)
                {
                    lsACC = db.datasources.Where(a => accLst.Contains(a.ServerFarm.account.Name)).ToList();
                }
                else
                {
                    if (acc == "null")
                    {
                        lsACC = db.datasources.Where(a => a.ServerFarm.account.Name.Equals(null)).ToList();
                    }
                    else
                    {
                        lsACC = db.datasources.Where(a => a.ServerFarm.account.Name.Equals(acc)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(dsST))
            {
                string[] stLst = dsST.Split(',');

                if (dsST.Count() > 1)
                {
                    lsST = db.datasources.Where(a => stLst.Contains(a.Status)).ToList();
                }
                else
                {
                    if (dsST == "null")
                    {
                        lsST = db.datasources.Where(a => a.Status.Equals(null)).ToList();
                    }
                    else
                    {
                        lsST = db.datasources.Where(a => a.Status.Equals(dsST)).ToList();
                    }
                }
            }
            if (!string.IsNullOrEmpty(StartWith))
            {
                lsSW = db.datasources.Where(x => x.Name.StartsWith(StartWith)).ToList();
            }
            if (string.IsNullOrEmpty(SearchValue) && string.IsNullOrEmpty(dc) && string.IsNullOrEmpty(dv) && string.IsNullOrEmpty(acc) && string.IsNullOrEmpty(StartWith) && string.IsNullOrEmpty(dsST))
            {
                //if (!string.IsNullOrEmpty(StartWith))
                //{
                //    lsDSs = db.datasources.Where(x => x.Name.StartsWith(StartWith)).AsQueryable();
                //}
                //else
                //{
                //    lsDSs = db.datasources;
                //}

                lsDSs = db.datasources.ToList();
                return View(lsDSs);
            }

            if (lsSW != null)
            {
                if (lsDSs.Count > 0)
                {
                    lsDSs = lsDSs.Intersect(lsSW).ToList();
                }
                else
                {
                    lsDSs = lsDSs.Union(lsSW).ToList();
                }
            }
            if (lsSV != null)
            {
                if (lsDSs.Count > 0)
                {
                    lsDSs = lsDSs.Intersect(lsSV).ToList();
                }
                else
                {
                    lsDSs = lsDSs.Union(lsSV).ToList();
                }
            }
            if (lsST != null)
            {
                if (lsDSs.Count > 0)
                {
                    lsDSs = lsDSs.Intersect(lsST).ToList();
                }
                else
                {
                    lsDSs = lsDSs.Union(lsST).ToList();
                }
            }
            if (lsDV != null)
            {
                if (lsDSs.Count > 0)
                {
                    lsDSs = lsDSs.Intersect(lsDV).ToList();
                }
                else
                {
                    lsDSs = lsDSs.Union(lsDV).ToList();
                }
            }
            if (lsDC != null)
            {
                if (lsDSs.Count > 0)
                {
                    lsDSs = lsDSs.Intersect(lsDC).ToList();
                }
                else
                {
                    lsDSs = lsDSs.Union(lsDC).ToList();
                }
            }
            if (lsACC != null)
            {
                if (lsDSs.Count > 0)
                {
                    lsDSs = lsDSs.Intersect(lsACC).ToList();
                }
                else
                {
                    lsDSs = lsDSs.Union(lsACC).ToList();
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

            //Get DS Comments --------------------------------------------------
            List<comment> cmlistSelectListItems = new List<comment>();
            foreach (comment cm in db.comments
                .Where(a => a.entity_Id == id && a.entity.ToLower() == "datasource")
                .OrderByDescending(a => a.featured == true).ThenByDescending(b => b.timestamp))
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
        [Authorize]
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
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Description,GUID,Monitored,Status")] datasource datasource, int serverId, string hdContactsArray)
        {
            if (!(bool)Session["EditData"])
            {
                return RedirectToAction("Index");
            }
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

        // GET: datasources/Edit/5
        [Authorize]
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
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,GUID,Monitored,Status")] datasource datasource, string hdContactsArray)
        {
            if (!(bool)Session["EditData"])
            {
                return RedirectToAction("Index");
            }
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
        [Authorize]
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
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!(bool)Session["EditData"])
            {
                return RedirectToAction("Index");
            }
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

        // GET: accounts/Delete/5
        [Authorize]
        public ActionResult DeleteDatasourceContact(int? id)
        {

            return View();
        }
        // POST: accounts/Delete/5
        [HttpPost, ActionName("DeleteDatasourceContact")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteDatasourceContact(int dsId, int contactId)
        {
            if (!(bool)Session["EditData"])
            {
                return RedirectToAction("Index");
            }
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


        #region supporting code 
        //private void GetDataSourceContacts(datasource datasource)
        //{
        //    try
        //    {
        //        ////Get Contacts --------------------------------------------------
        //        //List<SelectListItem> listSelectListItems = new List<SelectListItem>();
        //        //foreach (contact contact in datasource.contacts)
        //        //{
        //        //	SelectListItem selectList = new SelectListItem()
        //        //	{
        //        //		Text = contact.Name,
        //        //		Value = contact.Id.ToString(),
        //        //		Selected = false
        //        //	};
        //        //	listSelectListItems.Add(selectList);
        //        //}
        //        //ViewBag.contacts = listSelectListItems;
        //        ////-----------------------------------------------------------------------


        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        private void GetDatasourceEntityTypes()
        {
            //Get List of DatasourceEntityType ----------------------------------------------
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
                        Text = server.FQDN,
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
            //Get List of DatasourceStatus ----------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName == "DatasourceStatus").OrderBy(a => a.PickListValue))
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
                               .OrderByDescending(a => a.featured == true).ThenByDescending(b => b.timestamp)
                               .ToList();

                return Json(new { ok = true, data = comments, message = "ok" });
            }
            else
            {
                return null;
            }

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
        # endregion
    }
}

#region old code
//var StatusList = db.datasources.OrderBy(a => a.Status).GroupBy(a => a.Status);
//var DCList = db.datasources.OrderBy(a => a.ServerFarm.DataCenter).GroupBy(a => a.ServerFarm.DataCenter);
//var DVList = db.datasources.OrderBy(a => a.ServerFarm.DeployedVersion).GroupBy(a => a.ServerFarm.DeployedVersion);
//var ACCList = db.datasources.OrderBy(a => a.ServerFarm.account.Name).GroupBy(a => a.ServerFarm.account.Name);

//if (opt.StartsWith("dc"))
//{
//    if (!string.IsNullOrEmpty(dc))
//    {
//        dc = dc + ',' + opt.Substring(3, opt.Length - 3);
//    }
//    else
//    {
//        dc = opt.Substring(3, opt.Length - 3);
//    }


//}
//if (opt.StartsWith("dv"))
//{
//    if(!string.IsNullOrEmpty(dv))
//    {
//        dv = dv + ',' + opt.Substring(3, opt.Length - 3);
//    }
//    else
//    {
//        dv = opt.Substring(3, opt.Length - 3);
//    }
//}
//if (opt.StartsWith("acc"))
//{
//    if(!string.IsNullOrEmpty(acc))
//    {
//        acc = acc + ',' + opt.Substring(4, opt.Length - 4);
//    }
//    else
//    {
//        acc = opt.Substring(4, opt.Length - 4);
//    }
//}
//if (opt.StartsWith("dsST"))
//{
//    if(!string.IsNullOrEmpty(dsST))
//    {
//        dsST = dsST + ',' + opt.Substring(5, opt.Length - 5);
//    }
//    else
//    {
//        dsST = opt.Substring(5, opt.Length - 5);
//    }
//}
#endregion