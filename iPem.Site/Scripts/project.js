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
                                fieldLabel: $$iPems.lang.Project.Window.Id,
                                allowBlank: false,
                                readOnly: true
                            },
                            {
                                id: 'StartTime',
                                name: 'StartTime',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.Project.Window.StartTime,
                                allowBlank: false,
                                editable: false
                            },
                            {
                                id: 'Responsible',
                                name: 'Responsible',
                                xtype: 'textfield',
                                fieldLabel: $$iPems.lang.Project.Window.Responsible,
                                allowBlank: false
                            },
                            {
                                id: 'Comment',
                                name: 'Comment',
                                xtype: 'textareafield',
                                fieldLabel: $$iPems.lang.Project.Window.Comment,
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
                                fieldLabel: $$iPems.lang.Project.Window.Name,
                                allowBlank: false
                            },
                            {
                                id: 'EndTime',
                                name: 'EndTime',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.Project.Window.EndTime,
                                allowBlank: false,
                                editable: false
                            },
                            {
                                id: 'ContactPhone',
                                name: 'ContactPhone',
                                xtype: 'textfield',
                                fieldLabel: $$iPems.lang.Project.Window.ContactPhone,
                                allowBlank: false
                            },
                            {
                                id: 'Company',
                                name: 'Company',
                                xtype: 'textfield',
                                fieldLabel: $$iPems.lang.Project.Window.Company,
                                allowBlank: false
                            },
                            {
                                id: 'Enabled',
                                name: 'Enabled',
                                xtype: 'checkboxfield',
                                fieldLabel: $$iPems.lang.Project.Window.Enabled,
                                boxLabel: $$iPems.lang.Project.Window.EnabledLabel,
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
          xtype: 'button', text: $$iPems.lang.Save, handler: function (el, e) {
              var form = Ext.getCmp('saveForm').getForm(),
                  result = Ext.getCmp('saveResult');

              result.setTextWithIcon('', '');
              if (form.isValid()) {
                  result.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
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
                  result.setTextWithIcon($$iPems.lang.FormError, 'x-icon-error');
              }
          }
      },
      {
          xtype: 'button', text: $$iPems.lang.Close, handler: function (el, e) {
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
        waitMsg: $$iPems.lang.AjaxHandling,
        waitTitle: $$iPems.lang.SysTipTitle,
        success: function (form, action) {
            Ext.getCmp('Name').setReadOnly(true);
            Ext.getCmp('saveResult').setTextWithIcon('', '');
            saveWnd.setGlyph(0xf002);
            saveWnd.setTitle($$iPems.lang.Project.Window.EditTitle);
            saveWnd.opaction = $$iPems.Action.Edit;
            saveWnd.show();
        }
    });
};

var currentPanel = Ext.create("Ext.grid.Panel", {
    glyph: 0xf046,
    title: $$iPems.lang.Project.Title,
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
        emptyText: $$iPems.lang.GridEmptyText,
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: $$iPems.lang.Project.Columns.Index,
        dataIndex: 'Index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.Id,
        name: 'Id',
        dataIndex: 'Id',
        width: 100,
        align: 'left',
        sortable: false
    }, {
        text: $$iPems.lang.Project.Columns.Name,
        dataIndex: 'Name',
        width: 100,
        align: 'left',
        sortable: false
    }, {
        text: $$iPems.lang.Project.Columns.StartTime,
        dataIndex: 'StartTime',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.EndTime,
        dataIndex: 'EndTime',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.Responsible,
        dataIndex: 'Responsible',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.ContactPhone,
        dataIndex: 'ContactPhone',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.Company,
        dataIndex: 'Company',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.Creator,
        dataIndex: 'Creator',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.CreatedTime,
        dataIndex: 'CreatedTime',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.Comment,
        dataIndex: 'Comment',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.Enabled,
        dataIndex: 'Enabled',
        width: 60,
        align: 'left',
        sortable: true,
        renderer: function (value) {
            return value ? $$iPems.lang.StatusTrue : $$iPems.lang.StatusFalse;
        }
    }, {
        xtype: 'actioncolumn',
        width: 100,
        align: 'center',
        menuDisabled: true,
        menuText: $$iPems.lang.Operate,
        text: $$iPems.lang.Operate,
        items: [{
            getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                return (r.get('Creator') === $$iPems.associatedEmployee) ? 'x-cell-icon x-icon-edit' : 'x-cell-icon x-icon-hidden';
            },
            handler: function (grid, rowIndex, colIndex) {
                var record = grid.getStore().getAt(rowIndex);
                if (Ext.isEmpty(record)) return false;

                if (record.raw.Creator === $$iPems.associatedEmployee)
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
                    fieldLabel: $$iPems.lang.Project.ToolBar.ProjectName,
                    labelWidth: 60,
                    width: 508,
                    maxLength: 100,
                    emptyText: $$iPems.lang.MultiConditionEmptyText
                }), {
                    xtype: 'button',
                    text: $$iPems.lang.Query,
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
                    text: $$iPems.lang.Import,
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
                    fieldLabel: $$iPems.lang.Project.ToolBar.StartTime,
                    labelWidth: 60,
                    width: 250,
                    value: Ext.Date.getFirstDateOfMonth(new Date()),
                    editable: false,
                    allowBlank: false
                }, {
                    id: 'end-datefield',
                    xtype: 'datefield',
                    fieldLabel: $$iPems.lang.Project.ToolBar.EndTime,
                    labelWidth: 60,
                    value: Ext.Date.subtract(Ext.Date.add(Ext.Date.getFirstDateOfMonth(new Date()), Ext.Date.MONTH, +1), Ext.Date.SECOND, 1),
                    width: 250,
                    editable: false,
                    allowBlank: false
                }, {
                    xtype: 'button',
                    text: $$iPems.lang.Project.Window.AddTitle,
                    glyph: 0xf001,
                    handler: function (el, e) {
                        var form = Ext.getCmp('saveForm').getForm();
                        form.load({
                            url: '/Project/GetProject',
                            params: { id: '', action: $$iPems.Action.Add },
                            waitMsg: $$iPems.lang.AjaxHandling,
                            waitTitle: $$iPems.lang.SysTipTitle,
                            success: function (form, action) {
                                Ext.getCmp('saveResult').setTextWithIcon('', '');
                                saveWnd.setGlyph(0xf001);
                                saveWnd.setTitle($$iPems.lang.Project.Window.AddTitle);
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