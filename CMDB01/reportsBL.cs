using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMDB01.Models;

namespace CMDB01
{
    public class reportsBL
    {
        public CMDB context;

        public reportsBL (CMDB _context)
        {
            context = _context;
        }


        public List<account> getAccounts()
        {
            List<account> lst = context.accounts.ToList();
            return lst;
        }

        public List<serverFarms> getServers()
        {
            List<serverFarms> lst = context.serverFarms.ToList();
            return lst;
            
        }
        
    }
}