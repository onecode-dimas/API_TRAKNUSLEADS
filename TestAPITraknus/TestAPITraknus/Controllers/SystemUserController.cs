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
    public class SystemUserController : ApiController
    {
        // GET: SystemUser

        [HttpGet]
        [Route("api/systemuser/getid/{id}")] //kalo gamau pakek route di comment aja
        public List<SystemUser> GetID(Guid Id)
        {
            try
            {
                List<SystemUser> lst = new List<SystemUser>();

                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login
                QueryExpression querysystemuserposition = new QueryExpression("position");
                querysystemuserposition.ColumnSet = new ColumnSet(true);
                EntityCollection GetPosition = service.RetrieveMultiple(querysystemuserposition);
                var posisi = GetPosition.Entities.FirstOrDefault();

                QueryExpression querysystemuserdivision = new QueryExpression("new_division");
                querysystemuserdivision.ColumnSet = new ColumnSet(true);
                EntityCollection GetDivision = service.RetrieveMultiple(querysystemuserdivision);
                var divisi = GetDivision.Entities.FirstOrDefault();


                QueryExpression querySystemUser = new QueryExpression("systemuser");
                querySystemUser.ColumnSet.AllColumns = true;
                querySystemUser.Criteria.AddCondition("systemuserid", ConditionOperator.Equal, Id);
                //querySystemUser.Criteria.AddCondition("positionid", ConditionOperator.Equal, posisi.Id);
                //querySystemUser.Criteria.AddCondition("agt_division", ConditionOperator.Equal, divisi.Id);
                EntityCollection systemuserColl = service.RetrieveMultiple(querySystemUser);

                foreach (Entity a in systemuserColl.Entities)
                {
                    SystemUser obj = new SystemUser();

                    var getPosisi = a.GetAttributeValue<EntityReference>("positionid");
                    var getDivisi = a.GetAttributeValue<EntityReference>("agt_division");

                    a.Id = Id;

                    obj.systemuserid = a.Id;
                    //obj.agt_name = a.Attributes["agt_name"].ToString();
                    obj.fullname = a.Attributes["firstname"].ToString() + " " + a.Attributes["lastname"].ToString();
                    if (getPosisi != null) { obj.positionidname = getPosisi.Name; } else { obj.positionidname = null; }
                    if (getDivisi != null) { obj.divisionidname = getDivisi.Name; } else { obj.divisionidname = null; }
                    
                    //obj.agt_subject = a.Attributes["agt_subject"].ToString();;

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


        
        [HttpGet]
        [Route("api/systemuser/getposition")] //kalo gamau pakek route di comment aja
        public List<positionRef> GetPosition()
        {
            try
            {
                List<positionRef> lst = new List<positionRef>();

                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login
                QueryExpression querysystemuserposition = new QueryExpression("position");
                querysystemuserposition.ColumnSet = new ColumnSet(true);
                EntityCollection GetPosition = service.RetrieveMultiple(querysystemuserposition);
                var posisi = GetPosition.Entities.FirstOrDefault();

                QueryExpression query = new QueryExpression("position");
                query.ColumnSet.AddColumns("positionid");
                query.ColumnSet.AddColumns("name");
                EntityCollection listentities = service.RetrieveMultiple(query);


                foreach (Entity a in listentities.Entities)
                {
                    positionRef obj = new positionRef();

                    //var getPosisi = a.Attributes["positionid"].ToString().ToUpper();
                    var getPosisi = a.Attributes["positionid"].ToString();
                    var getName = a.Attributes["name"].ToString();


                    obj.positionid = getPosisi;
                    obj.name = getName;

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

        [HttpGet]
        [Route("api/systemuser/getdivision")] //kalo gamau pakek route di comment aja
        public List<divisionRef> GetDivision()
        {
            try
            {
                List<divisionRef> lst = new List<divisionRef>();

                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login
                QueryExpression querysystemuserposition = new QueryExpression("new_division");
                querysystemuserposition.ColumnSet = new ColumnSet(true);
                EntityCollection GetPosition = service.RetrieveMultiple(querysystemuserposition);
                var posisi = GetPosition.Entities.FirstOrDefault();

                QueryExpression query = new QueryExpression("new_division");
                query.ColumnSet.AddColumns("new_divisionid");
                query.ColumnSet.AddColumns("new_code");
                query.ColumnSet.AddColumns("new_divisionname");
                EntityCollection listentities = service.RetrieveMultiple(query);


                foreach (Entity a in listentities.Entities)
                {
                    divisionRef obj = new divisionRef();

                    obj.divisionid = a.Attributes["new_divisionid"].ToString();
                    obj.code = a.Attributes["new_code"].ToString();
                    obj.name = a.Attributes["new_divisionname"].ToString();

                   
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





        [HttpPut]
        //PUT api/values/5
        [Route("api/systemuser/updatesystemuser/{id}")]  //kalo ga mau pakek route di comment aja
        public async Task<IHttpActionResult> Put(Guid id, [FromBody] UpdateSystemUser updateleads)
        {
            try
            {
                #region login
                string username = ConfigurationSettings.AppSettings["CRMUserName"].ToString();
                string password = ConfigurationSettings.AppSettings["CRMPassword"].ToString();

                IOrganizationService service = CRMServiceController.CreatingCRMService(username, password);
                #endregion login
                Entity myEntity = new Entity("systemuser");
                Entity getUser = service.Retrieve("systemuser", id, new ColumnSet(true));

                var pos = updateleads.positionidname;
                var div = updateleads.divisionidname;

                QueryExpression querysystemuserposition = new QueryExpression("position");
                querysystemuserposition.ColumnSet = new ColumnSet(true);
                EntityCollection GetPosition = service.RetrieveMultiple(querysystemuserposition);
                var posisi = GetPosition.Entities.FirstOrDefault();

                QueryExpression querysystemuserposition2 = new QueryExpression("position");
                querysystemuserposition2.ColumnSet = new ColumnSet(true);
                querysystemuserposition2.Criteria.AddCondition("name", ConditionOperator.Equal, pos);
                EntityCollection GetPosition2 = service.RetrieveMultiple(querysystemuserposition2);
                var posisi2 = GetPosition2.Entities.FirstOrDefault();
                if (pos != null)
                {
                    GetPosition2 = service.RetrieveMultiple(querysystemuserposition2);
                    posisi2 = GetPosition2.Entities.FirstOrDefault();
                }
                else
                {
                    posisi2 = new Entity();
                }


                QueryExpression querysystemuserdivision = new QueryExpression("new_division");
                querysystemuserdivision.ColumnSet = new ColumnSet(true);
                EntityCollection GetDivision = service.RetrieveMultiple(querysystemuserdivision);
                var divisi = GetDivision.Entities.FirstOrDefault();

                QueryExpression querysystemuserdivision2 = new QueryExpression("new_division");
                querysystemuserdivision2.ColumnSet = new ColumnSet(true);
                querysystemuserdivision2.ColumnSet.AddColumn("divisionid");
                querysystemuserdivision2.Criteria.AddCondition("new_code", ConditionOperator.Equal, div);
                EntityCollection GetDivision2 = new EntityCollection();
                var divisi2 = new Entity();
                if (div != null)
                {
                    GetDivision2 = service.RetrieveMultiple(querysystemuserdivision2);
                    divisi2 = GetDivision2.Entities.FirstOrDefault();
                }
                else
                {
                    divisi2 = new Entity();
                }


                #region get user
                QueryExpression querySystemUser = new QueryExpression("systemuser");
                querySystemUser.ColumnSet.AllColumns = true;
                querySystemUser.ColumnSet.AddColumn("agt_division");
                querySystemUser.Criteria.AddCondition("systemuserid", ConditionOperator.Equal, getUser.Id);
               // querySystemUser.Criteria.AddCondition("positionid", ConditionOperator.Equal, posisi.Id);
                // querySystemUser.Criteria.AddCondition("agt_division", ConditionOperator.Equal, divisi.Id);
                EntityCollection systemuserColl = service.RetrieveMultiple(querySystemUser);
                Entity systemuser = systemuserColl.Entities.FirstOrDefault();

              

                Entity getPos = service.Retrieve("systemuser", getUser.Id, new ColumnSet(true));
                var idnyapos = systemuser.Contains("positionid") ? systemuser.GetAttributeValue<EntityReference>("positionid") : null;
                if (idnyapos == null)
                {
                    Entity getPosid = service.Retrieve("position", posisi2.Id, new ColumnSet(true));
                    var idpos = getPosid.ToEntityReference();
                    myEntity["positionid"] = idpos;
                }
                else
                {
                    idnyapos.Id = posisi2.Id;
                    idnyapos.Name = pos;
                    idnyapos.LogicalName = "position";
                    myEntity["positionid"] = idnyapos;
                }

                var idnyadiv = systemuser.Contains("agt_division") ? systemuser.GetAttributeValue<EntityReference>("agt_division") : null;
                if (idnyadiv == null)
                {
                    Entity getDiv = service.Retrieve("new_division", divisi2.Id, new ColumnSet(true));
                    var iddiv = getDiv.ToEntityReference();
                    myEntity["agt_division"] = iddiv;
                }
                else
                {
                   idnyadiv.Id = divisi2.Id;
                   idnyadiv.Name = div;
                   idnyadiv.LogicalName = "new_division";
                   myEntity["agt_division"] = idnyadiv;
                }

                if (div == null)
                {
                    myEntity["agt_division"] = null;
                }
                if (pos == null)
                {
                    myEntity["positionid"] = null;
                }



                //myEntity["agt_division"] = idnyadiv;
                //myEntity["positionid"] = idnyapos;
                myEntity.Id = getUser.Id;
                service.Update(myEntity);

               
                #endregion

                return Ok("Systemuser has Been Updated");

            }
            catch (Exception ex)
            {
                HttpStatusCode status = new HttpStatusCode();
                var message = status + ex.Message;
                //return StatusCode(status);
                throw new Exception(message);
            }
        }


    }
}