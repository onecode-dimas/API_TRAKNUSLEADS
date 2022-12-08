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
    public class LeadsController : ApiController
    {
        [HttpGet]
        [Route("api/leads/getallleads")]  //kalo ga mau pakek route di comment aja
        public List<Leads> Get()
        {
            try
            {
                List<Leads> lst = new List<Leads>();

                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login

                QueryExpression queryLeads = new QueryExpression("agt_leads");
                queryLeads.ColumnSet.AllColumns = true;
                EntityCollection leadsColl = service.RetrieveMultiple(queryLeads);


                leadsColl.Entities.Count();
                //Entity systemuser = service.Retrieve("systemuser", userid, new ColumnSet(true));
                #region statusleads
                const string statusopen = "Open";
                const string statusacknowledge = "Acknowledge";
                const string statusassignbc = "Assign to BC";
                const string statusonprogress = "On Progress";
                const string statusresolve = "Resolve";
                const string statusclose = "Closed";
                const string statusassignpdh = "Assign to PDH";
                const string statusassignpss = "Assign to PSS";
                const string statusassignsdh = "Assign to SDH";
                const string statusassignofficer = "Assign to Officer";
                const string statuskeyaccount = "Key Account";
                const string statusassignboh = "Assign To BOH";
                #endregion
                foreach (Entity lds in leadsColl.Entities)
                {
                    Leads obj = new Leads();


                    //if(lds.Attributes["agt_name"].ToString() != null)
                    //{
                    var getCustomer = lds.GetAttributeValue<EntityReference>("agt_customer");

                    obj.agt_leadsid = lds.Id;
                    obj.agt_name = lds.Attributes["agt_name"].ToString();
                    //obj.agt_customerName = getCustomer.Name;
                    obj.agt_subject = lds.Attributes["agt_subject"].ToString();
                    obj.agt_statusleads = lds.GetAttributeValue<OptionSetValue>("agt_statusleads");
                    #region status leads
                    if (obj.agt_statusleads == null) { obj.statusleads = null; }
                    else if (obj.agt_statusleads.Value == 1) { obj.statusleads = statusopen; }
                    else if (obj.agt_statusleads.Value == 2) { obj.statusleads = statusacknowledge; }
                    else if (obj.agt_statusleads.Value == 3) { obj.statusleads = statusassignbc; }
                    else if (obj.agt_statusleads.Value == 4) { obj.statusleads = statusonprogress; }
                    else if (obj.agt_statusleads.Value == 5) { obj.statusleads = statusresolve; }
                    else if (obj.agt_statusleads.Value == 6) { obj.statusleads = statusclose; }
                    else if (obj.agt_statusleads.Value == 7) { obj.statusleads = statusassignpdh; }
                    else if (obj.agt_statusleads.Value == 8) { obj.statusleads = statusassignpss; }
                    else if (obj.agt_statusleads.Value == 9) { obj.statusleads = statusassignsdh; }
                    else if (obj.agt_statusleads.Value == 10) { obj.statusleads = statusassignofficer; }
                    else if (obj.agt_statusleads.Value == 11) { obj.statusleads = statuskeyaccount; }
                    else if (obj.agt_statusleads.Value == 12) { obj.statusleads = statusassignboh; }
                    else { obj.statusleads = null; }
                    #endregion status leads
                    obj.agt_customer = getCustomer;

                    //}
                    lst.Add(obj);
                }

                return lst;

            }
            catch(Exception ex)
            {
                HttpStatusCode status = new HttpStatusCode();
                var message = status + ex.Message;
                //return StatusCode(status);
                throw new Exception(message);
            }
        }


        [HttpGet]
        [Route("api/leads/getid/{id}")] //kalo gamau pakek route di comment aja
        public List<LeadsId> GetID(Guid Id)
        {
            try
            {
                List<LeadsId> lst = new List<LeadsId>();

                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login

                QueryExpression queryLeads = new QueryExpression("agt_leads");
                queryLeads.ColumnSet.AllColumns = true;
                queryLeads.Criteria.AddCondition("agt_leadsid", ConditionOperator.Equal, Id);
                EntityCollection leadsColl = service.RetrieveMultiple(queryLeads);

                #region statusleads
                const string statusopen = "Open";
                const string statusacknowledge = "Acknowledge";
                const string statusassignbc = "Assign to BC";
                const string statusonprogress = "On Progress";
                const string statusresolve = "Resolve";
                const string statusclose = "Closed";
                const string statusassignpdh = "Assign to PDH";
                const string statusassignpss = "Assign to PSS";
                const string statusassignsdh = "Assign to SDH";
                const string statusassignofficer = "Assign to Officer";
                const string statuskeyaccount = "Key Account";
                const string statusassignboh = "Assign To BOH";
                #endregion
                foreach (Entity a in leadsColl.Entities)
                {
                    LeadsId obj = new LeadsId();

                    var getCustomer = a.GetAttributeValue<EntityReference>("agt_customer");

                    a.Id = Id;

                    obj.agt_leadsid = a.Id;
                    obj.agt_name = a.Attributes["agt_name"].ToString();
                    // obj.agt_customerName = getCustomer.Name;
                    obj.agt_subject = a.Attributes["agt_subject"].ToString();
                    obj.agt_statusleads = a.GetAttributeValue<OptionSetValue>("agt_statusleads");
                    #region status leads
                    if (obj.agt_statusleads == null) { obj.statusleads = null; }
                    else if (obj.agt_statusleads.Value == 1) { obj.statusleads = statusopen; }
                    else if (obj.agt_statusleads.Value == 2) { obj.statusleads = statusacknowledge; }
                    else if (obj.agt_statusleads.Value == 3) { obj.statusleads = statusassignbc; }
                    else if (obj.agt_statusleads.Value == 4) { obj.statusleads = statusonprogress; }
                    else if (obj.agt_statusleads.Value == 5) { obj.statusleads = statusresolve; }
                    else if (obj.agt_statusleads.Value == 6) { obj.statusleads = statusclose; }
                    else if (obj.agt_statusleads.Value == 7) { obj.statusleads = statusassignpdh; }
                    else if (obj.agt_statusleads.Value == 8) { obj.statusleads = statusassignpss; }
                    else if (obj.agt_statusleads.Value == 9) { obj.statusleads = statusassignsdh; }
                    else if (obj.agt_statusleads.Value == 10) { obj.statusleads = statusassignofficer; }
                    else if (obj.agt_statusleads.Value == 11) { obj.statusleads = statuskeyaccount; }
                    else if (obj.agt_statusleads.Value == 12) { obj.statusleads = statusassignboh; }
                    else { obj.statusleads = null; }
                    #endregion status leads
                    obj.agt_customer = getCustomer;

                    lst.Add(obj);
                }
                return lst;

            }
            catch (Exception ex)
            {
                HttpStatusCode status = new HttpStatusCode();
                var message = status + ex.Message;
                //return StatusCode(status);
                throw new Exception(message);
            }
        }






        [HttpPost]
        [Route("api/leads/createleads")]  //kalo ga mau pakek route di comment aja
        public async Task<IHttpActionResult> Post([FromBody] LeadsCreate createleads)
        {
            try
            {
                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login

                QueryExpression ldsquery = new QueryExpression("agt_leads");
                ldsquery.ColumnSet.AllColumns = true;



                Entity lstdua = new Entity();
                lstdua.LogicalName = "agt_leads";

                var id = lstdua.Id;
                //Entity getLeads = service.Retrieve("agt_leads", lstdua.Id, new ColumnSet(true));


                //lstdua.Attributes["agt_name"] = "- / 22 / 00020";
                var getCustomer = createleads.agt_customer;

                lstdua.Attributes["agt_customer"] = getCustomer ; //createleads.agt_customer.Equals(accountdata.Id);//new Guid(createleads.agt_customer); // new Lookup("TRAKTOR NUSANTARA, PT", createleads.agt_customer);
                lstdua.Attributes["agt_branch1"] = createleads.agt_branch1;
                lstdua.Attributes["agt_typeleads"] = new OptionSetValue(createleads.agt_typeleads);
                lstdua.Attributes["agt_subject"] = createleads.agt_subject;
                lstdua.Attributes["agt_description"] = createleads.agt_description;
                lstdua.Attributes["agt_urgency"] = new OptionSetValue(createleads.agt_urgency);
                lstdua.Attributes["agt_channel"] = new OptionSetValue(createleads.agt_channel);
                //lstdua.Attributes["agt_statusleads"] = new OptionSetValue(1);

                //lstdua.Attributes["CreatedBy"] = "1CBA90DC-4DE9-E111-9AA2-544249894792";
                //lstdua.Attributes["CreatedOn"] = DateTime.Now;

                var leadsColl = service.Create(lstdua);


                return Ok("Leads has Been Created");
            }

            catch (Exception ex)
            {
                HttpStatusCode status = new HttpStatusCode();
                var message = status + ex.Message;
                //return StatusCode(status);
                throw new Exception(message);
            }

        }
      



        [HttpPut]
        //PUT api/values/5
        [Route("api/leads/updateleads/{id}")]  //kalo ga mau pakek route di comment aja
        public async Task<IHttpActionResult> Put(Guid id, [FromBody] LeadsUpdate updateleads)
        {
            try
            {
                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login

                #region get leads id
                Leads lst = new Leads();
                QueryExpression queryLeads = new QueryExpression("agt_leads");
                queryLeads.ColumnSet.AllColumns = true;
                EntityCollection leadsCollid = service.RetrieveMultiple(queryLeads);
                Leads obj = new Leads();
                foreach (Entity lds in leadsCollid.Entities)
                {

                    obj.agt_leadsid = lds.Id;
                    //lst.Add(obj);
                }
                #endregion

                id = obj.agt_leadsid;

                //updateleads.agt_leadsid = id;

                Entity myEntity = new Entity("agt_leads");

                myEntity.Id = id;

                //myEntity["agt_customer"] = updateleads.agt_customer;
                //myEntity["agt_branch1"] = updateleads.agt_branch1;
                //myEntity["agt_typeleads"] = new OptionSetValue(updateleads.agt_typeleads);
                myEntity["agt_subject"] = updateleads.agt_subject;
                //myEntity["agt_description"] = updateleads.agt_description;
                //myEntity["agt_urgency"] = new OptionSetValue(updateleads.agt_urgency);
                //myEntity["agt_channel"] = new OptionSetValue(updateleads.agt_channel);

                /*var leadsColl =*/ service.Update(myEntity);


                return Ok("Leads has Been Updated");

            }
            catch (Exception ex)
            {
                HttpStatusCode status = new HttpStatusCode();
                var message = status + ex.Message;
                //return StatusCode(status);
                throw new Exception(message);
            }
        }



        [HttpDelete]
        // DELETE api/values/5
        [Route("api/leads/deleteleads/{id}")]  //kalo ga mau pakek route di comment aja
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            try
            {
                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login

              

                Entity myEntity = new Entity("agt_leads");
                myEntity.Id = id;

                service.Delete("agt_leads", myEntity.Id);


                return Ok("Leads has Been Deleted");
            }
            catch(Exception ex)
            {
                HttpStatusCode status = new HttpStatusCode();
                var message = status + ex.Message;
                throw new Exception(message);            }
        }


    }
}