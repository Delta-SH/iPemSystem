(function () {
    Ext.define("ReportModel", {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'employeeCode', type: 'string' },
            { name: 'employeeName', type: 'string' },
            { name: 'employeeType', type: 'string' },
            { name: 'department', type: 'string' },
            { name: 'cardId', type: 'string' },
            { name: 'decimalCard', type: 'string' },
            { name: 'count', type: 'int' }
        ],
        idProperty: 'index'
    });

    Ext.define("DetailModel", {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'recType', type: 'string' },
            { name: 'cardId', type: 'string' },
            { name: 'decimalCard', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'remark', type: 'string' }
        ],
        idProperty: 'index'
    });

    var empTypeStore = new Ext.data.ArrayStore({
        fields: ['id', 'text'],
        data: [[0, '正式员工'], [1, '外协人员']]
    });

    var keyTypeStore = new Ext.data.ArrayStore({
        fields: ['id', 'text'],
        data: [[1, '按卡号查询'], [2, '按工号查询'], [3, '按姓名查询']]
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'ReportModel',
        DownloadURL: '/Report/DownloadHistory400210',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistory400210',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                parent: 'root',
            },
            listeners: {
                exception: function (proxy, response, operation) {
                    Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            },
            simpleSortMode: true
        },
    });

    var detailStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'DetailModel',
        DownloadURL: '/Report/DownloadHistoryDetail400210',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistoryDetail400210',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            simpleSortMode: true
        },
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var detailPagingToolbar = $$iPems.clonePagingToolbar(detailStore)

    var currentLayout = Ext.create('Ext.grid.Panel', {
        glyph: 0xf029,
        title: '刷卡次数统计',
        region: 'center',
        store: currentStore,
        bbar: currentPagingToolbar,
        selType: 'cellmodel',
        viewConfig: {
            loadMask: true,
            stripeRows: true,
            trackOver: false,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
        },
        columns: [
            { text: '序号', dataIndex: 'index', width: 60 },
            { text: '刷卡人员', dataIndex: 'employeeName', width: 150 },
            { text: '人员类型', dataIndex: 'employeeType' },
            { text: '所属部门', dataIndex: 'department' },
            { text: '刷卡卡号', dataIndex: 'decimalCard' },
            {
                text: '刷卡次数',
                dataIndex: 'count',
                renderer: function (value, p, record) {
                    return Ext.String.format('<a data="{0}" class="grid-link" href="javascript:void(0);">{1}</a>', record.get('cardId'), value);
                }
            }
        ],
        listeners: {
            cellclick: function (view, td, cellIndex, record, tr, rowIndex, e) {
                var elements = Ext.fly(td).select('a.grid-link');
                if (elements.getCount() == 0) return false;
                detail(elements.first().getAttribute('data'));
            }
        },
        dockedItems: [{
            xtype: 'panel',
            dock: 'top',
            items: [
                {
                    xtype: 'toolbar',
                    border: false,
                    items: [
                        {
                            id: 'rangePicker',
                            xtype: 'DevicePicker',
                            fieldLabel: '查询范围',
                            emptyText: '默认全部',
                            labelWidth: 60,
                            width: 220
                        },
                        {
                            id: 'startField',
                            xtype: 'datefield',
                            fieldLabel: '开始时间',
                            labelWidth: 60,
                            width: 220,
                            value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
                            editable: false,
                            allowBlank: false
                        },
                        {
                            id: 'endField',
                            xtype: 'datefield',
                            fieldLabel: '结束时间',
                            labelWidth: 60,
                            width: 220,
                            value: new Date(),
                            editable: false,
                            allowBlank: false
                        },
                        {
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
                            id: 'empTypeField',
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
                            width: 220,

                        },
                        {
                            id: 'typeField',
                            xtype: 'combobox',
                            fieldLabel: '模糊筛选',
                            store: keyTypeStore,
                            mode: 'local',
                            valueField: 'id',
                            displayField: 'text',
                            value: 1,
                            triggerAction: 'all',
                            labelWidth: 60,
                            width: 220,
                            editable: false
                        },
                        {
                            id: 'keysField',
                            xtype: 'textfield',
                            emptyText: '多关键字请以;分隔，例: A;B;C',
                            width: 220
                        },
                        {
                            id: 'exportButton',
                            xtype: 'button',
                            glyph: 0xf010,
                            text: '数据导出',
                            disabled: true,
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
        title: '刷卡详情',
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
                { text: '所属区域', dataIndex: 'area' },
                { text: '所属站点', dataIndex: 'station' },
                { text: '所属机房', dataIndex: 'room' },
                { text: '设备名称', dataIndex: 'device' },
                { text: '刷卡类型', dataIndex: 'recType' },
                { text: '刷卡描述', dataIndex: 'remark' },
                { text: '刷卡卡号', dataIndex: 'decimalCard' },
                { text: '刷卡时间', dataIndex: 'time' }
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
        var parent = Ext.getCmp('rangePicker').getValue(),
            startDate = Ext.getCmp('startField').getRawValue(),
            endDate = Ext.getCmp('endField').getRawValue(),
            empTypes = Ext.getCmp('empTypeField').getValue(),
            keyType = Ext.getCmp('typeField').getValue(),
            keywords = Ext.getCmp('keysField').getRawValue(),
            proxy = currentStore.getProxy();

        proxy.extraParams.parent = parent;
        proxy.extraParams.startDate = startDate;
        proxy.extraParams.endDate = endDate;
        proxy.extraParams.empTypes = empTypes;
        proxy.extraParams.keyType = keyType;
        proxy.extraParams.keywords = keywords;
        proxy.extraParams.cache = false;
        currentStore.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
                Ext.getCmp('exportButton').setDisabled(success === false);
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
        }
    });

})();