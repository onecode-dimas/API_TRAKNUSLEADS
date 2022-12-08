using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPITraknus.Models
{
    public abstract class account
    {
        public account()
        {
            Leadses = new HashSet<Leads>();
        }

        public Guid accountid { get; set; }
        public string name { get; set; }

        public virtual ICollection<Leads> Leadses { get; set; }
    }
}