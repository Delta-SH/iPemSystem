//#region Global
var currentNode = null;
Ext.apply(Ext.form.field.VTypes, {
    Formula: function (val, field) {
        var result = $$iPems.validateFormula(val, true);
        field.vtypeText = result;
        return result === $$iPems.formulaResults.Success;
    },
    FormulaText: '公式格式错误'
});
//#endregion

//#region Window

var signalWin = Ext.create('Ext.window.Window', {
    glyph: 0xf051,
    title: '信号选择器',
    height: 500,
    width: 700,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    target: null,
    layout: {
        type: 'hbox',
        align: 'stretch'
    },
    items: [
        {
            id:'signalLeftPanel',
            xtype: 'treepanel',
            width: 230,
            autoScroll: true,
            useArrows: false,
            rootVisible: false,
            margin: '5 0 5 5',
            root: { expanded: false },
            viewConfig: {
                loadMask: true
            },
            store: Ext.create('Ext.data.TreeStore', {
                autoLoad: false,
                proxy: {
                    type: 'ajax',
                    url: '/Maintenance/GetFormulaDevices',
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
                select: function (me, record, item, index) {
                    var signalCenterPanel = Ext.getCmp('signalCenterPanel'),
                        signalCenterStore = signalCenterPanel.getStore();

                    signalCenterStore.proxy.extraParams.parent = record.getId();
                    signalCenterStore.loadPage(1);
                }
            },
            tbar: [
                {
                    xtype: 'DeviceTypeMultiCombo',
                    emptyText: '设备筛选，默认全部',
                    fieldLabel: null,
                    flex: 1,
                    listeners: {
                        change: function (me, newValue, oldValue) {
                            var signalCenterPanel = Ext.getCmp('signalCenterPanel'),
                                signalCenterStore = signalCenterPanel.getStore(),
                                signalLeftPanel = Ext.getCmp('signalLeftPanel'),
                                signalLeftStore = signalLeftPanel.getStore();

                            signalCenterStore.proxy.extraParams.parent = null;
                            signalCenterStore.removeAll();

                            signalLeftStore.proxy.extraParams.devTypes = newValue;
                            signalLeftPanel.setRootNode({ id: 'root', expanded: true });
                        }
                    }
                }
            ]
        },
        {
            xtype: 'container',
            flex: 1,
            margin: 5,
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            items: [
                {
                    id:'signalCenterPanel',
                    xtype: 'grid',
                    flex: 1,
                    store: Ext.create('Ext.data.Store', {
                        autoLoad: false,
                        pageSize: 20,
                        fields: [
                            { name: 'Key', type: 'int' },
                            { name: 'Value', type: 'string' }
                        ],
                        proxy: {
                            type: 'ajax',
                            url: '/Maintenance/GetFormulaPoints',
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
                    }),
                    columns: [
                        { text: '序号', dataIndex: 'Key', width: 60 },
                        { text: '信号标识', dataIndex: 'Value', width: 350 }
                    ],
                    listeners: {
                        selectionchange: function (me, models) {
                            var signalField = Ext.getCmp('signalField'),
                                values = [];

                            Ext.Array.each(models, function (item, index, allItems) {
                                values.push(item.get('Value'));
                            });

                            signalField.setValue(values.join(';'));
                        }
                    }
                }, {
                    id: 'signalField',
                    xtype: 'textareafield',
                    height: 100,
                    fieldLabel: '已选信号标识',
                    labelAlign: 'top',
                    readOnly: true,
                    allowBlank: false
                }
            ]
        }
    ],
    buttons: [
      { xtype: 'tbfill' },
      {
          xtype: 'button',
          text: '确定',
          handler: function (el, e) {
              var signalField = Ext.getCmp('signalField');
              if (!signalField.isValid()) return;

              if (!Ext.isEmpty(signalWin.target)) {
                  var value = signalField.getRawValue();
                  signalWin.target.setRawValue(value);
              }

              signalWin.close();
          }
      },
      {
          xtype: 'button',
          text: '关闭',
          handler: function (el, e) {
              signalWin.close();
          }
      }
    ]
});

var formulaWin = Ext.create('Ext.window.Window', {
    glyph: 0xf051,
    title: '公式编辑器',
    height: 500,
    width: 700,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    target: null,
    layout: {
        type: 'hbox',
        align: 'stretch'
    },
    items: [
        {
            id: 'formulaLeftPanel',
            xtype: 'treepanel',
            width: 230,
            autoScroll: true,
            useArrows: false,
            rootVisible: false,
            margin: '5 0 5 5',
            root: { expanded: false },
            viewConfig: {
                loadMask: true
            },
            store: Ext.create('Ext.data.TreeStore', {
                autoLoad: false,
                proxy: {
                    type: 'ajax',
                    url: '/Maintenance/GetFormulaDevices',
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
                select: function (me, record, item, index) {
                    var formulaCenterPanel = Ext.getCmp('formulaCenterPanel'),
                        formulaCenterStore = formulaCenterPanel.getStore();

                    formulaCenterStore.proxy.extraParams.parent = record.getId();
                    formulaCenterStore.loadPage(1);
                }
            },
            tbar: [
                {
                    xtype: 'DeviceTypeMultiCombo',
                    emptyText: '设备筛选，默认全部',
                    fieldLabel: null,
                    flex: 1,
                    listeners: {
                        change: function (me, newValue, oldValue) {
                            var formulaCenterPanel = Ext.getCmp('formulaCenterPanel'),
                                formulaCenterStore = formulaCenterPanel.getStore(),
                                formulaLeftPanel = Ext.getCmp('formulaLeftPanel'),
                                formulaLeftStore = formulaLeftPanel.getStore();

                            formulaCenterStore.proxy.extraParams.parent = null;
                            formulaCenterStore.removeAll();

                            formulaLeftStore.proxy.extraParams.devTypes = newValue;
                            formulaLeftPanel.setRootNode({ id: 'root', expanded: true });
                        }
                    }
                }
            ]
        },
        {
            xtype: 'container',
            flex: 1,
            margin: 5,
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            items: [
                {
                    id: 'formulaCenterPanel',
                    xtype: 'grid',
                    flex: 1,
                    store: Ext.create('Ext.data.Store', {
                        autoLoad: false,
                        pageSize: 20,
                        fields: [
                            { name: 'Key', type: 'int' },
                            { name: 'Value', type: 'string' }
                        ],
                        proxy: {
                            type: 'ajax',
                            url: '/Maintenance/GetFormulaPoints',
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
                    }),
                    columns: [
                        { text: '序号', dataIndex: 'Key', width: 60 },
                        { text: '变量（双击变量，将其加入公式）', dataIndex: 'Value', width: 350 }
                    ],
                    listeners: {
                        itemdblclick: function (me, record, item, index, e) {
                            $$iPems.insertAtCursor(Ext.getCmp('formulaField'), record.get('Value'));
                        }
                    }
                }, {
                    id: 'formulaField',
                    xtype: 'textareafield',
                    height: 100,
                    fieldLabel: '能耗公式',
                    labelAlign: 'top',
                    vtype: 'Formula'
                }, {
                    xtype: 'toolbar',
                    margin: '5 0 0 0',
                    items: [
                        {
                            text: '+', handler: function () {
                                $$iPems.insertAtCursor(Ext.getCmp('formulaField'), ' + ');
                            }
                        },
                        '-',
                        {
                            text: '-', handler: function () {
                                $$iPems.insertAtCursor(Ext.getCmp('formulaField'), ' - ');
                            }
                        },
                        '-',
                        {
                            text: '×', handler: function () {
                                $$iPems.insertAtCursor(Ext.getCmp('formulaField'), ' * ');
                            }
                        },
                        '-',
                        {
                            text: '÷', handler: function () {
                                $$iPems.insertAtCursor(Ext.getCmp('formulaField'), ' / ');
                            }
                        },
                        '-',
                        {
                            text: '(', handler: function () {
                                $$iPems.insertAtCursor(Ext.getCmp('formulaField'), '( ');
                            }
                        },
                        '-',
                        {
                            text: ')', handler: function () {
                                $$iPems.insertAtCursor(Ext.getCmp('formulaField'), ' )');
                            }
                        },
                        '-',
                        {
                            text: '清空', handler: function () {
                                var formulaField = Ext.getCmp('formulaField');

                                formulaField.reset();
                                formulaField.focus();
                            }
                        },
                        '-',
                        {
                            text: '删除', handler: function () {
                                $$iPems.deleteAtCursor(Ext.getCmp('formulaField'));
                            }
                        }
                    ]
                }
            ]
        }
    ],
    buttons: [
      { xtype: 'tbfill' },
      {
          xtype: 'button',
          text: '确定',
          handler: function (el, e) {
              var formulaField = Ext.getCmp('formulaField');
              if (!formulaField.isValid()) return;

              if (!Ext.isEmpty(formulaWin.target)) {
                  var value = formulaField.getRawValue();
                  formulaWin.target.setRawValue(value);
              }

              formulaWin.close();
          }
      },
      {
          xtype: 'button',
          text: '关闭',
          handler: function (el, e) {
              formulaWin.close();
          }
      }
    ]
});

//#endregion

//#region ContextMenu
var formulaContextOnStation = Ext.create('Ext.menu.Menu', {
    plain: true,
    border: false,
    source: null,
    target: null,
    items: [{
        itemId: 'copy',
        glyph: 0xf053,
        text: '复制公式',
        handler: function () {
            var me = formulaContextOnStation;
            if (!Ext.isEmpty(me.target))
                me.source = me.target;
        }
    }, '-', {
        itemId: 'paste',
        glyph: 0xf054,
        text: '粘帖公式',
        handler: function () {
            var me = formulaContextOnStation;
            if (Ext.isEmpty(me.source)) return false;
            if (Ext.isEmpty(me.target)) return false;

            var confirm = Ext.String.format('您确定要粘贴公式吗？<br/>{0} -> {1}', me.source.data.text, me.target.data.text);
            Ext.Msg.confirm('确认对话框', confirm, function (buttonId, text) {
                if (buttonId === 'yes') {
                    var id = me.source.getId(),
                    form = centerPanel.getForm();

                    form.reset();
                    form.load({
                        url: '/Maintenance/GetFormula',
                        params: { node: id },
                        waitTitle: '系统提示',
                        waitMsg: '正在处理...',
                        success: function (form, action) { }
                    });
                }
            });
        }
    }],
    listeners: {
        show: function (me) {
            me.getComponent('paste').setDisabled(Ext.isEmpty(me.source));
        }
    }
});

var formulaContextOnRoom = Ext.create('Ext.menu.Menu', {
    plain: true,
    border: false,
    source: null,
    target: null,
    items: [{
        itemId: 'copy',
        glyph: 0xf053,
        text: '复制公式',
        handler: function () {
            var me = formulaContextOnRoom;
            if (!Ext.isEmpty(me.target))
                me.source = me.target;
        }
    }, '-', {
        itemId: 'paste',
        glyph: 0xf054,
        text: '粘帖公式',
        handler: function () {
            var me = formulaContextOnRoom;
            if (Ext.isEmpty(me.source)) return false;
            if (Ext.isEmpty(me.target)) return false;

            var confirm = Ext.String.format('您确定要粘贴公式吗？<br/>{0} -> {1}', me.source.data.text, me.target.data.text);
            Ext.Msg.confirm('确认对话框', confirm, function (buttonId, text) {
                if (buttonId === 'yes') {
                    var id = me.source.getId(),
                    form = centerPanel.getForm();

                    form.reset();
                    form.load({
                        url: '/Maintenance/GetFormula',
                        params: { node: id },
                        waitTitle: '系统提示',
                        waitMsg: '正在处理...',
                        success: function (form, action) { }
                    });
                }
            });
        }
    }],
    listeners: {
        show: function (me) {
            me.getComponent('paste').setDisabled(Ext.isEmpty(me.source));
        }
    }
});

var formulaContextOnDev = Ext.create('Ext.menu.Menu', {
    plain: true,
    border: false,
    source: null,
    target: null,
    items: [{
        itemId: 'copy',
        glyph: 0xf053,
        text: '复制公式',
        handler: function () {
            var me = formulaContextOnDev;
            if (!Ext.isEmpty(me.target))
                me.source = me.target;
        }
    }, '-', {
        itemId: 'paste',
        glyph: 0xf054,
        text: '粘帖公式',
        handler: function () {
            var me = formulaContextOnDev;
            if (Ext.isEmpty(me.source)) return false;
            if (Ext.isEmpty(me.target)) return false;

            var confirm = Ext.String.format('您确定要粘贴公式吗？<br/>{0} -> {1}', me.source.data.text, me.target.data.text);
            Ext.Msg.confirm('确认对话框', confirm, function (buttonId, text) {
                if (buttonId === 'yes') {
                    var id = me.source.getId(),
                    form = centerPanel.getForm();

                    form.reset();
                    form.load({
                        url: '/Maintenance/GetFormula',
                        params: { node: id },
                        waitTitle: '系统提示',
                        waitMsg: '正在处理...',
                        success: function (form, action) { }
                    });
                }
            });
        }
    }],
    listeners: {
        show: function (me) {
            me.getComponent('paste').setDisabled(Ext.isEmpty(me.source));
        }
    }
});
//#endregion

//#region UI

var westPanel = Ext.create('Ext.tree.Panel', {
    glyph: 0xf011,
    title: '系统层级',
    region: 'west',
    width: 250,
    autoScroll: true,
    collapsible: true,
    collapsed: false,
    useArrows: false,
    rootVisible: false,
    split: true,
    root: {
        id: 'root',
        text: '全部',
        icon: $$iPems.icons.Home,
        expanded: true
    },
    viewConfig: {
        loadMask: true
    },
    store: Ext.create('Ext.data.TreeStore', {
        autoLoad: false,
        proxy: {
            type: 'ajax',
            url: '/Component/GetDevices',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                multiselect: false,
                leafselect: false
            }
        }
    }),
    listeners: {
        select: function (me, record, index) {
            currentNode = record;
            if (changeNode(record) === true) {
                var id = record.getId(),
                    form = centerPanel.getForm(),
                    save = Ext.getCmp('save'),
                    result = Ext.getCmp('saveResult');

                result.setTextWithIcon('', '');
                form.reset();
                form.load({
                    url: '/Maintenance/GetFormula',
                    params: { node: id },
                    waitTitle: '系统提示',
                    waitMsg: '正在处理...',
                    success: function (form, action) { }
                });
            }
        },
        itemcontextmenu: function (me, record, item, index, e) {
            e.stopEvent();
            var id = record.getId(),
                keys = $$iPems.SplitKeys(id);

            if (keys.length !== 2) return false;

            var type = parseInt(keys[0]);
            if (type === $$iPems.SSH.Station) {
                formulaContextOnStation.target = record;
                formulaContextOnStation.showAt(e.getXY());
            } else if (type === $$iPems.SSH.Room) {
                formulaContextOnRoom.target = record;
                formulaContextOnRoom.showAt(e.getXY());
            } else if (type === $$iPems.SSH.Device) {
                formulaContextOnDev.target = record;
                formulaContextOnDev.showAt(e.getXY());
            }
        }
    },
    tbar: [
            {
                id: 'formula-target-search-field',
                xtype: 'textfield',
                emptyText: '请输入筛选条件...',
                flex: 1,
                listeners: {
                    change: function (me, newValue, oldValue, eOpts) {
                        delete me._filterData;
                        delete me._filterIndex;
                    }
                }
            },
            {
                xtype: 'button',
                glyph: 0xf005,
                handler: function () {
                    var tree = Ext.getCmp('formula-target'),
                        search = Ext.getCmp('formula-target-search-field'),
                        text = search.getRawValue(),
                        separator = '/',
                        root = tree.getRootNode();

                    if (Ext.isEmpty(text, false)) {
                        return;
                    }

                    if (text.length < 2) {
                        return;
                    }

                    if (search._filterData != null
                        && search._filterIndex != null) {
                        var index = search._filterIndex + 1;
                        var paths = search._filterData;
                        if (index >= paths.length) {
                            index = 0;
                            Ext.Msg.show({ title: '系统提示', msg: '搜索完毕', buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                        }

                        var nodes = Ext.Array.from(paths[index]);
                        var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                        tree.selectPath(path);
                        search._filterIndex = index;
                    } else {
                        Ext.Ajax.request({
                            url: '/Component/FilterRoomPath',
                            params: { text: text },
                            mask: new Ext.LoadMask({ target: tree, msg: '正在处理...' }),
                            success: function (response, options) {
                                var data = Ext.decode(response.responseText, true);
                                if (data.success) {
                                    var len = data.data.length;
                                    if (len > 0) {
                                        var nodes = Ext.Array.from(data.data[0]);
                                        var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                                        tree.selectPath(path);

                                        search._filterData = data.data;
                                        search._filterIndex = 0;
                                    } else {
                                        Ext.Msg.show({ title: '系统提示', msg: Ext.String.format('未找到指定内容:<br/>{0}', text), buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                    }
                                } else {
                                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                }
                            }
                        });
                    }
                }
            }
    ]
});

var centerPanel = Ext.create('Ext.form.Panel', {
    region: 'center',
    glyph: 0xf052,
    title: '能耗公式',
    disabled: false,
    autoScroll: true,
    waitMsgTarget: true,
    border: true,
    fieldDefaults: {
        labelWidth: 60,
        labelAlign: 'left'
    },
    defaults: {
        anchor: '95%'
    },
    bodyPadding: '0 10 0 10',
    items: [
        {
            itemId: 'ktnh',
            xtype: 'fieldset',
            title: '空调',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'ktFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'ktCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'ktRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'zmnh',
            xtype: 'fieldset',
            title: '照明',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'zmFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'zmCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'zmRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'bgnh',
            xtype: 'fieldset',
            title: '办公',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'bgFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'bgCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'bgRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'dynh',
            xtype: 'fieldset',
            title: '开关电源',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'dyFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'dyCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'dyRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'upsnh',
            xtype: 'fieldset',
            title: 'UPS',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'upsFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'upsCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'upsRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'itnh',
            xtype: 'fieldset',
            title: 'IT设备',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'itFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'itCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'itRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'qtnh',
            xtype: 'fieldset',
            title: '其他',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'qtFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'qtCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'qtRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'znh',
            xtype: 'fieldset',
            title: '总能耗',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'ttFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'ttCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'ttRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'sdtd',
            xtype: 'fieldset',
            title: '停电',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'tdFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '停电标识',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showSignalWin(currentNode, this, false, false, true);
                    }
                },
                {
                    name: 'tdRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'wdsd',
            xtype: 'fieldset',
            title: '环境',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'wdFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '温度标识',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showSignalWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'sdFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '湿度标识',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    onTriggerClick: function () {
                        showSignalWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'wdRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'yjfd',
            xtype: 'fieldset',
            title: '发电机组(仅针对发电机组设备，其他设备无需设置)',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'fdFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '发电标识',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    editable: false,
                    onTriggerClick: function () {
                        showSignalWin(currentNode, this, false, false, true);
                    }
                },
                {
                    name: 'yjFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '电量公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    editable: false,
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'yjCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '运算类型',
                    margin: '10 10 0 10'
                },
                {
                    name: 'yjRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        },
        {
            itemId: 'byq',
            xtype: 'fieldset',
            title: '变压器(仅针对变压器设备，其他设备无需设置)',
            margin: '10 0 10 0',
            layout: 'anchor',
            disabled: true,
            hidden: true,
            defaults: {
                anchor: '100%'
            },
            items: [
                {
                    name: 'byFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '能耗公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    editable: false,
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'byCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '能耗运算',
                    margin: '10 10 0 10'
                },
                {
                    name: 'xsFormulas',
                    xtype: 'triggerfield',
                    fieldLabel: '线损公式',
                    triggerCls: 'x-fx-trigger',
                    vtype: 'Formula',
                    margin: '10 10 0 10',
                    editable: false,
                    onTriggerClick: function () {
                        showFormulaWin(currentNode, this, true, false, false);
                    }
                },
                {
                    name: 'xsCompute',
                    xtype: 'ComputeCombo',
                    fieldLabel: '线损运算',
                    margin: '10 10 0 10'
                },
                {
                    name: 'byRemarks',
                    xtype: 'textfield',
                    fieldLabel: '备注',
                    margin: '10 10 10 10'
                }
            ]
        }
    ],
    buttonAlign: 'left',
    buttons: [
        {
            id: 'save',
            xtype: 'button',
            text: '保存公式',
            disabled: true,
            hidden: true,
            cls: 'custom-button custom-success',
            handler: function () {
                var formulaForm = centerPanel.getForm(),
                    saveResult = Ext.getCmp('saveResult');

                saveResult.setTextWithIcon('', '');
                if (formulaForm.isValid()) {
                    saveResult.setTextWithIcon('正在处理...', 'x-icon-loading');
                    formulaForm.submit({
                        submitEmptyText: false,
                        clientValidation: true,
                        preventWindow: true,
                        url: '/Maintenance/SaveFormula',
                        params: { node: currentNode.getId() },
                        success: function (form, action) {
                            saveResult.setTextWithIcon(action.result.message, 'x-icon-accept');
                        },
                        failure: function (form, action) {
                            var message = '客户端未知错误';
                            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                message = action.result.message;

                            saveResult.setTextWithIcon(message, 'x-icon-error');
                        }
                    });
                } else {
                    saveResult.setTextWithIcon('表单填写错误', 'x-icon-error');
                }
            }
        },
        { id: 'saveResult', xtype: 'iconlabel', text: '' }
    ]
});

var viewport = Ext.create('Ext.panel.Panel', {
    region: 'center',
    layout: 'border',
    border: false,
    items: [westPanel, centerPanel]
});

//#endregion

//#region Methods

var changeNode = function (node) {
    var id = node.getId(),
        keys = $$iPems.SplitKeys(id),
        ktnh = centerPanel.getComponent('ktnh'),
        zmnh = centerPanel.getComponent('zmnh'),
        bgnh = centerPanel.getComponent('bgnh'),
        dynh = centerPanel.getComponent('dynh'),
        upsnh = centerPanel.getComponent('upsnh'),
        itnh = centerPanel.getComponent('itnh'),
        qtnh = centerPanel.getComponent('qtnh'),
        znh = centerPanel.getComponent('znh'),
        sdtd = centerPanel.getComponent('sdtd'),
        wdsd = centerPanel.getComponent('wdsd'),
        yjfd = centerPanel.getComponent('yjfd'),
        byq = centerPanel.getComponent('byq'),
        save = Ext.getCmp('save');

    if (keys.length !== 2) return false;

    var type = parseInt(keys[0]);
    if (type === $$iPems.SSH.Station) {
        ktnh.setDisabled(false); ktnh.show();
        zmnh.setDisabled(false); zmnh.show();
        bgnh.setDisabled(false); bgnh.show();
        dynh.setDisabled(false); dynh.show();
        upsnh.setDisabled(false); upsnh.show();
        itnh.setDisabled(false); itnh.show();
        qtnh.setDisabled(false); qtnh.show();
        znh.setDisabled(false); znh.show();
        sdtd.setDisabled(false); sdtd.show();
        wdsd.setDisabled(false); wdsd.show();
        yjfd.setDisabled(true); yjfd.hide();
        byq.setDisabled(true); byq.hide();
        save.setDisabled(false); save.show();
        return true;
    } else if (type === $$iPems.SSH.Room) {
        ktnh.setDisabled(false); ktnh.show();
        zmnh.setDisabled(false); zmnh.show();
        bgnh.setDisabled(false); bgnh.show();
        dynh.setDisabled(false); dynh.show();
        upsnh.setDisabled(false); upsnh.show();
        itnh.setDisabled(false); itnh.show();
        qtnh.setDisabled(false); qtnh.show();
        znh.setDisabled(false); znh.show();
        sdtd.setDisabled(false); sdtd.show();
        wdsd.setDisabled(false); wdsd.show();
        yjfd.setDisabled(true); yjfd.hide();
        byq.setDisabled(true); byq.hide();
        save.setDisabled(false); save.show();
        return true;
    } else if (type === $$iPems.SSH.Device) {
        ktnh.setDisabled(true); ktnh.hide();
        zmnh.setDisabled(true); zmnh.hide();
        bgnh.setDisabled(true); bgnh.hide();
        dynh.setDisabled(true); dynh.hide();
        upsnh.setDisabled(true); upsnh.hide();
        itnh.setDisabled(true); itnh.hide();
        qtnh.setDisabled(true); qtnh.hide();
        znh.setDisabled(true); znh.hide();
        sdtd.setDisabled(true); sdtd.hide();
        wdsd.setDisabled(true); wdsd.hide();
        yjfd.setDisabled(false); yjfd.show();
        byq.setDisabled(false); byq.show();
        save.setDisabled(false); save.show();
        return true;
    } else {
        ktnh.setDisabled(true); ktnh.hide();
        zmnh.setDisabled(true); zmnh.hide();
        bgnh.setDisabled(true); bgnh.hide();
        dynh.setDisabled(true); dynh.hide();
        upsnh.setDisabled(true); upsnh.hide();
        itnh.setDisabled(true); itnh.hide();
        qtnh.setDisabled(true); qtnh.hide();
        znh.setDisabled(true); znh.hide();
        sdtd.setDisabled(true); sdtd.hide();
        wdsd.setDisabled(true); wdsd.hide();
        yjfd.setDisabled(true); yjfd.hide();
        byq.setDisabled(true); byq.hide();
        save.setDisabled(true); save.hide();
        return false;
    }

    return false;
};

var showSignalWin = function (node, target, ai, di, al) {
    var signalCenterPanel = Ext.getCmp('signalCenterPanel'),
        signalCenterStore = signalCenterPanel.getStore(),
        signalLeftPanel = Ext.getCmp('signalLeftPanel'),
        signalLeftStore = signalLeftPanel.getStore(),
        signalField = Ext.getCmp('signalField'),
        originValue = target.getValue();

    signalCenterStore.proxy.extraParams.parent = null;
    signalCenterStore.proxy.extraParams.ai = ai;
    signalCenterStore.proxy.extraParams.di = di;
    signalCenterStore.proxy.extraParams.al = al;
    signalCenterStore.removeAll();

    signalLeftStore.proxy.extraParams.target = node.getId();
    signalLeftPanel.setRootNode({ id: 'root', expanded: true });

    signalField.setValue(originValue);
    signalWin.target = target;
    signalWin.show();
};

var showFormulaWin = function (node, target, ai, di, al) {
    var formulaCenterPanel = Ext.getCmp('formulaCenterPanel'),
        formulaCenterStore = formulaCenterPanel.getStore(),
        formulaLeftPanel = Ext.getCmp('formulaLeftPanel'),
        formulaLeftStore = formulaLeftPanel.getStore(),
        formulaField = Ext.getCmp('formulaField'),
        originValue = target.getValue();

    formulaCenterStore.proxy.extraParams.parent = null;
    formulaCenterStore.proxy.extraParams.ai = ai;
    formulaCenterStore.proxy.extraParams.di = di;
    formulaCenterStore.proxy.extraParams.al = al;
    formulaCenterStore.removeAll();

    formulaLeftStore.proxy.extraParams.target = node.getId();
    formulaLeftPanel.setRootNode({ id: 'root', expanded: true });

    formulaField.setValue(originValue);
    formulaWin.target = target;
    formulaWin.show();
};

//#endregion

//#region onReady

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(viewport);
    }
});

//#endregion