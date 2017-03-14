using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Contents
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                                   //总条数
        public int page;                                     //当前页
        public readonly int pagesize = 15;                    //设置每页显示的大小
        public readonly int kindId = 3;                      //类别种类

        public int classId;
        public string keywords = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["classId"] as string, out this.classId))
            {
                this.classId = 0;
            }
            if (!string.IsNullOrEmpty(Request.Params["keywords"]))
            {
                this.keywords = Request.Params["keywords"].Trim();
            }

            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewContents");
                this.TreeBind();
                this.RptBind("Id>0" + CombSqlTxt(this.classId, this.keywords), "Id asc");
            }
        }

        #region 组合查询语句
        protected string CombSqlTxt(int _classId, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (_classId > 0)
            {
                strTemp.Append(" and ClassId in(select Id from Channel where KindId=" + this.kindId + " and ClassList like '%," + _classId + ",%')");
            }
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and Title like '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 组合URL语句
        protected string CombUrlTxt(int _classId, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (_classId > 0)
            {
                strTemp.Append("classId=" + _classId.ToString() + "&");
            }
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append("keywords=" + Server.UrlEncode(_keywords) + "&");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 内容类别绑定
        private void TreeBind()
        {
            DtCms.BLL.Channel cbll = new DtCms.BLL.Channel();
            DataTable dt = cbll.GetList(0, this.kindId);

            this.ddlClassId.Items.Clear();
            this.ddlClassId.Items.Add(new ListItem("所有类别", ""));
            foreach (DataRow dr in dt.Rows)
            {
                int ClassLayer = Convert.ToInt32(dr["ClassLayer"]);
                string Id = dr["Id"].ToString().Trim();
                string Title = dr["Title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlClassId.Items.Add(new ListItem(Title, Id));

                }
                else
                {
                    Title = "├ " + Title;
                    Title = StringPlus.StringOfChar(ClassLayer - 1, "　") + Title;

                    this.ddlClassId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 数据列表绑定
        private void RptBind(string strWhere, string orderby)
        {
            if (!int.TryParse(Request.Params["page"] as string, out this.page))
            {
                this.page = 0;
            }
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
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
            if (this.classId > 0)
            {
                this.ddlClassId.SelectedValue = this.classId.ToString();
            }
            this.txtKeywords.Text = this.keywords;

            this.rptList.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
            this.rptList.DataBind();
        }
        #endregion

        //类别筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?" + this.CombUrlTxt(_classId, this.keywords) + "page=0");
            }
            else
            {
                Response.Redirect("List.aspx?" + this.CombUrlTxt(0, this.keywords) + "page=0");
            }
        }

        //查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, txtKeywords.Text.Trim()) + "page=0");
        }

        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("delContents");
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
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
            JscriptPrint("批量删除成功啦！", "List.aspx?" + CombUrlTxt(this.classId, this.keywords) + "page=0", "Success");
        }
        
    }
}
