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
        text: $$iPems.lang.Site.MenuNavRoot,
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '/Account/GetMenusInRole',
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
        text: $$iPems.lang.Site.MenuNavRoot,
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '/Account/GetAreasInRole',
        reader: {
            type: 'json',
            successProperty: 'success',
            messageProperty: 'message',
            totalProperty: 'total',
            root: 'data'
        }
    }
});

var operateStore = Ext.create('Ext.data.TreeStore', {
    autoLoad: false,
    root: {
        id: -10078,
        text: $$iPems.lang.Site.MenuNavRoot,
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '/Account/GetOperateInRole',
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
                fieldLabel: $$iPems.lang.Role.Window.ID,
                allowBlank: false,
                readOnly: true,
                labelAlign: 'top'
            },
            {
                id: 'name',
                name: 'name',
                xtype: 'textfield',
                fieldLabel: $$iPems.lang.Role.Window.Name,
                allowBlank: false,
                labelAlign: 'top'
            },
            {
                id: 'comment',
                name: 'comment',
                xtype: 'textareafield',
                fieldLabel: $$iPems.lang.Role.Window.Comment,
                height: 100,
                labelAlign: 'top'
            },
            {
                id: 'enabled',
                name: 'enabled',
                xtype: 'checkboxfield',
                fieldLabel: $$iPems.lang.Role.Window.Enabled,
                boxLabel: $$iPems.lang.Role.Window.EnabledLabel,
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
            title: $$iPems.lang.Role.Window.MenuTitle,
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
            title: $$iPems.lang.Role.Window.AreaTitle,
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
            id: 'operateMenus',
            xtype: 'treepanel',
            title: $$iPems.lang.Role.Window.OperateTitle,
            glyph: 0xf028,
            store: operateStore,
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
          xtype: 'button', text: $$iPems.lang.Save, handler: function (el, e) {
              var form = Ext.getCmp('saveForm').getForm(),
                  result = Ext.getCmp('saveResult');

              result.setTextWithIcon('', '');
              if (form.isValid()) {
                  var menus = Ext.getCmp('treeMenus').getChecked();
                  if (menus.length === 0) {
                      result.setTextWithIcon($$iPems.lang.Role.MenuError, 'x-icon-error');
                      return false;
                  }

                  var areas = Ext.getCmp('areaMenus').getChecked();
                  if (areas.length === 0) {
                      result.setTextWithIcon($$iPems.lang.Role.AreaError, 'x-icon-error');
                      return false;
                  }

                  var operates = Ext.getCmp('operateMenus').getChecked();

                  var menuIds = [];
                  menus.forEach(function (m) {
                      menuIds.push(m.data.id);
                  });

                  var areaIds = [];
                  areas.forEach(function (m) {
                      areaIds.push(m.data.id);
                  });

                  var operateIds = [];
                  operates.forEach(function (m) {
                      operateIds.push(m.data.id);
                  });

                  result.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
                  form.submit({
                      submitEmptyText: false,
                      clientValidation: true,
                      preventWindow: true,
                      url: '/Account/SaveRole',
                      params: {
                          menuIds: menuIds,
                          areaIds: areaIds,
                          operateIds: operateIds,
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
          xtype: 'button',
          text: $$iPems.lang.Close,
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
        waitMsg: $$iPems.lang.AjaxHandling,
        waitTitle: $$iPems.lang.SysTipTitle,
        success: function (form, action) {
            form.clearInvalid();
            Ext.getCmp('name').setReadOnly(true);

            var menuIds = [];
            if(!Ext.isEmpty(action.result.data.menuIds))
                menuIds = action.result.data.menuIds;

            var areaIds = [];
            if (!Ext.isEmpty(action.result.data.areaIds))
                areaIds = action.result.data.areaIds;

            var operateIds = [];
            if (!Ext.isEmpty(action.result.data.operateIds))
                operateIds = action.result.data.operateIds;

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

            var root3 = Ext.getCmp('operateMenus').getRootNode();
            if (root3.hasChildNodes()) {
                root3.eachChild(function (c) {
                    c.cascadeBy(function (n) {
                        var checked = Ext.Array.contains(operateIds, n.data.id);
                        n.set('checked', checked);
                    });
                });
            }

            Ext.getCmp('saveResult').setTextWithIcon('', '');
            saveWnd.setGlyph(0xf002);
            saveWnd.setTitle($$iPems.lang.Role.Window.EditTitle);
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
                url: '/Account/DeleteRole',
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
    glyph: 0xf033,
    title: $$iPems.lang.Role.Title,
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
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: $$iPems.lang.Role.Columns.Index,
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Role.Columns.ID,
        dataIndex: 'id',
        width: 150,
        align: 'center',
        sortable: false
    }, {
        text: $$iPems.lang.Role.Columns.Name,
        dataIndex: 'name',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: $$iPems.lang.Role.Columns.Comment,
        dataIndex: 'comment',
        flex: 1,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Role.Columns.Enabled,
        dataIndex: 'enabled',
        width: 100,
        align: 'center',
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
            text: $$iPems.lang.Role.ToolBar.Add,
            glyph: 0xf001,
            handler: function (el, e) {
                var basic = Ext.getCmp('saveForm').getForm();
                basic.load({
                    url: '/Account/GetRole',
                    params: { id: '', action: $$iPems.Action.Add },
                    waitMsg: $$iPems.lang.AjaxHandling,
                    waitTitle: $$iPems.lang.SysTipTitle,
                    success: function (form, action) {
                        form.clearInvalid();

                        Ext.getCmp('name').setReadOnly(false);
                        var root = Ext.getCmp('treeMenus').getRootNode();
                        if (root.hasChildNodes()) {
                            root.eachChild(function (c) {
                                c.cascadeBy(function (n) {
                                    var checked = Ext.Array.contains(action.result.data.menuIds, n.data.id);
                                    n.set('checked', checked);
                                });
                            });
                        }

                        Ext.getCmp('saveResult').setTextWithIcon('', '');
                        saveWnd.setGlyph(0xf001);
                        saveWnd.setTitle($$iPems.lang.Role.Window.AddTitle);
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
                $$iPems.download({ url: '/Account/DownloadRoles', params: { condition: currentStore.getProxy().extraParams.condition } });
            }
        }, '-', {
            xtype: 'textfield',
            id: 'namesfield',
            fieldLabel: $$iPems.lang.Role.ToolBar.Name,
            labelWidth: 60,
            width: 250,
            maxLength: 100,
            emptyText: $$iPems.lang.MultiConditionEmptyText
        }, {
            xtype: 'button',
            text: $$iPems.lang.Query,
            glyph: 0xf005,
            handler: function (el, e) {
                var namesfield = Ext.getCmp('namesfield');
                if (!Ext.isEmpty(namesfield) && namesfield.isValid()) {
                    currentStore.getProxy().extraParams.condition = namesfield.getRawValue();
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
        currentStore.load();
        menuStore.load();
        areaStore.load();
        operateStore.load();
    }
});