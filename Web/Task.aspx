<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task.aspx.cs" Inherits="Task" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Static/Libs/mustache.js"></script>
    <script src="Static/Libs/underscore-min.js"></script>
    <script src="Static/Libs/jquery-1.10.2.min.js"></script>
    <link href="Static/Libs/bootstrap-3.3.4-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Static/css/all.css" rel="stylesheet" />
    <script src="Static/Libs/bootstrap-3.3.4-dist/js/bootstrap.min.js"></script>

    <script src="Static/Libs/jQuery-File-Upload-9.11.0/js/vendor/jquery.ui.widget.js"></script>
    <script src="Static/Libs/jQuery-File-Upload-9.11.0/js/jquery.fileupload.js"></script>
    <script src="Static/Libs/jQuery-File-Upload-9.11.0/js/jquery.iframe-transport.js"></script>


</head>
<body>
 
  
    <div class="title-header">
        <div class="page-header"> 
        <h1>Scarecrow  <small>页面监控</small>
            <button id="addTaskBtn" class="btn btn-success btn-lg"><span class="glyphicon glyphicon-plus" aria-hidden="true"></span> 增加监控地址</button>
        </h1>

</div>

    </div>

    <script type="text/template" id="tpl">


        <div class="bar green" idx="{{TaskId}}">
            <span class="task-idx">{{TaskId}}.</span>
            <input class="task-url" value="{{TaskUrl}}" disabled="disabled" />
            <span class="task-label"></span>
            <span>{{TaskType}}</span>
            <span class="task-label">扫描:</span>
            <span class="badge">{{TaskMonitoringCount}}</span>
            <span class="task-label">错误:</span>
            <span class="badge">{{TaskErrorCount}}</span>

            <span class="task-label"></span>
            <button class="btn btn-info cls-all-err"  >清空错误</button>

            <span class="task-label"></span>
 
            <div tabindex="500" class="btn btn-primary btn-file change-tpl-img"><i class="glyphicon glyphicon-folder-open"></i> 变更模板图<input idx="{{TaskId}}"  type="file"   class="upload-input" /></div>

            <span class="task-label">启动</span>
            <input type="checkbox" class="ckb-enable" {{#TaskEnable}} checked  {{/TaskEnable}} />
            <span class="task-label">报警</span>
            <input type="checkbox" class="ckb-alert" {{#TaskAlert}} checked {{/TaskAlert}} />
        </div>


    </script>




    <!-- Modal -->
<div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="myModalLabel">增加监控站点</h4>
      </div>
      <div class="modal-body">

        <form>
          <div class="form-group">
            <label for="browser_url">监控地址:</label>
            <input class="form-control" id="browser_url" placeholder="请输入带http://的全文网址" />
          </div>
          <div class="form-group">
            <label for="browser_width">浏览器宽:</label>
            <input type="number" class="form-control" id="browser_width" placeholder="浏览器的宽度"  value="1920"/>
          </div>
            <div class="form-group">
            <label for="browser_height">浏览器高:</label>
            <input type="number" class="form-control" id="browser_height" placeholder="浏览器的宽度" value="1080" />
          </div>
            <div class="form-group">
            <label for="browser_script">页面打开后预处理的js脚本:</label>
            <input type="text" class="form-control" id="browser_script" placeholder="页面打开后预处理的js脚本,没有的话就不填" />
          </div>
            <div class="form-group">
            <label for="browser_type">浏览器种类:</label>
            <select id="browser_type">
                <option selected>WebKit</option>
                <option>Gecko</option>
         
            </select>
          </div>
            <img id="img_pre_view" width="200" height="300" style="display:none"   />
            <span id="img_pre_view_tips" style="display:none"   >右键图片另存为</span>

            <br /><br />
            
            <button  type="button" class="btn btn-default" id="get_pre_view">获取预览图</button>
            <br /><br />
          <div class="form-group" id="tpl_panel"  style="display:none" >
            <label for="tplImg">上传模板:</label>
            <input type="file" id="tplImg">
            <p class="help-block">把ps好的模板上传到这里.</p>
          </div>
 
    
        </form>

      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
        <button type="button" class="btn btn-primary" disabled id="submit_btn">提交</button>
      </div>
    </div>
  </div>
</div>


 


    <script src="Static/task.js"></script>
</body>
</html>
