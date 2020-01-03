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
                project.ProjectId = row[0].ToString();
                project.ProjectName = row[1].ToString();
                project.ProjectDescription = row[2].ToString();
                project.ProjectStartDate = Convert.ToDateTime(row[3].ToString());
                project.ProjectEndDate = Convert.ToDateTime(row[4].ToString());             
                projects.Add(project);
            }
            return projects;

        }
        //Get All modules assigned by manager 
        public List<ReleaseManagementModel> GetAllModulesForDeveloper(string projectId,string username)
        {
            List<ReleaseManagementModel> modules = new List<ReleaseManagementModel>();
            DataSet dsGetModules = dal.GetAllmodulesForDeveloper(projectId,username);
            ReleaseManagementModel module;
            foreach (DataRow row in dsGetModules.Tables[0].Rows)
            {
                module = new ReleaseManagementModel();
                module.ModuleId = row[0].ToString();
                module.ModuleName = row[1].ToString();
                module.ModuleDescription = row[2].ToString();
                module.ModuleStatus = row[3].ToString();
                module.ModuleStartDate = Convert.ToDateTime(row[4].ToString());
                module.ModuleEndDate = Convert.ToDateTime(row[5].ToString());
                modules.Add(module);
            }
            return modules;
        }
        public bool UpdateModuleStatustoManager(string module_Id)
        {
            return dal.UpdateModuleStatustoManager(module_Id);
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
                bug.ModuleId = row[0].ToString();
                bug.ModuleName = row[1].ToString();
                bug.Bugstatus = row[2].ToString();
                bug.Bugname = row[3].ToString();

                Modules.Add(bug);
            }
            return Modules;

        }
        // Get All ModuleNames And ModuleDescription//
        public List<ReleaseManagementModel> GetAllModuleNamesAndModuleDescription(string moduleId)
        {
            List<ReleaseManagementModel> Modules = new List<ReleaseManagementModel>();
            DataSet dsGetAllModulesAndModuleDescription = dal.GetModuleNamesAndModuleDescription(moduleId);
            ReleaseManagementModel bug;
            foreach (DataRow row in dsGetAllModulesAndModuleDescription.Tables[0].Rows)
            {
                bug = new ReleaseManagementModel();
                bug.ModuleId = row[0].ToString();
                bug.ModuleName = row[1].ToString();
                bug.ModuleDescription = row[2].ToString();
                Modules.Add(bug);
            }
            return Modules;
        }


        //Update BugStatus To Tester//
        public bool UpdateBugStatusToTester(string moduleId)
        {
            string bugStatus = "Bug Fixed";
            return dal.UpdateBugStatusToTester(moduleId,bugStatus);
        }
        //Get BugStatus From Tester//

        
        //tester
        public List<ReleaseManagementModel> GetAllTesterProjects(string username)
        {
            return dal.GetAllTesterProjects(username);

        }
        public List<ReleaseManagementModel> GetAllTesterModules(string P_id,string username)
        {
            return dal.GetAllTesterModules(P_id,username);
        }
        public bool UpdateModuleStatusByTester(string module_Id)
        {
            return dal.UpdateModuleStatusByTester(module_Id);
        }
        //tester2
        public List<ReleaseManagementModel> bugFixedModuleData(string tname)
        {
            return dal.bugFixedModuleData(tname);
        }
        public ReleaseManagementModel GetBugFixedModule(string moduleId)
        {
            
            DataSet dsGetBugFixedModules = dal.GetBugFixedModules(moduleId);
            ReleaseManagementModel bugFixedModule = new ReleaseManagementModel();
            foreach (DataRow row in dsGetBugFixedModules.Tables[0].Rows)
            {
                bugFixedModule.ModuleId = row[0].ToString();
                bugFixedModule.ModuleName = row[1].ToString();
                bugFixedModule.ModuleDescription = row[2].ToString();
                bugFixedModule.ModuleStatus = row[3].ToString();
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
                project.ProjectName = row[1].ToString();
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
                employee.EmployeeId = row[1].ToString();
                employee.EmployeeName = row[2].ToString();
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
                    employee.EmployeeId = allEmployees[i].EmployeeId;
                    Employees.Add(employee);
                }
                
            }
            if (Employees.Count != 0)
                return Employees;
            else
                return allEmployees;

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
            manager.ModuleStatus = "In Progress(D)";
            bool modStatus = false;
            bool insertRoleStatus = false;
            string employeeId = dal.GetEmployeeIdForName(manager.EmployeeName);
            manager.EmployeeId = employeeId;
            bool assignedStatus = dal.AssignModuleToEmployee(manager);
            if (assignedStatus == true)
            {
                modStatus = dal.UpdateModuleStatus(manager.ModuleId, manager.ModuleStatus);
            }
            else
            {
                modStatus = false;
            }
            string role = "Developer";
            if (modStatus == true)
            {
                insertRoleStatus = dal.InsertRole(manager.EmployeeId, role, manager.ModuleId);
            }
            return insertRoleStatus;

        }
        public bool AssignModuleToTester(ReleaseManagementModel manager)
        {
            
             manager.ModuleStatus = "In Progress(T)";           
            bool modStatus = false;
            bool insertRoleStatus = false;
            string empId = dal.GetEmployeeIdForName(manager.EmployeeName);
            manager.EmployeeId = empId;
            bool assignedStatus = dal.AssignModuleToEmployee(manager);
            if (assignedStatus == true)
            {
                modStatus = dal.UpdateModuleStatus(manager.ModuleId,manager.ModuleStatus);
            }
            else
            {
                modStatus = false;
            }
            string role = "Tester";           
            if (modStatus == true)
            {
                insertRoleStatus = dal.InsertRole(manager.EmployeeId,role,manager.ModuleId);
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
            DataSet dsGetprojectId = dal.GetProjectDetails(username);
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
        public bool UpdateCompletedModuleStatus(string projectId)
        {
            return dal.UpdateCompletedmoduleStatus(projectId);
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
            int count = dal.GetProjectCount(projectName);
            string projectId = "";

            bool status = false;
            if (count == 0)
            {
                status = dal.InsertProjects(manager);
            }
            string employeeId = dal.GetEmployeeId(username);
            if (status == true)
            {
                projectId = dal.GetProjectIdForManager(projectName);
            }
            string role = "Manager";
            return dal.InsertRoles(employeeId, role, projectId);

        }
        public DateTime GetProjectDate(string projectId)
        {
            return dal.GetProjectDate(projectId);
        }
        public bool InsertModuleDetails(string projectName, ReleaseManagementModel[] manager)
        {
            bool inserted = false;
            string projectId = dal.GetProjectIdForManager(projectName);
            int count = 0;
            foreach (var item in manager)
            {
                item.ProjectId = projectId;
                item.ModuleStatus = "Not Started";
                count = dal.GetModuleCountToInsert(item.ModuleName);
                if (count == 0)
                {
                    inserted = dal.InsertProjectModules(item);
                }
            }
            return inserted;
        }

        public List<ReleaseManagementModel> GetAllProjects(string username)
        {
            return dal.GetAllProjects(username);
        }
        public List<ReleaseManagementModel> GetAllModules(string project_Id)
        {
            return dal.GetAllModules(project_Id);
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

        public List<ReleaseManagementModel> GetAllEmps()
        {
            return dal.GetAllEmployeesForAdmin();
        }
        public bool InsertEmpDetails(ReleaseManagementModel[] admin)
        {
            bool inserted = false;
            foreach(var item in admin)
            {
                inserted = dal.InsertEmployee(item);
            }

            return inserted;

        }

        public bool UpdateEmpDetails(ReleaseManagementModel admin)
        {
            bool updated = false;
            
            updated = dal.UpdateEmployee(admin);


            return updated;

        }
    }
}
