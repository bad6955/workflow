using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Workflow.Utility
{
    public static class JSONtoHTML
    {
        public static string ConvertToHTML(string JSON)
        {
            JArray jsonArr = (JArray) JsonConvert.DeserializeObject(JSON);
            Control formDoc = CreateElement("div", "", "", "");
            foreach (JObject jObj in jsonArr)
            {
                IEnumerable<JProperty> props = jObj.Properties();
                string label = "";
                string name = "";
                string className = "";

                string elementType = jObj["subtype"].ToString();

                if (elementType.Length > 0)
                {
                    try
                    {
                        label = jObj["label"].ToString();
                        name = jObj["name"].ToString();
                        className = jObj["className"].ToString();
                    }
                    catch(Exception e){ }
                    try
                    {
                        name = jObj["name"].ToString();
                    }
                    catch (Exception e){ }
                    try
                    {
                        className = jObj["className"].ToString();
                    }
                    catch (Exception e){ }

                    Control ele = CreateElement(elementType, label, className, name);
                    formDoc.Controls.Add(ele);
                }
            }

            string html = RenderControl(formDoc);
            return html;
        }

        private static Control CreateElement(string eleType, string label, string className, string name)
        {
            HtmlGenericControl ele = new HtmlGenericControl(eleType);

            if(label.Length > 0)
            {
                LiteralControl text = new LiteralControl(label);
                ele.Controls.Add(text);
            }

            if(className.Length > 0)
            {
                ele.Attributes["class"] = className;
            }

            if(name.Length > 0)
            {
                ele.ID = name;
            }

            return ele;
        }

        private static string RenderControl(Control control)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter writer = new HtmlTextWriter(sw);
            control.RenderControl(writer);
            return sb.ToString();
        }
    }
}