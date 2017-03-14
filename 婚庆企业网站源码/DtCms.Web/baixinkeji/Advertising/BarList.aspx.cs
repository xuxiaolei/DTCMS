using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;

namespace DtCms.Web.Admin.Advertising
{
    public partial class BarList : DtCms.Web.UI.ManagePage
    {
        protected string list = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["id"] + ""))
                    {
                        XmlDocument xmldoc = new XmlDocument();
                        xmldoc.Load(Server.MapPath("./../../xml/images.xml"));
                        XmlNodeList nodelist = xmldoc.SelectNodes("//root/menu");
                        foreach (XmlNode nl in nodelist)
                        {
                            if (nl.Attributes[0].Value == Request.QueryString["id"].ToString())
                            {
                                xmldoc.DocumentElement.RemoveChild(nl);
                            }
                        }
                        xmldoc.Save(Server.MapPath("./../../xml/images.xml"));

                        JscriptPrint("删除成功啦！", "BarList.aspx", "Success");
                    }

                    XmlDocument xmldoc1 = new XmlDocument();
                    xmldoc1.Load(Server.MapPath("./../../xml/images.xml"));
                    XmlNodeList nodelist1 = xmldoc1.SelectNodes("//root/menu");
                    foreach (XmlNode nl in nodelist1)
                    {
                        list += "<tr><td align=\"center\">" + nl.Attributes[1].Value + "</td><td align=\"center\"><span><a href='BarEdit.aspx?id=" + nl.Attributes[0].Value + "'>编辑</a>&nbsp;&nbsp;&nbsp;<a href='BarList.aspx?id=" + nl.Attributes[0].Value + "'>删除</a></span></td>";
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
