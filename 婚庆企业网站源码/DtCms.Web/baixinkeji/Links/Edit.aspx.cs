using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Links
{
    public partial class Edit : DtCms.Web.UI.ManagePage
    {
        public int Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            chkLoginLevel("editLinks");
            if (!int.TryParse(Request.Params["id"] as string, out this.Id))
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改的信息不存在或参数不正确。", "back", "Error");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(this.Id);
            }
        }

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            DtCms.Model.Links model = bll.GetModel(_id);
            txtTitle.Text = model.Title;
            txtWebUrl.Text = model.WebUrl;
            if (model.IsImage == 1)
            {
                cbIsImage.Checked = true;
            }
            txtImgUrl.Text = model.ImgUrl;
            txtUserName.Text = model.UserName;
            txtUserTel.Text = model.UserTel;
            txtUserMail.Text = model.UserMail;
            if (model.IsRed == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.IsLock == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            txtSortId.Text = model.SortId.ToString();
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Links bll = new DtCms.BLL.Links();
            DtCms.Model.Links model = bll.GetModel(this.Id);
            model.Title = txtTitle.Text.Trim();
            model.WebUrl = txtWebUrl.Text.Trim();
            model.IsImage = 0;
            if (cbIsImage.Checked == true)
            {
                model.IsImage = 1;
            }
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.UserName = txtUserName.Text.Trim();
            model.UserTel = txtUserTel.Text.Trim();
            model.UserMail = txtUserMail.Text.Trim();
            model.SortId = int.Parse(txtSortId.Text.Trim());
            model.IsRed = 0;
            model.IsLock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.IsRed = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.IsLock = 1;
            }
            bll.Update(model);
            JscriptPrint("链接编辑成功啦！", "List.aspx", "Success");
        }
    }
}
