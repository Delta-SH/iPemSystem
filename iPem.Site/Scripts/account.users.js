Ext.apply(Ext.form.VTypes, {
    password: function (val, field) {
        if (field.confirmTo) {
            var pwd = Ext.getCmp(field.confirmTo);
            return (val == pwd.getValue());
        }
        return true;
    }
});

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
            roles: [],
            names: []
        },
        listeners: {
            exception: function (proxy, response, operation) {
                Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
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
                                fieldLabel: '用户标识',
                                allowBlank: false,
                                readOnly: true
                            },
                            {
                                id: 'roleId',
                                name: 'roleId',
                                xtype: 'combobox',
                                fieldLabel: '隶属角色',
                                displayField: 'text',
                                valueField: 'id',
                                typeAhead: true,
                                queryMode: 'local',
                                triggerAction: 'all',
                                emptyText: '请选择隶属角色...',
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
                                fieldLabel: '登录密码',
                                allowBlank: false,
                                hidden: false
                            },
                            {
                                id: 'limited',
                                name: 'limited',
                                xtype: 'datefield',
                                fieldLabel: '有效日期',
                                value: new Date(),
                                editable: false,
                                allowBlank: false
                            },
                            {
                                id: 'comment',
                                name: 'comment',
                                xtype: 'textareafield',
                                fieldLabel: '用户备注',
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
                                    fieldLabel: '用户名称',
                                    allowBlank: false,
                                    readOnly: false
                                },
                                {
                                    id: 'empId',
                                    name: 'empId',
                                    xtype: 'EmployeePicker',
                                    fieldLabel: '隶属员工',
                                    allowBlank: false
                                },
                                {
                                    id: 'confirmPassword',
                                    name: 'confirmPassword',
                                    xtype: 'textfield',
                                    inputType: 'password',
                                    fieldLabel: '确认密码',
                                    allowBlank: false,
                                    hidden: false,
                                    vtype: 'password',
                                    vtypeText: '密码不一致',
                                    confirmTo: 'password'
                                },
                                {
                                    id: 'isLockedOut',
                                    name: 'isLockedOut',
                                    xtype: 'checkboxfield',
                                    fieldLabel: '锁定状态',
                                    boxLabel: '(勾选锁定用户)',
                                    inputValue: true,
                                    checked: false
                                },
                                {
                                    id: 'enabled',
                                    name: 'enabled',
                                    xtype: 'checkboxfield',
                                    fieldLabel: '用户状态',
                                    boxLabel: '(勾选表示启用)',
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
          text: '保存',
          handler: function (el, e) {
              var form = Ext.getCmp('saveForm'),
                  baseForm = form.getForm(),
                  result = Ext.getCmp('saveResult');

              result.setTextWithIcon('', '');
              if (baseForm.isValid()) {
                  result.setTextWithIcon('正在处理...', 'x-icon-loading');
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
                  result.setTextWithIcon('表单填写错误', 'x-icon-error');
              }
          }
      }, {
          xtype: 'button',
          text: '关闭',
          handler: function (el, e) {
              saveWnd.close();
          }
      }
    ]
});

var resetWnd = Ext.create('Ext.window.Window', {
    title: '重置密码',
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
                    fieldLabel: '用户名称',
                    allowBlank: false,
                    readOnly: true
                },
                {
                    id: 'reset-password',
                    itemId: 'password',
                    xtype: 'textfield',
                    inputType: 'password',
                    fieldLabel: '新的密码',
                    allowBlank: false,
                    hidden: false
                },
                {
                    id: 'reset-confirmPassword',
                    itemId: 'confirmPassword',
                    xtype: 'textfield',
                    inputType: 'password',
                    fieldLabel: '确认密码',
                    allowBlank: false,
                    hidden: false,
                    vtype: 'password',
                    vtypeText: '密码不一致',
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
          text: '重置',
          handler: function (el, e) {
              var form = Ext.getCmp('resetForm'),
                  baseForm = form.getForm(),
                  id = form.getComponent('id').getValue(),
                  result = Ext.getCmp('resetResult');

              result.setTextWithIcon('', '');
              if (baseForm.isValid() && !Ext.isEmpty(id)) {
                  Ext.Msg.confirm('确认对话框', '您确认要重置密码吗？', function (buttonId, text) {
                      if (buttonId === 'yes') {
                          result.setTextWithIcon('正在处理...', 'x-icon-loading');
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
                  result.setTextWithIcon('表单填写错误', 'x-icon-error');
              }
          }
      },
      {
          xtype: 'button',
          text: '关闭',
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
        waitMsg: '正在处理...',
        waitTitle: '系统提示',
        success: function (form, action) {
            form.clearInvalid();
            Ext.getCmp('saveResult').setTextWithIcon('', '');

            var uid = Ext.getCmp('uid'),
                password = Ext.getCmp('password'),
                confirmPassword = Ext.getCmp('confirmPassword');

            uid.setReadOnly(true);
            password.disable();
            password.hide();
            confirmPassword.disable();
            confirmPassword.hide();

            saveWnd.setGlyph(0xf002);
            saveWnd.setTitle('编辑用户');
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

    Ext.Msg.confirm('确认对话框', '您确认要删除吗？', function (buttonId, text) {
        if (buttonId === 'yes') {
            Ext.Ajax.request({
                url: '/Account/DeleteUser',
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
    glyph: 0xf012,
    title: '用户信息',
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
        text: '用户名称',
        dataIndex: 'uid',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '隶属角色',
        dataIndex: 'roleName',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '隶属员工',
        dataIndex: 'empName',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '员工工号',
        dataIndex: 'empNo',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '性别',
        dataIndex: 'sexName',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '联系电话',
        dataIndex: 'mobile',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: 'Email',
        dataIndex: 'email',
        width: 100,
        align: 'left',
        sortable: true
    }, {
        text: '创建日期',
        dataIndex: 'created',
        align: 'left',
        width: 100,
        sortable: true
    }, {
        text: '有效日期',
        dataIndex: 'limited',
        align: 'left',
        width: 100,
        sortable: true
    }, {
        text: '最后登录日期',
        dataIndex: 'lastLogined',
        align: 'left',
        width: 100,
        sortable: true
    }, {
        text: '密码更改日期',
        dataIndex: 'lastPasswordChanged',
        align: 'left',
        width: 100,
        sortable: true
    }, {
        text: '锁定状态',
        dataIndex: 'isLockedOut',
        align: 'left',
        width: 100,
        sortable: true,
        renderer: function (value) { return value ? '锁定' : '正常'; }
    }, {
        text: '最后锁定日期',
        dataIndex: 'lastLockedout',
        align: 'left',
        width: 100,
        sortable: true
    }, {
        text: '用户备注',
        dataIndex: 'comment',
        align: 'left',
        width: 100,
        sortable: true
    }, {
        text: '用户状态',
        dataIndex: 'enabled',
        width: 100,
        align: 'left',
        sortable: true,
        renderer: function (value) { return value ? '有效' : '禁用'; }
    }, {
        xtype: 'actioncolumn',
        width: 120,
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
            text: '新增用户',
            glyph: 0xf001,
            handler: function (el, e) {
                var basic = Ext.getCmp('saveForm').getForm();
                basic.load({
                    url: '/Account/GetUser',
                    params: { id: '', action: $$iPems.Action.Add },
                    waitMsg: '正在处理...',
                    waitTitle: '系统提示',
                    success: function (form, action) {
                        form.clearInvalid();
                        Ext.getCmp('saveResult').setTextWithIcon('', '');

                        var uid = Ext.getCmp('uid'),
                            password = Ext.getCmp('password'),
                            confirmPassword = Ext.getCmp('confirmPassword');

                        uid.setReadOnly(false);
                        password.enable();
                        password.show();
                        confirmPassword.reset();
                        confirmPassword.enable();
                        confirmPassword.show();

                        saveWnd.setGlyph(0xf001);
                        saveWnd.setTitle('新增用户');
                        saveWnd.opaction = $$iPems.Action.Add;
                        saveWnd.show();
                    }
                });
            }
        }, '-', Ext.create('Ext.ux.MultiCombo', {
            id: 'roles-multicombo',
            fieldLabel: '角色名称',
            valueField: 'id',
            displayField: 'text',
            delimiter: $$iPems.Delimiter,
            queryMode: 'local',
            triggerAction: 'all',
            selectionMode: 'all',
            emptyText: '默认全部',
            forceSelection: true,
            labelWidth: 60,
            width: 250,
            store: multiComboRoleStore
        }), {
            id: 'names-textbox',
            xtype: 'textfield',
            fieldLabel: '用户名称',
            labelWidth: 60,
            width: 250,
            maxLength: 100,
            emptyText: '多条件请以;分隔，例: A;B;C'
        }, {
            xtype: 'button',
            text: '数据查询',
            glyph: 0xf005,
            handler: function (el, e) {
                var rolesfield = Ext.getCmp('roles-multicombo'),
                    namesfield = Ext.getCmp('names-textbox');
                if (rolesfield.isValid() && namesfield.isValid()) {
                    var roles = rolesfield.getSelectedValues(),
                        raw = namesfield.getRawValue(),
                        names = !Ext.isEmpty(raw) ? raw.split($$iPems.Delimiter) : [];

                    currentStore.getProxy().extraParams.roles = roles;
                    currentStore.getProxy().extraParams.names = names;
                    currentStore.loadPage(1);
                }
            }
        }, '-', {
            xtype: 'button',
            text: '数据导出',
            glyph: 0xf010,
            handler: function (el, e) {
                $$iPems.download({
                    url: '/Account/DownloadUsers',
                    params: currentStore.getProxy().extraParams
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