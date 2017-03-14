using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DtCms.Web.Admin.Links
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            chkLoginLevel("addLinks");
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.Model.Links model = new DtCms.Model.Links();
            DtCms.BLL.Links bll = new DtCms.BLL.Links();

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
            bll.Add(model);
            JscriptPrint("链接增加成功啦！", "Add.aspx", "Success");
        }
    }
}
