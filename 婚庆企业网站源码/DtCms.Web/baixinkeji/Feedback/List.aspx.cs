using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Feedback
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                                   //总条数
        public int page;                                     //当前页
        public readonly int pagesize = 15;                    //设置每页显示的大小

        public string property = "";
        public string keywords = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Params["property"]))
            {
                this.property = Request.Params["property"].Trim();
            }
            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }

            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewFeedback");
                this.RptBind("Id>0" + CombSqlTxt(this.property, this.keywords), "AddTime desc");
            }
        }

        #region 组合查询语句
        protected string CombSqlTxt(string _property, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            switch (_property)
            {
                case "isLock":
                    strTemp.Append(" and IsLock=1");
                    break;
                case "unLock":
                    strTemp.Append(" and IsLock=0");
                    break;
            }
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and Title like '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 组合URL语句
        protected string CombUrlTxt(string _property, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_property))
            {
                strTemp.Append("property=" + _property + "&");
            }
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append("keywords=" + Server.UrlEncode(_keywords) + "&");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 数据列表绑定
        private void RptBind(string strWhere, string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
            //获得总条数
            this.pcount = bll.GetCount(strWhere);
            if (this.pcount > 0)
            {
                this.lbtnDel.Enabled = true;
                this.lbtnAudit.Enabled = true;
            }
            else
            {
                this.lbtnDel.Enabled = false;
                this.lbtnAudit.Enabled = false;
            }
            this.txtKeywords.Text = this.keywords;
            this.ddlProperty.SelectedValue = this.property;

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
            this.rptList.DataBind();
        }
        #endregion

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + this.CombUrlTxt(this.ddlProperty.SelectedValue, this.keywords) + "page=0");
        }

        //关健字查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.property, txtKeywords.Text.Trim()) + "page=0");
        }

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delFeedback");
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
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
            JscriptPrint("批量删除成功啦！", "List.aspx?" + CombUrlTxt(this.property, this.keywords) + "page=0", "Success");
        }

        //审核
        protected void lbtnAudit_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((Label)rptList.Items[i].FindControl("lb_id")).Text);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("cb_id");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "IsLock=0");
                }
            }
            JscriptPrint("批量审核通过啦！", "List.aspx?" + CombUrlTxt(this.property, this.keywords) + "page=" + this.page, "Success");
        }
        
    }
}
