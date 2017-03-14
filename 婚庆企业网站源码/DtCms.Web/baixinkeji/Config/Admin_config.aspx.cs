using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DtCms.Web.UI;

namespace DtCms.Web.Admin.Config
{
    public partial class admin_config : ManagePage
    {
        private DtCms.BLL.WebSet bll = new DtCms.BLL.WebSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkLoginLevel("editConfig");
                LoadWevSet();
            }
        }

        public void LoadWevSet()
        {
            //取得配置信息
            //bll.Get_Config(rwc);
            //string pathurl = Server.MapPath(ConfigurationManager.AppSettings["Configpath"].ToString());
            //model = bll.loadConfig(pathurl);
            //赋值给对应的控件
            txtWebName.Text = webset.WebName;
            txtWebUrl.Text = webset.WebUrl;
            txtWebPath.Text = webset.WebPath;
            txtWebLogPath.Text = webset.WeblogPath;
            txtWebTel.Text = webset.WebTel;
            txtWebFax.Text = webset.WebFax;
            txtTell.Text = webset.Tell;
            txtWebEmail.Text = webset.WebEmail;
            txtWebCrod.Text = webset.WebCrod;
            txtAddress.Text = webset.Address;
            txtWebCopyright.Text = webset.WebCopyright;
            txtWebKeywords.Text = webset.WebKeywords.ToString();
            txtWebDescription.Text = webset.WebDescription.ToString();
            rblWebLogStatus.SelectedValue = webset.WebLogStatus.ToString();
            txtWebKillKeywords.Text = webset.WebKillKeywords.ToString();
            txtWebProSize.Text = webset.WebProSize.ToString();
            txtWebNewsSize.Text = webset.WebNewsSize.ToString();
            txtWebFilePath.Text = webset.WebFilePath.ToString();
            txtWebFileType.Text = webset.WebFileType.ToString();
            txtWebFileSize.Text = webset.WebFileSize.ToString();
            rblIsThumbnail.SelectedValue = webset.IsThumbnail.ToString();
            txtProWidth.Text = webset.ProWidth.ToString();
            txtProHight.Text = webset.ProHight.ToString();
            rblIsWatermark.SelectedValue = webset.IsWatermark.ToString();
            rblWatermarkStatus.SelectedValue = webset.WatermarkStatus.ToString();
            txtImgQuality.Text = webset.ImgQuality.ToString();
            txtImgWaterPath.Text = webset.ImgWaterPath.ToString();
            txtImgWaterTransparency.Text = webset.ImgWaterTransparency.ToString();
            txtWaterText.Text = webset.WaterText.ToString();
            ddlWaterFont.SelectedValue = webset.WaterFont.ToString();
            txtFontSize.Text = webset.FontSize.ToString();
            messlist.SelectedValue = webset.FeedBack.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //赋值给MODEL
                DtCms.Model.WebSet model = new DtCms.Model.WebSet();
                model.WebName = txtWebName.Text;
                model.WebUrl = txtWebUrl.Text;
                model.WebPath = txtWebPath.Text;
                model.WeblogPath = txtWebLogPath.Text;
                model.WebTel = txtWebTel.Text;
                model.WebFax = txtWebFax.Text;
                model.Tell = txtTell.Text;
                model.WebEmail = txtWebEmail.Text;
                model.WebCrod = txtWebCrod.Text;
                model.Address = txtAddress.Text;
                model.WebCopyright = txtWebCopyright.Text;

                model.WebKeywords = txtWebKeywords.Text.Trim();
                model.WebDescription = txtWebDescription.Text.Trim();
                model.WebLogStatus = int.Parse(rblWebLogStatus.SelectedValue);
                model.WebKillKeywords = txtWebKillKeywords.Text.Trim();

                model.WebProSize = int.Parse(txtWebProSize.Text.Trim());
                model.WebNewsSize = int.Parse(txtWebNewsSize.Text.Trim());
                model.WebFilePath = txtWebFilePath.Text;
                model.WebFileType = txtWebFileType.Text;
                model.WebFileSize = int.Parse(txtWebFileSize.Text.Trim());
                model.IsThumbnail = int.Parse(rblIsThumbnail.SelectedValue);
                model.ProWidth = int.Parse(txtProWidth.Text.Trim());
                model.ProHight = int.Parse(txtProHight.Text.Trim());
                model.IsWatermark = int.Parse(rblIsWatermark.SelectedValue.Trim());
                model.WatermarkStatus = int.Parse(rblWatermarkStatus.SelectedValue.Trim());
                model.ImgQuality = int.Parse(txtImgQuality.Text.Trim());
                model.ImgWaterPath = txtImgWaterPath.Text.Trim();
                model.ImgWaterTransparency = int.Parse(txtImgWaterTransparency.Text.Trim());
                model.WaterText = txtWaterText.Text.Trim();
                model.WaterFont = ddlWaterFont.SelectedValue;
                model.FontSize = int.Parse(txtFontSize.Text.Trim());
                model.FeedBack = int.Parse(messlist.SelectedValue);

                ////修改配置信息
                bll.saveConifg(model, Server.MapPath(ConfigurationManager.AppSettings["Configpath"].ToString()));
                JscriptPrint("系统设置成功啦！", "admin_config.aspx", "Success");
            }
            catch
            {

                JscriptMsg(350, 280, "错误提示", "<b>文件写入失败！</b>请检查是否有写入权限，如果没有，请联系管理员开启写入该文件的权限！", "admin_config.aspx", "Error");
            }
        }
    }
}
