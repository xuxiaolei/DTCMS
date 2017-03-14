<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarList.aspx.cs" Inherits="DtCms.Web.Admin.Advertising.BarList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>广告管理</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
</head>
<body style="padding:10px;">
<form id="form1" runat="server">
    <div class="navigation"><span class="add"><a href="BarAdd.aspx">增加图片</a></span><b>您当前的位置：管理中心 &gt; 首页Flash设置 </b></div>
    <div class="spClear"></div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th width="70%">名称</th>
        <th width="30%">管理操作</th>
      </tr>
      <%=list %>
      </table>

    <div class="spClear"></div>
</form>
</body>
</html>
