using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace DtCms.Web
{
    public partial class Video : DtCms.Web.UI.BasePage
    {
        protected string path = ""; protected string title = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = DtCms.DBUtility.DbHelperOleDb.Query("select * from Article order by AddTime desc").Tables[0];

                if (dt.Rows.Count > 0)
                {
                    path = dt.Rows[0]["ImgUrl"].ToString();

                    title = dt.Rows[0]["Title"].ToString();
                }

                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();

                if (!string.IsNullOrEmpty(Request.QueryString["id"] + ""))
                {
                    DtCms.Model.Article art = new DtCms.BLL.Article().GetModel(int.Parse(Request.QueryString["id"].ToString()));

                    title = art.Title;
                }
            }
        }
    }
}
