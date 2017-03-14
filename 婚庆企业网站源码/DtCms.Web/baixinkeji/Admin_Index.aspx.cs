using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DtCms.Web.UI;

namespace DtCms.Web.Admin
{
    public partial class admin_index : ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    lblAdminName.Text = Session["AdminName"].ToString();
                    switch (int.Parse(Session["AdminType"].ToString()))
                    {
                        case 1:
                            lblAdminName.Text += "（超级管理员）";
                            break;
                        case 2:
                            lblAdminName.Text += "（系统管理员）";
                            break;
                        case 3:
                            lblAdminName.Text += "（内容管理员）";
                            break;
                    }
                }
                catch
                {
                }
            }
        }

        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Session["AdminNo"] = null;
            Session["AdminName"] = null;
            Session["AdminType"] = null;
            Session["AdminLevel"] = null;

            Response.Redirect("login.aspx");
        }
    }
}
