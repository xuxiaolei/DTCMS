using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Manage
{
    public partial class edit :DtCms.Web.UI. ManagePage
    {
        public int Id;
        protected string strLevel;
        protected int strType;

        protected void Page_Load(object sender, EventArgs e)
        {
            chkLoginLevel("editManage");
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
        private void ShowInfo(int editID)
        {
            DtCms.BLL.Admin bll = new DtCms.BLL.Admin();
            DtCms.Model.Admin model = new DtCms.Model.Admin();
            model = bll.GetModel(editID);
            txtUserName.Text = model.UserName;
            if (model.IsLock == 1)
            {
                this.rblIsLock.Items[1].Selected = true;
            }
            else
            {
                this.rblIsLock.Items[0].Selected = true;
            }
            txtReadName.Text = model.ReadName;
            txtUserEmail.Text = model.UserEmail;
            this.strLevel = model.UserLevel;
            this.strType = model.UserType;
            if (model.UserType == 1)
            {
                this.rblUserType.Items[0].Selected = true;
            }
            if (model.UserType == 2)
            {
                this.rblUserType.Items[1].Selected = true;
            }
            if (model.UserType == 3)
            {
                this.rblUserType.Items[2].Selected = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Admin bll = new DtCms.BLL.Admin();
            DtCms.Model.Admin model = bll.GetModel(this.Id);

            string UserPwd = txtUserPwd.Text.Trim();
            string UserLevel = string.Empty;
            int UserType = Convert.ToInt32(rblUserType.SelectedValue);
            if (UserType > 1)
            {
                UserLevel = "," + Request.Form["cbLevel"].Trim() + ",";
            }
            if (UserPwd != null && UserPwd != "")
            {
                model.UserPwd = DtCms.Common.DESEncrypt.Encrypt(UserPwd);
            }
            model.ReadName = txtReadName.Text.Trim();
            model.UserEmail = txtUserEmail.Text.Trim();
            model.UserType = UserType;
            model.IsLock = Convert.ToInt32(rblIsLock.SelectedValue);
            model.UserLevel = UserLevel;

            bll.Update(model);
            JscriptPrint("管理员修改成功啦！", "List.aspx", "Success");
        }
    }
}
