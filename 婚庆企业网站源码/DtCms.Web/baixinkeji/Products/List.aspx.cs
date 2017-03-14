using System;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Products
{
    public partial class List : DtCms.Web.UI.ManagePage
    {
        public int pcount;                                   //总条数
        public int page;                                     //当前页
        public readonly int pagesize = 14;                    //设置每页显示的大小
        public readonly int kindId = 2;                      //类别种类

        public int classId;
        public string property = "";
        public string keywords = "";
        public string prolistview = "";

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
            if (!string.IsNullOrEmpty(Request.Params["property"]))
            {
                this.property = Request.Params["property"].Trim();
            }
            if (Request.Cookies["Pro_List_View"] != null)
            {
                this.prolistview = Request.Cookies["Pro_List_View"].Value.ToString();
            }

            if (!Page.IsPostBack)
            {
                chkLoginLevel("viewProducts");
                this.TreeBind();
                this.RptBind("Id>0" + CombSqlTxt(this.classId, this.keywords, this.property), "AddTime desc");
            }
        }

        #region 组合查询语句
        protected string CombSqlTxt(int _classId, string _keywords, string _property)
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
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        strTemp.Append(" and IsLock=1");
                        break;
                    case "isMsg":
                        strTemp.Append(" and IsMsg=1");
                        break;
                    case "isTop":
                        strTemp.Append(" and IsTop=1");
                        break;
                    case "isRed":
                        strTemp.Append(" and IsRed=1");
                        break;
                    case "isHot":
                        strTemp.Append(" and IsHot=1");
                        break;
                    case "isSlide":
                        strTemp.Append(" and IsSlide=1");
                        break;
                }
            }

            return strTemp.ToString();
        }
        #endregion

        #region 组合URL语句
        protected string CombUrlTxt(int _classId, string _keywords, string _property)
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
            if (!string.IsNullOrEmpty(_property))
            {
                strTemp.Append("property=" + _property + "&");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 产品类别绑定
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
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
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
            this.ddlProperty.SelectedValue = this.property;
            //图表或列表显示
            switch (this.prolistview)
            {
                case "Txt":
                    this.rptList2.Visible = false;
                    this.rptList1.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
                    this.rptList1.DataBind();
                    break;
                default:
                    this.rptList1.Visible = false;
                    this.rptList2.DataSource = bll.GetPageList(this.pagesize, this.page, strWhere, orderby);
                    this.rptList2.DataBind();
                    break;
            }
        }
        #endregion

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int id = Convert.ToInt32(((Label)e.Item.FindControl("lb_id")).Text);
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            DtCms.Model.Products model = bll.GetModel(id);
            switch (e.CommandName.ToLower())
            {
                case "ibtnmsg":
                    if (model.IsMsg == 1)
                        bll.UpdateField(id, "IsMsg=0");
                    else
                        bll.UpdateField(id, "IsMsg=1");
                    break;
                case "ibtntop":
                    if (model.IsTop == 1)
                        bll.UpdateField(id, "IsTop=0");
                    else
                        bll.UpdateField(id, "IsTop=1");
                    break;
                case "ibtnred":
                    if (model.IsRed == 1)
                        bll.UpdateField(id, "IsRed=0");
                    else
                        bll.UpdateField(id, "IsRed=1");
                    break;
                case "ibtnhot":
                    if (model.IsHot == 1)
                        bll.UpdateField(id, "IsHot=0");
                    else
                        bll.UpdateField(id, "IsHot=1");
                    break;
                case "ibtnslide":
                    if (model.IsSlide == 1)
                        bll.UpdateField(id, "IsSlide=0");
                    else
                        bll.UpdateField(id, "IsSlide=1");
                    break;
            }
            this.RptBind("Id>0" + CombSqlTxt(this.classId, this.keywords, this.property), "AddTime desc");
        }

        //类别筛选
        protected void ddlClassId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _classId;
            if (int.TryParse(this.ddlClassId.SelectedValue.ToString(), out _classId))
            {
                Response.Redirect("List.aspx?" + this.CombUrlTxt(_classId, this.keywords, this.property) + "page=0");
            }
            else
            {
                Response.Redirect("List.aspx?" + this.CombUrlTxt(0, this.keywords, this.property) + "page=0");
            }
        }

        //属性筛选
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + this.CombUrlTxt(this.classId, this.keywords, this.ddlProperty.SelectedValue) + "page=0");
        }

        //关健字查询
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?" + this.CombUrlTxt(this.classId, txtKeywords.Text.Trim(), this.property) + "page=0");
        }
        //删除
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            chkLoginLevel("viewProducts");
            DtCms.BLL.Products bll = new DtCms.BLL.Products();
            switch (this.prolistview)
            {
                case "Txt":
                    for (int i = 0; i < this.rptList1.Items.Count; i++)
                    {
                        int id = Convert.ToInt32(((Label)this.rptList1.Items[i].FindControl("lb_id")).Text);
                        CheckBox cb = (CheckBox)this.rptList1.Items[i].FindControl("cb_id");
                        if (cb.Checked)
                        {
                            bll.Delete(id);
                        }
                    }
                    break;
                default:
                    for (int i = 0; i < this.rptList2.Items.Count; i++)
                    {
                        int id = Convert.ToInt32(((Label)this.rptList2.Items[i].FindControl("lb_id")).Text);
                        CheckBox cb = (CheckBox)this.rptList2.Items[i].FindControl("cb_id");
                        if (cb.Checked)
                        {
                            bll.Delete(id);
                        }
                    }
                    break;
            }
            JscriptPrint("批量删除成功啦！", "List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.property) + "page=0", "Success");
        }

        protected void ibtnViewTxt_Click(object sender, ImageClickEventArgs e)
        {
            //写入Cookes
            Response.Cookies["Pro_List_View"].Value = "Txt";
            Response.Cookies["Pro_List_View"].Expires = DateTime.Now.AddMonths(1);
            Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.property) + "page=" + this.page);
        }

        protected void ibtnViewImg_Click(object sender, ImageClickEventArgs e)
        {
            //写入Cookes
            Response.Cookies["Pro_List_View"].Value = "Img";
            Response.Cookies["Pro_List_View"].Expires = DateTime.Now.AddMonths(1);
            Response.Redirect("List.aspx?" + CombUrlTxt(this.classId, this.keywords, this.property) + "page=" + this.page);
        }

    }
}
