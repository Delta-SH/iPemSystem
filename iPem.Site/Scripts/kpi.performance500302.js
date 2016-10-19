Ext.define('ReportModel', {
	extend: 'Ext.data.Model',
	fields: [
        { name: 'index', type: 'int' },
        { name: 'name', type: 'string' },
        { name: 'kt', type: 'float' },
        { name: 'zm', type: 'float' },
        { name: 'bg', type: 'float' },
        { name: 'sb', type: 'float' },
        { name: 'kgdy', type: 'float' },
        { name: 'ups', type: 'float' },
        { name: 'qt', type: 'float' },
        { name: 'zl', type: 'float' },
        { name: 'ktrate', type: 'string' },
        { name: 'zmrate', type: 'string' },
        { name: 'bgrate', type: 'string' },
        { name: 'sbrate', type: 'string' },
        { name: 'kgdyrate', type: 'string' },
        { name: 'upsrate', type: 'string' },
        { name: 'qtrate', type: 'string' }
	],
	idProperty: 'index'
});

var query = function (store) {
	var range = Ext.getCmp('rangePicker'),
        types = Ext.getCmp('stationTypeMultiCombo'),
        points = Ext.getCmp('pointMultiPicker'),
        start = Ext.getCmp('startField'),
        end = Ext.getCmp('endField');

	if (!range.isValid()) return;
	if (!points.isValid()) return;
	if (!start.isValid()) return;
	if (!end.isValid()) return;

	store.proxy.extraParams.parent = range.getValue();
	store.proxy.extraParams.types = types.getValue();
	store.proxy.extraParams.points = points.getValue();
	store.proxy.extraParams.startDate = start.getRawValue();
	store.proxy.extraParams.endDate = end.getRawValue();
	store.loadPage(1);
};

var print = function (store) {
	$$iPems.download({
		url: '/KPI/Download500301',
		params: store.proxy.extraParams
	});
};

var currentStore = Ext.create('Ext.data.Store', {
	autoLoad: false,
	pageSize: 20,
	model: 'ReportModel',
	proxy: {
		type: 'ajax',
		url: '/KPI/Request500301',
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

var currentPanel = Ext.create("Ext.grid.Panel", {
	glyph: 0xf029,
	title: '能耗分类统计',
	region: 'center',
	store: currentStore,
	columnLines: true,
	disableSelection: false,
	viewConfig: {
		loadMask: true,
		trackOver: true,
		stripeRows: true,
		emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
	},
	columns: [{
		text: '序号',
		dataIndex: 'index',
		width: 60,
		align: 'left',
		sortable: true
	}, {
		text: '名称',
		dataIndex: 'name',
		align: 'left',
		flex: 1,
		sortable: true
	}, {
		text: '空调能耗(kWh)',
		dataIndex: 'kt',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '照明能耗(kWh)',
		dataIndex: 'zm',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '办公能耗(kWh)',
		dataIndex: 'bg',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '设备能耗(kWh)',
		dataIndex: 'sb',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '开关电源能耗(kWh)',
		dataIndex: 'kgdy',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: 'UPS能耗(kWh)',
		dataIndex: 'ups',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '其他能耗(kWh)',
		dataIndex: 'qt',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '总能耗(kWh)',
		dataIndex: 'zl',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '空调能耗占比',
		dataIndex: 'kt',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '照明能耗占比',
		dataIndex: 'zm',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '办公能耗占比',
		dataIndex: 'bg',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '设备能耗占比',
		dataIndex: 'sb',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '开关电源能耗占比',
		dataIndex: 'kgdy',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: 'UPS能耗占比',
		dataIndex: 'ups',
		width: 150,
		align: 'left',
		sortable: true
	}, {
		text: '其他能耗占比',
		dataIndex: 'qt',
		width: 150,
		align: 'left',
		sortable: true
	}],
	dockedItems: [{
		xtype: 'panel',
		dock: 'top',
		items: [{
			xtype: 'toolbar',
			border: false,
			items: [{
				id: 'rangePicker',
				xtype: 'AreaPicker',
				selectAll: true,
				allowBlank: false,
				emptyText: '请选择查询范围...',
				fieldLabel: '查询范围',
				width: 280,
			}, {
				id: 'countCombo',
				xtype: 'combobox',
				fieldLabel: '统计粒度',
				displayField: 'text',
				valueField: 'id',
				typeAhead: true,
				queryMode: 'local',
				triggerAction: 'all',
				selectOnFocus: true,
				forceSelection: true,
				labelWidth: 60,
				width: 280,
				value: $$iPems.Organization.Area,
				store: Ext.create('Ext.data.Store', {
					fields: ['id', 'text'],
					data: [
                        { id: $$iPems.Organization.Area, text: '区域' },
                        { id: $$iPems.Organization.Station, text: '站点' },
                        { id: $$iPems.Organization.Room, text: '机房' },
					]
				})
			}, {
				xtype: 'button',
				glyph: 0xf005,
				text: '数据查询',
				handler: function (me, event) {
					query(currentStore);
				}
			}]
		}, {
			xtype: 'toolbar',
			border: false,
			items: [{
				id: 'startField',
				xtype: 'datefield',
				fieldLabel: '开始时间',
				labelWidth: 60,
				width: 280,
				value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
				editable: false,
				allowBlank: false
			}, {
				id: 'endField',
				xtype: 'datefield',
				fieldLabel: '结束时间',
				labelWidth: 60,
				width: 280,
				value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
				editable: false,
				allowBlank: false
			}, {
				xtype: 'button',
				glyph: 0xf010,
				text: '数据导出',
				handler: function (me, event) {
					print(currentStore);
				}
			}]
		}]
	}],
	bbar: currentPagingToolbar
})

Ext.onReady(function () {
	var pageContentPanel = Ext.getCmp('center-content-panel-fw');
	if (!Ext.isEmpty(pageContentPanel)) {
		pageContentPanel.add(currentPanel);
	}
});