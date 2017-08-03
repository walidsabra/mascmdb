using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class contract
    {
        public int Id { get; set; } //Contract Id
        public string Name { get; set; } //Contract Name

        public string ContractDate { get; set; }
        public string Status { get; set; } //

        public string Opportunity { get; set; }
        public string PCode { get; set; } //Projector Code

        public virtual List<server> Servers { get; set; }
        public virtual List<contact> contacts { get; set; }
        public virtual account account { get; set; }

        public string AccountName
        {
            get
            {
                CMDB db = new CMDB();
                return db.accounts.Where(x => x.Id == account.Id).FirstOrDefault().Name;
            }
        }

        public string UtlimateId
        {
            get
            {
                CMDB db = new CMDB();
                return db.accounts.Where(x => x.Id == account.Id).FirstOrDefault().UltimateId;
            }
        }
    }
}