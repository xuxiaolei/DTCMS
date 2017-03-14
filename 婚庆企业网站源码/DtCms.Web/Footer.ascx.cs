using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace DtCms.Web
{
    public partial class Footer : System.Web.UI.UserControl
    {
        protected internal DtCms.Model.WebSet webset;
        protected void Page_Load(object sender, EventArgs e)
        {
            webset = new DtCms.BLL.WebSet().loadConfig(Server.MapPath(ConfigurationManager.AppSettings["Configpath"].ToString()));
        }
    }
}