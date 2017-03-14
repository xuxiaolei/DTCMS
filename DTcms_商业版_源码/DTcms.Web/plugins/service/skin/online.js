//在线客服脚本
var currentSrc = function(){
	var a = document.scripts,
		b = a[a.length - 1].src;
	return b.substring(0, b.lastIndexOf("/") + 1);
};		
var styleSrc = currentSrc();
var onlineService = function(){
	var	$box = $("<div>").addClass("onlineservice").appendTo("body"),
		$mini = $("<a>").addClass("mini").attr("title","在线客服").appendTo($box),
		$panel = $("<div>").addClass("listpanel").hide().appendTo($box),
		$hd = $("<div>").addClass("heading").appendTo($panel),
		$close = $("<a>").addClass("close").appendTo($hd),
		$bd = $("<div>").addClass("wrapper").appendTo($panel);

	var f = document.createElement("link");
	f.type = "text/css";
	f.rel = "stylesheet";
	f.setAttribute("href", styleSrc+"online.css"); 
	document.getElementsByTagName("head")[0].appendChild(f);
	$mini.on("click",function(){
		$mini.hide();
		$panel.show("fast");
	}),
	$close.on("click",function(){
		$mini.show("fast");
		$panel.hide("fast");
	});
	$.ajax({ 
		type: "POST",
		dataType: "json",
		cache:true,
		url: "/plugins/service/tools/ajax.ashx",
		beforeSend: function () {
		},
		success: function(data) {
			if (!jQuery.isEmptyObject(data)) {
				if(data.status == 2){
					$mini.hide();
					$panel.show("fast");
				}
				var i,j,title,img_url,link_url,html,list,
						json = data.groups;
				html = '<dl>'
					 +'<dt>在线咨询热线</dt>'
					 +'<dd class="tel">'+data.content+'</dd>'
					 +'</dl>';
				for(i=0;i<json.length;i++){
					title = unescape(json[i].title || "");
					list = json[i].list;
					html +='<dl>'
						+'<dt>'+title+'</dt>';
					for(j=0;j<list.length;j++){
						title = unescape(list[j].name || "");
						img_url = list[j].img || "";
						link_url = list[j].url || "";
						html += '<dd class="qq"><a target="_blank" href="'+link_url+'"><img src="'+img_url+'" alt="点击这里给 '+title+' 发消息" title="点击这里给 '+title+' 发消息"><em>'+title+'</em></a></dd>';
					}
					html += '</dl>';
				}
				$bd.html(html);
			}
		},
		error: function () {
			$box.hide();
		}
	});
};
//始初化
$(function(){
	onlineService();
});