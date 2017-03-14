<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Feedback.Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>回复留言</title>
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
      <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 留言管理 &gt; 回复留言</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">回复留言信息</th>
        </tr>
        <tr>
            <td width="15%" align="right">留言标题：</td>
            <td width="85%"><%=model.Title %></td>
        </tr>
        <tr>
            <td align="right">联系人：</td>
            <td><%=model.UserName %></td>
        </tr>
        <tr>
            <td align="right">联系电话：</td>
            <td><%=model.UserTel %></td>
        </tr>
        <tr>
            <td align="right">联系QQ：</td>
            <td><%=model.UserQQ %></td>
        </tr>
        <tr>
            <td align="right">留言时间：</td>
            <td><%=model.AddTime.ToString() %></td>
        </tr>
        <tr>
            <td align="right" valign="top">留言内容：</td>
            <td style="line-height:150%;"><%=model.Content %></td>
        </tr>
        <tr>
            <td align="right">回复内容：</td>
            <td>
              <asp:TextBox ID="txtReContent" runat="server" TextMode="MultiLine" 
            CssClass="textarea" style="width:300px;height:80px;" HintTitle="回复留言内容" 
            HintInfo="请填写将要回复的内容，字符不限。"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="margin-top:10px;text-align:center;">
        <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" 
        />&nbsp;<input name="重置" type="reset" class="submit" value="重置" />
    </div>
    </form>
</body>
</html>
