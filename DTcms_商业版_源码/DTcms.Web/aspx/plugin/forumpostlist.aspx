<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.Plugin.Forum.post_list" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	
	StringBuilder templateBuilder = new StringBuilder(220000);
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(Utils.ObjectToStr(model.boardname));
	templateBuilder.Append(" - 交互论坛 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" name=\"keywords\" />\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" name=\"description\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/forum.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n	$(document).ready(function() { \r\n		$(\"ul#postlist  li\").hover(function() { \r\n			$(this).addClass(\"lihover\"); \r\n		}, function() { \r\n			$(this).removeClass(\"lihover\"); \r\n		}); \r\n	}); \r\n</");
	templateBuilder.Append("script>\r\n<style type=\"text/css\">\r\n.digg { float:right; }\r\n</style>\r\n</head>\r\n<body>\r\n<!--Header-->\r\n");

	templateBuilder.Append("<div class=\"header\">\r\n  <div class=\"header-wrap\">\r\n    <div class=\"section\">\r\n      <div class=\"left-box\">\r\n        <a class=\"logo\" href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</a>\r\n        <p class=\"nav\">\r\n          <a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">资讯</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">商城</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">图片</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">下载</a>\r\n        </p>\r\n      </div>\r\n      <div class=\"search\">\r\n        <input id=\"keywords\" name=\"keywords\" class=\"input\" type=\"text\" onkeydown=\"if(event.keyCode==13){SiteSearch('");
	templateBuilder.Append(linkurl("search"));

	templateBuilder.Append("', '#keywords');return false};\" placeholder=\"输入回车搜索\" x-webkit-speech=\"\" />\r\n        <input class=\"submit\" type=\"submit\" onclick=\"SiteSearch('");
	templateBuilder.Append(linkurl("search"));

	templateBuilder.Append("', '#keywords');\" value=\"搜索\" />\r\n      </div>\r\n      <div class=\"right-box\">\r\n      <script type=\"text/javascript\">\r\n			$.ajax({\r\n				type: \"POST\",\r\n				url: \"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_check_login\",\r\n				dataType: \"json\",\r\n				timeout: 20000,\r\n				success: function (data, textStatus) {\r\n					if (data.status == 1) {\r\n						$(\"#menu\").prepend('<li class=\"line\"><a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\">退出</a></li>');\r\n						$(\"#menu\").prepend('<li class=\"login\"><em></em><a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">会员中心</a></li>');\r\n					} else {\r\n						$(\"#menu\").prepend('<li class=\"line\"><a href=\"");
	templateBuilder.Append(linkurl("register"));

	templateBuilder.Append("\">注册</a></li>');\r\n						$(\"#menu\").prepend('<li class=\"login\"><em></em><a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">登录</a></li>');\r\n					}\r\n				}\r\n			});\r\n		</");
	templateBuilder.Append("script>\r\n        <ul id=\"menu\">\r\n          <li>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("cart"));

	templateBuilder.Append("\">购物车<span id=\"shoppingCartCount\"><script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_cart_count\"></");
	templateBuilder.Append("script></span>件</a>\r\n          </li>\r\n          <li><a href=\"");
	templateBuilder.Append(linkurl("content","contact"));

	templateBuilder.Append("\">联系我们</a></li>\r\n        </ul>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Header-->\r\n<div class=\"mainpage\">\r\n<div class=\"section\">\r\n  <div class=\"sitemap\">当前位置：<a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a> > <a href=\"");
	templateBuilder.Append(linkurl("forum"));

	templateBuilder.Append("\">交互论坛</a> > <a href='");
	templateBuilder.Append(linkurl("forumpostlist",model.id));

	templateBuilder.Append("'>");
	templateBuilder.Append(Utils.ObjectToStr(model.boardname));
	templateBuilder.Append("</a></div>\r\n\r\n  <div style=\"border:1px solid #B5B5B5; background:#FFF; margin-top:10px; padding:10px;\">\r\n  <h1 style=\"font-size:13px;\">");
	templateBuilder.Append(Utils.ObjectToStr(model.boardname));
	templateBuilder.Append("</h1>\r\n  <p style=\"font-size:10px;\">");
	templateBuilder.Append(Utils.ObjectToStr(model.content));
	templateBuilder.Append("</p>\r\n  </div>\r\n\r\n  <div style=\"border:1px solid #B5B5B5; background:#FFF; margin-top:12px; padding:20px;\">\r\n    <div class=\"top-tools\">\r\n      <div class=\"tools-left\"><a href=\"");
	templateBuilder.Append(linkurl("forumpostpub",board_id,0,"add"));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/pn_post.png\" width=\"80\" height=\"33\" /></a></div>\r\n      <div class=\"tools-right\" > \r\n        ");
	DataTable postList = new DTcms.Web.Plugin.Forum.post_list().get_post_list(board_id,15, page, "post_type=1", out totalcount);

	string pagelist = get_page_link(15, page, totalcount, "forumpostlist", board_id, "__id__");

	templateBuilder.Append("\r\n        <!--放置页码-->\r\n        <div class=\"page-box\" >\r\n          <div id=\"pagination\" class=\"digg\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n        </div>\r\n        <div class=\"line10\"></div>\r\n        <!--/放置页码--> \r\n      </div>\r\n    </div>\r\n    <div class=\"post-list\">\r\n      <div class=\"post-list-title\">\r\n        <table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">\r\n          <tr>\r\n            <td style=\"text-indent:2em;\">主题</td>\r\n            <td width=\"160\" align=\"center\" valign=\"middle\">作者</td>\r\n            <td width=\"120\" align=\"center\">回复/查看</td>\r\n            <td width=\"160\" align=\"center\">最后回复</td>\r\n          </tr>\r\n        </table>\r\n      </div>\r\n      <div class=\"post-list-main\">\r\n        <ul id=\"postlist\">\r\n          ");
	foreach(DataRow dr in postList.Rows)
	{

	templateBuilder.Append("\r\n          <li>\r\n            <table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">\r\n              <tr>\r\n                <td class=\"item\" ");
	if (Utils.ObjectToStr(dr["is_top"])=="1")
	{

	templateBuilder.Append("style=\" background:url(");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/pin.gif) no-repeat  4px 13px;\"");
	}	//end for if

	templateBuilder.Append(" ><a href='");
	templateBuilder.Append(linkurl("forumpostshow",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("' target=\"_blank\">" + Utils.ObjectToStr(dr["title"]) + "</a> ");
	if (Utils.ObjectToStr(dr["is_red"])=="1")
	{

	templateBuilder.Append("<img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/digest.gif\" width=\"21\" height=\"14\"  alt=\"\"/>");
	}	//end for if

	if (Utils.ObjectToStr(dr["is_hot"])=="1")
	{

	templateBuilder.Append("<img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/hot.gif\" width=\"22\" height=\"14\"  alt=\"\"/>");
	}	//end for if

	templateBuilder.Append("</td>\r\n                <td width=\"160\" align=\"center\" valign=\"middle\">\r\n                    <span>");
	string postusername = get_user_name(Utils.StrToInt(Utils.ObjectToStr(dr["user_id"]), 0));
	templateBuilder.Append(Utils.ObjectToStr(postusername));
	templateBuilder.Append("</span><br/>\r\n                    <em>" + Utils.ObjectToStr(dr["add_time"]) + "</em>\r\n                </td>\r\n                <td width=\"120\" align=\"center\">\r\n                    <span>" + Utils.ObjectToStr(dr["reply_count"]) + "</span><br/>\r\n                    <em>" + Utils.ObjectToStr(dr["click"]) + "</em>\r\n                </td>\r\n                <td width=\"160\" align=\"center\">\r\n                	<span>");
	string replyusername = get_user_name(Utils.StrToInt(Utils.ObjectToStr(dr["reply_user_id"]), 0));
	templateBuilder.Append(Utils.ObjectToStr(replyusername));
	templateBuilder.Append("</span><br/>\r\n                  	<em>" + Utils.ObjectToStr(dr["reply_time"]) + "</em>\r\n                </td>\r\n              </tr>\r\n            </table>\r\n          </li>\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n        </ul>\r\n      </div>\r\n    </div>\r\n    <div class=\"bottom-tools\">\r\n      <div class=\"tools-left\"><a href=\"");
	templateBuilder.Append(linkurl("forumpostpub",board_id,0,"add"));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/pn_post.png\" width=\"80\" height=\"33\" /></a></div>\r\n      <div class=\"tools-right\"> \r\n        <!--放置页码-->\r\n        <div class=\"page-box\" >\r\n          <div id=\"pagination1\" class=\"digg\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n        </div>\r\n        <div class=\"line10\"></div>\r\n        <!--/放置页码--> \r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n</div>\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer clearfix\">\r\n  <div class=\"foot-nav\">\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首 页</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("content","about"));

	templateBuilder.Append("\">关于我们</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">新闻资讯</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频专区</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">资源下载</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">图片分享</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">留言反馈</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">友情链接</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("content","contact"));

	templateBuilder.Append("\">联系我们</a>\r\n  </div>\r\n  <div class=\"copyright\">\r\n    <p>版权所有 ");
	templateBuilder.Append(site.company.ToString());

	templateBuilder.Append(" 粤ICP备11064298号 DTcms版本号：");
	templateBuilder.Append(Utils.GetVersion().ToString());

	templateBuilder.Append("</p>\r\n    <p>Copyright &copy; 2009-2015 dtcms.net Corporation,All Rights Reserved.</p>\r\n    <p><script src=\"http://s24.cnzz.com/stat.php?id=1996164&web_id=1996164&show=pic\" language=\"javascript\"></");
	templateBuilder.Append("script></p>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->	\r\n</body>\r\n</html>");
	Response.Write(templateBuilder.ToString());
}
</script>
