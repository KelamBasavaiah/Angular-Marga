using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ManagerBLLibrary;
using ReleaseManagementProjectLibrary;
using System.Web.Http.Cors;
using ReleaseManagementProject.Models;
namespace ReleaseManagementProject.Controllers
{
    [EnableCors("http://localhost:4200", "*", "GET,POST,PUT","DELETE")]
    public class ValuesController : ApiController
    {
        ManagerBL bl = new ManagerBL();
        //GET api/values
        

        public List<ReleaseManagementModel> Get(string username)
         {

            return bl.GetAllAssignedModules(username);
        }


        public List<ReleaseManagementModel> Get()
        {
            return bl.GetEmployeesToAssign();
        }

        // POST api/values
        public bool Post([FromBody]ReleaseManagementModel value)
        {
            try
            {
                return bl.AssignModuleToTester(value);

            }
            catch(Exception e)
            {
                return false;
            }

        }

        // PUT api/values/5
        //public bool Put(string modulename,string module)
        //{
        //    return bl.UpdateModuleStatusAfterTesting(modulename);
        //}
        public bool Put(string modulename,[FromBody]ReleaseManagementModel values)
        {
            return bl.UpdateModuleStatusAfterTesting(values.ModuleId);
        }
        public bool Delete(string projectId)
        {
            return bl.UpdateCompletedModuleStatus(projectId);
        }
        // DELETE api/values/5
       
    }
}
