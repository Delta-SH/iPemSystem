var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

var dictionary = function () {
    this.keys = new Array();
    this.data = new Array();

    this.put = function (key, value) {
        if (this.data[key] == null) {
            this.keys.push(value);
        }
        this.data[key] = value;
    };

    this.get = function (key) {
        return this.data[key];
    };

    this.remove = function (key) {
        this.keys.remove(key);
        this.data[key] = null;
    };

    this.isEmpty = function () {
        return this.keys.length == 0;
    };

    this.size = function () {
        return this.keys.length;
    };
};

var ellipsis = function (text, max) {
    if (text.length * 2 < max) { return text; }
    var temp1 = text.replace(/[^\x00-\xff]/g, '^^');
    var temp2 = temp1.substring(0, max);
    var hanzi_num = (temp2.split('\^').length - 1) / 2;
    max = max - hanzi_num;
    var res = text.substring(0, max);
    if (max < text.length) { return res + '...'; } else { return res; }
};

var mapContainer, detailWindow;
$(document).ready(function () {
    if (typeof (BMap) === 'undefined') {
        $('body').css({ height: 'auto' });
        $('#container').hide();
        $('#search-panel').hide();
        $('#unconnected-panel').show();
        return false;
    }

    //定义常量
    var icon0 = new BMap.Icon("/Content/themes/images/map/location0.png", new BMap.Size(32, 32));
    var icon1 = new BMap.Icon("/Content/themes/images/map/location1.png", new BMap.Size(32, 32));
    var icon2 = new BMap.Icon("/Content/themes/images/map/location2.png", new BMap.Size(32, 32));
    var icon3 = new BMap.Icon("/Content/themes/images/map/location3.png", new BMap.Size(32, 32));
    var icon4 = new BMap.Icon("/Content/themes/images/map/location4.png", new BMap.Size(32, 32));
    var shadow = new BMap.Icon("/Content/themes/images/map/shadow.png", new BMap.Size(32, 32));

    //初始化
    mapContainer = new BMap.Map('container');
    mapContainer.centerAndZoom('上海市', 11);

    //添加标准控件
    mapContainer.addControl(new BMap.ScaleControl());
    mapContainer.addControl(new BMap.MapTypeControl());
    mapContainer.addControl(new BMap.NavigationControl({
        type: BMAP_NAVIGATION_CONTROL_ZOOM,
        anchor: BMAP_ANCHOR_BOTTOM_RIGHT
    }));

    //添加地图右键菜单
    var contextMenu = new BMap.ContextMenu();
    contextMenu.addItem(new BMap.MenuItem('当前坐标', function (point) { prompt('经度,纬度：', point.lng + "," + point.lat); }, { iconUrl: '/Content/themes/images/map/point.png' }));
    mapContainer.addContextMenu(contextMenu);

    //启用全景地图
    var stCtrl = new BMap.PanoramaControl();
    stCtrl.setOffset(new BMap.Size(10, 60));
    mapContainer.addControl(stCtrl);

    //启用滚轮缩放功能
    mapContainer.enableScrollWheelZoom(true);

    //添加搜索功能
    var acValue, acCtrl = new BMap.Autocomplete({ 'input': 'searchbox-input', 'location': '上海市' });

    acCtrl.addEventListener("onhighlight", function (e) {
        var str = "", value = "";
        var _value = e.fromitem.value;
        if (e.fromitem.index > -1) {
            value = _value.province + _value.city + _value.district + _value.street + _value.business;
        }
        str = "FromItem<br />index = " + e.fromitem.index + "<br />value = " + value;

        value = "";
        if (e.toitem.index > -1) {
            _value = e.toitem.value;
            value = _value.province + _value.city + _value.district + _value.street + _value.business;
        }
        str += "<br />ToItem<br />index = " + e.toitem.index + "<br />value = " + value;

        $("#search-results").html(str);
    });

    acCtrl.addEventListener("onconfirm", function (e) {
        var _value = e.item.value;
        acValue = _value.province + _value.city + _value.district + _value.street + _value.business;
        $("#search-results").html("onconfirm<br />index = " + e.item.index + "<br />value = " + acValue);

        search();
    });

    $('#search-button').on('click', function () {
        acValue = $('#searchbox-input').val();
        if (acValue.length === 0) return;
        search();
    });

    var search = function () {
        var local = new BMap.LocalSearch(mapContainer, {
            renderOptions: { map: mapContainer }
        });

        mapContainer.clearOverlays();
        local.search(acValue);
    };

    //根据IP定位
    (new BMap.LocalCity()).get(function (result) {
        var cityName = result.name;
        mapContainer.centerAndZoom(cityName, 11);
        acCtrl.setLocation(cityName);
    });

    //范围变化，重新请求标记
    mapContainer.addEventListener("zoomend", function () {
        delay(request, 1000);
    });

    mapContainer.addEventListener("dragend", function () {
        delay(request, 1000);
    });

    //请求站点标记
    var configs;
    var request = function () {
        var bs = mapContainer.getBounds();
        var bssw = bs.getSouthWest();
        var bsne = bs.getNorthEast();

        var translateCallback = function (data) {
            if (data.status === 0) {
                if (!(configs && configs.length > 0))
                    return;

                var markers = [],
                    points = data.points;
                for (var i = 0; i < points.length; i++) {
                    markers.push(createMarker(configs[i], points[i]));
                }

                var zoom = mapContainer.getZoom();
                doLabel(zoom, markers);

                for (var i = 0; i < markers.length; i++) {
                    mapContainer.addOverlay(markers[i]);
                }
            }
        }

        $.ajax({
            url: "/Configuration/GetMarkers",
            //兼容GPS经纬度范围，需要减0.02和0.01
            data: { minlng: (bssw.lng - 0.02).toFixed(6), minlat: (bssw.lat - 0.01).toFixed(6), maxlng: bsne.lng, maxlat: bsne.lat },
            dataType: 'json',
            beforeSend: function () {
                clearMarkers();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('ajax请求时发生错误');
            },
            success: function (data, textStatus, jqXHR) {
                if (data.success === true) {
                    configs = data.data;

                    var points = [];
                    for (var i = 0; i < configs.length; i++) {
                        var point = new BMap.Point(configs[i].lng, configs[i].lat);
                        points.push(point);
                    }

                    var convertor = new BMap.Convertor();
                    convertor.translate(points, 1, 5, translateCallback);
                }
            }
        });
    };

    //添加标记
    var geoc = new BMap.Geocoder();
    var createMarker = function (cfg, point) {
        var marker = new BMap.Marker(point || new BMap.Point(cfg.lng, cfg.lat), { title: cfg.name, enableMassClear: false });
        var label = new BMap.Label(cfg.name, { offset: new BMap.Size(30, 3) });
        marker._cfg = cfg;

        if (cfg.level === 0) {
            label.setStyle({ background: '#48ac2e', border: '#48ac2e', 'border-radius': '3px', padding: '3px 5px', color: "#fff" })
            marker.setIcon(icon0);
            marker.setShadow(shadow);
            marker.setLabel(label);
        } else if (cfg.level === 1) {
            label.setStyle({ background: '#f04b51', border: '#f04b51', 'border-radius': '3px', padding: '3px 5px', color: "#fff" })
            marker.setIcon(icon1);
            marker.setShadow(shadow);
            marker.setLabel(label);
        } else if (cfg.level === 2) {
            label.setStyle({ background: '#efa91f', border: '#efa91f', 'border-radius': '3px', padding: '3px 5px', color: "#fff" })
            marker.setIcon(icon2);
            marker.setShadow(shadow);
            marker.setLabel(label);
        } else if (cfg.level === 3) {
            label.setStyle({ background: '#f5d313', border: '#f5d313', 'border-radius': '3px', padding: '3px 5px', color: "#fff" })
            marker.setIcon(icon3);
            marker.setShadow(shadow);
            marker.setLabel(label);
        } else if (cfg.level === 4) {
            label.setStyle({ background: '#0892cd', border: '#0892cd', 'border-radius': '3px', padding: '3px 5px', color: "#fff" })
            marker.setIcon(icon4);
            marker.setShadow(shadow);
            marker.setLabel(label);
        }

        marker.addEventListener("click", function (e) {
            var cfg = e.target._cfg;

            initWindow();
            detailWindow.open(e.target);
            detailWindow.setTitle(cfg.name);
            detailWindow.setContent(getContent(cfg));

            geoc.getLocation(e.target.getPosition(), function (rs) {
                var address = rs.addressComponents;
                var detail = address.province + address.city + address.district + address.street + address.streetNumber;
                $("table.content tr:last td:last").html(detail).attr({ title: detail });
            });
        });

        return marker;
    };

    //删除标记
    var clearMarkers = function () {
        var markers = mapContainer.getOverlays();
        for (var i = 0; i < markers.length; i++) {
            var marker = markers[i];
            if (!marker._cfg) continue;
            mapContainer.removeOverlay(marker);
        }
    };

    //根据范围大小决定是否显示标签
    var doLabel = function (zoom, markers) {
        for (var i = 0; i < markers.length; i++) {
            var marker = markers[i];
            if (!marker._cfg) continue;

            if (zoom < 11)
                marker.getLabel().hide();
            else
                marker.getLabel().show();
        }
    };

    //初始化详细信息窗体
    var initWindow = function(){
        if (!detailWindow) {
            detailWindow = new BMapLib.SearchInfoWindow(mapContainer, '', {
                title: '',      //标题
                width: 290,             //宽度
                height: 135,              //高度
                panel: "panel",         //检索结果面板
                enableAutoPan: true,     //自动平移
                searchTypes: [
                    BMAPLIB_TAB_SEARCH,   //周边检索
                    BMAPLIB_TAB_TO_HERE,  //到这里去
                    BMAPLIB_TAB_FROM_HERE //从这里出发
                ]
            });
        }

        if (detailWindow._isOpen)
            detailWindow.close();
    };

    //获取详细信息窗体内容
    var getContent = function (cfg) {
        var content = [
            '<div class=\"contentWndBox\">',
            '<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"content\">',
            '<tr>',
            '<td valign=\"top\" class=\"title\">类型：</td>',
            '<td valign=\"top\" title=\"' + cfg.type + '\">' + cfg.type + '</td>',
            '</tr>',
            '<tr>',
            '<td valign=\"top\" class=\"title\">经度：</td>',
            '<td valign=\"top\" title=\"' + cfg.lng + '\">' + cfg.lng + '</td>',
            '</tr>',
            '<tr>',
            '<td valign=\"top\" class=\"title\">纬度：</td>',
            '<td valign=\"top\" title=\"' + cfg.lat + '\">' + cfg.lat + '</td>',
            '</tr>',
            '<tr>',
            '<td valign=\"top\" class=\"title\">地址：</td>',
            '<td valign=\"top\" title=\"\"></td>',
            '</tr>',
            '</table>',
            '<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"alarm\">',
            '<tr>',
            '<td class=\"title\">一级告警：</td>',
            '<td><a href=\"/Home/ActiveAlarm\" target=\"_blank\">' + cfg.alm1 + '&nbsp;条»</a></td>',
            '<td class=\"title\">二级告警：</td>',
            '<td><a href=\"/Home/ActiveAlarm\" target=\"_blank\">' + cfg.alm2 + '&nbsp;条»</a></td>',
            '</tr>',
            '<tr>',
            '<td class=\"title\">三级告警：</td>',
            '<td><a href=\"/Home/ActiveAlarm\" target=\"_blank\">' + cfg.alm3 + '&nbsp;条»</a></td>',
            '<td class=\"title\">四级告警：</td>',
            '<td><a href=\"/Home/ActiveAlarm\" target=\"_blank\">' + cfg.alm4 + '&nbsp;条»</a></td>',
            '</tr>',
            '</table>',
            '</div>'
        ];

        return content.join('');
    };
});