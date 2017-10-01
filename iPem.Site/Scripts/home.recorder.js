var g_iWndIndex = 0; // 当前选择窗口
var g_iWndTypes = 1; // 默认窗口数
var g_iWndStatus = new Map(); // 窗口的自定义状态
var g_iCameras = new Map(); // 所有的摄像机信息
var g_iLogins = new Map(); // 已登录的摄像机信息
var g_iTimeLine = null; // 录像时间轴
var g_iTimeItems = new vis.DataSet(); // 录像文件
var g_iTimeGroups = new vis.DataSet([
      { id: 1, content: '命令', title: '移动侦测录像、智能录像', className: 'record-group1' },
      { id: 2, content: '定时', title: '定时录像', className: 'record-group2' },
      { id: 3, content: '报警', title: '动测或报警、报警和动测', className: 'record-group3' },
      { id: 4, content: '手动', title: '手动录像', className: 'record-group4' }
]); // 录像文件分组
var g_iRecTypes = []; // 录像类型过滤
var g_iRecDownloads = new Map(); // 录像下载列表
var g_iRecTimer = null; // 下载进度定时器
var lock = false; //全局锁

$(document).ready(function () {

    //#region ui

    // 设置窗口尺寸
    setWindowSize();

    // 窗口事件绑定
    $(window).bind({
        resize: function () {
            setWindowSize();

            var $Restart = $("#restartDiv");
            if ($Restart.length > 0) {
                var oSize = getWindowSize();
                $Restart.css({
                    width: oSize.width + "px",
                    height: oSize.height + "px"
                });
            }
        },
        beforeunload: function () {
            clearInterval(g_iRecTimer);
            stopAllPlayback();
        }
    });

    // 下载进度定时器
    g_iRecTimer = setInterval(function () {
        if (g_iRecDownloads.size() <= 0) return;

        $(g_iRecDownloads.values()).each(function () {
            if (this == null) return true;
            this.Process = downProcess(this.DownloadID);
            if (this.Process === -1 || this.Process >= 100) {
                g_iRecDownloads.remove(this.File);
            }
        });

        var status = g_iWndStatus.get(g_iWndIndex);
        if (status != null && lock === false) {
            try {
                lock = true;
                var rec = g_iRecDownloads.get(status.File);
                var process = $('#process'), download = $('#download');
                if (rec == null) {
                    if (status.Downloading === true) status.Downloading = false;
                    process.html('');
                    if (process.hasClass('hidden') === false)
                        process.addClass('hidden');

                    if (download.hasClass('active') === true)
                        download.removeClass('active');
                } else {
                    if (status.Downloading === false) status.Downloading = true;
                    process.html(rec.Process + '%');
                    if (process.hasClass('hidden') === true)
                        process.removeClass('hidden');

                    if (download.hasClass('active') === false)
                        download.addClass('active');
                }
            } finally {
                lock = false;
            }
        }
    }, 2000);

    // 重新加载后，定位到已选中的菜单位置
    var menu = $('li.menu-item > a.selected');
    if (menu.length > 0) $('.nav-menu').animate({ scrollTop: menu.offset().top - 40 }, 300);

    // 展开/折叠菜单
    $('li.menu-item > a').on('click', function (e) {
        e.preventDefault();
        var me = $(this);
        me.siblings("ul.expandable-level1").slideToggle(300);

        // 登录DVR
        var data = JSON.parse(me.attr('data'));
        if (g_iLogins.contain(data.ip) === false) {
            login(data.ip, data.port, data.uid, data.pwd);
        }
    });

    // 选中菜单
    $('ul.expandable-level1 > li a').on('click', function (e) {
        e.preventDefault();
        var me = $(this);
        if (me.hasClass('selected') === false) {
            $('li.menu-item a').removeClass('selected');
            me.addClass('selected').parents('ul.expandable-level1').prev().addClass('selected');
        }

        var data = JSON.parse(me.attr('data'));
        if (g_iLogins.contain(data.ip) === false) {
            alert("尚未登录视频服务器");
            return false;
        }

        // 查询回放录像
        $('#record-search').attr('ip', data.ip);
        $('#record-search').attr('channel', data.channel);
        $('#record-search').attr('zero', data.zero);
        $('#record-search').removeClass('disabled');
    });

    // 清屏右键菜单
    $('#record-events').contextmenu({
        target: '#events-context-menu',
        onItem: function (context, e) {
            context.children(':first').html('');
        }
    });

    // 绑定日历
    $('#calendar-trigger').on('click', function (e) {
        e.preventDefault();
        WdatePicker({
            el: $('#calendar')[0],
            dateFmt: 'yyyy-MM-dd',
            isShowClear: false,
            firstDayOfWeek: 1,
            isShowWeek: true,
            maxDate: '%y-%M-%d'
        });
    });
    $('#calendar').val(vis.moment().format('YYYY-MM-DD'));

    // 查询录像
    $('#record-search').on('click', function (e) {
        e.preventDefault();
        if (g_iTimeLine == null) return;
        var date = $('#calendar').val()
        if (date == '') return;
        var ip = $(this).attr('ip');
        if (typeof (ip) == 'undefined') return;
        var channel = $(this).attr('channel');
        if (typeof (channel) == 'undefined') return;
        var zero = $(this).attr('zero') === 'true';
        var start = vis.moment(date).hour(0).minute(0).second(0);
        var end = vis.moment(date).hour(23).minute(59).second(59);

        g_iRecTypes = [];
        if ($('#timing').is(':checked')) g_iRecTypes.push('timing');
        if ($('#motion').is(':checked')) g_iRecTypes.push('motion');
        if ($('#motionOrAlarm').is(':checked')) g_iRecTypes.push('motionOrAlarm');
        if ($('#motionAndAlarm').is(':checked')) g_iRecTypes.push('motionAndAlarm');
        if ($('#manual').is(':checked')) g_iRecTypes.push('manual');
        if ($('#smart').is(':checked')) g_iRecTypes.push('smart');

        g_iTimeLine.setOptions({ start: start, end: end.clone().hour(12).minute(0).second(0), min: start, max: end });

        iSearchTimes = 0;
        recordSearch(ip, channel, zero, start.format('YYYY-MM-DD HH:mm:ss'), end.format('YYYY-MM-DD HH:mm:ss'));
    });

    //#endregion

    //#region controller

    // 播放/暂停视频
    $('#play').on('click', function (e) {
        e.preventDefault();
        var status = g_iWndStatus.get(g_iWndIndex);
        if (status == null) return false;

        if (status.Playing === 0) {
            if (startPlayback(status.IP, status.Channel, status.Zero, status.Start, status.End) === true) {
                status.OpenSound = false;
                status.Recording = false;
                status.Speed = 0;
                status.Playing = 1;
            } else {
                alert("回放失败，请重试。");
                return false;
            }
        } else if (status.Playing === 1) {
            if (pause() === false) {
                alert("暂停失败，请重试。");
                return false;
            } else {
                status.Playing = 2;
            }
        } else if (status.Playing === 2) {
            if (resume() === false) {
                alert("恢复失败，请重试。");
                return false;
            } else {
                status.Playing = 1;
            }
        } else if (status.Playing === 3) {
            if (resume() === false) {
                alert("恢复失败，请重试。");
                return false;
            } else {
                status.Speed = 0;
                status.Playing = 1;
            }
        }

        resetIcons(status);
    });

    // 停止视频
    $('#stop').on('click', function (e) {
        e.preventDefault();
        if (stopPlayback() === false) {
            alert("停止失败，请重试。");
            return false;
        }

        var status = g_iWndStatus.get(g_iWndIndex);
        if (status != null) {
            status.OpenSound = false;
            status.Recording = false;
            status.Playing = 0;
        }

        resetIcons(status);
    });

    //慢放
    $('#kuaitui').on('click', function (e) {
        e.preventDefault();
        var status = g_iWndStatus.get(g_iWndIndex);
        if (status == null) return false;

        if (status.Playing === 1) {
            if (playSlow(status.Speed - 1) === false) {
                return false;
            } else {
                status.Speed -= 1;
            }
        }
    });

    //快放
    $('#kuaijin').on('click', function (e) {
        e.preventDefault();
        var status = g_iWndStatus.get(g_iWndIndex);
        if (status == null) return false;

        if (status.Playing === 1) {
            if (playFast(status.Speed + 1) === false) {
                return false;
            } else {
                status.Speed += 1;
            }
        }
    });

    //单帧
    $('#danzhen').on('click', function (e) {
        e.preventDefault();
        var status = g_iWndStatus.get(g_iWndIndex);
        if (status == null) return false;

        if (status.Playing === 1) {
            if (frame() === false) {
                return false;
            } else {
                status.Playing = 3;
            }

            resetIcons(status);
        } else if (status.Playing === 3) {
            if (frame() === false) {
                return false;
            }
        }
    });

    // 抓图
    $('#capture').on('click', function (e) {
        e.preventDefault();
        var status = g_iWndStatus.get(g_iWndIndex);
        if (status == null) return false;

        if (status.Playing === 1 || status.Playing === 3) {
            if (capturePic() === true) {
                alert('抓图成功（保存路径参见本地配置）');
            } else {
                alert('抓图失败');
            }
        }
    });

    // 剪辑/停止剪辑
    $('#recut').on('click', function (e) {
        e.preventDefault();
        var status = g_iWndStatus.get(g_iWndIndex);
        if (status == null || status.Playing !== 1) return false;

        if (status.Recording === true) {
            if (stopRecord() === false) {
                alert("停止剪辑失败，请重试。");
                return false;
            } else {
                status.Recording = false;
                alert("剪辑成功（保存路径参见本地配置）");
            }
        } else if (status.Recording === false) {
            if (startRecord() === true) {
                status.Recording = true;
            } else {
                alert("剪辑失败，请重试。");
                return false;
            }
        }

        resetIcons(status);
    });

    // 声音/静音
    $('#voice').on('click', function (e) {
        e.preventDefault();
        var status = g_iWndStatus.get(g_iWndIndex);
        if (status == null || status.Playing !== 1) return false;

        if (status.OpenSound === true) {
            if (closeSound() === false) {
                alert("静音失败，请重试。");
                return false;
            } else {
                status.OpenSound = false;
            }
        } else if (status.OpenSound === false) {
            if (openSound() === true) {
                //关闭所有窗口的声音
                $(g_iWndStatus.values()).each(function () {
                    if (this == null) return true;
                    this.OpenSound = false;
                });

                status.OpenSound = true;
            } else {
                alert("开启声音失败，请重试。");
                return false;
            }
        }

        resetIcons(status);
    });

    // 下载
    $('#download').on('click', function (e) {
        e.preventDefault();
        var status = g_iWndStatus.get(g_iWndIndex);
        if (status == null) return false;

        if (status.Downloading === false) {
            var iDownloadID = startDownloadRecord(status.IP, status.Channel, status.Uri);
            if (iDownloadID == null) {
                alert('录像下载失败');
                return false;
            }

            var rec = new Object();
            rec.File = status.File;
            rec.DownloadID = iDownloadID;
            rec.Process = 0;
            g_iRecDownloads.set(status.File, rec);
            status.Downloading = true;
        } else {
            if (confirm('您确认要停止下载吗？') == true) {
                var rec = g_iRecDownloads.get(status.File);
                if (rec == null) {
                    status.Downloading = false;
                } else {
                    if (stopDownloadRecord(status.IP, status.Channel, rec.DownloadID) == true) {
                        status.Downloading = false;
                    } else {
                        alert('停止下载失败');
                        return false;
                    }
                }
            }
        }
        
        resetIcons(status);
    });

    // 全屏
    $('#fullscreen').on('click', function (e) {
        e.preventDefault();
        fullScreen();
    });

    // 4*4分屏
    $('#screen16').on('click', function (e) {
        e.preventDefault();
        if ($(this).hasClass('active')) return false;
        $('#controller > a.screen').removeClass('active');
        $(this).addClass('active');

        changeWndNum(4);
    });

    // 3*3分屏
    $('#screen9').on('click', function (e) {
        e.preventDefault();
        if ($(this).hasClass('active')) return false;
        $('#controller > a.screen').removeClass('active');
        $(this).addClass('active');

        changeWndNum(3);
    });

    // 2*2分屏
    $('#screen4').on('click', function (e) {
        e.preventDefault();
        if ($(this).hasClass('active')) return false;
        $('#controller > a.screen').removeClass('active');
        $(this).addClass('active');

        changeWndNum(2);
    });

    // 1*1分屏
    $('#screen1').on('click', function (e) {
        e.preventDefault();
        if ($(this).hasClass('active')) return false;
        $('#controller > a.screen').removeClass('active');
        $(this).addClass('active');

        changeWndNum(1);
    });

    //#endregion

    //#region timeline

    var options = {
        start: vis.moment().hour(0).minute(0).second(0),
        end: vis.moment().hour(12).minute(0).second(0),
        min: vis.moment().hour(0).minute(0).second(0),
        max: vis.moment().hour(23).minute(59).second(59),
        zoomMin: 1000, // second
        zoomMax: 1000 * 60 * 60 * 24,  // day
        showMajorLabels: true,
        stack:false,
        minHeight: 100,
        format: {
            majorLabels: {
                millisecond: 'HH:mm:ss （提示：双击播放视频）',
                second: 'MM-DD HH:mm （提示：双击播放视频）',
                minute: 'ddd MM-DD （提示：双击播放视频）',
                hour: 'ddd YYYY-MM-DD （提示：双击播放视频）',
            }
        },
        editable: { remove: true },
        margin: { axis: 4, item: 4 }
    };

    g_iTimeLine = new vis.Timeline(document.getElementById('visualization'), g_iTimeItems, g_iTimeGroups, options);

    //监听事件
    g_iTimeLine.on('doubleClick', function (properties) {
        if (properties.item != null) {
            var index = properties.item;
            if (iSearchResult.length > index) {
                var current = iSearchResult[index];
                var status = g_iWndStatus.get(g_iWndIndex);
                if (status == null) {
                    status = createStatus(g_iWndIndex, iSearchResult.IP, iSearchResult.Channel, iSearchResult.Zero, current.FileName, current.Start, current.End, current.PlaybackURI);
                    g_iWndStatus.set(status.Id, status);
                } else {
                    initStatus(status, iSearchResult.IP, iSearchResult.Channel, iSearchResult.Zero, current.FileName, current.Start, current.End, current.PlaybackURI);
                }

                if (startPlayback(iSearchResult.IP, iSearchResult.Channel, iSearchResult.Zero, current.Start, current.End) === true) {
                    status.Playing = 1;
                } else {
                    alert("回放失败，请重试。");
                }

                resetIcons(status);
            }
        }
    });

    //#endregion

    //#region plugin

    //#region 检查插件是否已经安装过
    var iRet = WebVideoCtrl.I_CheckPluginInstall();
    if (-2 == iRet) {
        $('#errorPanel').removeClass('hidden');
        $('#unsupport').removeClass('hidden');
        return;
    } else if (-1 == iRet) {
        $('#errorPanel').removeClass('hidden');
        $('#uninstall').removeClass('hidden');
        return;
    } else if (0 == iRet) {
        $('#recorderPanel').removeClass('hidden');
    } else {
        return;
    }
    //#endregion

    //#region 初始化插件参数及插入插件
    WebVideoCtrl.I_InitPlugin("100%", "100%", {
        szColorProperty: "sub-border-select:157fcc",
        bWndFull: true,
        iWndowType: g_iWndTypes,
        cbSelWnd: function (xmlDoc) {
            var current = parseInt($(xmlDoc).find("SelectWnd").eq(0).text(), 10);
            if (current !== g_iWndIndex) {
                g_iWndIndex = current;
                var status = g_iWndStatus.get(g_iWndIndex);
                if (status == null) {
                    initIcons();
                } else {
                    resetIcons(status);
                }
            }
        },
        cbEvent: function (iType, index) {
            if (iType === 2) {
                var status = g_iWndStatus.get(index);
                if (status != null) {
                    status.Playing = 0;
                    status.Recording = false;

                    if (index === g_iWndIndex)
                        resetIcons(status);
                }
            }
        }
    });

    var iRet = WebVideoCtrl.I_InsertOBJECTPlugin("plugin");
    if (-1 == iRet) return;
    //#endregion

    //#region 检查插件是否最新
    var iVer = WebVideoCtrl.I_CheckPluginVersion();
    if (-1 == iVer) {
        $('#recorderPanel').addClass('hidden');
        $('#errorPanel').removeClass('hidden');
        $('#upgrade').removeClass('hidden');
        return;
    }
    //#endregion

    //#region 加载摄像机集合
    $('li.menu-item > a').each(function () {
        var data = JSON.parse($(this).attr('data'));
        g_iCameras.set(data.ip, data);
    });
    //#endregion

    //#endregion

});

//#region api

// 创建窗口参数
function createStatus(id, ip, channel, zero, file, start, end, uri) {
    var status = new Object();
    status.Id = id; // 窗口序号1-16
    status.IP = ip;
    status.Channel = channel;
    status.Zero = zero;
    status.File = file;
    status.Start = start;
    status.End = end;
    status.Uri = uri;

    status.Playing = 0; // 0:停止 1:播放 2：暂停 3：单帧
    status.Speed = 0;
    status.Recording = false;
    status.OpenSound = false;
    status.Downloading = g_iRecDownloads.contain(file);
    return status;
}

// 初始化窗口参数
function initStatus(status, ip, channel, zero, file, start, end, uri) {
    status.IP = ip;
    status.Channel = channel;
    status.Zero = zero;
    status.File = file;
    status.Start = start;
    status.End = end;
    status.Uri = uri;

    status.Playing = 0; // 0:停止 1:播放 2：暂停 3：单帧
    status.Speed = 0;
    status.Recording = false;
    status.OpenSound = false;
    status.Downloading = g_iRecDownloads.contain(file);
}

// 重置控制图标
function resetIcons(status) {
    var play = $('#play').children(':first');
    if (status.Playing === 1 && play.hasClass('ipems-icon-font-play')) {
        play.removeClass('ipems-icon-font-play').addClass('ipems-icon-font-pause');
    } else if (status.Playing !== 1 &&  play.hasClass('ipems-icon-font-pause')) {
        play.removeClass('ipems-icon-font-pause').addClass('ipems-icon-font-play');
    }

    var recut = $('#recut');
    if (status.Recording === true && recut.hasClass('active') === false) {
        recut.addClass('active');
    } else if (status.Recording === false && recut.hasClass('active') === true) {
        recut.removeClass('active');
    }

    var sound = $('#voice').children(':first');
    if (status.OpenSound === true && sound.hasClass('ipems-icon-font-unvoice')) {
        sound.removeClass('ipems-icon-font-unvoice').addClass('ipems-icon-font-voice');
    } else if (status.OpenSound === false && sound.hasClass('ipems-icon-font-voice')) {
        sound.removeClass('ipems-icon-font-voice').addClass('ipems-icon-font-unvoice');
    }

    if (lock === false) {
        try {
            lock = true;
            var download = $('#download');
            if (status.Downloading === false && download.hasClass('active') === true) {
                download.removeClass('active');
            } else if (status.Downloading === true && download.hasClass('active') === false) {
                download.addClass('active');
            }

            var process = $('#process');
            if (status.Downloading === false && process.hasClass('hidden') === false) {
                process.addClass('hidden');
            }
        } finally {
            lock = false;
        }
    }
}

// 初始化控制图标
function initIcons() {
    var play = $('#play').children(':first');
    play.removeClass('ipems-icon-font-pause').addClass('ipems-icon-font-play');

    var recut = $('#recut');
    recut.removeClass('active');

    var sound = $('#voice').children(':first');
    sound.removeClass('ipems-icon-font-voice').addClass('ipems-icon-font-unvoice');

    var download = $('#download');
    download.removeClass('active');

    var process = $('#process');
    if (process.hasClass('hidden') === false) process.addClass('hidden');
}

// 重绘录像时间轴
function redraw() {
    var items = [];
    $(iSearchResult).each(function (index) {
        items.push({
            id: index,
            content: this.FileName,
            group: getRecordType(this.Type),
            start: vis.moment(this.Start),
            end: vis.moment(this.End),
            title: ['<div>录像名称：' + this.FileName + '</div>', '<div>录像类型：' + getRecordTypeName(this.Type) + '</div>', '<div>开始时间：' + this.Start + '</div>', '<div>结束时间：' + this.End + '</div>', '<div>录像时长：' + vis.moment('2017-01-01').add(this.Interval, 's').format('HH:mm:ss') + '</div>', '<div>文件大小：' + this.FileSize + '</div>'].join('')
        });
    });

    g_iTimeItems.clear();
    g_iTimeItems.add(items);
    $('html,body').animate({ scrollTop: $(document).height() - $(window).height() }, 500);
};

// 窗口分割数
function changeWndNum(iType) {
    g_iWndTypes = parseInt(iType, 10);
    WebVideoCtrl.I_ChangeWndNum(g_iWndTypes);
}

// 登录
function login(szIP, szPort, szUsername, szPassword) {
    if ("" == szIP || "" == szPort)
        return;

    var iRet = WebVideoCtrl.I_Login(szIP, 1, szPort, szUsername, szPassword, {
        success: function (xmlDoc) {
            g_iLogins.set(szIP, []);
            logger(szIP + " 登录成功");

            setTimeout(function () {
                getChannelInfo(szIP);
            }, 100);
        },
        error: function () {
            logger(szIP + " 登录失败");
        }
    });

    if (-1 == iRet) logger(szIP + " 已登录过");
}

// 退出
function logout(szIP) {
    if (szIP == "")
        return;

    var iRet = WebVideoCtrl.I_Logout(szIP);
    if (0 == iRet) {
        g_iLogins.remove(szIP);
        logger(szIP + " 退出成功");
    } else {
        logger(szIP + " 退出失败");
    }
}

// 获取通道
function getChannelInfo(szIP) {
    if ("" == szIP)
        return;

    // 模拟通道
    WebVideoCtrl.I_GetAnalogChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("VideoInputChannel");
            var channels = g_iLogins.get(szIP);
            if (channels == null) channels = [];

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text();

                if ("" == name) name = "Camera " + (i < 9 ? "0" + (i + 1) : (i + 1));
                channels.push({ "id": id, "name": name, "zero": false });
            });
            logger(szIP + " 获取模拟通道成功");
        },
        error: function () {
            logger(szIP + " 获取模拟通道失败");
        }
    });

    // 数字通道
    WebVideoCtrl.I_GetDigitalChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("InputProxyChannelStatus");
            var channels = g_iLogins.get(szIP);
            if (channels == null) channels = [];

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text(),
					online = $(this).find("online").eq(0).text();

                if ("false" == online) // 过滤禁用的数字通道
                    return true;

                if ("" == name) name = "IPCamera " + (i < 9 ? "0" + (i + 1) : (i + 1));
                channels.push({ "id": id, "name": name, "zero": false });
            });
            logger(szIP + " 获取数字通道成功");
        },
        error: function () {
            logger(szIP + " 获取数字通道失败");
        }
    });

    // 零通道
    WebVideoCtrl.I_GetZeroChannelInfo(szIP, {
        async: false,
        success: function (xmlDoc) {
            var oChannels = $(xmlDoc).find("ZeroVideoChannel");
            var channels = g_iLogins.get(szIP);
            if (channels == null) channels = [];

            $.each(oChannels, function (i) {
                var id = $(this).find("id").eq(0).text(),
					name = $(this).find("name").eq(0).text(),
                    enabled = $(this).find("enabled").eq(0).text();

                if ("" == name) name = "Zero Channel " + (i < 9 ? "0" + (i + 1) : (i + 1));
                if ("true" == enabled) // 过滤禁用的零通道
                    channels.push({ "id": id, "name": name, "zero": true });
            });
            logger(szIP + " 获取零通道成功");
        },
        error: function () {
            logger(szIP + " 获取零通道失败");
        }
    });
}

// 获取设备信息
function getDeviceInfo(szIP) {
    if ("" == szIP)
        return;

    WebVideoCtrl.I_GetDeviceInfo(szIP, {
        success: function (xmlDoc) {
            var data = new Object();
            data.deviceID = $(xmlDoc).find("deviceID").eq(0).text();
            data.deviceName = $(xmlDoc).find("deviceName").eq(0).text();
            data.model = $(xmlDoc).find("model").eq(0).text();
            data.serialNumber = $(xmlDoc).find("serialNumber").eq(0).text();
            data.macAddress = $(xmlDoc).find("macAddress").eq(0).text();
            data.firmwareVersion = $(xmlDoc).find("firmwareVersion").eq(0).text() + " " + $(xmlDoc).find("firmwareReleasedDate").eq(0).text();
            data.encoderVersion = $(xmlDoc).find("encoderVersion").eq(0).text() + " " + $(xmlDoc).find("encoderReleasedDate").eq(0).text();
            logger(szIP + " 获取设备信息成功");
        },
        error: function () {
            logger(szIP + " 获取设备信息失败");
        }
    });
}

// 搜索录像
var iSearchTimes = 0;
var iSearchResult = [];
function recordSearch(szIP, iChannelID, bZeroChannel, szStartTime, szEndTime) {
    // 零通道不支持录像搜索
    if (bZeroChannel) {
        logger('零通道不支持录像搜索');
        return;
    }

    // 首次搜索
    if (0 == iSearchTimes) {
        iSearchResult = [];
        iSearchResult.IP = szIP;
        iSearchResult.Channel = iChannelID;
        iSearchResult.Zero = bZeroChannel;
    }

    WebVideoCtrl.I_RecordSearch(szIP, iChannelID, szStartTime, szEndTime, {
        iSearchPos: iSearchTimes * 40,
        success: function (xmlDoc) {
            var me = $(xmlDoc), status = me.find("responseStatusStrg").eq(0).text();
            if ("NO MATCHES" === status) {
                logger(szIP + "(" + iChannelID + ")未查询到录像");
                return;
            };

            // 最多查询3次，120条记录
            // if (iSearchTimes == 2) status = "OK";
            me.find("searchMatchItem").each(function () {
                var szPlaybackURI = $(this).children('playbackURI').text();
                if (szPlaybackURI.indexOf("name=") < 0) return true;

                var szType = $(this).children('metadataDescriptor').text();
                if ($.inArray(szType, g_iRecTypes) === -1) return true;

                var record = new Object();
                record.trackID = $(this).children('trackID').text();
                record.Type = szType;
                record.Start = vis.moment($(this).children('startTime').text()).utc().format("YYYY-MM-DD HH:mm:ss");
                record.End = vis.moment($(this).children('endTime').text()).utc().format("YYYY-MM-DD HH:mm:ss");
                record.PlaybackURI = szPlaybackURI;
                record.FileName = szPlaybackURI.substring(szPlaybackURI.indexOf("name=") + 5, szPlaybackURI.indexOf("&size="));
                record.FileSize = szPlaybackURI.substring(szPlaybackURI.indexOf("size=") + 5);
                record.Interval = vis.moment($(this).children('endTime').text()).diff(vis.moment($(this).children('startTime').text()), 'seconds')
                iSearchResult.push(record);
            });

            if ("MORE" === status) {
                iSearchTimes++;
                recordSearch(szIP, iChannelID, bZeroChannel, szStartTime, szEndTime); // 继续搜索
            } else if ("OK" === status) {
                logger(szIP + "(" + iChannelID + ")" + (iSearchResult.length > 0 ? '查询到' + iSearchResult.length + '条录像' : '未查询到录像'));
                redraw();
            }
        },
        error: function () {
            logger(szIP + "(" + iChannelID + ")查询录像失败");
        }
    });
}

// 开始回放
function startPlayback(szIP, iChannelID, bZeroChannel, szStartTime, szEndTime) {
    // 零通道不支持回放
    if (bZeroChannel) {
        return null;
    }

    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    // 已经在播放了，先停止
    if (oWndInfo != null) {
        WebVideoCtrl.I_Stop();
    }

    var iRet = WebVideoCtrl.I_StartPlayback(szIP, {
        iChannelID: iChannelID,
        szStartTime: szStartTime,
        szEndTime: szEndTime
    });

    if (0 == iRet) {
        logger(szIP + "(" + iChannelID + ")开始回放成功");
        return true;
    } else {
        logger(szIP + "(" + iChannelID + ")开始回放失败");
        return false;
    }
}

// 停止回放
function stopPlayback() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_Stop();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")停止回放成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")停止回放失败");
            return false;
        }
    }

    return null;
}

// 停止全部回放
function stopAllPlayback() {
    $(g_iWndStatus.values()).each(function () {
        if (this == null) return true;

        var oWndInfo = WebVideoCtrl.I_GetWindowStatus(this.Id);
        if (oWndInfo != null) {
            var iRet = WebVideoCtrl.I_Stop(this.Id);
            if (0 == iRet) {
                this.Recording = false;
                this.Playing = false;
            }
        }
    });

    logger("全部回放停止成功");
}

// 开始倒放
function reversePlayback(szIP, iChannelID, bZeroChannel, szStartTime, szEndTime) {
    // 零通道不支持回放
    if (bZeroChannel) {
        return null;
    }

    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    // 已经在播放了，先停止
    if (oWndInfo != null) {
        WebVideoCtrl.I_Stop();
    }

    var iRet = WebVideoCtrl.I_ReversePlayback(szIP, {
        iChannelID: iChannelID,
        szStartTime: szStartTime,
        szEndTime: szEndTime
    });

    if (0 == iRet) {
        logger(szIP + "(" + iChannelID + ")开始倒放成功");
        return true;
    } else {
        logger(szIP + "(" + iChannelID + ")开始倒放失败");
        return false;
    }
}

// 单帧
function frame() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_Frame();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")单帧播放成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")单帧播放失败");
            return false;
        }
    }

    return null;
}

// 暂停
function pause() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_Pause();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")暂停成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")暂停失败");
            return false;
        }
    }

    return null;
}

// 恢复
function resume() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_Resume();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")恢复成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")恢复失败");
            return false;
        }
    }

    return null;
}

// 慢放
function playSlow(speed) {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_PlaySlow();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")" + (speed === 0 ? '正常速度' : (speed > 0 ? ('快放X' + speed) : ('慢放X' + (speed * -1)))));
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")慢放失败");
            return false;
        }
    }

    return null;
}

// 快放
function playFast(speed) {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_PlayFast();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")" + (speed === 0 ? '正常速度' : (speed < 0 ? ('慢放X' + (speed * -1)) : ('快放X' + speed))));
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")快放失败");
            return false;
        }
    }

    return null;
}

// OSD时间
function getOSDTime() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var szTime = WebVideoCtrl.I_GetOSDTime();
        if (szTime != -1) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")获取OSD时间成功");
            return szTime;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")获取OSD时间失败");
        }
    }

    return null;
}

// 下载录像
function startDownloadRecord(szIP, iChannelID, szPlaybackURI) {
    var szFileName = szIP + "_" + iChannelID + "_" + new Date().getTime();
    var iDownloadID = WebVideoCtrl.I_StartDownloadRecord(szIP, szPlaybackURI, szFileName);
    if (iDownloadID >= 0) return iDownloadID;

    var iErrorValue = WebVideoCtrl.I_GetLastError();
    if (34 == iErrorValue) {
        logger(szIP + "(" + iChannelID + ")录像已存在");
    } else if (33 == iErrorValue) {
        logger(szIP + "(" + iChannelID + ")磁盘空间不足");
    } else {
        logger(szIP + "(" + iChannelID + ")下载失败");
    }

    return null;
}

// 停止下载
function stopDownloadRecord(szIP, iChannelID, iDownloadID) {
    var iRet = WebVideoCtrl.I_StopDownloadRecord(iDownloadID);
    if (0 == iRet) {
        logger(szIP + "(" + iChannelID + ")停止下载成功");
        return true;
    } else {
        logger(szIP + "(" + iChannelID + ")停止下载失败");
        return false;
    }
}

// 下载进度
function downProcess(iDownloadID) {
    var iStatus = WebVideoCtrl.I_GetDownloadStatus(iDownloadID);
    if (0 == iStatus) {
        var iProcess = WebVideoCtrl.I_GetDownloadProgress(iDownloadID);
        if (iProcess >= 100) {
            WebVideoCtrl.I_StopDownloadRecord(iDownloadID);
            logger("录像下载完成(" + iDownloadID + ")");
        }
        return iProcess;
    } else {
        WebVideoCtrl.I_StopDownloadRecord(iDownloadID);
        logger("录像已经下载失败(" + iDownloadID + ")");
        return -1;
    }
}

// 打开声音
function openSound() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        // 循环遍历所有窗口，如果有窗口打开了声音，先关闭
        var allWndInfo = WebVideoCtrl.I_GetWindowStatus();
        for (var i = 0, iLen = allWndInfo.length; i < iLen; i++) {
            var current = allWndInfo[i];
            if (current.bSound) {
                WebVideoCtrl.I_CloseSound(current.iIndex);
                break;
            }
        }

        // 打开声音
        var iRet = WebVideoCtrl.I_OpenSound();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")打开声音成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")打开声音失败");
            return false;
        }
    }

    return null;
}

// 关闭声音
function closeSound() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_CloseSound();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")关闭声音成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")关闭声音失败");
            return false;
        }
    }

    return null;
}

// 抓图
function capturePic() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var szPicName = oWndInfo.szIP + "_" + oWndInfo.iChannelID + "_" + new Date().getTime(),
			iRet = WebVideoCtrl.I_CapturePic(szPicName);

        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")抓图成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")抓图失败");
            return false;
        }
    }

    return null;
}

// 开始录像
function startRecord() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var szFileName = oWndInfo.szIP + "_" + oWndInfo.iChannelID + "_" + new Date().getTime(),
			iRet = WebVideoCtrl.I_StartRecord(szFileName);

        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")开始录像成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")开始录像失败");
            return false;
        }
    }

    return null;
}

// 停止录像
function stopRecord() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_StopRecord();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")停止录像成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")停止录像失败");
            return false;
        }
    }

    return null;
}

// 全屏
function fullScreen() {
    WebVideoCtrl.I_FullScreen(true);
}

// 获取本地参数
function getLocalCfg() {
    var xmlDoc = WebVideoCtrl.I_GetLocalCfg(),
        cfg = new Object();

    cfg.BuffNumberType = $(xmlDoc).find("BuffNumberType").eq(0).text();
    cfg.PlayWndType = $(xmlDoc).find("PlayWndType").eq(0).text();
    cfg.IVSMode = $(xmlDoc).find("IVSMode").eq(0).text();
    cfg.CaptureFileFormat = $(xmlDoc).find("CaptureFileFormat").eq(0).text();
    cfg.PackgeSize = $(xmlDoc).find("PackgeSize").eq(0).text();
    cfg.RecordPath = $(xmlDoc).find("RecordPath").eq(0).text();
    cfg.DownloadPath = $(xmlDoc).find("DownloadPath").eq(0).text();
    cfg.CapturePath = $(xmlDoc).find("CapturePath").eq(0).text();
    cfg.PlaybackPicPath = $(xmlDoc).find("PlaybackPicPath").eq(0).text();
    cfg.PlaybackFilePath = $(xmlDoc).find("PlaybackFilePath").eq(0).text();
    cfg.ProtocolType = $(xmlDoc).find("ProtocolType").eq(0).text();

    logger("本地配置获取成功");
    return cfg;
}

// 设置本地参数
function setLocalCfg(cfg) {
    var arrXml = [];

    arrXml.push("<LocalConfigInfo>");
    arrXml.push("<PackgeSize>" + cfg.PackgeSize + "</PackgeSize>");
    arrXml.push("<PlayWndType>" + cfg.PlayWndType + "</PlayWndType>");
    arrXml.push("<BuffNumberType>" + cfg.BuffNumberType + "</BuffNumberType>");
    arrXml.push("<RecordPath>" + cfg.RecordPath + "</RecordPath>");
    arrXml.push("<CapturePath>" + cfg.CapturePath + "</CapturePath>");
    arrXml.push("<PlaybackFilePath>" + cfg.PlaybackFilePath + "</PlaybackFilePath>");
    arrXml.push("<PlaybackPicPath>" + cfg.PlaybackPicPath + "</PlaybackPicPath>");
    arrXml.push("<DownloadPath>" + cfg.DownloadPath + "</DownloadPath>");
    arrXml.push("<IVSMode>" + cfg.IVSMode + "</IVSMode>");
    arrXml.push("<CaptureFileFormat>" + cfg.CaptureFileFormat + "</CaptureFileFormat>");
    arrXml.push("<ProtocolType>" + cfg.ProtocolType + "</ProtocolType>");
    arrXml.push("</LocalConfigInfo>");

    var iRet = WebVideoCtrl.I_SetLocalCfg(arrXml.join(""));
    if (0 == iRet) {
        logger("本地配置设置成功");
        return true;
    } else {
        logger("本地配置设置失败");
        return false;
    }
}

// 远程配置库
function remoteConfig(szIP) {
    if ("" == szIP)
        return;

    var iRet = WebVideoCtrl.I_RemoteConfig(szIP, {
        iLan: 1
    });

    if (-1 == iRet) {
        logger(szIP + " 调用远程配置库失败");
        return false;
    } else {
        logger(szIP + " 调用远程配置库成功");
        return true;
    }
}

// 恢复默认参数
function restoreDefault(szIP) {
    WebVideoCtrl.I_RestoreDefault(szIP, "basic", {
        timeout: 30000,
        success: function (xmlDoc) {
            $("#restartDiv").remove();
            logger(szIP + " 恢复默认参数成功");

            //恢复完成后需要重启
            WebVideoCtrl.I_Restart(szIP, {
                success: function (xmlDoc) {
                    $("<div id='restartDiv' class='freeze'>重启中...</div>").appendTo("body");
                    var oSize = getWindowSize();
                    $("#restartDiv").css({
                        width: oSize.width + "px",
                        height: oSize.height + "px",
                        lineHeight: oSize.height + "px",
                        left: 0,
                        top: 0
                    });
                    setTimeout(function () { reconnect(szIP); }, 20000);
                },
                error: function () {
                    logger(szIP + " 重启失败");
                }
            });
        },
        error: function () {
            logger(szIP + " 恢复默认参数失败");
        }
    });
}

// 重连
function reconnect(szIP) {
    WebVideoCtrl.I_Reconnect(szIP, {
        success: function (xmlDoc) {
            $("#restartDiv").remove();
        },
        error: function () {
            setTimeout(function () { reconnect(szIP); }, 5000);
        }
    });
}

// 检查插件版本
function clickCheckPluginVersion() {
    var iRet = WebVideoCtrl.I_CheckPluginVersion();
    if (0 == iRet) {
        alert("您的插件版本已经是最新的！");
    } else {
        alert("检测到新的插件版本！");
    }
}

// 打开选择框 0：文件夹 1：文件
function openFileDlg(iType) {
    return WebVideoCtrl.I_OpenFileDlg(iType);
}

//#endregion

//#region util

// 键值对类
function Map() {
    this.keys = new Array();
    this.data = new Object();

    //添加键值对
    this.set = function (key, value) {
        var _key = 'k' + key;
        if (this.contain(key) === false)
            this.keys.push(_key);

        this.data[_key] = value;
    };

    //获取键对应的值
    this.get = function (key) {
        var _key = 'k' + key;
        if (this.contain(key) === false)
            return null;

        return this.data[_key];
    };

    //去除键值
    this.remove = function (key) {
        var _key = 'k' + key;
        if (this.contain(key) === true) {
            this.keys.splice($.inArray(_key, this.keys), 1);
            delete this.data[_key];
        }
    };

    //判断键值元素是否存在
    this.contain = function (key) {
        var _key = 'k' + key;
        return $.inArray(_key, this.keys) > -1;
    };

    //判断键值元素是否为空
    this.isEmpty = function () {
        return this.keys.length == 0;
    };

    //获取键值元素大小
    this.size = function () {
        return this.keys.length;
    };

    //获取所有的值
    this.values = function () {
        var values = [];
        var map = this;
        $(this.keys).each(function () {
            values.push(map.data[this]);
        });
        return values;
    }
}

// 显示操作日志
logger = function (msg) {
    var me = $('#record-events > .panel-body');
    me.html('<div class="record-event"><strong>' + vis.moment().format('MM-DD HH:mm:ss') + '</strong> ' + msg + '</div>' + me.html());
};

// 设置组件尺寸
setWindowSize = function () {
    var oSize = getWindowSize();
    $('#nav-wrapper').css({ height: oSize.height - 40 });
    $('#recorder,#condition').css({ height: oSize.height - 100 });
    $('#record-events').css({ height: oSize.height - 282 });
};

// 获得窗口尺寸
getWindowSize = function () {
    var nWidth = $(this).width() + $(this).scrollLeft(),
		nHeight = $(this).height() + $(this).scrollTop();

    return { width: nWidth, height: nHeight };
};

// 获得录像类型
getRecordType = function (iTypeStr) {
    if ('motion' === iTypeStr)
        return 1;
    else if ('smart' === iTypeStr)
        return 1;
    else if ('timing' === iTypeStr)
        return 2;
    else if ('motionOrAlarm' === iTypeStr)
        return 3;
    else if ('motionAndAlarm' === iTypeStr)
        return 3;
    else if ('manual' === iTypeStr)
        return 4;

    return -1;
};

// 获得录像类型
getRecordTypeName = function (iTypeStr) {
    if ('motion' === iTypeStr)
        return '移动侦测';
    else if ('smart' === iTypeStr)
        return '智能录像';
    else if ('timing' === iTypeStr)
        return '定时录像';
    else if ('motionOrAlarm' === iTypeStr)
        return '动测或报警';
    else if ('motionAndAlarm' === iTypeStr)
        return '报警和动测';
    else if ('manual' === iTypeStr)
        return '手动录像';

    return '未定义';
};

//#endregion
