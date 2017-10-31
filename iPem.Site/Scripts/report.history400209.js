(function () {
    Ext.define("CardRecordModel", {
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
            { name: 'remark', type: 'string' },
            { name: 'employeeCode', type: 'string' },
            { name: 'employeeName', type: 'string' },
            { name: 'employeeType', type: 'string' },
            { name: 'department', type: 'string' }
        ],
        idProperty: 'index'
    });

    var keyTypeStore = new Ext.data.ArrayStore({
        fields: ['id', 'text'],
        data: [[1, '按卡号查询'], [2, '按工号查询'], [3, '按姓名查询']]
    });

    var recTypeStore = new Ext.data.ArrayStore({
        fields: ['id', 'text'],
        data: [[0, '正常开门'],[1, '非法开门'],[2, '远程开门'],[-1, '其他记录']]
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'CardRecordModel',
        proxy: {
            type: 'ajax',
            url: '/Report/RequestHistory400209',
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
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var currentLayout = Ext.create('Ext.grid.Panel', {
        glyph: 0xf029,
        title: '刷卡记录统计',
        region: 'center',
        store: currentStore,
        bbar: currentPagingToolbar,
        viewConfig: {
            loadMask: true,
            stripeRows: true,
            trackOver: true,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
        },
        columns: [
            { text: '序号', dataIndex: 'index', width: 60 },
            { text: '所属区域', dataIndex: 'area' },
            { text: '所属站点', dataIndex: 'station' },
            { text: '所属机房', dataIndex: 'room' },
            { text: '设备名称', dataIndex: 'device' },
            { text: '刷卡类型', dataIndex: 'recType' },
            { text: '刷卡描述', dataIndex: 'remark', width: 150 },
            { text: '刷卡卡号', dataIndex: 'decimalCard' },
            { text: '刷卡时间', dataIndex: 'time', width: 150 },
            { text: '刷卡人员', dataIndex: 'employeeName' },
            { text: '人员类型', dataIndex: 'employeeType' },
            { text: '所属部门', dataIndex: 'department' }
        ],
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
                        },
                    ]
                },
                {
                    xtype: 'toolbar',
                    border: false,
                    items: [
                        {
                            id: 'recTypeField',
                            xtype: 'multicombo',
                            fieldLabel: '刷卡类型',
                            store: recTypeStore,
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
                            id: 'keyTypeField',
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
                                print();
                            }
                        }
                    ]
                }
            ]
        }]
    });

    var query = function () {
        var parent = Ext.getCmp('rangePicker').getValue(),
            startDate = Ext.getCmp('startField').getRawValue(),
            endDate = Ext.getCmp('endField').getRawValue(),
            recTypes = Ext.getCmp('recTypeField').getValue(),
            keyType = Ext.getCmp('keyTypeField').getValue(),
            keyWords = Ext.getCmp('keysField').getRawValue(),
            proxy = currentStore.getProxy();

        proxy.extraParams.parent = parent;
        proxy.extraParams.startDate = startDate;
        proxy.extraParams.endDate = endDate;
        proxy.extraParams.recTypes = recTypes;
        proxy.extraParams.keyType = keyType;
        proxy.extraParams.keyWords = keyWords;
        proxy.extraParams.cache = false;
        currentStore.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
                Ext.getCmp('exportButton').setDisabled(success === false);
            }
        });
    };

    var print = function () {
        $$iPems.download({
            url: '/Report/DownloadHistory400209',
            params: currentStore.getProxy().extraParams
        });
    };

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

})();