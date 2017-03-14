<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="DtCms.Web.Admin.Products.Add" ValidateRequest="false" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>发布产品</title>
    <link rel="stylesheet" type="text/css" href="../images/style.css" />
    <script type="text/javascript" src="../../js/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.validate.min.js"></script>
    <script type="text/javascript" src="../../js/messages_cn.js"></script>
    <script type="text/javascript" src="../../js/jquery.form.js"></script>
    <script type="text/javascript" src="../js/cursorfocus.js"></script>
    <script type="text/javascript" src="../js/multipleupload.js"></script>
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
      <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 业务管理 &gt; 发布业务</b>
    </div>
    <div style="padding-bottom:10px;"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">发布业务</th>
        </tr>
        <tr>
            <td width="15%" align="right">业务标题：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w250 required" 
            maxlength="250" minlength="2" HintTitle="发布的业务标题名称" HintInfo="控制在100个字符内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">产品规格：</td>
            <td>
            <asp:TextBox ID="txtGuige" runat="server" CssClass="input"
            maxlength="50" HintTitle="产品的规格" HintInfo="控制在50个字符内，如“件”、“盒”，没有可不填。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">产品型号：</td>
            <td>
            <asp:TextBox ID="txtXinghao" runat="server" CssClass="input" 
            maxlength="50" HintTitle="产品的型号" HintInfo="控制在50个字符内，如“MT-2010456”，没有可不填。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">产品价格：</td>
            <td>
            <asp:TextBox ID="txtPrice" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="产品的价格" HintInfo="货币格式如“150.5”,单位为元，0代表暂无价格。">0</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">所属类别：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass="required" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right">图片：</td>
            <td>
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left" HintTitle="业务的图片" HintInfo="请在右边“选择/上传”上传大图，自动生成缩位图。"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload" name="FileUpload" /></a>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
        </tr>
        <tr>
            <td align="right" valign="top">业务介绍：</td>
            <td>
            <FCKeditorV2:FCKeditor ID="FCKeditor" runat="server" BasePath="~/FCKedit/" Height="400px" ToolbarSet="ROY" Width="100%" Value=""></FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr>
            <td align="right">业务属性：</td>
            <td>
                <asp:CheckBoxList ID="cblItem" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">允许评论</asp:ListItem>
                    <asp:ListItem Value="1">置顶</asp:ListItem>
                    <asp:ListItem Value="1">推荐</asp:ListItem>
                    <asp:ListItem Value="1">热点</asp:ListItem>
                    <asp:ListItem Value="1">幻灯</asp:ListItem>
                    <asp:ListItem Value="1">隐藏该产品</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">浏览次数：</td>
            <td>
            <asp:TextBox ID="txtClick" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="产品的浏览次数" HintInfo="纯数字，本产品被浏览的次数。">0</asp:TextBox>
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
