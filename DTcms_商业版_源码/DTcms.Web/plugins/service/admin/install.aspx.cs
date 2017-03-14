using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.Plugin.Service.admin
{
    public partial class install : Web.UI.ManagePage
    {
        protected int property = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.property = DTRequest.GetQueryInt("property");

            if (!Page.IsPostBack)
            {
                ChkAdminLevel("plugin_lineservice_install", DTEnums.ActionEnum.View.ToString()); //检查权限
                //赋值
                ShowInfo();
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            DTcms.Web.Plugin.Service.BLL.install bll = new DTcms.Web.Plugin.Service.BLL.install();
            DTcms.Web.Plugin.Service.Model.install model = bll.loadConfig("../config/install.config");

            rblStatus.SelectedValue = model.status.ToString();
            txtContent.Text = model.content;
        }
        #endregion

        #region 保存操作
        /// <summary>
        /// 保存配置信息
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_lineservice_install", DTEnums.ActionEnum.Instal.ToString()); //检查权限
            DTcms.Web.Plugin.Service.BLL.install bll = new DTcms.Web.Plugin.Service.BLL.install();
            DTcms.Web.Plugin.Service.Model.install model = bll.loadConfig("../config/install.config");
            try
            {
                model.status = Utils.StrToInt(rblStatus.SelectedValue, 0);
                model.content = txtContent.Text.Trim();

                bll.saveConifg(model, "../config/install.config");
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改在线插件配置信息"); //记录日志
                JscriptMsg("修改在线插件配置信息成功！", "install.aspx");
            }
            catch
            {
                JscriptMsg("文件写入失败，请检查文件夹权限！", "");
            }
        }
        #endregion
    }
}