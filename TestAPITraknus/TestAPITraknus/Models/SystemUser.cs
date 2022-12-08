using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAPITraknus.Models
{
    public class SystemUser
    {
        public Guid systemuserid { get; set; }
        public string fullname { get; set; }
        public string positionidname { get; set; }
        public string divisionidname { get; set; }
    }


    public class positionRef
    {
        public string positionid { get; set; }
        public string name { get; set; }
    }

    public class divisionRef
    {
        public string divisionid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }


    public class UpdateSystemUser
    {
        //public Guid systemuserid { get; set; }
        public string positionidname { get; set; }
        public string divisionidname { get; set; }
    }
}