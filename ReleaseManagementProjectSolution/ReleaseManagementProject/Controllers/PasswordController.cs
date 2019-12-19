using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReleaseManagementProjectLibrary;
using ManagerBLLibrary;
using System.Web.Http.Cors;
using System.Net.Mail;

namespace ReleaseManagementProject.Controllers
{
    [EnableCors("http://localhost:4200","*","PUT")]
    public class PasswordController : ApiController
    {
        ManagerBL bl = new ManagerBL();
        public bool Put([FromBody]ReleaseManagementModel model)
        {
            bool updated = false;
            string email = bl.GetEmail(model.Username);
            updated = bl.UpdateForgotPassword(model.Username, model.Password);
            if (updated)
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("divhyadarsh429@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Password updation Status";
                mail.Body = email + "Password updated Successfully";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("divhyadarsh429@gmail.com", "dbinfosys29");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            return updated;
        }
    }
}
