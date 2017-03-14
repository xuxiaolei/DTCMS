<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DTcms.Web.Plugin.Lable.admin.edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑标签</title>
<link href="../../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../../../<%=siteConfig.webmanagepath %>/skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/ueditor/ueditor.all.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/ueditor/lang/zh-cn/zh-cn.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/common.js"></script>
<script type="text/javascript">
    var contentType = <%=contentType%>;
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //标签内容类型
        $("#ddlType").change(function () {
            if ($(this).val() == "1") {
                deleteEditor();
            } else {
                createEditor();
            }
        });
        //是否加载编辑器
        if(contentType==0){
            UE.getEditor('txtContent');
        }
    });
    
    //绑定编辑器
    function createEditor() {
        UE.getEditor('txtContent');
    }
    //删除编辑器
    function deleteEditor() {
        UE.getEditor('txtContent').destroy();
    }
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="index.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="index.aspx"><span>标签内容</span></a>
  <i class="arrow"></i>
  <span>标签编辑</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">标签编辑</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>标签名称</dt>
    <dd><asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-20" sucmsg=" " /><span class="Validform_checktip">*</span></dd>
  </dl>
  <dl>
    <dt>调用名称</dt>
    <dd><asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*2-50" sucmsg=" " /><span class="Validform_checktip">*</span></dd>
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
    <dt>标签内容类型</dt>
    <dd>
        <div class="rule-single-select">
            <asp:DropDownList ID="ddlType" runat="server">
              <asp:ListItem Value="0" Selected="True">内容编辑器</asp:ListItem>
              <asp:ListItem Value="1">纯文本内容</asp:ListItem>
            </asp:DropDownList>
         </div>
    </dd>
  </dl>
  <dl>
    <dt>内容</dt>
    <dd>
        <textarea id="txtContent" style="height:300px; width:100%;" runat="server"></textarea>
        <asp:Panel ID="Panel1" runat="server"></asp:Panel>
        <span class="Validform_checktip"></span></dd>
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
