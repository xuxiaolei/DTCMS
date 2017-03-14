<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_choose.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_choose" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>创建同类选择窗口</title>
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../js/common.js"></script>
<script type="text/javascript" src="../js/laytpl.js"></script>
<script type="text/javascript" src="../js/laypage.js"></script>
<script type="text/javascript">
     //获取父窗体对象
    var api = top.dialog.get(window),
        sidername = '<%=sidername %>';
    $(function () {
        //设置按钮及事件
        api.button([{
            value: '确定',
            callback: function () {
                submitForm();
                return false;
            },
            autofocus: true
        }, {
            value: '取消',
            callback: function () { }
        }
        ]);
        //全选
        $("#select_all").click(function () {
            if ($(this).prop("checked") == true) {
                $("input[name='data-checkbox']").prop("checked", true);
            } else {
                $("input[name='data-checkbox']").prop("checked", false);
            }
        });
        //加载分类列表
        $.ajax({
            type: "post",
            dataType: "json",
            cache: true,
            url: "../../tools/admin_ajax.ashx?action=get_category_list&channel_id=<%=channel_id%>",
            success: function (data) {
                if (data.status == "y") {
                    laytpl('{{# for(var i = 0, len = d.list.length; i < len; i++){ }}<option value="{{ d.list[i].id }}">{{ d.list[i].title }}</option>{{# } }}').render(data, function (html) {
                        $("#category").append(html);
                        $(".rule-single-select").ruleSingleSelect();
                    });
                }
            }
        });
        //内容加载
        var lock = false,
            indexPage = 1,
            pageSize = 10,
            category = $("#category"),
            keyword = $("#keywords"),
            tbody = $("table tbody");
        $("#ajaxButton").click(function () {
            ajaxContent(indexPage, pageSize);
        })
        //Ajax读取
        function ajaxContent(page, size) {
            if (!lock) {
                $.ajax({
                    type: "post",
                    dataType: "json",
                    cache: true,
                    url: "../../tools/admin_ajax.ashx?action=get_content_list&channel_id=<%=channel_id%>&page=" + indexPage + "&size=" + pageSize + "&category_id=" + category.val() + "&keyword=" + keyword.val(),
                    beforeSend: function () {
                        lock = true;
                    },
                    success: function (data) {
                        if (data.status == "y") {
                            laytpl('{{# for(var i = 0, len = d.list.length; i < len; i++){ }}<tr class="data"><td><input type="checkbox" name="data-checkbox" value="{{ d.list[i].id }}"></td><td align="center">{{ d.list[i].id }}</td><td>{{ d.list[i].title }}</td><td>{{ d.list[i].category }}</td></tr>{{# } }}').render(data, function (html) {
                                tbody.html(html);
                                api.height(data.list.length * 35 + 112).reset();
                            });
                            //分页插件
                            laypage({
                                cont: 'PageContent',
                                pages: GetPageSize(data.total, size),
                                curr: page || 1,
                                skin: 'yahei',
                                jump: function (obj, first) {
                                    if (!first) {
                                        indexPage = obj.curr;
                                        ajaxContent(indexPage, size);
                                    }
                                }
                            });
                        } else {
                            tbody.html('<tr><td colspan="4" align="center" valign="middle">' + data.info + '</td></tr>');
                        }
                    },
                    complete: function () {
                        lock = false;
                    },
                    error: function () {
                        tbody.html('<tr><td colspan="4" align="center" valign="middle">数据加载失败，请联系管理员！</td></tr>');
                    }
                });
            }
        }
        //回调参数
        function submitForm() {
            var list = "";
            $("input[name='data-checkbox']").each(function () {
                if ($(this).prop("checked") == true) {
                    list += '<tr class="data"><td><input type="hidden" name="' + sidername + '" value="' + $(this).val() + '">' + $(this).val() + '</td><td>' + $(this).parent().parent().find("td").eq(2).html() + '</td><td><a href="javascript:;" onclick="remoreThis(this)">删除</a></td></tr>';
                }
            });
            api.close(list).remove();
            return false;
        }
        // 计算分页数量
        function GetPageSize(totalCount, pageSize) {
            //计算页数
            if (totalCount < 1 || pageSize < 1) {
                return 1;
            }
            var pageCount = totalCount / pageSize;
            if (pageCount < 1) {
                return 1;
            }
            if (totalCount % pageSize > 0) {
                return (pageCount += 1);
            }
            else {
                if (totalCount % pageSize == 0)
                    return pageCount;
            }
            if (pageCount <= 1) {
                return 1;
            }
            return 1;
        }
    });
</script>
</head>
<body>
<div class="div-content">
<div class="ajax_dialog">
    <div class="title">
            <div class="rule-single-select">
                <select name="category" id="category">
                    <option selected="selected" value="0">全部</option>
                </select>
            </div>
            <input name="keywords" class="keywords" id="keywords" type="text" /><input type="button" name="button" class="button" id="ajaxButton" value="搜索">
        </div>
        <div class="list">
            <table border="0" cellpadding="0" cellspacing="0" class="listTable" width="100%">
               <thead>
                  <tr class="thead">
                    <th><input type="checkbox" name="select_all" id="select_all"></th>
                    <th>ID</th>
                    <th>标题</th>
                    <th>类别</th>
                  </tr>
              </thead>
              <tbody>
              </tbody>
            </table>
            <div class="pagelist">
              <div id="PageContent" class="default"></div>
            </div>
        </div>
</div>
</div>
</body>
</html>