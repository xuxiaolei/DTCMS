<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BarEdit.aspx.cs" Inherits="DtCms.Web.Admin.Advertising.BarEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
</head>
<body style="padding:10px;">
<form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="BarList.aspx?Pid=1">返回列表</a></span><b>您当前的位置：管理中心 &gt; 首页Flash设置 &gt; 编辑图片</b>
    </div>
    <div class="spClear"></div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th colspan="2" align="left">增加图片</th>
      </tr>
       <tr>
         <td align="right">名称：</td>
         <td>
          <asp:TextBox ID="txtTitle" runat="server" CssClass="input w250 required" size="30" 
            maxlength="50" ></asp:TextBox>
           </td>
        </tr>
       <tr>
         <td align="right">上传文件：</td>
         <td>
             <asp:FileUpload ID="FileUpload1" runat="server" CssClass="input w250 required" />
         </td>
       </tr>
       <tr>
         <td align="right">链接地址：</td>
         <td> 
          <asp:TextBox ID="txtLinkUrl" runat="server" CssClass="input w250 url"  maxlength="250" ></asp:TextBox>
           </td>
       </tr>
     </table>
     <div style="margin-top:10px; text-align:center;">
            <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" 
                onclick="btnSave_Click" />
            &nbsp;&nbsp;<input type="reset" name="button" id="btnReset" value="重 置" class="submit" />
     </div>
</form>
</body>
</html>
