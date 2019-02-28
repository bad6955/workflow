using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Workflow.Data;
using Workflow.Models;

namespace Workflow
{
    public partial class Workflows : System.Web.UI.Page
    {
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
                    CreateClientWorkflowList(user.CompanyId);
                }
                else if (user.RoleId == 2)
                {
                    CreateWorkflowList(user.UserId);
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

                        //if they are trying to edit and they are admin, show form builder
                        if (Request.QueryString["edit"] != null && user.RoleId == 4)
                        {
                            workflowBuilder.Visible = true;
                            WorkflowName.Text = w.WorkflowName;
                            CreateWorkflowBtn.Text = "Update Workflow";

                            LoadWorkflowSteps(workflowId);
                        }
                        //otherwise just show the form viewer
                        else
                        {
                            workflowViewer.Visible = true;
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
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
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
            var workflowNode = "";
            List<WorkflowModel> workflows = WorkflowUtil.GetWorkflows();
            var count = 0;
            for (int i = 0; i < 5 && i < workflows.Count; i++)
            {
                workflowNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/workflow.png\"/></div>";
                workflowNode += "<div class=\"content\"><a class=\"header\">" + workflows[i].WorkflowName + "</a><div class=\"meta\">";
                workflowNode += "<span class=\"stay\">" + "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "'>View Workflow</a>" + " | ";
                workflowNode += "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "&edit=1'>Edit Workflow</a>" + " | ";
                workflowNode += "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "&del=1'>Delete Workflow</a>" + "</span></div></div></div>";
                workflowList.InnerHtml += workflowNode;
                count++;
            }
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateClientWorkflowList(int companyId)
        {
            CreateNewWorkflowBtn.Visible = false;
            var workflowNode = "";
            List<WorkflowModel> workflows = WorkflowUtil.GetCompanyWorkflows(companyId);
            var count = 0;
            for (int i = 0; i < 5 && i < workflows.Count; i++)
            {
                workflowNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/workflow.png\"/></div>";
                workflowNode += "<div class=\"content\"><a class=\"header\">" + workflows[i].WorkflowName + "</a><div class=\"meta\">";
                workflowNode += "<span class=\"stay\">" + "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "'>View Workflow</a>" + "</span></div></div></div>";
                workflowList.InnerHtml += workflowNode;
                count++;
            }
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void CreateWorkflowList(int userId)
        {
            var workflowNode = "";
            List<WorkflowModel> workflows = WorkflowUtil.GetCoachWorkflows(userId);
            var count = 0;
            for (int i = 0; i < 5 && i < workflows.Count; i++)
            {
                workflowNode = "<div class=\"item\"><div class=\"ui small image\"><img src=\"assets/icons/workflow.png\"/></div>";
                workflowNode += "<div class=\"content\"><a class=\"header\">" + workflows[i].WorkflowName + "</a><div class=\"meta\">";
                workflowNode += "<span class=\"stay\">" + "<a href='Workflows.aspx?wid=" + workflows[i].WorkflowId + "'>View Workflow</a>" + "</span></div></div></div>";
                workflowList.InnerHtml += workflowNode;
                count++;
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
            List<WorkflowComponent> compList = WorkflowComponentUtil.GetWorkflowComponents(workflowId);
            int i = 0;
            foreach (Panel panelControls in WorkflowSteps.Controls.OfType<Panel>())
            {
                string id = panelControls.ID.Replace("stepControl", string.Empty);
                TextBox stepTitle = (TextBox)panelControls.FindControl("stepTitle" + id);
                DropDownList formSelector = (DropDownList)panelControls.FindControl("formSelector" + id);
                int formId = int.Parse(formSelector.SelectedValue);
                WorkflowComponentUtil.UpdateWorkflowComponent(compList[i].WFComponentID, stepTitle.Text, formId);
                i++;
            }
        }

        protected void AddWorkflowComponentBtn_Click(object sender, EventArgs e)
        {
            int workflowId = 0;
            List<Guid> ids = this.ControlIDs;
            Guid guid = Guid.NewGuid();
            ids.Add(guid);

            WorkflowComponent wc = null;
            if (Request.QueryString["wid"] != null)
            {
                workflowId = int.Parse(Request.QueryString["wid"]);
                WorkflowUtil.UpdateWorkflow(workflowId, WorkflowName.Text);
                SaveComponents(workflowId);
                wc = WorkflowComponentUtil.CreateWorkflowComponent(workflowId);
            }
            else
            {
                WorkflowModel w = WorkflowUtil.CreateWorkflow(WorkflowName.Text);
                workflowId = w.WorkflowId;
                SaveComponents(workflowId);
                wc = WorkflowComponentUtil.CreateWorkflowComponent(workflowId);
            }
            Panel componentPanel = CreateWorkflowStep(guid, wc);
            this.ControlIDs = ids;
            Response.Redirect("Workflows.aspx?edit=1&wid=" + workflowId);
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
            p.ID = id;

            TextBox stepTitleTb = new TextBox();
            SetID(stepTitleTb, "stepTitle", id);
            stepTitleTb.Attributes.Add("placeholder", "Step Title");
            stepTitleTb.Text = wc.ComponentTitle;
            p.Controls.Add(stepTitleTb);

            DropDownList formSelector = new DropDownList();
            SetID(formSelector, "formSelector", id);
            formSelector.DataSource = FormUtil.GetAllFormTemplates();
            formSelector.DataValueField = "FormId";
            formSelector.DataTextField = "FormName";
            formSelector.DataBind();
            formSelector.SelectedValue = wc.FormID.ToString();

            p.Controls.Add(formSelector);

            Button delBtn = new Button();
            SetID(delBtn, "delBtn", id);
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

        // ====== NAV ======
        protected void DashboardBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }

        protected void ProjectBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Projects.aspx");
        }

        protected void WorkflowBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Workflows.aspx");
        }

        protected void FormBtn_Click(Object sender, EventArgs e)
        {
            Response.Redirect("Forms.aspx");
        }

        protected void LogoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}