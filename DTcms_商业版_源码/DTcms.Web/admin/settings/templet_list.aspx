<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templet_list.aspx.cs" Inherits="DTcms.Web.admin.settings.templet_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>模板管理</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //只能选中一项
        $(".checkall input").click(function () {
            $(".checkall input").prop("checked", false);
            $(this).prop("checked", true);
        });
        //管理模板检测
        $("#btnManage").click(function () {
            if ($(".checkall input:checked").size() < 1) {
                top.dialog({
                    title: '提示',
                    content: '对不起，请选中您要管理的模板！',
                    okValue: '确定',
                    ok: function () { }
                }).showModal();
                return false;
            }
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>界面管理</span>
  <i class="arrow"></i>
  <span>模板管理</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"></a>
      <div class="l-list">
        <div class="menu-list">
          <div class="rule-multi-radio">
            <asp:RadioButtonList ID="rblSiteId" runat="server" RepeatDirection="Horizontal" 
                  RepeatLayout="Flow" AutoPostBack="True" 
                  onselectedindexchanged="rblSiteId_SelectedIndexChanged">
            </asp:RadioButtonList>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<div class="table-container">
    <div class="template_gird">
      <asp:Repeater ID="rptCurrent" runat="server" onitemcommand="rptList_ItemCommand">
        <HeaderTemplate>
          <div class="current_template">
            <div class="heading">当前使用模板</div>
         </HeaderTemplate>
         <ItemTemplate>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="top">
                        <div class="template_pic" id="template_pic"><img src="../../templates/<%#Eval("skinname")%>/<%#Eval("preview")%>" /></div>
                    </td>
                    <td valign="top">
                        <div class="template_info">
                            <div class="template_foldername"><%#Eval("skinname")%><span class="time">DATE:<%#Eval("createdate")%></div>
                            <div class="template_name pt10"><%#Eval("name")%><span class="demo"><a href="<%#Eval("demo") %>" target="_blank" title="查看演示">查看演示</a></span></div>
                            <div class="template_author">作者：<span class="author"><a href="<%#Eval("website") %>" target="_blank" title="访问作者主页"><%#Eval("author")%></a></span></div>
                            <div class="description">版本：<%#Eval("version")%></div>
                            <div class="description">描述：<%#Eval("description")%></div>
                            <div class="action_line"><asp:HiddenField ID="hideSkinName" Value='<%# Eval("skinname")%>' runat="server" /><asp:LinkButton ID="lbtnStart" CommandName="lbtnStart" runat="server" Text="重新生成模板" CssClass="focus" ToolTip="重新生成模板" /> <a href="templet_file_list.aspx?skin=<%#Eval("skinname")%>" title="编辑模板文件">编辑模板文件</a></div>
                        </div>
                     </td>
                </tr>
            </table>
         </ItemTemplate>
          <FooterTemplate>
            <%#rptCurrent.Items.Count == 0 ? "<div class=\"nodata\">暂无应用模版</div>" : ""%>
           </div>
          </FooterTemplate>
      </asp:Repeater>
      <asp:Repeater ID="rptList" runat="server" onitemcommand="rptList_ItemCommand">
        <HeaderTemplate>
        <div class="template_list">
         <div class="heading">其他可用模板</div>
        </HeaderTemplate>
          <ItemTemplate>
            <div class="template">
	            <div class="template_pic"><img src="../../templates/<%#Eval("skinname")%>/<%#Eval("preview")%>"></div>
	            <div class="template_foldername">
		            <%#Eval("skinname")%><span class="time">DATE:<%#Eval("createdate")%></span></div>
	            <div class="template_name"><%#Eval("name")%><span class="demo"><a href="<%#Eval("demo") %>" target="_blank" title="查看演示">查看演示</a></span></div>
	            <div class="template_author">作者：<span class="author"><a href="<%#Eval("website") %>" target="_blank" title="查看演示"><%#Eval("author")%></a></span></div>
                <div class="teplate_version">版本：<span class="version"><%#Eval("version")%></span></div>
	            <div class="description">描述：<%#Eval("description")%></div>
	            <div class="action_line">
                    <asp:HiddenField ID="hideSkinName" Value='<%# Eval("skinname")%>' runat="server" />
		            <asp:LinkButton ID="lbtnStart" CommandName="lbtnStart" runat="server" Text="启用此模板" />|<a href="templet_file_list.aspx?skin=<%#Eval("skinname")%>">编辑模板文件</a>
	            </div>
            </div>
          </ItemTemplate>
          <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<div class=\"nodata\">暂无其它可应用模版</div>" : ""%>
           </div>
          </FooterTemplate>
      </asp:Repeater>
    </div>
</div>
<!--/列表-->

</form>
</body>
</html>
