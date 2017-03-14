<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.Plugin.Forum.board" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	
	StringBuilder templateBuilder = new StringBuilder(220000);
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>交互论坛 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" name=\"keywords\" />\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" name=\"description\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/forum.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/image/dnt.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/image/float.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body>\r\n<!--Header-->\r\n");

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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"mainpage\">\r\n<div class=\"section\" >\r\n	<div class=\"sitemap\">当前位置：<a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a> > <a href=\"");
	templateBuilder.Append(linkurl("forum"));

	templateBuilder.Append("\">交互论坛</a></div>\r\n</div>\r\n</div>\r\n\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/image/forumhot.css\" />\r\n\r\n<div class=\"main cl forumhot\">\r\n    <table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\r\n	    <tbody>\r\n	        <tr>\r\n		        <td>\r\n		            <div class=\"title_bar xg2\">\r\n                        <ul id=\"tabswi1_A\" class=\"tab_forumhot\">\r\n			                <li><a href=\"javascript:;\">热帖榜</a></li>\r\n				            <li style=\"padding-left:510px\"><a href=\"javascript:;\">新帖榜</a></li>\r\n				        </ul>\r\n                    </div>\r\n		            <div id=\"tabswi1_B\" class=\"pd cl\">\r\n                        <div class=\"newHotB\" style=\"\">	\r\n				            <ul class=\"hotlist cl one\">\r\n                            ");
	DataTable postList1 = new DTcms.Web.Plugin.Forum.post_list().get_post_list(10, "is_lock=0 and post_type=1", "click desc");

	foreach(DataRow dr1 in postList1.Rows)
	{

	templateBuilder.Append("\r\n						        <li>\r\n					                <em>[ " + Utils.ObjectToStr(dr1["click"]) + " ]</em>\r\n					                <a href=\"");
	templateBuilder.Append(linkurl("forumpostshow",Utils.ObjectToStr(dr1["id"])));

	templateBuilder.Append("\" target=\"_blank\">" + Utils.ObjectToStr(dr1["title"]) + "</a>\r\n					            </li>\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                            </ul>\r\n\r\n				            <ul class=\"hotlist cl one\">\r\n                            ");
	DataTable postList2 = new DTcms.Web.Plugin.Forum.post_list().get_post_list(10, "is_lock=0 and post_type=1", "add_time desc");

	foreach(DataRow dr2 in postList2.Rows)
	{

	templateBuilder.Append("\r\n						        <li>\r\n					                <em>[ " + Utils.ObjectToStr(dr2["click"]) + " ]</em>\r\n					                <a href=\"");
	templateBuilder.Append(linkurl("forumpostshow",Utils.ObjectToStr(dr2["id"])));

	templateBuilder.Append("\" target=\"_blank\">" + Utils.ObjectToStr(dr2["title"]) + "</a>\r\n					            </li>\r\n			                ");
	}	//end for if

	templateBuilder.Append("\r\n					        </ul>\r\n\r\n			            </div>\r\n		            </div>\r\n		        </td>\r\n\r\n	        </tr>\r\n	    </tbody>\r\n    </table>\r\n</div>\r\n\r\n\r\n\r\n<!--topic-->\r\n<div class=\"main cl\" id=\"wp\">\r\n\r\n    ");
	DataTable boardList3 = new DTcms.Web.Plugin.Forum.board().get_board_getchildlist(0);

	foreach(DataRow dr3 in boardList3.Rows)
	{

	templateBuilder.Append("\r\n	<div class=\"mainbox list\">\r\n		<div class=\"titlebar xg2\">\r\n            <span class=\"y\">\r\n            分区版主：" + Utils.ObjectToStr(dr3["moderator_list"]) + "\r\n			</span>\r\n			<h2>" + Utils.ObjectToStr(dr3["boardname"]) + "</h2>\r\n		</div>\r\n		<div class=\"fi\" style=\"\">\r\n		<table cellspacing=\"0\" cellpadding=\"0\">\r\n\r\n          ");
	DataTable boardList4 = new DTcms.Web.Plugin.Forum.board().get_board_getchildlist(Utils.StrToInt(Utils.ObjectToStr(dr3["id"]), 0));

	foreach(DataRow dr4 in boardList4.Rows)
	{

	templateBuilder.Append("\r\n		  <tbody>\r\n			<tr>\r\n			  <th class=\"notopic\">\r\n				<h2><a href=\"");
	templateBuilder.Append(linkurl("forumpostlist",Utils.ObjectToStr(dr4["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr4["boardname"]) + "</a></h2>\r\n				<p>");
	templateBuilder.Append(Utils.DropHTML(Utils.ObjectToStr(dr4["content"]),98));

	templateBuilder.Append("</p>\r\n			  </th>\r\n			  <td class=\"nums\"><em>主题：" + Utils.ObjectToStr(dr4["subject_count"]) + " / 帖子：" + Utils.ObjectToStr(dr4["post_count"]) + "</em></td>\r\n			</tr>\r\n		</tbody>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n	\r\n	    </table>\r\n	    </div>\r\n     </div>\r\n");
	}	//end for if

	templateBuilder.Append("\r\n\r\n</div>\r\n<!--end topic-->\r\n\r\n<!--Footer-->\r\n");

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
