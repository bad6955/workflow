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
    public partial class Forms : System.Web.UI.Page
    {
        int fieldCt { get; set; }
        int approvalCt { get; set; }

        //prevents users from using back button to return to login protected pages
        protected override void OnInit(EventArgs e)
        {
            approvalCt = 1;
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
                userLbl.Text = user.Email;

                //checks user is an admin
                if (user.RoleId == 4)
                {
                    adminDiv.Visible = true;
                }

                GenerateApprovalDropdowns();
                if (ApprovalDialogStatus.Value == "1")
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "ApprovalPopup", "approvalDialog();", true);
                }
            }
            else
            {
                //kicks them out if they arent
                Response.Redirect("Login.aspx");
            }
        }

        private void GenerateApprovalDropdowns()
        {
            try
            {
                approvalCt = int.Parse(SelectedApprovalCt.Value);
            }
            catch(Exception e)
            {

            }
            if(approvalCt > 1)
            {
                ApprovalRole2.Visible = true;
            }
            if (approvalCt > 2)
            {
                ApprovalRole3.Visible = true;
            }
            if (approvalCt > 3)
            {
                ApprovalRole4.Visible = true;
            }

            List<Role> roles = RoleUtil.GetRoles();
            ApprovalRole1.DataSource = roles;
            ApprovalRole1.DataTextField = "roleName";
            ApprovalRole1.DataValueField = "roleId";
            ApprovalRole1.DataBind();
            ApprovalRole1.SelectedIndex = 0;

            ApprovalRole2.DataSource = roles;
            ApprovalRole2.DataTextField = "roleName";
            ApprovalRole2.DataValueField = "roleId";
            ApprovalRole2.DataBind();
            ApprovalRole2.SelectedIndex = 0;

            ApprovalRole3.DataSource = roles;
            ApprovalRole3.DataTextField = "roleName";
            ApprovalRole3.DataValueField = "roleId";
            ApprovalRole3.DataBind();
            ApprovalRole3.SelectedIndex = 0;

            ApprovalRole4.DataSource = roles;
            ApprovalRole4.DataTextField = "roleName";
            ApprovalRole4.DataValueField = "roleId";
            ApprovalRole4.DataBind();
            ApprovalRole4.SelectedIndex = 0;
        }

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

        protected void LogoutBtn_Click(Object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void CreateFormBtn_Click(object sender, EventArgs e)
        {

        }

        protected void CreateTextFieldBtn_Click(object sender, EventArgs e)
        {
            FormFieldset.InnerHtml += "<div runat=\"server\" ID=\"FormDiv"+ fieldCt + "\" class=\"form-editor-field\">";
            FormFieldset.InnerHtml += "<asp:TextBox runat=\"server\" ID=\"FormField" + fieldCt + "\" placeholder=\"Form Field Text\" TextMode=\"MultiLine\"></asp:TextBox>";
            FormFieldset.InnerHtml += "</div>";
            fieldCt++;
        }

        protected void CreateApprovalPopupBtn_Click(object sender, EventArgs e)
        {
            ApprovalDialog.Visible = true;
            ApprovalDialogStatus.Value = "1";
            Page.ClientScript.RegisterStartupScript(GetType(), "ApprovalPopup", "approvalDialog();", true);
        }

        protected void ApprovalCt_SelectedIndexChanged(object sender, EventArgs e)
        {
            approvalCt = int.Parse(SelectedApprovalCt.Value);
            ApprovalDialogStatus.Value = "1";
        }

        protected void CreateApprovalBtn_Click(object sender, EventArgs e)
        {
            ApprovalDialogStatus.Value = "0";
        }
    }
}