using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Menu.admin
{
    public partial class edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        private string backUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.menu bll = new BLL.menu();
            this.id = DTRequest.GetQueryInt("id");

            string _action = DTRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.id = DTRequest.GetQueryInt("id");
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!bll.Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                //检查权限
                ChkAdminLevel("plugin_menu", DTEnums.ActionEnum.Show.ToString());

                //上一页地址
                this.backUrl = DTRequest.GetUrlReferrer();

                //绑定类别
                TreeBind();

                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    ddlParentId.SelectedValue = this.id.ToString();
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.menu bll = new BLL.menu();
            Model.menu model = bll.GetModel(_id);
            txtName.Text = model.title;
            txtUrl.Text = model.link_url;
            ddlParentId.SelectedValue = model.parent_id.ToString();
            txtSort.Text = model.sort_id.ToString();
            rblHide.SelectedValue = model.is_lock.ToString();
            rblMode.SelectedValue = model.open_mode;
            txtName.Focus(); //设置焦点，防止JS无法提交
            txtCssTxt.Text = model.css_txt;
            txtIconUrl.Text = model.img_url;
            if (!string.IsNullOrEmpty(model.img_url))
            {
                ImgDiv.Visible = true;
                IconUrl.ImageUrl = model.img_url;
            }
        }
        #endregion

        #region 绑定类别=================================
        private void TreeBind()
        {
            BLL.menu bll = new BLL.menu();
            DataTable dt = bll.GetList(0, "", "sort_id asc,id asc", 0);

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

        #region 增加操作=================================
        private bool DoAdd()
        {
            try
            {
                BLL.menu bll = new BLL.menu();
                Model.menu model = new Model.menu();
                model.title = txtName.Text;
                model.link_url = txtUrl.Text;
                model.parent_id = int.Parse(ddlParentId.SelectedValue);
                model.open_mode = rblMode.SelectedValue;
                model.is_lock = int.Parse(rblHide.SelectedValue);
                model.sort_id = Utils.StrToInt(txtSort.Text, 99);
                model.css_txt = txtCssTxt.Text;
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
                    model.img_url = txtIconUrl.Text.Trim();
                }
                model.add_time = DateTime.Now;

                if (bll.Add(model) > 0)
                {
                    AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "添加菜单内容:" + model.title); //记录日志
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

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            try
            {
                BLL.menu bll = new BLL.menu();
                Model.menu model = bll.GetModel(_id);

                model.title = txtName.Text;
                model.link_url = txtUrl.Text;
                //如果选择的父ID不是自己,则更改
                int parentId = int.Parse(ddlParentId.SelectedValue);
                if (parentId != model.id)
                {
                    model.parent_id = parentId;
                }
                model.open_mode = rblMode.SelectedValue;
                model.is_lock = int.Parse(rblHide.SelectedValue);
                model.sort_id = Utils.StrToInt(txtSort.Text, 99);
                model.css_txt = txtCssTxt.Text;
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
                    if (txtIconUrl.Text.Trim() == "" && !string.IsNullOrEmpty(model.img_url))
                    {
                        Utils.DeleteFile(model.img_url);
                    }
                    model.img_url = txtIconUrl.Text.Trim();
                }
                if (bll.Update(model))
                {
                    AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改菜单内容:" + model.title); //记录日志
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

        //保存操作
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Url = this.backUrl != "" ? this.backUrl : "index.aspx";

            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("plugin_menu", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改菜单信息成功！", Url);
            }
            else //添加
            {
                ChkAdminLevel("plugin_menu", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加菜单信息成功！", Url);
            }
        }
    }
}