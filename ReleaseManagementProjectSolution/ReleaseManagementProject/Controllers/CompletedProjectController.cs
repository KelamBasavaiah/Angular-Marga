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
    [EnableCors("http://localhost:4200", "*", "GET,POST,PUT,DELETE")]

    public class CompletedProjectController : ApiController
    {
        ManagerBL bl = new ManagerBL();
        [SkipMyGlobalActionFilter]
        public List<ReleaseManagementModel> GetProjects(string username)
      {
           
            return bl.GetProjects(username);

        }
        [SkipMyGlobalActionFilter]
        public List<ReleaseManagementModel> delete(string userName)
        {
            return bl.GetAllCompletedModules(userName);
        }
        public bool Post([FromBody]ReleaseManagementModel value)
        {
            return bl.AssignModuleToDeveloper(value);

        }
    }
}
