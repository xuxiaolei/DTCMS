using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Feedback
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        public DtCms.Model.Feedback model = new DtCms.Model.Feedback();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要回复的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                chkLoginLevel("editFeedback");
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
            model = bll.GetModel(_id);
            txtReContent.Text = DtCms.Common.StringPlus.ToTxt(model.ReContent);
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Feedback bll = new DtCms.BLL.Feedback();
            DtCms.Model.Feedback model = bll.GetModel(this.Id);
            model.ReContent = DtCms.Common.StringPlus.ToHtml(txtReContent.Text);
            model.ReTime = DateTime.Now;
            bll.Update(model);
            JscriptPrint("留言回复成功啦！", "List.aspx", "Success");
        }
    }
}
