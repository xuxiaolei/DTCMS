using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Advertising
{
    public partial class AdvEdit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                chkLoginLevel("editAdvertising");
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int editID)
        {
            DtCms.BLL.Advertising bll = new DtCms.BLL.Advertising();
            DtCms.Model.Advertising model = bll.GetModel(editID);

            this.txtTitle.Text = model.Title;
            this.rblAdType.SelectedValue = model.AdType.ToString();
            this.txtAdRemark.Text = model.AdRemark;
            this.txtAdNum.Text = model.AdNum.ToString();
            this.txtAdPrice.Text = model.AdPrice.ToString();
            this.txtAdWidth.Text = model.AdWidth.ToString();
            this.txtAdHeight.Text = model.AdHeight.ToString();
            this.rblAdTarget.SelectedValue = model.AdTarget;
        }

        //修改保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Advertising bll = new DtCms.BLL.Advertising();
            DtCms.Model.Advertising model = bll.GetModel(this.Id);

            model.Title = this.txtTitle.Text.Trim();
            model.AdType = int.Parse(this.rblAdType.SelectedValue);
            model.AdRemark = this.txtAdRemark.Text;
            model.AdNum = int.Parse(this.txtAdNum.Text.Trim());
            model.AdPrice = decimal.Parse(this.txtAdPrice.Text);
            model.AdWidth = int.Parse(this.txtAdWidth.Text);
            model.AdHeight = int.Parse(this.txtAdHeight.Text);
            model.AdTarget = this.rblAdTarget.SelectedValue;
            bll.Update(model);
            JscriptPrint("广告位修改成功啦！", "AdvList.aspx", "Success");
        }
    }
}
