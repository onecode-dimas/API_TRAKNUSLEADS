using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPITraknus.Models
{
    public class LeadsId
    {
        public Guid agt_leadsid { get; set; }
        public string agt_name { get; set; }

        public EntityReference agt_customer { get; set; }
        public string agt_customerName { get; set; }
        public string agt_subject { get; set; }
        public OptionSetValue agt_statusleads { get; set; }
        public string statusleads { get; set; }
    }
}