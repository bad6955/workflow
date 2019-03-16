using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Data;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow
{
    public partial class Projects : System.Web.UI.Page
    {
        private int count = 0;
        private String projectNode = "";
        //prevents users from using back button to return to login protected pages
        protected override void OnInit(EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.MinValue);

            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //validates that the user is logged in
            if (Session["User"] != null)
            {
                User user = (User)Session["User"];
                userLbl.Text = user.FullName;
                if (user.RoleId == 1)
                {
                    CreateClientProjectList(user.CompanyId);
                    CreateNewProjectBtn.Visible = false;
                }
                else if (user.RoleId == 2)
                {
                    CreateProjectList(user.UserId);
                    CreateNewProjectBtn.Visible = false;
                }
                else if (user.RoleId == 4 || user.RoleId == 3)
                {
                    CreateAdminProjectList();
                    if (user.RoleId == 4)
                    {
                        AdminBtn.Visible = true;
                    }
                }

                //loads the selected form if there is one
                if (Request.QueryString["pid"] != null)
                {
                    projectListing.Visible = false;
                    int projId = int.Parse(Request.QueryString["pid"]);
                    Project p = ProjectUtil.GetProject(projId);


                    //if they are trying to edit and they are admin, show project builder
                    if (Request.QueryString["edit"] != null && user.RoleId == 4)
                    {
                        projectBuilder.Visible = true;
                        GenerateProjectDropdowns();
                        CreateProjectBtn.Text = "Update Project";
                        ProjectName.Text = p.Name;

                        CompanySelect.SelectedValue = p.CompanyId.ToString();
                        CoachSelect.SelectedValue = p.CoachId.ToString();
                        WorkflowSelect.SelectedValue = p.WorkflowId.ToString();
                        ProjectNotes.Text = p.Notes;
                    }
                    //otherwise just show the project viewer
                    else
                    {
                        projectViewer.Visible = true;
                        if(user.RoleId == 4 || user.RoleId == 3)
                        {
                            if (ProjectUtil.CheckProjectCompletion(projId))
                            {
                                ProjectFileDownloader.Visible = true;
                            }
                        }
                        ProjectView(p);
                    }
                }
                //if theyre an admin and trying to make a new project
                else if (Request.QueryString["edit"] != null && Request.QueryString["pid"] == null && user.RoleId == 4)
                {
                    projectListing.Visible = false;
                    projectBuilder.Visible = true;
                    GenerateProjectDropdowns();
                }
            }
            else
            {
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
        }

        protected void ProjectView(Project p)
        {
            projectNode += "<h2>" + p.Name + "</h2><div id=\"project-top-div\"><div class=\"project-info\"><div id=\"project-top\"><div class=\"project-item\">";
            projectNode += "<i class=\"huge circular building icon\"></i><h3>" + CompanyUtil.GetCompany(p.CompanyId).CompanyName + "</h3></div><div class=\"project-item\">";
            projectNode += "<i class=\"huge circular user icon\"></i><h3>" + UserUtil.GetCoach(p.CoachId).FullName + "</h3></div>";
            projectNode += "<div class=\"project-item\"><i class=\"huge circular money icon\"></i>";
            projectNode += "<h3>Funding Source</h3></div></div></div>";
            projectNode += "<h3 style=\"font-weight:100\">Project Notes:</h3><p>" + p.Notes + "</p></div>";

            projectNode += "<div class=\"wrapper\"><ol class=\"ProgressBar\">";
            try
            {
                foreach (WorkflowComponent com in WorkflowComponentUtil.GetWorkflowComponents(p.WorkflowId))
                {
                    projectNode += "<li class=\"ProgressBar-step\" id=\"li" + com.WFComponentID + "\">";
                    Form form = FormUtil.GetForm(com.FormID);
                        projectNode += "<svg class=\"ProgressBar-icon\"></svg><span class=\"ProgressBar-stepLabel\">" + com.ComponentTitle + "</span>";
                            
                    projectNode += "<div class=\"li-dropdown\" id=\"li-drop" + com.WFComponentID + "\">";
                    projectNode += "<div class=\"workflow-form\"><i class=\"big inbox icon\"></i><h3>" + FormUtil.GetForm(com.FormID).FormName + "</h3></div></div></li>";
                }
            } catch(Exception e) { }
            projectNode += "</ol></div>";

            projectViewer.InnerHtml += projectNode;
        }

        private void GenerateProjectDropdowns()
        {
            //fills out company dropdown
            CompanySelect.DataSource = Data.CompanyUtil.GetClientCompanies();
            CompanySelect.DataTextField = "companyName";
            CompanySelect.DataValueField = "companyId";
            CompanySelect.DataBind();
            CompanySelect.SelectedIndex = 0;

            //fills out workflow dropdown
            WorkflowSelect.DataSource = Data.WorkflowUtil.GetWorkflows();
            WorkflowSelect.DataTextField = "workflowName";
            WorkflowSelect.DataValueField = "workflowId";
            WorkflowSelect.DataBind();
            WorkflowSelect.SelectedIndex = 0;

            //fills out company dropdown
            CoachSelect.DataSource = Data.UserUtil.GetCoaches();
            CoachSelect.DataTextField = "fullName";
            CoachSelect.DataValueField = "userId";
            CoachSelect.DataBind();
            CoachSelect.SelectedIndex = 0;

            Panel aster = new Panel();
            aster.CssClass = "ui teal left corner label";
            Literal icon = new Literal();
            icon.Text = "<i class=\"white asterisk icon\"></i>";
            aster.Controls.Add(icon);
            ProjectNamePanel.Controls.Add(aster);
        }

        private void CreateProjectList(int userId)
        {
            projectNode = "";
            projectList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<Project> projects = ProjectUtil.GetCoachProjects(userId);
            for (int i = 0; i < projects.Count && i < 5; i++)
            {
                MakeText(projects, projectNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + projects.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateAdminProjectList()
        {
            projectNode = "";
            projectList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<Project> projects = ProjectUtil.GetProjects();
            for (int i = 0; i < projects.Count && i < 5; i++)
            {
                MakeText(projects, projectNode, i);
                //MakeAdminText(projects, projectNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + projects.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateClientProjectList(int companyId)
        {
            CreateNewProjectBtn.Visible = false;
            projectNode = "";
            projectList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<Project> projects = ProjectUtil.GetCompanyProjects(companyId);
            for (int i = 0; i < projects.Count && i < 5; i++)
            {
                MakeText(projects, projectNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + projects.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        protected void DashboardBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void WorkflowBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx");
        }

        protected void FormBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        protected void ProjectBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        protected void LogoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void AdminBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void CreateProjectBtn_Click(object sender, EventArgs e)
        {
            int companyId = int.Parse(SelectedCompany.Value);
            int workflowId = int.Parse(SelectedWorkflow.Value);
            int coachId = int.Parse(SelectedCoach.Value);
            string projectName = ProjectName.Text;
            string projectNotes = ProjectNotes.Text;

            if (projectName.Length > 0)
            {
                if (companyId != -1)
                {
                    if (workflowId != -1)
                    {
                        if (coachId != -1)
                        {
                            Project p = ProjectUtil.CreateProject(projectName, workflowId, companyId, coachId, projectNotes);
                            User user = (User)Session["User"];
                            Log.Info(user.Identity + " created project " + projectName + " with a Workflow of " + WorkflowUtil.GetWorklowName(workflowId) + " assigned to " + CompanyUtil.GetCompanyName(companyId) + " under Coach " +UserUtil.GetCoachName(coachId) + " with notes: " + projectNotes);
                            Response.Redirect("Projects.aspx?pid=" + p.ProjectId);
                        }
                    }
                }
            }
            else
            {
                //enter valid name
            }
        }

        protected void CreateNewProjectBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx?edit=1");
        }

        protected void LoadMoreProjects(object sender, EventArgs e)
        {
            ViewState["count"] = Convert.ToInt32(ViewState["count"]) + 1;
            int loaded = Convert.ToInt32(ViewState["count"]);

            List<Project> projects = ProjectUtil.GetProjects();
            if (loaded == 1)
            {
                ViewState["count"] = Convert.ToInt32(ViewState["count"]) + 1;
                loaded = Convert.ToInt32(ViewState["count"]);
            }
            for (int i = 5; i < loaded * 5 && i < projects.Count; i++)
            {
                MakeText(projects, projectNode, i);
            }

            numberShowing.InnerHtml = "";
            var showing = "Showing 1 - " + count + " of " + projects.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void MakeText(List<Project> projects, string projectNode, int i)
        {
            User coach = UserUtil.GetCoach(projects[i].CoachId);
            WorkflowModel workflow = WorkflowUtil.GetWorkflow(projects[i].WorkflowId);
            projectNode = "<div class=\"item\"><div class=\"ui small image\"><i class=\"huge inbox icon\"/></i></div>";
            projectNode += "<div class=\"content\"><a class=\"header\" href='Projects.aspx?pid=" + projects[i].ProjectId + "'>" + projects[i].Name + "</a><div class=\"meta\">";
            projectNode += "<span class=\"stay\">" + coach.FullName + " | " + workflow.WorkflowName + "</span></div><div class=\"description\">";
            projectNode += projects[i].Notes + "</div></div></div>";


            projectList.InnerHtml += projectNode;
            projectNode = "";
            count++;
        }

        protected void ProjectFileDownloader_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["pid"] != null)
            {
                int projId = int.Parse(Request.QueryString["pid"]);
                Project p = ProjectUtil.GetProject(projId);
                WorkflowModel w = WorkflowUtil.GetWorkflow(p.WorkflowId);
                List<WorkflowComponent> workflowComponents = WorkflowComponentUtil.GetWorkflowComponents(w.WorkflowId);
                string zipPath = String.Format("{0} - {1} - {2}.zip", w.WorkflowName, p.Name, CompanyUtil.GetCompanyName(p.CompanyId));
                //delete the zip if it exists
                if (File.Exists(zipPath))
                {
                    File.Delete(zipPath);
                }

                using (ZipArchive zip = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    //for each form get the file
                    foreach (WorkflowComponent wc in workflowComponents)
                    {
                        Form f = FormUtil.GetForm(wc.FormID);
                        string pdfName = string.Format("{0} - {1} - {2}.pdf", w.WorkflowName, f.FormName, CompanyUtil.GetCompanyName(p.CompanyId));
                        string pdfPath = string.Format("./PDFGen/{0}", pdfName);
                        zip.CreateEntryFromFile(pdfPath, pdfName);
                    }
                }

                SendFile(zipPath);
            }
        }

        private void SendFile(string path)
        {
            FileInfo f = new FileInfo(path);
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", path));
            Response.AddHeader("Content-Length", f.Length.ToString());
            Response.ContentType = "text/plain";
            Response.Flush();
            Response.TransmitFile(f.FullName);
            Response.End();
        }
    }
}