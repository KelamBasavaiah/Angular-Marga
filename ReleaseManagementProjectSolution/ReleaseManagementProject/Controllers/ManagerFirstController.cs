using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ManagerBLLibrary;
using ReleaseManagementProjectLibrary;
using System.Web.Http.Cors;

namespace ReleaseManagementProject.Controllers
{
    [EnableCors("http://localhost:4200", "*", "GET,POST,PUT,DELETE")]

    public class ManagerFirstController : ApiController
    {

        ManagerBL bl = new ManagerBL();
        // GET api/values
       


        //Get All Projects
        public List<ReleaseManagementModel> Get(string username)
        {


            
            return bl.GetAllProjects(username);



        }
        
        
        //Insert Modules
        public bool Post(string projectName, [FromBody]ReleaseManagementModel[] value)
        {
            try
            {
                return bl.InsertModuleDetails(projectName, value);

            }
            catch(Exception e)
            {
                
                return false;
            }
        }

        //// GET api/values/5
        /////Get All Modules
        public List<ReleaseManagementModel> Delete(string projectId)
        {
            return bl.GetAllModules(projectId);
        }
    }

}
