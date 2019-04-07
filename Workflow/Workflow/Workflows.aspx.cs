using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Data;
using Workflow.Models;
using Workflow.Utility;

namespace Workflow
{
    public partial class Workflows : System.Web.UI.Page
    {
        private int count = 0;
        private String workflowNode = "";
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
                if (user.RoleId == 4)
                {
                    AdminBtn.Visible = true;
                }

                if (user.RoleId == 1)
                {
                    CreateClientWorkflowList(user.CompanyId);
                    CreateNewWorkflowBtn.Visible = false;
                }
                else if (user.RoleId == 2)
                {
                    CreateWorkflowList(user.UserId);
                    CreateNewWorkflowBtn.Visible = false;
                }
                else if (user.RoleId == 4 || user.RoleId == 3)
                {
                    CreateAdminWorkflowList();
                }

                //loads the selected form if there is one
                if (Request.QueryString["wid"] != null)
                {
                    int workflowId = int.Parse(Request.QueryString["wid"]);
                    WorkflowModel w = WorkflowUtil.GetWorkflow(workflowId);

                    //admin and trying to del
                    if (Request.QueryString["del"] != null && user.RoleId == 4)
                    {
                        if (WorkflowUtil.DeleteWorkflow(w.WorkflowId))
                        {
                            Log.Info(user.Identity + " deleted a workflow template " + w.WorkflowName);
                            ReloadSection();
                        }
                        else
                        {
                            WorkflowError.Visible = true;
                            WorkflowError.Text = "Unable to delete Workflow " + w.WorkflowName;
                        }
                    }
                    else
                    {
                        workflowListing.Visible = false;

                        //if they are trying to edit and they are admin, show workflow builder
                        if (Request.QueryString["edit"] != null && user.RoleId == 4)
                        {
                            if (WorkflowUtil.EditableWorkflow(w.WorkflowId))
                            {
                                workflowBuilder.Visible = true;
                                WorkflowName.Text = w.WorkflowName;
                                CreateWorkflowBtn.Text = "Update Workflow";

                                LoadWorkflowSteps(workflowId);
                            }
                            else
                            {
                                workflowListing.Visible = true;
                                WorkflowError.Visible = true;
                                WorkflowError.Text = "Unable to edit Workflow " + w.WorkflowName + " while projects are assigned to it"; 
                            }
                        }
                        //otherwise just show the workflow viewer
                        else
                        {
                            workflowViewer.Visible = true;
                            List<WorkflowComponent> comps = WorkflowComponentUtil.GetWorkflowComponents(w.WorkflowId);
                            ProjectInformation(w, comps);
                        }
                    }
                }
                //if theyre an admin and trying to make a new form
                else if (Request.QueryString["edit"] != null && Request.QueryString["wid"] == null && user.RoleId == 4)
                {
                    workflowListing.Visible = false;
                    workflowBuilder.Visible = true;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void DashboardBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void ProjectBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        protected void FormBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        protected void WorkflowBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx");
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            //FormAuthentication.SignOut(); if we are using the form authenication, then remove the // else remove entirely
            Response.Redirect("Login.aspx");
        }

        protected void AdminBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Admin.aspx");
        }

        protected void LoadMoreWorkflows(object sender, EventArgs e)
        {
            ViewState["workflowcount"] = Convert.ToInt32(ViewState["workflowcount"]) + 1;
            int loaded = Convert.ToInt32(ViewState["workflowcount"]);

            List<WorkflowModel> workflows = new List<WorkflowModel>();
            User user = (User) Session["User"];
            if (user.RoleId == 1)
            {
                workflows = WorkflowUtil.GetCompanyWorkflows(user.CompanyId);
            }
            else if (user.RoleId == 2)
            {
                workflows = WorkflowUtil.GetCoachWorkflows(user.UserId);
            }
            else if (user.RoleId == 4 || user.RoleId == 3)
            {
                workflows = WorkflowUtil.GetWorkflows();
            }
            if (loaded == 1)
            {
                ViewState["workflowcount"] = Convert.ToInt32(ViewState["workflowcount"]) + 1;
                loaded = Convert.ToInt32(ViewState["workflowcount"]);
            }
            for (int i = 5; i < loaded * 5 && i < workflows.Count; i++)
            {
                if(user.RoleId == 4)
                {
                    MakeAdminText(workflows, workflowNode, i);
                }
                else if(user.RoleId == 1)
                {
                    MakeClientText(workflows, workflowNode, i);
                }
                else
                {
                    MakeText(workflows, workflowNode, i);
                }
            }

            numberShowing.InnerHtml = "";
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void MakeClientText(List<WorkflowModel> workflows, String workflowNode, int i)
        {
            workflowNode = "<div class=\"item\"><div class=\"ui small image\"><i class=\"huge sitemap icon\"/></i></div>";
            workflowNode += "<div class=\"content\"><a class=\"header\" href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "'>" + workflows[i].WorkflowName + "</a><div class=\"meta\">";
            workflowNode += "</div></div></div>";
            workflowList.InnerHtml += workflowNode;
            count++;
        }

        private void MakeText(List<WorkflowModel> workflows, String workflowNode, int i)
        {
            workflowNode = "<div class=\"item\"><div class=\"ui small image\"><i class=\"huge sitemap icon\"/></i></div>";
            workflowNode += "<div class=\"content\"><a class=\"header\" href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "'>" + workflows[i].WorkflowName + "</a><div class=\"meta\">";
            workflowNode += "</div></div></div>";
            workflowList.InnerHtml += workflowNode;
            count++;
        }

        private void MakeAdminText(List<WorkflowModel> workflows, String workflowNode, int i)
        {
            workflowNode = "<div class=\"item\"><div class=\"ui small image\"><i class=\"huge sitemap icon\"/></i></div>";
            workflowNode += "<div class=\"content\"><a class=\"header\" href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "'>" + workflows[i].WorkflowName + "</a><div class=\"meta\">";
            workflowNode += "<span class=\"stay\">" + "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "&edit=1'>Edit Workflow</a>" + " | ";
            workflowNode += "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "&del=1'>Delete Workflow</a>" + "</span></div></div></div>";
            workflowList.InnerHtml += workflowNode;
            count++;
        }

        private void ReloadSection()
        {
            Response.Redirect("Workflows.aspx");
        }

        private void ReloadCurrentPage()
        {
            Response.Redirect(Request.RawUrl);
        }

        private void CreateAdminWorkflowList()
        {
            workflowNode = "";
            workflowList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<WorkflowModel> workflows = WorkflowUtil.GetWorkflows();
            for (int i = 0; i < 5 && i < workflows.Count; i++)
            {
                MakeAdminText(workflows, workflowNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateClientWorkflowList(int companyId)
        {
            CreateNewWorkflowBtn.Visible = false;
            workflowNode = "";
            workflowList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<WorkflowModel> workflows = WorkflowUtil.GetCompanyWorkflows(companyId);
            for (int i = 0; i < 5 && i < workflows.Count; i++)
            {
                MakeClientText(workflows, workflowNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateWorkflowList(int userId)
        {
            workflowNode = "";
            workflowList.InnerHtml = "";
            numberShowing.InnerHtml = "";
            List<WorkflowModel> workflows = WorkflowUtil.GetCoachWorkflows(userId);
            for (int i = 0; i < 5 && i < workflows.Count; i++)
            {
                MakeText(workflows, workflowNode, i);
            }
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        protected void CreateNewWorkflowBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx?edit=1");
        }

        // ======= WORKFLOW EDITOR ======
        private void LoadWorkflowSteps(int workflowId)
        {
            List<Guid> ids = this.ControlIDs;
            List<WorkflowComponent> compList = WorkflowComponentUtil.GetWorkflowComponents(workflowId);
            int i = 0;
            foreach (WorkflowComponent item in compList)
            {
                if (ids.Count == i)
                {
                    Guid guid = Guid.NewGuid();
                    ids.Add(guid);
                    this.ControlIDs = ids;
                }
                CreateWorkflowStep(ids[i], item);
                i++;
            }
        }

        protected void CreateWorkflowBtn_Click(object sender, EventArgs e)
        {
            //create new
            if (WorkflowName.Text.Length > 0 && Request.QueryString["wid"] == null)
            {
                WorkflowModel w = WorkflowUtil.CreateWorkflow(WorkflowName.Text);
                User user = (User)Session["User"];
                Log.Info(user.Identity + " created a new workflow template " + w.WorkflowName);
                SaveComponents(w.WorkflowId);
            }
            //update existing
            else if (WorkflowName.Text.Length > 0 && Request.QueryString["wid"] != null)
            {
                int workflowId = int.Parse(Request.QueryString["wid"]);
                WorkflowUtil.UpdateWorkflow(workflowId, WorkflowName.Text);

                SaveComponents(workflowId);
            }
        }

        private void SaveComponents(int workflowId)
        {
            User user = (User)Session["User"];
            WorkflowModel w = WorkflowUtil.GetWorkflow(workflowId);
            List<WorkflowComponent> compList = WorkflowComponentUtil.GetWorkflowComponents(workflowId);
            int i = 0;
            foreach (Panel panelControls in WorkflowSteps.Controls.OfType<Panel>())
            {
                string id = panelControls.ID.Replace("stepControl", string.Empty);
                Panel div = (Panel)panelControls.FindControl("title" + id);
                TextBox stepTitle = (TextBox)div.FindControl("stepTitle" + id);
                DropDownList formSelector = (DropDownList)panelControls.FindControl("formSelector" + id);
                int formId = int.Parse(formSelector.SelectedValue);
                WorkflowComponentUtil.UpdateWorkflowComponent(compList[i].WFComponentID, stepTitle.Text, formId);
                Log.Info(user.Identity + " updated " + w.WorkflowName + " with component " + stepTitle.Text + " assigned to form " + FormUtil.GetFormTemplate(formId).FormName);
                i++;
            }
        }

        protected void AddWorkflowComponentBtn_Click(object sender, EventArgs e)
        {
            if (WorkflowName.Text.Length > 0)
            {
                int workflowId = 0;
                List<Guid> ids = this.ControlIDs;
                Guid guid = Guid.NewGuid();
                ids.Add(guid);

                WorkflowComponent wc = null;
                if (Request.QueryString["wid"] != null)
                {
                    if (WorkflowName.Text.Length > 0)
                    {
                        workflowId = int.Parse(Request.QueryString["wid"]);
                        WorkflowUtil.UpdateWorkflow(workflowId, WorkflowName.Text);
                        SaveComponents(workflowId);
                        wc = WorkflowComponentUtil.CreateWorkflowComponent(workflowId);
                    }
                }
                else
                {
                    if (WorkflowName.Text.Length > 0)
                    {
                        WorkflowModel w = WorkflowUtil.CreateWorkflow(WorkflowName.Text);
                        User user = (User)Session["User"];
                        Log.Info(user.Identity + " created workflow template " + w.WorkflowName);
                        workflowId = w.WorkflowId;
                        SaveComponents(workflowId);
                        wc = WorkflowComponentUtil.CreateWorkflowComponent(workflowId);
                    }
                }
                Panel componentPanel = CreateWorkflowStep(guid, wc);
                this.ControlIDs = ids;
                Response.Redirect("Workflows.aspx?edit=1&wid=" + workflowId);
            }
            else
            {
                WorkflowError.Visible = true;
                WorkflowError.Text = "Please name the Workflow before adding steps";
            }
        }

        private void DelWorkflowComponentBtn_Click(object sender, EventArgs e)
        {
            //finds + removes the selected step
            List<Guid> ids = this.ControlIDs;
            int wfcId = int.Parse(((Button)sender).CommandArgument.ToString().Split(',')[1]);
            string id = ((Button)sender).CommandArgument.ToString().Split(',')[0];
            Guid guid = Guid.Parse(id);
            Panel panelControls = (Panel)WorkflowSteps.FindControl("stepControl" + id);
            WorkflowSteps.Controls.Remove(panelControls);
            ids.Remove(guid);
            this.ControlIDs = ids;

            WorkflowComponentUtil.DeleteWorkflowComponent(wfcId);
            ReloadCurrentPage();
        }

        private Panel CreateWorkflowStep(Guid guid, WorkflowComponent wc)
        {
            string id = guid.ToString("N");
            Panel p = new Panel();
            SetID(p, "stepControl", id);
            p.CssClass = "create-workflow-component";
            p.ID = id;

            Panel uilefticon = new Panel();
            uilefticon.CssClass = "ui left icon input";
            SetID(uilefticon, "title", id);
            Literal icon = new Literal();
            icon.Text = "<i class=\"file icon\"></i>";

            TextBox stepTitleTb = new TextBox();
            SetID(stepTitleTb, "stepTitle", id);
            stepTitleTb.Attributes.Add("placeholder", "Step Title");
            stepTitleTb.Text = wc.ComponentTitle;

            uilefticon.Controls.Add(stepTitleTb);
            uilefticon.Controls.Add(icon);
            p.Controls.Add(uilefticon);

            DropDownList formSelector = new DropDownList();
            SetID(formSelector, "formSelector", id);
            formSelector.CssClass = "form-selector-dropdown";
            formSelector.DataSource = FormUtil.GetAllFormTemplates();
            formSelector.DataValueField = "FormId";
            formSelector.DataTextField = "FormName";
            formSelector.DataBind();
            formSelector.SelectedValue = wc.FormID.ToString();

            p.Controls.Add(formSelector);

            Button delBtn = new Button();
            SetID(delBtn, "delBtn", id);
            delBtn.CssClass = "ui orange basic button";
            delBtn.Text = "Remove";
            delBtn.CommandArgument = id + "," + wc.WFComponentID;
            delBtn.Click += new EventHandler(DelWorkflowComponentBtn_Click);
            p.Controls.Add(delBtn);

            Literal end = new Literal();
            end.Text = "<hr/>";
            p.Controls.Add(end);

            WorkflowSteps.Controls.Add(p);
            return p;
        }

        private void SetID(Control control, string name, string uniqueID)
        {
            control.ID = string.Format("{0}{1}", name, uniqueID);
        }

        private List<Guid> ControlIDs
        {
            get
            {
                List<Guid> guids = new List<Guid>();

                if (ViewState["IDs"] != null && !string.IsNullOrWhiteSpace((string)ViewState["IDs"]))
                {
                    //adds each csv to the id list
                    string[] vals = ((string)ViewState["IDs"]).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string val in vals)
                    {
                        guids.Add(Guid.Parse(val.Trim()));
                    }
                }

                return guids;
            }
            set
            {
                //turns the id list into a csv string
                string vals = string.Join(",", value.ToArray<Guid>());
                ViewState["IDs"] = vals;
            }
        }

        protected void ProjectInformation(WorkflowModel workflow, List<WorkflowComponent> comps)
        {
            workflowNode += "<h1><a href='Workflows.aspx'>Workflows</a> > " + workflow.WorkflowName + "</h1><hr/><h2>Workflow Steps</h2>";

            List<Form> forms = new List<Form>();
            foreach (WorkflowComponent comp in comps)
            {
                forms.Add(FormUtil.GetFormTemplate(comp.FormID));

            }
            workflowNode += "<div class=\"wrapper\"><ol class=\"ProgressBar\">";

            try
            {
                foreach (WorkflowComponent com in comps)
                {
                    workflowNode += "<li class=\"ProgressBar-step\" id=\"li" + com.WFComponentID + "\"><svg class=\"ProgressBar-icon\"><use xlink:href=\"#checkmark-bold\"/></svg>";
                    workflowNode += "<span class=\"ProgressBar-stepLabel\"><a href='Forms.aspx?fid=" + com.FormID + "'>" + com.ComponentTitle + "</a></span><div class=\"li-dropdown\" id=\"li-drop" + com.WFComponentID + "\">";
                    workflowNode += "<div class=\"workflow-form\"><i class=\"big inbox icon\"></i><h3><a href='Forms.aspx?fid=" + com.FormID + "'>" + FormUtil.GetFormTemplate(com.FormID).FormName + "</a></h3></div></div></li>";
                }
            } catch (Exception e) { }
            workflowNode += "</ol></div>";

            workflowNode += "<h2>Forms</h2><div id=\"workflow-forms\">";

            try
            {
                foreach (Form form in forms)
                {
                    workflowNode += "<div class=\"workflow-form\"><i class=\"big inbox icon\"></i><h3><a href='Forms.aspx?fid=" + form.FormId + "'>" + form.FormName + "</a></h3></div>";
                }
            } catch(Exception e) { }

            workflowNode += "</div></div>";

            workflowViewer.InnerHtml += workflowNode;
            workflowNode = "";
        }
    }
}
