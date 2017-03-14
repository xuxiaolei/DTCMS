<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DtCms.Web.Admin.login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>网站信息管理系统</title>
    <link rel="stylesheet" type="text/css" href="images/style.css">
    <script type="text/javascript" src="../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="js/focus.js"></script>
</head>
<body>
<form id="login_form" runat="server">
<div id="login_body">
	<div id="login_div">
		<div id="login_form_div">
				<table border=0 width=300>
				<tbody>
				<tr>
					<td width="170">
                    	<label>管理员帐号<br />
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="login_input" 
                            HintTitle="请输入登录帐号" HintInfo="用户名必须是字母或数字，不能包含空格或其它非法字符，不区分大小写。"></asp:TextBox>
                        </label>
                        <BR>
                        <label>管理密码<br />
                            <asp:TextBox ID="txtUserPwd" runat="server" CssClass="login_input" 
                            HintTitle="请输入登录密码" HintInfo="登录密码必须>=6位且是字母或数字，不能包含空格或其它非法字符，不区分大小写。" 
                            TextMode="Password"></asp:TextBox>
                        </label>
                    </td>
                    <td align="left">
                        <asp:ImageButton ID="loginsubmit" runat="server" CssClass="login_btn" 
                            ImageUrl="~/baixinkeji/Images/login_btn.gif" onclick="loginsubmit_Click" />
                    </td>
                </tr>
				<tr>
				  <td colspan="2" class="tipbox" style="background:url(Images/hint.gif) 0 6px no-repeat; padding-left:15px;">提示：<asp:Label ID="lbMsg" 
                          runat="server" Text="登录失败3次，需关闭后才能重新登录"></asp:Label>
                    </td>
				  </tr>
				</tbody>
                </table>
		</div>
	</div>
		<div id="login_footer"> © 2010 - 2020</div>
</div>
</form>
</body>
</html>
