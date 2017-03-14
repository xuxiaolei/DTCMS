using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class sys_config : Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_config", DTEnums.ActionEnum.View.ToString()); //检查权限
                //编辑权限
                if (!ChkAuthority("sys_config", DTEnums.ActionEnum.Edit.ToString()))
                {
                    this.btnSubmit.Visible = false;
                }

                ShowInfo();
            }
        }

        #region 赋值操作
        private void ShowInfo()
        {
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig();

            webname.Text = model.webname;
            weburl.Text = model.weburl;
            webcompany.Text = model.webcompany;
            webaddress.Text = model.webaddress;
            webtel.Text = model.webtel;
            webfax.Text = model.webfax;
            webmail.Text = model.webmail;
            webcrod.Text = model.webcrod;

            webpath.Text = model.webpath;
            webmanagepath.Text = model.webmanagepath;
            staticstatus.SelectedValue = model.staticstatus.ToString();
            staticextension.Text = model.staticextension;
            if (model.memberstatus == 1)
            {
                memberstatus.Checked = true;
            }
            else
            {
                memberstatus.Checked = false;
            }
            if (model.commentstatus == 1)
            {
                commentstatus.Checked = true;
            }
            else
            {
                commentstatus.Checked = false;
            }
            if (model.logstatus == 1)
            {
                logstatus.Checked = true;
            }
            else
            {
                logstatus.Checked = false;
            }
            if (model.webstatus == 1)
            {
                webstatus.Checked = true;
            }
            else
            {
                webstatus.Checked = false;
            }
            webclosereason.Text = model.webclosereason;
            webcountcode.Text = model.webcountcode;
            smsapiurl.Text = model.smsapiurl;
            smsusername.Text = model.smsusername;
            if (!string.IsNullOrEmpty(model.smspassword))
            {
                smspassword.Attributes["value"] = defaultpassword;
            }
            smssubmit.Text = model.smssubmit;
            smssendpara.Text = model.smssendpara;
            smssign.SelectedValue = model.smssign.ToString();
            smssigntxt.Text = model.smssigntxt;
            smssendanswer.SelectedValue = model.smssendanswer.ToString();
            smssendcode.Text = model.smssendcode;
            smssendlable.Text = model.smssendlable;
            smsmark.Text = model.smsmark;
            smssendcount.Text = model.smssendcount.ToString();
            smsqueryapiurl.Text = model.smsqueryapiurl;
            smsquerypara.Text = model.smsquerypara;
            smsqueryanswer.SelectedValue = model.smsqueryanswer.ToString();
            smsquerycode.Text = model.smsquerycode;
            smsqueryformat.Text = model.smsqueryformat;
            smserrorcode.Text = model.smserrorcode;

            emailsmtp.Text = model.emailsmtp;
            if (model.emailssl == 1)
            {
                emailssl.Checked = true;
            }
            else
            {
                emailssl.Checked = false;
            }
            emailport.Text = model.emailport.ToString();
            emailfrom.Text = model.emailfrom;
            emailusername.Text = model.emailusername;
            if (!string.IsNullOrEmpty(model.emailpassword))
            {
                emailpassword.Attributes["value"] = defaultpassword;
            }
            emailnickname.Text = model.emailnickname;

            filepath.Text = model.filepath;
            filesave.SelectedValue = model.filesave.ToString();
            fileremote.SelectedValue = model.fileremote.ToString();
            imgextension.Text = model.imgextension;
            fileextension.Text = model.fileextension;
            videoextension.Text = model.videoextension;
            attachsize.Text = model.attachsize.ToString();
            videosize.Text = model.videosize.ToString();
            imgsize.Text = model.imgsize.ToString();
            imgmaxheight.Text = model.imgmaxheight.ToString();
            imgmaxwidth.Text = model.imgmaxwidth.ToString();
            thumbnailheight.Text = model.thumbnailheight.ToString();
            thumbnailwidth.Text = model.thumbnailwidth.ToString();
            thumbnailmode.SelectedValue = model.thumbnailmode;
            watermarktype.SelectedValue = model.watermarktype.ToString();
            watermarkposition.Text = model.watermarkposition.ToString();
            watermarkimgquality.Text = model.watermarkimgquality.ToString();
            //水印图片
            watermarkpic.Text = model.watermarkpic;
            if (!string.IsNullOrEmpty(model.watermarkpic))
            {
                ImgDiv.Visible = true;
                ImgUrl.ImageUrl = model.watermarkpic;
            }
            watermarktransparency.Text = model.watermarktransparency.ToString();
            watermarktext.Text = model.watermarktext;
            watermarkfont.SelectedValue = model.watermarkfont;
            watermarkfontsize.Text = model.watermarkfontsize.ToString();
            if (model.fomatpage == 1)
            {
                chbFomatPage.Checked = true;
            }
            if (model.pagecache == 1)
            {
                chbPageCache.Checked = true;
            }
            txtCacheTime.Text = model.cachetime.ToString();
            txtCacheFix.Text = model.cachefix;
            rblDelTable.SelectedValue = model.deltable.ToString();
            txtAuthor.Text = model.author;
            txtSource.Text = model.source;
            //输出短信数量
            SetSmsCount();
        }
        #endregion

        #region 获取短信数量
        private void SetSmsCount()
        {
            string errorMsg = string.Empty;
            int start,len,count = new BLL.sms_message().GetAccountQuantity(out errorMsg);
            //分析
            start = siteConfig.smsqueryformat.IndexOf("{");
            if (start > 0)
            {
                smsTip.Text = siteConfig.smsqueryformat.Substring(0, start);
                if (!string.IsNullOrWhiteSpace(errorMsg))
                {
                    labSmsCount.InnerText = errorMsg;
                }
                else
                {
                    len = siteConfig.smsqueryformat.Length - start;
                    labSmsCount.InnerText = siteConfig.smsqueryformat.Substring(start, len).Replace("{0}", count.ToString());
                }
            }
        }
        #endregion

        /// <summary>
        /// 保存配置信息
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_config", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.siteconfig bll = new BLL.siteconfig();
            Model.siteconfig model = bll.loadConfig();
            try
            {
                model.webname = webname.Text;
                model.weburl = weburl.Text;
                model.webcompany = webcompany.Text;
                model.webaddress = webaddress.Text;
                model.webtel = webtel.Text;
                model.webfax = webfax.Text;
                model.webmail = webmail.Text;
                model.webcrod = webcrod.Text;

                model.webpath = webpath.Text;
                string managepath = webmanagepath.Text;
                if (model.webmanagepath != managepath)
                {
                    new BLL.navigation().updateicon(model.webmanagepath, managepath);
                    model.webmanagepath = managepath;
                }
                model.staticstatus = Utils.StrToInt(staticstatus.SelectedValue, 0);
                model.staticextension = staticextension.Text;
                if (memberstatus.Checked == true)
                {
                    model.memberstatus = 1;
                }
                else
                {
                    model.memberstatus = 0;
                }
                if (commentstatus.Checked == true)
                {
                    model.commentstatus = 1;
                }
                else
                {
                    model.commentstatus = 0;
                }
                if (logstatus.Checked == true)
                {
                    model.logstatus = 1;
                }
                else
                {
                    model.logstatus = 0;
                }
                if (webstatus.Checked == true)
                {
                    model.webstatus = 1;
                }
                else
                {
                    model.webstatus = 0;
                }
                model.webclosereason = webclosereason.Text;
                model.webcountcode = webcountcode.Text;

                model.smsapiurl = smsapiurl.Text;
                model.smsusername = smsusername.Text;
                //判断密码是否更改
                if (smspassword.Text.Trim() != "" && smspassword.Text.Trim() != defaultpassword)
                {
                    model.smspassword = smspassword.Text.Trim();
                }
                model.smssubmit = smssubmit.Text;
                model.smssendpara = smssendpara.Text.Trim();
                model.smssign = Utils.StrToInt(smssign.SelectedValue, 0);
                model.smssigntxt = smssigntxt.Text.Trim();
                model.smssendanswer = Utils.StrToInt(smssendanswer.SelectedValue, 0);
                model.smssendcode = smssendcode.Text.Trim();
                model.smssendlable = smssendlable.Text.Trim();
                model.smsmark = smsmark.Text.Trim();
                model.smssendcount = Utils.StrToInt(smssendcount.Text.Trim(), 10);
                model.smsqueryapiurl = smsqueryapiurl.Text.Trim();
                model.smsquerypara = smsquerypara.Text.Trim();
                model.smsqueryanswer = Utils.StrToInt(smsqueryanswer.SelectedValue, 0);
                model.smsquerycode = smsquerycode.Text.Trim();
                model.smsqueryformat = smsqueryformat.Text.Trim();
                model.smserrorcode = smserrorcode.Text.Trim();

                model.emailsmtp = emailsmtp.Text;
                if (emailssl.Checked == true)
                {
                    model.emailssl = 1;
                }
                else
                {
                    model.emailssl = 0;
                }
                model.emailport = Utils.StrToInt(emailport.Text.Trim(), 25);
                model.emailfrom = emailfrom.Text;
                model.emailusername = emailusername.Text;
                //判断密码是否更改
                if (emailpassword.Text.Trim() != defaultpassword)
                {
                    model.emailpassword = DESEncrypt.Encrypt(emailpassword.Text, model.sysencryptstring);
                }
                model.emailnickname = emailnickname.Text;

                model.filepath = filepath.Text;
                model.filesave = Utils.StrToInt(filesave.SelectedValue, 2);
                model.fileremote = Utils.StrToInt(fileremote.SelectedValue, 0);
                model.imgextension = imgextension.Text.Trim();
                model.fileextension = fileextension.Text;
                model.videoextension = videoextension.Text;
                model.attachsize = Utils.StrToInt(attachsize.Text.Trim(), 0);
                model.videosize = Utils.StrToInt(videosize.Text.Trim(), 0);
                model.imgsize = Utils.StrToInt(imgsize.Text.Trim(), 0);
                model.imgmaxheight = Utils.StrToInt(imgmaxheight.Text.Trim(), 0);
                model.imgmaxwidth = Utils.StrToInt(imgmaxwidth.Text.Trim(), 0);
                model.thumbnailheight = Utils.StrToInt(thumbnailheight.Text.Trim(), 0);
                model.thumbnailwidth = Utils.StrToInt(thumbnailwidth.Text.Trim(), 0);
                model.thumbnailmode = thumbnailmode.SelectedValue;
                model.watermarktype = Utils.StrToInt(watermarktype.SelectedValue, 0);
                model.watermarkposition = Utils.StrToInt(watermarkposition.Text.Trim(), 9);
                int quality = Utils.StrToInt(watermarkimgquality.Text.Trim(), 80);
                if (quality<10)
                {
                    quality = 10;
                }else if (quality>100)
                {
                    quality = 100;
                }
                model.watermarkimgquality = quality;
                //判断上传图片
                if (this.imgUpload.HasFile)
                {
                    //上传前先删除原图片
                    if (!string.IsNullOrEmpty(model.watermarkpic))
                    {
                        Utils.DeleteFile(model.watermarkpic);
                    }
                    Model.upLoad upfile = new Web.UI.UpLoad().fileSaveAs(this.imgUpload.PostedFile, 0, false, false);
                    if (upfile.status > 0)
                    {
                        model.watermarkpic = upfile.path;
                    }
                }
                else
                {
                    //判断是否需要删除原图
                    if (watermarkpic.Text.Trim() == "" && !string.IsNullOrEmpty(model.watermarkpic))
                    {
                        Utils.DeleteFile(model.watermarkpic);
                    }
                    model.watermarkpic = watermarkpic.Text.Trim();
                }
                model.watermarktransparency = Utils.StrToInt(watermarktransparency.Text.Trim(), 5);
                model.watermarktext = watermarktext.Text;
                model.watermarkfont = watermarkfont.Text;
                model.watermarkfontsize = Utils.StrToInt(watermarkfontsize.Text.Trim(), 12);
                if (chbFomatPage.Checked == true)
                {
                    model.fomatpage = 1;
                }
                else
                {
                    model.fomatpage = 0;
                }
                if (chbPageCache.Checked == true)
                {
                    model.pagecache = 1;
                }
                else
                {
                    model.pagecache = 0;
                }

                model.cachetime = Utils.StrToInt(txtCacheTime.Text.Trim(), 30);
                model.cachefix = txtCacheFix.Text.Trim();
                model.deltable = Utils.StrToInt(rblDelTable.SelectedValue, 0);
                model.author = txtAuthor.Text.Trim();
                model.source = txtSource.Text.Trim();
                bll.saveConifg(model);
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改系统配置信息"); //记录日志
                JscriptMsg("修改系统配置成功！", "sys_config.aspx");
            }
            catch
            {
                JscriptMsg("文件写入失败，请检查文件夹权限！", "");
            }
        }

    }
}