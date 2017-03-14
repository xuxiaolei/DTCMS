using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;


namespace DTcms.Web.Plugin.Baseback.admin
{
    public partial class index : DTcms.Web.UI.ManagePage
    {
        Model.siteconfig config = new BLL.siteconfig().loadConfig();

        protected void Page_Load(object sender, EventArgs e)
        {
            RptBind();

            if (!Page.IsPostBack)
            {
                ChkAdminLevel("plugin_baseback", DTEnums.ActionEnum.View.ToString()); //检查权限
            }
        }

        #region 数据绑定=================================
        private void RptBind()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("file_name", Type.GetType("System.String"));
            dt.Columns.Add("file_size", Type.GetType("System.String"));
            dt.Columns.Add("upload_date", Type.GetType("System.String"));

            string dirPath = Utils.GetMapPath(config.webpath + siteConfig.webmanagepath + "/backups/");
            if (Directory.Exists(dirPath))
            {
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                FileInfo[] arrFiles = dir.GetFiles();
                foreach (FileInfo f in arrFiles)
                {
                    DataRow dr = dt.NewRow();
                    dr["file_name"] = f.Name;
                    dr["file_size"] = Utils.CountSize(Convert.ToInt32(f.Length));
                    dr["upload_date"] = f.LastWriteTime;
                    dt.Rows.Add(dr);
                }
            }
            rptList.DataSource = dt;
            rptList.DataBind();
        }
        #endregion

        /// <summary>
        /// 备份数据库
        /// </summary>
        protected void lbtnBack_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_baseback", DTEnums.ActionEnum.Back.ToString()); //检查权限
            try
            {
                string str_filename = Common.Utils.GetRamCode();
                string backpath = Utils.GetMapPath(config.webpath + siteConfig.webmanagepath + "/backups/");
                string filename = backpath + "#" + str_filename + ".bak";
                if (!Directory.Exists(backpath))
                    Directory.CreateDirectory(backpath);
                string dbCstring = DBUtility.DbHelperSQL.connectionString;
                string[] a_dbNamestring = dbCstring.Split(';');
                string[] a_dbNameS = a_dbNamestring[3].ToString().Split('=');
                string Sql = "backup DATABASE [" + a_dbNameS[1].ToString() + "] to disk='" + filename + "' with format";
                DBUtility.DbHelperSQL.ExecuteSql(Sql);

                AddAdminLog(DTEnums.ActionEnum.Back.ToString(), "备份数据库文件:" + filename); //记录日志
                JscriptMsg("数据备份成功！", "index.aspx");
            }
            catch (Exception ex)
            {
                JscriptMsg("备份失败！数据库与网站须同一服务器！", "index.aspx");
            }
        }

        /// <summary>
        /// 下载与删除
        /// </summary>
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string filename = ((HiddenField)e.Item.FindControl("hideName")).Value;
            string backpath = Utils.GetMapPath(config.webpath + siteConfig.webmanagepath + "/backups/") + filename;
            switch (e.CommandName)
            {
                case "lbtnRestore":
                    ChkAdminLevel("plugin_baseback", DTEnums.ActionEnum.Restore.ToString()); //检查权限
                    AddAdminLog(DTEnums.ActionEnum.Restore.ToString(), "备份数据库备份文件：" + filename); //记录日志

                    FileStream fileStream = new FileStream(backpath, FileMode.Open);
                    long fileSize = fileStream.Length;
                    byte[] fileBuffer = new byte[fileSize];
                    fileStream.Read(fileBuffer, 0, (int)fileSize);
                    fileStream.Close();
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                    Response.AddHeader("Content-Length", fileSize.ToString());
                    Response.BinaryWrite(fileBuffer);
                    Response.End();
                    Response.Close();
                    break;
                case "lbtnDelete":
                    ChkAdminLevel("plugin_baseback", DTEnums.ActionEnum.Delete.ToString()); //检查权限
                    if (File.Exists(backpath))
                    {
                        File.Delete(backpath);
                        AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除数据备份文件：" + filename); //记录日志
                    }
                    JscriptMsg("删除数据成功！", "index.aspx");
                    break;
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_baseback", DTEnums.ActionEnum.Delete.ToString()); //检查权限

            int sucCount = 0;
            int errorCount = 0;
            string backpath = Utils.GetMapPath(config.webpath + siteConfig.webmanagepath + "/backups/");

            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string fileName = ((HiddenField)rptList.Items[i].FindControl("hideName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (File.Exists(backpath + fileName))
                    {
                        File.Delete(backpath + fileName);
                        sucCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除数据库备份文件" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", "index.aspx");
        }
    }
}