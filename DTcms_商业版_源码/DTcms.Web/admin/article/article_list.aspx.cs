using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.article
{
    public partial class article_list : Web.UI.ManagePage
    {
        protected int channel_id;
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int category_id;
        protected string channel_name = string.Empty;
        protected string property = string.Empty;
        protected string keywords = string.Empty;
        protected string prolistview = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            this.category_id = DTRequest.GetQueryInt("category_id");
            this.keywords = DTRequest.GetQueryString("keywords");
            this.property = DTRequest.GetQueryString("property");

            if (channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back");
                return;
            }
            Model.channel model = new BLL.channel().GetModel(this.channel_id);
            this.channel_name = model.name;
            this.pageSize = GetPageSize(10);
            this.prolistview = Utils.GetCookie("article_list_view_" + this.channel_name);
            if (string.IsNullOrEmpty(this.prolistview) && model.is_type > 0)
            {
                this.prolistview = "Txt";
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                //添加权限
                if (!ChkAuthority("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Add.ToString()))
                {
                    this.addBtnPannel.Visible = false;
                }
                //修改权限
                if (!ChkAuthority("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Edit.ToString()))
                {
                    this.editBtnPannel.Visible = this.ddlMoveId.Visible = false;
                }
                //审核权限
                if (!ChkAuthority("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Audit.ToString()))
                {
                    this.auditBtnPannel.Visible = false;
                }
                //删除权限
                if (!ChkAuthority("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Delete.ToString()))
                {
                    this.delBtnPannel.Visible = false;
                }
                string return_term = string.Empty;
                TreeBind(this.channel_id, out return_term); //绑定类别
                WeiXinBind(); //绑定微信公众号

                if (!string.IsNullOrEmpty(return_term))
                {
                    return_term = "status<2 and category_id in (" + return_term + ")";
                }
                else
                {
                    return_term = "status<2";
                }
                RptBind(this.channel_id, this.category_id, return_term + CombSqlTxt(this.keywords, this.property), "sort_id asc,add_time desc,id desc");
            }
        }

        #region 绑定类别
        private void TreeBind(int _channel_id, out string return_term)
        {
            BLL.article_category bll = new BLL.article_category();
            return_term = string.Empty;
            string strWhere = string.Empty;
            //获取管理员
            Model.manager adminModel = GetAdminInfo();
            if (adminModel.role_type != 1)
            {
                List<int> idlist = new List<int>();
                //获取频道名称
                string channel_name = new BLL.channel().GetChannelName(this.channel_id);
                //获取权限列表
                string nav_name = "channel_" + channel_name + "_category_";
                DataTable dt2 = new BLL.manager_role_value().GetList(0, string.Format("role_id={0} and action_type='Show' and nav_name like '{1}%'", adminModel.role_id, nav_name), "").Tables[0];
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        idlist.Add(int.Parse(dr["nav_name"].ToString().Replace(nav_name, string.Empty)));
                    }
                }
                else
                {
                    idlist.Add(0);
                }
                return_term = string.Join(",", idlist);
                strWhere = string.Format("id in ({0})", return_term);
            }
            DataTable dt = bll.GetList(0, this.channel_id, strWhere);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("所有类别", ""));
            this.ddlMoveId.Items.Clear();
            this.ddlMoveId.Items.Add(new ListItem("批量移动...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                    this.ddlMoveId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                    this.ddlMoveId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 绑定微信公众号===========================
        private void WeiXinBind()
        {
            Model.weixin_account model = new BLL.weixin_account().GetModel();
            if (model == null || model.is_push == 0)
            {
                this.btnWxPost.Visible = false;
            }
        }
        #endregion

        #region 数据绑定
        private void RptBind(int _channel_id, int _category_id, string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.category_id > 0)
            {
                this.ddlCategoryId.SelectedValue = _category_id.ToString();
            }
            this.ddlProperty.SelectedValue = this.property;
            this.txtKeywords.Text = this.keywords;
            //图表或列表显示
            BLL.article bll = new BLL.article();
            switch (this.prolistview)
            {
                case "Txt":
                    this.rptList2.Visible = false;
                    this.rptList1.DataSource = bll.GetList(_channel_id, _category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
                    this.rptList1.DataBind();
                    break;
                default:
                    this.rptList1.Visible = false;
                    this.rptList2.DataSource = bll.GetList(_channel_id, _category_id, this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
                    this.rptList2.DataBind();
                    break;
            }
            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                _channel_id.ToString(), _category_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like '%" + _keywords + "%'");
            }
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        strTemp.Append(" and status=1");
                        break;
                    case "unIsLock":
                        strTemp.Append(" and status=0");
                        break;
                    case "isMsg":
                        strTemp.Append(" and is_msg=1");
                        break;
                    case "isTop":
                        strTemp.Append(" and is_top=1");
                        break;
                    case "isRed":
                        strTemp.Append(" and is_red=1");
                        break;
                    case "isHot":
                        strTemp.Append(" and is_hot=1");
                        break;
                    case "isSlide":
                        strTemp.Append(" and is_slide=1");
                        break;
                }
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回图文每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("article_page_size", "DTcmsPage"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 替换内容图片路径=========================
        private string ReplaceImagesPath(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            Regex reg = new Regex("IMG[^>]*?src\\s*=\\s*(?:\"(?<1>[^\"]*)\"|'(?<1>[^\']*)')", RegexOptions.IgnoreCase);
            MatchCollection m = reg.Matches(content);
            foreach (Match math in m)
            {
                string imgUri = math.Groups[1].Value;
                //如果是本地图片，则加上http://网址/网站安装目录/上传目录/文件名
                if (imgUri.StartsWith(siteConfig.webpath + siteConfig.filepath + "/"))
                {
                    string newImgPath = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + imgUri;
                    content = content.Replace(imgUri, newImgPath);
                }
            }
            return content;
        }
        #endregion

        #region 返回站点链接网址=========================
        private string GetLinkDomain()
        {
            Model.channel channelModel = new BLL.channel().GetModel(this.channel_id);
            Model.channel_site siteModel = new BLL.channel_site().GetModel(channelModel.site_id);
            if (siteModel == null || string.IsNullOrEmpty(siteModel.domain))
            {
                return siteConfig.weburl;
            }
            return siteModel.domain;
        }
        #endregion

        //设置操作
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            int id = Convert.ToInt32(((HiddenField)e.Item.FindControl("hidId")).Value);
            BLL.article bll = new BLL.article();
            Model.article model = bll.GetModel(id);
            switch (e.CommandName)
            {
                case "lbtnIsMsg":
                    if (model.is_msg == 1)
                        bll.UpdateField(id, "is_msg=0");
                    else
                        bll.UpdateField(id, "is_msg=1");
                    break;
                case "lbtnIsTop":
                    if (model.is_top == 1)
                        bll.UpdateField(id, "is_top=0");
                    else
                        bll.UpdateField(id, "is_top=1");
                    break;
                case "lbtnIsRed":
                    if (model.is_red == 1)
                        bll.UpdateField(id, "is_red=0");
                    else
                        bll.UpdateField(id, "is_red=1");
                    break;
                case "lbtnIsHot":
                    if (model.is_hot == 1)
                        bll.UpdateField(id, "is_hot=0");
                    else
                        bll.UpdateField(id, "is_hot=1");
                    break;
                case "lbtnIsSlide":
                    if (model.is_slide == 1)
                        bll.UpdateField(id, "is_slide=0");
                    else
                        bll.UpdateField(id, "is_slide=1");
                    break;
            }
            this.RptBind(this.channel_id, this.category_id, "id>0" + CombSqlTxt(this.keywords, this.property), "sort_id asc,add_time desc,id desc");
        }

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), txtKeywords.Text, this.property));
        }

        //筛选类别
        protected void ddlCategoryId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), ddlCategoryId.SelectedValue, this.keywords, this.property));
        }

        //筛选属性
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
               this.channel_id.ToString(), this.category_id.ToString(), this.keywords, ddlProperty.SelectedValue));
        }

        //设置文字列表显示
        protected void lbtnViewTxt_Click(object sender, EventArgs e)
        {
            Utils.WriteCookie("article_list_view_" + this.channel_name, "Txt", 14400);
            Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, this.page.ToString()));
        }

        //设置图文列表显示
        protected void lbtnViewImg_Click(object sender, EventArgs e)
        {
            Utils.WriteCookie("article_list_view_" + this.channel_name, "Img", 14400);
            Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, this.page.ToString()));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("article_page_size", "DTcmsPage", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            if (this.rptList1.Visible == true)
            {
                rptList = this.rptList1;
            }
            else
            {
                rptList = this.rptList2;
            }
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存" + this.channel_name + "频道内容排序"); //记录日志
            JscriptMsg("保存排序成功！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //批量移动
        protected void ddlMoveId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            int sucCount = 0; //成功数量
            if (ddlMoveId.SelectedValue == "")
            {
                ddlMoveId.SelectedIndex = 0;
                JscriptMsg("请选择要移动的类别！", string.Empty);
                return;
            }
            BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            if (this.rptList1.Visible == true)
            {
                rptList = this.rptList1;
            }
            else
            {
                rptList = this.rptList2;
            }
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    sucCount++;
                    bll.UpdateField(id, "category_id=" + ddlMoveId.SelectedValue);
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "批量移动" + this.channel_name + "频道内容分类"); //记录日志
            JscriptMsg("批量移动成功" + sucCount + "条", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //批量审核
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Audit.ToString()); //检查权限
            BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            if (this.rptList1.Visible == true)
            {
                rptList = this.rptList1;
            }
            else
            {
                rptList = this.rptList2;
            }
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "status=0");
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Audit.ToString(), "审核" + this.channel_name + "频道内容信息"); //记录日志
            JscriptMsg("批量审核成功！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //加入回收站
        protected void btnRecycle_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            if (this.rptList1.Visible == true)
            {
                rptList = this.rptList1;
            }
            else
            {
                rptList = this.rptList2;
            }
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "status=2");
                    sucCount++;
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除到回收站" + this.channel_name + "频道内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除到回收站" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            if (this.rptList1.Visible == true)
            {
                rptList = this.rptList1;
            }
            else
            {
                rptList = this.rptList2;
            }
            //获取站点信息
            Model.channel_site site = new BLL.channel_site().GetSiteModel(this.channel_id);
            //循环删除
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除" + this.channel_name + "频道内容成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property));
        }

        //微信推送
        protected void btnWxPost_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            //检查该微信公众号是否开启消息推送功能
            Model.weixin_account wxModel = new BLL.weixin_account().GetModel();
            if (wxModel == null || wxModel.is_push == 0)
            {
                JscriptMsg("微信账户未开启消息推送！", string.Empty);
                return;
            }
            string errmsg = string.Empty; //错误消息
            string linkdomain = GetLinkDomain(); //链接网址
            List<Model.article> artls = new List<Model.article>(); //选中文章的实体
            API.Weixin.Common.CRMComm wxComm = new API.Weixin.Common.CRMComm();
            List<Senparc.Weixin.MP.AdvancedAPIs.GroupMessage.NewsModel> ls = new List<Senparc.Weixin.MP.AdvancedAPIs.GroupMessage.NewsModel>();

            BLL.article bll = new BLL.article();
            Repeater rptList = new Repeater();
            if (this.rptList1.Visible == true)
            {
                rptList = this.rptList1;
            }
            else
            {
                rptList = this.rptList2;
            }
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Model.article model = bll.GetWXModel(id); //获取文章实体
                    if (model == null || string.IsNullOrEmpty(model.img_url.Trim()))
                    {
                        JscriptMsg("错误：所选择信息有些没有图片！", string.Empty);
                        return;
                    }
                    artls.Add(model);
                }
            }

            //判断是否超出推送的数量
            if (artls.Count == 0 || artls.Count > 10)
            {
                JscriptMsg("错误：推送消息数量应在1-10条范围！", string.Empty);
                return;
            }
            //上传及群发消息
            foreach (Model.article modelt in artls)
            {
                //上传永久素材获取media_id
                string mediaId = wxComm.UploadForeverMedia(Utils.GetMapPath(modelt.img_url), out errmsg);
                if (string.IsNullOrEmpty(mediaId))
                {
                    JscriptMsg("错误：" + errmsg, string.Empty);
                    return;
                }
                //添加消息实体
                Senparc.Weixin.MP.AdvancedAPIs.GroupMessage.NewsModel newsModel = new Senparc.Weixin.MP.AdvancedAPIs.GroupMessage.NewsModel();
                newsModel.thumb_media_id = mediaId; //图文消息缩略图的media_id
                newsModel.title = modelt.title; //图文消息的标题
                newsModel.content_source_url = linkdomain; //点击“阅读原文”后的页面
                newsModel.content = ReplaceImagesPath(modelt.content); //图文消息页面的内容，替换图片路径
                newsModel.digest = modelt.zhaiyao; //图文消息的描述
                newsModel.show_cover_pic = "1"; //是否显示封面，1为显示，0为不显示
                ls.Add(newsModel); //添加实体到泛型
            }
            //开始群发消息
            bool result = wxComm.SendGroupMessageByGroupId(ls, out errmsg);
            if (!result)
            {
                JscriptMsg("错误：" + errmsg, string.Empty);
                return;
            }
            JscriptMsg("微信消息推送成功！", Utils.CombUrlTxt("article_list.aspx", "channel_id={0}&category_id={1}&keywords={2}&property={3}&page={4}",
                this.channel_id.ToString(), this.category_id.ToString(), this.keywords, this.property, this.page.ToString()));
        }
    }
}