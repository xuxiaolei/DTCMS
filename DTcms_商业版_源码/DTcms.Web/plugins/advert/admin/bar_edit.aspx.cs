using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.Plugin.Advert.admin
{
    public partial class bar_edit : DTcms.Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected int aid = 0; //广告位ID
        protected int id = 0;

        protected string backUrl = string.Empty;

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
                if (!new BLL.advert_banner().Exists(this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back");
                    return;
                }
            }
            else
            {
                this.aid = DTRequest.GetQueryInt("aid");
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("plugin_advert_bar", DTEnums.ActionEnum.View.ToString()); //检查权限

                TreeBind(); //绑定广告位

                //上一页地址
                this.backUrl = DTRequest.GetUrlReferrer();

                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else if (this.aid > 0)
                {
                    ddlAdvertId.SelectedValue = this.aid.ToString();
                }
            }
        }

        #region 绑定广告位===============================
        private void TreeBind()
        {
            BLL.advert bll = new BLL.advert();
            DataTable dt = bll.GetList(0, "", "add_time desc").Tables[0];

            this.ddlAdvertId.Items.Clear();
            this.ddlAdvertId.Items.Add(new ListItem("所有广告位", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlAdvertId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.advert_banner bll = new BLL.advert_banner();
            Model.advert_banner model = bll.GetModel(_id);
            this.aid = model.aid;
            ddlAdvertId.SelectedValue = model.aid.ToString();
            txtTitle.Text = model.title;
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtStartTime.Text = model.start_time.ToString("yyyy-MM-dd");
            txtEndTime.Text = model.end_time.ToString("yyyy-MM-dd");
            rblTarget.SelectedValue = model.target;
            txtLinkUrl.Text = model.link_url;
            
            txtContent.Text = model.content;
            //图片
            txtFilePath.Text = model.file_path;
            //判断是否是图片
            if (!string.IsNullOrEmpty(model.file_path) && Utils.IsImage(Utils.GetFileExt(model.file_path)))
            {
                ImgDiv.Visible = true;
                ImgUrl.ImageUrl = model.file_path;
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.advert_banner model = new Model.advert_banner();
            BLL.advert_banner bll = new BLL.advert_banner();
            model.aid = int.Parse(ddlAdvertId.SelectedValue);
            model.title = txtTitle.Text.Trim();
            model.start_time = DateTime.Parse(this.txtStartTime.Text.Trim());
            model.end_time = DateTime.Parse(this.txtEndTime.Text.Trim());
            model.target = rblTarget.SelectedValue;
            model.link_url = txtLinkUrl.Text.Trim();
            model.content = txtContent.Text;
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.add_time = DateTime.Now;
            this.aid = model.aid;
            //判断上传图片
            if (this.imgUpload.HasFile)
            {
                DTcms.Model.upLoad upfile = new Web.UI.UpLoad().fileSaveAs(this.imgUpload.PostedFile, 0, false, false);
                if (upfile.status > 0)
                {
                    model.file_path = upfile.path;
                }
            }
            else
            {
                model.file_path = txtFilePath.Text.Trim();
            }
            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加广告内容：" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.advert_banner bll = new BLL.advert_banner();
            Model.advert_banner model = bll.GetModel(_id);
            model.aid = int.Parse(ddlAdvertId.SelectedValue);
            model.title = txtTitle.Text.Trim();
            model.start_time = DateTime.Parse(this.txtStartTime.Text.Trim());
            model.end_time = DateTime.Parse(this.txtEndTime.Text.Trim());
            model.link_url = txtLinkUrl.Text.Trim();
            model.target = rblTarget.SelectedValue;
            model.content = txtContent.Text;
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            this.aid = model.aid;
            //判断上传图片
            if (this.imgUpload.HasFile)
            {
                //上传前先删除原图片
                if (!string.IsNullOrEmpty(model.file_path))
                {
                    Utils.DeleteFile(model.file_path);
                }
                DTcms.Model.upLoad upfile = new Web.UI.UpLoad().fileSaveAs(this.imgUpload.PostedFile, 0, false, false);
                if (upfile.status > 0)
                {
                    model.file_path = upfile.path;
                }
            }
            else
            {
                //判断是否需要删除原图
                if (txtFilePath.Text.Trim() == "" && !string.IsNullOrEmpty(model.file_path))
                {
                    Utils.DeleteFile(model.file_path);
                }
                model.file_path = txtFilePath.Text.Trim();
            }
            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改广告内容：" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Url = this.backUrl != "" ? this.backUrl : "bar_list.aspx?aid=" + this.aid.ToString();

            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("plugin_advert_bar", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", string.Empty);
                    return;
                }
                JscriptMsg("编辑成功！", Url);
            }
            else //添加
            {
                ChkAdminLevel("plugin_advert_bar", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误啦！", string.Empty);
                    return;
                }
                JscriptMsg("添加成功！", Url);
            }
        }

    }
}