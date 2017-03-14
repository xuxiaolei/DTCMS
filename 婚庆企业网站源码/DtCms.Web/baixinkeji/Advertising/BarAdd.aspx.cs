using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

namespace DtCms.Web.Admin.Advertising
{
    public partial class BarAdd : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
                {
                    JscriptPrint("名称不能为空！", "", "");
                }
                else if (string.IsNullOrEmpty(FileUpload1.FileName))
                {
                    JscriptPrint("上传图片不能为空！", "", "");
                }
                else
                {
                    string ext = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.'));
                    string spath = @"/images/flaimg/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ext;//相对路径

                    if (ext.ToLower() == ".jpg" || ext.ToLower() == ".gif" || ext.ToLower() == ".png")
                    {
                        FileUpload1.SaveAs(Server.MapPath(spath));

                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.Load(Server.MapPath("./../../xml/images.xml"));
                        XmlNode root = xmldoc.SelectSingleNode("root");//查找<bookstore>

                        XmlElement element = xmldoc.CreateElement("menu");
                        element.SetAttribute("id", DateTime.Now.ToString("yyyyMMddhhmmss"));
                        element.SetAttribute("title", this.txtTitle.Text.Trim());
                        element.SetAttribute("url", this.txtLinkUrl.Text.Trim());
                        element.SetAttribute("frame", "_parent");
                        element.SetAttribute("imageUrl", spath);

                        root.AppendChild(element);
                        xmldoc.Save(Server.MapPath("./../../xml/images.xml"));

                        JscriptPrint("发布成功啦！", "BarAdd.aspx?Pid=1", "Success");
                    }
                    else
                    {
                        JscriptPrint("上传图片格式错误，只支持jpg,gif,png格式图片！", "", "");
                    }

                }
            }
            catch (Exception)
            {
            }
        }
    }
}
