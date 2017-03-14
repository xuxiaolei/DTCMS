<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templet_file_edit.aspx.cs" Inherits="DTcms.Web.admin.settings.templet_file_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑模板文件</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/linedtextarea.js"></script>
<script type="text/javascript">
    $(function () {
        mainPageResize();
        $(window).resize(function () {
            //延迟执行,防止多次触发
            setTimeout(function () {
                mainPageResize();
            }, 100);
        });
        //行号
        $("#txtContent").linedtextarea();
    });
    //布局
    function mainPageResize() {
        var winHeight = $(window).height();
        $("#txtContent").height(winHeight - 100);
    }
</script>
</head>

<body style="overflow:hidden;">
<form id="form1" runat="server">
<div class="editTemplate">
  <dl>
    <dt><div class="templatefilepath">template/<%=pathName%>/<%=fileName%></div></dt>
    <dd>
      <asp:TextBox ID="txtContent" runat="server" CssClass="input code" TextMode="MultiLine" style="width:100%;height:450px;"></asp:TextBox>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input type="reset" name="btnReset" id="btnReset" class="btn yellow" value="重置" />
  </div>
</div>
<!--/工具栏-->

</form>
</body>
</html>
