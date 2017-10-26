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
    public class contactsController : Controller
    {
        private CMDB db = new CMDB();

        //Get List of ServerFarms --------------------------------------------------------
        private void GetServerFarms(string account, string datacenter)
        {
            List<serverFarms> lstFarms = new List<serverFarms>();
            if (account == "All Accounts")
            {
                account = null;
            }
            if (datacenter == "All Data Centers")
            {
                datacenter = null;
            }

            if (string.IsNullOrEmpty(account) && string.IsNullOrEmpty(datacenter))
            {
                lstFarms = db.serverFarms
                    .OrderBy(a => a.Name).ToList();
            }
            else if (!string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(datacenter))
            {
                lstFarms = db.serverFarms
                    .Where(x => x.DataCenter.ToLower() == datacenter.ToLower() && x.account.Name.ToLower() == account.ToLower())
                    .OrderBy(a => a.Name).ToList();
            }
            else if (!string.IsNullOrEmpty(account) && string.IsNullOrEmpty(datacenter))
            {
                lstFarms = db.serverFarms
                    .Where(x => x.account.Name.ToLower() == account.ToLower())
                    .OrderBy(a => a.Name).ToList();
            }
            else if (string.IsNullOrEmpty(account) && !string.IsNullOrEmpty(datacenter))
            {
                lstFarms = db.serverFarms
                    .Where(x => x.DataCenter.ToLower() == datacenter.ToLower())
                    .OrderBy(a => a.Name).ToList();
            }
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (serverFarms pl in lstFarms)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.Name,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.ServerFarms = listSelectListItems;
            //--------------------------------------------------------------------
        }


        //Get List of Datasources --------------------------------------------------------
        private void GetDatasources(string serverfarm, string account)
        {
            List<datasource> lstDatasources = new List<datasource>();
            if (serverfarm == "All Server Frams")
            {
                serverfarm = null;
            }
            if (account == "All Accounts")
            {
                account = null;
            }

            if (string.IsNullOrEmpty(serverfarm) && string.IsNullOrEmpty(account))
            {
                lstDatasources = db.datasources
                    .OrderBy(a => a.Name).ToList();
            }
            else if (!string.IsNullOrEmpty(serverfarm) && !string.IsNullOrEmpty(account))
            {
                lstDatasources = db.datasources
                 .Where(x => x.ServerFarm.account.Name.ToLower()== account.ToLower() && x.ServerFarm.Name.ToLower() == serverfarm.ToLower())
                 .OrderBy(a => a.Name).ToList();
            }
            else if (string.IsNullOrEmpty(serverfarm) && !string.IsNullOrEmpty(account))
            {
                lstDatasources = db.datasources
                 .Where(x => x.ServerFarm.account.Name.ToLower() == account.ToLower())
                 .OrderBy(a => a.Name).ToList();
            }
            else if (!string.IsNullOrEmpty(serverfarm) && string.IsNullOrEmpty(account))
            {
                lstDatasources = db.datasources
                 .Where(x => x.ServerFarm.Name.ToLower() == serverfarm.ToLower())
                 .OrderBy(a => a.Name).ToList();
            }

            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (datasource pl in lstDatasources)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.Name,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.Datasources = listSelectListItems;
            //--------------------------------------------------------------------
        }


        //Get List of DataCenters --------------------------------------------------------
        private void GetDataCenters(string account)
        {
            List<serverFarms> lstDataCenters = new List<serverFarms>();
            if (account == "All Data Centers")
            {
                account = null;
            }


            if (string.IsNullOrEmpty(account))
            {
                lstDataCenters = db.serverFarms
                    .OrderBy(a => a.Name).ToList();
            }
            else
            {
                lstDataCenters = db.serverFarms
                    .Where(x => x.account.Name.ToLower() == account.ToLower())
                    .OrderBy(a => a.Name).ToList();
            }
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (serverFarms pl in lstDataCenters)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.DataCenter,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.DataCenters = listSelectListItems;
            //--------------------------------------------------------------------
        }

        //Get List of DataCenters --------------------------------------------------------
        private void GetAccounts()
        {
            List<account> lstAccounts = new List<account>();

                lstAccounts = db.accounts
                    .OrderBy(a => a.Name).ToList();

            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (account pl in lstAccounts)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.Name,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.Accounts = listSelectListItems;
            //--------------------------------------------------------------------
        }


        // GET: contacts
        public ActionResult Index(string SearchValue, string StartWith, string datacenterLst, string accountLst, string serverfarmLst, string datasourceLst)
        {
            IQueryable<contact> lscontacts;
            List<contact> lstDCs, lstAccs, lstSFs, lstDSs;
      
            GetAccounts();
            GetDataCenters(accountLst);
            GetServerFarms(accountLst, datacenterLst);
            GetDatasources(serverfarmLst, accountLst);
            

            //dc
            if (!string.IsNullOrEmpty(datacenterLst))
            {               
                lstDCs = new List<contact>();
                IQueryable<ContactLinks> lstlnks1 = db.contactlinks.Where(x => x.server.DataCenter.Contains(datacenterLst)).AsQueryable();

                foreach (var cn in lstlnks1)
                {
                    lstDCs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
                }
            }
            else
            {
                lstDCs = db.contacts.ToList();
            }

            //acc
            if (!string.IsNullOrEmpty(accountLst))
            {
                lstAccs = new List<contact>();
                IQueryable<ContactLinks> lstlnks2 = db.contactlinks.Where(x => x.account.Name.Contains(accountLst)).AsQueryable();

                foreach (var cn in lstlnks2)
                {
                    lstAccs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
                }
            }
            else
            {
                lstAccs = db.contacts.ToList();
            }

            //sf
            if (!string.IsNullOrEmpty(serverfarmLst))
            {
                lstSFs = new List<contact>();
                IQueryable<ContactLinks> lstlnks3 = db.contactlinks.Where(x => x.server.Name.Contains(serverfarmLst)).AsQueryable();

                foreach (var cn in lstlnks3)
                {
                    lstSFs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
                }
            }
            else
            {
                lstSFs = db.contacts.ToList();
            }

            //ds
            if (!string.IsNullOrEmpty(datasourceLst))
            {
                lstDSs = new List<contact>();
                IQueryable<ContactLinks> lstlnks4 = db.contactlinks.Where(x => x.datasource.Name.Contains(datasourceLst)).AsQueryable();

                foreach (var cn in lstlnks4)
                {
                    lstDSs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
                }
            }
            else
            {
                lstDSs = db.contacts.ToList();
            }

            lscontacts = lstDCs.Intersect(lstAccs).Intersect(lstSFs).Intersect(lstDSs).AsQueryable();

            if (!string.IsNullOrEmpty(SearchValue))
            {
                IQueryable<contact> lsName, lsCompany, lsEmail, lsPhone;
                lsName = db.contacts.Where(x => x.Name.Contains(SearchValue)).AsQueryable();
                lsCompany = db.contacts.Where(x => x.company.Contains(SearchValue)).AsQueryable();
                lsEmail = db.contacts.Where(x => x.email.Contains(SearchValue)).AsQueryable();
                lsPhone = db.contacts.Where(x => x.phone.Contains(SearchValue)).AsQueryable();

                lscontacts = lsName.Union(lsCompany).Union(lsEmail).Union(lsPhone);
            }
            if (!string.IsNullOrEmpty(StartWith))
            {
                lscontacts = db.contacts.Where(x => x.Name.StartsWith(StartWith)).AsQueryable();
            }
            

            return View(lscontacts.ToList());
        }

        // GET: contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,email,phone,company")] contact contact)
        {
            if (ModelState.IsValid)
            {
                db.contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,email,phone,company")] contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            contact contact = db.contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            contact contact = db.contacts.Find(id);
            db.contacts.Remove(contact);
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
