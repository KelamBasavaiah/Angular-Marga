using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ReleaseManagementDALLibrary;
using ReleaseManagementProjectLibrary;

namespace ManagerBLLibrary
{
    public class ManagerBL
    {
        ManagerDAL dal;
        public ManagerBL()
        {
            dal = new ManagerDAL();
        }
            public bool checkingadminlogin(string username, string password)
            {
                return dal.checkadminlogin(username, password);
            }
    
    public bool checkinguserlogin(string username, string password)
    {
        return dal.checkuserlogin(username, password);
    }
        public List<ReleaseManagementModel> GetAllProjectsForDeveloperFromManager(string username)
        {
            List<ReleaseManagementModel> projects = new List<ReleaseManagementModel>();
            DataSet dsGetProject = dal.GetAllProjectForDeveloperFromManager(username);
            ReleaseManagementModel project;
            foreach (DataRow row in dsGetProject.Tables[0].Rows)
            {
                project = new ReleaseManagementModel();
                project.ProjectName = row[0].ToString();
                project.ProjectDescription = row[1].ToString();
                project.ProjectStartDate = Convert.ToDateTime(row[2].ToString());
                project.ProjectEndDate = Convert.ToDateTime(row[3].ToString());             
                projects.Add(project);
            }
            return projects;

        }
        //Get All modules assigned by manager 
        public List<ReleaseManagementModel> GetAllModulesForDeveloper(string projectName,string username)
        {
            List<ReleaseManagementModel> modules = new List<ReleaseManagementModel>();
            DataSet dsGetModules = dal.GetAllmodulesForDeveloper(projectName,username);
            ReleaseManagementModel module;
            foreach (DataRow row in dsGetModules.Tables[0].Rows)
            {
                module = new ReleaseManagementModel();
                module.ModuleName = row[0].ToString();
                module.ModuleDescription = row[1].ToString();
                module.ModuleStatus = row[2].ToString();
                module.ModuleStartDate = Convert.ToDateTime(row[3].ToString());
                module.ModuleEndDate = Convert.ToDateTime(row[4].ToString());
                modules.Add(module);
            }
            return modules;
        }
        public bool UpdateModuleStatustoManager(string module_name)
        {
            return dal.UpdateModuleStatustoManager(module_name);
        }

        //Get All ModuleNames And BugNames and BugStatus//
        public List<ReleaseManagementModel> GetAllModuleNamesAndBugNames(string username)
        {
            List<ReleaseManagementModel> Modules = new List<ReleaseManagementModel>();
            DataSet dsGetAllModules = dal.GetAllModuleNamesAndBugNames(username);
            ReleaseManagementModel bug;
            foreach (DataRow row in dsGetAllModules.Tables[0].Rows)
            {
                bug = new ReleaseManagementModel();
                bug.ModuleName = row[0].ToString();
                bug.Bugstatus = row[1].ToString();
                bug.Bugname = row[2].ToString();

                Modules.Add(bug);
            }
            return Modules;

        }
        // Get All ModuleNames And ModuleDescription//
        public List<ReleaseManagementModel> GetAllModuleNamesAndModuleDescription(string modulename)
        {
            List<ReleaseManagementModel> Modules = new List<ReleaseManagementModel>();
            DataSet dsGetAllModulesAndModuleDescription = dal.GetModuleNamesAndModuleDescription(modulename);
            ReleaseManagementModel bug;
            foreach (DataRow row in dsGetAllModulesAndModuleDescription.Tables[0].Rows)
            {
                bug = new ReleaseManagementModel();
                bug.ModuleName = row[0].ToString();
                bug.ModuleDescription = row[1].ToString();
                Modules.Add(bug);
            }
            return Modules;
        }


        //Update BugStatus To Tester//
        public bool UpdateBugStatusToTester(string modulename)
        {
            string bugStatus = "Bug Fixed";
            return dal.UpdateBugStatusToTester(modulename,bugStatus);
        }
        //Get BugStatus From Tester//

        
        //tester
        public List<ReleaseManagementModel> GetAllTesterProjects(string username)
        {
            return dal.GetAllTesterProjects(username);

        }
        public List<ReleaseManagementModel> GetAllTesterModules(string P_Name,string username)
        {
            return dal.GetAllTesterModules(P_Name,username);
        }
        public bool UpdateModuleStatusByTester(string module_name)
        {
            return dal.UpdateModuleStatusByTester(module_name);
        }
        //tester2
        public List<ReleaseManagementModel> bugFixedModuleData(string tname)
        {
            return dal.bugFixedModuleData(tname);
        }
        public ReleaseManagementModel GetBugFixedModule(string modulename)
        {
            
            DataSet dsGetBugFixedModules = dal.GetBugFixedModules(modulename);
            ReleaseManagementModel bugFixedModule = new ReleaseManagementModel();
            foreach (DataRow row in dsGetBugFixedModules.Tables[0].Rows)
            {
                bugFixedModule.ModuleName = row[0].ToString();
                bugFixedModule.ModuleDescription = row[1].ToString();
                bugFixedModule.ModuleStatus = row[2].ToString();
            }
            return bugFixedModule;
        }
        public ReleaseManagementModel testerCreateBug(ReleaseManagementModel tCB)
        {
            tCB = dal.testerCreateBug(tCB);
            return tCB;
        }
        //Manager
        //Get all projects in a list with project id,name and description
        public List<ReleaseManagementModel> GetAllAssignedModules(string userName)
        {
            DataSet dsGetprojectId = dal.GetProjectId(userName);
            List<ReleaseManagementModel> projects = new List<ReleaseManagementModel>();
            ReleaseManagementModel project;

            foreach (DataRow row in dsGetprojectId.Tables[0].Rows)
            {
                project = new ReleaseManagementModel();
                project.ProjectId = row[0].ToString();
                project.ProjectName = row[0].ToString();
                projects.Add(project);
            }
            DataSet dsGetAllAssignedModules;
            ReleaseManagementModel modules;
            List<ReleaseManagementModel> assignedModules = new List<ReleaseManagementModel>();
            for (int i = 0; i < projects.Count; i++)
            {
               
                dsGetAllAssignedModules = dal.GetAllAssignedModules(projects[i].ProjectId);
                foreach (DataRow row in dsGetAllAssignedModules.Tables[0].Rows)
                {
                    modules = new ReleaseManagementModel();
                    modules.ProjectName = row[0].ToString();
                    modules.ModuleName = row[1].ToString();
                    modules.ModuleStatus = row[2].ToString();
                    modules.ModuleId = row[3].ToString();
                    assignedModules.Add(modules);
                    

                }
                
            }

            DataSet dsGetAllDeveloperName = dal.GetAllDeveloperName();
            DataSet dsGetAllTesterName = dal.GetAllTesterName();       
            List<ReleaseManagementModel> developerList = new List<ReleaseManagementModel>();

            ReleaseManagementModel developer;
            foreach(DataRow row in dsGetAllDeveloperName.Tables[0].Rows)
            {
                developer = new ReleaseManagementModel();
                developer.DeveloperName = row[0].ToString();
                developer.ModuleId = row[1].ToString();
                developerList.Add(developer);
            }
            List<ReleaseManagementModel> testerList = new List<ReleaseManagementModel>();

            ReleaseManagementModel tester ;
            foreach (DataRow row in dsGetAllTesterName.Tables[0].Rows)
            {
                tester = new ReleaseManagementModel();
                tester.TesterName = row[0].ToString();
                tester.ModuleId = row[1].ToString();
                testerList.Add(tester);
            }
            for (int i = 0; i < developerList.Count; i++)
            {
                for(int j = 0; j < assignedModules.Count; j++)
                {
                    if (developerList[i].ModuleId == assignedModules[j].ModuleId)
                    {
                        assignedModules[j].DeveloperName = developerList[i].DeveloperName;
                    }
                }
            
            }
            for (int i = 0; i < testerList.Count; i++)
            {
                for (int j = 0; j < assignedModules.Count; j++)
                {
                    if (testerList[i].ModuleId == assignedModules[j].ModuleId)
                    {
                        assignedModules[j].TesterName = testerList[i].TesterName;
                    }
                }

            }
            for (int i=0;i<assignedModules.Count;i++)
            {
                if (assignedModules[i].ModuleStatus == "Completed"){
                    assignedModules.Remove(assignedModules[i]);

                }
                if(assignedModules[i].ModuleStatus=="Not Started")
                {
                    assignedModules.Remove(assignedModules[i]);
                }

            }
            return assignedModules;

        }
        public List<ReleaseManagementModel> GetAllEmployees()
        {
            List<ReleaseManagementModel> employees = new List<ReleaseManagementModel>();
            DataSet dsGetAllEmployees = dal.GetAllEmployees();
            ReleaseManagementModel employee;
            foreach (DataRow row in dsGetAllEmployees.Tables[0].Rows)
            {
                employee = new ReleaseManagementModel();
                employee.EmployeeId = row[0].ToString();
                employee.EmployeeName = row[1].ToString();
                employees.Add(employee);
            }
            return employees;
        }
        public List<ReleaseManagementModel> GetAllAssignedEmployees()
        {
            List<ReleaseManagementModel> employees = new List<ReleaseManagementModel>();
            DataSet dsGetAllEmployees = dal.GetAllAssignedEmployees();
            ReleaseManagementModel employee;
            foreach (DataRow row in dsGetAllEmployees.Tables[0].Rows)
            {
                employee = new ReleaseManagementModel();
                employee.EmployeeId = row[0].ToString();
                employee.EmployeeName = row[1].ToString();

                employees.Add(employee);
            }


            return employees;
        }
        public List<ReleaseManagementModel> GetEmployeesToAssign()
        {
            List<ReleaseManagementModel> assignedEmployees = GetAllAssignedEmployees();
            List<ReleaseManagementModel> allEmployees = GetAllEmployees();
            List<ReleaseManagementModel> Employees = new List<ReleaseManagementModel>();
            ReleaseManagementModel employee;
                int count = 0;
            for(int i = 0; i < allEmployees.Count; i++)
            {
                count = 0;
                for(int j = 0; j < assignedEmployees.Count; j++)
                {
                    if (allEmployees[i].EmployeeId == assignedEmployees[j].EmployeeId)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    employee = new ReleaseManagementModel();
                    employee.EmployeeName = allEmployees[i].EmployeeName;
                    Employees.Add(employee);
                }
                
            }
            return Employees;

        }
        public List<ReleaseManagementModel> GetAllModuleDetails()
        {
            List<ReleaseManagementModel> modules = new List<ReleaseManagementModel>();
            DataSet dsGetAllModuleDetails = dal.GetAllModuleDetails();
            ReleaseManagementModel module;
            foreach (DataRow row in dsGetAllModuleDetails.Tables[0].Rows)
            {
                module = new ReleaseManagementModel();
                module.ModuleId = row[0].ToString();
                module.ModuleName = row[1].ToString();
                modules.Add(module);
            }
            return modules;
        }
        public bool AssignModuleToDeveloper(ReleaseManagementModel manager)
        {
            List<ReleaseManagementModel> modules = GetAllModuleDetails();
            for(int i = 0; i < modules.Count; i++)
            {
                if (modules[i].ModuleName == manager.ModuleName)
                {
                    manager.ModuleId = modules[i].ModuleId;
                }
            }
            string moduleId = manager.ModuleId;
            List<ReleaseManagementModel> employees = new List<ReleaseManagementModel>();
            DataSet dsGetAllEmployees = dal.GetAllEmployees();
            ReleaseManagementModel employee;
            foreach (DataRow row in dsGetAllEmployees.Tables[0].Rows)
            {
                employee = new ReleaseManagementModel();
                employee.EmployeeId = row[0].ToString();
                employee.EmployeeName = row[1].ToString();
                employees.Add(employee);
            }
            for(int i = 0; i < employees.Count; i++)
            {
                if (employees[i].EmployeeName == manager.DeveloperName)
                {
                    manager.EmployeeId = employees[i].EmployeeId;
                }
            }
            string EmpId = manager.EmployeeId;
            string status = "In Progress";
            string moduleName = manager.ModuleName;
            bool modStatus = false;
            bool insertRoleStatus = false;
            bool assignedStatus= dal.AssignModuleToEmployee(manager);
            if (assignedStatus == true)
            {
                 modStatus= dal.UpdateModuleStatus(moduleName, status);
            }
            else
            {
                modStatus = false;
            }
            string role = "developer";
           
            if (modStatus == true)
            {
                insertRoleStatus= dal.InsertRole(EmpId,role,moduleId);
            }
            return insertRoleStatus;

        }
        public bool AssignModuleToTester(ReleaseManagementModel manager)
        {
            List<ReleaseManagementModel> modules = GetAllModuleDetails();
            for (int i = 0; i < modules.Count; i++)
            {
                if (modules[i].ModuleName == manager.ModuleName)
                {
                    manager.ModuleId = modules[i].ModuleId;
                    break;
                }
            }
            string moduleId = manager.ModuleId;
            List<ReleaseManagementModel> employees = new List<ReleaseManagementModel>();
            DataSet dsGetAllEmployees = dal.GetAllEmployees();
            ReleaseManagementModel employee;
            foreach (DataRow row in dsGetAllEmployees.Tables[0].Rows)
            {
                employee = new ReleaseManagementModel();
                employee.EmployeeId = row[0].ToString();
                employee.EmployeeName = row[1].ToString();
                employees.Add(employee);
            }
            for (int i = 0; i < employees.Count; i++)
            {
                if (employees[i].EmployeeName == manager.TesterName)
                {
                    manager.EmployeeId = employees[i].EmployeeId;
                }
            }
            string EmpId = manager.EmployeeId;
            string status = "In Progress";
            string moduleName = manager.ModuleName;
            bool modStatus = false;
            bool insertRoleStatus = false;
            bool assignedStatus = dal.AssignModuleToEmployee(manager);
            if (assignedStatus == true)
            {
                modStatus = dal.UpdateModuleStatus(moduleName, status);
            }
            else
            {
                modStatus = false;
            }
            string role = "tester";           
            if (modStatus == true)
            {
                insertRoleStatus = dal.InsertRole(EmpId,role,moduleId);
            }
            return insertRoleStatus;

        }

        public bool UpdateModuleStatusAfterTesting(string moduleId)
        { 
            string moduleStatus="Completed";
            return dal.UpdateModuleStatus(moduleId, moduleStatus);
        }
        public List<ReleaseManagementModel> GetAllCompletedModules(string userName)
        {
            List<ReleaseManagementModel> comlpetedModules = new List<ReleaseManagementModel>();
            DataSet dsGetprojectId = dal.GetProjectId(userName);
            List<ReleaseManagementModel> projects = new List<ReleaseManagementModel>();
            ReleaseManagementModel project;

            foreach (DataRow row in dsGetprojectId.Tables[0].Rows)
            {
                project = new ReleaseManagementModel();
                project.ProjectId = row[0].ToString();
                project.ProjectName = row[0].ToString();
                projects.Add(project);
            }

            DataSet dsGetAllCompletedModules;
            ReleaseManagementModel modules;
            for (int i = 0; i < projects.Count; i++)
            {
                dsGetAllCompletedModules = dal.GetAllCompletedModules(projects[i].ProjectId);
                foreach (DataRow row in dsGetAllCompletedModules.Tables[0].Rows)
                {
                    modules = new ReleaseManagementModel();
                    modules.ProjectName = row[0].ToString();
                    modules.ModuleName = row[1].ToString();
                    modules.ModuleStatus = row[2].ToString();
                    modules.ModuleId = row[3].ToString();
                    comlpetedModules.Add(modules);
                    
                    
                }
                
            }
                      
            DataSet dsGetAllDeveloperName = dal.GetAllDeveloperName();
            DataSet dsGetAllTesterName = dal.GetAllTesterName();


            List<ReleaseManagementModel> developerList = new List<ReleaseManagementModel>();

            ReleaseManagementModel developer;
            foreach (DataRow row in dsGetAllDeveloperName.Tables[0].Rows)
            {
                developer = new ReleaseManagementModel();
                developer.DeveloperName = row[0].ToString();
                developer.ModuleId = row[1].ToString();
                developerList.Add(developer);
            }
            List<ReleaseManagementModel> testerList = new List<ReleaseManagementModel>();

            ReleaseManagementModel tester;
            foreach (DataRow row in dsGetAllTesterName.Tables[0].Rows)
            {
                tester = new ReleaseManagementModel();
                tester.TesterName = row[0].ToString();
                tester.ModuleId = row[1].ToString();
                testerList.Add(tester);
            }
            for (int i = 0; i < developerList.Count; i++)
            {
                for (int j = 0; j < comlpetedModules.Count; j++)
                {
                    if (developerList[i].ModuleId == comlpetedModules[j].ModuleId)
                    {
                        comlpetedModules[j].DeveloperName = developerList[i].DeveloperName;
                    }
                }

            }
            for (int i = 0; i < testerList.Count; i++)
            {
                for (int j = 0; j < comlpetedModules.Count; j++)
                {
                    if (testerList[i].ModuleId == comlpetedModules[j].ModuleId)
                    {
                        comlpetedModules[j].TesterName = testerList[i].TesterName;
                    }
                }

            }
          
            return comlpetedModules;

        }
        public List<ReleaseManagementModel> GetProjects(string username)
        {
            DataSet dsGetprojectId = dal.GetProjectId(username);
            List<ReleaseManagementModel> projects = new List<ReleaseManagementModel>();
            ReleaseManagementModel project;
            
            foreach (DataRow row in dsGetprojectId.Tables[0].Rows)
            {
                project = new ReleaseManagementModel();
                project.ProjectId = row[0].ToString();
                project.ProjectName = row[1].ToString();
                projects.Add(project);
            }
            for(int i = 0; i < projects.Count; i++)
            {
                projects[i].TotalModules = dal.GetModuleCount(projects[i].ProjectId);
                projects[i].ModuleCount = dal.GetCompletedModuleCount(projects[i].ProjectId);
                
            }

            return projects;
        }
        public bool UpdateCompletedModuleStatus(string projectname)
        {
            return dal.UpdateCompletedmoduleStatus(projectname);
        }
        public List<ReleaseManagementModel> GetAllCompletedProjects(string username)
        {
            DataSet dsGetprojectId = dal.GetProjectId(username);
            List<ReleaseManagementModel> projects = new List<ReleaseManagementModel>();
            ReleaseManagementModel project;

            foreach (DataRow row in dsGetprojectId.Tables[0].Rows)
            {
                project = new ReleaseManagementModel();
                project.ProjectId = row[0].ToString();
                project.ProjectName = row[1].ToString();
                projects.Add(project);
            }
            for(int i = 0; i < projects.Count; i++)
            {
                projects[i].TotalModules = dal.GetModuleCount(projects[i].ProjectId);
                projects[i].ModuleCount = dal.GetCompletedModulesCount(projects[i].ProjectId);
            }
            List<ReleaseManagementModel> completedProjects = new List<ReleaseManagementModel>();
            ReleaseManagementModel completedProject = null;
            for(int i = 0; i < projects.Count; i++)
            {
                if (projects[i].TotalModules == projects[i].ModuleCount)
                {
                    DataSet dscompletedproject = dal.GetCompletedProject(projects[i].ProjectId);
                    foreach (DataRow row in dscompletedproject.Tables[0].Rows)
                    {
                        completedProject = new ReleaseManagementModel();
                        completedProject.ProjectName = row[0].ToString();
                        completedProject.ProjectDescription = row[1].ToString();
                        completedProject.ProjectStartDate = Convert.ToDateTime(row[2].ToString());
                        completedProject.ProjectEndDate = Convert.ToDateTime(row[3].ToString());
                       completedProjects.Add(completedProject);
                    }
                }
            }
            return completedProjects;


        }
        public bool InsertProjectDetails(string username, ReleaseManagementModel manager)
        {
            string projectName = manager.ProjectName;
            string projectId = "";
            bool status = dal.InsertProjects(manager);
            string employeeId = dal.GetEmployeeId(username);
            if (status == true)
            {
                projectId = dal.GetProjectIdForManager(projectName);
            }
            string role = "Manager";
            return dal.InsertRoles(employeeId, role, projectId);

        }
        public bool InsertModuleDetails(string projectName, ReleaseManagementModel[] manager)
        {
            bool inserted = false;
            string projectId = dal.GetProjectIdForManager(projectName);
            foreach (var item in manager)
            {
                item.ProjectId = projectId;
                item.ModuleStatus = "Not started";
                inserted = dal.InsertProjectModules(item);
            }
            return inserted;


        }

        public List<ReleaseManagementModel> GetAllProjects(string username)
        {
            return dal.GetAllProjects(username);
        }
        public List<ReleaseManagementModel> GetAllModules(string project_name)
        {
            return dal.GetAllModules(project_name);
        }
        public string GetEmail(string username)
        {
            return dal.GetEmail(username);
        }
        public bool UpdateForgotPassword(string username,string password)
        {
            bool updated = false;
            updated = dal.UpdatePassword(username, password);
            return updated;
        }
    }
}
