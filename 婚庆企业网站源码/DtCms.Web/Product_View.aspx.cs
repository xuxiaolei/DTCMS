using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DtCms.Web
{
    public partial class Product_View : DtCms.Web.UI.BasePage
    {
        protected DtCms.Model.Products pro = new DtCms.Model.Products();

        protected void Page_Load(object sender, EventArgs e)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            //查询分页绑定数据

            if (!string.IsNullOrEmpty(Request.QueryString["Id"] + ""))
            {
                pro = new DtCms.BLL.Products().GetModel(int.Parse(Request.QueryString["Id"].ToString()));

                DataTable dt2 = DtCms.DBUtility.DbHelperOleDb.Query("select * from Channel where ClassList like '%,77,%' and ID not in(77)").Tables[0];
                this.Repeater2.DataSource = dt2;
                this.Repeater2.DataBind();
            }
            
        }

        /// <summary>
        /// 获得类型名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected string GetType(object id)
        {
            return new DtCms.BLL.Channel().GetModel(int.Parse(id.ToString())).Title;
        }
    }
}
