using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Products
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkLoginLevel("addProducts");
                TreeBind();
                if (!string.IsNullOrEmpty(Request.Params["classId"]))
                {
                    ddlClassId.SelectedValue = Request.Params["classId"].Trim();
                }
            }
        }

        //绑定类别
        private void TreeBind()
        {
            DtCms.BLL.Channel cbll = new DtCms.BLL.Channel();
            DataTable dt = cbll.GetList(0, 2);

            this.ddlClassId.Items.Clear();
            this.ddlClassId.Items.Add(new ListItem("请选择所属类别...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["Id"].ToString();
                int ClassLayer = int.Parse(dr["ClassLayer"].ToString());
                string Title = dr["Title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlClassId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = StringPlus.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlClassId.Items.Add(new ListItem(Title, Id));
                }
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.Model.Products model = new DtCms.Model.Products();
            DtCms.BLL.Products bll = new DtCms.BLL.Products();

            model.Title = txtTitle.Text.Trim();
            model.Guige = txtGuige.Text.Trim();
            model.Xinghao = txtXinghao.Text.Trim();
            model.Price = decimal.Parse(txtPrice.Text.Trim());
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.ImgUrl = txtImgUrl.Text.Trim();
            model.Content = FCKeditor.Value;
            model.Click = int.Parse(txtClick.Text.Trim());

            model.IsMsg = 0;
            model.IsTop = 0;
            model.IsRed = 0;
            model.IsHot = 0;
            model.IsSlide = 0;
            model.IsLock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.IsMsg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.IsTop = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.IsRed = 1;
            }
            if (cblItem.Items[3].Selected == true)
            {
                model.IsHot = 1;
            }
            if (cblItem.Items[4].Selected == true)
            {
                model.IsSlide = 1;
            }
            if (cblItem.Items[5].Selected == true)
            {
                model.IsLock = 1;
            }
            bll.Add(model);
            JscriptPrint("发布成功啦！", "Add.aspx?classId=" + ddlClassId.SelectedValue, "Success");
        }

    }
}
