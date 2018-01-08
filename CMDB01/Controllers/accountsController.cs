using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CMDB01.Models;
using Newtonsoft.Json;
using System.Web.UI.WebControls;

namespace CMDB01.Controllers
{
    public class accountsController : Controller
    {
        private Models.CMDB db = new Models.CMDB();

        //Get List of AccountStatus ----------------------------------------------
        private void GetAccountStatus()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName.ToLower() == "accountstatus").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.AccountStatus = listSelectListItems;
            //--------------------------------------------------------------------
        }

        //Get List of AccountContractType ----------------------------------------------
        private void GetAccountContractType()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName.ToLower() == "contracttype").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.AccountContractType = listSelectListItems;
        }
        //--------------------------------------------------------------------

        //Get List of IMS --------------------------------------------------------
        private void GetIMS()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x => x.PickListName.ToLower() == "ims").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.IMS = listSelectListItems;
            //--------------------------------------------------------------------
        }


        // GET: accounts
        [Authorize]
        public ActionResult Index(string SearchValue, string StartWith, string accST, string bl, string ims)
        {
            IQueryable<account> lstAccounts, lstName, lstUltimate, lstProjector, lstOpportunity;

            if (!string.IsNullOrEmpty(SearchValue))
            {
                lstName = db.accounts.Where(x => x.Name.Contains(SearchValue)).AsQueryable();
                lstUltimate = db.accounts.Where(x => x.UltimateId.Contains(SearchValue)).AsQueryable();
                lstProjector = db.accounts.Where(x => x.ProjectorProject.Contains(SearchValue)).AsQueryable();
                lstOpportunity = db.accounts.Where(x => x.Opportunity.Contains(SearchValue)).AsQueryable();

                lstAccounts = lstName.Concat(lstUltimate).Concat(lstProjector).Concat(lstOpportunity);
            }
            else if (!string.IsNullOrEmpty(accST))
            {
                if (accST == "null")
                {
                    lstAccounts = db.accounts.Where(x => x.Status.Equals(null)).AsQueryable();
                }
                else
                {
                    lstAccounts = db.accounts.Where(x => x.Status.Equals(accST)).AsQueryable();
                }
            }
            else if (!string.IsNullOrEmpty(bl))
            {
                if (bl=="null")
                {
                    lstAccounts = db.accounts.Where(x => x.Billable.Equals(null)).AsQueryable();
                }
                else
                {
                    lstAccounts = db.accounts.Where(x => x.Billable.Equals(bl)).AsQueryable();
                }
            }
            else if (!string.IsNullOrEmpty(ims))
            {
                if (ims =="null")
                {
                    lstAccounts = db.accounts.Where(x => x.RequestIMS.Equals(null)).AsQueryable();
                }
                else
                {
                    lstAccounts = db.accounts.Where(x => x.RequestIMS.Equals(ims)).AsQueryable();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(StartWith))
                {
                    lstAccounts = db.accounts.Where(x => x.Name.StartsWith(StartWith)).AsQueryable();
                }
                else
                {
                    lstAccounts = db.accounts.OrderBy(a=>a.Name);
                }
            }
            return View(lstAccounts.ToList());
        }

        // GET: accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);

            //Get account Comments --------------------------------------------------
            List<comment> cmlistSelectListItems = new List<comment>();
            foreach (comment cm in db.comments
                .Where(a => a.entity_Id == id && a.entity.ToLower() == "account")
                .OrderByDescending(a=>a.featured == true).ThenByDescending(b=>b.timestamp))
            {
                cmlistSelectListItems.Add(cm);
            }
            ViewBag.AccountCMs = cmlistSelectListItems;
            //-----------------------------------------------------------------------

            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: accounts/Create
        public ActionResult Create()
        {
            GetContacts();
            GetAccountEntityTypes();
            GetIMS();
            GetAccountStatus();
            GetAccountContractType();


            return View();
        }

        // POST: accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UltimateId,Status,Opportunity,ProjectorProject,RequestIMS,Billable,contracttype")] account account, string hdContactsArray)
        {
            if (ModelState.IsValid)
            {
                processContacts(account, hdContactsArray, "Create");

                db.accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            GetContacts();
            GetServers();
            GetAccountEntityTypes();
            GetIMS();
            GetAccountStatus();
            GetAccountContractType();


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UltimateId,Status,Opportunity,ProjectorProject,RequestIMS,Billable,contracttype")] account account, string hdContactsArray)
        {
            if (ModelState.IsValid)
            {
                processContacts(account, hdContactsArray, "Edit");
                
                db.Entry(account).State = EntityState.Modified;
                //List<System.Data.Entity.Infrastructure.DbEntityEntry> entries = db.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            account account = db.accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {

                db.contactlinks.RemoveRange(db.contactlinks.Where(a => a.account.Id == id));
                //foreach(server srv in db.servers.Where(a => a.account.Id == id))
                //{
                //    db.datasources.RemoveRange(db.datasources.Where(a => a.server.Id == srv.Id));
                //    db.servers.Remove(srv);
                //}

                account account = db.accounts.Find(id);
                db.accounts.Remove(account);
                db.SaveChanges();
            }
            catch (Exception)
            {
                //throw;
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
        [HttpPost]
        public JsonResult GetComments(int AccountId)
        {
            if (AccountId > 0)
            {
                var comments = db.comments
                               .Where(x => x.entity_Id == AccountId && x.entity == "Account")
                               .OrderByDescending(a=> a.featured == true).ThenByDescending(b=>b.timestamp)
                               .ToList();

                return Json(new { ok = true, data = comments, message = "ok" });
            }
            else
            {
                return null;
            }

        }


        // GET: accounts/Delete/5
        public ActionResult DeleteAccountContact(int? id)
        {

            return View();
        }
        // POST: accounts/Delete/5
        [HttpPost, ActionName("DeleteAccountContact")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAccountContact(int accId, int contactId)
        {
            try
            {
                db.contactlinks.RemoveRange(db.contactlinks.Where(a => a.account.Id == accId && a.contact.Id == contactId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Edit", new {Id = accId });
        }

        private void processContacts(account account, string hdContactsArray, string mode)
        {
            if (!string.IsNullOrEmpty(hdContactsArray))
            {
                List<ContactLinks> contactLinks = new List<ContactLinks>();
                var items = JsonConvert.DeserializeObject<List<contactRec>>(hdContactsArray);
                foreach (var item in items)
                {
                    if (item.isInform)
                    {
                        ContactLinks con = new ContactLinks();
                        con.account = account;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Account";
                        con.entityCategory = "Inform Only";
                        contactLinks.Add(con);
                    }
                    if (item.isAll)
                    {
                        ContactLinks con = new ContactLinks();
                        con.account = account;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Account";
                        con.entityCategory = "All Comms";
                        contactLinks.Add(con);
                    }
                    if (!string.IsNullOrEmpty(item.opt))
                    {
                        ContactLinks con = new ContactLinks();
                        con.account = account;
                        con.contact = db.contacts.Where(a => a.Id == item.contactId).FirstOrDefault();
                        con.entityType = "Account";
                        con.entityCategory = item.opt;
                        contactLinks.Add(con);
                    }
                }
                if (mode == "Create")
                {
                    account.AccountContacts = contactLinks;
                }
                if (mode == "Edit")
                {
                    db.contactlinks.AddRange(contactLinks);
                }

            }
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

        private void GetServers()
        {
            //Get List of Contacts ----------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (serverFarms server in db.serverFarms.OrderBy(a => a.Name))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = server.Name,
                    Value = server.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.servers = listSelectListItems;
            //--------------------------------------------------------------------
        }

        private void GetAccountEntityTypes()
        {
            //Get List of Contacts ----------------------------------------------
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (PickList pl in db.PickLists.Where(x=>x.PickListName == "AccountEntityType").OrderBy(a => a.PickListValue))
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = pl.PickListValue,
                    Value = pl.Id.ToString(),
                    Selected = false
                };
                listSelectListItems.Add(selectList);
            }
            ViewBag.AccountEntityTypes = listSelectListItems;
            //--------------------------------------------------------------------
        }

    }
    public class contactRec
    {
        public int contactId { get; set; }
        public bool isAll { get; set; }
        public bool isInform { get; set; }
        public bool isOutage { get; set; }
        public bool isMajor { get; set; }
        public bool isMinor { get; set; }
        public bool isChange { get; set; }
        public string opt { get; set; }
    }
}
