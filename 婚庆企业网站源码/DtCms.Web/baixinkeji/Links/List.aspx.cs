using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Links
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                                   //总条数
        public int page;                                     //当前页
        public readonly int pagesize = 15;                   //设置每页显示的大小

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewLinks");
                this.RptBind("");
            }
        }

        #region 数据列表绑定
        private void RptBind(string strWhere)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            //获得总条数
            this.pcount = bll.GetCount(strWhere);
            if (this.pcount > 0)
            {
                this.lbtnDel.Enabled = true;
            }
            else
            {
                this.lbtnDel.Enabled = false;
            }

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, "AddTime desc");
            this.rptList.DataBind();
        }
        #endregion

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delLinks");
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            //批量删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            JscriptPrint("批量删除成功啦！", "List.aspx", "Success");
        }
    }
}
