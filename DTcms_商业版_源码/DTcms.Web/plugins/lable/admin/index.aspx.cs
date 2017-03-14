using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.Plugin.Lable.admin
{
    public partial class index : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int id;
        protected int page;
        protected int pageSize;

        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = DTRequest.GetQueryString("keywords", true);
            this.id = DTRequest.GetQueryIntValue("id", 0);
            this.page = DTRequest.GetQueryIntValue("page", 1);
            this.pageSize = GetPageSize(10); //每页数量

            if (!Page.IsPostBack)
            {
                //读取用户
                DTcms.Model.manager model = GetAdminInfo();

                //检查权限
                ChkAdminLevel("plugin_lable", DTEnums.ActionEnum.Show.ToString());

                //绑定数据
                BLL.lable bll = new BLL.lable();
                DataSet _list = bll.GetList(pageSize, page, CombSqlTxt(this.keywords), "sort_id asc,id asc", out totalCount);
                this.rptList.DataSource = _list;
                this.rptList.DataBind();

                //插入关键词
                this.txtKeywords.Text = Utils.Htmls(this.keywords);

                //绑定页码
                this.txtPageNum.Text = pageSize.ToString();
                string pageUrl = Utils.CombUrlTxt("index.aspx", "keywords={0}&page={1}", keywords, "__id__");
                PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
            }
        }

        #region 返回每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("lable_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //检查权限
            ChkAdminLevel("plugin_lable", DTEnums.ActionEnum.Delete.ToString());
            BLL.lable bll = new BLL.lable();

            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            Repeater rptList = new Repeater();
            rptList = this.rptList;
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除标签内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("成功删除 " + sucCount + " 条,失败 " + errorCount + " 条！", Utils.CombUrlTxt("index.aspx", "keywords={0}", this.keywords));
        }

        /// <summary>
        /// 关键词查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("index.aspx", "keywords={0}", txtKeywords.Text));
        }

        /// <summary>
        /// 每页显示总数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("lable_page_size", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("index.aspx", "keywords={0}&page={1}", keywords));
        }

        /// <summary>
        /// 保存排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_lable", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.lable bll = new BLL.lable();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            JscriptMsg("保存排序成功！", Utils.CombUrlTxt("index.aspx", "keywords={0}", this.keywords));
        }

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append("title like  '%" + _keywords + "%'");
            }
            return strTemp.ToString();
        }
        #endregion
    }
}