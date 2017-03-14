using System;
using System.Data;
using System.Web;
using DtCms.Web.UI;

namespace DtCms.Web.Admin.Channel
{
    public partial class Edit : ManagePage
    {
        private DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
        private DtCms.Model.Channel model = new DtCms.Model.Channel();
        public int kindId; //种类
        public int classId;    //ID
        protected void Page_Load(object sender, EventArgs e)
        {
            chkLoginLevel("editChannel");
            //取得栏目传参
            if (int.TryParse(Request.Params["kindId"], out kindId) && int.TryParse(Request.Params["classId"], out classId))
            {
                model = bll.GetModel(classId);
                if (!Page.IsPostBack)
                {
                    ShowInfo();
                }
            }
            else
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要修改类别的种类不明确或参数不正确。", "back", "Error");
            }
        }

        //绑定数据
        private void ShowInfo()
        {
            this.lblPid.Text = model.ParentId.ToString();
            if (model.ParentId>0)
            {
                this.lblPname.Text = bll.GetChannelTitle(model.ParentId);
            }
            else
            {
                this.lblPname.Text = "顶级类别";
            }
            this.txtTitle.Text = model.Title;
            this.txtPageUrl.Text = model.PageUrl;
            this.txtClassOrder.Text = model.ClassOrder.ToString();
        }

        //保存修改
        protected void btnSave_Click(object sender, EventArgs e)
        {
            model.Title = txtTitle.Text.Trim();
            model.PageUrl = txtPageUrl.Text.Trim();
            model.ClassOrder = int.Parse(txtClassOrder.Text.Trim());
            //修改栏目
            bll.Update(model);
            JscriptPrint("类别修改成功啦！", "List.aspx?kindId=" + kindId, "Success");
        }

    }
}
