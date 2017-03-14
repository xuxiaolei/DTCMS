<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.Plugin.Forum.post_show" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	base.OnInit(e);
	
	StringBuilder templateBuilder = new StringBuilder(220000);
	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n");
	string board_name = get_board_name(model.board_id);

	templateBuilder.Append("\r\n<title>");
	templateBuilder.Append(Utils.ObjectToStr(model.title));
	templateBuilder.Append(" - 交互论坛 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("}</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" name=\"keywords\" />\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" name=\"description\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"../../../templates/main/css/style.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"../images/forum.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/forum.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n<link href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
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
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n	var tolink = '");
	templateBuilder.Append(linkurl("forumpostlist",model.board_id));

	templateBuilder.Append("'\r\n	$(document).ready(function() { \r\n	    //artDialog.alert(\"xx\")\r\n		$(\"ul#postlist  li\").hover(function() { \r\n			$(this).addClass(\"lihover\"); \r\n		}, function() { \r\n			$(this).removeClass(\"lihover\"); \r\n		});\r\n});\r\n\r\n//创建窗口\r\nfunction showMoveDialog(boardid,id) {\r\n    var d1 = top.dialog({\r\n        id: 'movedialog',\r\n        title: '移动帖子操作',\r\n        content: '<div style=\"width:420px; height:160px; margin:auto;\"><dl class=\"op-list\"><dt>移动至：</dt><dd><select name=\"toboardid\" id=\"toboardid\" style=\"border:1px solid #B5B5B5;\">");
	DataTable boardList = new DTcms.Web.Plugin.Forum.board().get_board_list(0);

	foreach(DataRow dr in boardList.Rows)
	{

	templateBuilder.Append("<option value=\"" + Utils.ObjectToStr(dr["id"]) + "\">" + Utils.ObjectToStr(dr["boardname"]) + "</option>");
	}	//end for if

	templateBuilder.Append("</select></dd></dl><dl class=\"op-list\"><dt>操作原因：</dt><dd><textarea name=\"opremark\" id=\"opremark\" style=\"height:80px; width:280px; border:1px solid #B5B5B5; \"></textarea></dd></dl></div>',\r\n        okValue: '确定操作',\r\n        ok: function () {\r\n			var tbid = $(\"#toboardid\").val();\r\n			var opremark = $(\"#opremark\").val();\r\n			if(tbid==boardid){\r\n				 jsdialog(\"提示\",\"目标版块和当前版块是同一版块，无需移动！\");	 \r\n			}\r\n			else\r\n			{\r\n				$.ajax({\r\n             		type: \"post\",\r\n             		url: \"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/ajax.ashx?action=move\",\r\n             		data: {toboardid:tbid, opremark:opremark, postid:id},\r\n             		dataType: \"json\",\r\n			 		success: function(data){\r\n						if(data.status==1){\r\n							okdialog(\"提示\",data.msg);\r\n							d1.close();\r\n						}\r\n						else{\r\n							jsdialog(\"提示\",data.msg);\r\n						}\r\n			 		}\r\n				});\r\n			}\r\n			return false;//阻止默认关闭	\r\n        }\r\n    }).showModal();\r\n}\r\n\r\n\r\nfunction showOpDialog(id,action,tip) {\r\n    var d2 = top.dialog({\r\n        id: 'opdialog',\r\n        title: '帖子操作',\r\n        content: '<div style=\"width:420px; height:160px; margin:auto;\"><dl class=\"op-list\"><dt>操作类型：</dt><dd style=\"margin-top:0px; padding-top:0px;\">'+tip+'</dd></dl><dl class=\"op-list\"><dt>操作原因：</dt><dd><textarea name=\"opremark\" id=\"opremark\" style=\"height:80px; width:280px; border:1px solid #B5B5B5; \"></textarea></dd></dl></div>',\r\n        okValue: '确定操作',\r\n        ok: function () {\r\n			var opremark = $(\"#opremark\").val();\r\n			$.ajax({\r\n				type: \"post\",\r\n				url: \"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/ajax.ashx?action=\"+action,\r\n				data: {opremark:opremark, optip:tip, postid:id},\r\n				dataType: \"json\",\r\n				success: function(data){\r\n					if(data.status==1){\r\n						okdialog1(\"提示\",data.msg);\r\n						d2.close();\r\n					}\r\n					else{\r\n						jsdialog(\"提示\",data.msg);\r\n					}\r\n			 	}\r\n			});\r\n			return false;//阻止默认关闭	\r\n        }\r\n    }).showModal();\r\n}\r\n\r\nfunction showdelDialog(id,action,tip) {\r\n    var d3 = top.dialog({\r\n        id: 'opdialog',\r\n        title: '帖子操作',\r\n        content: '<div style=\"width:420px; height:160px; margin:auto;\"><dl class=\"op-list\"><dt>操作类型：</dt><dd style=\"margin-top:0px; padding-top:0px;\">'+tip+'</dd></dl><dl class=\"op-list\"><dt>操作原因：</dt><dd><textarea name=\"opremark\" id=\"opremark\" style=\"height:80px; width:280px; border:1px solid #B5B5B5; \"></textarea></dd></dl></div>',\r\n        okValue: '确定操作',\r\n        ok: function () {\r\n			var opremark = $(\"#opremark\").val();\r\n			$.ajax({\r\n				type: \"post\",\r\n				url: \"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/ajax.ashx?action=\"+action,\r\n				data: {opremark:opremark, optip:tip, postid:id},\r\n				dataType: \"json\",\r\n				success: function(data){\r\n					if(data.status==1){\r\n						okdialog(\"提示\",data.msg);\r\n						d3.close();\r\n					}\r\n					else{\r\n						jsdialog(\"提示\",data.msg);\r\n					}\r\n			 	}\r\n			});\r\n			return false;//阻止默认关闭	\r\n        }\r\n    }).showModal();\r\n}\r\n\r\n\r\n//弹出一个Dialog窗口\r\nfunction jsdialog(msgtitle, msgcontent) {\r\n    var d = top.dialog({\r\n        title: msgtitle,\r\n        content: msgcontent,\r\n    }).showModal();\r\n}\r\n\r\nfunction okdialog(msgtitle, msgcontent) {\r\n	\r\n    var d = top.dialog({\r\n        title: msgtitle,\r\n        content: msgcontent,\r\n		cancelValue: '确定',\r\n		cancel:function () {\r\n				location=tolink;\r\n			}\r\n    }).showModal();\r\n}\r\n\r\nfunction okdialog1(msgtitle, msgcontent) {\r\n	\r\n    var d = top.dialog({\r\n        title: msgtitle,\r\n        content: msgcontent,\r\n		cancelValue: '确定',\r\n		cancel:function () {\r\n				location.reload();\r\n			}\r\n    }).showModal();\r\n}\r\n\r\n\r\n</");
	templateBuilder.Append("script>\r\n<style type=\"text/css\">\r\n.digg {\r\n	float:right;\r\n}\r\n</style>\r\n</head>\r\n<body>\r\n\r\n<!--Header-->\r\n");

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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"mainpage\">\r\n  <div class=\"section\" >\r\n    <div class=\"sitemap\">当前位置：<a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a> > <a href=\"");
	templateBuilder.Append(linkurl("forum"));

	templateBuilder.Append("\">交互论坛</a> > <a href='");
	templateBuilder.Append(linkurl("forumpostlist",model.board_id));

	templateBuilder.Append("'>");
	templateBuilder.Append(Utils.ObjectToStr(board_name));
	templateBuilder.Append("</a></div>\r\n    <div style=\"border:1px solid #B5B5B5; background:#FFF; margin-top:20px; padding:20px;\">\r\n      <div class=\"top-tools\">\r\n        <div class=\"tools-left\"><a href=\"");
	templateBuilder.Append(linkurl("forumpostpub",model.board_id,post_id,"reply"));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/pn_reply.png\" width=\"77\" height=\"33\"  alt=\"\"/></a></div>\r\n        <div class=\"tools-right\" >\r\n          ");
	DataTable postList = new DTcms.Web.Plugin.Forum.post_show().get_reply_list(model.board_id,post_id, 10, page, "", out totalcount);

	string pagelist = get_page_link(10, page, totalcount, "forumpostshow", post_id , "__id__");

	templateBuilder.Append("\r\n          <!--放置页码-->\r\n          <div class=\"page-box\" >\r\n            <div id=\"pagination\" class=\"digg\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n          </div>\r\n          <div class=\"line10\"></div>\r\n          <!--/放置页码--> \r\n        </div>\r\n      </div>\r\n      <div class=\"post-show\">\r\n        <ul>\r\n          <li>\r\n            <div class=\"post-show-leftl\"><span class=\"span06\"> 查看：</span><span class=\"span01\">");
	templateBuilder.Append(Utils.ObjectToStr(model.click));
	templateBuilder.Append("</span><span class=\"span06\"> | </span><span class=\"span06\"> 回复：</span><span class=\"span01\">");
	templateBuilder.Append(Utils.ObjectToStr(model.reply_count));
	templateBuilder.Append("</span> </div>\r\n            <div class=\"post-show-right\"> <span class=\"span02\">");
	templateBuilder.Append(Utils.ObjectToStr(model.title));
	templateBuilder.Append("</span> </div>\r\n          </li>\r\n          ");
	foreach(DataRow dr in postList.Rows)
	{

	DTcms.Model.users pum = get_user_model(Utils.StrToInt(Utils.ObjectToStr(dr["user_id"]), 0));

	templateBuilder.Append("\r\n          <li>\r\n            <div class=\"post-show-left\" style=\"word-spacing:8px;\">\r\n              <div class=\"post-show-top\"> <b style=\"color:#111;\">");
	templateBuilder.Append(Utils.ObjectToStr(pum.user_name));
	templateBuilder.Append("</b> </div>\r\n              <div class=\"post-show-mid\">\r\n                <div class=\"uinfo\">\r\n                  ");
	if (pum.avatar=="")
	{

	templateBuilder.Append("\r\n                  <img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/user-avatar.png\" style=\"width:120px; height:120px;\" />\r\n                  ");
	}
	else
	{

	templateBuilder.Append("\r\n                  <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(pum.avatar));
	templateBuilder.Append("\" style=\"width:120px; height:120px;\" />\r\n                  ");
	}	//end for if

	templateBuilder.Append("\r\n                  <p>\r\n                    ");
	string groupname = GetUserGroupName(pum.group_id);

	templateBuilder.Append("\r\n                    级&nbsp;&nbsp;别：<span class=\"span04\">");
	templateBuilder.Append(Utils.ObjectToStr(groupname));
	templateBuilder.Append("</span><br/>\r\n                    积&nbsp;&nbsp;分：<span class=\"span04\">");
	templateBuilder.Append(Utils.ObjectToStr(pum.point));
	templateBuilder.Append("</span><br />\r\n                    注册时间：<span class=\"span03\">");	templateBuilder.Append(Utils.ObjectToDateTime(pum.reg_time).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span>\r\n                  </p>\r\n                </div>\r\n              </div>\r\n            </div>\r\n            <div class=\"post-show-right\">\r\n              <div class=\"post-show-top\"> <span class=\"span03\">发表于：" + Utils.ObjectToStr(dr["add_time"]) + "</span> </div>\r\n              <div class=\"post-show-mid\">\r\n                <div class=\"cm\">\r\n                ");
	if (Utils.ObjectToStr(dr["is_lock"])=="1")
	{

	if (is_moderator==1)
	{

	templateBuilder.Append("\r\n                " + Utils.ObjectToStr(dr["content"]) + "\r\n                <br/>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                <span class=\"span01\">【已锁定】</span>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                " + Utils.ObjectToStr(dr["content"]) + "\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </div>\r\n              </div>\r\n              <div class=\"post-show-bottom\">\r\n                ");
	if (is_moderator==1)
	{

	templateBuilder.Append("\r\n                <div class=\"span05\">\r\n                ");
	if (Utils.ObjectToStr(dr["is_lock"])=="0")
	{

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showOpDialog(" + Utils.ObjectToStr(dr["id"]) + ",'set_lock','帖子锁定操作');\">锁定</a>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showOpDialog(" + Utils.ObjectToStr(dr["id"]) + ",'set_lock','取消锁定操作');\">取消锁定</a>\r\n                ");
	}	//end for if

	if (Utils.ObjectToStr(dr["parent_post_id"])=="0")
	{

	if (Utils.ObjectToStr(dr["is_top"])=="0")
	{

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showOpDialog(" + Utils.ObjectToStr(dr["id"]) + ",'set_top','帖子置顶操作');\">置顶</a>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showOpDialog(" + Utils.ObjectToStr(dr["id"]) + ",'set_top','取消置顶操作');\">取消置顶</a>\r\n                ");
	}	//end for if

	if (Utils.ObjectToStr(dr["is_red"])=="0")
	{

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showOpDialog(" + Utils.ObjectToStr(dr["id"]) + ",'set_red','帖子加精操作');\">精华</a>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showOpDialog(" + Utils.ObjectToStr(dr["id"]) + ",'set_red','取消加精操作');\">取消精华</a>\r\n                ");
	}	//end for if

	if (Utils.ObjectToStr(dr["is_hot"])=="0")
	{

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showOpDialog(" + Utils.ObjectToStr(dr["id"]) + ",'set_hot','设置热门操作');\">热门</a>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showOpDialog(" + Utils.ObjectToStr(dr["id"]) + ",'set_hot','取消热门操作');\">取消热门</a>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                <a href=\"javascript:;\" onclick=\"showMoveDialog(" + Utils.ObjectToStr(dr["board_id"]) + "," + Utils.ObjectToStr(dr["id"]) + ");\">移动</a>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                <a href='");
	templateBuilder.Append(linkurl("forumpostpub",model.board_id,Utils.ObjectToStr(dr["id"]),"edit"));

	templateBuilder.Append("'>编辑</a> \r\n                 <a href=\"javascript:;\" onclick=\"showdelDialog(" + Utils.ObjectToStr(dr["id"]) + ",'del','删除操作');\">删除</a>\r\n                </div>\r\n                ");
	}
	else
	{

	if (umodel.user_name==pum.user_name)
	{

	templateBuilder.Append("\r\n                <div class=\"span05\"><a href='");
	templateBuilder.Append(linkurl("forumpostpub",model.board_id,Utils.ObjectToStr(dr["id"]),"edit"));

	templateBuilder.Append("'>编辑</a></div>\r\n                ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n                <div class=\"span06\">发帖IP:" + Utils.ObjectToStr(dr["user_ip"]) + "</div>\r\n              </div>\r\n            </div>\r\n            <div class=\"clear\"></div>\r\n          </li>\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n        </ul>\r\n      </div>\r\n      <div class=\"bottom-tools\">\r\n        <div class=\"tools-left\"><a href=\"");
	templateBuilder.Append(linkurl("forumpostpub",model.board_id,post_id,"reply"));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/forum/images/pn_reply.png\" width=\"77\" height=\"33\"  alt=\"\"/></a></div>\r\n        <div class=\"tools-right\"> \r\n          <!--放置页码-->\r\n          <div class=\"page-box\" >\r\n            <div id=\"pagination1\" class=\"digg\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n          </div>\r\n          <div class=\"line10\"></div>\r\n          <!--/放置页码--> \r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<!--Footer-->\r\n");

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


	templateBuilder.Append("\r\n<!--/Footer-->\r\n\r\n</body>\r\n</html>");
	Response.Write(templateBuilder.ToString());
}
</script>
