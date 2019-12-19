using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseManagementProjectLibrary
{
    public class ReleaseManagementModel
    {
        string employeeId, employeeName, projectId, projectName, projectDescription,
            moduleId, moduleName, moduleDescription, role, moduleStatus;
        string developerName, testerName;
        string username, password;
        string employee_mail;
        string bugname, bugstatus, bugid;
        DateTime projectStartDate, projectEndDate, moduleStartDate, moduleEndDate, empStartDate, empEndDate;
        int moduleCount,totalModules;

        public string EmployeeId { get => employeeId; set => employeeId = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public string ProjectId { get => projectId; set => projectId = value; }
        public string ProjectName { get => projectName; set => projectName = value; }
        public string ProjectDescription { get => projectDescription; set => projectDescription = value; }       
        public string ModuleId { get => moduleId; set => moduleId = value; }
        public string ModuleName { get => moduleName; set => moduleName = value; }
        public string ModuleDescription { get => moduleDescription; set => moduleDescription = value; }        
        public string Role { get => role; set => role = value; }       
        public string ModuleStatus { get => moduleStatus; set => moduleStatus = value; }
        public string DeveloperName { get => developerName; set => developerName = value; }
        public string TesterName { get => testerName; set => testerName = value; }
        public DateTime ModuleEndDate { get => moduleEndDate; set => moduleEndDate = value; }
        public DateTime ProjectStartDate { get => projectStartDate; set => projectStartDate = value; }
        public DateTime ProjectEndDate { get => projectEndDate; set => projectEndDate = value; }
        public DateTime ModuleStartDate { get => moduleStartDate; set => moduleStartDate = value; }
        public DateTime EmpStartDate { get => empStartDate; set => empStartDate = value; }
        public DateTime EmpEndDate { get => empEndDate; set => empEndDate = value; }
        public int ModuleCount { get => moduleCount; set => moduleCount = value; }
        public int TotalModules { get => totalModules; set => totalModules = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Bugname { get => bugname; set => bugname = value; }
        public string Bugstatus { get => bugstatus; set => bugstatus = value; }
        public string Bugid { get => bugid; set => bugid = value; }
        public string Employee_mail { get => employee_mail; set => employee_mail = value; }
    }
}
