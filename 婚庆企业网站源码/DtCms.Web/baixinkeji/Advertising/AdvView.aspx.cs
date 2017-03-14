using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Advertising
{
    public partial class AdvView : DtCms.Web.UI.ManagePage
    {
        public int id;
        public DtCms.Model.Advertising model = new DtCms.Model.Advertising();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要调用的广告不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                DtCms.BLL.Advertising bll = new DtCms.BLL.Advertising();
                model = bll.GetModel(this.id);
            }
        }

        #region 显示广告类型名称
        protected string GetTypeName(string strId)
        {
            switch (strId)
            {
                case "1":
                    return "文字";
                case "2":
                    return "图片";
                case "3":
                    return "幻灯片";
                case "4":
                    return "动画";
                case "5":
                    return "FLV视频";
                case "6":
                    return "代码";
                default:
                    return "其它";
            }
        }
        #endregion
    }
}
