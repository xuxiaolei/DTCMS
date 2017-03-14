<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="DtCms.Web.Admin.Article.Edit" ValidateRequest="false" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑资讯</title>
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
            //显示关闭高级选项
            $("#upordown").toggle(function() {
                $(this).text("关闭高级选项");
                $(this).removeClass();
                $(this).addClass("up-01");
                $(".upordown").show();
            }, function() {
                $(this).text("显示高级选项");
                $(this).removeClass();
                $(this).addClass("up-02");
                $(".upordown").hide();
            });
        });
    </script>
</head>
<body style="padding:10px;">
    <form id="form1" runat="server">
    <div class="navigation">
      <span class="back"><a href="List.aspx">返回列表</a></span><b>您当前的位置：首页 &gt; 视频管理 &gt; 编辑视频</b>
    </div>
    <div style="padding-bottom:10px;"></div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="msgtable">
        <tr>
            <th colspan="2" align="left">发布视频</th>
        </tr>
        <tr>
            <td width="15%" align="right">视频标题：</td>
            <td width="85%">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="input w380 required" 
            maxlength="250" minlength="3" HintTitle="发布的文章标题" HintInfo="控制在100个字数内，标题文本尽量不要太长。"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">文章作者：</td>
            <td>
            <asp:TextBox ID="txtAuthor" runat="server" CssClass="input w250 required" 
            maxlength="50" HintTitle="文章作者" HintInfo="控制在50个字数内，如“管理员”。">管理员</asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">文章来源：</td>
            <td>
            <asp:TextBox ID="txtForm" runat="server" CssClass="input w250 required" 
            maxlength="100" HintTitle="信息来源" HintInfo="控制在50个字数内，如“本站”。">本站</asp:TextBox>
            <span id="upordown" class="up-02">显示高级选项</span>
            </td>
        </tr>
        <tr class="upordown hide" style="display:none;">
            <td align="right">Meta关键字：</td>
            <td>
            <asp:TextBox ID="txtKeyword" runat="server" CssClass="input w250" 
            maxlength="100" HintTitle="Meta关键字" HintInfo="用于搜索引擎，如有多个关健字请用英文的,号分隔，不填写将自动提取标题。"></asp:TextBox>
            </td>
        </tr>
        <tr class="upordown hide" style="display:none;">
            <td align="right">Meta描述：</td>
            <td>
            <asp:TextBox ID="txtZhaiyao" runat="server" CssClass="textarea wh380"  
            maxlength="250" HintTitle="Meta描述" 
                    HintInfo="用于搜索引擎，控制在250个字数内，不填写将自动提取。" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr class="upordown hide" style="display:none;">
            <td align="right">文章导读：</td>
            <td>
            <asp:TextBox ID="txtDaodu" runat="server" CssClass="textarea wh380" HintTitle="文章导读属性" 
                    maxlength="250" HintInfo="控制在250个字数内，纯文本，不填写将自动提取。" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">所属栏目：</td>
            <td><asp:DropDownList id="ddlClassId" CssClass=" required" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td align="right">上传视频文件：</td>
            <td style="display:none;">
                <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input w380 left"></asp:TextBox>
                <a href="javascript:void(0);" class="files"><input type="file" id="FileUpload" name="FileUpload"></a>
                <span class="uploading">正在上传，请稍候...</span>
            </td>
             <td>
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="input w250 required"  />
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right" valign="top">文章内容：</td>
            <td>
            <FCKeditorV2:FCKeditor ID="FCKeditor" runat="server" BasePath="~/FCKedit/" Height="400px" ToolbarSet="ROY" Width="100%" Value=""></FCKeditorV2:FCKeditor>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">文章属性：</td>
            <td>
                <asp:CheckBoxList ID="cblItem" runat="server" 
                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="1">允许评论</asp:ListItem>
                    <asp:ListItem Value="1">置顶</asp:ListItem>
                    <asp:ListItem Value="1">推荐</asp:ListItem>
                    <asp:ListItem Value="1">热点</asp:ListItem>
                    <asp:ListItem Value="1">幻灯</asp:ListItem>
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr style="display:none;">
            <td align="right">浏览次数：</td>
            <td>
            <asp:TextBox ID="txtClick" runat="server" CssClass="input required number" size="10" 
            maxlength="10" HintTitle="文章的浏览次数" HintInfo="纯数字，本文章被阅读的次数。">0</asp:TextBox>
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
