Ext.define('ProjectModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'start', type: 'string' },
        { name: 'end', type: 'string' },
        { name: 'responsible', type: 'string' },
        { name: 'contact', type: 'string' },
        { name: 'company', type: 'string' },
        { name: 'creator', type: 'string' },
        { name: 'createdtime', type: 'string' },
        { name: 'comment', type: 'string' },
        { name: 'enabled', type: 'boolean' }
    ],
    idProperty: 'id'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'ProjectModel',
    proxy: {
        type: 'ajax',
        url: '/Project/GetProjects',
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

var saveWnd = Ext.create('Ext.window.Window', {
    title: 'Project',
    height: 400,
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
        defaultType: 'textfield',
        fieldDefaults: {
            labelWidth: 60,
            labelAlign: 'left',
            margin: '15 15 15 15',
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
                        items: [
                            {
                                name: 'id',
                                xtype: 'textfield',
                                fieldLabel: '工程标识',
                                allowBlank: false,
                                readOnly: true
                            },
                            {
                                name: 'start',
                                xtype: 'datefield',
                                fieldLabel: '开始时间',
                                allowBlank: false,
                                editable: false
                            },
                            {
                                name: 'responsible',
                                xtype: 'textfield',
                                fieldLabel: '负责人员',
                                allowBlank: false
                            },
                            {
                                name: 'comment',
                                xtype: 'textareafield',
                                fieldLabel: '备注信息',
                                height: 100
                            },
                        ]
                    },
                    {
                        xtype: 'container',
                        flex: 1,
                        layout: 'anchor',
                        items: [
                            {
                                id: 'name',
                                name: 'name',
                                xtype: 'textfield',
                                fieldLabel: '工程名称',
                                allowBlank: false
                            },
                            {
                                name: 'end',
                                xtype: 'datefield',
                                fieldLabel: '结束时间',
                                allowBlank: false,
                                editable: false
                            },
                            {
                                name: 'contact',
                                xtype: 'textfield',
                                fieldLabel: '联系电话',
                                allowBlank: false
                            },
                            {
                                name: 'company',
                                xtype: 'textfield',
                                fieldLabel: '施工公司',
                                allowBlank: false
                            },
                            {
                                name: 'enabled',
                                xtype: 'checkboxfield',
                                fieldLabel: '工程状态',
                                boxLabel: '(勾选表示启用)',
                                inputValue: true,
                                checked: true
                            }
                        ]
                    }
                ]
            }
        ]
    }],
    buttons: [
      { id: 'saveResult', xtype: 'iconlabel', text: '' },
      { xtype: 'tbfill' },
      {
          xtype: 'button', text: '保存', handler: function (el, e) {
              var form = Ext.getCmp('saveForm').getForm(),
                  result = Ext.getCmp('saveResult');

              result.setTextWithIcon('', '');
              if (form.isValid()) {
                  result.setTextWithIcon('正在处理...', 'x-icon-loading');
                  form.submit({
                      submitEmptyText: false,
                      clientValidation: true,
                      preventWindow: true,
                      url: '/Project/SaveProject',
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
                          var message = 'undefined error.';
                          if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                              message = action.result.message;

                          result.setTextWithIcon(message, 'x-icon-error');
                      }
                  });
              }
          }
      },
      {
          xtype: 'button', text: '关闭', handler: function (el, e) {
              saveWnd.close();
          }
      }
    ]
});

var editCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    Ext.getCmp('saveForm').getForm().load({
        url: '/Project/GetProject',
        params: { id: record.raw.id, action: $$iPems.Action.Edit },
        waitMsg: '正在处理...',
        waitTitle: '系统提示',
        success: function (form, action) {
            form.clearInvalid();
            Ext.getCmp('name').setReadOnly(true);
            Ext.getCmp('saveResult').setTextWithIcon('', '');

            saveWnd.setGlyph(0xf002);
            saveWnd.setTitle('编辑工程');
            saveWnd.opaction = $$iPems.Action.Edit;
            saveWnd.show();
        }
    });
};

var currentPanel = Ext.create("Ext.grid.Panel", {
    glyph: 0xf046,
    title: '工程信息管理',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: false,
    listeners: {},
    viewConfig: {
        forceFit: false,
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
        text: '工程名称',
        dataIndex: 'name',
        align: 'left',
        width: 200
    }, {
        text: '开始时间',
        dataIndex: 'start',
        align: 'center',
        width: 150
    }, {
        text: '结束时间',
        dataIndex: 'end',
        align: 'center',
        width: 150
    }, {
        text: '负责人员',
        dataIndex: 'responsible',
        align: 'center',
        sortable: true
    }, {
        text: '联系电话',
        dataIndex: 'contact',
        align: 'center',
        sortable: true
    }, {
        text: '施工公司',
        dataIndex: 'company',
        align: 'left',
        sortable: true
    }, {
        text: '创建人员',
        dataIndex: 'creator',
        align: 'center',
        sortable: true
    }, {
        text: '创建时间',
        dataIndex: 'createdtime',
        align: 'center',
        width: 150
    }, {
        text: '备注信息',
        dataIndex: 'comment',
        align: 'left',
        sortable: true
    }, {
        text: '工程状态',
        dataIndex: 'enabled',
        align: 'center',
        renderer: function (value) {
            return value ? '有效' : '禁用';
        }
    }, {
        xtype: 'actioncolumn',
        width: 100,
        align: 'center',
        menuDisabled: true,
        menuText: '操作',
        text: '操作',
        items: [{
            getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                return (r.get('creator') === $$iPems.currentEmployee) ? 'x-cell-icon x-icon-edit' : 'x-cell-icon x-icon-hidden';
            },
            handler: function (grid, rowIndex, colIndex) {
                var record = grid.getStore().getAt(rowIndex);
                if (Ext.isEmpty(record)) return false;

                editCellClick(grid, rowIndex, colIndex);
            }
        }]
    }],
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        items: [
            Ext.create('Ext.toolbar.Toolbar', {
                border: false,
                items: [Ext.create('Ext.form.TextField', {
                    id: 'names-textfield',
                    fieldLabel: '工程名称',
                    labelWidth: 60,
                    width: 508,
                    maxLength: 100,
                    emptyText: '多条件请以;分隔，例: A;B;C'
                }), {
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
            Ext.create('Ext.toolbar.Toolbar', {
                border: false,
                items: [{
                    id: 'start-datefield',
                    xtype: 'datefield',
                    fieldLabel: '开始时间',
                    labelWidth: 60,
                    width: 250,
                    value: Ext.Date.getFirstDateOfMonth(new Date()),
                    editable: false,
                    allowBlank: false
                }, {
                    id: 'end-datefield',
                    xtype: 'datefield',
                    fieldLabel: '结束时间',
                    labelWidth: 60,
                    value: Ext.Date.subtract(Ext.Date.add(Ext.Date.getFirstDateOfMonth(new Date()), Ext.Date.MONTH, +1), Ext.Date.SECOND, 1),
                    width: 250,
                    editable: false,
                    allowBlank: false
                }, {
                    xtype: 'button',
                    text: '新增工程',
                    glyph: 0xf001,
                    handler: function (el, e) {
                        var form = Ext.getCmp('saveForm').getForm();
                        form.load({
                            url: '/Project/GetProject',
                            params: { id: '', action: $$iPems.Action.Add },
                            waitMsg: '正在处理...',
                            waitTitle: '系统提示',
                            success: function (form, action) {
                                form.clearInvalid();
                                Ext.getCmp('name').setReadOnly(false);
                                Ext.getCmp('saveResult').setTextWithIcon('', '');

                                saveWnd.setGlyph(0xf001);
                                saveWnd.setTitle('新增工程');
                                saveWnd.opaction = $$iPems.Action.Add;
                                saveWnd.show();
                            }
                        });
                    }
                }]
            })
        ]
    }],
    bbar: currentPagingToolbar
});

var query = function () {
    var namesfield = Ext.getCmp('names-textfield'),
        startDate = Ext.getCmp('start-datefield'),
        endDate = Ext.getCmp('end-datefield');

    var me = currentStore, proxy = me.getProxy();
    proxy.extraParams.name = namesfield.getRawValue();
    proxy.extraParams.startDate = startDate.getRawValue();
    proxy.extraParams.endDate = endDate.getRawValue();
    proxy.extraParams.cache = false;
    me.loadPage(1, {
        callback: function (records, operation, success) {
            proxy.extraParams.cache = success;
            Ext.getCmp('exportButton').setDisabled(success === false);
        }
    });
};

var print = function () {
    $$iPems.download({
        url: '/Project/DownloadProjects',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
    }
});