using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace DtCms.Web
{
    public partial class Product : DtCms.Web.UI.BasePage
    {
        protected int page = 0; protected int back = 0; protected int next = 1; protected string type = ""; protected string title = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            //查询分页绑定数据
            if (!string.IsNullOrEmpty(Request.QueryString["page"] + ""))
            {
                page = int.Parse(Request.QueryString["page"].ToString());

                back = page - 1;
                next = page + 1;

                if (back < 0)
                {
                    back =0;
                }
            }


            if (!string.IsNullOrEmpty(Request.QueryString["title"] + ""))
            {
                title = " and Title like '%" + Request.QueryString["title"] + "%'";
            }

            if (!string.IsNullOrEmpty(Request.QueryString["type"] + ""))
            {
                type = Request.QueryString["type"].ToString();
                DataTable dt = bll.GetPageList(15, page, "ClassId not in(78,79) and ClassId=" + Request.QueryString["type"] + title, "  ID desc").Tables[0];
                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();
            }
            else
            {
                DataTable dt = bll.GetPageList(15, page, "ClassId not in(78,79)" + title, "  ID desc").Tables[0];
                this.Repeater1.DataSource = dt;
                this.Repeater1.DataBind();
            }

            DataTable dt2 = DtCms.DBUtility.DbHelperOleDb.Query("select * from Channel where ClassList like '%,77,%' and ID not in(77)").Tables[0];
            this.Repeater2.DataSource = dt2;
            this.Repeater2.DataBind();
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
