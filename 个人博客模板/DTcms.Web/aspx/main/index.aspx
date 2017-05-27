<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.index" ValidateRequest="false" %>
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
	templateBuilder.Append("/script/org.js\" data-main=\"indexMain\"></");
	templateBuilder.Append("script>\r\n    <title>");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_title));
	templateBuilder.Append("</title>\r\n    <meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n    <meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<!--    /*\r\n     * *************************************************\r\n                           (0 0)\r\n       +-------------oOO----(_)----------------+\r\n       |          Powered By : DTmao            |\r\n       |          http://www.dtmao.cc           |\r\n       |             QQ:809451989             |\r\n       |        E-mail:809451989@qq.com        |\r\n       |       专注 DTcms /模板/插件/二次开发                   |\r\n       +--------------------------oOO----------+\r\n                          |__|__|\r\n                           || ||\r\n                          ooO Ooo\r\n     * *************************************************\r\n    */-->\r\n</head>\r\n<body>\r\n    <!--Header-->\r\n    ");

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


	templateBuilder.Append("\r\n    <!--/Header-->\r\n    <div id=\"sitecontent\">\r\n        <div id=\"indexPage\">\r\n            <div id=\"mslider\">\r\n                <style type=\"text/css\">\r\n                    #indexPage #mslider, #indexPage #mslider ul li\r\n                    {\r\n                        height: 0 px;\r\n                    }\r\n                </style>\r\n                <script type=\"text/javascript\">\r\n                    $(function () {\r\n                        $(\"#mslider li video\").each(function (index, element) {\r\n                            element.play();\r\n                        });\r\n                    })\r\n                </");
	templateBuilder.Append("script>\r\n                <ul class=\"slider\" data-options-auto=\"\" data-options-mode=\"\" data-options-pause=\"\">\r\n                    ");
	DataTable bannerList = get_article_list("banner", 0, 3, "status=0  and img_url<>''");

	int bannerdr__loop__id=0;
	foreach(DataRow bannerdr in bannerList.Rows)
	{
		bannerdr__loop__id++;


	if (bannerdr__loop__id==1)
	{

	templateBuilder.Append("\r\n                    <li style=\"background-image: url(" + Utils.ObjectToStr(bannerdr["img_url"]) + ")\" class=\"active\">\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                        <li style=\"background-image: url(" + Utils.ObjectToStr(bannerdr["img_url"]) + ")\">\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                            <div id=\"tempImage_");
	templateBuilder.Append(Utils.ObjectToStr(bannerdr__loop__id));
	templateBuilder.Append("\">\r\n                            </div>\r\n                            <img style=\"display: none\" src=\"" + Utils.ObjectToStr(bannerdr["img_url"]) + "\" /><div class=\"mask\">\r\n                            </div>\r\n                            <a target=\"_blank\">\r\n                                <div>\r\n                                    <p class=\"title ellipsis\">\r\n                                    </p>\r\n                                    <p class=\"subtitle\">\r\n                                    </p>\r\n                                </div>\r\n                                <div class=\"sliderArrow fa fa-angle-down\">\r\n                                </div>\r\n                            </a></li>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            <div id=\"mproject\" class=\"module \">\r\n                <div class=\"bgmask\">\r\n                </div>\r\n                <div class=\"content\">\r\n                    <div class=\"header\">\r\n                        <p class=\"title\">\r\n                            作品</p>\r\n                        <p class=\"subtitle\">\r\n                            works</p>\r\n                    </div>\r\n                    <div id=\"projectlist\">\r\n                        <div class=\"wrapper\">\r\n                            ");
	DataTable worksList = get_article_list("works", 0, 6, "status=0  and img_url<>''");

	int worksdr__loop__id=0;
	foreach(DataRow worksdr in worksList.Rows)
	{
		worksdr__loop__id++;


	templateBuilder.Append("\r\n                            <div class=\"projectitem wow fadeInUp\" data-wow-delay=\".");
	templateBuilder.Append(Utils.ObjectToStr(worksdr__loop__id));
	templateBuilder.Append("s\">\r\n                                <a href=\"");
	templateBuilder.Append(linkurl("works_show",Utils.ObjectToStr(worksdr["id"])));

	templateBuilder.Append("\" target=\"_blank\">\r\n                                    <img src=\"" + Utils.ObjectToStr(worksdr["img_url"]) + "\" width=\"500\" height=\"320\" /><div class=\"project_info\">\r\n                                        <div>\r\n                                            <p class=\"title\">\r\n                                                " + Utils.ObjectToStr(worksdr["title"]) + "</p>\r\n                                            <p class=\"subtitle\">\r\n                                                " + Utils.ObjectToStr(worksdr["sub_title"]) + "</p>\r\n                                        </div>\r\n                                        <div class=\"line1\">\r\n                                        </div>\r\n                                    </div>\r\n                                </a>\r\n                            </div>\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"clear\">\r\n                    </div>\r\n                    <a href=\"");
	templateBuilder.Append(linkurl("works_list"));

	templateBuilder.Append("\" id=\"projectmore\">VIEW MORE<strong class=\"line2\"></strong></a></div>\r\n            </div>\r\n            <div id=\"mteam\" class=\"module \">\r\n                <div class=\"bgmask\">\r\n                </div>\r\n                <div class=\"content fw\">\r\n                    <div class=\"module-slider\">\r\n                        <div class=\"slider_control prev fl\">\r\n                        </div>\r\n                        <div class=\"slider_control next fr\">\r\n                        </div>\r\n                        <div class=\"slider_wrapper\">\r\n                            <ul class=\"slider\">\r\n                                <li>\r\n                                    <div class=\"header wow slideInUp\" data-wow-delay=\".2s\">\r\n                                        <a href=\"");
	templateBuilder.Append(linkurl("about","about"));

	templateBuilder.Append("\" target=\"_blank\">\r\n                                            <img src=\"");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/upload/71/201512/1449068307441.jpg\" width=\"180\" height=\"180\" /><p\r\n                                                class=\"title\">\r\n                                                張子建</p>\r\n                                            <p class=\"subtitle\">\r\n                                                ZIJNS</p>\r\n                                            <div class=\"line1\">\r\n                                            </div>\r\n                                        </a>\r\n                                    </div>\r\n                                    <p class=\"description wow slideInUp\" data-wow-delay=\".3s\">\r\n                                        創研綜合設計研究所研究員，視覺研究者，東方哲學設計理論的倡導者。其具有東方意境與神韻的作品在社會上擁有廣泛的影響力與號召力。</p>\r\n                                </li>\r\n                            </ul>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <div id=\"mcontact\" class=\"module   bgShow\" style=\"background-image: url(");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/upload/2/201508/1438618614879.jpg)\">\r\n                <div class=\"bgmask\">\r\n                </div>\r\n                <div class=\"content fw\">\r\n                    <div class=\"header wow fadeInUp\" data-wow-delay=\".1s\">\r\n                        <p class=\"title\">\r\n                            聯繫</p>\r\n                        <p class=\"subtitle\">\r\n                            Contact</p>\r\n                    </div>\r\n                    <div id=\"contactlist\">\r\n                        <div id=\"contactinfo\" class=\"fl wow fadeInLeft\" data-wow-delay=\".2s\">\r\n                            <h3 class=\"ellipsis\">\r\n                                ");
	templateBuilder.Append(Utils.ObjectToStr(site.company));
	templateBuilder.Append("</h3>\r\n                            <p class=\"ellipsis\">\r\n                                Address：");
	templateBuilder.Append(Utils.ObjectToStr(site.address));
	templateBuilder.Append("\r\n                            </p>\r\n                            <!--                            <p class=\"ellipsis\">\r\n                                Zipcode：101100</p>-->\r\n                            <p class=\"ellipsis\">\r\n                                Tel：");
	templateBuilder.Append(Utils.ObjectToStr(site.tel));
	templateBuilder.Append("</p>\r\n                            <p class=\"ellipsis\">\r\n                                Mobile：");
	templateBuilder.Append(Utils.ObjectToStr(site.fax));
	templateBuilder.Append("</p>\r\n                            <p class=\"ellipsis\">\r\n                                Email：");
	templateBuilder.Append(Utils.ObjectToStr(site.email));
	templateBuilder.Append("</p>\r\n                            <div>\r\n                                <a class=\"fl\" target=\"_blank\" href=\"http://weibo.com/zijns\"><i class=\"fa fa-weibo\"></i>\r\n                                </a><a class=\"fl\" target=\"_blank\" href=\"tencent://message/?uin=809451989&Site=uemo&Menu=yes\">\r\n                                    <i class=\"fa fa-qq\"></i></a><a id=\"mpbtn\" class=\"fl\" href=\"");
	templateBuilder.Append("/templates/blog");
	templateBuilder.Append("/upload/1/201508/1438424052624.jpg\">\r\n                                        <i class=\"fa fa-weixin\"></i></a>\r\n                            </div>\r\n                        </div>\r\n                        <div id=\"contactform\" class=\"fr wow fadeInRight\" data-wow-delay=\".2s\">\r\n                            <link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n                            <!--<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />-->\r\n                            <!--<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>-->\r\n                            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n                            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n                            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n                            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n                            <script type=\"text/javascript\">\r\n                                $(function () {\r\n                                    //初始化发表评论表单\r\n                                    AjaxInitForm('#feedback_form', '#btnSubmit', 1);\r\n                                });\r\n                            </");
	templateBuilder.Append("script>\r\n                            <form id=\"feedback_form\" name=\"feedback_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/feedback/ajax.ashx?action=add&site=");
	templateBuilder.Append(Utils.ObjectToStr(site.build_path));
	templateBuilder.Append("\">\r\n                            <p>\r\n                                <input type=\"text\" class=\"inputtxt\" id=\"txtUserName\" name=\"txtUserName\" placeholder=\"Name\" /></p>\r\n                            <p>\r\n                                <input type=\"text\" class=\"inputtxt\" id=\"txtUserEmail\" name=\"txtUserEmail\" placeholder=\"Email\" /></p>\r\n                            <p>\r\n                                <input type=\"text\" class=\"inputtxt\" id=\"txtUserTel\" name=\"txtUserTel\" placeholder=\"Tel\" /></p>\r\n                            <p>\r\n                                <textarea class=\"inputtxt\" id=\"txtContent\" name=\"txtContent\" placeholder=\"Content\"></textarea></p>\r\n                            <p>\r\n                                <input class=\"inputsub\" type=\"submit\" id=\"btnSubmit\" name=\"btnSubmit\" value=\"Submit\" /></p>\r\n                            </form>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <!--Footer-->\r\n    ");

	templateBuilder.Append("<div id=\"footer\" style=\"position: relative; z-index: 2\">\r\n    <p>\r\n        COPYRIGHT (©) 2017 張子建 Zijns. <a class=\"beian\" href=\"http://www.miitbeian.gov.cn/\"\r\n            style=\"display: inline; width: auto; color: #8e8e8e\" target=\"_blank\"></a>&nbsp;&nbsp;&nbsp;&nbsp;技术支持<a\r\n                target=\"_blank\" href=\"http://www.luyixian.cn\">\r\n                <img src=\"http://img.dtmao.cc/dtmao/img/logo.png\" width=\"50\" height=\"20\" /></a>\r\n    </p>\r\n</div>\r\n<div id=\"shares\">\r\n    <a id=\"sshare\"><i class=\"fa fa-share-alt\"></i></a><a href=\"http://service.weibo.com/share/share.php?appkey=3206975293&amp;\"\r\n        target=\"_blank\" id=\"sweibo\"><i class=\"fa fa-weibo\"></i></a><a href=\"javascript:;\"\r\n            id=\"sweixin\"><i class=\"fa fa-mobile\"></i></a><a href=\"javascript:;\" id=\"gotop\"><i\r\n                class=\"fa fa-angle-up\"></i></a>\r\n</div>\r\n<div class=\"fixed\" id=\"fixed_weixin\">\r\n    <div class=\"fixed-container\">\r\n        <div id=\"qrcode\">\r\n        </div>\r\n        <p>\r\n            扫描二维码分享到微信</p>\r\n    </div>\r\n</div>\r\n");


	templateBuilder.Append("\r\n    <!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
