var g_iWndIndex = 0; // 当前选择窗口
var g_iWndTypes = 1; // 默认窗口数
var g_iWndStatus = new Map(); // 窗口的自定义状态
var g_iCameras = new Map(); // 所有的摄像机信息
var g_iLogins = new Map(); // 已登录的摄像机信息

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
        }
    });

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
function createStatus(id, ip, mask, channel, zero) {
    var status = new Object();
    status.Id = id; // 窗口序号1-16
    status.Ip = ip;
    status.Mask = mask;
    status.Channel = channel;
    status.Zero = zero;

    status.Playing = false;
    status.Recording = false;
    status.EZoom = false;
    status.DZoom = false;
    status.OpenSound = false;
    status.Volume = 50; // 0-100
    status.PTZSpeed = 4; // 1,2,3,4,5,6,7
    return status;
}

// 初始化窗口参数
function initStatus(status, ip, mask, channel, zero) {
    status.Ip = ip;
    status.Mask = mask;
    status.Channel = channel;
    status.Zero = zero;

    status.Playing = false;
    status.Recording = false;
    status.EZoom = false;
    status.DZoom = false;
    status.OpenSound = false;
    status.Volume = 50; // 0-100
    status.PTZSpeed = 4; // 1,2,3,4,5,6,7
}

// 重置控制图标
function resetIcons(status) {
    var play = $('#play').children(':first');
    if (status.Playing === false && play.hasClass('ipems-icon-font-pause')) {
        play.removeClass('ipems-icon-font-pause').addClass('ipems-icon-font-play');
    } else if (status.Playing === true && play.hasClass('ipems-icon-font-play')) {
        play.removeClass('ipems-icon-font-play').addClass('ipems-icon-font-pause');
    }

    var record = $('#record');
    if (status.Recording === true && record.hasClass('active') === false) {
        record.addClass('active');
    } else if (status.Recording === false && record.hasClass('active') === true) {
        record.removeClass('active');
    }

    var ezoom = $('#ezoom');
    if (status.EZoom === true && ezoom.hasClass('active') === false) {
        $('#controller > .zoom').removeClass('active');
        ezoom.addClass('active');
    } else if (status.EZoom === false && ezoom.hasClass('active') === true) {
        ezoom.removeClass('active');
    }

    var dzoom = $('#dzoom');
    if (status.DZoom === true && dzoom.hasClass('active') === false) {
        $('#controller > .zoom').removeClass('active');
        dzoom.addClass('active');
    } else if (status.DZoom === false && dzoom.hasClass('active') === true) {
        dzoom.removeClass('active');
    }

    var sound = $('#voice').children(':first');
    if (status.OpenSound === true && sound.hasClass('ipems-icon-font-unvoice')) {
        sound.removeClass('ipems-icon-font-unvoice').addClass('ipems-icon-font-voice');
    } else if (status.OpenSound === false && sound.hasClass('ipems-icon-font-voice')) {
        sound.removeClass('ipems-icon-font-voice').addClass('ipems-icon-font-unvoice');
    }
}

// 初始化控制图标
function initIcons() {
    var play = $('#play').children(':first');
    play.removeClass('ipems-icon-font-pause').addClass('ipems-icon-font-play');

    var record = $('#record').children(':first');
    record.removeClass('active');

    var ezoom = $('#ezoom').children(':first');
    ezoom.removeClass('active');

    var dzoom = $('#dzoom').children(':first');
    dzoom.removeClass('active');

    var sound = $('#voice').children(':first');
    sound.removeClass('ipems-icon-font-voice').addClass('ipems-icon-font-unvoice');
}

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

// 开始预览
function startRealPlay(szIP, iStreamType, iChannelID, bZeroChannel) {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);

    if ("" == szIP) return null;

    if (oWndInfo != null) {// 已经在播放了，先停止
        WebVideoCtrl.I_Stop();
    }

    var iRet = WebVideoCtrl.I_StartRealPlay(szIP, {
        iStreamType: iStreamType,
        iChannelID: iChannelID,
        bZeroChannel: bZeroChannel
    });

    if (0 == iRet) {
        logger(szIP + "(" + iChannelID + ")开始预览成功");
        return true;
    } else {
        logger(szIP + "(" + iChannelID + ")开始预览失败");
        return false;
    }
}

// 停止预览
function stopRealPlay() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_Stop();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")停止预览成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")停止预览失败");
            return false;
        }
    }

    return null;
}

// 停止全部预览
function stopAllRealPlay() {
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

    logger("全部预览停止成功");
}

// 恢复
function resume() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_Resume();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")恢复成功");
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")恢复失败");
        }
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

// 设置音量
function setVolume(iVolume) {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex),
		iVolume = parseInt(iVolume, 10);

    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_SetVolume(iVolume);
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")设置音量" + iVolume + "成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")设置音量" + iVolume + "失败");
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

// 启用电子放大
function enableEZoom() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_EnableEZoom();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")启用电子放大成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")启用电子放大失败");
            return false;
        }
    }

    return null;
}

// 禁用电子放大
function disableEZoom() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_DisableEZoom();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")禁用电子放大成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")禁用电子放大失败");
            return false;
        }
    }

    return null;
}

// 启用3D放大
function enable3DZoom() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_Enable3DZoom();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")启用3D放大成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")启用3D放大失败");
            return false;
        }
    }

    return null;
}

// 禁用3D放大
function disable3DZoom() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        var iRet = WebVideoCtrl.I_Disable3DZoom();
        if (0 == iRet) {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")禁用3D放大成功");
            return true;
        } else {
            logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")禁用3D放大失败");
            return false;
        }
    }

    return null;
}

// 全屏
function fullScreen() {
    WebVideoCtrl.I_FullScreen(true);
}

// PTZ控制 1,2,3,4,5,6,7,8为方向PTZ，9为自动
var g_bPTZAuto = false;
function startPTZControl(iPTZIndex, bZeroChannel, iPTZSpeed) {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (bZeroChannel) { // 零通道不支持云台
        return;
    }

    if (oWndInfo != null) {
        if (9 == iPTZIndex && g_bPTZAuto) {
            iPTZSpeed = 0;// 自动开启后，速度置为0可以关闭自动
        } else {
            g_bPTZAuto = false;// 点击其他方向，自动肯定会被关闭
        }

        WebVideoCtrl.I_PTZControl(iPTZIndex, false, {
            iPTZSpeed: iPTZSpeed,
            success: function (xmlDoc) {
                if (9 == iPTZIndex) {
                    g_bPTZAuto = !g_bPTZAuto;
                }
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")开启云台成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")开启云台失败");
            }
        });
    }
}

// PTZ停止
function endPTZControl() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(1, true, {
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")停止云台成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")停止云台失败");
            }
        });
    }
}

// 调焦+
function PTZZoomIn() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(10, false, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")调焦+成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")调焦+失败");
            }
        });
    }
}

// 调焦-
function PTZZoomOut() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(11, false, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")调焦-成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")调焦-失败");
            }
        });
    }
}

// 停止调焦
function PTZZoomStop() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(11, true, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")调焦停止成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")调焦停止失败");
            }
        });
    }
}

// 聚焦+
function PTZFocusIn() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(12, false, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")聚焦+成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")聚焦+失败");
            }
        });
    }
}

// 聚焦-
function PTZFoucusOut() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(13, false, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")聚焦-成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")聚焦-失败");
            }
        });
    }
}

// 停止聚焦
function PTZFoucusStop() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(12, true, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")聚焦停止成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")聚焦停止失败");
            }
        });
    }
}

// 光圈+
function PTZIrisIn() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(14, false, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")光圈+成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")光圈+失败");
            }
        });
    }
}

// 光圈-
function PTZIrisOut() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(15, false, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")光圈-成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")光圈-失败");
            }
        });
    }
}

// 停止光圈
function PTZIrisStop() {
    var oWndInfo = WebVideoCtrl.I_GetWindowStatus(g_iWndIndex);
    if (oWndInfo != null) {
        WebVideoCtrl.I_PTZControl(14, true, {
            iWndIndex: g_iWndIndex,
            success: function (xmlDoc) {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")光圈停止成功");
            },
            error: function () {
                logger(oWndInfo.szIP + "(" + oWndInfo.iChannelID + ")光圈停止失败");
            }
        });
    }
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
    var me = $('#ptz-events > .panel-body');
    me.html('<div class="ptz-event"><small><strong>' + dateFormat(new Date(), "yyyy-MM-dd hh:mm:ss") + '</strong> ' + msg + '</small></div>' + me.html());
};

// 格式化日期
dateFormat = function (oDate, fmt) {
    var o = {
        "M+": oDate.getMonth() + 1, //月份
        "d+": oDate.getDate(), //日
        "h+": oDate.getHours(), //小时
        "m+": oDate.getMinutes(), //分
        "s+": oDate.getSeconds(), //秒
        "q+": Math.floor((oDate.getMonth() + 3) / 3), //季度
        "S": oDate.getMilliseconds()//毫秒
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (oDate.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
};

// 设置组件尺寸
setWindowSize = function () {
    var oSize = getWindowSize();
    $('#nav-wrapper').css({ height: oSize.height - 40 });
    $('#recorder').css({ height: oSize.height - 62 });
};

// 设置窗口尺寸
getWindowSize = function () {
    var nWidth = $(this).width() + $(this).scrollLeft(),
		nHeight = $(this).height() + $(this).scrollTop();

    return { width: nWidth, height: nHeight };
};

//#endregion
