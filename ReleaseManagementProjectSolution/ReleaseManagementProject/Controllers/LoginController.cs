using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ManagerBLLibrary;
using ReleaseManagementProjectLibrary;
using System.Web.Http.Cors;
using AuthenticationService.Models;
using AuthenticationService.Managers;
using System.Security.Claims;
using System.Net.Mail;
using ReleaseManagementProject.Models;

namespace ReleaseManagementProject.Controllers
{
    [EnableCors("http://localhost:4200", "*", "GET,POST,PUT,DELETE")]

    public class LoginController : ApiController
    {
        ManagerBL bl = new ManagerBL();

        [SkipMyGlobalActionFilter]
        public List<ReleaseManagementModel> GetCompletedProjects(string username)
        {
            return bl.GetAllCompletedProjects(username);
        }

        [SkipMyGlobalActionFilter]
        //public HttpResponseMessage Post(ReleaseManagementModel releaseManagementModel)
        //{
        //    string
        //    if (bl.checkingadminlogin(username, password) == true)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}
        public string Post(ReleaseManagementModel userLogin)
        {
            string token = "";
            bool value = bl.checkinguserlogin(userLogin.Username, userLogin.Password);
            if (value)

            {
                IAuthContainerModel model = GetJWTContainerModel(userLogin.Username, userLogin.Role);
                IAuthService authService = new JWTService(model.SecretKey);
                token = authService.GenerateToken(model);
                var message = Request.CreateResponse(HttpStatusCode.OK, "Authentication successfull");
                message.Headers.Add("JWT_TOKEN", token);

                return token;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
        }
        #region Private Methods
        private static JWTContainerModel GetJWTContainerModel(string userName, string role)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, role),
                    
                }
            };
        }
        #endregion
            

        }

    }

