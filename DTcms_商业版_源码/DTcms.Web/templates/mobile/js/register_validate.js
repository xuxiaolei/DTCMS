//=====================初始化代码======================
$(function(){
	//发送短信
	$("#btnSendcode").click(function(){
		//检查是否输入手机号码
		if($("#txtMobile").val()==""){
			dialog({title:'提示', content:'对不起，请先输入手机号码！', okValue:'确定', ok:function (){}}).showModal();
			return false;
		}
		//设置按钮状态
		$("#txtCode").prop("disabled", false);
		$("#btnSendcode").prop("disabled", true);
		$.ajax({
			type: "POST",
			url: $("#btnSendcode").attr("url"),
			dataType: "json",
			data: {
				"mobile" : $("#txtMobile").val()
			},
			timeout: 20000,
			success: function(data, textStatus) {
				if (data.status == 1){
					var d = dialog({content:data.msg}).show();
					setTimeout(function () {
						d.close().remove();
					}, 2000);
				} else {
					$("#btnSendcode").prop("disabled", false);
					dialog({title:'提示', content:data.msg, okValue:'确定', ok:function (){}}).showModal();
				}
			},
			error: function (XMLHttpRequest, textStatus, errorThrown) {
				$("#btnSendcode").prop("disabled", false);
				dialog({title:'提示', content:'状态：' + textStatus + '；出错提示：' + errorThrown, okValue:'确定', ok:function (){}}).showModal();
			}
		});
	});
    //初始化验证表单
	$("#regform").Validform({
		tiptype:3,
		callback:function(form){
			//AJAX提交表单
            $.ajax({
				type:"post",
				url:form.attr("url"),
				data:form.serialize(),
				dataType:"json",
                beforeSend:showRequest,
                success:showResponse,
                error: showError,
                timeout: 60000
            });
            return false;
		}
	});

    //表单提交前
    function showRequest(formData, jqForm, options) {
		$("#btnSubmit").val("正在提交...")
        $("#btnSubmit").prop("disabled", true);
    }
    //表单提交后
    function showResponse(data, textStatus) {
        if (data.status == 1) { //成功
            location.href = data.url;
        } else { //失败
            dialog({title:'提示', content:data.msg, okValue:'确定', ok:function (){}}).showModal();
            $("#btnSubmit").val("再次提交");
            $("#btnSubmit").prop("disabled", false);
        }
    }
    //表单提交出错
    function showError(XMLHttpRequest, textStatus, errorThrown) {
        dialog({title:'提示', content:'状态：' + textStatus + '；出错提示：' + errorThrown, okValue:'确定', ok:function (){}}).showModal();
        $("#btnSubmit").val("再次提交");
        $("#btnSubmit").prop("disabled", false);
    }
});