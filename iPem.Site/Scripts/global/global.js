﻿/* ========================================================================
 * Global: global.js
 * /Scripts/global/global.js
 * ========================================================================
 */

Ext.require(['*']);
Ext.setGlyphFontFamily('ipems-icon-font');

/*ajax timeout*/
Ext.Ajax.timeout = 300000;
Ext.override(Ext.data.Connection, { timeout: Ext.Ajax.timeout });
Ext.override(Ext.data.JsonP, { timeout: Ext.Ajax.timeout });
Ext.override(Ext.data.proxy.Server, { timeout: Ext.Ajax.timeout });
Ext.override(Ext.data.proxy.Ajax, { timeout: Ext.Ajax.timeout });
Ext.override(Ext.form.Basic, {
    timeout: Ext.Ajax.timeout / 1000,
    afterAction: function (action, success) {
        this.callParent(arguments);

        //prevent the failed window
        var prevent = action.preventWindow || false;
        if (prevent !== true && !success) {
            var message = '';
            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                message = action.result.message;

            $$iPems.ShowFailure(action.response, message);
        }
    },
    getValues: function (asString, dirtyOnly, includeEmptyText, useDataValues) {
        var values = {},
            fields = this.getFields().items,
            f,
            fLen = fields.length,
            isArray = Ext.isArray,
            field, data, val, bucket, name;

        for (f = 0; f < fLen; f++) {
            field = fields[f];

            if (!dirtyOnly || field.isDirty()) {
                data = field[useDataValues ? 'getModelData' : 'getSubmitData'](includeEmptyText);

                if (Ext.isObject(data)) {
                    for (name in data) {
                        if (data.hasOwnProperty(name)) {
                            val = data[name];

                            //dont submit empty values when includeEmptyText is false.
                            if (!includeEmptyText && val === '') {
                                continue;
                            }

                            if (includeEmptyText && val === '') {
                                val = field.emptyText || '';
                            }

                            if (values.hasOwnProperty(name)) {
                                bucket = values[name];

                                if (!isArray(bucket)) {
                                    bucket = values[name] = [bucket];
                                }

                                if (isArray(val)) {
                                    values[name] = bucket.concat(val);
                                } else {
                                    bucket.push(val);
                                }
                            } else {
                                values[name] = val;
                            }
                        }
                    }
                }
            }
        }

        if (asString) {
            values = Ext.Object.toQueryString(values);
        }
        return values;
    }
});

/*global ajax exception handler*/
Ext.override(Ext.Ajax, { unauthorizedCode: 400 });
Ext.Ajax.on('beforerequest', function (conn, options) {
    if (!Ext.isEmpty(options.mask))
        options.mask.show();
});
Ext.Ajax.on('requestcomplete', function (conn, response, options) {
    if (!Ext.isEmpty(options.mask))
        options.mask.hide();
});
Ext.Ajax.on('requestexception', function (conn, response, options) {
    if (!Ext.isEmpty(options.mask))
        options.mask.hide();

    if (response.status === Ext.Ajax.unauthorizedCode) {
        var data = Ext.decode(response.responseText, true);
        if (!Ext.isEmpty(data) && !Ext.isEmpty(data.LoginUrl)) {
            window.location.href = data.LoginUrl;
            return false;
        }
    }

    //prevent the failed window
    var prevent = options.preventWindow || false;
    if (prevent !== true) {
        $$iPems.ShowFailure(response, response.responseText);
    }
});
Ext.direct.Manager.on('exception', function (event) {
    if (!Ext.isEmpty(event) && !Ext.isEmpty(event.message))
        Ext.Msg.show({ title: '系统错误', msg: event.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
});

/*global datetime formart*/
Ext.override(Ext.form.field.Date, {
    format: 'Y-m-d'
});

/*show failure window*/
window.$$iPems.ShowFailure = function (response, errorMsg) {
    var bodySize = Ext.getBody().getViewSize(),
        width = (bodySize.width < 500) ? bodySize.width - 50 : 500,
        height = (bodySize.height < 300) ? bodySize.height - 50 : 300,
        win;

    if (Ext.isEmpty(errorMsg))
        errorMsg = response.responseText;

    //prevent the failed window when redirect to an new page.
    if (response.status === 0
        && Ext.isEmpty(response.statusText)
        && Ext.isEmpty(errorMsg))
        return false;

    win = new Ext.window.Window({
        modal: true,
        width: width,
        height: height,
        title: '系统错误',
        layout: "fit",
        maximizable: true,
        items: [{
            xtype: "container",
            layout: {
                type: "vbox",
                align: "stretch"
            },
            items: [
                {
                    xtype: "container",
                    height: 42,
                    layout: "absolute",
                    defaultType: "label",
                    items: [
                        {
                            xtype: "component",
                            x: 5,
                            y: 5,
                            html: '<div class="x-message-box-error" style="width:32px;height:32px"></div>'
                        },
                        {
                            x: 42,
                            y: 5,
                            html: "<b>Status Code: </b>"
                        },
                        {
                            x: 128,
                            y: 5,
                            text: response.status
                        },
                        {
                            x: 42,
                            y: 23,
                            html: "<b>Status Text: </b>"
                        },
                        {
                            x: 128,
                            y: 23,
                            text: response.statusText
                        }
                    ]
                },
                {
                    flex: 1,
                    xtype: "htmleditor",
                    value: errorMsg,
                    readOnly: true,
                    enableAlignments: false,
                    enableColors: false,
                    enableFont: false,
                    enableFontSize: false,
                    enableFormat: false,
                    enableLinks: false,
                    enableLists: false,
                    enableSourceEdit: false
                }
            ]
        }]
    });

    win.show();
}

Ext.override(Ext.panel.Header, {
    initComponent: function () {
        this.callParent(arguments);
        this.insert(0, this.iconCmp);
        this.insert(1, this.titleCmp);
    }
});

/*
 修复当Grid分组，然后折叠第一组，其他组无法选中行的问题
*/
Ext.override(Ext.view.Table, {
    getRecord: function (node) {
        node = this.getNode(node);
        if (node) {
            return this.dataSource.data.get(node.getAttribute('data-recordId'));
        }
    },
    indexInStore: function (node) {
        node = this.getNode(node, true);
        if (!node && node !== 0) {
            return -1;
        }

        return this.dataSource.indexOf(this.getRecord(node));
    }
});

window.$$iPems.Action = { Add: 0, Edit: 1, Delete: 2 };
window.$$iPems.SSH = { Root: -1, Area: 0, Station: 1, Room: 2, Fsu: 3, Device: 4, Point: 5 };
window.$$iPems.Point = { AL: 0, DO: 1, AO: 2, AI: 3, DI: 4 };
window.$$iPems.State = { Normal: 0, Level1: 1, Level2: 2, Level3: 3, Level4: 4, Opevent: 5, Invalid: 6 };
window.$$iPems.Level = { Level0: 0, Level1: 1, Level2: 2, Level3: 3, Level4: 4 };
window.$$iPems.Result = { Undefine: -1, Failure: 0, Success: 1 };

window.$$iPems.GetStateCls = function (value) {
    switch (value) {
        case $$iPems.State.Normal:
            return 'point-status-normal';
        case $$iPems.State.Level1:
            return 'point-status-level1';
        case $$iPems.State.Level2:
            return 'point-status-level2';
        case $$iPems.State.Level3:
            return 'point-status-level3';
        case $$iPems.State.Level4:
            return 'point-status-level4';
        case $$iPems.State.Opevent:
            return 'point-status-opevent';
        case $$iPems.State.Invalid:
            return 'point-status-invalid';
        default:
            return '';
    }
};

window.$$iPems.GetLevelCls = function (value) {
    switch (value) {
        case $$iPems.Level.Level0:
            return 'alm-level0';
        case $$iPems.Level.Level1:
            return 'alm-level1';
        case $$iPems.Level.Level2:
            return 'alm-level2';
        case $$iPems.Level.Level3:
            return 'alm-level3';
        case $$iPems.Level.Level4:
            return 'alm-level4';
        default:
            return '';
    }
};

window.$$iPems.UpdateIcons = function (tree, nodes) {
    nodes = nodes || [];

    if (nodes.length === 0) {
        var root = tree.getRootNode();
        if (root.hasChildNodes()) {
            root.eachChild(function (c) {
                c.cascadeBy(function (n) {
                    nodes.push(n.getId());
                });
            });
        }
    }

    if (nodes.length === 0) return;

    Ext.Ajax.request({
        url: '/Account/GetNodeIcons',
        method: 'POST',
        jsonData: nodes,
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                var icons = {},
                    root = tree.getRootNode();

                Ext.each(data.data, function (item, index, allItems) {
                    icons[item.id] = item;
                });

                $$iPems.SetIcon(root, icons[root.getId()]);
                if (root.hasChildNodes()) {
                    root.eachChild(function (c) {
                        c.cascadeBy(function (n) {
                            $$iPems.SetIcon(n, icons[n.getId()]);
                        });
                    });
                }
            }
        }
    });
};

window.$$iPems.SetIcon = function (node, icon) {
    if (Ext.isEmpty(icon)) return;

    var prefix = '', type = icon.type, level = icon.level;

    if (type === $$iPems.SSH.Root)
        prefix = 'all'
    else if (type === $$iPems.SSH.Area)
        prefix = 'diqiu'
    else if (type === $$iPems.SSH.Station)
        prefix = 'juzhan'
    else if (type === $$iPems.SSH.Room)
        prefix = 'room'
    else if (type === $$iPems.SSH.Device)
        prefix = 'device'

    if (level === $$iPems.Level.Level0)
        node.set('iconCls', Ext.String.format('{0}-level-0', prefix));
    else if (level === $$iPems.Level.Level1)
        node.set('iconCls', Ext.String.format('{0}-level-1', prefix));
    else if (level === $$iPems.Level.Level2)
        node.set('iconCls', Ext.String.format('{0}-level-2', prefix));
    else if (level === $$iPems.Level.Level3)
        node.set('iconCls', Ext.String.format('{0}-level-3', prefix));
    else if (level === $$iPems.Level.Level4)
        node.set('iconCls', Ext.String.format('{0}-level-4', prefix));
};

window.$$iPems.FileType = {
    zip: [".zip", ".rar", ".7z", ".gz", ".tar", ".jar"],
    img: [".bmp", ".jpg", ".jpeg", ".png", ".gif", ".ico"],
    music: [".mp3", ".wav", ".wma", ".ogg", ".m4a", ".mmf", ".amr"],
    video: [".mp4", ".3gp", ".mpg", ".avi", ".wmv", ".flv", ".swf"],
    word: [".doc", ".docx", ".wps", ".rtf"],
    excel: [".xls", ".xlsx", ".et", ".csv"],
    power: [".ppt", ".pptx", ".dps"],
    exe: [".exe", ".msi", ".cmd", ".bat"],
    pdf: [".pdf"],
    txt: [".txt", ".log", ".cfg", ".config", ".sql"],
    xml: [".xml", ".js", ".css", ".cs"],
    html: [".htm", ".html", ".shtml", ".shtm", ".cshtml"]
};

window.$$iPems.FileIcon16 = function (file) {
    if (Ext.isEmpty(file)) return '/Content/themes/icons/files/file16.png';

    var ext = file.substr(file.lastIndexOf(".")).toLowerCase();

    if (Ext.Array.contains($$iPems.FileType.zip, ext)) {
        return "/Content/themes/icons/files/zip16.png"
    } else if (Ext.Array.contains($$iPems.FileType.img, ext)) {
        return "/Content/themes/icons/files/img16.png"
    } else if (Ext.Array.contains($$iPems.FileType.music, ext)) {
        return "/Content/themes/icons/files/music16.png"
    } else if (Ext.Array.contains($$iPems.FileType.video, ext)) {
        return "/Content/themes/icons/files/video16.png"
    } else if (Ext.Array.contains($$iPems.FileType.word, ext)) {
        return "/Content/themes/icons/files/word16.png"
    } else if (Ext.Array.contains($$iPems.FileType.excel, ext)) {
        return "/Content/themes/icons/files/excel16.png"
    } else if (Ext.Array.contains($$iPems.FileType.power, ext)) {
        return "/Content/themes/icons/files/pp16.png"
    } else if (Ext.Array.contains($$iPems.FileType.exe, ext)) {
        return "/Content/themes/icons/files/exe16.png"
    } else if (Ext.Array.contains($$iPems.FileType.pdf, ext)) {
        return "/Content/themes/icons/files/pdf16.png"
    } else if (Ext.Array.contains($$iPems.FileType.txt, ext)) {
        return "/Content/themes/icons/files/txt16.png"
    } else if (Ext.Array.contains($$iPems.FileType.xml, ext)) {
        return "/Content/themes/icons/files/xml16.png"
    } else if (Ext.Array.contains($$iPems.FileType.html, ext)) {
        return "/Content/themes/icons/files/html16.png"
    }

    return '/Content/themes/icons/files/file16.png';
};

window.$$iPems.FileIcon32 = function (file) {
    if (Ext.isEmpty(file)) return '/Content/themes/icons/files/file32.png';

    var ext = file.substr(file.lastIndexOf(".")).toLowerCase();

    if (Ext.Array.contains($$iPems.FileType.zip, ext)) {
        return "/Content/themes/icons/files/zip32.png"
    } else if (Ext.Array.contains($$iPems.FileType.img, ext)) {
        return "/Content/themes/icons/files/img32.png"
    } else if (Ext.Array.contains($$iPems.FileType.music, ext)) {
        return "/Content/themes/icons/files/music32.png"
    } else if (Ext.Array.contains($$iPems.FileType.video, ext)) {
        return "/Content/themes/icons/files/video32.png"
    } else if (Ext.Array.contains($$iPems.FileType.word, ext)) {
        return "/Content/themes/icons/files/word32.png"
    } else if (Ext.Array.contains($$iPems.FileType.excel, ext)) {
        return "/Content/themes/icons/files/excel32.png"
    } else if (Ext.Array.contains($$iPems.FileType.power, ext)) {
        return "/Content/themes/icons/files/pp32.png"
    } else if (Ext.Array.contains($$iPems.FileType.exe, ext)) {
        return "/Content/themes/icons/files/exe32.png"
    } else if (Ext.Array.contains($$iPems.FileType.pdf, ext)) {
        return "/Content/themes/icons/files/pdf32.png"
    } else if (Ext.Array.contains($$iPems.FileType.txt, ext)) {
        return "/Content/themes/icons/files/txt32.png"
    } else if (Ext.Array.contains($$iPems.FileType.xml, ext)) {
        return "/Content/themes/icons/files/xml32.png"
    } else if (Ext.Array.contains($$iPems.FileType.html, ext)) {
        return "/Content/themes/icons/files/html32.png"
    }

    return '/Content/themes/icons/files/file32.png';
};

/*global delimiter*/
window.$$iPems.Delimiter = ';';
window.$$iPems.Separator = '┆';

/*Split Node Keys*/
window.$$iPems.SplitKeys = function (key) {
    if(Ext.isEmpty(key)) return [];
    return key.split($$iPems.Separator);
};

/*download files via ajax*/
window.$$iPems.download = function (config) {
    config = config || {};
    var url = config.url,
        method = config.method || 'POST',
        params = config.params || {};

    var form = Ext.create('Ext.form.Panel', {
        standardSubmit: true,
        url: url,
        method: method
    });

    form.submit({
        target: '_blank',
        params: params
    });

    Ext.defer(function () {
        form.close();
    }, 100);
};

/*datetime parse funtion*/
window.$$iPems.datetimeParse = function (date, format) {
    return Ext.Date.parse(date, format || 'Y-m-d H:i:s', true);
};

/*date parse funtion*/
window.$$iPems.dateParse = function (date, format) {
    return Ext.Date.parse(date, format || 'Y-m-d', true);
};

/*clone paging toolbar*/
window.$$iPems.clonePagingToolbar = function (store) {
    return Ext.create('Ext.PagingToolbar', {
        store: store,
        displayInfo: true,
        items: ['-',
            {
                store: new Ext.data.Store({
                    fields: [{ name: 'id', type: 'int' }, { name: 'text', type: 'string' }, { name: 'comment', type: 'string' }],
                    data: [
                        { id: 10, text: '10', comment: '10' },
                        { id: 20, text: '20', comment: '20' },
                        { id: 50, text: '50', comment: '50' },
                        { id: 100, text: '100', comment: '100' },
                        { id: 200, text: '200', comment: '200' },
                        { id: 500, text: '500', comment: '500' }
                    ]
                }),
                xtype: 'combobox',
                fieldLabel: '显示条数',
                displayField: 'text',
                valueField: 'id',
                typeAhead: true,
                queryMode: 'local',
                triggerAction: 'all',
                emptyText: '每页显示的条数',
                selectOnFocus: true,
                forceSelection: true,
                labelWidth: 60,
                width: 200,
                value: store.pageSize,
                listeners: {
                    select: function (combo) {
                        store.pageSize = parseInt(combo.getValue());
                        store.loadPage(1);
                    }
                }
            }
        ]
    });
};

/*select tree node by paths*/
window.$$iPems.selectNodePath = function (tree, paths, callback) {
    var root = tree.getRootNode(),
        field = 'id',
        separator = '/',
        path = separator + root.get(field) + separator + paths.join(separator);

    tree.selectPath(path, field, separator, callback || Ext.emptyFn);
};

window.$$iPems.animateNumber = function (target, now) {
    if (Ext.isString(target))
        target = Ext.get(target);

    now = Math.round(now);
    var display = target.getHTML();
    var current = Ext.isEmpty(display) ? 0 : parseInt(display);
    var count = Math.abs(now - current);
    var numUp = now > current;

    var step = 25,
        speed = Math.round(count / step),
        int_speed = 25;

    var interval = setInterval(function () {
        if (numUp) {
            if (speed > 1 && current + speed < now) {
                current += speed;
                target.setHTML(current);
            } else if (current < now) {
                current += 1;
                target.setHTML(current);
            } else {
                clearInterval(interval);
            }
        } else {
            if (speed > 1 && current - speed > now) {
                current -= speed;
                target.setHTML(current);
            } else if (current > now) {
                current -= 1;
                target.setHTML(current);
            } else {
                clearInterval(interval);
            }
        }
    }, int_speed);
}

window.$$iPems.formulaResults = {
    Success: '验证成功',
    Failure: '验证失败',
    E01: '不允许连续的运算符',
    E02: '不允许空括号',
    E03: '括号配对失败',
    E04: '变量格式错误({0})',
    E05: '变量存在多个特殊字符"@"({0})',
    E06: '变量存在多个特殊字符">>"({0})',
    E07: '"("后面不允许直接是运算符',
    E08: '")"前面不允许直接是运算符',
    E09: '"("前面仅允许是运算符或"("',
    E10: '")"后面仅允许是运算符或")"'
};

window.$$iPems.validateFormula = function (formula, allowEmpty) {
    result = $$iPems.formulaResults.Success
    formula = formula.replace(/\s+/g, '');

    // 空字符串,直接返回
    if ('' === formula)
        return (allowEmpty || true) ? $$iPems.formulaResults.Success : $$iPems.formulaResults.Failure;

    // 运算符连续
    if (/[\+\-\*\/]{2,}/.test(formula))
        return $$iPems.formulaResults.E01;

    // 空括号
    if (/\(\)/.test(formula))
        return $$iPems.formulaResults.E02;

    // 括号不配对
    var stack = [];
    for (var i = 0, item; i < formula.length; i++) {
        item = formula.charAt(i);
        if ('(' === item)
            stack.push('(');
        else if (')' === item) {
            if (0 === stack.length) {
                result = $$iPems.formulaResults.E03;
                return false;
            }

            stack.pop();
        }
    }

    if ($$iPems.formulaResults.Success !== result)
        return result;

    if (0 !== stack.length)
        return $$iPems.formulaResults.E03;

    // (后面是运算符
    if (/\([\+\-\*\/]/.test(formula))
        return $$iPems.formulaResults.E07;

    // )前面是运算符
    if (/[\+\-\*\/]\)/.test(formula))
        return $$iPems.formulaResults.E08;

    // (前面不是运算符和(
    if (/[^\+\-\*\/\(]\(/.test(formula))
        return $$iPems.formulaResults.E09;

    // )后面不是运算符和)
    if (/\)[^\+\-\*\/\)]/.test(formula))
        return $$iPems.formulaResults.E10;

    var variables = $$iPems.SplitKeys(formula.replace(/\(|\)/g, '').replace(/[\+\-\*\/]/g, $$iPems.Separator));
    Ext.Array.each(variables, function (item, index) {
        if (!/^\d+(\.\d+)?$/.test(item)) {
            if (!/^@.+>>.+>>.+$/.test(item)) {
                result = Ext.String.format($$iPems.formulaResults.E04, item);
                return false;
            }

            var starts = item.match(/@/g);
            if (starts.length !== 1) {
                result = Ext.String.format($$iPems.formulaResults.E05, item);
                return false;
            }

            var separators = item.match(/>>/g);
            if (separators.length !== 2) {
                result = Ext.String.format($$iPems.formulaResults.E06, item);
                return false;
            }
        }
    });

    return result;
};

window.$$iPems.insertAtCursor = function (target, text) {
    var me = target.inputEl.dom,
        val = target.getValue();

    target.focus();
    if (document.selection) {
        var sel = document.selection.createRange();
        sel.text = text;
    } else if (typeof me.selectionStart === 'number' && typeof me.selectionEnd === 'number') {
        var startPos = me.selectionStart,
            endPos = me.selectionEnd,
            cursorPos = startPos;

        target.setValue(val.substring(0, startPos) + text + val.substring(endPos, val.length));
        cursorPos += text.length;
        me.selectionStart = me.selectionEnd = cursorPos;
    } else {
        target.setValue(val + text);
    }
};

window.$$iPems.deleteAtCursor = function (target) {
    var me = target.inputEl.dom,
        val = target.getValue();

    target.focus();
    if (document.selection) {
        var sel = document.selection.createRange();
        if (sel.text.length > 0) {
            document.selection.clear();
        } else {
            sel.moveStart('character', -1);
            document.selection.clear();
        }
    } else if (typeof me.selectionStart === 'number' && typeof me.selectionEnd === 'number') {
        var startPos = me.selectionStart,
            endPos = me.selectionEnd;

        if (startPos === endPos)
            startPos = (startPos >= 1 ? startPos - 1 : 0);

        target.setValue(val.substring(0, startPos) + val.substring(endPos, val.length));
        me.selectionStart = me.selectionEnd = startPos;
    } else {
        target.setValue(val.substring(0, val.length - 1));
    }
};

//global tasks
window.$$iPems.Tasks = {
    badgeTask: Ext.util.TaskManager.newTask({
        run: function () {
            Ext.Ajax.request({
                url: '/Home/GetBadges',
                preventWindow: true,
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success) {
                        var me = data.data;
                        if (!Ext.isEmpty(me)) {
                            var ntetip = Ext.get('noticeCount'),
                                almtip = Ext.get('actAlmCount');

                            if (!Ext.isEmpty(ntetip)) {
                                ntetip.setHTML(me.notices > 99 ? '99+' : me.notices);
                                ntetip.setDisplayed(me.notices > 0);
                            }

                            if (!Ext.isEmpty(almtip)) {
                                almtip.setHTML(me.alarms > 99 ? '99+' : me.alarms);
                                almtip.setDisplayed(me.alarms > 0);
                            }

                            //restart
                            $$iPems.Tasks.badgeTask.fireOnStart = false;
                            $$iPems.Tasks.badgeTask.restart();
                        }
                    }
                }
            });
        },
        fireOnStart: true,
        interval: 15000,
        repeat: 1
    }),
    actAlmTask: Ext.util.TaskManager.newTask({
        run: Ext.emptyFn,
        fireOnStart: true,
        interval: 15000,
        repeat: 1
    }),
    actPointTask: Ext.util.TaskManager.newTask({
        run: Ext.emptyFn,
        fireOnStart: true,
        interval: 15000,
        repeat: 1
    }),
    homeTasks: {
        almTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 15000,
            repeat: 1
        }),
        svrTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 15000,
            repeat: 1
        }),
        energyTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 60000,
            repeat: 1
        }),
        cuttingTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 60000,
            repeat: 1
        }),
        powerTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 60000,
            repeat: 1
        }),
        offTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 60000,
            repeat: 1
        }),
        consumptionTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 300000,
            repeat: 1
        })
    },
    fsuTask: Ext.util.TaskManager.newTask({
        run: Ext.emptyFn,
        fireOnStart: true,
        interval: 10000,
        repeat: 1
    })
};