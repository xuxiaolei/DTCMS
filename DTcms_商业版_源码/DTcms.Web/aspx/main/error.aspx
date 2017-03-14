<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.error" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>
<script runat="server">
private bool writeHtml = false;
private bool writeCache = false;
private const int site_id = 4;
private string cacheFile = null;
override protected void OnInit(EventArgs e)
{
	cacheFile = "/aspx/main/cache/error.cache";
	if (config.pagecache == 1 && Utils.FileExists(cacheFile)){
		this.Response.Write(PageCache.ReadHtmlFile(cacheFile));
		if (PageCache.CacheFileTime(cacheFile, config.cachetime)){
			this.Response.End();
		}
		else
		{
			writeCache = true;
		}
	}
	else{
		writeHtml = true;
		writeCache = true;
	}
	base.OnInit(e);
}
protected override void Render( HtmlTextWriter writer )
{
	if (this.writeCache)
	{
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>提示信息</title>\r\n<style type=\"text/css\">\r\nbody{padding:0;margin:0;width:100%;height:100%;text-align:center;font-size:12px;font-family:\"Microsoft YaHei\";}\r\na:link,a:visited{text-decoration:none;color:#0068a6;}\r\na:hover,a:active{color:#ff6600;text-decoration: underline}\r\n.showMsg{margin:-100px auto auto -225px;position:absolute;border:1px solid #267cb7;width:450px;top:50%;left:50%;text-align:left;}\r\n.showMsg h5{margin:0;padding:0 0 0 10px;background:#3083eb;color:#fff; height:38px;line-height:38px;overflow:hidden;font-size:14px;text-align:left;}\r\n.showMsg .content{padding:20px;font-size:14px;min-height:84px;_height:84px;background:#fff;}\r\n.showMsg .footer{background:#e4ecf7;line-height:34px;height:34px;text-align:center;}\r\n</style>\r\n</head>\r\n<body>\r\n<div class=\"showMsg\">\r\n	<h5>提示信息</h5>\r\n    <div class=\"content\">\r\n       ");
	templateBuilder.Append(Utils.ObjectToStr(msg));
	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"footer\">\r\n    	<a href=\"javascript:history.back();\" >[点这里返回上一页]</a>\r\n	</div>\r\n</div>\r\n</body>\r\n</html>");
	if (this.writeHtml){
		this.Response.Write(templateBuilder.ToString());
	}
	if (config.pagecache == 1){
		PageCache.WriteHtmlFile(config.pagecache, config.fomatpage, cacheFile, templateBuilder.ToString(), config.cachetime);
	}
	this.Response.End();
	}
	else
	{
		base.Render(writer);
	}
}
</script>
