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
        private Models.CMDB01 db = new Models.CMDB01();

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
                 .Where(x => x.ServerFarm.account.Name.ToLower() == account.ToLower() && x.ServerFarm.Name.ToLower() == serverfarm.ToLower())
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

        //Get List of Accounts --------------------------------------------------------
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

        public ActionResult Links(string dc, string ds, string acc, string sf, string Options)
        {
            List<ContactLinks> lstDCs = new List<ContactLinks>();
            List<SelectListItem> mlist = new List<SelectListItem>();
            List<contact> clist = new List<contact>();
            SelectListGroup CATg = new SelectListGroup { Name = "Categories" };
            SelectListGroup ACCg = new SelectListGroup { Name = "Accounts" };
            SelectListGroup SFg = new SelectListGroup { Name = "Server Farms" };
            SelectListGroup DSg = new SelectListGroup { Name = "Datasources" };
            SelectListGroup DCg = new SelectListGroup { Name = "Data Centers" };


            if (!string.IsNullOrEmpty(Options))
            {
                //Remove the Prefixes 


                //filter the db context
            }
            else
            {
                Options = "";
            }

            #region FillTheViewBag
            string[] ACCcat = db.contactlinks.Where(a => a.entityType == "Account").Select(b => b.entityCategory).Distinct().ToArray();
            ACCcat = ACCcat.Select(a => a.Insert(0, "Account - ")).ToArray();
            string[] SFcat = db.contactlinks.Where(a => a.entityType == "Server").Select(b => b.entityCategory).Distinct().ToArray();
            SFcat = SFcat.Select(a => a.Insert(0, "Server Farm - ")).ToArray();
            string[] DScat = db.contactlinks.Where(a => a.entityType == "Datasource").Select(b => b.entityCategory).Distinct().ToArray();
            DScat = DScat.Select(a => a.Insert(0, "Datasource - ")).ToArray();

            List<string> catLST = ACCcat.Union(SFcat).Union(DScat).ToList();

            List<string> dsLST = (from cl in db.contactlinks
                                  join cc in db.datasources on cl.datasource.Id equals cc.Id
                                  select cc.Name).Distinct().ToList();

            List<string> sfLST = (from cl in db.contactlinks
                                  join cc in db.serverFarms on cl.server.Id equals cc.Id
                                  select cc.Name).Distinct().ToList();
            foreach (string str1 in dsLST)
            {
                string srvFarm = db.datasources.Where(a => a.Name == str1).Select(b => b.ServerFarm.Name).FirstOrDefault();
                if (!sfLST.Contains(srvFarm))
                {
                    sfLST.Add(srvFarm);
                }
            }
            List<string> dcLST = new List<string>();
            foreach (string str2 in sfLST)
            {
                string sfDC = db.serverFarms.Where(a => a.Name == str2).Select(b => b.DataCenter).FirstOrDefault();
                if (!string.IsNullOrEmpty(sfDC))
                {
                    dcLST.Add(sfDC);
                }
            }

            List<string> accLST = (from cl in db.contactlinks
                                   join cc in db.accounts on cl.account.Id equals cc.Id
                                   select cc.Name).Distinct().ToList();
            foreach (string str3 in sfLST)
            {
                string sfACC = db.serverFarms.Where(a => a.Name == str3).Select(b => b.account.Name).FirstOrDefault();
                if (!accLST.Contains(sfACC))
                {
                    accLST.Add(sfACC);
                }
            }


            //Categories             
            foreach (var st in catLST.Distinct().OrderBy(q => q))
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = CATg
                };
                mlist.Add(li);
            };

            //Accounts             
            foreach (var st in accLST.Distinct().OrderBy(q => q))
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = ACCg
                };
                mlist.Add(li);
            };

            //Server Farms             
            foreach (var st in sfLST.Distinct().OrderBy(q => q))
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = SFg
                };
                mlist.Add(li);
            };

            //Data Centers            
            foreach (var st in dcLST.Distinct().OrderBy(q => q))
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = DCg
                };
                mlist.Add(li);
            };

            //Datasources             
            foreach (var st in dsLST.Distinct().OrderBy(q => q))
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = DSg
                };
                mlist.Add(li);
            };

            ViewBag.conLinkedOptions = mlist.ToList();
            #endregion

            if (!string.IsNullOrEmpty(Options))
            {
                
                List<ContactLinks> lstDatasources = new List<ContactLinks>(), lstCategories = new List<ContactLinks>(), lstDataCenters = new List<ContactLinks>(), lstServerFarms = new List<ContactLinks>(), lstAccounts = new List<ContactLinks>();
                //Split Options
                string[] ops = Options.Split(',');
                foreach (string op in ops)
                {
                    //Datasources List
                    List<ContactLinks> linkDS = db.contactlinks.Where(x => x.datasource.Name == op && x.entityType.ToLower()== "datasource").ToList();
                    if (lstDatasources.Count() > 0)
                    {
                        lstDatasources = lstDatasources.Concat(linkDS).ToList();
                    }
                    else
                    {
                        lstDatasources = linkDS;
                    }
                    lstDatasources.Distinct();

                    //ServerFarm List
                    int opId = db.serverFarms.Where(a => a.Name == op).Select(b => b.Id).FirstOrDefault();
                    List<ContactLinks> linkSF = db.contactlinks.Where(x => x.server.Name == op && x.entityType.ToLower() == "serverfarm").ToList();
                    List<ContactLinks> linkSFds = db.contactlinks.Where(x => x.server.Name == op && x.entityType.ToLower() == "datasource").ToList();
                    if (lstServerFarms.Count() > 0)
                    {
                        lstServerFarms = lstServerFarms.Concat(linkSF).ToList();
                    }
                    else
                    {
                        lstServerFarms = linkSF;
                    }
                    lstServerFarms.Distinct();

                    //Account List
                    List<ContactLinks> linkACC = db.contactlinks.Where(x => x.account.Name == op && x.entityType.ToLower() == "account").ToList();
                    if (lstAccounts.Count() > 0)
                    {
                        lstAccounts = lstAccounts.Concat(linkACC).ToList();
                    }
                    else
                    {
                        lstAccounts = linkACC;
                    }
                    lstAccounts.Distinct();

                    //Data Centers List
                    List<ContactLinks> linkDC = db.contactlinks.Where(x => x.server.DataCenter == op && x.entityType.ToLower() == "server").ToList();
                    if (lstDataCenters.Count() > 0)
                    {
                        lstDataCenters = lstDataCenters.Concat(linkDC).ToList();
                    }
                    else
                    {
                        lstDataCenters = linkDC;
                    }
                    lstDataCenters.Distinct();

                    //Categories List
                    if (op.StartsWith("Account - "))
                    {
                        string op1 = op.Substring(10,op.Length - 10);
                        List<ContactLinks> link = db.contactlinks.Where(x => x.entityCategory == op1 && x.entityType.ToLower() == "account").ToList();
                        if (lstCategories.Count() > 0)
                        {
                            lstCategories = lstCategories.Concat(link).ToList();
                        }
                        else
                        {
                            lstCategories = link;
                        }
                    }
                    else if (op.StartsWith("Datasource - "))
                    {
                        string op2 = op.Substring(13, op.Length - 13);
                        List<ContactLinks> link = db.contactlinks.Where(x => x.entityCategory == op2 && x.entityType.ToLower() == "datasource").ToList();
                        if (lstCategories.Count() > 0)
                        {
                            lstCategories = lstCategories.Concat(link).ToList();
                        }
                        else
                        {
                            lstCategories = link;
                        }
                    }
                    else if (op.StartsWith("Server Farm - "))
                    {
                        string op3 = op.Substring(14, op.Length - 14);
                        List<ContactLinks> link = db.contactlinks.Where(x => x.entityCategory == op3 && x.entityType.ToLower() == "server").ToList();
                        if (lstCategories.Count() > 0)
                        {
                            lstCategories = lstCategories.Concat(link).ToList();
                        }
                        else
                        {
                            lstCategories = link;
                        }
                    }

                    lstCategories.Distinct();
                }

                IEnumerable<ContactLinks>[] lists = new[] { lstAccounts, lstDataCenters, lstDatasources, lstServerFarms, lstCategories};

                foreach (List<ContactLinks> lst in lists)
                {
                    if (lst.Count > 0)
                    {
                        if (lstDCs.Count> 0)
                        {
                            lst.Intersect(lstDCs); 
                        }
                        else
                        {
                            lstDCs = lst;
                        }
                    }
                }

                //lstDCs = lstCategories.Union(lstAccounts).Union(lstDataCenters).Union(lstDatasources).Union(lstServerFarms).Distinct().ToList();


            }
            else
            {
                lstDCs = db.contactlinks.ToList();
            }

            List<string> emails = new List<string>();
            if (lstDCs.Count() > 0)
            {
                foreach (var cn in lstDCs)
                {
                    contact li = new contact
                    {
                        Id = cn.contact.Id, 
                        Name = cn.contact.Name,
                        email = cn.contact.email
                    };
                    if(!clist.Any(a=>a.Id == li.Id))
                    {
                        clist.Add(li);
                    }
                    emails.Add(cn.contact.email);
                    
                }
                ViewBag.cList = clist.Distinct().ToList();
                ViewBag.emails = string.Join("; ", emails.Distinct().ToList());
            }


            return View(lstDCs);
        }


        // GET: contacts
        public ActionResult Index(string SearchValue, string StartWith, string datacenterLst, string accountLst, string serverfarmLst, string datasourceLst, string dc, string ds, string acc, string sf, string Options)
        {
            IQueryable<contact> lscontacts;
            List<contact> lstDCs, lstAccs, lstSFs, lstDSs;

            GetAccounts();
            GetDataCenters(accountLst);
            GetServerFarms(accountLst, datacenterLst);
            GetDatasources(serverfarmLst, accountLst);


            //--New Code Start--
            List<SelectListItem> mlist = new List<SelectListItem>();
            SelectListGroup CATg = new SelectListGroup { Name = "Categories" };
            SelectListGroup ACCg = new SelectListGroup { Name = "Accounts" };
            SelectListGroup SFg = new SelectListGroup { Name = "Server Farms" };
            SelectListGroup DSg = new SelectListGroup { Name = "Datasources" };
            SelectListGroup DCg = new SelectListGroup { Name = "Data Centers" };

            string[] ACCcat = db.contactlinks.Where(a => a.entityType == "Account").Select(b => b.entityCategory).Distinct().ToArray();
            ACCcat = ACCcat.Select(a => a.Insert(0, "Account - ")).ToArray();
            string[] SFcat = db.contactlinks.Where(a => a.entityType == "ServerFarm").Select(b => b.entityCategory).Distinct().ToArray();
            SFcat = SFcat.Select(a => a.Insert(0, "Server Farm - ")).ToArray();
            string[] DScat = db.contactlinks.Where(a => a.entityType == "Datasource").Select(b => b.entityCategory).Distinct().ToArray();
            DScat = DScat.Select(a => a.Insert(0, "Datasource - ")).ToArray();

            string[] catLST = ACCcat.Union(SFcat).Union(DScat).ToArray();

            string[] accLST = (from cl in db.contactlinks
                               join cc in db.accounts on cl.account.Id equals cc.Id
                               select cc.Name).Distinct().ToArray();

            string[] sfLST = (from cl in db.contactlinks
                              join cc in db.serverFarms on cl.server.Id equals cc.Id
                              select cc.Name).Distinct().ToArray();

            string[] dcLST = (from cl in db.contactlinks
                              join cc in db.serverFarms on cl.server.Id equals cc.Id
                              select cc.DataCenter).Distinct().ToArray();

            string[] dsLST = (from cl in db.contactlinks
                              join cc in db.datasources on cl.datasource.Id equals cc.Id
                              select cc.Name).Distinct().ToArray();

            if (!string.IsNullOrEmpty(Options))
            {
                //filter the db context
            }
            else
            {
                Options = "";
            }
            //Categories             
            foreach (var st in catLST)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = CATg
                };
                mlist.Add(li);
            };

            //Accounts             
            foreach (var st in accLST)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = ACCg
                };
                mlist.Add(li);
            };

            //Server Farms             
            foreach (var st in sfLST)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = SFg
                };
                mlist.Add(li);
            };

            //Data Centers            
            foreach (var st in dcLST)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = DCg
                };
                mlist.Add(li);
            };

            //Datasources             
            foreach (var st in dsLST)
            {
                bool Sel = false;
                string key = "null";
                if (!string.IsNullOrEmpty(st)) { key = st; }
                if (Options.Contains(key)) { Sel = true; }

                SelectListItem li = new SelectListItem
                {
                    Value = key, //change value to Id later 
                    Text = key,
                    Selected = Sel,
                    Group = DSg
                };
                mlist.Add(li);
            };

            ViewBag.conLinkedOptions = mlist.ToList();

            //dc
            if (!string.IsNullOrEmpty(Options))
            {
                //first 
                lstDCs = new List<contact>();
                IQueryable<ContactLinks> lstlnks1 = db.contactlinks.Where(x => Options.Contains(x.datasource.Name) && x.entityType.ToLower() == "datasource").AsQueryable();
                IQueryable<ContactLinks> lstlnks2 = db.contactlinks.Where(x => Options.Contains(x.server.Name) && x.entityType.ToLower() == "server farm").AsQueryable();
                IQueryable<ContactLinks> lstlnks3 = db.contactlinks.Where(x => Options.Contains(x.server.DataCenter) && x.entityType.ToLower() == "server farm").AsQueryable();
                IQueryable<ContactLinks> lstlnks4 = db.contactlinks.Where(x => Options.Contains(x.account.Name) && x.entityType.ToLower() == "account").AsQueryable();
                IQueryable<ContactLinks> lstlnks5 = db.contactlinks.Where(x => Options.Contains(x.entityCategory)).AsQueryable();

                IQueryable<ContactLinks> lstlnks = lstlnks1.Union(lstlnks2).Union(lstlnks3).Union(lstlnks4).Union(lstlnks5).Distinct().AsQueryable();
                foreach (var cn in lstlnks)
                {
                    lstDCs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
                }
            }
            else
            {
                lstDCs = db.contacts.ToList();
            }

            //--New Code End --



            ////dc
            //if (!string.IsNullOrEmpty(datacenterLst))
            //{               
            //    lstDCs = new List<contact>();
            //    IQueryable<ContactLinks> lstlnks1 = db.contactlinks.Where(x => x.server.DataCenter.Contains(datacenterLst)).AsQueryable();

            //    foreach (var cn in lstlnks1)
            //    {
            //        lstDCs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
            //    }
            //}
            //else
            //{
            //    lstDCs = db.contacts.ToList();
            //}

            ////acc
            //if (!string.IsNullOrEmpty(accountLst))
            //{
            //    lstAccs = new List<contact>();
            //    IQueryable<ContactLinks> lstlnks2 = db.contactlinks.Where(x => x.account.Name.Contains(accountLst)).AsQueryable();

            //    foreach (var cn in lstlnks2)
            //    {
            //        lstAccs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
            //    }
            //}
            //else
            //{
            //    lstAccs = db.contacts.ToList();
            //}

            ////sf
            //if (!string.IsNullOrEmpty(serverfarmLst))
            //{
            //    lstSFs = new List<contact>();
            //    IQueryable<ContactLinks> lstlnks3 = db.contactlinks.Where(x => x.server.Name.Contains(serverfarmLst)).AsQueryable();

            //    foreach (var cn in lstlnks3)
            //    {
            //        lstSFs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
            //    }
            //}
            //else
            //{
            //    lstSFs = db.contacts.ToList();
            //}

            ////ds
            //if (!string.IsNullOrEmpty(datasourceLst))
            //{
            //    lstDSs = new List<contact>();
            //    IQueryable<ContactLinks> lstlnks4 = db.contactlinks.Where(x => x.datasource.Name.Contains(datasourceLst)).AsQueryable();

            //    foreach (var cn in lstlnks4)
            //    {
            //        lstDSs.Add(db.contacts.Where(a => a.Id == cn.contact.Id).FirstOrDefault());
            //    }
            //}
            //else
            //{
            //    lstDSs = db.contacts.ToList();
            //}

            //lscontacts = lstDCs.Intersect(lstAccs).Intersect(lstSFs).Intersect(lstDSs).AsQueryable();

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


            return View(lstDCs.Distinct().ToList());
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
