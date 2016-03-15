Ext.require(['*']);
Ext.setGlyphFontFamily('ipems-icon-font');

/*ajax timeout*/
Ext.Ajax.timeout = 300000;
Ext.override(Ext.form.Basic, { timeout: Ext.Ajax.timeout / 1000 });
Ext.override(Ext.data.proxy.Server, { timeout: Ext.Ajax.timeout });
Ext.override(Ext.data.Connection, { timeout: Ext.Ajax.timeout });

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

    if (!Ext.isEmpty(options.preventWindow) && options.preventWindow)
        return;

    $$iPems.ShowFailure(response, response.responseText);
});

Ext.direct.Manager.on('exception', function (event) {
    if (!Ext.isEmpty(event) && !Ext.isEmpty(event.message))
        Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: event.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
});

Ext.override(Ext.data.proxy.Server, {
    constructor: function (config) {
        this.callOverridden([config]);
        this.addListener('exception', function (proxy, response, operation) {
            var message = '';
            var data = Ext.decode(response.responseText, true);
            if (!Ext.isEmpty(data) && !Ext.isEmpty(data.message))
                message = data.message;

            $$iPems.ShowFailure(response, message);
        });
    }
});

Ext.override(Ext.form.Basic, {
    afterAction: function (action, success) {
        this.callParent(arguments);

        if (!Ext.isEmpty(action.preventWindow) && action.preventWindow)
            return;

        if (!success) {
            var message = '';
            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                message = action.result.message;

            $$iPems.ShowFailure(action.response, message);
        }
    }
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

    win = new Ext.window.Window({
        modal: true,
        width: width,
        height: height,
        title: $$iPems.lang.SysErrorTitle,
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

/*ajax action*/
window.$$iPems.Action = {
    Add: 0,
    Edit: 1,
    Delete: 2
};

/*organization*/
window.$$iPems.Organization = {
    Area: 0,
    Sta: 1,
    Room: 2,
    FSU: 3,
    Dev: 4,
    Node: 5
};

/*Alarm Level*/
window.$$iPems.AlmLevel = {
    Level1: 1,
    Level2: 2,
    Level3: 3,
    Level4: 4
};

/*Alarm Level*/
window.$$iPems.GetAlmLevelCls = function (value) {
    switch (value) {
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

/*global delimiter*/
window.$$iPems.Delimiter = ';';

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

/*clone paging toolbar*/
window.$$iPems.clonePagingToolbar = function (store) {
    return new Ext.PagingToolbar({
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
                        { id: 200, text: '200', comment: '200' }
                    ]
                }),
                xtype: 'combobox',
                fieldLabel: $$iPems.lang.DisplayRows,
                displayField: 'text',
                valueField: 'id',
                typeAhead: true,
                queryMode: 'local',
                triggerAction: 'all',
                emptyText: $$iPems.lang.PlsSelectEmptyText,
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

//global tasks
window.$$iPems.Tasks = {
    noticeTask: Ext.util.TaskManager.newTask({
        run: function () {
            Ext.Ajax.request({
                url: '../Home/GetNoticesCount',
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
        interval: 10000,
        repeat: 1
    }),
    actAlmNoticeTask: Ext.util.TaskManager.newTask({
        run: function () {
            Ext.Ajax.request({
                url: '../Home/GetActAlmCount',
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
        interval: 10000,
        repeat: 1
    }), 
    actAlmTask: Ext.util.TaskManager.newTask({
        run: Ext.emptyFn,
        fireOnStart: true,
        interval: 15000,
        repeat: 1
    })
};

/*master page*/
Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();

    //start tasks
    $$iPems.Tasks.noticeTask.start();
    $$iPems.Tasks.actAlmNoticeTask.start();

    /*home page*/
    var _titlebar = Ext.create('Ext.panel.Panel', {
        border: false,
        contentEl: 'title-bar'
    });

    var _pagebody = Ext.create('Ext.panel.Panel', {
        border: false,
        contentEl: 'page-body-content'
    });

    var _north = Ext.create('Ext.panel.Panel', {
        region: 'north',
        border: false,
        items: [_titlebar, _pagebody]
    });

    var viewport = Ext.create('Ext.container.Viewport', {
        id: 'main-viewport',
        layout: 'border',
        items: [
                {
                    id: 'top-nav-panel-fw',
                    region: 'north',
                    contentEl: 'top-nav-panel',
                    height: 51,
                    border: false
                },
                {
                    id: 'left-nav-panel-fw',
                    region: 'west',
                    title: $$iPems.lang.Site.TreeTitle,
                    xtype: 'treepanel',
                    glyph: 0xf011,
                    width: 220,
                    collapsible: true,
                    collapsed: false,
                    autoScroll: true,
                    useArrows: false,
                    rootVisible: true,
                    hidden: !$$iPems.menuVisible,
                    margins: '5 0 5 5',
                    contentEl: 'left-nav-panel',
                    bodyCls: 'left-nav',
                    store: new Ext.data.TreeStore({
                        autoLoad: false,
                        root: {
                            id: -10078,
                            text: $$iPems.lang.Site.TreeRoot,
                            href: '/Home',
                            icon: $$iPems.icons.Home,
                            root: true
                        },
                        proxy: {
                            type: 'ajax',
                            url: '../Home/GetNavMenus',
                            reader: {
                                type: 'json',
                                successProperty: 'success',
                                messageProperty: 'message',
                                totalProperty: 'total',
                                root: 'data'
                            }
                        }
                    }),
                    listeners: {
                        render: function (el) {
                            el.getStore().load();
                        },
                        load: function (el, node, records, successful) {
                            if (successful) {
                                var me = this,
                                    record = el.getNodeById($$iPems.menuId);

                                if (!Ext.isEmpty(record)) {
                                    me.selectPath(record.getPath());
                                } else {
                                    me.getRootNode().expand(false);
                                }
                            }
                        }
                    }
                },
                {
                    id: 'center-content-panel-fw',
                    region: 'center',
                    layout: 'border',
                    contentEl: 'center-content-panel',
                    border: false,
                    items: [_north],
                    autoScroll: true,
                    padding: 5
                }
        ]
    });

    /*title tip*/
    var help = Ext.get('help-btn');
    if (!Ext.isEmpty(help)) {
        help.on('click', function (el, e) {
            var arrow = Ext.get('tip-arrow');
            if (Ext.isEmpty(arrow)) {
                return false;
            }

            var tip = Ext.get('help-tip');
            if (Ext.isEmpty(tip)) {
                return false;
            }

            tip.setDisplayed(!tip.isVisible());
            arrow.anchorTo(help, 'tc-bc', [-2, 1]);
            viewport.doLayout();
        });
    }

    /*show page content*/
    Ext.getBody().setDisplayed(true);
});