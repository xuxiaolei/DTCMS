<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdvEdit.aspx.cs" Inherits="DtCms.Web.Admin.Advertising.AdvEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑广告位</title>
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
      <span class="back"><a href="AdvList.aspx">返回列表</a></span><b>您当前的位置：管理中心 &gt; 系统管理 &gt; 编辑广告位</b>
    </div>
    <div class="spClear"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
      <tr>
        <th colspan="2" align="left">编辑广告位</th>
      </tr>
      <tr>
        <td width="25%" align="right">广告位名称：</td>
        <td width="75%">
          <asp:TextBox ID="txtTitle" runat="server" CssClass="input required" size="30" 
            maxlength="50" HintTitle="广告位名称" HintInfo="请填写广告位名称，至少1个字符，小于50个字符，如“首页顶部通栏广告”。"></asp:TextBox>
        </td>
       </tr>
       <tr>
         <td align="right">广告位类型：</td>
         <td>
             <asp:RadioButtonList ID="rblAdType" runat="server" RepeatDirection="Horizontal" 
                 RepeatLayout="Flow">
                 <asp:ListItem Selected="True" Value="1">文字 </asp:ListItem>
                 <asp:ListItem Value="2">图片 </asp:ListItem>
                 <asp:ListItem Value="3">幻灯片 </asp:ListItem>
                 <asp:ListItem Value="4">动画 </asp:ListItem>
                 <asp:ListItem Value="5">FLV视频 </asp:ListItem>
                 <asp:ListItem Value="6">代码 </asp:ListItem>
             </asp:RadioButtonList>
           </td>
        </tr>
       <tr>
         <td align="right">备注说明：</td>
         <td>
          <asp:TextBox ID="txtAdRemark" runat="server" CssClass="input" size="50" 
            maxlength="100" HintTitle="广告位备注说明" HintInfo="请填写广告位备注说明，至少1个字符，小于100个字符。"></asp:TextBox>
           </td>
       </tr>
       <tr>
         <td align="right">显示广告数：</td>
         <td>
          <asp:TextBox ID="txtAdNum" runat="server" CssClass="input required number" size="5" 
            maxlength="10" HintTitle="广告位要显示的广告数量" HintInfo="请填写显示广告数量，只能输入正整数，不能小于零。"></asp:TextBox>
           &nbsp;条</td>
       </tr>
       <tr>
         <td align="right">价格：</td>
         <td>
          <asp:TextBox ID="txtAdPrice" runat="server" CssClass="input required number" size="5" 
            maxlength="10" HintTitle="广告位价格" HintInfo="请填写该广告位的每月价格，单位为元/月。"></asp:TextBox>
           &nbsp;元/月</td>
       </tr>
       <tr>
         <td align="right">宽度：</td>
         <td> 
          <asp:TextBox ID="txtAdWidth" runat="server" CssClass="input required number" size="5" 
            maxlength="10" HintTitle="广告位的宽度" HintInfo="请填写该广告位的宽度，只能输入正整数，不能小于零。"></asp:TextBox>
           &nbsp;px</td>
       </tr>
       <tr>
         <td align="right">高度：</td>
         <td> 
          <asp:TextBox ID="txtAdHeight" runat="server" CssClass="input required number" size="5" 
            maxlength="10" HintTitle="广告位的高度" HintInfo="请填写该广告位的高度，只能输入正整数，不能小于零。"></asp:TextBox>
           &nbsp;px</td>
       </tr>
       <tr>
         <td align="right">链接目标：</td>
         <td>
             <asp:RadioButtonList ID="rblAdTarget" runat="server" 
                 RepeatDirection="Horizontal" RepeatLayout="Flow">
                 <asp:ListItem Selected="True" Value="_blank">新窗口 </asp:ListItem>
                 <asp:ListItem Value="_self">原窗口 </asp:ListItem>
             </asp:RadioButtonList>
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
