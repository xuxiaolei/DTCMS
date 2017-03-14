using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class templet_list : Web.UI.ManagePage
    {
        protected int site_id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.site_id = DTRequest.GetQueryInt("site_id");

            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.View.ToString()); //检查权限

                //绑定网点
                BLL.channel_site bll = new BLL.channel_site();
                DataTable dt = bll.GetList(0, "", "is_default desc,sort_id asc,id desc").Tables[0];
                this.rblSiteId.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    if (this.site_id == 0)
                    {
                        this.site_id = Utils.StrToInt(dr["id"].ToString(), 0);
                    }
                    this.rblSiteId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
                }
                rblSiteId.SelectedValue = this.site_id.ToString();
                RptBind(this.site_id); //绑定模板
            }
        }

        #region 数据绑定
        private void RptBind(int _site_id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("skinname", Type.GetType("System.String"));
            dt.Columns.Add("version", Type.GetType("System.String"));
            dt.Columns.Add("preview", Type.GetType("System.String"));
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Columns.Add("demo", Type.GetType("System.String"));
            dt.Columns.Add("img", Type.GetType("System.String"));
            dt.Columns.Add("author", Type.GetType("System.String"));
            dt.Columns.Add("description", Type.GetType("System.String"));
            dt.Columns.Add("website", Type.GetType("System.String"));
            dt.Columns.Add("createdate", Type.GetType("System.String"));

            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath("../../templates/"));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                DataRow dr = dt.NewRow();
                Model.template model = GetInfo(dir.FullName);
                if (model != null)
                {
                    dr["skinname"] = dir.Name;  //文件夹名称
                    dr["version"] = model.version;  //模板版本
                    dr["preview"] = model.preview;  //缩略图
                    dr["name"] = model.name;    // 模板名称
                    dr["demo"] = model.demo;    // 演示地址
                    dr["img"] = "../../templates/" + dir.Name + "/about.png";   // 模板图片
                    dr["author"] = model.author;    //作者
                    dr["description"] = model.description;  //版本描述
                    dr["website"] = model.website;  //网站域名
                    dr["createdate"] = model.createdate;    //创建日期
                    dt.Rows.Add(dr);
                }
            }
            
            //获取站点信息
            Model.channel_site modelt = new BLL.channel_site().GetModel(this.site_id);
            if (modelt != null)
            {
                //复制新表
                DataTable dtNew = dt.Clone();
                //重新筛选
                DataRow[] drArr = dt.Select(string.Format("skinname='{0}'", modelt.templet_path));
                for (int i = 0; i < drArr.Length; i++)
                {
                    dtNew.ImportRow(drArr[i]);
                }
                this.rptCurrent.DataSource = dtNew;
                this.rptCurrent.DataBind();

                //复制新表
                DataTable dtNew2 = dt.Clone();
                DataRow[] oldArr = dt.Select(string.Format("skinname<>'{0}'", modelt.templet_path));
                //重新写入表
                for (int i = 0; i < oldArr.Length; i++)
                {
                    dtNew2.ImportRow(oldArr[i]);
                }
                this.rptList.DataSource = dtNew2;
                this.rptList.DataBind();
            }
            else
            {
                this.rptList.DataSource = dt;
                this.rptList.DataBind();
            }
        }
        #endregion

        #region 读取模板配置信息=========================
        /// <summary>
        /// 从模板说明文件中获得模板说明信息
        /// </summary>
        /// <param name="xmlPath">模板路径(不包含文件名)</param>
        /// <returns>模板说明信息</returns>
        private Model.template GetInfo(string xmlPath)
        {
            ///存放关于信息的文件 about.xml是否存在,不存在返回空串
            if (!File.Exists(xmlPath + @"\about.xml"))
            {
                return null;
            }
            try
            {
                return (Model.template)SerializationHelper.Load(typeof(Model.template), xmlPath + @"\about.xml");
            }
            catch
            {
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 选择站点
        /// </summary>
        protected void rblSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _site_id = int.Parse(rblSiteId.SelectedValue);
            Response.Redirect(Utils.CombUrlTxt("templet_list.aspx", "site_id={0}", _site_id.ToString()));
        }

        /// <summary>
        /// 应用模版
        /// </summary>
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.Build.ToString()); //检查权限
            string skinName = ((HiddenField)e.Item.FindControl("hideSkinName")).Value;
            int _site_id = Utils.StrToInt(rblSiteId.SelectedValue, 0);
            switch (e.CommandName)
            {
                case "lbtnStart":
                    //获取站点信息
                    Model.channel_site modelt = new BLL.channel_site().GetModel(_site_id);
                    if (modelt != null)
                    {
                        MarkTemplates(modelt.build_path, skinName, modelt.inherit_id > 0 ? modelt.inherit_id : modelt.id);
                        //修改当前频道分类当前模板名
                        new BLL.channel_site().UpdateField(modelt.id, "templet_path='" + skinName + "'");
                        AddAdminLog(DTEnums.ActionEnum.Build.ToString(), "生成模板:" + skinName);//记录日志
                        JscriptMsg("应用模板成功！", "templet_list.aspx?site_id=" + modelt.id);
                    }
                    break;
            }
            JscriptMsg("应用模板失败！", "templet_list.aspx?site_id=" + _site_id);
        }

        #region 全部生成模板=============================
        /// <summary>
        /// 生成全部模板
        /// </summary>
        /// <param name="buildPath">生成目录</param>
        /// <param name="skinName">模板名称</param>
        /// <param name="site_id">网点ID</param>
        private void MarkTemplates(string buildPath, string skinName, int _site_id)
        {
            //取得ASP目录下的所有文件
            string fullDirPath = Utils.GetMapPath(string.Format("{0}aspx/{1}/", siteConfig.webpath, buildPath));
            //获得URL配置列表
            BLL.url_rewrite bll = new BLL.url_rewrite();
            List<Model.url_rewrite> ls = bll.GetList("");

            DirectoryInfo dirFile = new DirectoryInfo(fullDirPath);
            //删除不属于URL映射表里的文件，防止冗余
            if (Directory.Exists(fullDirPath))
            {
                foreach (FileInfo file in dirFile.GetFiles())
                {
                    //检查文件
                    //Model.url_rewrite modelt = ls.Find(p => p.page.ToLower() == file.Name.ToLower());
                    //if (modelt == null)
                    //{
                    file.Delete();
                    //}
                }
            }

            //遍历URL配置列表
            foreach (Model.url_rewrite modelt in ls)
            {
                //如果URL配置对应的模板文件存在则生成
                string fullPath = Utils.GetMapPath(string.Format("{0}templates/{1}/{2}", siteConfig.webpath, skinName, modelt.templet));
                if (File.Exists(fullPath))
                {
                    //判断是否开启缓存
                    string isCache = string.Empty;
                    string cachePath = string.Empty;
                    if (modelt.cache != null && modelt.cachepath != null)
                    {
                        isCache = modelt.cache;
                        cachePath = modelt.cachepath;
                    }
                    PageTemplate.GetTemplate(siteConfig.webpath, "templates", skinName, modelt.templet, modelt.page, modelt.inherit, buildPath, modelt.channel, modelt.pagesize, isCache, cachePath, siteConfig.cachefix, modelt.type, _site_id, 1);
                }
            }
        }
        #endregion
    }
}