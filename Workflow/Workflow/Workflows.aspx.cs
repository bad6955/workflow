﻿using System;
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
                            test.Visible = true;
                            WorkflowName.Text = w.WorkflowName;
                            CreateWorkflowBtn.Text = "Update Workflow";

                            LoadWorkflowSteps(workflowId);
                        }
                        //otherwise just show the form viewer
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
                    test.Visible = true;
                }
            }
            else
            {
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
        }

        protected void LoadMoreWorkflows(object sender, EventArgs e)
        {
            ViewState["workflowcount"] = Convert.ToInt32(ViewState["workflowcount"]) + 1;
            int loaded = Convert.ToInt32(ViewState["workflowcount"]);
            
            List<WorkflowModel> workflows = WorkflowUtil.GetAllWorkflows();
            if (loaded == 1)
            {
                ViewState["workflowcount"] = Convert.ToInt32(ViewState["workflowcount"]) + 1;
                loaded = Convert.ToInt32(ViewState["workflowcount"]);
            }
            for (int i = 5; i < loaded * 5 && i < workflows.Count; i++)
            {
                MakeText(workflows, workflowNode, i);
            }

            numberShowing.InnerHtml = "";
            var showing = "Showing 1 - " + count + " of " + workflows.Count + " Results";
            numberShowing.InnerHtml += showing;
        }

        private void MakeText(List<WorkflowModel> workflows, String workflowNode, int i)
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
                MakeText(workflows, workflowNode, i);
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
                MakeText(workflows, workflowNode, i);
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
                Panel formSelector = (Panel)panelControls.FindControl("formSelector" + id);
                Panel dropdownselect = (Panel)formSelector.FindControl("menu" + id);

                var selected = "-1";
                Console.WriteLine("Child doesnt");
                foreach (Control child in dropdownselect.Controls)
                {
                    Panel c = (Panel)child;
                    Console.WriteLine("Child exists");
                    if (c.Attributes["class"] == "item active selected")
                        selected = c.Attributes["data-value"];
                    else
                        selected = "1";
                }
                int formId = int.Parse(selected);
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

            Panel dropdownPanel = new Panel();
            SetID(dropdownPanel, "formSelector", id);
            dropdownPanel.CssClass = "ui selection dropdown";


            Literal input = new Literal();
            input.Text = "<input type=\"hidden\">";
            dropdownPanel.Controls.Add(input);
            Literal icon = new Literal();
            icon.Text = "<i class=\"dropdown icon\"></i>";
            dropdownPanel.Controls.Add(icon);
            Literal def = new Literal();
            def.Text = "<div class=\"default text\">--SELECT WORKFLOW--</div>";
            dropdownPanel.Controls.Add(def);

            Panel dropdown = new Panel();
            dropdown.CssClass = "menu";
            SetID(dropdown, "menu", id);
            
            foreach(Form form in FormUtil.GetAllForms())
            {
                Panel item = new Panel();
                item.CssClass = "item";
                SetID(item, "item", id+form.FormId);
                item.Attributes.Add("data-value", form.FormId.ToString());
                item.Controls.Add(new LiteralControl(form.FormName));
                item.DataBind();
                dropdown.Controls.Add(item);
            }

            /*DropDownList formSelector = new DropDownList();
            SetID(formSelector, "formSelector", id);
            formSelector.DataSource = FormUtil.GetAllFormTemplates();
            formSelector.DataValueField = "FormId";
            formSelector.DataTextField = "FormName";
            formSelector.DataBind();
            formSelector.SelectedValue = wc.FormID.ToString();*/

            dropdownPanel.Controls.Add(dropdown);
            p.Controls.Add(dropdownPanel);

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
            workflowNode = "";
            /*workflowNode += "<div class=\"row\">";
            workflowNode += "<div class=\"col-lg-4 col-sm-6 text-center mb-4\">";
            workflowNode += "<img class=\"rounded-circle img-fluid d-block mx-auto\" src=\"http://placehold.it/200x200\" alt=\"\">";
            workflowNode += "<h3>Username</h3></div><div class=\"col-lg-4 col-sm-6 text-center mb-4\">";
            workflowNode += "<img class=\"rounded-circle img-fluid d-block mx-auto\" src=\"http://placehold.it/200x200\" alt=\"\">";
            workflowNode += "<h3>Coach Name</h3></div><div class=\"col-lg-4 col-sm-6 text-center mb-4\">";
            workflowNode += "<img class=\"rounded-circle img-fluid d-block mx-auto\" src=\"http://placehold.it/200x200\" alt=\"\">";
            workflowNode += "<h3>Funding Source Name</h3></div></div>";*/

            workflowNode += "<h1>"+workflow.WorkflowName+"</h1>";
            workflowNode += "<table class=\"ui teal table\"><thead><tr><th>Component Title</th></tr></thead><tbody>";
            foreach (WorkflowComponent comp in comps)
            {
                workflowNode += "<tr><td>"+comp.ComponentTitle+"</td></tr>";
            }
            workflowNode += "</tbody></table>";

            workflowViewer.InnerHtml = workflowNode;
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