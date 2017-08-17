(function () {
	Ext.define('NoticeModel', {
		extend: 'Ext.data.Model',
		fields: [
			{ name: 'index', type: 'int' },
            { name: 'id', type: 'string' },
            { name: 'title', type: 'string' },
            { name: 'content', type: 'string' },
            { name: 'created', type: 'string' },
			{ name: 'enabled', type: 'boolean' }
		],
		idProperty: 'id'
	});

	var currentStore = Ext.create('Ext.data.Store', {
		autoLoad: false,
		pageSize: 20,
		model: 'NoticeModel',
		proxy: {
			type: 'ajax',
			url: '/Account/GetNotices',
			reader: {
				type: 'json',
				successProperty: 'success',
				messageProperty: 'message',
				totalProperty: 'total',
				root: 'data'
			},
			extraParams: {
				startDate: '',
				endDate: ''
			},
			listeners: {
			    exception: function (proxy, response, operation) {
			        Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
			    }
			},
			simpleSortMode: true
		}
	});

	var noticeRoleStore = Ext.create('Ext.data.Store', {
		autoLoad: false,
		pageSize: 1024,
		fields: [
			{ name: 'id', type: 'string' },
			{ name: 'text', type: 'string' },
			{ name: 'comment', type: 'string' }
		],
		proxy: {
			type: 'ajax',
			url: '/Account/GetComboRoles',
			reader: {
				type: 'json',
				successProperty: 'success',
				messageProperty: 'message',
				totalProperty: 'total',
				root: 'data'
			}
		}
	});

	var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

	var saveWnd = Ext.create('Ext.window.Window', {
		title: 'Detail',
		glyph: 0xf025,
		height: 400,
		width: 500,
		modal: true,
		border: false,
		hidden: true,
		closeAction: 'hide',
		opaction: $$iPems.Action.Add,
		items: [{
			xtype: 'form',
			itemId: 'saveForm',
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
					itemId: 'id',
					name: 'id',
					xtype: 'hidden',
					value: ''
				},
				{
					itemId: 'title',
					name: 'title',
					xtype: 'textfield',
					fieldLabel: '消息标题',
					maxLength: 256,
				    enforceMaxLength:true,
					allowBlank: false
				},
				{
					itemId: 'content',
					name: 'content',
					xtype: 'textareafield',
					autoScroll: true,
					height:150,
					fieldLabel: '详细信息',
					allowBlank: false
				}, Ext.create('Ext.ux.MultiCombo', {
					itemId: 'toRoles',
					fieldLabel: '广播对象',
					valueField: 'id',
					displayField: 'text',
					delimiter: $$iPems.Delimiter,
					queryMode: 'local',
					triggerAction: 'all',
					selectionMode: 'all',
					emptyText: '默认全部',
					forceSelection: true,
					store: noticeRoleStore,
				    submitValue: false
				}), {
					itemId: 'enabled',
					name: 'enabled',
					xtype: 'checkboxfield',
					fieldLabel: '消息状态',
					boxLabel: '(勾选表示启用)',
					inputValue: true,
					checked: false
				}
			]
		}],
		buttonAlign: 'right',
		buttons: [
			{ id: 'saveResult', xtype: 'iconlabel', text: '' },
			{ xtype: 'tbfill' },
			{
				xtype: 'button',
				text: '保存',
				handler: function (el, e) {
					var form = saveWnd.getComponent('saveForm'),
						baseForm = form.getForm(),
						saveResult = Ext.getCmp('saveResult');

					saveResult.setTextWithIcon('', '');
					if (baseForm.isValid()) {
						saveResult.setTextWithIcon('正在处理...', 'x-icon-loading');

						var toRoles = form.getComponent('toRoles'),
							roles = toRoles.getSelectedValues();

						baseForm.submit({
						    clientValidation: true,
						    submitEmptyText: false,
							preventWindow: true,
							url: '/Account/SaveNotice',
							params: {
								action: saveWnd.opaction,
								roles: roles
							},
							success: function (form, action) {
								saveResult.setTextWithIcon(action.result.message, 'x-icon-accept');
								if (saveWnd.opaction == $$iPems.Action.Add)
									currentStore.loadPage(1);
								else
									currentPagingToolbar.doRefresh();
							},
							failure: function (form, action) {
								var message = 'undefined error.';
								if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
									message = action.result.message;

								saveResult.setTextWithIcon(message, 'x-icon-error');
							}
						});
					} else {
						saveResult.setTextWithIcon('表单填写错误', 'x-icon-error');
					}
				}
			}, {
				xtype: 'button',
				text: '关闭',
				handler: function (el, e) {
					saveWnd.hide();
				}
			}
		]
	});

	var editCellClick = function (grid, rowIndex, colIndex) {
		var record = grid.getStore().getAt(rowIndex);
		if (Ext.isEmpty(record)) return false;

		var basic = saveWnd.getComponent('saveForm').getForm();
		basic.load({
			url: '/Account/GetNotice',
			params: { id: record.raw.id, action: $$iPems.Action.Edit },
			waitMsg: '正在处理...',
			waitTitle: '系统提示',
			success: function (form, action) {
			    form.clearInvalid();
			    Ext.getCmp('saveResult').setTextWithIcon('', '');

				saveWnd.setTitle('编辑消息');
				saveWnd.opaction = $$iPems.Action.Edit;
				saveWnd.show();
			}
		});
	};

	var deleteCellClick = function (grid, rowIndex, colIndex) {
		var record = grid.getStore().getAt(rowIndex);
		if (Ext.isEmpty(record)) return false;

		Ext.Msg.confirm('确认对话框', '您确认要删除吗？', function (buttonId, text) {
			if (buttonId === 'yes') {
				Ext.Ajax.request({
					url: '/Account/DeleteNotice',
					params: { id: record.raw.id },
					mask: new Ext.LoadMask(grid, { msg: '正在处理...' }),
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

	var currentGridPanel = Ext.create('Ext.grid.Panel', {
	    glyph: 0xf025,
	    title: '系统消息',
		region: 'center',
		store: currentStore,
		columnLines: true,
		disableSelection: false,
		loadMask: true,
		forceFit: false,
		listeners: {},
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
		    text: '消息标题',
			dataIndex: 'title',
			width: 150,
			align: 'left',
			sortable: true
		}, {
		    text: '详细信息',
			dataIndex: 'content',
			flex: 1,
			align: 'left',
			sortable: false,
			renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
				metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
				return value;
			}
		}, {
		    text: '消息日期',
			dataIndex: 'created',
			width: 150,
			align: 'center',
			sortable: true
		}, {
		    text: '消息状态',
			dataIndex: 'enabled',
			width: 80,
			align: 'center',
			sortable: true,
			renderer: function (value) { return value ? '有效' : '禁用'; }
		}, {
			xtype: 'actioncolumn',
			width: 80,
			align: 'center',
			menuDisabled: true,
			menuText: '操作',
			text: '操作',
			items: [{
			    iconCls: 'x-cell-icon x-icon-edit',
				handler: function (grid, rowIndex, colIndex) {
					editCellClick(grid, rowIndex, colIndex);
				}
			}, {
				iconCls: 'x-cell-icon x-icon-delete',
				handler: function (grid, rowIndex, colIndex) {
					deleteCellClick(grid, rowIndex, colIndex);
				}
			}]
		}],
		tbar: Ext.create('Ext.toolbar.Toolbar', {
			items: [{
				xtype: 'button',
				text: '新增消息',
				glyph: 0xf001,
				handler: function (el, e) {
					var basic = saveWnd.getComponent('saveForm').getForm();
					basic.load({
						url: '/Account/GetNotice',
						params: { id: '', action: $$iPems.Action.Add },
						waitMsg: '正在处理...',
						waitTitle: '系统提示',
						success: function (form, action) {
						    form.clearInvalid();
						    Ext.getCmp('saveResult').setTextWithIcon('', '');

							saveWnd.setTitle('新增消息');
							saveWnd.opaction = $$iPems.Action.Add;
							saveWnd.show();
						}
					});
				}
			}, '-', {
				id: 'begin-datefield',
				xtype: 'datefield',
				fieldLabel: '开始日期',
				labelWidth: 60,
				width: 250,
				value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
				editable: false,
				allowBlank: false
			}, {
				id: 'end-datefield',
				xtype: 'datefield',
				fieldLabel: '结束日期',
				labelWidth: 60,
				width: 250,
				value: new Date(),
				editable: false,
				allowBlank: false
			}, {
				xtype: 'button',
				text: '数据查询',
				glyph: 0xf005,
				handler: function (el, e) {
				    query();
				}
			}]
		}),
		bbar: currentPagingToolbar
	});

	var query = function () {
	    var startDate = Ext.getCmp('begin-datefield').getRawValue(),
            endDate = Ext.getCmp('end-datefield').getRawValue();

	    var me = currentStore, proxy = me.getProxy();
	    proxy.extraParams.startDate = startDate;
	    proxy.extraParams.endDate = endDate;
	    me.loadPage(1);
	};

	Ext.onReady(function () {
		/*add components to viewport panel*/
		var pageContentPanel = Ext.getCmp('center-content-panel-fw');
		if (!Ext.isEmpty(pageContentPanel)) {
			pageContentPanel.add(currentGridPanel);

			//load store data
			noticeRoleStore.load();
			Ext.defer(query, 2000);
		}
	});
})();