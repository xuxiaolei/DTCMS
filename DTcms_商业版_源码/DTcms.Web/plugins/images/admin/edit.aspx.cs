using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Images.admin
{
    public partial class edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected string reurl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.images bll = new BLL.images();
            this.reurl = DTRequest.GetQueryString("reurl");

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
                ChkAdminLevel("plugin_images", DTEnums.ActionEnum.Show.ToString());

                //绑定标记
                TreeBind();

                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 绑定类别
        private void TreeBind()
        {
            BLL.images bll = new BLL.images();
            DataTable dt = bll.GetSign().Tables[0];
            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("选择标识", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlCategoryId.Items.Add(new ListItem(dr["sign"].ToString().Trim(), dr["sign"].ToString().Trim()));
            }
        }
        #endregion

        #region 赋值操作
        private void ShowInfo(int _id)
        {
            BLL.images bll = new BLL.images();
            Model.images model = bll.GetModel(_id);
            txtName.Text = model.title;
            txtUrl.Text = model.link_url;
            txtSort.Text = model.sort_id.ToString();
            rblHide.SelectedValue = model.is_lock.ToString();
            txtSign.Text = model.sign;
            txtColor.Text = model.back_color;
            txtContent.Value = model.content;
            txtName.Focus(); //设置焦点，防止JS无法提交
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
                BLL.images bll = new BLL.images();
                Model.images model = new Model.images();

                model.title = txtName.Text;
                model.link_url = txtUrl.Text;
                model.img_url = txtImgUrl.Text;
                model.is_lock = int.Parse(rblHide.SelectedValue);
                model.sort_id = Utils.StrToInt(txtSort.Text, 99);
                model.add_time = DateTime.Now;
                model.back_color = txtColor.Text;
                model.content = txtContent.Value;
                model.sign = txtSign.Text;
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
                    AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "添加图片信息" + model.title); //记录日志
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
                BLL.images bll = new BLL.images();
                Model.images model = bll.GetModel(_id);

                model.title = txtName.Text;
                model.link_url = txtUrl.Text;
                model.img_url = txtImgUrl.Text;
                model.is_lock = int.Parse(rblHide.SelectedValue);
                model.sort_id = Utils.StrToInt(txtSort.Text, 99);
                model.back_color = txtColor.Text;
                model.content = txtContent.Value;
                model.sign = txtSign.Text;
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
                    AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改图片信息" + model.title); //记录日志
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
            string _url = reurl != "" ? this.reurl.Replace("^", "&") : "index.aspx";

            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("plugin_images", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改图片信息成功！", _url);
            }
            else //添加
            {
                ChkAdminLevel("plugin_images", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加图片信息成功！", _url);
            }
        }
    }
}