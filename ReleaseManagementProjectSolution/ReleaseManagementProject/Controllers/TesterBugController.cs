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

    public class TesterBugController : ApiController
    {
        ManagerBL bl = new ManagerBL();

        public IEnumerable<ReleaseManagementModel> Get(string username)
        {
            return bl.bugFixedModuleData(username);
        }
        public ReleaseManagementModel Post([FromBody] ReleaseManagementModel tcb)
        {
            if (tcb != null)
                return bl.testerCreateBug(tcb);
            else
                return new ReleaseManagementModel();
        }
    }
}
