<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DTcms.Web.Plugin.Menu.admin.edit" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑菜单</title>
<link href="../../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../../../<%=siteConfig.webmanagepath %>/skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/common.js"></script>
<style type="text/css">
.imgdiv{
	padding-top:10px;
}
.imgdiv img{
	padding:2px 3px;
	border:1px solid #dedede;
}
</style>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="index.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="index.aspx"><span>菜单列表</span></a>
  <i class="arrow"></i>
  <span>菜单编辑</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">菜单编辑</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>菜单名称</dt>
    <dd><asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*2-20" sucmsg=" " /><span class="Validform_checktip">*</span></dd>
  </dl>
  <dl>
    <dt>链接地址</dt>
    <dd><asp:TextBox ID="txtUrl" runat="server" CssClass="input normal" /><span class="Validform_checktip">*</span></dd>
  </dl>
  <dl>
    <dt>所属父类别</dt>
    <dd>
        <div class="rule-single-select">
            <asp:DropDownList id="ddlParentId" runat="server"></asp:DropDownList>
        </div>
    </dd>
  </dl>
  <dl>
    <dt>打开方式</dt>
    <dd>
        <div class="rule-multi-radio">
            <asp:RadioButtonList ID="rblMode" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem Value="_blank">_blank</asp:ListItem>
                <asp:ListItem Value="_self" Selected="True">_self</asp:ListItem>
                <asp:ListItem Value="_parent">_parent</asp:ListItem>
                <asp:ListItem Value="_top">_top</asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </dd>
  </dl>
  <dl>
    <dt>排序</dt>
    <dd><asp:TextBox ID="txtSort" runat="server" CssClass="input small" datatype="n" sucmsg=" " Text="99" /><span class="Validform_checktip">*</span></dd>
  </dl>
  <dl>
    <dt>状态</dt>
    <dd>
        <div class="rule-multi-radio">
           <asp:RadioButtonList ID="rblHide" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="0" Selected="True">正常</asp:ListItem>
            <asp:ListItem Value="1">锁定</asp:ListItem>
            </asp:RadioButtonList>
        </div>
    </dd>
  </dl>
  <dl>
    <dt>样式扩展</dt>
    <dd><asp:TextBox ID="txtCssTxt" runat="server" CssClass="input normal" /></dd>
  </dl>
  <dl>
    <dt>图标</dt>
    <dd>
      <asp:TextBox ID="txtIconUrl" runat="server" maxlength="255"  CssClass="input normal" />
      <asp:FileUpload ID="imgUpload" runat="server" CssClass="upimg" />
    </dd>
    <dd id="ImgDiv" class="imgdiv" runat="server" visible="false">
        <asp:Image runat="server" ID="IconUrl" Width="48" Height="48" />
    </dd>
  </dl>
</div>

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->
</form>
</body>
</html>