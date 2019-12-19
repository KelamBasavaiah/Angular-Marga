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

    public class DeveloperBugController : ApiController
    {
        ManagerBL bl = new ManagerBL();

        public IEnumerable<ReleaseManagementModel> Get(string username)
        {

            return bl.GetAllModuleNamesAndBugNames(username);

        }
        public IEnumerable<ReleaseManagementModel> Get(string modulename,string value)
        {
            return bl.GetAllModuleNamesAndModuleDescription(modulename);

        }
        // PUT: api/ModuleNameAndDescription/5
        public bool Put(string value, [FromBody]string value1)
        {
            return bl.UpdateBugStatusToTester(value);
        }

    }
}
