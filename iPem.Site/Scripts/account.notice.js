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
				begin: '',
				end: ''
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
					fieldLabel: $$iPems.lang.Notice.Window.Title,
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
					fieldLabel: $$iPems.lang.Notice.Window.Content,
					allowBlank: false
				}, Ext.create('Ext.ux.MultiCombo', {
					itemId: 'toRoles',
					fieldLabel: $$iPems.lang.Notice.Window.Role,
					valueField: 'id',
					displayField: 'text',
					delimiter: $$iPems.Delimiter,
					queryMode: 'local',
					triggerAction: 'all',
					selectionMode: 'all',
					emptyText: $$iPems.lang.AllEmptyText,
					forceSelection: true,
					store: noticeRoleStore,
				    submitValue: false
				}), {
					itemId: 'enabled',
					name: 'enabled',
					xtype: 'checkboxfield',
					fieldLabel: $$iPems.lang.Notice.Window.Enabled,
					boxLabel: $$iPems.lang.Notice.Window.EnabledLabel,
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
				text: $$iPems.lang.Save,
				handler: function (el, e) {
					var form = saveWnd.getComponent('saveForm'),
						baseForm = form.getForm(),
						saveResult = Ext.getCmp('saveResult');

					saveResult.setTextWithIcon('', '');
					if (baseForm.isValid()) {
						saveResult.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');

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
						saveResult.setTextWithIcon($$iPems.lang.FormError, 'x-icon-error');
					}
				}
			}, {
				xtype: 'button',
				text: $$iPems.lang.Close,
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
			waitMsg: $$iPems.lang.AjaxHandling,
			waitTitle: $$iPems.lang.SysTipTitle,
			success: function (form, action) {
			    form.clearInvalid();

				Ext.getCmp('saveResult').setTextWithIcon('', '');
				saveWnd.setTitle($$iPems.lang.Notice.Window.EditTitle);
				saveWnd.opaction = $$iPems.Action.Edit;
				saveWnd.show();
			}
		});
	};

	var deleteCellClick = function (grid, rowIndex, colIndex) {
		var record = grid.getStore().getAt(rowIndex);
		if (Ext.isEmpty(record)) return false;

		Ext.Msg.confirm($$iPems.lang.ConfirmWndTitle, $$iPems.lang.ConfirmDelete, function (buttonId, text) {
			if (buttonId === 'yes') {
				Ext.Ajax.request({
					url: '/Account/DeleteNotice',
					params: { id: record.raw.id },
					mask: new Ext.LoadMask(grid, { msg: $$iPems.lang.AjaxHandling }),
					success: function (response, options) {
						var data = Ext.decode(response.responseText, true);
						if (data.success)
							currentPagingToolbar.doRefresh();
						else
							Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
					}
				});
			}
		});
	};

	var currentGridPanel = Ext.create('Ext.grid.Panel', {
	    glyph: 0xf025,
		title: $$iPems.lang.Notice.Title,
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
			preserveScrollOnRefresh: true
		},
		columns: [{
		    text: $$iPems.lang.Notice.Columns.Index,
			dataIndex: 'index',
			width: 60,
			align: 'left',
			sortable: true
		}, {
		    text: $$iPems.lang.Notice.Columns.Title,
			dataIndex: 'title',
			width: 150,
			align: 'left',
			sortable: true
		}, {
		    text: $$iPems.lang.Notice.Columns.Content,
			dataIndex: 'content',
			flex: 1,
			align: 'left',
			sortable: false,
			renderer: function (value, metadata, record, rowIndex, columnIndex, store, view) {
				metadata.tdAttr = Ext.String.format("data-qtip='{0}'", value);
				return value;
			}
		}, {
		    text: $$iPems.lang.Notice.Columns.Created,
			dataIndex: 'created',
			width: 150,
			align: 'center',
			sortable: true
		}, {
		    text: $$iPems.lang.Notice.Columns.Enabled,
			dataIndex: 'enabled',
			width: 80,
			align: 'center',
			sortable: true,
			renderer: function (value) { return value ? $$iPems.lang.StatusTrue : $$iPems.lang.StatusFalse; }
		}, {
			xtype: 'actioncolumn',
			width: 80,
			align: 'center',
			menuDisabled: true,
			menuText: $$iPems.lang.Operate,
			text: $$iPems.lang.Operate,
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
				text: $$iPems.lang.Notice.ToolBar.Add,
				glyph: 0xf001,
				handler: function (el, e) {
					var basic = saveWnd.getComponent('saveForm').getForm();
					basic.load({
						url: '/Account/GetNotice',
						params: { id: '', action: $$iPems.Action.Add },
						waitMsg: $$iPems.lang.AjaxHandling,
						waitTitle: $$iPems.lang.SysTipTitle,
						success: function (form, action) {
						    form.clearInvalid();

							Ext.getCmp('saveResult').setTextWithIcon('', '');
							saveWnd.setTitle($$iPems.lang.Notice.Window.AddTitle);
							saveWnd.opaction = $$iPems.Action.Add;
							saveWnd.show();
						}
					});
				}
			}, '-', {
				id: 'begin-datefield',
				xtype: 'datefield',
				fieldLabel: $$iPems.lang.Notice.ToolBar.Begin,
				labelWidth: 60,
				width: 250,
				value: Ext.Date.add(new Date(), Ext.Date.DAY, -1),
				editable: false,
				allowBlank: false
			}, {
				id: 'end-datefield',
				xtype: 'datefield',
				fieldLabel: $$iPems.lang.Notice.ToolBar.End,
				labelWidth: 60,
				width: 250,
				value: new Date(),
				editable: false,
				allowBlank: false
			}, {
				id: 'query',
				xtype: 'button',
				text: $$iPems.lang.Query,
				glyph: 0xf005,
				listeners: {
					'click' : function(el, e) {
						var begin = Ext.getCmp('begin-datefield').getRawValue(),
							end = Ext.getCmp('end-datefield').getRawValue();

						currentStore.getProxy().extraParams.begin = begin;
						currentStore.getProxy().extraParams.end = end;
						currentStore.loadPage(1);
					}
				}
			}]
		}),
		bbar: currentPagingToolbar
	});

	Ext.onReady(function () {
		/*add components to viewport panel*/
		var pageContentPanel = Ext.getCmp('center-content-panel-fw');
		if (!Ext.isEmpty(pageContentPanel)) {
			pageContentPanel.add(currentGridPanel);

			//load store data
			Ext.getCmp('query').fireEvent('click');
			noticeRoleStore.load();
		}
	});
})();