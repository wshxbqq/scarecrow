# scarecrow
网页像素级监控 实现方案.
###方案简述:
本项目是基于 .net(4.0+) 开发的. 结合webDriver 使用网页截图然后分析截图后图像与模板进行比对.

从而可以检测出一般性的样式bug.

###使用方法
1.首先所有配置 和控制面板都在 这个页面上.

![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_14-53-58_721___12333.png)

`部署后页面是介个样子的.`

![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_14-55-08_990___1.png)

点击右上方
![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_14-55-51_268___1222.png)
弹出 增加检测任务的面板
![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_14-56-33_768___2.png)

我们 以 baidu的 页面为例. 点击`获取预览图`

因为我们选择的是 webkit内核. 所以你会看到后台启动了一个 Phantomjs 的 无头 driver
![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_14-59-09_783___3.png)

我们在得到这张图后 需要使用ps 切割掉所有的`运营区块`.所谓运营区块,就是经常会变化的区域,这些区域不纳入像素比对范畴.
![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_15-02-28_204___q.png)

挖掉后变成这样..

![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_15-03-04_559___7.png)

点击上传模板

![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_15-03-43_332___12z.png)

将制作好的 模板图上传到服务器上.

就可以看到刚才新添加进去的任务.

![](http://wshxbqq-wshxbqq.stor.sinaapp.com/2016-01-07_15-04-36_111___as.png)
