Ext.define('ProjectModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'Index', type: 'int' },
        { name: 'Id', type: 'string' },
        { name: 'Name', type: 'string' },
        { name: 'StartTime', type: 'string' },
        { name: 'EndTime', type: 'string' },
        { name: 'Responsible', type: 'string' },
        { name: 'ContactPhone', type: 'string' },
        { name: 'Company', type: 'string' },
        { name: 'Creator', type: 'string' },
        { name: 'CreatedTime', type: 'string' },
        { name: 'Comment', type: 'string' },
        { name: 'Enabled', type: 'boolean' }
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
                                id: 'Id',
                                name: 'Id',
                                xtype: 'textfield',
                                fieldLabel: '工程标识',
                                allowBlank: false,
                                readOnly: true
                            },
                            {
                                id: 'StartTime',
                                name: 'StartTime',
                                xtype: 'datefield',
                                fieldLabel: '开始时间',
                                allowBlank: false,
                                editable: false
                            },
                            {
                                id: 'Responsible',
                                name: 'Responsible',
                                xtype: 'textfield',
                                fieldLabel: '负责人员',
                                allowBlank: false
                            },
                            {
                                id: 'Comment',
                                name: 'Comment',
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
                                id: 'Name',
                                name: 'Name',
                                xtype: 'textfield',
                                fieldLabel: '工程名称',
                                allowBlank: false
                            },
                            {
                                id: 'EndTime',
                                name: 'EndTime',
                                xtype: 'datefield',
                                fieldLabel: '结束时间',
                                allowBlank: false,
                                editable: false
                            },
                            {
                                id: 'ContactPhone',
                                name: 'ContactPhone',
                                xtype: 'textfield',
                                fieldLabel: '联系电话',
                                allowBlank: false
                            },
                            {
                                id: 'Company',
                                name: 'Company',
                                xtype: 'textfield',
                                fieldLabel: '施工公司',
                                allowBlank: false
                            },
                            {
                                id: 'Enabled',
                                name: 'Enabled',
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
                  result.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
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
                              currentStore.loadPage(1);
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
              } else {
                  result.setTextWithIcon('表单填写错误', 'x-icon-error');
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
        params: { id: record.raw.Id, action: $$iPems.Action.Edit },
        waitMsg: '正在处理，请稍后...',
        waitTitle: '系统提示',
        success: function (form, action) {
            Ext.getCmp('Name').setReadOnly(true);
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
    title: '工程管理信息',
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
        dataIndex: 'Index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: '工程标识',
        name: 'Id',
        dataIndex: 'Id',
        width: 100,
        align: 'left',
        sortable: false
    }, {
        text: '工程名称',
        dataIndex: 'Name',
        width: 100,
        align: 'left',
        sortable: false
    }, {
        text: '开始时间',
        dataIndex: 'StartTime',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: '结束时间',
        dataIndex: 'EndTime',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: '负责人员',
        dataIndex: 'Responsible',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '联系电话',
        dataIndex: 'ContactPhone',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '施工公司',
        dataIndex: 'Company',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '创建人员',
        dataIndex: 'Creator',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '创建时间',
        dataIndex: 'CreatedTime',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '备注信息',
        dataIndex: 'Comment',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '工程状态',
        dataIndex: 'Enabled',
        width: 60,
        align: 'left',
        sortable: true,
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
                return (r.get('Creator') === $$iPems.currentEmployee) ? 'x-cell-icon x-icon-edit' : 'x-cell-icon x-icon-hidden';
            },
            handler: function (grid, rowIndex, colIndex) {
                var record = grid.getStore().getAt(rowIndex);
                if (Ext.isEmpty(record)) return false;

                if (record.raw.Creator === $$iPems.currentEmployee)
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
                        var namesfield = Ext.getCmp('names-textfield');
                        var startTime = Ext.getCmp('begin-datefield');
                        var endTime = Ext.getCmp('end-datefield');

                        currentStore.getProxy().extraParams.name = namesfield.getRawValue();
                        currentStore.getProxy().extraParams.startTime = startTime.getRawValue();
                        currentStore.getProxy().extraParams.endTime = endTime.getRawValue();
                        currentStore.loadPage(1);
                    }
                }, '-', {
                    xtype: 'button',
                    text: '数据导出',
                    glyph: 0xf010,
                    handler: function (el, e) {
                        $$iPems.download({ url: '/Project/DownloadProjects', params: { Name: currentStore.getProxy().extraParams.Name, StartTime: currentStore.getProxy().extraParams.StartTime, endTime: currentStore.getProxy().extraParams.endTime } });
                    }
                }]
            }),
            Ext.create('Ext.toolbar.Toolbar', {
                border: false,
                items: [{
                    id: 'begin-datefield',
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
                            waitMsg: '正在处理，请稍后...',
                            waitTitle: '系统提示',
                            success: function (form, action) {
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
})

Ext.onReady(function () {
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentPanel);
        currentStore.load();
    }
});