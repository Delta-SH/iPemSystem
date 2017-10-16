(function () {

    //#region Model
    Ext.define('OutEmployeeModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'id', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'sex', type: 'string' },
            { name: 'dept', type: 'string' },
            { name: 'icard', type: 'string' },
            { name: 'icardAddress', type: 'string' },
            { name: 'address', type: 'string' },
            { name: 'company', type: 'string' },
            { name: 'project', type: 'string' },
            { name: 'workPhone', type: 'string' },
            { name: 'mobilePhone', type: 'string' },
            { name: 'email', type: 'string' },
            { name: 'empName', type: 'string' },
            { name: 'remarks', type: 'string' },
            { name: 'enabled', type: 'boolean' },
            { name: 'cardId', type: 'string' },
            { name: 'decimalCard', type: 'string' },
            { name: 'devCount', type: 'int' }
        ],
        idProperty: 'index'
    });

    Ext.define('DetailModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
            { name: 'room', type: 'string' },
            { name: 'card', type: 'string' },
            { name: 'name', type: 'string' }
        ],
        idProperty: 'index'
    });
    //#endregion

    //#region Store
    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'OutEmployeeModel',
        DownloadURL: '/Report/DownloadBase400106',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestBase400106',
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

    var detailStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'DetailModel',
        DownloadURL: '/Report/DownloadDetail400106',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestDetail400106',
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

    var empTypeStore = new Ext.data.ArrayStore({
        fields: ['id', 'text'],
        data: [[0, '未发卡人员'], [1, '已发卡人员']]
    });

    var keyTypeStore = new Ext.data.ArrayStore({
        fields: ['id', 'text'],
        data: [[1, '按卡号查询'], [2, '按姓名查询'], [3, '按负责人姓名查询'], [4, '按负责人工号查询']]
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var detailPagingToolbar = $$iPems.clonePagingToolbar(detailStore);
    //#endregion

    var currentLayout = Ext.create('Ext.panel.Panel', {
        id: 'currentLayout',
        region: 'center',
        border: false,
        bodyCls: 'x-border-body-panel',
        layout: {
            type: 'vbox',
            align: 'stretch',
            pack: 'start'
        },
        items: [{
            xtype: 'grid',
            glyph: 0xf029,
            title: '外协信息',
            collapsible: true,
            collapseFirst: false,
            margin: '5 0 0 0',
            flex: 2,
            store: currentStore,
            tools: [{
                type: 'print',
                tooltip: '数据导出',
                handler: function (event, toolEl, panelHeader) {
                    print(currentStore);
                }
            }],
            viewConfig: {
                loadMask: true,
                stripeRows: true,
                trackOver: true,
                emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
            },
            columns: [
                {
                    text: '序号',
                    dataIndex: 'index',
                    width: 60
                }, {
                    text: '姓名',
                    dataIndex: 'name'
                }, {
                    text: '性别',
                    dataIndex: 'sex'
                }, {
                    text: '部门',
                    dataIndex: 'dept'
                }, {
                    text: '身份证号',
                    dataIndex: 'icard'
                }, {
                    text: '身份证住址',
                    dataIndex: 'icardAddress'
                }, {
                    text: '现住地址',
                    dataIndex: 'address'
                }, {
                    text: '公司名称',
                    dataIndex: 'company'
                }, {
                    text: '所属项目',
                    dataIndex: 'project',
                }, {
                    text: '办公电话',
                    dataIndex: 'workPhone'
                }, {
                    text: '移动电话',
                    dataIndex: 'mobilePhone'
                }, {
                    text: '邮箱',
                    dataIndex: 'email'
                }, {
                    text: '负责人',
                    dataIndex: 'empName'
                }, {
                    text: '备注',
                    dataIndex: 'remarks'
                }, {
                    text: '状态',
                    dataIndex: 'enabled',
                    renderer: function (value) { return value ? '有效' : '禁用'; }
                }, {
                    text: '关联卡片',
                    dataIndex: 'decimalCard'
                }, {
                    text: '授权设备',
                    dataIndex: 'devCount',
                    renderer: function (value, p, record) {
                        return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', record.get('cardId'), value);
                    }
                }],
            listeners: {
                cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
                    var elements = Ext.fly(td).select('a.grid-link');
                    if (elements.getCount() == 0) return false;
                    detail(elements.first().getAttribute('data'));
                }
            },
            bbar: currentPagingToolbar,
        }],
        dockedItems: [{
            xtype: 'panel',
            glyph: 0xf034,
            title: '筛选条件',
            collapsible: true,
            collapsed: false,
            dock: 'top',
            items: [
                {
                    xtype: 'toolbar',
                    border: false,
                    items: [
                        {
                            id: 'deptField',
                            xtype: 'DepartmentMultiCombo',
                            emptyText: '默认全部',
                            width: 280
                        }, {
                            id: 'emptypeField',
                            xtype: 'multicombo',
                            fieldLabel: '人员类型',
                            store: empTypeStore,
                            valueField: 'id',
                            displayField: 'text',
                            editable: false,
                            align: 'center',
                            delimiter: $$iPems.Delimiter,
                            emptyText: '默认全部',
                            queryMode: 'local',
                            triggerAction: 'all',
                            selectionMode: 'all',
                            forceSelection: true,
                            labelWidth: 60,
                            width: 280
                        }, {
                            xtype: 'button',
                            glyph: 0xf005,
                            text: '数据查询',
                            handler: function (me, event) {
                                query();
                            }
                        }
                    ]
                },
                {
                    xtype: 'toolbar',
                    border: false,
                    items: [
                        {
                            id: 'keytypeField',
                            xtype: 'combobox',
                            fieldLabel: '模糊筛选',
                            store: keyTypeStore,
                            mode: 'local',
                            valueField: 'id',
                            displayField: 'text',
                            value: 1,
                            triggerAction: 'all',
                            labelWidth: 60,
                            width: 280,
                            editable: false
                        }, {
                            id: 'keywordsField',
                            xtype: 'textfield',
                            emptyText: '多关键字请以;分隔，例: A;B;C',
                            width: 280
                        }, {
                            xtype: 'button',
                            glyph: 0xf010,
                            text: '数据导出',
                            handler: function (me, event) {
                                print(currentStore);
                            }
                        }
                    ]
                }
            ]
        }]
    });

    var detailWnd = Ext.create('Ext.window.Window', {
        title: '授权设备详情',
        glyph: 0xf029,
        height: 500,
        width: 800,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        layout: 'border',
        items: [{
            xtype: 'grid',
            region: 'center',
            border: false,
            store: detailStore,
            bbar: detailPagingToolbar,
            forceFit: false,
            viewConfig: {
                forceFit: true,
                loadMask: false,
                stripeRows: true,
                trackOver: true,
                preserveScrollOnRefresh: true,
                emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
            },
            columns: [
                { text: '序号', dataIndex: 'index', width: 60 },
                { text: '所属区域', dataIndex: 'area', width: 150 },
                { text: '所属站点', dataIndex: 'station', width: 150 },
                { text: '所属机房', dataIndex: 'room', width: 150 },
                { text: '设备名称', dataIndex: 'name', width: 150 },
            ]
        }],
        buttonAlign: 'right',
        buttons: [{
            xtype: 'button',
            text: '导出',
            handler: function (el, e) {
                print(detailStore);
            }
        }, {
            xtype: 'button',
            text: '关闭',
            handler: function (el, e) {
                detailWnd.hide();
            }
        }]
    });

    var query = function () {
        var deptField = Ext.getCmp('deptField'),
            emptypeField = Ext.getCmp('emptypeField'),
            keytypeField = Ext.getCmp('keytypeField'),
            keywordsField = Ext.getCmp('keywordsField'),
            departments = deptField.getValue(),
            emptypes = emptypeField.getValue(),
            keytype = keytypeField.getValue(),
            keywords = keywordsField.getValue();

        var proxy = currentStore.getProxy();
        proxy.extraParams.departments = departments;
        proxy.extraParams.emptypes = emptypes;
        proxy.extraParams.keytype = keytype;
        proxy.extraParams.keywords = keywords;
        proxy.extraParams.cache = false;
        currentStore.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
            }
        });
    };

    var print = function (store) {
        $$iPems.download({
            url: store.DownloadURL,
            params: store.getProxy().extraParams
        });
    };

    var detail = function (card) {
        var proxy = detailStore.getProxy();
        proxy.extraParams.card = card;

        detailStore.removeAll();
        detailStore.loadPage(1);
        detailWnd.show();
    };

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);

            //load data
            Ext.defer(query, 2000);
        }
    });

})();