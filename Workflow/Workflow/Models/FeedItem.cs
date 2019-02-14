using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Workflow.Data;

namespace Workflow.Models
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:FeedItem runat=server></{0}:FeedItem>")]
    public class FeedItem : WebControl
    {
        int id;
        int projectId;
        string text;
        DateTime time;
        string html;

        public FeedItem(string text)
        {
            this.text = text;
        }

        public FeedItem(string text, int projectId)
        {
            this.text = text;
            this.projectId = projectId;
        }

        public FeedItem(int id, string text, DateTime time, int projectId)
        {
            this.id = id;
            this.text = text;
            this.time = time;
            this.projectId = projectId;
        }

        /*
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                String s = (String)ViewState["Text"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["Text"] = value;
            }
        }
        */

        protected override void CreateChildControls()
        {
            Controls.Clear();

            TimeSpan feedTime = (DateTime.Now - time);
            string timeStr = "Updated " + TimeDifference(feedTime) + " ago";

            Button dismissBtn = new Button();
            dismissBtn.ID = "DismissBtn" + id;
            dismissBtn.Text = "Dismiss";
            dismissBtn.CssClass = "ui button";
            //dismissBtn.Click += DismissBtn_Click;
            dismissBtn.Click += new EventHandler(DismissBtn_Click);

            HtmlGenericControl itemDiv = new HtmlGenericControl("div");
            HtmlGenericControl buttonDiv = new HtmlGenericControl("div");
            HtmlGenericControl icon = new HtmlGenericControl("i");
            HtmlGenericControl closeIcon = new HtmlGenericControl("/i");
            HtmlGenericControl contentDiv = new HtmlGenericControl("div");
            HtmlGenericControl aText = new HtmlGenericControl("a");
            HtmlGenericControl closeAText = new HtmlGenericControl("/a");
            HtmlGenericControl descDiv = new HtmlGenericControl("div");
            HtmlGenericControl closeDiv = new HtmlGenericControl("/div");
            LiteralControl textControl = new LiteralControl(text);
            LiteralControl timeControl = new LiteralControl(timeStr);

            itemDiv.Attributes["class"] = "item";
            itemDiv.ID = "FeedItem" + id;
            buttonDiv.Attributes["class"] = "right floated content";
            icon.Attributes["class"] = "bell outline icon";
            contentDiv.Attributes["class"] = "content";
            aText.Attributes["class"] = "header";
            descDiv.Attributes["class"] = "description";

            aText.Controls.Add(textControl);
            descDiv.Controls.Add(timeControl);
            contentDiv.Controls.Add(aText);
            //contentDiv.Controls.Add(closeAText);
            contentDiv.Controls.Add(descDiv);
            //contentDiv.Controls.Add(closeDiv);

            buttonDiv.Controls.Add(dismissBtn);
            itemDiv.Controls.Add(buttonDiv);
            //itemDiv.Controls.Add(closeDiv);
            itemDiv.Controls.Add(icon);
            itemDiv.Controls.Add(contentDiv);
            //itemDiv.Controls.Add(closeDiv);

            string html = RenderControl(itemDiv);
            this.html = html;

            this.Controls.Add(itemDiv);
            /*
            html += "<div class=\"right floated content\">";
            html += "<asp:Button runat=\"server\" ID=\"DismissBtn" + id + "\" OnClick=\"DismissBtn_Click\" class=\"ui button\" Text=\"Dismiss\"></asp:Button>";
            html += "</div>";
            html += "<i class=\"bell outline icon\"></i>";
            html += "<div class=\"content\">";
            html += "<a class=\"header\">" + text + "</a>";
            html += "<div class=\"description\">Updated " + TimeDifference(feedTime) + " ago</div>";
            html += "</div>";
            html += "</div>";
            return html;

            <div id="FeedItem2" class="item">
                <div class="right floated content">
                    <input type="submit" name="DismissBtn2" value="Dismiss" id="DismissBtn2" class="ui button">
                </div>
                <div class="content">
                    <a class="header">Added as a coach on company's project</a>
                    <!--/a-->
                    <div class="description">Updated 0 min ago</div>
                </div>
            </div>


            */
        }

        private string RenderControl(Control control)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter writer = new HtmlTextWriter(sw);
            control.RenderControl(writer);
            return sb.ToString();
        }

        private string TimeDifference(TimeSpan t)
        {
            string s = "";
            if(t.Days > 0)
            {
                s += t.Days + " days";
            }
            else if(t.Hours > 0)
            {
                s += t.Hours + " hours";
            }
            else if (t.Minutes > 0)
            {
                s += t.Minutes + " min";
            }
            else
            {
                s += "0 min";
            }
            return s;
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.Write(html);
        }

        public void DismissBtn_Click(object sender, EventArgs e)
        {
            FeedUtil.DismissItem(this.id);
            EnsureChildControls();
            this.Visible = false;
        }
    }
}
