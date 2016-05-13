(function () {
    var h5template = [
        '<audio id="media-player" controls="controls" style="width:100%;">',
            '<source src="" type="audio/wav">',
            '<div class="alert alert-danger" role="alert">',
                '<span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>',
                '<span class="sr-only">Error:</span>',
                '浏览器不支持HTML5 Audio，请将浏览器升级到最新版本。',
            '</div>',
        '</audio>'
    ];

    var wmptemplate = [
        '<object id="media-player" classid="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6" type="application/x-oleobject" style="width:100%; height:65px;">',
            '<param name="URL" value="" />',
            '<param name="volume" value="100" />',
        '</object>'
    ];

    var wmptips = [
        '<div class="alert alert-danger" role="alert">',
            '<span class="glyphicon glyphicon-exclamation-sign" aria-hidden="true"></span>',
            '<span class="sr-only">Error:</span>',
            '系统未安装Windows Media Player或安装版本较低，请安装最新版本的Windows Media Player。',
        '</div>'
    ];

    $.ieVersion = (function () {
        var ua = window.navigator.userAgent;
        var msie = ua.indexOf('MSIE ');
        var trident = ua.indexOf('Trident/');

        if (msie > 0) {
            return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
        }

        if (trident > 0) {
            var rv = ua.indexOf('rv:');
            return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
        }

        return -1;
    }());

    $.isIE = $.ieVersion > -1;

    $.playing = false;

    $.initDelay = 20;

    $.startDelay = 5;

    var installed = function () {
        var activex,
            plugin;

        try {
            if (window.ActiveXObject || 'ActiveXObject' in window)
                activex = new ActiveXObject('WMPlayer.OCX.7');
            else if (window.GeckoActiveXObject || 'GeckoActiveXObject' in window)
                activex = new GeckoActiveXObject('WMPlayer.OCX.7');
        } catch (oError) { }

        try {
            if (navigator.mimeTypes)
                plugin = navigator.mimeTypes['application/x-mplayer2'].enabledPlugin;
        } catch (oError) { }

        return (activex || plugin);
    };

    var play = function (text) {
        var url = "/Home/Speaker?text=" + encodeURIComponent(text);
        if ($.player) {
            var player = $.player[0];
            if (!$.isIE) {
                player.src = url;
                player.play();
            } else {
                player.URL = url;
                player.controls.play();
            }
        }
    };

    var stop = function () {
        if ($.player) {
            var player = $.player[0];
            if (!$.isIE) {
                player[0].pause();
                player[0].currentTime = 0;
            } else {
                player[0].controls.stop();
            }
        }
    };

    var init = function () {
        if ($.contents && $.contents.length > 0)
            return false;

        $.ajax({
            url: "/Home/GetSpeech",
            dataType: 'json',
            cache: false,
            beforeSend: function(XHR){
                if ($.initTask)
                    window.clearInterval($.initTask);
            },
            success: function (data, textStatus, jqXHR) {
                if (data.success)
                    $.contents = data.data;
            }
        }).always(function () {
            $.initTask = window.setInterval(init, $.initDelay * 1000);
        });
    };

    var start = function () {
        if ($.playing)
            return false;

        if ($.contents && $.contents.length > 0) {
            $.playing = true;
            window.setTimeout(function () {
                play($.contents.shift());
            }, 1000);
        }
    };

    $(document).ready(function () {
        var container = $("#media-container");
        if (container) {
            if ($.isIE)
                container.html(installed() ? wmptemplate.join("") : wmptips.join(""));
            else
                container.html(h5template.join(""));
        }

        $.player = $('#media-player');
        if ($.player && !$.isIE) {
            $.player.on('ended', function () {
                $.playing = false;
            });
        }

        init();
        window.setInterval(start, $.startDelay * 1000);
    });
})();