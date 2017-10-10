using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMDB01.Models
{
    public class PickList
    {
        public int Id { get; set; } //Id
        public string PickListName { get; set; } //PickListName i.e. EntityType
        public string PickListValue { get; set; } //Value i.e. ServerFarmAdmin, DatasourceAdmin
    }

}

//public int Id { get; set; } //Service Id
//public string Name { get; set; } //Service Name



////All Comms
////iNFORM ONLY

////Configuration
////Outage
////Datasource
////Server Farm