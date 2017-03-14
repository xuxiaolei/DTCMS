using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.Plugin.Menu.admin
{
    public partial class index : Web.UI.ManagePage
    {
        protected int parent_id = 0;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.parent_id = DTRequest.GetQueryInt("parent_id");

            this.keywords = DTRequest.GetQueryString("keywords", true);

            if (!Page.IsPostBack)
            {
                //检查权限
                ChkAdminLevel("plugin_menu", DTEnums.ActionEnum.Show.ToString());

                //绑定类别
                TreeBind();

                //绑定路径
                SiteMapBind();

                //绑定数据
                BLL.menu bll = new BLL.menu();
                DataTable _list = bll.GetList(0, CombSqlTxt(this.keywords), "sort_id asc,id asc", this.parent_id);
                this.rptList.DataSource = _list;
                this.rptList.DataBind();

                //插入关键词
                this.txtKeywords.Text = Utils.Htmls(this.keywords);
            }
        }

        #region 绑定路径
        private void SiteMapBind()
        {
            if (this.parent_id > 0)
            {
                BLL.menu bll = new BLL.menu();
                Model.menu model = bll.GetModel(this.parent_id);

                LabelStatus.Text += "<a href=\"index.aspx\">全部</a><i></i>";

                DataTable dt = bll.GetList(0, "id in(" + model.class_list.Substring(1, (model.class_list).LastIndexOf(",") - 1) + ")", "charindex(','+ltrim(id)+',','" + model.class_list + "')", 0);
                foreach (DataRow dr in dt.Rows)
                {
                    LabelStatus.Text += "<a href=\"index.aspx?parent_id=" + dr["id"].ToString() + "\">" + dr["title"].ToString() + "</a><i></i>";
                }
            }
        }
        #endregion

        #region 绑定类别=================================
        private void TreeBind()
        {
            BLL.menu bll = new BLL.menu();
            DataTable dt = bll.GetList(0, "parent_id=0", "sort_id asc, id desc", 0);

            this.ddlClass.Items.Clear();
            this.ddlClass.Items.Add(new ListItem("所有组", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                this.ddlClass.Items.Add(new ListItem(Title, Id));
            }
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
            ChkAdminLevel("plugin_menu", DTEnums.ActionEnum.Delete.ToString());
            BLL.menu bll = new BLL.menu();

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
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除菜单内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
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

        //美化列表
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                if (classLayer == 1)
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle, (classLayer - 2) * 24, LitImg2, LitImg1);
                }
            }
        }

        //筛选类别
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("index.aspx", "parent_id={0}", ddlClass.SelectedValue));
        }

        /// <summary>
        /// 保存排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_menu", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.menu bll = new BLL.menu();
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
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改菜单排序！"); //记录日志
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