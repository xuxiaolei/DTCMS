using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace DtCms.Web
{
    public partial class Top : System.Web.UI.UserControl
    {
        protected internal DtCms.Model.WebSet webset;
        protected string top1 = "<li>"; protected string top2 = "<li>"; protected string top3 = "<li>";
        protected string top4 = "<li>"; protected string top5 = "<li>"; protected string top6 = "<li>";
        protected string top7 = "<li>";
        protected void Page_Load(object sender, EventArgs e)
        {
            webset = new DtCms.BLL.WebSet().loadConfig(Server.MapPath(ConfigurationManager.AppSettings["Configpath"].ToString()));

            if (!string.IsNullOrEmpty(Request.QueryString["top"] + ""))
            {
                string topcss=Request.QueryString["top"].ToString();
                switch (topcss)
                {
                    case "2":
                        top2 = "<li class=\"currentnav\">";
                        break;
                    case "3":
                        top3 = "<li class=\"currentnav\">";
                        break;
                    case "4":
                        top4 = "<li class=\"currentnav\">";
                        break;
                    case "5":
                        top5 = "<li class=\"currentnav\">";
                        break;
                    case "6":
                        top6 = "<li class=\"currentnav\">";
                        break;
                    case "7":
                        top7 = "<li class=\"currentnav\">";
                        break;
                }
            }
            else
            {
                top1 = "<li class=\"currentnav\">";
            }
        }

        /// <summary>
        /// 点击搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Product.aspx?top=2&title=" + Server.UrlEncode(this.titletxt.Text.Trim()));
        }
    }
}