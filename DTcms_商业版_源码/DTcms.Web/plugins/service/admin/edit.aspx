<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="DTcms.Web.Plugin.Service.admin.edit" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑客服</title>
<link href="../../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../../../<%=siteConfig.webmanagepath %>/skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });
    //生成参数
    function makeLink() {
        var d = dialog({
            title: 'QQ\旺旺在线状态生成',
            content: '<select id="accessPatterns" class="select" style=" margin-bottom:10px;"><option value="0">QQ</option><option value="1">旺旺</option></select><br/><textarea rows="2" cols="20" id="qqww" class="input"></textarea>',
            ok: function () {
                var value = $("#qqww").val();
                this.close(value);
                this.remove();
            },
            cancel: false

        });
        d.addEventListener('close', function () {
            if (this.returnValue != "") {
                if ($("#accessPatterns").val() == 0) {
                    $("#txtLinkUrl").val('http://wpa.qq.com/msgrd?v=3&uin=' + this.returnValue + '&site=qq&menu=yes');
                    $("#txtImgUrl").val('http://wpa.qq.com/pa?p=2:' + this.returnValue + ':4');
                    wangwang(this.returnValue)
                } else {
                    var text = wangwang(this.returnValue);
                    $("#txtLinkUrl").val('http://www.taobao.com/webww/ww.php?ver=3&touid=' + text + '&siteid=cntaobao&status=2&charset=utf-8');
                    $("#txtImgUrl").val('http://amos.alicdn.com/online.aw?v=2&uid=' + text + '&site=cntaobao&s=2&charset=utf-8');
                }
            }
        });
        d.show();
        return false;
    }
    //旺旺编码
    function wangwang(ww) {
        var txt = URLEncode(ww);
        if (txt != false) {
            return txt;
        }
        return ww;
    }
    function URLEncode(fld) {
        if (fld == "") return false;
        var encodedField = "";
        var s = fld;
        if (typeof encodeURIComponent == "function") {
            encodedField = encodeURIComponent(s);
        }
        else {
            encodedField = encodeURIComponentNew(s);
        }
        return encodedField;
    }
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="index.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../../../<%=siteConfig.webmanagepath %>/center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>插件管理</span>
  <i class="arrow"></i>
  <span>客服管理</span>
  <i class="arrow"></i>
  <span>编辑客服</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div class="content-tab-wrap">
  <div id="floatHead" class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a href="javascript:;" onclick="tabs(this);" class="selected">编辑客服信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>所属分组</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlGroupId" runat="server" datatype="*" sucmsg=" "></asp:DropDownList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>启用状态</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblIsLock" runat="server"  RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Selected="True" Value="0">正常</asp:ListItem>
            <asp:ListItem Value="1">暂停</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>排序数字</dt>
    <dd>
      <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
      <span class="Validform_checktip">*数字，越小越向前</span>
    </dd>
  </dl>
  <dl>
    <dt>客服名称</dt>
    <dd>
      <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" "></asp:TextBox>
    </dd>
  </dl>
  <dl>
    <dt>链接地址</dt>
    <dd><asp:TextBox ID="txtLinkUrl" runat="server" maxlength="255"  CssClass="input normal" TextMode="MultiLine" /> <a class="icon-btn add" onclick="makeLink()"><span>快速生成</span></a></dd>
  </dl>
  <dl>
    <dt>图片地址</dt>
    <dd>
      <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" TextMode="MultiLine" />
    </dd>
  </dl>
  <dl>
    <dt>填写说明</dt>
    <dd>
      QQ调用代码说明：<a target="_blank" href="http://wp.qq.com">http://wp.qq.com</a><br />
      阿里旺旺代码调用：<a target="_blank" href="http://page.1688.com/html/wangwang/download/windows/wbtx.html">http://page.1688.com/html/wangwang/download/windows/wbtx.html</a>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-list">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
  <div class="clear"></div>
</div>
<!--/工具栏-->
</form>
</body>
</html>
