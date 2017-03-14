<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templet_file_list.aspx.cs" Inherits="DTcms.Web.admin.settings.templet_file_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>模板文件管理</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    $(function () {
        mainPageResize();
        $(window).resize(function () {
            //延迟执行,防止多次触发
            setTimeout(function () {
                mainPageResize();
            }, 100);
        });
    });
    //布局
    function mainPageResize() {
        var winHeight = $(window).height(),
            winWidth = $(window).width();
        $("#templatefilelist").height(winHeight - 100);
        $("#templateFrame").height(winHeight - 67).width(winWidth-315);
    }

    //显示图片
    function ballImages(src) {
        top.dialog({
            title: src.substr(src.lastIndexOf("/")),
            content: '<img src="' + src + '" style="max-width:800px;" />'
        }).showModal();
    }
</script>
</head>

<body class="mainbody" style="overflow:hidden;">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>界面管理</span>
  <i class="arrow"></i>
  <a href="templet_list.aspx"><span>模板管理</span></a>
  <i class="arrow"></i>
  <span><%=skinName%></span>
</div>
<!--/导航栏-->

<!--列表-->
<div class="tablebody">
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%;height:500px;">
        <tbody>
            <tr>
                <td valign="top" style="width:280px">
                    <asp:Repeater ID="rptList" runat="server">
                        <HeaderTemplate>
                        <div class="folderTagline" id="folderTagline">
                            <span class="folderroot">文件路径</span>
                            <span class="folder"><a href="templet_file_list.aspx?skin=<%=skinName%>" target="_self"><%=skinName%></a></span>
                            <span class="raquo">/</span>
                            <%
                                string temp = string.Empty;
                                foreach (string path in filepath.Split('/'))
                                {
                                    if (path != "")
                                    {
                            %>
                            <span class="folder"><a href="templet_file_list.aspx?skin=<%=skinName%>&filepath=<%=temp + path %>" target="_self"><%=path%></a></span>
                            <span class="raquo">/</span>
                            <%
                                        temp += path + "/";
                                    }
                                }
                            %>
                        </div>
                        <ul class="templatefilelist" id="templatefilelist">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="<%#Eval("type") %>">
                                <span class="icon"></span>
                                <div class="info">
                                    <div class="tline"><span class="name"><a href="<%#Eval("url") %>" target="<%#Eval("target")%>"><%#Eval("name") %></a></span></div>
                                    <div class="bline"><span class="size"><%#Eval("size")%></span><span class="time"><%#Eval("updatetime")%></span></div>
                                 </div>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                        </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
                <td valign="top">
                    <div class="template_code_editor">
                        <iframe src="blank.html" name="templateFrame" id="templateFrame" class="templateFrame" frameborder="no" scrolling="auto"></iframe>
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</div>
<!--/列表-->

</form>
</body>
</html>
