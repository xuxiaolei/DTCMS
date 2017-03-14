using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

namespace DtCms.Web.Admin.Advertising
{
    public partial class BarEdit : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["id"] + ""))
                    {
                        XmlDocument xmldoc1 = new XmlDocument();
                        xmldoc1.Load(Server.MapPath("./../../xml/images.xml"));
                        XmlNodeList nodelist1 = xmldoc1.SelectNodes("//root/menu");
                        foreach (XmlNode nl in nodelist1)
                        {
                            if (nl.Attributes[0].Value == Request.QueryString["id"].ToString())
                            {
                                this.txtTitle.Text = nl.Attributes[1].Value;
                                this.txtLinkUrl.Text = nl.Attributes[2].Value;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
                {
                    JscriptPrint("名称不能为空！", "", "");
                }
                else
                {
                    string spath = "";

                    if (!string.IsNullOrEmpty(FileUpload1.FileName))
                    {
                        string ext = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.'));
                        spath = @"/images/flaimg/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ext;//相对路径
                        if (ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                        {
                            FileUpload1.SaveAs(Server.MapPath(spath));
                        }
                        else
                        {
                            JscriptPrint("上传图片格式错误，只支持jpg,gif,png格式图片！", "", "");
                        }
                    }


                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(Server.MapPath("./../../xml/images.xml"));
                    XmlNodeList nodelist1 = xmldoc.SelectNodes("//root/menu");
                    foreach (XmlNode nl in nodelist1)
                    {
                        if (nl.Attributes[0].Value == Request.QueryString["id"].ToString())
                        {
                            XmlElement element = (XmlElement)nl;

                            element.SetAttribute("title", this.txtTitle.Text.Trim());
                            element.SetAttribute("url", this.txtLinkUrl.Text.Trim());
                            if (!string.IsNullOrEmpty(spath))
                            {
                                element.SetAttribute("imageUrl", spath);
                            }
                        }
                    }

                    xmldoc.Save(Server.MapPath("./../../xml/images.xml"));

                    JscriptPrint("发布成功啦！", "BarList.aspx?Pid=1", "Success");


                }
            }
            catch (Exception)
            {
            }
        }
    }
}
