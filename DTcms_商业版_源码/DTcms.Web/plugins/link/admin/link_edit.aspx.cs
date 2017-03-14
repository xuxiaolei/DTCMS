using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Link.admin
{
    public partial class link_edit : DTcms.Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        private string backUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.link().Exists(this.id))
                {
                    JscriptMsg("内容不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("plugin_link", DTEnums.ActionEnum.View.ToString()); //检查权限
                //上一页地址
                this.backUrl = DTRequest.GetUrlReferrer();
                //绑定站点
                TreeBind();
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定站点=================================
        private void TreeBind()
        {
            DTcms.BLL.channel_site bll = new DTcms.BLL.channel_site();
            DataTable dt = bll.GetList(0, "", "sort_id asc,id desc").Tables[0];

            this.ddlSitePath.Items.Clear();
            this.ddlSitePath.Items.Add(new ListItem("请选择站点...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlSitePath.Items.Add(new ListItem(dr["title"].ToString(), dr["build_path"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.link bll = new BLL.link();
            Model.link model = bll.GetModel(_id);

            ddlSitePath.SelectedValue = model.site_path;
            txtTitle.Text = model.title;
            if (model.is_red == 1)
            {
                cbIsRed.Checked = true;
            }
            else
            {
                cbIsRed.Checked = false;
            }
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtUserName.Text = model.user_name;
            txtUserTel.Text = model.user_tel;
            txtEmail.Text = model.email;
            txtSiteUrl.Text = model.site_url;
            //图片
            txtImgUrl.Text = model.img_url;
            if (!string.IsNullOrEmpty(model.img_url))
            {
                ImgDiv.Visible = true;
                ImgUrl.ImageUrl = model.img_url;
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.link model = new Model.link();
            BLL.link bll = new BLL.link();

            model.site_path = ddlSitePath.SelectedValue;
            model.title = txtTitle.Text.Trim();
            model.is_lock = Utils.StrToInt(rblIsLock.SelectedValue, 0);
            if (cbIsRed.Checked == true)
            {
                model.is_red = 1;
            }
            else
            {
                model.is_red = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.user_name = txtUserName.Text.Trim();
            model.user_tel = txtUserTel.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.site_url = txtSiteUrl.Text.Trim();
            model.is_image = 1;
            if (string.IsNullOrEmpty(model.img_url))
            {
                model.is_image = 0;
            }
            //判断上传图片
            if (this.imgUpload.HasFile)
            {
                DTcms.Model.upLoad upfile = new Web.UI.UpLoad().fileSaveAs(this.imgUpload.PostedFile, 0, false, false);
                if (upfile.status > 0)
                {
                    model.img_url = upfile.path;
                }
            }
            else
            {
                model.img_url = txtImgUrl.Text.Trim();
            }
            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加友情链接：" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.link bll = new BLL.link();
            Model.link model = bll.GetModel(_id);

            model.site_path = ddlSitePath.SelectedValue;
            model.title = txtTitle.Text.Trim();
            model.is_lock = Utils.StrToInt(rblIsLock.SelectedValue, 0);
            if (cbIsRed.Checked == true)
            {
                model.is_red = 1;
            }
            else
            {
                model.is_red = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.user_name = txtUserName.Text.Trim();
            model.user_tel = txtUserTel.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.site_url = txtSiteUrl.Text.Trim();
            model.is_image = 1;
            if (string.IsNullOrEmpty(model.img_url))
            {
                model.is_image = 0;
            }
            //判断上传图片
            if (this.imgUpload.HasFile)
            {
                //上传前先删除原图片
                if (!string.IsNullOrEmpty(model.img_url))
                {
                    Utils.DeleteFile(model.img_url);
                }
                DTcms.Model.upLoad upfile = new Web.UI.UpLoad().fileSaveAs(this.imgUpload.PostedFile, 0, false, false);
                if (upfile.status > 0)
                {
                    model.img_url = upfile.path;
                }
            }
            else
            {
                //判断是否需要删除原图
                if (txtImgUrl.Text.Trim() == "" && !string.IsNullOrEmpty(model.img_url))
                {
                    Utils.DeleteFile(model.img_url);
                }
                model.img_url = txtImgUrl.Text.Trim();
            }
            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改友情链接：" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Url = this.backUrl != "" ? this.backUrl : "index.aspx";
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("plugin_link", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("修改链接成功！", Url);
            }
            else //添加
            {
                ChkAdminLevel("plugin_link", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("添加链接成功！", Url);
            }
        }

    }
}