<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.Plugin.Service.admin.index" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>在线客服管理</title>
<link href="../../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../../../<%=siteConfig.webmanagepath %>/skin/default/style.css" rel="stylesheet" type="text/css" />
<link href="../../../css/pagination.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/common.js"></script>
<script type="text/javascript">
    //弹出提示
    function LoadTip(obj,json,js) {
        top.dialog({
            title: "调用地址",
            width: 450,
            content: '<h3 style="margin:5px 0;">JS</h3><textarea rows="2" cols="20" class="input" style="width:100%">' + js + '</textarea><h3 style="margin:5px 0;">JSON</h3><textarea rows="2" cols="20" class="input" style="width:100%">' + json + '</textarea>'
        }).show();
    }
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../../../<%=siteConfig.webmanagepath %>/center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>插件管理</span>
  <i class="arrow"></i>
  <span>在线客服</span>
  <i class="arrow"></i>
  <span>客服列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div class="toolbar-wrap">
  <div id="floatHead" class="toolbar">
    <div class="l-list">
      <ul class="icon-list">
        <li><a class="add" href="edit.aspx?action=<%=DTEnums.ActionEnum.Add %>&reurl=<%=transfer()%>"><i></i><span>新增</span></a></li>
        <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
        <li><asp:LinkButton ID="btnSave" runat="server" CssClass="save" onclick="btnSave_Click"><i></i><span>保存</span></asp:LinkButton></li>
        <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
        <li><a class="list" href="javascript:;" onclick="LoadTip(this, '<%=siteConfig.weburl + siteConfig.webpath + "plugins/service/tools/ajax.ashx"%>', '<%=siteConfig.weburl + siteConfig.webpath + "plugins/service/skin/online.js" %>');"><i></i><span>调用地址</span></a></li>
      </ul>
      <div class="menu-list">
        <div class="rule-single-select">
          <asp:DropDownList ID="ddlGroupId" runat="server" AutoPostBack="True" onselectedindexchanged="ddlGroupId_SelectedIndexChanged"></asp:DropDownList>
        </div>
      </div>
    </div>
    <div class="r-list">
      <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" AutoPostBack="True" ontextchanged="btnSearch_Click" />
      <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" onclick="btnSearch_Click">查询</asp:LinkButton>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<asp:Repeater ID="rptList" runat="server">
<HeaderTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
  <tr>
    <th width="6%">选择</th>
      <th align="left">客服名称</th>
      <th width="13%" align="left">所属分组</th>
      <th width="13%" align="left">显示图片</th>
      <th width="16%" align="left">发布时间</th>
      <th width="60" align="center">排序</th>
      <th width="60">状态</th>
      <th width="8%">操作</th>
    </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" />
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
    <td><a href="edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>&reurl=<%# transfer()%>"><%#Eval("title")%></a></td>
    <td><%#new DTcms.Web.Plugin.Service.BLL.online_service_group().GetTitle(Convert.ToInt32(Eval("group_id")))%></td>
    <td><a target="_blank" href="<%#Eval("link_url")%>"><img src="<%#Eval("img_url")%>" /></a></td>
    <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
    <td align="center"><asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeydown="return checkNumber(event);" /></td>

    <td align="center">
      <%# Convert.ToInt32(Eval("is_lock")) == 0 ? "显示" : "关闭"%>
    </td>
    <td align="center"><a href="edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>&reurl=<%# transfer()%>">修改</a></td>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暂无记录</td></tr>" : ""%>
</table>
</FooterTemplate>
</asp:Repeater>
<!--/列表-->
</form>
</body>
</html>
