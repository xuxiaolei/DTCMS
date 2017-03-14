using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Article
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
                chkLoginLevel("editArticle");
                TreeBind();
                ShowInfo(this.Id);
            }
        }

        //绑定类别
        private void TreeBind()
        {
            DtCms.BLL.Channel cbll = new DtCms.BLL.Channel();
            DataTable dt = cbll.GetList(0, 1);

            this.ddlClassId.Items.Clear();
            this.ddlClassId.Items.Add(new ListItem("请选择所属类别...", "1"));
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
            DtCms.BLL.Article bll = new DtCms.BLL.Article();
            DtCms.Model.Article model = bll.GetModel(_id);

            txtTitle.Text = model.Title;
            txtAuthor.Text = model.Author;
            txtForm.Text = model.Form;
            txtKeyword.Text = model.Keyword;
            txtZhaiyao.Text = model.Zhaiyao;
            ddlClassId.SelectedValue = model.ClassId.ToString();
            txtImgUrl.Text = model.ImgUrl;
            txtDaodu.Text = model.Daodu;
            FCKeditor.Value = model.Content;
            txtClick.Text = model.Click.ToString();
            if (model.IsMsg == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.IsTop == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            if (model.IsRed == 1)
            {
                cblItem.Items[2].Selected = true;
            }
            if (model.IsHot == 1)
            {
                cblItem.Items[3].Selected = true;
            }
            if (model.IsSlide == 1)
            {
                cblItem.Items[4].Selected = true;
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DtCms.BLL.Article bll = new DtCms.BLL.Article();
                DtCms.Model.Article model = bll.GetModel(this.Id);

                //保存用户头像
                if (!string.IsNullOrEmpty(FileUpload1.FileName))
                {
                    if (FileUpload1.PostedFile.ContentLength / 1024 > 100)
                    {
                        JscriptPrint("上传的视频过大，视频文件要低于100MB！", "", "");
                    }
                    else
                    {
                        string ext = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.'));
                        string spath = @"video/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ext;//相对路径
                        if (ext.ToLower() == ".swf")
                        {
                            FileUpload1.SaveAs(Server.MapPath(spath));
                            model.ImgUrl = spath;
                        }
                        else
                        {
                            JscriptPrint("只支持swf视频文件！", "", "");
                        }
                    }
                }

                model.Title = txtTitle.Text.Trim();
                model.Author = txtAuthor.Text.Trim();
                model.Form = txtForm.Text.Trim();
                model.Keyword = txtKeyword.Text.Trim();
                model.Zhaiyao = StringPlus.DropHTML(txtZhaiyao.Text, 250);
                model.Daodu = StringPlus.DropHTML(txtDaodu.Text, 250);
                model.ClassId = int.Parse(ddlClassId.SelectedValue);
                model.Content = FCKeditor.Value;
                model.Click = int.Parse(txtClick.Text.Trim());

                model.IsMsg = 0;
                model.IsTop = 0;
                model.IsRed = 0;
                model.IsHot = 0;
                model.IsSlide = 0;
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
                bll.Update(model);
                JscriptPrint("编辑成功啦！", "List.aspx", "Success");

            }
            catch (Exception)
            {
                JscriptPrint("视频文件过大！", "", "");
            }
        }

    }
}
