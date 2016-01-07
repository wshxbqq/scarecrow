/// <reference path="Libs/jquery-1.10.2.min.js" />
/// <reference path="Libs/underscore-min.js" />
/// <reference path="Libs/mustache.js" />
/// <reference path="Libs/mustache.js" />



$("#addTaskBtn").click(function () {
    resetPopUpPanel();
    $("#myModal").modal();
    $("#get_pre_view").click(function () {
        $("#get_pre_view")
            .unbind()
            .attr("disabled", "disabled")
            .html("抓图中");

        $.getJSON("/Handler/addTask.aspx", {
            browser_url: $("#browser_url").val(),
            browser_type: $("#browser_type").val(),
            browser_script: $("#browser_script").val(),
            browser_width: $("#browser_width").val(),
            browser_height: $("#browser_height").val()
        }, function (data) {
            $("#img_pre_view").show();
            $("#img_pre_view_tips").show();
            $("#tpl_panel").show();

            $("#img_pre_view").attr("src", data.path);
            $("#get_pre_view").hide();
            

            $('#tplImg').fileupload({
                url: "/Handler/upLoadImg.aspx?id=" + data.taskId,
                dataType: 'text',
                done: function (e, dataInner) {
                    $("#submit_btn").removeAttr("disabled").click(function () {
                        $.getJSON("/Handler/setTaskStatus.aspx", {
                            taskId: data.taskId,
                            Task_enable: "true",
                            Task_alert: "true",
                        }, function () {
                            reloadList();
                            $("#myModal").modal("hide");
                        });
                    });
                }
            });
        });
    });



});

var popUpHTML = $("#myModal").html();
function resetPopUpPanel() {
    $("#myModal").html("").html(popUpHTML);


}


function reloadList() {
    var html = [];
    var tpl = $("#tpl").html();
    $.getJSON("/Handler/indexList.aspx", function (data) {
        for(var i in data){
            var h = Mustache.render(tpl,data[i]);
            html.push(h);
        }
        $(".bar").remove();
        $(".title-header").after(html.join(''));
        $(".upload-input").each(function (n, i) {
            $(i).fileupload({
                url: "/Handler/upLoadImg.aspx?id=" + $(i).attr("idx"),
                dataType: 'text',
                done: function (e, dataInner) {
                    alert("上传完毕");
                }
            });
        });


       
    });
   

}
reloadList();


$("body").delegate(".bar[idx] .ckb-enable", "change", function () {
    var _this = this;
    var tId=$(this).parent().attr("idx");
    $.getJSON("/Handler/setTaskStatus.aspx", {
        taskId: tId,
        Task_enable: this.checked
    }, function (data) {
        if (data) {

        } else {
            _this.checked = false;
            alert("未找到对应的模板图片");
        }

    })

});

$("body").delegate(".bar[idx] .ckb-alert", "change", function () {

    var tId = $(this).parent().attr("idx");
    $.getJSON("/Handler/setTaskStatus.aspx", {
        taskId: tId,
        Task_alert: this.checked
    }, function (data) {
        if (data) {

        } else {
            _this.checked = false;
            alert("未找到对应的模板图片");
        }

    })

});

$("body").delegate(".cls-all-err", "click", function () {

    var tId = $(this).parent().attr("idx");
    $.getJSON("/Handler/clearError.aspx", {
        taskId: tId
    }, function (data) {
        if (data) {
            reloadList();
            alert("已清理.");
        } else {
            _this.checked = false;
            alert("未找到对应的模板图片");
        }

    })

});




