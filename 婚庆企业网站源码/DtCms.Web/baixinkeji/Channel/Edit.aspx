<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Channel.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>修改类别</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../js/cursorfocus.js"></script>
    <script type="text/javascript">
        $(function() {
            //表单验证JS
            $("#form1").validate({
                //出错时添加的标签
                errorElement: "span",
                success: function(label) {
                    //正确时的样式
                    label.text(" ").addClass("success");
                }
            });
        });
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="list.aspx?kindId=<%=kindId %>">返回列表</a></span><b>您当前的位置：首页 &gt; 类别管理 &gt; 修改类别</b>
    </div>
    <div style="padding-bottom:10px;"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th colspan="2" align="left">修改类别信息</th>
      </tr>
      <tr>
        <td width="25%" align="right">所属父类别：</td>
        <td width="75%">
            <asp:Label ID="lblPid" runat="server" Text="0" Visible="False"></asp:Label><asp:Label ID="lblPname" runat="server"></asp:Label>
          </td>
       </tr>
      <tr>
        <td width="25%" align="right">类别名称：</td>
        <td width="75%">
          <asp:TextBox ID="txtTitle" runat="server" CssClass="input required" size="30" 
            maxlength="50" HintTitle="类别名称" HintInfo="请填写该类别的名称，至少1个字符，小于50个字符。"></asp:TextBox>
        </td>
       </tr>
       <tr>
        <td width="25%" align="right">管理导航：</td>
        <td width="75%">
          <asp:TextBox ID="txtPageUrl" runat="server" CssClass="input required" size="30" 
            maxlength="250" HintTitle="类别管理导航" HintInfo="请填写该类别的导航路径，至少1个字符，小于250个字符。如：/Article/List.aspx"></asp:TextBox>
        </td>
       </tr>
       <tr>
         <td width="25%" align="right">优先级别：</td>
         <td width="75%">
            <asp:TextBox ID="txtClassOrder" CssClass="input required number" size="10" runat="server" maxlength="9" HintTitle="类别分类优先级别" HintInfo="纯数字，数字越少越向前。"></asp:TextBox>
         </td>
       </tr>
     </table>
     <div style="margin-top:10px; text-align:center;">
            <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" />
            &nbsp;&nbsp; 
            <input type="reset" name="button" id="btnReset" value="重 置" class="submit" />
     </div>
              
    </form>
</body>
</html>