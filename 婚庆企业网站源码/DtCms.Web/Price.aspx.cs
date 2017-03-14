using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace DtCms.Web
{
    public partial class Price : DtCms.Web.UI.BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            //查询分页绑定数据

            DataTable dt = bll.GetList("ClassId=78").Tables[0];
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();

            this.Repeater2.DataSource = dt;
            this.Repeater2.DataBind();
        }
    }
}
