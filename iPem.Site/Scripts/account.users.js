Ext.ns('Ext.app');
Ext.app.REMOTING_API = { url: '/Account/Router', type: 'remoting', actions: { user: [{ name: 'query', len: 0 }] } };
Ext.direct.Manager.addProvider(Ext.app.REMOTING_API);

Ext.define('UserModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'id', type: 'string' },
        { name: 'uid', type: 'string' },
        { name: 'roleId', type: 'string' },
        { name: 'roleName', type: 'string' },
        { name: 'empId', type: 'string' },
        { name: 'empName', type: 'string' },
        { name: 'empNo', type: 'string' },
        { name: 'sex', type: 'int' },
        { name: 'sexName', type: 'string' },
        { name: 'mobile', type: 'string' },
        { name: 'email', type: 'string' },
        { name: 'password', type: 'string' },
        { name: 'created', type: 'string' },
        { name: 'limited', type: 'string' },
        { name: 'lastLogined', type: 'string' },
        { name: 'lastPasswordChanged', type: 'string' },
        { name: 'isLockedOut', type: 'boolean' },
        { name: 'lastLockedout', type: 'string' },
        { name: 'comment', type: 'string' },
        { name: 'enabled', type: 'boolean' }
    ],
    idProperty: 'id'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'UserModel',
    proxy: {
        type: 'ajax',
        url: '/Account/GetUsers',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        },
        extraParams: {
            rids: [],
            names: []
        },
        simpleSortMode: true
    }
});

var comboRoleStore = Ext.create('Ext.data.Store',{
    autoLoad: false,
    pageSize: 1024,
    fields: [{ name: 'id', type: 'string' }, { name: 'text', type: 'string' }, { name: 'comment', type: 'string' }],
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
    },
    listeners: {
        load: function (store, records, successful) {
            if (successful 
                && !Ext.isEmpty(records) 
                && records.length > 0)
                Ext.getCmp('roleId').select(records[0]);
        }
    }
});

var multiComboRoleStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 1024,
    fields: [{ name: 'id', type: 'string' }, { name: 'text', type: 'string' }, { name: 'comment', type: 'string' }],
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

Ext.apply(Ext.form.VTypes, {
    password: function (val, field) { 
        if (field.confirmTo) {
            var pwd = Ext.getCmp(field.confirmTo);
            return (val == pwd.getValue());
        }
        return true;
    }
});

var saveWnd = Ext.create('Ext.window.Window', {
    title: 'User',
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
                                id: 'id',
                                name: 'id',
                                xtype: 'textfield',
                                fieldLabel: $$iPems.lang.User.Window.ID,
                                allowBlank: false,
                                readOnly: true
                            },
                            {
                                id: 'roleId',
                                name: 'roleId',
                                xtype: 'combobox',
                                fieldLabel: $$iPems.lang.User.Window.Role,
                                displayField: 'text',
                                valueField: 'id',
                                typeAhead: true,
                                queryMode: 'local',
                                triggerAction: 'all',
                                emptyText: $$iPems.lang.PlsSelectEmptyText,
                                selectOnFocus: true,
                                forceSelection: true,
                                allowBlank: false,
                                store: comboRoleStore
                            },
                            {
                                id: 'password',
                                name: 'password',
                                xtype: 'textfield',
                                inputType: 'password',
                                fieldLabel: $$iPems.lang.User.Window.Password,
                                allowBlank: false,
                                hidden: false
                            },
                            {
                                id: 'limited',
                                name: 'limited',
                                xtype: 'datefield',
                                fieldLabel: $$iPems.lang.User.Window.Limited,
                                value: new Date(),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'comment',
                                name: 'comment',
                                xtype: 'textareafield',
                                fieldLabel: $$iPems.lang.User.Window.Comment,
                                height: 100,
                                maxLength: 500
                            }
                        ]
                    },
                    {
                        xtype: 'container',
                        flex: 1,
                        layout: 'anchor',
                        items: [
                                {
                                    id: 'uid',
                                    name: 'uid',
                                    xtype: 'textfield',
                                    fieldLabel: $$iPems.lang.User.Window.Name,
                                    allowBlank: false,
                                    readOnly: false
                                },
                                {
                                    id: 'empId',
                                    name: 'empId',
                                    xtype: 'EmployeePicker',
                                    fieldLabel: $$iPems.lang.User.Window.EmpName,
                                    allowBlank: false
                                },
                                {
                                    id: 'confirmPassword',
                                    name: 'confirmPassword',
                                    xtype: 'textfield',
                                    inputType: 'password',
                                    fieldLabel: $$iPems.lang.User.Window.Confirm,
                                    allowBlank: false,
                                    hidden: false,
                                    vtype: 'password',
                                    vtypeText: $$iPems.lang.User.ConfirmError,
                                    confirmTo: 'password'
                                },
                                {
                                    id: 'isLockedOut',
                                    name: 'isLockedOut',
                                    xtype: 'checkboxfield',
                                    fieldLabel: $$iPems.lang.User.Window.IsLockedOut,
                                    boxLabel: $$iPems.lang.User.Window.LockedOutLabel,
                                    inputValue: true,
                                    checked: false
                                },
                                {
                                    id: 'enabled',
                                    name: 'enabled',
                                    xtype: 'checkboxfield',
                                    fieldLabel: $$iPems.lang.User.Window.Enabled,
                                    boxLabel: $$iPems.lang.User.Window.EnabledLabel,
                                    inputValue: true,
                                    checked: false
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
          xtype: 'button',
          text: $$iPems.lang.Save,
          handler: function (el, e) {
              var form = Ext.getCmp('saveForm'),
                  baseForm = form.getForm(),
                  result = Ext.getCmp('saveResult');

              result.setTextWithIcon('', '');
              if (baseForm.isValid()) {
                  result.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
                  baseForm.submit({
                      submitEmptyText: false,
                      clientValidation: true,
                      preventWindow: true,
                      url: '/Account/SaveUser',
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
      }, {
          xtype: 'button',
          text: $$iPems.lang.Close,
          handler: function (el, e) {
              saveWnd.close();
          }
      }
    ]
});

var resetWnd = Ext.create('Ext.window.Window', {
    title: $$iPems.lang.User.Window.ResetPassword,
    glyph: 0xf022,
    height: 220,
    width: 400,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    items: [
        {
            xtype: 'form',
            id: 'resetForm',
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
                    xtype: 'hiddenfield',
                    value: ''
                },
                {
                    itemId: 'uid',
                    xtype: 'textfield',
                    fieldLabel: $$iPems.lang.User.Window.Name,
                    allowBlank: false,
                    readOnly: true
                },
                {
                    id: 'reset-password',
                    itemId: 'password',
                    xtype: 'textfield',
                    inputType: 'password',
                    fieldLabel: $$iPems.lang.User.Window.Password,
                    allowBlank: false,
                    hidden: false
                },
                {
                    id: 'reset-confirmPassword',
                    itemId: 'confirmPassword',
                    xtype: 'textfield',
                    inputType: 'password',
                    fieldLabel: $$iPems.lang.User.Window.Confirm,
                    allowBlank: false,
                    hidden: false,
                    vtype: 'password',
                    vtypeText: $$iPems.lang.User.ConfirmError,
                    confirmTo: 'reset-password'
                }
            ]
        }
    ],
    buttons: [
      { id: 'resetResult', xtype: 'iconlabel', text: '' },
      { xtype: 'tbfill' },
      {
          xtype: 'button',
          text: $$iPems.lang.Reset,
          handler: function (el, e) {
              var form = Ext.getCmp('resetForm'),
                  baseForm = form.getForm(),
                  id = form.getComponent('id').getValue(),
                  result = Ext.getCmp('resetResult');

              result.setTextWithIcon('', '');
              if (baseForm.isValid() && !Ext.isEmpty(id)) {
                  Ext.Msg.confirm($$iPems.lang.ConfirmWndTitle, $$iPems.lang.User.ConfirmReset, function (buttonId, text) {
                      if (buttonId === 'yes') {
                          result.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
                          var password = form.getComponent('password').getValue();
                          baseForm.submit({
                              submitEmptyText: false,
                              clientValidation: true,
                              preventWindow: true,
                              url: '/Account/ResetPassword',
                              params: {
                                  id: id,
                                  password: password
                              },
                              success: function (form, action) {
                                  result.setTextWithIcon(action.result.message, 'x-icon-accept');
                              },
                              failure: function (form, action) {
                                  var message = 'undefined error.';
                                  if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                      message = action.result.message;

                                  result.setTextWithIcon(message, 'x-icon-error');
                              }
                          });
                      }
                  });
              } else {
                  result.setTextWithIcon($$iPems.lang.FormError, 'x-icon-error');
              }
          }
      },
      {
          xtype: 'button',
          text: $$iPems.lang.Close,
          handler: function (el, e) {
              resetWnd.close();
          }
      }
    ]
});

var editCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    var basic = Ext.getCmp('saveForm').getForm();
    basic.load({
        url: '/Account/GetUser',
        params: { id: record.raw.id, action: $$iPems.Action.Edit },
        waitMsg: $$iPems.lang.AjaxHandling,
        waitTitle: $$iPems.lang.SysTipTitle,
        success: function (form, action) {
            form.clearInvalid();

            var uid = Ext.getCmp('uid'),
                password = Ext.getCmp('password'),
                confirmPassword = Ext.getCmp('confirmPassword');

            uid.setReadOnly(true);
            password.disable();
            password.hide();
            confirmPassword.disable();
            confirmPassword.hide();

            Ext.getCmp('saveResult').setTextWithIcon('', '');
            saveWnd.setGlyph(0xf002);
            saveWnd.setTitle($$iPems.lang.User.Window.EditTitle);
            saveWnd.opaction = $$iPems.Action.Edit;
            saveWnd.show();
        }
    });
};

var resetCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    var form = Ext.getCmp('resetForm');
    form.getForm().reset();
    form.getComponent('id').setValue(record.raw.id);
    form.getComponent('uid').setValue(record.raw.uid);
    Ext.getCmp('resetResult').setTextWithIcon('', '');
    resetWnd.show();
};

var deleteCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    Ext.Msg.confirm($$iPems.lang.ConfirmWndTitle, $$iPems.lang.ConfirmDelete, function (buttonId, text) {
        if (buttonId === 'yes') {
            Ext.Ajax.request({
                url: '/Account/DeleteUser',
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
    glyph: 0xf012,
    title: $$iPems.lang.User.Title,
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
        emptyText: $$iPems.lang.GridEmptyText,
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: $$iPems.lang.User.Columns.Index,
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Name,
        dataIndex: 'uid',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Role,
        dataIndex: 'roleName',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.EmpName,
        dataIndex: 'empName',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.EmpNo,
        dataIndex: 'empNo',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Sex,
        dataIndex: 'sexName',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Mobile,
        dataIndex: 'mobile',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Email,
        dataIndex: 'email',
        width: 100,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Created,
        dataIndex: 'created',
        align: 'center',
        width: 100,
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Limited,
        dataIndex: 'limited',
        align: 'center',
        width: 100,
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.LastLogined,
        dataIndex: 'lastLogined',
        align: 'center',
        width: 100,
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.LastPasswordChanged,
        dataIndex: 'lastPasswordChanged',
        align: 'center',
        width: 100,
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.IsLockedOut,
        dataIndex: 'isLockedOut',
        align: 'center',
        width: 100,
        sortable: true,
        renderer: function (value) { return value ? $$iPems.lang.StatusLocked : $$iPems.lang.StatusUnlocked; }
    }, {
        text: $$iPems.lang.User.Columns.LastLockedout,
        dataIndex: 'lastLockedout',
        align: 'center',
        width: 100,
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Comment,
        dataIndex: 'comment',
        align: 'left',
        width: 100,
        sortable: true
    }, {
        text: $$iPems.lang.User.Columns.Enabled,
        dataIndex: 'enabled',
        width: 100,
        align: 'center',
        sortable: true,
        renderer: function (value) { return value ? $$iPems.lang.StatusTrue : $$iPems.lang.StatusFalse;}
    }, {
        xtype: 'actioncolumn',
        width: 120,
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
            iconCls: 'x-cell-icon x-icon-password',
            handler: function (grid, rowIndex, colIndex) {
                resetCellClick(grid, rowIndex, colIndex);
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
            text: $$iPems.lang.User.ToolBar.Add,
            glyph: 0xf001,
            handler: function (el, e) {
                var basic = Ext.getCmp('saveForm').getForm();
                basic.load({
                    url: '/Account/GetUser',
                    params: { id: '', action: $$iPems.Action.Add },
                    waitMsg: $$iPems.lang.AjaxHandling,
                    waitTitle: $$iPems.lang.SysTipTitle,
                    success: function (form, action) {
                        form.clearInvalid();

                        var uid = Ext.getCmp('uid'),
                            password = Ext.getCmp('password'),
                            confirmPassword = Ext.getCmp('confirmPassword');

                        uid.setReadOnly(false);
                        password.enable();
                        password.show();
                        confirmPassword.enable();
                        confirmPassword.show();

                        Ext.getCmp('saveResult').setTextWithIcon('', '');
                        saveWnd.setGlyph(0xf001);
                        saveWnd.setTitle($$iPems.lang.User.Window.AddTitle);
                        saveWnd.opaction = $$iPems.Action.Add;
                        saveWnd.show();
                    }
                });
            }
        }, '-', {
            xtype: 'button',
            text: $$iPems.lang.Import,
            glyph: 0xf010,
            handler: function (el, e) {
                var params = currentStore.getProxy().extraParams;
                $$iPems.download({
                    url: '/Account/DownloadUsers',
                    params: {
                        rids: params.rids,
                        names: params.names
                    }
                });
            }
        }, '-', Ext.create('Ext.ux.MultiCombo', {
            id: 'rids-multicombo',
            fieldLabel: $$iPems.lang.User.ToolBar.Role,
            valueField: 'id',
            displayField: 'text',
            delimiter: $$iPems.Delimiter,
            queryMode: 'local',
            triggerAction: 'all',
            selectionMode: 'all',
            emptyText: $$iPems.lang.AllEmptyText,
            forceSelection: true,
            labelWidth: 60,
            width: 250,
            store: multiComboRoleStore
        }), {
            id: 'names-textbox',
            xtype: 'textfield',
            fieldLabel: $$iPems.lang.User.ToolBar.User,
            labelWidth: 60,
            width: 250,
            maxLength: 100,
            emptyText: $$iPems.lang.MultiConditionEmptyText
        }, {
            xtype: 'button',
            text: $$iPems.lang.Query,
            glyph: 0xf005,
            handler: function (el, e) {
                var ridsfield = Ext.getCmp('rids-multicombo');
                if (!ridsfield.isValid()) return false;
                var namesfield = Ext.getCmp('names-textbox');
                if (!namesfield.isValid()) return false;

                user.query(function (result, event, success) {
                    if (success) {
                        var rids = ridsfield.getSelectedValues(),
                            raw = namesfield.getRawValue(),
                            names = !Ext.isEmpty(raw) ? raw.split($$iPems.Delimiter) : [];

                        currentStore.getProxy().extraParams.rids = rids;
                        currentStore.getProxy().extraParams.names = names;
                        currentStore.loadPage(1);
                    }
                });
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
        currentStore.load();
        comboRoleStore.load();
        multiComboRoleStore.load();
    } 
});