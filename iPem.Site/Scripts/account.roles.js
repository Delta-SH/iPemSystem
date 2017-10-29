Ext.define('RoleModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'id', type: 'string' },
        { name: 'name', type: 'string' },
        { name: 'comment', type: 'string' },
        { name: 'enabled', type: 'boolean' }
    ],
    idProperty: 'id'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'RoleModel',
    proxy: {
        type: 'ajax',
        url: '/Account/GetRoles',
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
        extraParams: {
            condition: ''
        },
        simpleSortMode: true
    }
});

var menuStore = Ext.create('Ext.data.TreeStore',{
    autoLoad: false,
    root: {
        id: -10078,
        text: '系统主页',
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '/Account/GetAllMenus',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        }
    }
});

var areaStore = Ext.create('Ext.data.TreeStore',{
    autoLoad: false,
    root: {
        id: -10078,
        text: '系统主页',
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '/Account/GetAllAreas',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        }
    }
});

var permissionStore = Ext.create('Ext.data.TreeStore', {
    autoLoad: false,
    root: {
        id: -10078,
        text: '系统主页',
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '/Account/GetAllPermissions',
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
    title: 'Role',
    height: 400,
    width: 600,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    opaction: $$iPems.Action.Add,
    layout: {
        type: 'hbox',
        align: 'stretch'
    },
    items: [{
        id: 'saveForm',
        xtype: 'form',
        margin: '5 5 5 5',
        flex: 2,
        border: false,
        defaultType: 'textfield',
        defaults: {
            anchor: '100%'
        },
        fieldDefaults: {
            labelWidth: 60,
            labelAlign: "left",
            flex: 1,
            margin: '10 5 10 5'
        },
        items: [
            {
                id: 'id',
                name: 'id',
                xtype: 'textfield',
                fieldLabel: '角色标识',
                allowBlank: false,
                readOnly: true,
                labelAlign: 'top'
            },
            {
                id: 'name',
                name: 'name',
                xtype: 'textfield',
                fieldLabel: '角色名称',
                allowBlank: false,
                labelAlign: 'top'
            },
            {
                id: 'comment',
                name: 'comment',
                xtype: 'textareafield',
                fieldLabel: '角色备注',
                height: 100,
                labelAlign: 'top'
            },
            {
                id: 'enabled',
                name: 'enabled',
                xtype: 'checkboxfield',
                fieldLabel: '角色状态',
                boxLabel: '(勾选表示启用)',
                inputValue: true,
                checked: false,
                labelAlign: 'top'
            }
        ]
    },
    {
        xtype: "tabpanel",
        flex: 3,
        margin: '5 5 5 5',
        activeTab: 0,
        plain: true,
        defaults: {
            autoScroll: true,
            border: false,
            useArrows: false,
            rootVisible: false,
        },
        items: [{
            id: 'treeMenus',
            xtype: 'treepanel',
            title: '菜单权限',
            glyph: 0xf011,
            store: menuStore,
            listeners: {
                load: function (el, node, records, successful) {
                    if (successful)
                        this.getRootNode().expand(false);
                },
                checkchange: function (node, checked) {
                    node.eachChild(function (c) {
                        c.cascadeBy(function (n) {
                            n.set('checked', checked);
                        });
                    });
                }
            }
        },{
            id: 'areaMenus',
            xtype: 'treepanel',
            title: '区域权限',
            glyph: 0xf019,
            store: areaStore,
            listeners: {
                load: function (el, node, records, successful) {
                    if (successful)
                        this.getRootNode().expand(false);
                },
                checkchange: function (node, checked) {
                    node.eachChild(function (c) {
                        c.cascadeBy(function (n) {
                            n.set('checked', checked);
                        });
                    });
                }
            }
        }, {
            id: 'permissionMenus',
            xtype: 'treepanel',
            title: '操作权限',
            glyph: 0xf028,
            store: permissionStore,
            listeners: {
                load: function (el, node, records, successful) {
                    if (successful)
                        this.getRootNode().expand(false);
                },
                checkchange: function (node, checked) {
                    node.eachChild(function (c) {
                        c.cascadeBy(function (n) {
                            n.set('checked', checked);
                        });
                    });
                }
            }
        }]
    }],
    buttons: [
      { id: 'saveResult', xtype: 'iconlabel', text: '' },
      { xtype: 'tbfill' },
      {
          xtype: 'button',
          text: '保存',
          handler: function (el, e) {
              var form = Ext.getCmp('saveForm').getForm(),
                  result = Ext.getCmp('saveResult');

              result.setTextWithIcon('', '');
              if (form.isValid()) {
                  var menus = Ext.getCmp('treeMenus').getChecked();
                  if (menus.length === 0) {
                      result.setTextWithIcon('请选择角色的菜单权限...', 'x-icon-error');
                      return false;
                  }

                  var areas = Ext.getCmp('areaMenus').getChecked();
                  if (areas.length === 0) {
                      result.setTextWithIcon('请选择角色的区域权限...', 'x-icon-error');
                      return false;
                  }

                  var permissions = Ext.getCmp('permissionMenus').getChecked();

                  var menuIds = [];
                  menus.forEach(function (m) {
                      menuIds.push(m.data.id);
                  });

                  var areaIds = [];
                  areas.forEach(function (m) {
                      areaIds.push(m.data.id);
                  });

                  var permissionIds = [];
                  permissions.forEach(function (m) {
                      permissionIds.push(m.data.id);
                  });

                  result.setTextWithIcon('正在处理...', 'x-icon-loading');
                  form.submit({
                      submitEmptyText: false,
                      clientValidation: true,
                      preventWindow: true,
                      url: '/Account/SaveRole',
                      params: {
                          menus: menuIds,
                          areas: areaIds,
                          permissions: permissionIds,
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
              } else {
                  result.setTextWithIcon('表单填写错误', 'x-icon-error');
              }
          }
      },
      {
          xtype: 'button',
          text: '关闭',
          handler: function (el, e) {
              saveWnd.close();
          }
      }
    ]
});

var editCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    var basic = Ext.getCmp('saveForm').getForm();
    basic.load({
        url: '/Account/GetRole',
        params: { id: record.raw.id, action: $$iPems.Action.Edit },
        waitMsg: '正在处理...',
        waitTitle: '系统提示',
        success: function (form, action) {
            form.clearInvalid();
            Ext.getCmp('name').setReadOnly(true);
            Ext.getCmp('saveResult').setTextWithIcon('', '');

            var menuIds = [];
            if(!Ext.isEmpty(action.result.data.menus))
                menuIds = action.result.data.menus;

            var areaIds = [];
            if (!Ext.isEmpty(action.result.data.areas))
                areaIds = action.result.data.areas;

            var permissionIds = [];
            if (!Ext.isEmpty(action.result.data.permissions))
                permissionIds = action.result.data.permissions;

            var root1 = Ext.getCmp('treeMenus').getRootNode();
            if (root1.hasChildNodes()) {
                root1.eachChild(function (c) {
                    c.cascadeBy(function (n) {
                        var checked = Ext.Array.contains(menuIds, n.data.id);
                        n.set('checked', checked);
                    });
                });
            }

            var root2 = Ext.getCmp('areaMenus').getRootNode();
            if (root2.hasChildNodes()) {
                root2.eachChild(function (c) {
                    c.cascadeBy(function (n) {
                        var checked = Ext.Array.contains(areaIds, n.data.id);
                        n.set('checked', checked);
                    });
                });
            }

            var root3 = Ext.getCmp('permissionMenus').getRootNode();
            if (root3.hasChildNodes()) {
                root3.eachChild(function (c) {
                    c.cascadeBy(function (n) {
                        var checked = Ext.Array.contains(permissionIds, n.data.id);
                        n.set('checked', checked);
                    });
                });
            }

            saveWnd.setGlyph(0xf002);
            saveWnd.setTitle('编辑角色');
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
                url: '/Account/DeleteRole',
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
    glyph: 0xf033,
    title: '角色信息',
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: true,
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
        text: '角色标识',
        dataIndex: 'id',
        width: 150,
        align: 'center',
        sortable: false
    }, {
        text: '角色名称',
        dataIndex: 'name',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: '角色备注',
        dataIndex: 'comment',
        flex: 1,
        align: 'left',
        sortable: true
    }, {
        text: '角色状态',
        dataIndex: 'enabled',
        width: 100,
        align: 'center',
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
            text: '新增角色',
            glyph: 0xf001,
            handler: function (el, e) {
                var basic = Ext.getCmp('saveForm').getForm();
                basic.load({
                    url: '/Account/GetRole',
                    params: { id: '', action: $$iPems.Action.Add },
                    waitMsg: '正在处理...',
                    waitTitle: '系统提示',
                    success: function (form, action) {
                        form.clearInvalid();
                        Ext.getCmp('name').setReadOnly(false);
                        Ext.getCmp('saveResult').setTextWithIcon('', '');

                        var menuIds = [];
                        if (!Ext.isEmpty(action.result.data.menus))
                            menuIds = action.result.data.menus;

                        var areaIds = [];
                        if (!Ext.isEmpty(action.result.data.areas))
                            areaIds = action.result.data.areas;

                        var permissionIds = [];
                        if (!Ext.isEmpty(action.result.data.permissions))
                            permissionIds = action.result.data.permissions;

                        var root1 = Ext.getCmp('treeMenus').getRootNode();
                        if (root1.hasChildNodes()) {
                            root1.eachChild(function (c) {
                                c.cascadeBy(function (n) {
                                    var checked = Ext.Array.contains(menuIds, n.data.id);
                                    n.set('checked', checked);
                                });
                            });
                        }

                        var root2 = Ext.getCmp('areaMenus').getRootNode();
                        if (root2.hasChildNodes()) {
                            root2.eachChild(function (c) {
                                c.cascadeBy(function (n) {
                                    var checked = Ext.Array.contains(areaIds, n.data.id);
                                    n.set('checked', checked);
                                });
                            });
                        }

                        var root3 = Ext.getCmp('permissionMenus').getRootNode();
                        if (root3.hasChildNodes()) {
                            root3.eachChild(function (c) {
                                c.cascadeBy(function (n) {
                                    var checked = Ext.Array.contains(permissionIds, n.data.id);
                                    n.set('checked', checked);
                                });
                            });
                        }

                        saveWnd.setGlyph(0xf001);
                        saveWnd.setTitle('新增角色');
                        saveWnd.opaction = $$iPems.Action.Add;
                        saveWnd.show();
                    }
                });
            }
        }, '-', {
            xtype: 'textfield',
            id: 'namesfield',
            fieldLabel: '角色名称',
            labelWidth: 60,
            width: 250,
            maxLength: 100,
            emptyText: '多条件请以;分隔，例: A;B;C'
        }, {
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
    bbar: currentPagingToolbar
});

var query = function () {
    var namesfield = Ext.getCmp('namesfield');
    if (namesfield.isValid()) {
        var me = currentStore, proxy = me.getProxy();
        proxy.extraParams.condition = namesfield.getRawValue();
        proxy.extraParams.cache = false;
        me.loadPage(1, {
            callback: function (records, operation, success) {
                proxy.extraParams.cache = success;
                Ext.getCmp('exportButton').setDisabled(success === false);
            }
        });
    }
};

var print = function () {
    $$iPems.download({
        url: '/Account/DownloadRoles',
        params: currentStore.getProxy().extraParams
    });
};

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(currentGridPanel);

        //load store data
        menuStore.load();
        areaStore.load();
        permissionStore.load();
    }
});