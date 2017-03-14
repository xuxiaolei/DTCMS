using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web
{
    public partial class Company : DtCms.Web.UI.BasePage
    {
        protected int Id;//全局变量ID
        protected string bodyId;
        protected DtCms.Model.Contents model = new DtCms.Model.Contents();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["Id"] as string, out this.Id))
            {
                this.Id=1;
            }
            switch(this.Id)
            {
                case 6:
                    this.bodyId = " id=\"about\"";
                    break;
                case 2:
                    this.bodyId = " id=\"service\"";
                    break;
                case 3:
                    this.bodyId = " id=\"contact\"";
                    break;
                default:
                    this.bodyId = " id=\"home\"";
                    break;
            }
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            model = bll.GetModel(this.Id);
        }
    }
}
