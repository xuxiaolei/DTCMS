using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Common;

namespace DtCms.Web.Admin.Article
{
    public partial class Add : DtCms.Web.UI.ManagePage
    {
        public DtCms.BLL.Article bll = new DtCms.BLL.Article();
        public DtCms.Model.Article model = new DtCms.Model.Article();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                chkLoginLevel("addArticle");
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

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
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

                            model.Title = txtTitle.Text.Trim();
                            model.Author = txtAuthor.Text.Trim();
                            model.Form = txtForm.Text.Trim();
                            //自动提取关健字
                            if (txtKeyword.Text.Trim() != string.Empty)
                            {
                                model.Keyword = txtKeyword.Text.Trim();
                            }
                            else
                            {
                                model.Keyword = txtTitle.Text.Trim();
                            }
                            //自动提取摘要
                            if (txtZhaiyao.Text.Trim() != string.Empty)
                            {
                                model.Zhaiyao = StringPlus.DropHTML(txtZhaiyao.Text, 250);
                            }
                            else
                            {
                                model.Zhaiyao = StringPlus.DropHTML(FCKeditor.Value, 250);
                            }
                            //自动提取导读
                            if (txtDaodu.Text.Trim() != string.Empty)
                            {
                                model.Daodu = StringPlus.DropHTML(txtDaodu.Text, 250);
                            }
                            else
                            {
                                model.Daodu = StringPlus.DropHTML(FCKeditor.Value, 250);
                            }
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
                            bll.Add(model);
                            JscriptPrint("发布成功啦！", "Add.aspx?classId=" + ddlClassId.SelectedValue, "Success");
                        }
                        else
                        {
                            JscriptPrint("只支持swf视频文件！", "", "");
                        }
                    }
                }
                else
                {
                    JscriptPrint("请上传视频文件！", "", "");
                }
            }
            catch (Exception)
            {
                JscriptPrint("视频文件过大！", "", "");
            }
        }

    }
}
