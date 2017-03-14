<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="DtCms.Web.Admin.Links.Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>增加友情链接</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/cursorfocus.js"></script>
    <script type="text/javascript" src="../js/singleupload.js"></script>
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
        $(function() {
            $("#cbIsImage").bind("click",function(){
                if($(this).attr("checked") == true) {
                    $(".upordown").show();
                }else{
                    $(".upordown").hide();
                }
            });
        });
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 友情链接 &gt; 增加链接</b>
    </div>
    <div style="padding-bottom:10px;"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">增加友情链接</th>
        </tr>
        <tr>
            <td width="15%" align="right">网站标题：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input required" size="30" 
            maxlength="100" minlength="2" HintTitle="网站标题名称" HintInfo="控制在100个字符内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">网址：</td>
            <td>
            <asp:TextBox ID="txtWebUrl" runat="server" CssClass="input w380 url required"
            maxlength="250" HintTitle="该网站的网址" HintInfo="请填写该网站的网址，以“http://”开头，小于250个字符。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">设置：</td>
            <td>
                <asp:CheckBox ID="cbIsImage" runat="server" Text="图片链接" />
            </td>
        </tr>
        <tr class="upordown" style="display:none">
            <td align="right">图片地址：</td>
            <td>
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left" HintTitle="图片源地址" HintInfo="请直接输入源地址以“http://”开头或右边“选择/上传”上传网站LOGO图片。"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload" name="FileUpload" /></a>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">联系人：</td>
            <td>
            <asp:TextBox ID="txtUserName" runat="server" CssClass="input" size="30" 
            maxlength="20" HintTitle="联系人姓名" HintInfo="控制在20个字符内，该网站的负责人或其它联系人姓名。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">联系电话：</td>
            <td>
            <asp:TextBox ID="txtUserTel" runat="server" CssClass="input" size="30" 
            maxlength="30" HintTitle="联系的电话" HintInfo="控制在30个字符内，格式如“区号+电话号码”。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">电子邮箱：</td>
            <td>
            <asp:TextBox ID="txtUserMail" runat="server" CssClass="input" size="30" 
            maxlength="30" HintTitle="联系的电子邮箱" HintInfo="控制在30个字符内，E-mail格式一定要填写正确方便联系。"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">属性：</td>
            <td>
                <asp:CheckBoxList ID="cblItem" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">推荐到首页</asp:ListItem>
                    <asp:ListItem Value="1">隐藏</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td align="right">优先级别：</td>
            <td>
            <asp:TextBox ID="txtSortId" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="该链接的优先级别" HintInfo="纯数字，数字越小越往前排列，可为负数。">0</asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="margin-top:10px;text-align:center;">
  <asp:Button ID="btnSave" runat="server" Text="确认保存" CssClass="submit" onclick="btnSave_Click" 
        />
  &nbsp;
  <input name="重置" type="reset" class="submit" value="重置" />
</div>
    </form>
</body>
</html>
