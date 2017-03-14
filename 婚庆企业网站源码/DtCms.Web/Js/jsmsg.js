// JavaScript Document

//可以自动关闭的提示
function jsprint(msgtitle, url, msgcss) {
    $("#msgprint").remove();
    var cssname = "";
    switch (msgcss) {
        case "Success":
            cssname = "pcent correct";
            break;
        case "Error":
            cssname = "pcent disable";
            break;
        default:
            cssname = "pcent warning";
            break;
    }
    var str = "<div id=\"msgprint\" class=\"" + cssname + "\">" + msgtitle + "</div>";
    $("body").append(str);
    $("#msgprint").show();
    
    //2秒后清除提示
    setTimeout(function() {
        $("#msgprint").fadeOut(500);
        //如果动画结束则删除节点
        if (!$("#msgprint").is(":animated")) {
            $("#msgprint").remove();
        }
		//转向URL
		if (url == "back") {
			window.history.back(-1);
		} else if (url != "") {
			window.location.href = url;
		}
    }, 2000);
}