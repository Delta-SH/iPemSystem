Ext.require(['*']);
Ext.setGlyphFontFamily('ipems-icon-font');

/*ajax timeout*/
Ext.Ajax.timeout = 300000;
Ext.override(Ext.data.Connection, { timeout: Ext.Ajax.timeout });
Ext.override(Ext.data.JsonP, { timeout: Ext.Ajax.timeout });
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

/*override line highlight*/
Ext.override(Ext.chart.series.Line, {
    highlightItem: function () {
        var me = this,
            line = me.line;

        Ext.chart.series.Line.superclass.highlightItem.apply(this, arguments);
        if (line && !me.highlighted && me.highlightLine !== false) { // added the third condition 
            if (!('__strokeWidth' in line)) {
                line.__strokeWidth = parseFloat(line.attr['stroke-width']) || 0;
            }
            if (line.__anim) {
                line.__anim.paused = true;
            }

            line.__anim = new Ext.fx.Anim({
                target: line,
                to: {
                    'stroke-width': line.__strokeWidth + 3
                }
            });
            me.highlighted = true;
        }
    }
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

/*ajax action*/
window.$$iPems.Action = {
    Add: 0,
    Edit: 1,
    Delete: 2
};

/*organization*/
window.$$iPems.Organization = {
    Area: 0,
    Station: 1,
    Room: 2,
    Device: 3,
    Point: 4
};

/** 
Point Type
0-遥信信号（DI）
1-遥测信号（AI）
2-遥控信号（DO）
3-遥调信号（AO）
*/
window.$$iPems.Point = {
    DI: 0,
    AI: 1,
    DO: 2,
    AO: 3
};

/*Point Status*/
window.$$iPems.PointStatus = {
    Normal: 0,
    Level1: 1,
    Level2: 2,
    Level3: 3,
    Level4: 4,
    Opevent: 5,
    Invalid: 6
};

/*Status Css Class*/
window.$$iPems.GetPointStatusCls = function (value) {
    switch (value) {
        case $$iPems.PointStatus.Normal:
            return 'point-status-normal';
        case $$iPems.PointStatus.Level1:
            return 'point-status-level1';
        case $$iPems.PointStatus.Level2:
            return 'point-status-level2';
        case $$iPems.PointStatus.Level3:
            return 'point-status-level3';
        case $$iPems.PointStatus.Level4:
            return 'point-status-level4';
        case $$iPems.PointStatus.Opevent:
            return 'point-status-opevent';
        case $$iPems.PointStatus.Invalid:
            return 'point-status-invalid';
        default:
            return '';
    }
};

/*Alarm Level*/
window.$$iPems.AlmLevel = {
    Level1: 1,
    Level2: 2,
    Level3: 3,
    Level4: 4
};

/*Alarm Css Class*/
window.$$iPems.GetAlmLevelCls = function (value) {
    switch(value) {
        case $$iPems.AlmLevel.Level1:
            return 'alm-level1';
        case $$iPems.AlmLevel.Level2:
            return 'alm-level2';
        case $$iPems.AlmLevel.Level3:
            return 'alm-level3';
        case $$iPems.AlmLevel.Level4:
            return 'alm-level4';
        default:
            return '';
    }
};

/*Split Node Keys*/
window.$$iPems.SplitKeys = function (key) {
    if(Ext.isEmpty(key)) return [];
    return key.split($$iPems.Separator);
};

/*global delimiter*/
window.$$iPems.Delimiter = ';';
window.$$iPems.Separator = '┆';

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

//global tasks
window.$$iPems.Tasks = {
    noticeTask: Ext.util.TaskManager.newTask({
        run: function () {
            Ext.Ajax.request({
                url: '/Home/GetNoticesCount',
                preventWindow: true,
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success) {
                        var count = data.data,
                            tips = Ext.get('noticeCount');

                        if (!Ext.isEmpty(tips)) {
                            tips.setHTML(count > 99 ? '99+' : count);
                            tips.setDisplayed(count > 0);

                            //restart
                            $$iPems.Tasks.noticeTask.fireOnStart = false;
                            $$iPems.Tasks.noticeTask.restart();
                        }
                    }
                }
            });
        },
        fireOnStart: true,
        interval: 15000,
        repeat: 1
    }),
    actAlmNoticeTask: Ext.util.TaskManager.newTask({
        run: function () {
            Ext.Ajax.request({
                url: '/Home/GetActAlmCount',
                preventWindow: true,
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success) {
                        var count = data.data,
                            tips = Ext.get('actAlmCount');

                        if (!Ext.isEmpty(tips)) {
                            tips.setHTML(count > 99 ? '99+' : count);
                            tips.setDisplayed(count > 0);

                            //restart
                            $$iPems.Tasks.actAlmNoticeTask.fireOnStart = false;
                            $$iPems.Tasks.actAlmNoticeTask.restart();
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
            interval: 5000,
            repeat: 1
        }),
        unconnectedTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 15000,
            repeat: 1
        }),
        offTask: Ext.util.TaskManager.newTask({
            run: Ext.emptyFn,
            fireOnStart: true,
            interval: 15000,
            repeat: 1
        })
    }
};