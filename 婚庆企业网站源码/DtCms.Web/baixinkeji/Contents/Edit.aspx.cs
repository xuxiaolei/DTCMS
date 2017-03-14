using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Contents
{
    public partial class Edit : DtCms.Web.UI.ManagePage
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
                chkLoginLevel("editContents");
                TreeBind();
                ShowInfo(this.Id);
            }
        }

        //绑定类别
        private void TreeBind()
        {
            DtCms.BLL.Channel cbll = new DtCms.BLL.Channel();
            DataTable dt = cbll.GetList(0, 3);

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

        //赋值操作
        private void ShowInfo(int _id)
        {
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            DtCms.Model.Contents model = bll.GetModel(_id);
            txtTitle.Text = model.Title;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtSortId.Text = model.SortId.ToString();
            FCKeditor.Value = model.Content;
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            DtCms.Model.Contents model = bll.GetModel(this.Id);
            model.Title = txtTitle.Text.Trim();
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.Content = FCKeditor.Value;
            model.SortId = int.Parse(txtSortId.Text.Trim());
            bll.Update(model);
            JscriptPrint("内容编辑成功啦！", "List.aspx", "Success");
        }

    }
}
