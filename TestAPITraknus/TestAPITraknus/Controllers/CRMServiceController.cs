using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Crm.Sdk;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using TestAPITraknus.Models;

namespace TestAPITraknus.Controllers
{
    public class CRMServiceController
    {
        public static IExecutionContext context1;
        public static IOrganizationService CreatingCRMService(string _userName, string _passWord)
        {
            String _stringpathandquery = HttpContext.Current.Request.Url.PathAndQuery;
            string CRM_URL = HttpContext.Current.Request.Url.AbsoluteUri.Replace(_stringpathandquery, "/"); //10.0.10.56 / TNDEVCRM
            //var urlname = context1.OrganizationName;
            //CRM_URL += IExecutionContext.OrganizationName; // "TraktorNusantara";
            string serviceuri = ConfigurationManager.AppSettings["ServiceUri"].ToString();
            //string serviceuri = CRM_URL + ConfigurationManager.AppSettings["ServiceUriServer"].ToString();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IServiceManagement<IOrganizationService> orgServiceManagement = ServiceConfigurationFactory.CreateManagement<IOrganizationService>(new Uri(serviceuri));

            AuthenticationCredentials authCredentials = new AuthenticationCredentials();
            authCredentials.ClientCredentials.UserName.UserName = _userName;
            authCredentials.ClientCredentials.UserName.Password = _passWord;
            AuthenticationCredentials tokencredential = orgServiceManagement.Authenticate(authCredentials);


            OrganizationServiceProxy organizationProxy = new OrganizationServiceProxy(orgServiceManagement, authCredentials.ClientCredentials);
            organizationProxy.EnableProxyTypes();
            IOrganizationService service = (IOrganizationService)organizationProxy;
            return service;


        }
    }
}