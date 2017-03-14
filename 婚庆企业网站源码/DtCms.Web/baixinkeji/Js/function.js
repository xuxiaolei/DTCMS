//管理操作所需的所有函数
//全选取消按钮函数，调用样式如：
function checkAll(chkobj){
	if($(chkobj).text()=="全选")
	{
		$(chkobj).text("取消");
		$(".checkall input").attr("checked", true);
	}else{
		$(chkobj).text("全选");
		$(".checkall input").attr("checked", false);
	}
}