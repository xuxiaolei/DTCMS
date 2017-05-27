<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.article_show" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by DTcms Template Engine at 2017/4/24 20:35:56.
		本页面代码由DTcms模板引擎生成于 2017/4/24 20:35:56. 
	*/

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);
	const string channel = "news";

	templateBuilder.Append("<!DOCTYPE html>\r\n<html>\r\n<head>\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">\r\n    <meta content=\"yes\" name=\"apple-mobile-web-app-capable\" />\r\n    <meta content=\"black\" name=\"apple-mobile-web-app-status-bar-style\" />\r\n    <meta content=\"telephone=no\" name=\"format-detection\" />\r\n    <meta name=\"author\" content=\"dt猫（www.dtmao.cc）\" />\r\n    <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/css/lib.css\">\r\n    <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/css/style.css\">\r\n    <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/upload/71/71.css\">\r\n    <script type=\"text/javascript\" src=\"http://s.lfge.net/static/js/jquery-1.11.3.min.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/script/lib.min.js\"></");
	templateBuilder.Append("script>\r\n    <script type=\"text/javascript\" src=\"");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/script/org.js\" data-main=\"baseMain\"></");
	templateBuilder.Append("script>\r\n    ");
	string category_title = get_category_title(model.category_id,"研究");

	templateBuilder.Append("\r\n    <title>");
	templateBuilder.Append(Utils.ObjectToStr(model.title));
	templateBuilder.Append(" - ");
	templateBuilder.Append(Utils.ObjectToStr(category_title));
	templateBuilder.Append(" - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n    <meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(model.seo_keywords));
	templateBuilder.Append("\" />\r\n    <meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(model.seo_description));
	templateBuilder.Append("\" />\r\n<!--    /*\r\n     * *************************************************\r\n                           (0 0)\r\n       +-------------oOO----(_)----------------+\r\n       |          Powered By : DTmao            |\r\n       |          http://www.dtmao.cc           |\r\n       |             QQ:809451989             |\r\n       |        E-mail:809451989@qq.com        |\r\n       |       专注 DTcms /模板/插件/二次开发                   |\r\n       +--------------------------oOO----------+\r\n                          |__|__|\r\n                           || ||\r\n                          ooO Ooo\r\n     * *************************************************\r\n    */-->\r\n</head>\r\n<body class=\"nobanner\">\r\n    <!--Header-->\r\n    ");

	templateBuilder.Append("<div id=\"header\">\r\n    <div class=\"content\">\r\n        <a href=\"index.html\" id=\"logo\">\r\n            <img src=\"");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/upload/71/201512/1449070833744.png\" height=\"40\" /></a><ul\r\n                id=\"nav\">\r\n                <li class=\"navitem\"><a class=\"active\" href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\" target=\"_self\">Home\r\n                <strong class=\"nav_ico\"></strong></a></li>\r\n                <li class=\"navitem\"><a href=\"");
	templateBuilder.Append(linkurl("news_list",54,1));

	templateBuilder.Append("\" target=\"_self\">研究<strong\r\n                    class=\"nav_ico\"></strong></a></li>\r\n                <li class=\"navitem\"><a href=\"");
	templateBuilder.Append(linkurl("single","publish"));

	templateBuilder.Append("\" target=\"_self\">出版<strong\r\n                    class=\"nav_ico\"></strong></a></li>\r\n                <li class=\"navitem\"><a href=\"javascript:;\" target=\"\">更多<strong class=\"nav_ico\"></strong><i\r\n                    class=\"fa fa-angle-down\"></i></a>\r\n                    <ul class=\"subnav\">\r\n                        <li><a href=\"http://weibo.com/wuchangjian\" target=\"_self\">微博<i class=\"fa fa-angle-right\"></i></a></li>\r\n                        <li><a href=\"");
	templateBuilder.Append(linkurl("about","about"));

	templateBuilder.Append("\" target=\"_self\">简介<i class=\"fa fa-angle-right\"></i></a></li>\r\n                    </ul>\r\n                </li>\r\n            </ul>\r\n        <div class=\"clear\">\r\n        </div>\r\n    </div>\r\n    <a id=\"headSHBtn\" href=\"javascript:;\">X</a></div>\r\n");


	templateBuilder.Append("\r\n    <!--/Header-->\r\n    <div id=\"sitecontent\">\r\n        <div class=\"npagePage\" id=\"newspost\">\r\n            <div class=\"content\">\r\n                <div class=\"header fw\">\r\n                    <p class=\"title\">\r\n                        ");
	templateBuilder.Append(Utils.ObjectToStr(model.title));
	templateBuilder.Append("</p>\r\n                    <p class=\"subtitle\">\r\n                        ");
	templateBuilder.Append(Utils.ObjectToStr(model.add_time));
	templateBuilder.Append("</p>\r\n                </div>\r\n                <div class=\"fw postbody\">\r\n                    ");
	templateBuilder.Append(Utils.ObjectToStr(model.content));
	templateBuilder.Append("\r\n                </div>\r\n                <div id=\"pages\">\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <!--Footer-->\r\n    ");

	templateBuilder.Append("<div id=\"footer\" style=\"position: relative; z-index: 2\">\r\n    <p>\r\n        COPYRIGHT (©) 2017 張子建 Zijns. <a class=\"beian\" href=\"http://www.miitbeian.gov.cn/\"\r\n            style=\"display: inline; width: auto; color: #8e8e8e\" target=\"_blank\"></a>&nbsp;&nbsp;&nbsp;&nbsp;技术支持<a\r\n                target=\"_blank\" href=\"http://www.luyixian.cn\">\r\n                <img src=\"http://img.dtmao.cc/dtmao/img/logo.png\" width=\"50\" height=\"20\" /></a>\r\n    </p>\r\n</div>\r\n<div id=\"shares\">\r\n    <a id=\"sshare\"><i class=\"fa fa-share-alt\"></i></a><a href=\"http://service.weibo.com/share/share.php?appkey=3206975293&amp;\"\r\n        target=\"_blank\" id=\"sweibo\"><i class=\"fa fa-weibo\"></i></a><a href=\"javascript:;\"\r\n            id=\"sweixin\"><i class=\"fa fa-mobile\"></i></a><a href=\"javascript:;\" id=\"gotop\"><i\r\n                class=\"fa fa-angle-up\"></i></a>\r\n</div>\r\n<div class=\"fixed\" id=\"fixed_weixin\">\r\n    <div class=\"fixed-container\">\r\n        <div id=\"qrcode\">\r\n        </div>\r\n        <p>\r\n            扫描二维码分享到微信</p>\r\n    </div>\r\n</div>\r\n");


	templateBuilder.Append("\r\n    <!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
