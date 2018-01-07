using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMDB01.Models;

namespace CMDB01
{
    public class reportsBL
    {
       


        public List<account> getAccounts()
        {
            account acc = new account
            {
                Name = "Walid Account",
                Status = "ABC"
            };

            List<account> lst = new List<account>();
            lst.Add(acc);

            return lst;
        }
    }
}