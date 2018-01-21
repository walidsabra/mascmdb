using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using CMDB01.Models;
using Microsoft.Reporting.WebForms;

namespace CMDB01.Controllers
{
    public class SystemReportsController : Controller
    {
        private CMDB db = new CMDB();

        // GET: SystemReports
        public ActionResult Index()
        {
            return View(db.SystemReports.ToList());
        }

        // GET: SystemReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemReport systemReport = db.SystemReports.Find(id);
            if (systemReport == null)
            {
                return HttpNotFound();
            }

            //reportsBL rBL = new reportsBL(db);

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            //reportViewer.Width = Unit.Percentage(100);
            //reportViewer.Height = Unit.Percentage(100);

            reportViewer.ShowPrintButton = true;
            reportViewer.ShowExportControls = true;
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + systemReport.ReportPath;

            switch (systemReport.ReportName)
            {
                case "Accounts Report":
                    //List<account> lstAcc = rBL.getAccounts();
                    //reportViewer.LocalReport.DataSources.Add(new ReportDataSource(systemReport.DS1Name, lstAcc));
                    break;
            }


            ViewBag.ReportViewer = reportViewer;

            return View(systemReport);
        }

        // GET: SystemReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SystemReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReportName,ReportPath,ReportCategory,DS1Name,DS2Name,DS3Name,DS4Name,DS5Name")] SystemReport systemReport)
        {
            if (ModelState.IsValid)
            {
                db.SystemReports.Add(systemReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemReport);
        }

        // GET: SystemReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemReport systemReport = db.SystemReports.Find(id);
            if (systemReport == null)
            {
                return HttpNotFound();
            }
            return View(systemReport);
        }

        // POST: SystemReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReportName,ReportPath,ReportCategory,DS1Name,DS2Name,DS3Name,DS4Name,DS5Name")] SystemReport systemReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systemReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemReport);
        }

        // GET: SystemReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemReport systemReport = db.SystemReports.Find(id);
            if (systemReport == null)
            {
                return HttpNotFound();
            }
            return View(systemReport);
        }

        // POST: SystemReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SystemReport systemReport = db.SystemReports.Find(id);
            db.SystemReports.Remove(systemReport);
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
