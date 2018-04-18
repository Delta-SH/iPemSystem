Ext.define('FileModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'name', type: 'string' },
        { name: 'size', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'path', type: 'string' },
        { name: 'date', type: 'string' }
    ],
    idProperty: 'index'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 50,
    model: 'FileModel',
    proxy: {
        type: 'ajax',
        url: '/Ftp/GetFiles',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        },
        listeners: {
            exception: function (proxy, response, operation) {
                Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        },
        simpleSortMode: true
    }
});

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

var operationContextMenu = Ext.create('Ext.menu.Menu', {
    plain: true,
    border: false,
    record: null,
    items: [{
        itemId: 'upload',
        glyph: 0xf060,
        text: '上传',
        handler: function () {
            upload();
        }
    }, {
        itemId: 'download',
        glyph: 0xf059,
        text: '下载',
        handler: function () {
            var record = operationContextMenu.record;
            if (!Ext.isEmpty(record)) {
                download(record.get('name'));
            }
        }
    }, '-', {
        itemId: 'rename',
        glyph: 0xf002,
        text: '重命名',
        handler: function () {
            var record = operationContextMenu.record;
            if (!Ext.isEmpty(record)) {
                rename(record.get('name'));
            }
        }
    }, '-', {
        itemId: 'delete',
        glyph: 0xf004,
        text: '删除',
        handler: function () {
            var record = operationContextMenu.record;
            if (!Ext.isEmpty(record)) {
                delfile(record.get('name'));
            }
        }
    }]
});

var currentGridPanel = Ext.create('Ext.grid.Panel', {
    title: Ext.isEmpty($$FTP_KEY) ? $$FTP_TITLE : Ext.String.format('{0} - {1}', $$FTP_TITLE, $$FTP_KEY),
    glyph: 0xf021,
    region: 'center',
    store: currentStore,
    disableSelection: false,
    loadMask: true,
    viewConfig: {
        trackOver: true,
        stripeRows: false,
        emptyText: '<h1 style="margin:20px">没有文件记录</h1>'
    },
    tools: [
        {
            type: 'close',
            tooltip: '关闭',
            hidden: (window.frames.length === parent.frames.length),
            handler: function (event, toolEl, owner, tool) {
                parent.closeWin();
            }
        }
    ],
    columns: [{
        text: '名称',
        dataIndex: 'name',
        width: 250,
        sortable: true,
        renderer: function (value) {
            return "<img src='" + $$iPems.FileIcon32(value) + "' class='cell-img'>" + value
        }
    }, {
        text: '大小',
        dataIndex: 'size',
        width: 150,
        sortable: true
    }, {
        text: '类型',
        dataIndex: 'type',
        width: 150,
        sortable: true
    }, {
        text: '日期',
        dataIndex: 'date',
        width: 150,
        sortable: true
    }],
    tbar: Ext.create('Ext.toolbar.Toolbar', {
        items: [{
            xtype: 'button',
            text: '连接',
            glyph: 0xf021,
            handler: function (el, e) {
                connect();
            }
        }, '-', {
            id: 'uploadButton',
            xtype: 'button',
            text: '上传',
            glyph: 0xf060,
            disabled: Ext.isEmpty($$FTP_KEY),
            handler: function (el, e) {
                upload();
            }
        }, '-', {
            id: 'refreshButton',
            xtype: 'button',
            text: '刷新',
            glyph: 0xf058,
            disabled: Ext.isEmpty($$FTP_KEY),
            handler: function (el, e) {
                query();
            }
        }]
    }),
    bbar: currentPagingToolbar,
    listeners: {
        itemcontextmenu: function (me, record, item, index, e) {
            e.stopEvent();
            operationContextMenu.record = record;
            operationContextMenu.showAt(e.getXY());
        }
    }
});

var uploadWin = Ext.create('Ext.window.Window', {
    title: '文件上传框',
    glyph: 0xf060,
    height: 180,
    width: 400,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    items: [{
        itemId: 'uploadForm',
        xtype: 'form',
        border: false,
        defaultType: 'textfield',
        fieldDefaults: {
            labelWidth: 60,
            labelAlign: 'left',
            margin: '15 15 15 15',
            anchor: '100%'
        },
        items: [
            {
                id: 'updateFile',
                xtype: 'filefield',
                allowBlank: false,
                emptyText: '请选择需要上传的文件...',
                fieldLabel: '文件名称',
                buttonText: '浏览...',
                listeners: {
                    change: function (me, value) {
                        var newValue = value.replace(/C:\\fakepath\\/g, '');
                        me.setRawValue(newValue);
                    }
                }
            },
            {
                xtype: 'iconlabel',
                text: '提示：文件大小限制100M',
                iconCls: 'x-icon-tips',
                style: {
                    marginLeft: '75px'
                }
            }
        ]
    }],
    buttonAlign: 'right',
    buttons: [
        { id: 'uploadResult', xtype: 'iconlabel', text: '' },
        { xtype: 'tbfill' },
        {
            xtype: 'button',
            text: '上传',
            handler: function (el, e) {
                var form = uploadWin.getComponent('uploadForm'),
                    baseForm = form.getForm(),
                    uploadResult = Ext.getCmp('uploadResult');

                uploadResult.setTextWithIcon('', '');
                if (baseForm.isValid()) {
                    Ext.Msg.confirm('确认对话框', '您确认要上传吗？', function (buttonId, text) {
                        if (buttonId === 'yes') {
                            uploadResult.setTextWithIcon('正在上传...', 'x-icon-loading');
                            baseForm.submit({
                                clientValidation: true,
                                submitEmptyText: false,
                                preventWindow: true,
                                url: '/Ftp/Upload',
                                params: { key: $$FTP_KEY },
                                success: function (form, action) {
                                    uploadResult.setTextWithIcon(action.result.message, 'x-icon-accept');
                                    query();
                                },
                                failure: function (form, action) {
                                    var message = '客户端未知错误';
                                    if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                        message = action.result.message;

                                    uploadResult.setTextWithIcon(message, 'x-icon-error');
                                }
                            });
                        }
                    });
                }
            }
        }, {
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                uploadWin.hide();
            }
        }
    ]
});

var renameWin = Ext.create('Ext.window.Window', {
    title: '文件重命名框',
    glyph: 0xf002,
    height: 180,
    width: 400,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    items: [{
        itemId: 'renameForm',
        xtype: 'form',
        border: false,
        defaultType: 'textfield',
        fieldDefaults: {
            labelWidth: 60,
            labelAlign: 'left',
            margin: '15 15 15 15',
            anchor: '100%'
        },
        items: [
            {
                itemId: 'oldname',
                name: 'oldname',
                fieldLabel: '文件名称',
                allowBlank: false,
                readOnly: true
            },
            {
                itemId: 'newname',
                name: 'newname',
                fieldLabel: '重命名为',
                allowBlank: false
            }
        ]
    }],
    buttonAlign: 'right',
    buttons: [
        { id: 'renameResult', xtype: 'iconlabel', text: '' },
        { xtype: 'tbfill' },
        {
            xtype: 'button',
            text: '确定',
            handler: function (el, e) {
                var form = renameWin.getComponent('renameForm'),
                    baseForm = form.getForm(),
                    renameResult = Ext.getCmp('renameResult');

                renameResult.setTextWithIcon('', '');
                if (baseForm.isValid()) {
                    Ext.Msg.confirm('确认对话框', '您确认要重命名吗？', function (buttonId, text) {
                        if (buttonId === 'yes') {
                            renameResult.setTextWithIcon('正在重命名...', 'x-icon-loading');
                            baseForm.submit({
                                clientValidation: true,
                                submitEmptyText: false,
                                preventWindow: true,
                                url: '/Ftp/Rename',
                                params: { key: $$FTP_KEY },
                                success: function (form, action) {
                                    renameResult.setTextWithIcon(action.result.message, 'x-icon-accept');
                                    refresh();
                                },
                                failure: function (form, action) {
                                    var message = '客户端未知错误';
                                    if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                        message = action.result.message;

                                    renameResult.setTextWithIcon(message, 'x-icon-error');
                                }
                            });
                        }
                    });
                }
            }
        }, {
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                renameWin.hide();
            }
        }
    ]
});

var connectWin = Ext.create('Ext.window.Window', {
    title: 'FTP连接认证',
    glyph: 0xf021,
    height: 300,
    width: 400,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    items: [{
        itemId: 'connectForm',
        xtype: 'form',
        border: false,
        defaultType: 'textfield',
        fieldDefaults: {
            labelWidth: 60,
            labelAlign: 'left',
            margin: '15 15 15 15',
            anchor: '100%'
        },
        items: [
            {
                itemId: 'ip',
                name: 'ip',
                value: '127.0.0.1',
                fieldLabel: 'FTP地址',
                allowBlank: false
            },
            {
                itemId: 'port',
                name: 'port',
                xtype: 'numberfield',
                fieldLabel: 'FTP端口',
                value: 21,
                maxValue: 65535,
                minValue: 1
            },
            {
                itemId: 'user',
                name: 'user',
                fieldLabel: '用户名称',
                allowBlank: false
            },
            {
                itemId: 'password',
                name: 'password',
                xtype: 'textfield',
                inputType: 'password',
                fieldLabel: '认证密码',
                allowBlank: false
            },
            {
                itemId: 'directory',
                name: 'directory',
                fieldLabel: '工作目录',
                value: '/',
                allowBlank: false
            }
        ]
    }],
    buttonAlign: 'right',
    buttons: [
        { id: 'connectResult', xtype: 'iconlabel', text: '' },
        { xtype: 'tbfill' },
        {
            xtype: 'button',
            text: '连接',
            handler: function (el, e) {
                var form = connectWin.getComponent('connectForm'),
                    baseForm = form.getForm(),
                    connectResult = Ext.getCmp('connectResult');

                connectResult.setTextWithIcon('', '');
                if (baseForm.isValid()) {
                    connectResult.setTextWithIcon('正在认证...', 'x-icon-loading');
                    baseForm.submit({
                        clientValidation: true,
                        submitEmptyText: false,
                        preventWindow: true,
                        url: '/Ftp/Login',
                        success: function (form, action) {
                            $$FTP_KEY = action.result.message;
                            connectWin.hide();
                            currentGridPanel.setTitle(Ext.String.format('FTP文件管理 - {0}', $$FTP_KEY));
                            Ext.getCmp('uploadButton').setDisabled(false);
                            Ext.getCmp('refreshButton').setDisabled(false);
                            refresh();
                        },
                        failure: function (form, action) {
                            var message = '客户端未知错误';
                            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                message = action.result.message;

                            connectResult.setTextWithIcon(message, 'x-icon-error');
                        }
                    });
                }
            }
        }, {
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                connectWin.hide();
            }
        }
    ]
});

var query = function () {
    if (Ext.isEmpty($$FTP_KEY)) return false;
    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.key = $$FTP_KEY;
    proxy.extraParams.name = "";
    proxy.extraParams.cache = false;
    me.loadPage(1);
}

var refresh = function () {
    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.key = $$FTP_KEY;
    proxy.extraParams.name = "";
    proxy.extraParams.cache = false;
    currentPagingToolbar.doRefresh();
}

var connect = function () {
    connectWin.getComponent('connectForm').getForm().reset();
    Ext.getCmp('connectResult').setTextWithIcon('', '');
    connectWin.show();
}

var upload = function () {
    uploadWin.getComponent('uploadForm').getForm().reset();
    Ext.getCmp('uploadResult').setTextWithIcon('', '');
    uploadWin.show();
}

var download = function (name) {
    if (Ext.isEmpty(name)) {
        Ext.Msg.show({ title: '系统警告', msg: '文件名称不能为空', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
        return false;
    }

    Ext.Msg.confirm('确认对话框', Ext.String.format('您确认要下载"{0}"吗？', name), function (buttonId, text) {
        if (buttonId === 'yes') {
            $$iPems.download({
                url: '/Ftp/Download',
                params: {
                    key: $$FTP_KEY,
                    name: name
                }
            });
        }
    });
}

var rename = function (name) {
    if (Ext.isEmpty(name)) {
        Ext.Msg.show({ title: '系统警告', msg: '文件名称不能为空', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
        return false;
    }

    var form = renameWin.getComponent('renameForm');
    form.getForm().reset();
    form.getComponent('oldname').setRawValue(name);
    Ext.getCmp('renameResult').setTextWithIcon('', '');
    renameWin.show();
}

var delfile = function (name) {
    if (Ext.isEmpty(name)) {
        Ext.Msg.show({ title: '系统警告', msg: '文件名称不能为空', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
        return false;
    }

    Ext.Msg.confirm('确认对话框', Ext.String.format('您确认要删除"{0}"吗？', name), function (buttonId, text) {
        if (buttonId === 'yes') {
            Ext.Ajax.request({
                url: '/Ftp/Delete',
                params: {
                    key: $$FTP_KEY,
                    name: name
                },
                mask: new Ext.LoadMask(currentGridPanel, { msg: '正在删除...' }),
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success){
                        refresh();
                    } else {
                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                }
            });
        }
    });
}

Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();
    Ext.create('Ext.container.Viewport', {
        id: 'main-viewport',
        layout: 'border',
        items: [currentGridPanel]
    });

    //加载FTP文件
    query();
});