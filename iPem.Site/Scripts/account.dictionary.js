(function () {
    Ext.apply(Ext.form.field.VTypes, {
        IPv4: function (v) {
            return /^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/.test(v);
        },
        IPv4Text: 'IPv4地址格式错误',
        IPv4Mask: /[\d\.]/i
    }, {
        Formula: function (val, field) {
            var result = $$iPems.validateFormula(val, true);
            field.vtypeText = result;
            return result === $$iPems.formulaResults.Success;
        },
        FormulaText: '公式格式错误'
    });

    var currentFormulaNode = null;

    var formulaContextMenu01 = Ext.create('Ext.menu.Menu', {
        plain: true,
        border: false,
        source: null,
        target: null,
        items: [{
            itemId: 'copy',
            glyph: 0xf053,
            text: '复制公式',
            handler: function () {
                var me = formulaContextMenu01;
                if (!Ext.isEmpty(me.target))
                    me.source = me.target;
            }
        }, '-', {
            itemId: 'paste',
            glyph: 0xf054,
            text: '粘帖公式',
            handler: function () {
                var me = formulaContextMenu01;
                if (Ext.isEmpty(me.source)) return false;
                if (Ext.isEmpty(me.target)) return false;

                var confirm = Ext.String.format('您确定要粘贴公式吗？<br/>{0} -> {1}', me.source.data.text, me.target.data.text);
                Ext.Msg.confirm('确认对话框', confirm, function (buttonId, text) {
                    if (buttonId === 'yes') {
                        var tree = Ext.getCmp('formula-targets');
                        Ext.Ajax.request({
                            url: '/Account/PasteFormula',
                            params: { source: me.source.data.id, target: me.target.data.id },
                            mask: new Ext.LoadMask(tree, { msg: '正在处理...' }),
                            success: function (response, options) {
                                var result = Ext.decode(response.responseText, true);
                                if (result.success) {
                                    tree.fireEvent('select', tree.getSelectionModel(), currentFormulaNode, 0);
                                } else
                                    Ext.Msg.show({ title: '系统错误', msg: result.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                            }
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

    var formulaContextMenu02 = Ext.create('Ext.menu.Menu', {
        plain: true,
        border: false,
        source: null,
        target: null,
        items: [{
            itemId: 'copy',
            glyph: 0xf053,
            text: '复制公式',
            handler: function () {
                var me = formulaContextMenu02;
                if (!Ext.isEmpty(me.target))
                    me.source = me.target;
            }
        }, '-', {
            itemId: 'paste',
            glyph: 0xf054,
            text: '粘帖公式',
            handler: function () {
                var me = formulaContextMenu02;
                if (Ext.isEmpty(me.source)) return false;
                if (Ext.isEmpty(me.target)) return false;

                var confirm = Ext.String.format('您确定要粘贴公式吗？<br/>{0} -> {1}', me.source.data.text, me.target.data.text);
                Ext.Msg.confirm('确认对话框', confirm, function (buttonId, text) {
                    if (buttonId === 'yes') {
                        var tree = Ext.getCmp('formula-targets');
                        Ext.Ajax.request({
                            url: '/Account/PasteFormula',
                            params: { source: me.source.data.id, target: me.target.data.id },
                            mask: new Ext.LoadMask(tree, { msg: '正在处理...' }),
                            success: function (response, options) {
                                var result = Ext.decode(response.responseText, true);
                                if (result.success) {
                                    tree.fireEvent('select', tree.getSelectionModel(), currentFormulaNode, 0);
                                } else
                                    Ext.Msg.show({ title: '系统错误', msg: result.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                            }
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
                itemId: 'formulaDevPanel',
                xtype: 'treepanel',
                width: 230,
                autoScroll: true,
                useArrows: false,
                rootVisible: false,
                margin: '5 0 5 5',
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
                        url: '/Account/GetFormulaDevices',
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
                        var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                            pointPanel = cttPanel.getComponent('formulaVariables'),
                            store = pointPanel.getStore();

                        store.proxy.extraParams.parent = record.getId();
                        store.loadPage(1);
                    }
                },
                tbar: [
                    {
                        id: 'formulaDeviceType',
                        xtype: 'DeviceTypeMultiCombo',
                        emptyText: '设备筛选，默认全部',
                        fieldLabel: null,
                        flex: 1,
                        listeners: {
                            change: function (me, newValue, oldValue) {
                                var devPanel = formulaWin.getComponent('formulaDevPanel'),
                                devTypes = Ext.getCmp('formulaDeviceType'),
                                store = devPanel.getStore();

                                store.proxy.extraParams.devTypes = devTypes.getValue();
                                store.load();
                            }
                        }
                    }
                ]
            }, {
                itemId: 'formulaContentPanel',
                xtype: 'container',
                flex: 1,
                margin: 5,
                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },
                items: [
                    {
                        itemId: 'formulaVariables',
                        xtype: 'grid',
                        flex: 1,
                        store: Ext.create('Ext.data.Store', {
                            autoLoad: false,
                            pageSize: 20,
                            fields: [
                                { name: 'Id', type: 'int' },
                                { name: 'Value', type: 'string' }
                            ],
                            proxy: {
                                type: 'ajax',
                                url: '/Account/GetFormulaPoints',
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
                            { text: '序号', dataIndex: 'Id', width: 60 },
                            { text: '变量（双击变量，将其加入公式）', dataIndex: 'Value', width: 350 }
                        ],
                        listeners: {
                            itemdblclick: function (me, record, item, index, e) {
                                var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                    formula = cttPanel.getComponent('formulaField');

                                $$iPems.insertAtCursor(formula, record.get('Value'));
                            }
                        }
                    }, {
                        itemId: 'formulaField',
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
                                    var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                        formula = cttPanel.getComponent('formulaField');

                                    $$iPems.insertAtCursor(formula, ' + ');
                                }
                            },
                            '-',
                            {
                                text: '-', handler: function () {
                                    var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                        formula = cttPanel.getComponent('formulaField');

                                    $$iPems.insertAtCursor(formula, ' - ');
                                }
                            },
                            '-',
                            {
                                text: '×', handler: function () {
                                    var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                        formula = cttPanel.getComponent('formulaField');

                                    $$iPems.insertAtCursor(formula, ' * ');
                                }
                            },
                            '-',
                            {
                                text: '÷', handler: function () {
                                    var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                        formula = cttPanel.getComponent('formulaField');

                                    $$iPems.insertAtCursor(formula, ' / ');
                                }
                            },
                            '-',
                            {
                                text: '(', handler: function () {
                                    var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                        formula = cttPanel.getComponent('formulaField');

                                    $$iPems.insertAtCursor(formula, '( ');
                                }
                            },
                            '-',
                            {
                                text: ')', handler: function () {
                                    var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                        formula = cttPanel.getComponent('formulaField');

                                    $$iPems.insertAtCursor(formula, ' )');
                                }
                            },
                            '-',
                            {
                                text: '清空', handler: function () {
                                    var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                        formula = cttPanel.getComponent('formulaField');

                                    formula.reset();
                                    formula.focus();
                                }
                            },
                            '-',
                            {
                                text: '删除', handler: function () {
                                    var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                                        formula = cttPanel.getComponent('formulaField');

                                    $$iPems.deleteAtCursor(formula);
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
                  var cttPanel = formulaWin.getComponent('formulaContentPanel'),
                      formula = cttPanel.getComponent('formulaField');

                  if (!formula.isValid()) return;

                  if (!Ext.isEmpty(formulaWin.target))
                      formulaWin.target.setValue(formula.getValue());

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

    var showFormulaWnd = function (node, target) {
        var devPanel = formulaWin.getComponent('formulaDevPanel'),
            cttPanel = formulaWin.getComponent('formulaContentPanel'),
            pointPanel = cttPanel.getComponent('formulaVariables'),
            formula = cttPanel.getComponent('formulaField'),
            potStore = pointPanel.getStore();

        devPanel.setRootNode({ id: node.data.id, text: node.data.text, icon: $$iPems.icons.Home, expanded: true });

        potStore.proxy.extraParams.parent = node.data.id;
        potStore.removeAll();

        formula.setValue(target.getValue());
        formulaWin.target = target;
        formulaWin.show();
    };

    var layout = Ext.create('Ext.tab.Panel', {
        region: 'center',
        border: true,
        plain: true,
        items: [
            {
                id: 'wsconfig',
                title: '数据通信',
                glyph: 0xf047,
                xtype: 'form',
                overflowY: 'auto',
                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },
                items: [{
                    itemId: 'login',
                    xtype: 'fieldset',
                    title: 'WebService 登录信息',
                    margin: '20 20 10 20',
                    defaultType: 'textfield',
                    layout: 'anchor',
                    fieldDefaults: {
                        anchor: '100%',
                        labelWidth: 150,
                        labelAlign: 'left',
                        margin: 25
                    },
                    items: [
                        {
                            itemId: 'ip',
                            name: 'ip',
                            xtype: 'textfield',
                            fieldLabel: 'WebService 通信地址',
                            emptyText: '示例： 192.168.10.100',
                            vtype: 'IPv4',
                            allowBlank: false
                        },
                        {
                            itemId: 'port',
                            name: 'port',
                            xtype: 'numberfield',
                            fieldLabel: 'WebService 通信端口',
                            value: 8080,
                            minValue: 1,
                            maxValue: 65535,
                            allowBlank: false
                        },
                        {
                            itemId: 'uid',
                            name: 'uid',
                            xtype: 'textfield',
                            fieldLabel: 'WebService 登录帐号',
                            allowBlank: false
                        },
                        {
                            itemId: 'password',
                            name: 'password',
                            xtype: 'textfield',
                            fieldLabel: 'WebService 登录密码',
                            allowBlank: false
                        }
                    ]
                }, {
                    itemId: 'path',
                    xtype: 'fieldset',
                    title: 'WebService 访问路径',
                    margin: '10 20 20 20',
                    defaultType: 'textfield',
                    layout: 'anchor',
                    fieldDefaults: {
                        anchor: '100%',
                        labelWidth: 150,
                        labelAlign: 'left',
                        margin: 25
                    },
                    items: [
                        {
                            itemId: 'dataPath',
                            name: 'dataPath',
                            xtype: 'textfield',
                            fieldLabel: '实时数据 虚拟路径',
                            emptyText: '示例： /Services/GetData',
                            allowBlank: false
                        },
                        {
                            itemId: 'orderPath',
                            name: 'orderPath',
                            xtype: 'textfield',
                            fieldLabel: '远程控制 虚拟路径',
                            emptyText: '示例： /Services/SetPoint',
                            allowBlank: false
                        }
                    ]
                }],
                buttonAlign: 'left',
                buttons: [
                    {
                        xtype: 'button',
                        text: '保存当前页',
                        cls: 'custom-button custom-success',
                        handler: function () {
                            var wsconfig = Ext.getCmp('wsconfig'),
                                wsbasic = wsconfig.getForm(),
                                wsresult = Ext.getCmp('wsresult');

                            wsresult.setTextWithIcon('', '');
                            if (wsbasic.isValid()) {
                                wsresult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                wsbasic.submit({
                                    submitEmptyText: false,
                                    clientValidation: true,
                                    preventWindow: true,
                                    url: '/Account/SaveWs',
                                    success: function (form, action) {
                                        wsresult.setTextWithIcon(action.result.message, 'x-icon-accept');
                                    },
                                    failure: function (form, action) {
                                        var message = 'undefined error.';
                                        if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                            message = action.result.message;

                                        wsresult.setTextWithIcon(message, 'x-icon-error');
                                    }
                                });
                            } else {
                                wsresult.setTextWithIcon('表单填写错误', 'x-icon-error');
                            }
                        }
                    },
                    { id: 'wsresult', xtype: 'iconlabel', text: '' }
                ]
            },
            {
                id: 'tsconfig',
                title: '语音播报',
                glyph: 0xf048,
                xtype: 'form',
                overflowY: 'auto',
                items: [
                    {
                        xtype: 'container',
                        layout: 'hbox',
                        margin: '20 20 10 20',
                        items: [
                            {
                                xtype: 'fieldset',
                                flex: 1,
                                title: '语音播报',
                                defaultType: 'checkbox',
                                layout: 'anchor',
                                defaults: {
                                    anchor: '100%',
                                    margin: 15
                                },
                                items: [{
                                    xtype: 'checkboxgroup',
                                    columns: 2,
                                    items: [
                                        { boxLabel: '启用语音播报', name: 'basic', inputValue: 1 },
                                        { boxLabel: '循环播报告警', name: 'basic', inputValue: 2 },
                                        { boxLabel: '禁播工程告警', name: 'basic', inputValue: 3 }
                                    ]
                                }]
                            }, {
                                xtype: 'component',
                                width: 20
                            }, {
                                xtype: 'fieldset',
                                flex: 1,
                                title: '播报级别',
                                defaultType: 'radio',
                                layout: 'anchor',
                                defaults: {
                                    anchor: '100%',
                                    margin: 15
                                },
                                items: [
                                    {
                                        xtype: 'checkboxgroup',
                                        columns: 2,
                                        items: [
                                            { boxLabel: '一级告警', name: 'levels', inputValue: $$iPems.AlmLevel.Level1 },
                                            { boxLabel: '二级告警', name: 'levels', inputValue: $$iPems.AlmLevel.Level2 },
                                            { boxLabel: '三级告警', name: 'levels', inputValue: $$iPems.AlmLevel.Level3 },
                                            { boxLabel: '四级告警', name: 'levels', inputValue: $$iPems.AlmLevel.Level4 }
                                        ]
                                    }
                                ]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '播报内容',
                        margin: '10 20 20 20',
                        defaultType: 'checkbox',
                        layout: 'anchor',
                        defaults: {
                            anchor: '100%',
                            margin: 15
                        },
                        items: [{
                            xtype: 'checkboxgroup',
                            columns: 4,
                            items: [
                                { boxLabel: '所属区域', name: 'contents', inputValue: 1 },
                                { boxLabel: '所属站点', name: 'contents', inputValue: 2 },
                                { boxLabel: '所属机房', name: 'contents', inputValue: 3 },
                                { boxLabel: '所属设备', name: 'contents', inputValue: 4 },
                                { boxLabel: '所属信号', name: 'contents', inputValue: 5 },
                                { boxLabel: '告警时间', name: 'contents', inputValue: 6 },
                                { boxLabel: '告警等级', name: 'contents', inputValue: 7 },
                                { boxLabel: '告警描述', name: 'contents', inputValue: 8 }
                            ]
                        }]
                    },
                    {
                        xtype: 'fieldset',
                        title: '播报条件',
                        margin: 20,
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 60,
                            labelAlign: 'left',
                            margin: 15
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
                                        items: [
                                            {
                                                name: 'stationTypes',
                                                xtype: 'StationTypeMultiCombo',
                                                emptyText: '默认全部'
                                            },
                                            {
                                                name: 'deviceTypes',
                                                xtype: 'DeviceTypeMultiCombo',
                                                emptyText: '默认全部'
                                            },
                                            {
                                                name: 'pointNames',
                                                xtype: 'textfield',
                                                fieldLabel: '信号名称',
                                                emptyText: '多条件请以;分隔，例: A;B;C'
                                            }
                                        ]
                                    },
                                    {
                                        xtype: 'container',
                                        flex: 1,
                                        layout: 'anchor',
                                        items: [
                                            {
                                                name: 'roomTypes',
                                                xtype: 'RoomTypeMultiCombo',
                                                emptyText: '默认全部'
                                            },
                                            {
                                                name: 'logicTypes',
                                                xtype: 'SubLogicTypeMultiPicker',
                                                emptyText: '默认全部'
                                            },
                                            {
                                                name: 'pointExtset',
                                                xtype: 'textfield',
                                                fieldLabel: '信号标识',
                                                emptyText: '多条件请以;分隔，例: A;B;C'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
                buttonAlign: 'left',
                buttons: [
                    {
                        xtype: 'button',
                        text: '保存当前页',
                        cls: 'custom-button custom-success',
                        handler: function () {
                            var tsconfig = Ext.getCmp('tsconfig'),
                                tsbasic = tsconfig.getForm(),
                                tsresult = Ext.getCmp('tsresult');

                            tsresult.setTextWithIcon('', '');
                            if (tsbasic.isValid()) {
                                tsresult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                tsbasic.submit({
                                    submitEmptyText: false,
                                    clientValidation: true,
                                    preventWindow: true,
                                    url: '/Account/SaveTs',
                                    success: function (form, action) {
                                        tsresult.setTextWithIcon(action.result.message, 'x-icon-accept');
                                    },
                                    failure: function (form, action) {
                                        var message = 'undefined error.';
                                        if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                            message = action.result.message;

                                        tsresult.setTextWithIcon(message, 'x-icon-error');
                                    }
                                });
                            } else {
                                tsresult.setTextWithIcon('表单填写错误', 'x-icon-error');
                            }
                        }
                    },
                    { id: 'tsresult', xtype: 'iconlabel', text: '' }
                ]
            },
            {
                title: '能耗公式',
                glyph: 0xf052,
                layout: {
                    type: 'hbox',
                    align: 'stretch'
                },
                items: [
                    {
                        id: 'formula-targets',
                        xtype: 'treepanel',
                        width: 300,
                        margin: '15 10 10 20',
                        glyph: 0xf011,
                        title: '能耗对象',
                        autoScroll: true,
                        useArrows: false,
                        rootVisible: false,
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
                                url: '/Component/GetRooms',
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
                                currentFormulaNode = record;
                                var id = record.getId(),
                                    pueform = Ext.getCmp('formulasForm'),
                                    puebasic = pueform.getForm(),
                                    puesave = Ext.getCmp('puesave'),
                                    pueresult = Ext.getCmp('pueresult'),
                                    keys = $$iPems.SplitKeys(id),
                                    visible = keys.length === 2 && (keys[0] == $$iPems.Organization.Station || keys[0] == $$iPems.Organization.Room);

                                puebasic.reset();
                                pueform.setDisabled(!visible);
                                puesave.setDisabled(!visible);
                                pueresult.setTextWithIcon('', '');

                                if (visible) {
                                    puebasic.load({
                                        url: '/Account/GetFormula',
                                        params: { current: id },
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

                                if (keys.length === 2) {
                                    if (keys[0] == $$iPems.Organization.Station) {
                                        formulaContextMenu01.target = record;
                                        formulaContextMenu01.showAt(e.getXY());
                                    } else if (keys[0] == $$iPems.Organization.Room) {
                                        formulaContextMenu02.target = record;
                                        formulaContextMenu02.showAt(e.getXY());
                                    }
                                }
                            }
                        },
                        tbar: [
                                {
                                    id: 'formula-targets-search-field',
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
                                        var tree = Ext.getCmp('formula-targets'),
                                            search = Ext.getCmp('formula-targets-search-field'),
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
                    }, {
                        id: 'formulasForm',
                        xtype: 'form',
                        flex: 1,
                        border: false,
                        disabled: true,
                        overflowY: 'auto',
                        waitMsgTarget: true,
                        items: [
                            {
                                xtype: 'fieldset',
                                layout: 'anchor',
                                title: '空调能耗',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'ktFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
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
                                xtype: 'fieldset',
                                title: '照明能耗',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'zmFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
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
                                xtype: 'fieldset',
                                title: '办公能耗',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'bgFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
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
                                xtype: 'fieldset',
                                title: '设备能耗',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'sbFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
                                    },
                                    {
                                        name: 'sbRemarks',
                                        xtype: 'textfield',
                                        fieldLabel: '备注',
                                        margin: '10 10 10 10'
                                    }
                                ]
                            },
                            {
                                xtype: 'fieldset',
                                title: '开关电源能耗',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'kgdyFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
                                    },
                                    {
                                        name: 'kgdyRemarks',
                                        xtype: 'textfield',
                                        fieldLabel: '备注',
                                        margin: '10 10 10 10'
                                    }
                                ]
                            },
                            {
                                xtype: 'fieldset',
                                title: 'UPS能耗',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'upsFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
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
                                xtype: 'fieldset',
                                title: '其他能耗',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'qtFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
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
                                xtype: 'fieldset',
                                title: '总能耗',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'zlFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
                                    },
                                    {
                                        name: 'zlRemarks',
                                        xtype: 'textfield',
                                        fieldLabel: '备注',
                                        margin: '10 10 10 10'
                                    }
                                ]
                            },
                            {
                                xtype: 'fieldset',
                                title: 'PUE',
                                margin: '10 35 20 10',
                                fieldDefaults: {
                                    anchor: '100%',
                                    labelWidth: 40,
                                    labelAlign: 'left'
                                },
                                items: [
                                    {
                                        name: 'pueFormulas',
                                        xtype: 'triggerfield',
                                        fieldLabel: '公式',
                                        triggerCls: 'x-fx-trigger',
                                        vtype: 'Formula',
                                        margin: '10 10 0 10',
                                        onTriggerClick: function () {
                                            showFormulaWnd(currentFormulaNode, this);
                                        }
                                    },
                                    {
                                        name: 'pueRemarks',
                                        xtype: 'textfield',
                                        fieldLabel: '备注',
                                        margin: '10 10 10 10'
                                    }
                                ]
                            }
                        ]
                    }
                ],
                buttonAlign: 'left',
                buttons: [
                    {
                        id: 'puesave',
                        xtype: 'button',
                        text: '保存当前页',
                        cls: 'custom-button custom-success',
                        disabled: true,
                        handler: function () {
                            var pueform = Ext.getCmp('formulasForm'),
                                puebasic = pueform.getForm(),
                                pueresult = Ext.getCmp('pueresult');

                            pueresult.setTextWithIcon('', '');
                            if (puebasic.isValid()) {
                                pueresult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                puebasic.submit({
                                    submitEmptyText: false,
                                    clientValidation: true,
                                    preventWindow: true,
                                    url: '/Account/SaveFormula',
                                    params: {
                                        current: currentFormulaNode.data.id
                                    },
                                    success: function (form, action) {
                                        pueresult.setTextWithIcon(action.result.message, 'x-icon-accept');
                                    },
                                    failure: function (form, action) {
                                        var message = 'undefined error.';
                                        if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                            message = action.result.message;

                                        pueresult.setTextWithIcon(message, 'x-icon-error');
                                    }
                                });
                            } else {
                                pueresult.setTextWithIcon('表单填写错误', 'x-icon-error');
                            }
                        }
                    },
                    { id: 'pueresult', xtype: 'iconlabel', text: '' }
                ]
            },
            {
                id: 'rtconfig',
                title: '报表参数',
                glyph: 0xf044,
                xtype: 'form',
                overflowY: 'auto',
                items: [
                    {
                        xtype: 'fieldset',
                        title: '缓存管理',
                        margin: '10 35 20 20',
                        layout: {
                            type: 'hbox',
                            align: 'stretch'
                        },
                        items: [{
                            xtype: 'button',
                            text: '清空所有缓存',
                            cls: 'custom-button custom-danger',
                            margin: 15,
                            handler: function () {
                                Ext.Msg.confirm('确认对话框', '您确认要清空缓存吗？', function (buttonId, text) {
                                    if (buttonId === 'yes') {
                                        var cacheResult = Ext.getCmp('cacheResult');
                                        cacheResult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                        Ext.Ajax.request({
                                            url: '/Account/ClearCache',
                                            success: function (response, options) {
                                                var data = Ext.decode(response.responseText, true);
                                                if (data) cacheResult.setTextWithIcon(data.message, data.success ? 'x-icon-accept' : 'x-icon-error');
                                            }
                                        });
                                    }
                                });
                            }
                        },
                        {
                            xtype: 'button',
                            text: '清空全局缓存',
                            cls: 'custom-button custom-warning',
                            margin: 15,
                            handler: function () {
                                Ext.Msg.confirm('确认对话框', '您确认要清空缓存吗？', function (buttonId, text) {
                                    if (buttonId === 'yes') {
                                        var cacheResult = Ext.getCmp('cacheResult');
                                        cacheResult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                        Ext.Ajax.request({
                                            url: '/Account/ClearGlobalCache',
                                            success: function (response, options) {
                                                var data = Ext.decode(response.responseText, true);
                                                if (data) cacheResult.setTextWithIcon(data.message, data.success ? 'x-icon-accept' : 'x-icon-error');
                                            }
                                        });
                                    }
                                });
                            }
                        },
                        {
                            xtype: 'button',
                            text: '清空用户缓存',
                            cls: 'custom-button custom-info',
                            margin: 15,
                            handler: function () {
                                Ext.Msg.confirm('确认对话框', '您确认要清空缓存吗？', function (buttonId, text) {
                                    if (buttonId === 'yes') {
                                        var cacheResult = Ext.getCmp('cacheResult');
                                        cacheResult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                        Ext.Ajax.request({
                                            url: '/Account/ClearUserCache',
                                            success: function (response, options) {
                                                var data = Ext.decode(response.responseText, true);
                                                if (data) cacheResult.setTextWithIcon(data.message, data.success ? 'x-icon-accept' : 'x-icon-error');
                                            }
                                        });
                                    }
                                });
                            }
                        },
                        {
                            id: 'cacheResult',
                            xtype: 'iconlabel',
                            margin: '21 0 21 0',
                            text: '',
                            flex: 1
                        }]
                    },
                    {
                        xtype: 'fieldset',
                        title: '异常告警',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'chaoDuan',
                                    xtype: 'numberfield',
                                    fieldLabel: '超短告警',
                                    allowBlank: false,
                                    value: 1,
                                    minValue: 1,
                                    flex: 1
                                }, {
                                    xtype: 'displayfield',
                                    value: '分钟（注：超短告警是指告警历时小于该阈值的告警）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'chaoChang',
                                    xtype: 'numberfield',
                                    fieldLabel: '超长告警',
                                    allowBlank: false,
                                    value: 1,
                                    minValue: 1,
                                    flex: 1
                                }, {
                                    xtype: 'displayfield',
                                    value: '分钟（注：超长告警是指告警历时大于该阈值的告警）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'chaoPin',
                                    xtype: 'numberfield',
                                    fieldLabel: '超频告警',
                                    allowBlank: false,
                                    allowDecimals: false,
                                    value: 1,
                                    minValue: 1,
                                    flex: 1
                                }, {
                                    xtype: 'displayfield',
                                    value: '次数（注：超频告警是指告警次数小于该阈值的告警）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '市电停电',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 100,
                            labelAlign: 'left',
                            margin: 15
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                items: [{
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        name: 'tingDianXinHao',
                                        xtype: 'PointPicker',
                                        fieldLabel: '市电状态信号',
                                        allowBlank: false,
                                        emptyText: '请选择市电状态信号...'
                                    }, {
                                        name: 'weiTingDian',
                                        xtype: 'numberfield',
                                        fieldLabel: '市电正常测值',
                                        allowBlank: false,
                                        allowDecimals: false,
                                        emptyText: '市电正常状态时的测值',
                                        value: 0,
                                        minValue: 0
                                    }]
                                }, {
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        xtype: 'displayfield'
                                    }, {
                                        name: 'tingDian',
                                        xtype: 'numberfield',
                                        fieldLabel: '市电停电测值',
                                        allowBlank: false,
                                        allowDecimals: false,
                                        emptyText: '市电停电状态时的测值',
                                        value: 1,
                                        minValue: 0
                                    }]
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '油机发电',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 100,
                            labelAlign: 'left',
                            margin: 15
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                items: [{
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        name: 'faDianXinHao',
                                        xtype: 'PointPicker',
                                        fieldLabel: '油机状态信号',
                                        allowBlank: false,
                                        emptyText: '请选择油机状态信号...'
                                    }, {
                                        name: 'weiFaDian',
                                        xtype: 'numberfield',
                                        fieldLabel: '油机停机测值',
                                        allowBlank: false,
                                        allowDecimals: false,
                                        emptyText: '油机停机状态时的测值',
                                        value: 0,
                                        minValue: 0
                                    }]
                                }, {
                                    xtype: 'container',
                                    flex: 1,
                                    layout: 'anchor',
                                    items: [{
                                        xtype: 'displayfield'
                                    }, {
                                        name: 'faDian',
                                        xtype: 'numberfield',
                                        fieldLabel: '油机工作测值',
                                        allowBlank: false,
                                        allowDecimals: false,
                                        emptyText: '油机工作状态时的测值',
                                        value: 1,
                                        minValue: 0
                                    }]
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '系统设备完好率',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: 15,
                                items: [{
                                    name: 'whlHuLue',
                                    xtype: 'numberfield',
                                    fieldLabel: '忽略告警',
                                    allowBlank: false,
                                    value: 0,
                                    minValue: 0,
                                    flex: 1
                                }, {
                                    xtype: 'displayfield',
                                    value: '分钟（注：为了规避频繁告警，报表统计时将忽略告警历时小于该阈值的告警）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '故障处理及时率',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'jslGuiDing',
                                    xtype: 'numberfield',
                                    fieldLabel: '处理时长',
                                    allowBlank: false,
                                    value: 0,
                                    minValue: 0,
                                    flex: 1
                                }, {
                                    xtype: 'displayfield',
                                    value: '分钟（注：报表统计时将计算超过该规定处理时长的告警）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'jslHuLue',
                                    xtype: 'numberfield',
                                    fieldLabel: '忽略告警',
                                    allowBlank: false,
                                    value: 0,
                                    minValue: 0,
                                    flex: 1
                                }, {
                                    xtype: 'displayfield',
                                    value: '分钟（注：为了规避频繁告警，报表统计时将忽略告警历时小于该阈值的告警）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '告警确认及时率',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: 15,
                                items: [{
                                    name: 'jslQueRen',
                                    xtype: 'numberfield',
                                    fieldLabel: '确认时长',
                                    allowBlank: false,
                                    value: 0,
                                    minValue: 0,
                                    flex: 1
                                }, {
                                    xtype: 'displayfield',
                                    value: '分钟（注：报表统计时将计算超过该规定确认时长的告警）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '直流系统可用度(核心站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'hxzlxtkydXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '告警信号',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"开关电源蓄电池组总电压低"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'hxzlxtkydLeiXing',
                                    xtype: 'SubDeviceTypeMultiPicker',
                                    flex: 1,
                                    fieldLabel: '设备类型',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"开关电源蓄电池组"设备类型）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '交流不间断系统可用度(核心站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'hxjlxtkydXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '告警信号',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"UPS蓄电池组总电压低"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'hxjlxtkydPangLuXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '运行信号',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"UPS旁路运行"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'hxjlxtkydLeiXing',
                                    xtype: 'SubDeviceTypeMultiPicker',
                                    flex: 1,
                                    fieldLabel: '设备类型',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"UPS蓄电池组"设备类型）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '温控系统可用度(核心站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'hxwkxtkydXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '告警信号',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"高温告警"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '监控可用度(核心站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'hxjkkydXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '告警信号',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"监控系统采集设备中断"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'hxjkkydLeiXing',
                                    xtype: 'SubDeviceTypeMultiPicker',
                                    flex: 1,
                                    fieldLabel: '设备类型',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"采集设备"设备类型）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '关键监控测点接入率(其他站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'qtgjjkcdjrlLeiXing',
                                    xtype: 'SubDeviceTypeMultiPicker',
                                    flex: 1,
                                    fieldLabel: '设备类型',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"开关电源"、"蓄电池组"设备类型）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '开关电源带载合格率(其他站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'qtkgdydzhglLeiXing',
                                    xtype: 'SubDeviceTypeMultiPicker',
                                    flex: 1,
                                    fieldLabel: '设备类型',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"开关电源"设备类型）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }, {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'qtkgdydzhglztXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '工作状态',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"开关电源工作状态"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'qtkgdydzhglfzXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '负载电流',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"开关电源负载电流"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '蓄电池后备时长合格率(其他站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'qtxdchbschglLeiXing',
                                    xtype: 'SubDeviceTypeMultiPicker',
                                    flex: 1,
                                    fieldLabel: '设备类型',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"蓄电池组"设备类型）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }, {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'qtxdchbschglztXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '工作状态',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"蓄电池组工作状态"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'qtxdchbschglfzXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '负载电流',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"蓄电池组负载电流"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '温控容量合格率(其他站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'qtwkrlhglXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '告警信号',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"高温告警"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '直流系统可用度(其他站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 0 15',
                                items: [{
                                    name: 'qtzlxtkydXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '告警信号',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"开关电源一次下电"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            },
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'qtzlxtkydLeiXing',
                                    xtype: 'SubDeviceTypeMultiPicker',
                                    flex: 1,
                                    fieldLabel: '设备类型',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"开关电源"设备类型）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    },
                    {
                        xtype: 'fieldset',
                        title: '监控故障处理及时率(其他站点)',
                        margin: '10 35 20 20',
                        defaultType: 'textfield',
                        fieldDefaults: {
                            anchor: '100%',
                            labelWidth: 80,
                            labelAlign: 'left'
                        },
                        layout: 'anchor',
                        items: [
                            {
                                xtype: 'container',
                                anchor: '100%',
                                layout: 'hbox',
                                margin: '15 15 15 15',
                                items: [{
                                    name: 'qtjkgzcljslXinHao',
                                    xtype: 'PointMultiPicker',
                                    flex: 1,
                                    fieldLabel: '告警信号',
                                    allowBlank: false
                                }, {
                                    xtype: 'displayfield',
                                    value: '（注：设置"站点通信中断"信号）',
                                    margin: '0 0 0 15',
                                    flex: 1
                                }]
                            }
                        ]
                    }
                ],
                buttonAlign: 'left',
                buttons: [
                    {
                        xtype: 'button',
                        text: '保存当前页',
                        cls: 'custom-button custom-success',
                        handler: function () {
                            var rtconfig = Ext.getCmp('rtconfig'),
                                rtbasic = rtconfig.getForm(),
                                rtresult = Ext.getCmp('rtresult');

                            rtresult.setTextWithIcon('', '');
                            if (rtbasic.isValid()) {
                                rtresult.setTextWithIcon('正在处理...', 'x-icon-loading');
                                rtbasic.submit({
                                    submitEmptyText: false,
                                    clientValidation: true,
                                    preventWindow: true,
                                    url: '/Account/SaveRt',
                                    success: function (form, action) {
                                        rtresult.setTextWithIcon(action.result.message, 'x-icon-accept');
                                    },
                                    failure: function (form, action) {
                                        var message = 'undefined error.';
                                        if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                            message = action.result.message;

                                        rtresult.setTextWithIcon(message, 'x-icon-error');
                                    }
                                });
                            } else {
                                rtresult.setTextWithIcon('表单填写错误', 'x-icon-error');
                            }
                        }
                    },
                    { id: 'rtresult', xtype: 'iconlabel', text: '' }
                ]
            }
        ]
    });

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(layout);

            var wsconfig = Ext.getCmp('wsconfig'),
                wsbasic = wsconfig.getForm(),
                wsresult = Ext.getCmp('wsresult');

            wsbasic.load({
                url: '/Account/GetWs',
                waitMsg: '正在处理...',
                waitTitle: '系统提示',
                success: function (form, action) {
                    form.clearInvalid();
                    wsresult.setTextWithIcon('', '');
                }
            });

            var tsconfig = Ext.getCmp('tsconfig'),
                tsbasic = tsconfig.getForm(),
                tsresult = Ext.getCmp('tsresult');

            tsbasic.load({
                url: '/Account/GetTs',
                waitMsg: '正在处理...',
                waitTitle: '系统提示',
                success: function (form, action) {
                    form.clearInvalid();
                    tsresult.setTextWithIcon('', '');
                }
            });

            var rtconfig = Ext.getCmp('rtconfig'),
                rtbasic = rtconfig.getForm(),
                rtresult = Ext.getCmp('rtresult');

            rtbasic.load({
                url: '/Account/GetRt',
                waitMsg: '正在处理...',
                waitTitle: '系统提示',
                success: function (form, action) {
                    form.clearInvalid();
                    rtresult.setTextWithIcon('', '');
                }
            });
        }
    });
})();