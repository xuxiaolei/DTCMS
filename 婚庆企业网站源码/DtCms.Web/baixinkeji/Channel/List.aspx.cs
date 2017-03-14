using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DtCms.Web.UI;

namespace DtCms.Web.Admin.Channel
{
    public partial class List : ManagePage
    {
        public int kindId; //栏目种类
        DtCms.BLL.Channel bll = new DtCms.BLL.Channel();
        protected void Page_Load(object sender, EventArgs e)
        {
            //取得栏目传参
            if (int.TryParse(Request.Params["kindId"], out kindId))
            {
                if (!Page.IsPostBack)
                {
                    chkLoginLevel("viewChannel");
                    BindData();
                }
            }
            else
            {
                JscriptMsg(350, 230, "错误提示", "<b>出现错误啦！</b>您要管理的类别种类不明确或参数不正确。", "back", "Error");
            }
        }

        //数据绑定
        private void BindData()
        {
            DataTable dt = bll.GetList(0, kindId);
            this.rptClassList.DataSource = dt;
            this.rptClassList.DataBind();
        }

        //删除操作
        protected void rptClassList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HiddenField txtClassId = (HiddenField)e.Item.FindControl("txtClassId");
            switch (e.CommandName.ToLower())
            {
                case "btndel":
                    bll.Delete(Convert.ToInt32(txtClassId.Value));
                    BindData();
                    JscriptPrint("批量删除成功啦！", "", "Success");
                    break;
            }
        }
        //美化列表
        protected void rptClassList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField txtClassLayer = (HiddenField)e.Item.FindControl("txtClassLayer");
                string LitStyle = "<span style=width:{0}px;text-align:right;display:inline-block;>{1}{2}</span>";
                string LitImg1 = "<img src=../images/folder_open.gif align=absmiddle />";
                string LitImg2 = "<img src=../images/t.gif align=absmiddle />";

                int classLayer = Convert.ToInt32(txtClassLayer.Value);
                if (classLayer == 1)
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle, classLayer * 18, LitImg2, LitImg1);
                }
            }
        }
    }
}
