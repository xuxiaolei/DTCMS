using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;
using System.Data;
//www.51aspx.com
namespace DtCms.Web
{
    public partial class index : DtCms.Web.UI.BasePage
    {
        protected int Id;//全局变量ID
        protected string bodyId;
        protected DtCms.Model.Contents model = new DtCms.Model.Contents();

        protected void Page_Load(object sender, EventArgs e)
        {
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            model = bll.GetModel(6);

            DataTable dt = DtCms.DBUtility.DbHelperOleDb.Query("select * from Article order by AddTime desc").Tables[0];
            this.videolist.DataSource = dt;
            this.videolist.DataBind();
        }
    }
    
}
