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

        protected override void CreateChildControls()
        {
            Controls.Clear();

            TimeSpan feedTime = (DateTime.Now - time);
            string timeStr = "Updated " + TimeDifference(feedTime) + " ago";

            Button dismissBtn = new Button();
            dismissBtn.ID = "DismissBtn" + id;
            dismissBtn.Text = "Dismiss";
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

            if(this.projectId != -1)
            {
                aText.Attributes["href"] = "Projects.aspx?pid="+this.projectId;
            }
            descDiv.Attributes["class"] = "description";

            aText.Controls.Add(textControl);
            descDiv.Controls.Add(timeControl);
            contentDiv.Controls.Add(aText);
            contentDiv.Controls.Add(descDiv);
            //buttonDiv.Controls.Add(dismissBtn);
            //itemDiv.Controls.Add(buttonDiv);
            itemDiv.Controls.Add(icon);
            itemDiv.Controls.Add(contentDiv);

            string html = RenderControl(itemDiv);
            this.html = html;
            this.Controls.Add(itemDiv);
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
