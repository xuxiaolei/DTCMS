using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class builder_html : Web.UI.ManagePage
    {
        protected int site_id = 0;
        protected string build_path = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_builder_html", DTEnums.ActionEnum.View.ToString()); //检查权限
                //绑定网点
                BLL.channel_site bll = new BLL.channel_site();
                DataTable dt = bll.GetList(0, "is_mobile=0", "is_default desc,sort_id asc,id desc").Tables[0];
                this.rblSiteId.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    this.rblSiteId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
                }

                if (dt.Rows.Count == 1)
                {
                    this.site_id = Utils.StrToInt(dt.Rows[0]["id"].ToString(), 0);
                }

                if (this.site_id > 0)
                {
                    tabContent.Visible = true;
                    rblSiteId.SelectedValue = this.site_id.ToString();

                    Model.channel_site model = new BLL.channel_site().GetModel(this.site_id);
                    this.build_path = model.build_path;
                    //是否继承
                    if (model.inherit_id > 0)
                    {
                        this.site_id = model.inherit_id;
                    }
                    RptBind(this.site_id);
                }
            }
        }

        #region 数据绑定
        private void RptBind(int _site_id)
        {
            BLL.channel bll = new BLL.channel();
            DataTable dt = bll.GetList(0, "site_id=" + _site_id, "sort_id asc,id desc").Tables[0];
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        //嵌套绑定
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            BLL.article_category bll = new BLL.article_category();

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                DropDownList dropDownList0 = (DropDownList)e.Item.FindControl("dropDownList0");
                DropDownList dropDownList1 = (DropDownList)e.Item.FindControl("dropDownList1");
                DropDownList dropDownList2 = (DropDownList)e.Item.FindControl("dropDownList2");
                TextBox txtStart = (TextBox)e.Item.FindControl("txtStart");
                TextBox txtEnd = (TextBox)e.Item.FindControl("txtEnd");

                txtStart.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
                txtEnd.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //找到分类Repeater关联的数据项 
                DataRowView drv = (DataRowView)e.Item.DataItem;
                //提取站点ID 
                int _channel_id = Convert.ToInt32(drv["id"]);
                //绑定分类
                DataTable dt = bll.GetList(0, _channel_id);

                dropDownList0.Items.Clear();
                dropDownList0.Items.Add(new ListItem("全部", "0"));
                dropDownList1.Items.Clear();
                dropDownList1.Items.Add(new ListItem("全部", "0"));
                dropDownList2.Items.Clear();
                dropDownList2.Items.Add(new ListItem("全部", "0"));
                foreach (DataRow dr in dt.Rows)
                {
                    string Id = dr["id"].ToString();
                    int ClassLayer = int.Parse(dr["class_layer"].ToString());
                    string Title = dr["title"].ToString().Trim();

                    if (ClassLayer == 1)
                    {
                        dropDownList0.Items.Add(new ListItem(Title, Id));
                        dropDownList1.Items.Add(new ListItem(Title, Id));
                        dropDownList2.Items.Add(new ListItem(Title, Id));
                    }
                    else
                    {
                        Title = "├ " + Title;
                        Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                        dropDownList0.Items.Add(new ListItem(Title, Id));
                        dropDownList1.Items.Add(new ListItem(Title, Id));
                        dropDownList2.Items.Add(new ListItem(Title, Id));
                    }
                }
            }
        }

        /// <summary>
        /// 选择站点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _site_id = int.Parse(rblSiteId.SelectedValue);
            Response.Redirect(Utils.CombUrlTxt("builder_html.aspx", "site_id={0}", _site_id.ToString()));
        }
    }
}