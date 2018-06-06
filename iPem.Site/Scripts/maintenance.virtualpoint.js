//#region Global
var currentNode = null;
Ext.apply(Ext.form.field.VTypes, {
    Formula: function (val, field) {
        var result = $$iPems.validateFormula(val, true);
        field.vtypeText = result;
        return result === $$iPems.formulaResults.Success;
    },
    FormulaText: '公式格式错误'
}, {
    VsId: function (v) {
        return !/([\+\-\*\/\(\)@]|[>]{2})/.test(v);
    },
    VsIdText: '信号编号禁止出现“@”、“>>”、“+”、“-”、“*”、“/”、“(”、“)”等符号'
});
//#endregion

//#region Model

Ext.define('VPintModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'dev', type: 'string' },
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'type', type: 'string' },
        { name: 'formula', type: 'string' },
        { name: 'unit', type: 'string' },
        { name: 'saved', type: 'int' },
        { name: 'stats', type: 'int' },
        { name: 'categoryName', type: 'string' },
        { name: 'category', type: 'int' },
        { name: 'remark', type: 'string' }
    ],
    idProperty: 'index'
});

//#endregion

//#region Store

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'VPintModel',
    proxy: {
        type: 'ajax',
        url: '/Maintenance/GetVSignals',
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
        extraParams: {
            node: 'root',
            name: ''
        },
        simpleSortMode: true
    }
});

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

//#endregion

//#region Window

var saveWnd = Ext.create('Ext.window.Window', {
    title: '新增信号',
    height: 360,
    width: 600,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    opaction: $$iPems.Action.Add,
    items: [{
        xtype: 'form',
        id: 'saveForm',
        border: false,
        fieldDefaults: {
            labelWidth: 60,
            labelAlign: 'left',
            anchor: '100%'
        },
        items: [
            {
                xtype: 'container',
                layout: 'hbox',
                items: [
                    {
                        xtype: 'container',
                        flex: 1,
                        layout: 'anchor',
                        defaults: {
                            margin: '15 15 15 15'
                        },
                        items: [
                            {
                                id: 'id',
                                name: 'id',
                                xtype: 'textfield',
                                fieldLabel: '信号编码',
                                allowBlank: false,
                                vtype: 'VsId',
                                readOnly: false
                            },
                            {
                                name: 'typevalue',
                                xtype: 'PointTypeCombo',
                                fieldLabel: '信号类型',
                                _al: false,
                                _ao: false,
                                _do: false,
                                _di: false,
                                allowBlank: false
                            },
                            {
                                name: 'saved',
                                xtype: 'numberfield',
                                fieldLabel: '存储周期',
                                value: 30,
                                maxValue: 86400,
                                minValue: 30
                            }
                        ]
                    },
                    {
                        xtype: 'container',
                        flex: 1,
                        layout: 'anchor',
                        defaults:{
                            margin: '15 15 15 15'
                        },
                        items: [
                            {
                                id: 'name',
                                name: 'name',
                                xtype: 'textfield',
                                fieldLabel: '信号名称',
                                allowBlank: false
                            },
                            {
                                id: 'unit',
                                name: 'unit',
                                xtype: 'textfield',
                                fieldLabel: '单位/状态',
                                allowBlank: true
                            },
                            {
                                name: 'stats',
                                xtype: 'numberfield',
                                fieldLabel: '统计周期',
                                value: 0,
                                maxValue: 86400,
                                minValue: 0
                            }
                        ]
                    }
                ]
            },
            {
                name: 'formula',
                xtype: 'triggerfield',
                fieldLabel: '计算公式',
                triggerCls: 'x-fx-trigger',
                vtype: 'Formula',
                allowBlank: false,
                margin: '0 15 15 15',
                onTriggerClick: function () {
                    showFormulaWin(currentNode, this, true, false, false);
                }
            },
            {
                id: 'category',
                name: 'category',
                xtype: 'VsCategoryCombo',
                margin: '15 15 15 15'
            },
            {
                name: 'remark',
                xtype: 'textareafield',
                fieldLabel: '备注',
                margin: '15 15 15 15',
                height: 50,
                maxLength: 500
            },
            {
                xtype: 'hiddenfield',
                name: 'dev'
            }
        ]
    }],
    buttons: [
      { id: 'saveResult', xtype: 'iconlabel', text: '' },
      { xtype: 'tbfill' },
      {
          xtype: 'button',
          text: '保存',
          handler: function (el, e) {
              var form = Ext.getCmp('saveForm'),
                  baseForm = form.getForm(),
                  result = Ext.getCmp('saveResult');

              result.setTextWithIcon('', '');
              if (baseForm.isValid()) {
                  result.setTextWithIcon('正在处理...', 'x-icon-loading');
                  baseForm.submit({
                      submitEmptyText: false,
                      clientValidation: true,
                      preventWindow: true,
                      url: '/Maintenance/SaveVSignal',
                      params: {
                          action: saveWnd.opaction
                      },
                      success: function (form, action) {
                          result.setTextWithIcon(action.result.message, 'x-icon-accept');
                          if (saveWnd.opaction == $$iPems.Action.Add)
                              query();
                          else
                              currentPagingToolbar.doRefresh();
                      },
                      failure: function (form, action) {
                          var message = '客户端未知错误';
                          if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                              message = action.result.message;

                          result.setTextWithIcon(message, 'x-icon-error');
                      }
                  });
              } else {
                  result.setTextWithIcon('表单填写错误', 'x-icon-error');
              }
          }
      }, {
          xtype: 'button',
          text: '关闭',
          handler: function (el, e) {
              saveWnd.close();
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

var operationContext = Ext.create('Ext.menu.Menu', {
    plain: true,
    border: false,
    record: null,
    items: [{
        glyph: 0xf002,
        text: '编辑',
        handler: function () {
            editCellClick(operationContext.record);
        }
    }, '-', {
        glyph: 0xf004,
        text: '删除',
        handler: function () {
            deleteCellClick(operationContext.record);
        }
    }]
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
            query();
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

var centerPanel = Ext.create('Ext.grid.Panel', {
    region: 'center',
    glyph: 0xf052,
    title: '虚拟信号',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: false,
    viewConfig: {
        forceFit: true,
        trackOver: true,
        stripeRows: true,
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: '序号',
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        xtype: 'actioncolumn',
        width: 50,
        align: 'center',
        menuDisabled: true,
        menuText: '操作',
        text: '操作',
        items: [{
            iconCls: 'x-cell-icon x-icon-cog',
            handler: function (view, rowIndex, colIndex, item, e, record) {
                view.getSelectionModel().select(record);
                operationContext.record = record;
                operationContext.showAt(e.getXY());
            }
        }]
    }, {
        text: '编码',
        dataIndex: 'id',
        width: 100,
        sortable: true
    }, {
        text: '名称',
        dataIndex: 'name',
        width: 150,
        sortable: true
    }, {
        text: '类型',
        dataIndex: 'type',
        width: 60,
        sortable: true
    }, {
        text: '单位/状态',
        dataIndex: 'unit',
        width: 100,
        sortable: true
    }, {
        text: '存储周期(秒)',
        dataIndex: 'saved',
        width: 100,
        sortable: true
    }, {
        text: '统计周期(分钟)',
        dataIndex: 'stats',
        width: 100,
        sortable: true
    }, {
        text: '公式',
        dataIndex: 'formula',
        width: 150,
        sortable: true,
        renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
            metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
            return value;
        }
    }, {
        text: '信号分类',
        dataIndex: 'categoryName',
        width: 150,
        sortable: true
    }, {
        text: '备注',
        dataIndex: 'remark',
        width: 150,
        sortable: true
    }],
    tbar: Ext.create('Ext.toolbar.Toolbar', {
        items: [{
            xtype: 'button',
            text: '新增信号',
            glyph: 0xf001,
            handler: function (el, e) {
                if (currentNode == null) {
                    Ext.Msg.show({ title: '系统警告', msg: '请选择需要增加信号的设备节点', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
                    return false;
                }
                var id = currentNode.getId(),
                    keys = $$iPems.SplitKeys(id);

                if (keys.length !== 2) {
                    Ext.Msg.show({ title: '系统警告', msg: '请选择需要增加信号的设备节点', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
                    return false;
                }

                var dev = keys[1];
                var type = parseInt(keys[0]);
                if (type !== $$iPems.SSH.Device) {
                    Ext.Msg.show({ title: '系统警告', msg: '请选择需要增加信号的设备节点', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
                    return false;
                }

                var basic = Ext.getCmp('saveForm').getForm();
                basic.load({
                    url: '/Maintenance/GetVSignal',
                    params: { device: dev, point: null, action: $$iPems.Action.Add },
                    waitMsg: '正在处理...',
                    waitTitle: '系统提示',
                    success: function (form, action) {
                        form.clearInvalid();
                        Ext.getCmp('id').setReadOnly(false);
                        Ext.getCmp('saveResult').setTextWithIcon('', '');

                        saveWnd.setGlyph(0xf001);
                        saveWnd.setTitle('新增信号');
                        saveWnd.opaction = $$iPems.Action.Add;
                        saveWnd.show();
                    }
                });
            }
        }, '-', {
            xtype: 'textfield',
            id: 'namesfield',
            fieldLabel: '信号名称',
            labelWidth: 60,
            width: 360,
            maxLength: 100,
            emptyText: '多条件请以;分隔，例: A;B;C'
        }, {
            xtype: 'button',
            text: '数据查询',
            glyph: 0xf005,
            handler: function (el, e) {
                query();
            }
        }, '-', {
            id: 'exportButton',
            xtype: 'button',
            glyph: 0xf010,
            text: '数据导出',
            disabled: true,
            handler: function (el, e) {
                print();
            }
        }]
    }),
    bbar: currentPagingToolbar
});

var viewport = Ext.create('Ext.panel.Panel', {
    region: 'center',
    layout: 'border',
    border: false,
    items: [westPanel, centerPanel]
});

//#endregion

//#region Methods

var query = function () {
    if (currentNode == null) {
        Ext.Msg.show({ title: '系统警告', msg: '请选择左侧的设备节点', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
        return false;
    }

    var namesfield = Ext.getCmp('namesfield');
    if (namesfield.isValid()) {
        var me = currentStore, proxy = me.getProxy();
        proxy.extraParams.node = currentNode.getId();
        proxy.extraParams.name = namesfield.getRawValue();
        proxy.extraParams.cache = false;
        me.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
                Ext.getCmp('exportButton').setDisabled(success === false);
            }
        });
    }
};

var print = function () {
    $$iPems.download({
        url: '/Maintenance/DownloadVSignals',
        params: currentStore.getProxy().extraParams
    });
};

var editCellClick = function (record) {
    if (Ext.isEmpty(record)) return false;

    var basic = Ext.getCmp('saveForm').getForm();
    basic.load({
        url: '/Maintenance/GetVSignal',
        params: { device: record.get('dev'), point: record.get('id'), action: $$iPems.Action.Edit },
        waitMsg: '正在处理...',
        waitTitle: '系统提示',
        success: function (form, action) {
            form.clearInvalid();
            Ext.getCmp('id').setReadOnly(true);
            Ext.getCmp('saveResult').setTextWithIcon('', '');

            saveWnd.setGlyph(0xf002);
            saveWnd.setTitle('编辑信号');
            saveWnd.opaction = $$iPems.Action.Edit;
            saveWnd.show();
        }
    });
};

var deleteCellClick = function (record) {
    if (Ext.isEmpty(record)) return false;

    Ext.Msg.confirm('确认对话框', '您确认要删除吗？', function (buttonId, text) {
        if (buttonId === 'yes') {
            Ext.Ajax.request({
                url: '/Maintenance/DeleteVSignal',
                params: { device: record.get('dev'), point: record.get('id') },
                mask: new Ext.LoadMask(centerPanel, { msg: '正在处理...' }),
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success)
                        currentPagingToolbar.doRefresh();
                    else
                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            });
        }
    });
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