using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Contents
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkLoginLevel("addContents");
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

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DtCms.BLL.Contents bll = new DtCms.BLL.Contents();
            DtCms.Model.Contents model = new DtCms.Model.Contents();
            model.Title = txtTitle.Text.Trim();
            model.ClassId = int.Parse(ddlClassId.SelectedValue);
            model.Content = FCKeditor.Value;
            model.SortId = int.Parse(txtSortId.Text.Trim());
            bll.Add(model);
            JscriptPrint("内容添加成功啦！", "Add.aspx?classId=" + ddlClassId.SelectedValue, "Success");
        }
    }
}
