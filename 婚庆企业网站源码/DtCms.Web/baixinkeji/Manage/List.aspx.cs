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

namespace DtCms.Web.Admin.Manage
{
    public partial class list : ManagePage
    {
        DtCms.BLL.Admin bll = new DtCms.BLL.Admin();
        public int pcount { get; set; } //总条数
        public int page { get; set; } //当前页
        public readonly int pagesize = 15; //设置每页显示的大小

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chkLoginLevel("viewManage");
                RptBind();
            }
        }

        //绑定数据
        void RptBind()
        {
            string where = "";
            if (Request.QueryString["page"] != null && Convert.ToInt32(Request.QueryString["page"]) > 0)
            {
                page = Convert.ToInt32(Request.QueryString["page"]);
            }
            else
            {
                page = 0;
            }
            //利用PAGEDDAGASOURCE类来分页
            PagedDataSource pg = new PagedDataSource();
            pg.DataSource = bll.GetList(where).Tables[0].DefaultView;
            pg.AllowPaging = true;
            pg.PageSize = pagesize;
            pg.CurrentPageIndex = page;
            //获得总条数
            pcount = bll.GetCount(where);
            //绑定数据
            rptList.DataSource = pg;
            rptList.DataBind();
        }

        //批量删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delManage");
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptPrint("批量删除成功啦！", "", "Success");
            RptBind();
        }
    }
}
