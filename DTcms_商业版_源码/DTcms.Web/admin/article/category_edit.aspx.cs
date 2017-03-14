using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.admin.article
{
    public partial class category_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        protected int channel_id;
        private int id = 0;
        protected DateTime dtime;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            this.id = DTRequest.GetQueryInt("id");

            if (this.channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back");
                return;
            }
            this.channel_name = new BLL.channel().GetChannelName(this.channel_id); //取得频道名称
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.article_category().Exists(this.id))
                {
                    JscriptMsg("类别不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                //浏览权限
                ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.View.ToString());

                //编辑权限
                if (!ChkAuthority("channel_" + this.channel_name + "_category", this.action))
                {
                    this.btnSubmit.Visible = false;
                }

                TreeBind(this.channel_id); //绑定类别
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    if (this.id > 0)
                    {
                        this.ddlParentId.SelectedValue = this.id.ToString();
                    }
                }
            }
        }

        #region 绑定类别
        private void TreeBind(int _channel_id)
        {
            BLL.article_category bll = new BLL.article_category();
            string strWhere = string.Empty;
            //获取管理员
            Model.manager adminModel = GetAdminInfo();
            if (adminModel.role_type != 1)
            {
                List<int> idlist = new List<int>();
                //获取频道名称
                string channel_name = new BLL.channel().GetChannelName(this.channel_id);
                //获取权限列表
                string nav_name = "channel_" + channel_name + "_category_";
                DataTable dt2 = new BLL.manager_role_value().GetList(100, string.Format("role_id={0} and action_type='Show' and nav_name like '{1}%'", adminModel.role_id, nav_name), "").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        idlist.Add(int.Parse(dr["nav_name"].ToString().Replace(nav_name, string.Empty)));
                    }
                }
                else
                {
                    idlist.Add(0);
                }
                strWhere = string.Format("id in ({0})", string.Join(",", idlist));
            }
            DataTable dt = bll.GetList(0, this.channel_id, strWhere);

            this.ddlParentId.Items.Clear();
            this.ddlParentId.Items.Add(new ListItem("无父级分类", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            BLL.article_category bll = new BLL.article_category();
            Model.article_category model = bll.GetModel(_id);

            ddlParentId.SelectedValue = model.parent_id.ToString();
            txtCallIndex.Text = model.call_index;
            txtTitle.Text = model.title;
            txtSortId.Text = model.sort_id.ToString();
            txtSeoTitle.Text = model.seo_title;
            txtSeoKeywords.Text = model.seo_keywords;
            txtSeoDescription.Text = model.seo_description;
            txtLinkUrl.Text = model.link_url;
            txtContent.Value = model.content;
            rblPage.SelectedValue = model.is_page.ToString();
            rblStatus.SelectedValue = model.is_lock.ToString();
            //图片
            txtImgUrl.Text = model.img_url;
            if (!string.IsNullOrEmpty(model.img_url))
            {
                ImgDiv.Visible = true;
                ImgUrl.ImageUrl = model.img_url;
            }
        }
        #endregion

        #region 增加操作
        private bool DoAdd()
        {
            try
            {
                Model.article_category model = new Model.article_category();
                BLL.article_category bll = new BLL.article_category();
                model.site_id = new BLL.channel().GetSiteId(this.channel_id);
                model.channel_id = this.channel_id;
                model.call_index = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();
                model.parent_id = int.Parse(ddlParentId.SelectedValue);
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.seo_title = txtSeoTitle.Text;
                model.seo_keywords = txtSeoKeywords.Text;
                model.seo_description = txtSeoDescription.Text;
                model.link_url = txtLinkUrl.Text.Trim();
                model.content = txtContent.Value;
                model.is_page = int.Parse(rblPage.SelectedValue);
                model.is_lock = int.Parse(rblStatus.SelectedValue);
                //判断上传图片
                if (this.imgUpload.HasFile)
                {
                    Model.upLoad upfile = new Web.UI.UpLoad().fileSaveAs(this.imgUpload.PostedFile, 0, false, false);
                    if (upfile.status > 0)
                    {
                        model.img_url = upfile.path;
                    }
                }
                else
                {
                    model.img_url = txtImgUrl.Text.Trim();
                }
                //获取管理员
                int rolo_id = 0;
                Model.manager adminModel = GetAdminInfo();
                if (adminModel.role_type != 1)
                {
                    rolo_id = adminModel.role_id;
                }

                if (bll.Add(model, rolo_id) > 0)
                {
                    AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道栏目分类:" + model.title); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 修改操作
        private bool DoEdit(int _id)
        {
            try
            {
                BLL.article_category bll = new BLL.article_category();
                Model.article_category model = bll.GetModel(_id);

                int parentId = int.Parse(ddlParentId.SelectedValue);
                model.channel_id = this.channel_id;
                model.call_index = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();
                //如果选择的父ID不是自己,则更改
                if (parentId != model.id)
                {
                    model.parent_id = parentId;
                }
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.seo_title = txtSeoTitle.Text;
                model.seo_keywords = txtSeoKeywords.Text;
                model.seo_description = txtSeoDescription.Text;
                model.content = txtContent.Value;
                model.is_page = int.Parse(rblPage.SelectedValue);
                model.is_lock = int.Parse(rblStatus.SelectedValue);
                //判断上传图片
                if (this.imgUpload.HasFile)
                {
                    //上传前先删除原图片
                    if (!string.IsNullOrEmpty(model.img_url))
                    {
                        Utils.DeleteFile(model.img_url);
                    }
                    Model.upLoad upfile = new Web.UI.UpLoad().fileSaveAs(this.imgUpload.PostedFile, 0, false, false);
                    if (upfile.status > 0)
                    {
                        model.img_url = upfile.path;
                    }
                }
                else
                {
                    //判断是否需要删除原图
                    if (!string.IsNullOrEmpty(model.img_url))
                    {
                        Utils.DeleteFile(model.img_url);
                    }
                    model.img_url = txtImgUrl.Text.Trim();
                }
                if (bll.Update(model))
                {
                    AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道栏目分类:" + model.title); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        //保存类别
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {

                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改类别成功！", "category_list.aspx?channel_id=" + channel_id);
            }
            else //添加
            {
                ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加类别成功！", "category_list.aspx?channel_id=" + channel_id);
            }
        }

    }
}