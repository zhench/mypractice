## 在Linux(CentOS6.5)环境下部署ArcGIS API for Javascript ##

到目前为止最新的[API](https://developers.arcgis.com/javascript/)版本已经到了4.3，这篇文档就是使用这个版本进行部署的。

### 环境准备 ###

- 操作系统CentOS6.5；
- jdk8.0；
- tomcat7；
- arcgis api for javascript 4.3。

### 下载arcgis api for javascript ###
下载[地址](https://developers.arcgis.com/downloads/),下载的时候需要注册用户才可以下载。

### 下载tomcat ###

下载[地址](http://mirrors.tuna.tsinghua.edu.cn/apache/tomcat/tomcat-7/v7.0.77/bin/apache-tomcat-7.0.77.zip)。

### 安装tomcat

将apache-tomcat-7.0.77.zip拷贝到centos的目录下，使用unzip解压tomcat。

	unzip apache-tomcat-7.0.77.zip

解压之后需要将bin目录下的sh文件增加可执行权限。

	chmod +x /home/apache-tomcat-7.0.77/bin/*.sh

可以使用如下脚本启动tomcat。

	sh /home/apache-tomcat-7.0.77/bin/startup.sh 
	Using CATALINA_BASE:   /home/apache-tomcat-7.0.77
	Using CATALINA_HOME:   /home/apache-tomcat-7.0.77
	Using CATALINA_TMPDIR: /home/apache-tomcat-7.0.77/temp
	Using JRE_HOME:        /usr
	Using CLASSPATH:       /home/apache-tomcat-7.0.77/bin/bootstrap.jar:/home/apache-tomcat-7.0.77/bin/tomcat-juli.jar
	Tomcat started.

显示以上信息就说明tomcat已经启动。可以将tomcat启动脚本加入到rc.local中，就可视使tomcat在系统启动时自启动。
    
      1 #!/bin/sh
      2 #
      3 # This script will be executed *after* all the other init scripts.
      4 # You can put your own initialization stuff in here if you don't
      5 # want to do the full Sys V style init stuff.
      6 
      7 touch /var/lock/subsys/local
      8 
      9 sh /home/apache-tomcat-7.0.77/bin/startup.sh

### 部署arcgis api for javascript ###

把`arcgis_js_v43_api.zip`复制到tomcat下的webapps目录下并解压。

    unzip arcgis_js_v43_api.zip

把arcgis_js_api复制到webapps目录下，这一步是为了将来使用api时不用写很长的api地址

	[root@shuguiminipc webapps]# pwd
	/home/apache-tomcat-7.0.77/webapps
	[root@shuguiminipc webapps]# cp -r arcgis_js_v43_api/arcgis_js_api

打开$apache_tomcat_home\webapps\arcgis_js_api\library\4.3\4.3\init.js文件，搜索`[HOSTNAME_AND_PATH_TO_JSAPI]`,替换为`192.168.1.138:9080/arcgis_js_api/library/4.3/4.3/`

打开$apache_tomcat_home\webapps\arcgis_js_api\library\4.3\4.3\dojo\dojo.js文件，搜索`[HOSTNAME_AND_PATH_TO_JSAPI]`,替换为`192.168.1.138:9080/arcgis_js_api/library/4.3/4.3/`

**注：将IP地址修改为部署服务器的IP地址或者网址**

### 测试部署的API

通过使用下面的url就可以访问ArcGIS API for JavaScript:
	
	<script src="http://www.example.com/arcgis_js_api/library/4.3/4.3/init.js"></script>

新建一个html文档，使用下面的代码可以测试部署的API是否可用：

	<!DOCTYPE html>
	<html>
  	<head>
	    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	    <meta name="viewport" content="initial-scale=1, maximum-scale=1,user-scalable=no" />
	    <title>Test Map</title>
	    <link rel="stylesheet" href="http://192.168.1.138:9080/arcgis_js_api/library/4.3/4.3/dijit/themes/claro/claro.css" />
	    <link rel="stylesheet" href="http://192.168.1.138:9080/arcgis_js_api/library/4.3/4.3/esri/css/main.css" />
	    <style>
	      html, body, #ui-map-view {
	        margin: 0;
	        padding: 0;
	        width: 100%;
	        height: 100%;
	      }
	    </style>
	    <script src="http://192.168.1.138:9080/arcgis_js_api/library/4.3/4.3/init.js"></	script>
	    <script>
	      var myMap, view;
	      require([
	        "esri/Basemap",
	        "esri/layers/TileLayer",
	        "esri/Map",
	        "esri/views/MapView",
	        "dojo/domReady!"
	      ], function (Basemap, TileLayer, Map, MapView){
	        // --------------------------------------------------------------------
	        // If you do not have public Internet access then use the Basemap class
	        // and point this URL to your own locally accessible cached service.
	        //
	        // Otherwise you can just use one of the named hosted ArcGIS services.
	        // --------------------------------------------------------------------
	        var layer = new TileLayer({
	          url: "http://services.arcgisonline.com/arcgis/rest/services/World_Street_Map/MapServer"
	        });
	        var customBasemap = new Basemap({
	          baseLayers: [layer],
	          title: "Custom Basemap",
	          id: "myBasemap"
	        });
	        myMap = new Map({
	          basemap: customBasemap
	        });
	        view = new MapView({
	          center: [-111.87, 40.57], // long, lat
	          container: "ui-map-view",
	          map: myMap,
	          zoom: 6
	        });
	      });
	    </script>
	  </head>
	  <body class="claro">
	    <div id="ui-map-view"></div>
	  </body>
	</html>

使用浏览器打开这个文件，可以显示出地图就说明API部署正常。

---
[happy code, happy life.](http://www.dingqiuyan.com)


