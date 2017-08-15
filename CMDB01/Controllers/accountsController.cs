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
    public class accountsController : Controller
    {
        private CMDB db = new CMDB();

        // GET: accounts
        public ActionResult Index(string SearchValue, string StartWith)
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
            foreach (comment cm in db.comments.Where(a => a.entity_Id == id && a.entity == "account"))
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

            return View();
        }

        // POST: accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UltimateId,ContractDate,Status,Opportunity,ProjectorProject,RequestIMS,SuccessAdminLevel,LinktoC4S,Billable")] account account, string hdContactsArray)
        {
            if (ModelState.IsValid)
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
                    }
                    account.AccountContacts = contactLinks; 
                }

                db.accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: accounts/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UltimateId,ContractDate,Status,Opportunity,ProjectorProject,RequestIMS,SuccessAdminLevel,LinktoC4S,Billable")] account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
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
            db.contactlinks.RemoveRange(db.contactlinks.Where(a => a.account.Id == id));
            account account = db.accounts.Find(id);
            db.accounts.Remove(account);
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
        public JsonResult GetComments(int AccountId)
        {
            if (AccountId > 0)
            {
                var comments = db.comments
                               .Where(x => x.entity_Id == AccountId && x.entity == "Account")
                               .ToList();

                return Json(new { ok = true, data = comments, message = "ok" });
            }
            else
            {
                return null;
            }

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
    }
}
