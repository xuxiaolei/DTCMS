using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace DtCms.Web
{
    public partial class Team : DtCms.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            //查询分页绑定数据

            DataTable dt = bll.GetList("ClassId=79").Tables[0];
            int a = dt.Rows.Count;
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
        }
    }
}
