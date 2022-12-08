using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPITraknus.Models
{
    public class Leads
    {
        public Guid agt_leadsid { get; set; }
        public string agt_name { get; set; }

        public EntityReference agt_customer { get; set; }
        public string agt_customerName { get; set; }
        public string agt_subject { get; set; }
        public OptionSetValue agt_statusleads { get; set; }
        public string statusleads { get; set; }
    }


    public class LeadsOrigin
    {
        public string agt_customer { get; set; }
        public string agt_branch1 { get; set; }

        public string agt_typeleads { get; set; }
        public string agt_subject { get; set; }
        public string agt_description { get; set; }

        public int agt_urgency { get; set; }

        public int agt_channel { get; set; }

        public int agt_statusleads { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        //public virtual account Account { get; set; }
    }


    public class LeadsCreate
    {
        public EntityReference agt_customer { get; set; }
        public Guid agt_branch1 { get; set; }

        public int agt_typeleads { get; set; }
        public string agt_subject { get; set; }
        public string agt_description { get; set; }

        public int agt_urgency { get; set; }

        public int agt_channel { get; set; }

        //public OptionSetValue agt_statusleads { get; set; }

        //public string CreatedBy { get; set; }

        //public DateTime CreatedOn { get; set; }
        //public virtual account Account { get; set; }
    }



    public class LeadsUpdate
    {
        //public Guid agt_leadsid { get; set; }
       // public Guid agt_customer { get; set; }
       // public Guid agt_branch1 { get; set; }

        //public int agt_typeleads { get; set; }
        public string agt_subject { get; set; }
        //public string agt_description { get; set; }

        //public int agt_urgency { get; set; }

        //public int agt_channel { get; set; }

        //public OptionSetValue agt_statusleads { get; set; }

        //public string CreatedBy { get; set; }

        //public DateTime CreatedOn { get; set; }
        //public virtual account Account { get; set; }
    }


}