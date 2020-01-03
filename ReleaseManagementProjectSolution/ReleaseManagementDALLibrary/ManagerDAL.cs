using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using ReleaseManagementProjectLibrary;
namespace ReleaseManagementDALLibrary
{
    public class ManagerDAL
    {
        SqlConnection conn;
        SqlCommand cmdcheckadmin, cmdcheckuser, cmdGetAllAssignedModules, cmdGetAllEmployees, cmdGetAllAssignedEmployees, cmdAssignModuleToEmployee,
            cmdGetAllModuleDetails, cmdUpdatemoduleStatus, cmdGetAllDeveloperName, cmdGetAllTesterName, cmdGetAllCompletedModules,
            cmdGetEmployeesWithRole, cmdInsertRole, cmdGetProjectId, cmdGetEmployeeName,
            cmdGetModuleCount, cmdGetCompletedModuleCount, cmdUpdateCompletedmoduleStatus, cmdGetCompletedModulesCount,
            cmdGetAllCompletedProject, cmdGetEmployeeIdForName, cmdGetProjectDateForManager,cmdGetProjectcount,
            cmdGetModuleCountToInsert,
            //developer1
            cmdGetAllProjects, cmdGetAllModules, cmdUpdatemoduleStatusForManager,
            //developer2
            cmdGetAllModuleNamesAndBugNames, cmdGetModuleNamesAndModuleDescription,
            cmdUpdatemoduleStatusAfterBugFixed,
            //tester1
            cmdGetAllTestProjects, cmdGetAllTestModules, cmdUpdatemoduleStatusByTester,
            //tester2
            cmdFixModuleData, cmdCreateBug, cmdGetBugFixedModules,
            //ManagerFirstPart
            cmdInsertProjects, cmdInsertRoles, cmdInsertModules, cmdGetEmployeeId, cmdGetAllProjectsForManager,
            cmdGetAllModulesForManager, cmdGetProjectIdForManager,
            //mail
            cmdGetMail,
            //cmdUpdatePass
            cmdUpdatepass,
        //Admin
        cmdInsertEmp, cmdGetEmail, cmdGetEmps, cmdUpdateEmps;

        SqlDataAdapter daGetAllAssignedModules, daGetAllEmployees, daGetAllAssignedEmployees, daGetAllCompletedModules,
            daGetAllCompletedProject,
            daGetAllModuleDetails, daGetEmployeesWithRole, daGetAllDeveloperName, daGetAllTesterName, daGetProjectId,
            daGetProject, daGetModules,
            daGetModulesandBugs, daGetModuleNamesAndDescription,daGetBugFixedModules;
            

        public ManagerDAL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conUser"].ConnectionString);

        }
        public bool checkadminlogin(string username, string password)
        {
            bool check = false;

            cmdcheckadmin = new SqlCommand("proc_Adminlogin", conn);
            cmdcheckadmin.Parameters.Add("@un", SqlDbType.VarChar, 20);
            cmdcheckadmin.Parameters.Add("@pass", SqlDbType.VarChar, 20);
            cmdcheckadmin.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmdcheckadmin.Parameters[0].Value = username;
            cmdcheckadmin.Parameters[1].Value = password;

            SqlDataReader dataReader = cmdcheckadmin.ExecuteReader();

            if (dataReader.HasRows == true)
            {
                check = true;
                conn.Close();



            }
            return check;
        }
        public bool checkuserlogin(string username, string password)
        {
            bool check = false;

            cmdcheckuser = new SqlCommand("proc_Userlogin", conn);
            cmdcheckuser.Parameters.Add("@user", SqlDbType.VarChar, 20);
            cmdcheckuser.Parameters.Add("@pwd", SqlDbType.VarChar, 20);

            cmdcheckuser.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmdcheckuser.Parameters[0].Value = username;
            cmdcheckuser.Parameters[1].Value = password;



            SqlDataReader dataReader = cmdcheckuser.ExecuteReader();

            if (dataReader.HasRows == true)
            {
                check = true;
                conn.Close();




            }
            return check;
        }
        //Get All projects for a particular employeeid after login
        public DataSet GetAllProjectForDeveloperFromManager(string username)
        {
            cmdGetAllProjects = new SqlCommand("Proc_DeveloperProjectDetails", conn);
            cmdGetAllProjects.Parameters.AddWithValue("@userName", username);
            DataSet dsGetProject = new DataSet();
            daGetProject = new SqlDataAdapter(cmdGetAllProjects);
            daGetProject.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetProject.Fill(dsGetProject);
            return dsGetProject;
        }
        //Get all modules for a particular project assigned by Manager
        public DataSet GetAllmodulesForDeveloper(string projectId,string username)
        {
            cmdGetAllModules = new SqlCommand("Proc_GetModulesForDeveloper", conn);
            DataSet dsGetModules = new DataSet();
            cmdGetAllModules.Parameters.AddWithValue("@P_Id", projectId);
            cmdGetAllModules.Parameters.AddWithValue("@username", username);
            daGetModules = new SqlDataAdapter(cmdGetAllModules);
            daGetModules.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetModules.Fill(dsGetModules);
            return dsGetModules;
        }
        //update status from developer to manager
        public bool UpdateModuleStatustoManager(string module_Id)
        {
            bool updatedModuleStatus = false;
            cmdUpdatemoduleStatusForManager = new SqlCommand("proc_UpdateModuleStatusToManagerFromDeveloper", conn);
            cmdUpdatemoduleStatusForManager.Parameters.AddWithValue("@module_Id", module_Id);
            //cmdUpdatemoduleStatus.Parameters.AddWithValue("@module_status", developer.Module_Status1);
            cmdUpdatemoduleStatusForManager.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            if (cmdUpdatemoduleStatusForManager.ExecuteNonQuery() > 0)
            {
                updatedModuleStatus = true;
            }
            return updatedModuleStatus;
        }

        //Get AllModuleNameAndBugNameAndBugStatus //
        public DataSet GetAllModuleNamesAndBugNames(string username)
        {
            cmdGetAllModuleNamesAndBugNames = new SqlCommand("proc_GetModuleNamesandBugstatusandBugnames", conn);
            cmdGetAllModuleNamesAndBugNames.Parameters.AddWithValue("@username", username);
            DataSet dsGetModuleNamesAndBugNames = new DataSet();
            daGetModulesandBugs = new SqlDataAdapter(cmdGetAllModuleNamesAndBugNames);
            daGetModulesandBugs.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetModulesandBugs.Fill(dsGetModuleNamesAndBugNames);
            return dsGetModuleNamesAndBugNames;
        }
        //Get AllModuleNamesAndModuledescription//
        public DataSet GetModuleNamesAndModuleDescription(string moduleId)
        {
            cmdGetModuleNamesAndModuleDescription = new SqlCommand("proc_GetAllBugModules", conn);
            cmdGetModuleNamesAndModuleDescription.Parameters.AddWithValue("@moduleId", moduleId);
            DataSet dsGetModuleNamesAndModuleDescription = new DataSet();
            daGetModuleNamesAndDescription = new SqlDataAdapter(cmdGetModuleNamesAndModuleDescription);
            daGetModuleNamesAndDescription.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetModuleNamesAndDescription.Fill(dsGetModuleNamesAndModuleDescription);
            return dsGetModuleNamesAndModuleDescription;

        }

        //Update BugStatus to the Tester//
        public bool UpdateBugStatusToTester(string moduleId, string bugStatus)
        {
            bool updatedBugStatus = false;
            cmdUpdatemoduleStatusAfterBugFixed = new SqlCommand("proc_UpdateBugStatusToTester", conn);
            cmdUpdatemoduleStatusAfterBugFixed.Parameters.AddWithValue("@moduleId", moduleId);
            cmdUpdatemoduleStatusAfterBugFixed.Parameters.AddWithValue("@bugStatus", bugStatus);
            cmdUpdatemoduleStatusAfterBugFixed .CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            if (cmdUpdatemoduleStatusAfterBugFixed.ExecuteNonQuery() > 0)
            {
                updatedBugStatus = true;
            }
            return updatedBugStatus;
        }
      
        //Tester1
        public List<ReleaseManagementModel> GetAllTesterProjects(string userName)
        {
            ReleaseManagementModel tests = null;
            List<ReleaseManagementModel> testers = new List<ReleaseManagementModel>();
            cmdGetAllTestProjects = new SqlCommand("Proc_GetProjectDetailsForTester", conn);
            cmdGetAllTestProjects.Parameters.AddWithValue("@UserName", userName);
            cmdGetAllTestProjects.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader drprojects = cmdGetAllTestProjects.ExecuteReader();
            while (drprojects.Read())
            {
                tests = new ReleaseManagementModel();
                tests.ProjectId = drprojects[0].ToString();
                tests.ProjectName = drprojects[1].ToString();
                tests.ProjectDescription = drprojects[2].ToString();
                tests.ProjectStartDate =Convert.ToDateTime( drprojects[3].ToString());
                tests.ProjectEndDate = Convert.ToDateTime(drprojects[4].ToString());
                testers.Add(tests);
            }
            return testers;
        }
        public List<ReleaseManagementModel> GetAllTesterModules(string P_Id,string username)
        {
            ReleaseManagementModel tests = null;
            List<ReleaseManagementModel> testers = new List<ReleaseManagementModel>();
            cmdGetAllTestModules = new SqlCommand("Proc_GetModulesForTester", conn);
            cmdGetAllTestModules.Parameters.AddWithValue("@p_Id", P_Id);
            cmdGetAllTestModules.Parameters.AddWithValue("@username", username);
            cmdGetAllTestModules.CommandType = CommandType.StoredProcedure;
            conn.Open();
            SqlDataReader drmodules = cmdGetAllTestModules.ExecuteReader();
            while (drmodules.Read())
            {
                tests = new ReleaseManagementModel();
                tests.ModuleId = drmodules[0].ToString();
                tests.ModuleName = drmodules[1].ToString();
                tests.ModuleDescription = drmodules[2].ToString();
                tests.ModuleStatus = drmodules[3].ToString();
                tests.ModuleStartDate = Convert.ToDateTime(drmodules[4].ToString());
                tests.ModuleEndDate = Convert.ToDateTime(drmodules[5].ToString());
                testers.Add(tests);
            }
            return testers;
        }
        public bool UpdateModuleStatusByTester(string module_id)
        {
            bool updatedModuleStatus = false;
            cmdUpdatemoduleStatusByTester = new SqlCommand("proc_UpdateModuleStatusByTester", conn);
            cmdUpdatemoduleStatusByTester.Parameters.AddWithValue("@module_Id", module_id);
            cmdUpdatemoduleStatusByTester.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            if (cmdUpdatemoduleStatusByTester.ExecuteNonQuery() > 0)
            {
                updatedModuleStatus = true;
            }
            return updatedModuleStatus;
        }
        //tester2
        public List<ReleaseManagementModel> bugFixedModuleData(string username)
        {
            cmdFixModuleData = new SqlCommand("testerBugFixModule", conn);
            cmdFixModuleData.Parameters.AddWithValue("@username", username);
            cmdFixModuleData.CommandType = CommandType.StoredProcedure;

            List<ReleaseManagementModel> bFMMs = new List<ReleaseManagementModel>();

            conn.Open();

            SqlDataReader drBFMM = cmdFixModuleData.ExecuteReader();

            ReleaseManagementModel bFMM = null;

            while (drBFMM.Read())
            {
                bFMM = new ReleaseManagementModel();
                bFMM.ModuleId = drBFMM[0].ToString();
                bFMM.ModuleName = drBFMM[1].ToString();
                bFMM.Bugname = drBFMM[2].ToString();
                bFMM.Bugstatus = drBFMM[3].ToString();
                bFMMs.Add(bFMM);

            }
            conn.Close();
            return bFMMs;
        }
        public DataSet GetBugFixedModules(string moduleId)
        {
            cmdGetBugFixedModules = new SqlCommand("proc_testerBugModules", conn);
            DataSet dsGetBugFixedModules = new DataSet();
            cmdGetBugFixedModules.Parameters.AddWithValue("@moduleId",moduleId);
            daGetBugFixedModules = new SqlDataAdapter(cmdGetBugFixedModules);
            daGetBugFixedModules.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetBugFixedModules.Fill(dsGetBugFixedModules);
            return dsGetBugFixedModules;
        }

        public ReleaseManagementModel testerCreateBug(ReleaseManagementModel tCB)
        {
            cmdCreateBug = new SqlCommand("proc_createBug", conn);
            cmdCreateBug.CommandType = CommandType.StoredProcedure;
            cmdCreateBug.Parameters.AddWithValue("@module_Id", tCB.ModuleId);
            cmdCreateBug.Parameters.AddWithValue("@bug_name", tCB.Bugname);
            cmdCreateBug.Parameters.AddWithValue("@bug_status", tCB.Bugstatus);
            conn.Open();
            if (cmdCreateBug.ExecuteNonQuery() > 0)
            {
                return tCB;
            }


            return tCB;
        }


        //Manager
        //Get all projects with managerid

        public DataSet GetAllAssignedModules(string projectId)
        {
            cmdGetAllAssignedModules = new SqlCommand("proc_GetAllAssignedModulesForManager", conn);
            DataSet dsGetAllAssignedModules = new DataSet();
            cmdGetAllAssignedModules.Parameters.AddWithValue("@projectId",projectId);
            daGetAllAssignedModules = new SqlDataAdapter(cmdGetAllAssignedModules);
            daGetAllAssignedModules.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetAllAssignedModules.Fill(dsGetAllAssignedModules);
            return dsGetAllAssignedModules;
        }
        public DataSet GetAllEmployeesWithRole()
        {
            cmdGetEmployeesWithRole = new SqlCommand("proc_GetEmployeesWithRole", conn);
            DataSet dsGetEmployeeswithRole = new DataSet();
            daGetEmployeesWithRole = new SqlDataAdapter(cmdGetEmployeesWithRole);
            daGetEmployeesWithRole.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetEmployeesWithRole.Fill(dsGetEmployeeswithRole);
            return dsGetEmployeeswithRole;
        }
        public DataSet GetAllDeveloperName()
        {
            cmdGetAllDeveloperName = new SqlCommand("proc_EmpId", conn);
            DataSet dsGetAllDeveloperName = new DataSet();
            daGetAllDeveloperName = new SqlDataAdapter(cmdGetAllDeveloperName);
            daGetAllDeveloperName.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetAllDeveloperName.Fill(dsGetAllDeveloperName);
            return dsGetAllDeveloperName;
        }
        public DataSet GetAllTesterName()
        {
            cmdGetAllTesterName = new SqlCommand("proc_TesterID", conn);
            DataSet dsGetAllTesterName = new DataSet();
            daGetAllTesterName = new SqlDataAdapter(cmdGetAllTesterName);
            daGetAllTesterName.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetAllTesterName.Fill(dsGetAllTesterName);
            return dsGetAllTesterName;
        }
        public DataSet GetProjectId(string userName)
        {
            
           
            cmdGetProjectId = new SqlCommand("proc_GetProjectId", conn);
            cmdGetProjectId.Parameters.AddWithValue("@userName", userName);
            DataSet dsGetProjectId = new DataSet();
            cmdGetProjectId.CommandType = CommandType.StoredProcedure;
            daGetProjectId = new SqlDataAdapter(cmdGetProjectId);
            daGetProjectId.Fill(dsGetProjectId);
            return dsGetProjectId;
        }
        public string GetEmployeeName(string employeeId)
        {
            string employeeName = "";
            cmdGetEmployeeName = new SqlCommand("proc_GetEmployeeName", conn);
            cmdGetEmployeeName.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmdGetEmployeeName.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader employee_Name = cmdGetEmployeeName.ExecuteReader();
            while (employee_Name.Read())
            {
                employeeName = employee_Name[0].ToString();
            }
            conn.Close();
            return employeeName;
        }
        public int GetProjectCount(string projectName)
        {
            int projectCount = 0;
            cmdGetProjectcount = new SqlCommand("proc_GetProjectCount", conn);
            cmdGetProjectcount.Parameters.AddWithValue("@projectName", projectName);
            cmdGetProjectcount.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader project_Count = cmdGetProjectcount.ExecuteReader();
            while (project_Count.Read())
            {
                projectCount = Convert.ToInt32(project_Count[0].ToString());
            }
            conn.Close();
            return projectCount;
        }
        public int GetModuleCountToInsert(string moduleName)
        {
            int moduleCount = 0;
            cmdGetModuleCountToInsert = new SqlCommand("proc_GetModuleCountToInsert", conn);
            cmdGetModuleCountToInsert.Parameters.AddWithValue("@moduleName", moduleName);
            cmdGetModuleCountToInsert.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader module_Count = cmdGetModuleCountToInsert.ExecuteReader();
            while (module_Count.Read())
            {
                moduleCount = Convert.ToInt32(module_Count[0].ToString());
            }
            conn.Close();
            return moduleCount;
        }
        public DataSet GetAllEmployees()
        {
            DataSet dsGetAllEmployees = new DataSet();
            cmdGetAllEmployees = new SqlCommand("proc_GetAllEmployees", conn);
            daGetAllEmployees = new SqlDataAdapter(cmdGetAllEmployees);
            daGetAllEmployees.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetAllEmployees.Fill(dsGetAllEmployees);
            return dsGetAllEmployees;
        }
        public DataSet GetAllModuleDetails()
        {
            DataSet dsGetAllModuleDetails = new DataSet();
            cmdGetAllModuleDetails = new SqlCommand("proc_GetAllModuleDetails", conn);
            daGetAllModuleDetails = new SqlDataAdapter(cmdGetAllModuleDetails);
            daGetAllModuleDetails.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetAllModuleDetails.Fill(dsGetAllModuleDetails);
            return dsGetAllModuleDetails;
        }
        public DataSet GetAllAssignedEmployees()
        {

            DataSet dsGetAllAssignedEmployees = new DataSet();
            cmdGetAllAssignedEmployees = new SqlCommand("proc_GetAllAssignedEmployees", conn);
            daGetAllAssignedEmployees = new SqlDataAdapter(cmdGetAllAssignedEmployees);
            daGetAllAssignedEmployees.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetAllAssignedEmployees.Fill(dsGetAllAssignedEmployees);
            return dsGetAllAssignedEmployees;

        }
        public bool AssignModuleToEmployee(ReleaseManagementModel manager)
        {
            bool assignModuleToDeveloperStatus = false;
            cmdAssignModuleToEmployee = new SqlCommand("proc_AssignProjecttoEmployee", conn);
            cmdAssignModuleToEmployee.Parameters.AddWithValue("@ModuleId", manager.ModuleId);
            cmdAssignModuleToEmployee.Parameters.AddWithValue("@EmployeeId", manager.EmployeeId);
            cmdAssignModuleToEmployee.Parameters.AddWithValue("@EmpstartDate", manager.EmpStartDate);
            cmdAssignModuleToEmployee.Parameters.AddWithValue("@EmpEndDate", manager.EmpEndDate);
            cmdAssignModuleToEmployee.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            if (cmdAssignModuleToEmployee.ExecuteNonQuery() > 0)
            {
                assignModuleToDeveloperStatus = true;
            }
            conn.Close();
            return assignModuleToDeveloperStatus;
        }

        public bool UpdateModuleStatus(string moduleId,string moduleStatus)
        {
            bool updatedModuleStatus = false;
            cmdUpdatemoduleStatus = new SqlCommand("proc_UpdateModuleStatus", conn);
            cmdUpdatemoduleStatus.Parameters.AddWithValue("@moduleId",moduleId);
            cmdUpdatemoduleStatus.Parameters.AddWithValue("@moduleStatus",moduleStatus);
            cmdUpdatemoduleStatus.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            if (cmdUpdatemoduleStatus.ExecuteNonQuery() > 0)
            {
                updatedModuleStatus = true;
            }
            conn.Close();
            return updatedModuleStatus;
        }

        public bool InsertRole(string empId,string role,string projectId)
        {
            bool insertRoleStatus = false;
            cmdInsertRole = new SqlCommand("proc_InsertRole", conn);
            cmdInsertRole.Parameters.AddWithValue("@EmpId", empId);
            cmdInsertRole.Parameters.AddWithValue("@Role", role);
            cmdInsertRole.Parameters.AddWithValue("@projectId", projectId);
            cmdInsertRole.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            if (cmdInsertRole.ExecuteNonQuery() > 0)
            {
                insertRoleStatus = true;
            }
            conn.Close();
            return insertRoleStatus;


        }
        public DataSet GetProjectDetails(string userName)
        {


            cmdGetProjectId = new SqlCommand("proc_GetProjectId", conn);
            cmdGetProjectId.Parameters.AddWithValue("@userName", userName);
            DataSet dsGetProjectId = new DataSet();
            cmdGetProjectId.CommandType = CommandType.StoredProcedure;
            daGetProjectId = new SqlDataAdapter(cmdGetProjectId);
            daGetProjectId.Fill(dsGetProjectId);
            return dsGetProjectId;
        }
        public DataSet GetAllCompletedModules(string projectId)
        {
            cmdGetAllCompletedModules = new SqlCommand("proc_GetCompletedModules", conn);
            DataSet dsGetAllCompletedModules = new DataSet();
            cmdGetAllCompletedModules.Parameters.AddWithValue("@projectId", projectId);
            daGetAllCompletedModules = new SqlDataAdapter(cmdGetAllCompletedModules);
            daGetAllCompletedModules.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetAllCompletedModules.Fill(dsGetAllCompletedModules);
            return dsGetAllCompletedModules;
        }
        public int GetModuleCount(string projectid)
        {
            int moduleCount = 0;
            cmdGetModuleCount = new SqlCommand("proc_GetModuleCount", conn);
            cmdGetModuleCount.Parameters.AddWithValue("@projectId", projectid);
            cmdGetModuleCount.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader module_Count = cmdGetModuleCount.ExecuteReader();
            while (module_Count.Read())
            {
                moduleCount =  Convert.ToInt32(module_Count[0].ToString());
            }
            conn.Close();
            return moduleCount;

        }
        public int GetCompletedModuleCount(string projectid)
        {
            int moduleCount = 0;
            cmdGetCompletedModuleCount = new SqlCommand("proc_GetCompletedmodulesCount", conn);
            cmdGetCompletedModuleCount.Parameters.AddWithValue("@projectId", projectid);
            cmdGetCompletedModuleCount.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader module_Count = cmdGetCompletedModuleCount.ExecuteReader();
            while (module_Count.Read())
            {
                moduleCount = Convert.ToInt32(module_Count[0].ToString());
            }
            conn.Close();
            return moduleCount;

        }
        public bool UpdateCompletedmoduleStatus(string projectId)
        {
            bool updatedModuleStatus = false;
            cmdUpdateCompletedmoduleStatus = new SqlCommand("proc_UpdateCompletedModuleStatus", conn);
            cmdUpdateCompletedmoduleStatus.Parameters.AddWithValue("@projectId", projectId);
            cmdUpdateCompletedmoduleStatus.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            if (cmdUpdateCompletedmoduleStatus.ExecuteNonQuery() > 0)
            {
                updatedModuleStatus = true;
            }
            conn.Close();
            return updatedModuleStatus;

        }
        public int GetCompletedModulesCount(string projectid)
        {
            int moduleCount = 0;
            cmdGetCompletedModulesCount = new SqlCommand("proc_GetCompletedModuleCount", conn);
            cmdGetCompletedModulesCount.Parameters.AddWithValue("@projectId", projectid);
            cmdGetCompletedModulesCount.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader module_Count = cmdGetCompletedModulesCount.ExecuteReader();
            while (module_Count.Read())
            {
                moduleCount = Convert.ToInt32(module_Count[0].ToString());
            }
            conn.Close();
            return moduleCount;

        }
        public DataSet GetCompletedProject(string projectId)
        {
            DataSet dsGetAllCompletedProject = new DataSet();
            cmdGetAllCompletedProject = new SqlCommand("proc_GetCompletedProjects", conn);
            cmdGetAllCompletedProject.Parameters.AddWithValue("@projectId", projectId);
            daGetAllCompletedProject = new SqlDataAdapter(cmdGetAllCompletedProject);
            daGetAllCompletedProject.SelectCommand.CommandType = CommandType.StoredProcedure;
            daGetAllCompletedProject.Fill(dsGetAllCompletedProject);
            return dsGetAllCompletedProject;

        }
        //Manager First part
        public bool InsertProjects(ReleaseManagementModel manager)
        {
            bool _inserted = false;
            cmdInsertProjects = new SqlCommand("proc_InsertProjects", conn);
            cmdInsertProjects.Parameters.Add("@projectname", SqlDbType.VarChar, 40);
            cmdInsertProjects.Parameters.Add("@projectdescription", SqlDbType.VarChar, 70);
            cmdInsertProjects.Parameters.Add("@fromdate", SqlDbType.DateTime);
            cmdInsertProjects.Parameters.Add("@todate", SqlDbType.DateTime);
            cmdInsertProjects.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmdInsertProjects.Parameters[0].Value = manager.ProjectName;
            cmdInsertProjects.Parameters[1].Value = manager.ProjectDescription;
            cmdInsertProjects.Parameters[2].Value = manager.ProjectStartDate;
            cmdInsertProjects.Parameters[3].Value = manager.ProjectEndDate;
            if (cmdInsertProjects.ExecuteNonQuery() > 0)
                _inserted = true;
            else
                _inserted = false;
            conn.Close();
            return _inserted;

        }
        //Insert Roles
        public bool InsertRoles(string employeeId, string role, string projectId)
        {
            bool _insertedrole = false;
            cmdInsertRoles = new SqlCommand("proc_InsertRoleForManager", conn);
            cmdInsertRoles.Parameters.Add("@empid", SqlDbType.VarChar, 20);
            cmdInsertRoles.Parameters.Add("@role", SqlDbType.VarChar, 40);
            cmdInsertRoles.Parameters.Add("@proj_id", SqlDbType.VarChar, 4);
            cmdInsertRoles.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            cmdInsertRoles.Parameters[0].Value = employeeId;
            cmdInsertRoles.Parameters[1].Value = role;
            cmdInsertRoles.Parameters[2].Value = projectId;
            if (cmdInsertRoles.ExecuteNonQuery() > 0)
                _insertedrole = true;
            else
                _insertedrole = false;
            conn.Close();
            return _insertedrole;
        }
        //Get Project Date
        public DateTime GetProjectDate(string projectId)
        {
            cmdGetProjectDateForManager = new SqlCommand("proc_GetProjectDate", conn);
            cmdGetProjectDateForManager.Parameters.AddWithValue("@projectId", projectId);
            cmdGetProjectDateForManager.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            DateTime projDate = DateTime.Now;
            SqlDataReader drprojid = cmdGetProjectDateForManager.ExecuteReader();
            while (drprojid.Read())
            {
                projDate = Convert.ToDateTime(drprojid[0].ToString());
            }
            conn.Close();

            return projDate;

        }
        //Insert modules
        public bool InsertProjectModules(ReleaseManagementModel manager)
        {
            bool _insertedmodule = false;
            cmdInsertModules = new SqlCommand("proc_InsertModules", conn);
            cmdInsertModules.Parameters.Add("@projectid", SqlDbType.VarChar, 10);
            cmdInsertModules.Parameters.Add("@module_name", SqlDbType.VarChar, 20);
            cmdInsertModules.Parameters.Add("@module_description", SqlDbType.VarChar, 70);
            cmdInsertModules.Parameters.Add("@module_status", SqlDbType.VarChar, 40);
            cmdInsertModules.Parameters.Add("@from_date", SqlDbType.DateTime);
            cmdInsertModules.Parameters.Add("@to_date", SqlDbType.DateTime);
            cmdInsertModules.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open(); 
            cmdInsertModules.Parameters[0].Value = manager.ProjectId;
            cmdInsertModules.Parameters[1].Value = manager.ModuleName;
            cmdInsertModules.Parameters[2].Value = manager.ModuleDescription;
            cmdInsertModules.Parameters[3].Value = manager.ModuleStatus;
            cmdInsertModules.Parameters[4].Value = manager.ModuleStartDate;
            cmdInsertModules.Parameters[5].Value = manager.ModuleEndDate;
            if (cmdInsertModules.ExecuteNonQuery() > 0)
                _insertedmodule = true;
            else
                _insertedmodule = false;
            conn.Close();
            return _insertedmodule;
        }
        //Get Employee Id 
        public string GetEmployeeId(string username)
        {
            cmdGetEmployeeId = new SqlCommand("proc_GetEmployeeId", conn);
            cmdGetEmployeeId.Parameters.AddWithValue("@username", username);
            cmdGetEmployeeId.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string empId = "";
            SqlDataReader drempid = cmdGetEmployeeId.ExecuteReader();
            while (drempid.Read())
            {
                empId = drempid[0].ToString();

            }
            conn.Close();
            return empId;
        }
        public string GetEmployeeIdForName(string empname)
        {
            cmdGetEmployeeIdForName = new SqlCommand("proc_GetEmployeeIdForName", conn);
            cmdGetEmployeeIdForName.Parameters.AddWithValue("@empname", empname);
            cmdGetEmployeeIdForName.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string empId = "";
            SqlDataReader drempid = cmdGetEmployeeIdForName.ExecuteReader();
            while (drempid.Read())
            {
                empId = drempid[0].ToString();

            }
            conn.Close();
            return empId;
        }
        //Get Project Id
        public string GetProjectIdForManager(string projectname)
        {
            cmdGetProjectIdForManager = new SqlCommand("proc_GetProjectsId", conn);
            cmdGetProjectIdForManager.Parameters.AddWithValue("@projectname", projectname);
            cmdGetProjectIdForManager.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            string projId = "";
            SqlDataReader drprojid = cmdGetProjectIdForManager.ExecuteReader();
            while (drprojid.Read())
            {
                projId = drprojid[0].ToString();
            }
            conn.Close();

            return projId;

        }
        //Get All Projects
        public List<ReleaseManagementModel> GetAllProjects(string username)
        {
            cmdGetAllProjectsForManager = new SqlCommand("proc_GetAllProjects", conn);
            cmdGetAllProjectsForManager.Parameters.AddWithValue("@username", username);
            cmdGetAllProjectsForManager.CommandType = CommandType.StoredProcedure;
            List<ReleaseManagementModel> managers = new List<ReleaseManagementModel>();
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader drmanagers = cmdGetAllProjectsForManager.ExecuteReader();
           
            ReleaseManagementModel manager = null;
            while (drmanagers.Read())
            {
                manager = new ReleaseManagementModel();
                manager.ProjectId = drmanagers[0].ToString();
                manager.ProjectName = drmanagers[1].ToString();
                manager.ProjectDescription = drmanagers[2].ToString();
                manager.ProjectStartDate = Convert.ToDateTime(drmanagers[3]);
                manager.ProjectEndDate = Convert.ToDateTime(drmanagers[4]);
                managers.Add(manager);
            }
            conn.Close();
            return managers;
        }
        //Get All Modules
        public List<ReleaseManagementModel> GetAllModules(string projectId)
        {
            cmdGetAllModulesForManager = new SqlCommand("proc_GetAllModules", conn);
            cmdGetAllModulesForManager.Parameters.AddWithValue("@projectId", projectId);
            cmdGetAllModulesForManager.CommandType = CommandType.StoredProcedure;
            List<ReleaseManagementModel> managers = new List<ReleaseManagementModel>();
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader drmodules = cmdGetAllModulesForManager.ExecuteReader();
           
            ReleaseManagementModel manager = null;
            while (drmodules.Read())
            {
                manager = new ReleaseManagementModel();
                manager.ModuleId = drmodules[0].ToString();
                manager.ModuleName = drmodules[1].ToString();
                manager.ModuleDescription = drmodules[2].ToString();
                manager.ModuleStatus = drmodules[3].ToString();
                manager.ModuleStartDate = Convert.ToDateTime(drmodules[4]);
                manager.ModuleEndDate = Convert.ToDateTime(drmodules[5]);
                managers.Add(manager);
            }
            conn.Close();
            return managers;
        }
        public string GetEmail(string username)
        {
            string email = "";
            cmdGetMail = new SqlCommand("proc_GetEmail", conn);
            cmdGetMail.Parameters.Add("@uname", SqlDbType.VarChar, 20);
            cmdGetMail.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmdGetMail.Parameters[0].Value = username;
            SqlDataReader drUsers = cmdGetMail.ExecuteReader();
            if (drUsers.HasRows == false)
                return email;
            while (drUsers.Read())
            {
                email = drUsers[0].ToString();
            }
            return email;

        }
        public bool UpdatePassword(string username,string password)
        {
            bool updated = false;
            cmdUpdatepass = new SqlCommand("proc_UpdatePassword", conn);
            cmdUpdatepass.Parameters.Add("@uname", SqlDbType.VarChar, 20);
            cmdUpdatepass.Parameters.Add("@pass", SqlDbType.VarChar, 20);
            cmdUpdatepass.CommandType = CommandType.StoredProcedure;
            conn.Open();
            cmdUpdatepass.Parameters[0].Value = username;
            cmdUpdatepass.Parameters[1].Value = password;
            if (cmdUpdatepass.ExecuteNonQuery() > 0)
                updated = true;
            else
                updated = false;
            conn.Close();
            return updated;

        }

        public List<ReleaseManagementModel> GetAllEmployeesForAdmin()
        {
            cmdGetEmps = new SqlCommand("proc_GetAllEmployeeForAdmin", conn);
            cmdGetEmps.CommandType = CommandType.StoredProcedure;

            List<ReleaseManagementModel> admins = new List<ReleaseManagementModel>();
            OpenConnection();
            SqlDataReader drUsers = cmdGetEmps.ExecuteReader();
           
            ReleaseManagementModel admin = null;
            while (drUsers.Read())
            {
                admin = new ReleaseManagementModel();
                admin.EmployeeName = drUsers[2].ToString();
                admin.Employee_mail = drUsers[3].ToString();
                admins.Add(admin);
            }
            conn.Close();
            return admins;

        }
        public bool InsertEmployee(ReleaseManagementModel admin)
        {
            bool _inserted = false;
            cmdInsertEmp = new SqlCommand("proc_InsertEmployee", conn);
            cmdInsertEmp.Parameters.Add("@emp_name", SqlDbType.VarChar, 30);
            cmdInsertEmp.Parameters.Add("@employee_email", SqlDbType.VarChar, 30);
            cmdInsertEmp.CommandType = CommandType.StoredProcedure;

            OpenConnection();
            cmdInsertEmp.Parameters[0].Value = admin.EmployeeName;
            cmdInsertEmp.Parameters[1].Value = admin.Employee_mail;
            if (cmdInsertEmp.ExecuteNonQuery() > 0)
                _inserted = true;
            else
                _inserted = false;
            conn.Close();

            return _inserted;
        }
        public bool UpdateEmployee(ReleaseManagementModel admin)
        {
            bool updated = false;
            cmdUpdateEmps = new SqlCommand("proc_UpdateEmployee", conn);
            cmdUpdateEmps.Parameters.Add("@emp_id", SqlDbType.VarChar, 4);
            cmdUpdateEmps.Parameters.Add("@emp_name", SqlDbType.VarChar, 30);
            cmdUpdateEmps.Parameters.Add("@employee_email", SqlDbType.VarChar, 30);
            cmdUpdateEmps.CommandType = CommandType.StoredProcedure;

            OpenConnection();
            //doubt
            cmdUpdateEmps.Parameters[0].Value = admin.EmployeeName;
            cmdUpdateEmps.Parameters[1].Value = admin.Employee_mail;
            if (cmdUpdateEmps.ExecuteNonQuery() > 0)

                updated = true;
            else
                updated = false;
            conn.Close();
            return updated;

        }

        void OpenConnection()
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            conn.Open();
        }

    }
}
