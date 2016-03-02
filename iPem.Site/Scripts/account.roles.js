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
        url: '../Account/GetRoles',
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
        text: $$iPems.lang.Site.TreeRoot,
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '../Account/GetMenusInRole',
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
        text: $$iPems.lang.Site.TreeRoot,
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '../Account/GetAreasInRole',
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
        text: $$iPems.lang.Site.TreeRoot,
        icon: $$iPems.icons.Home,
        root: true
    },
    proxy: {
        type: 'ajax',
        url: '../Account/GetOperateInRole',
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
            { id: 'id', name: 'id', xtype: 'textfield', fieldLabel: $$iPems.lang.Role.ID, allowBlank: false, readOnly: true, labelAlign: 'top' },
            { id: 'name', name: 'name', xtype: 'textfield', fieldLabel: $$iPems.lang.Role.Name, allowBlank: false, labelAlign: 'top' },
            { id: 'comment', name: 'comment', xtype: 'textareafield', fieldLabel: $$iPems.lang.Role.Comment, height: 100, labelAlign: 'top' },
            { id: 'enabled', name: 'enabled', xtype: 'checkboxfield', fieldLabel: $$iPems.lang.Role.Enabled, boxLabel: $$iPems.lang.Role.EnabledLabel, inputValue: true, checked: false, labelAlign: 'top' }
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
            title: $$iPems.lang.Role.MenuTitle,
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
            title: $$iPems.lang.Role.AreaTitle,
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
            title: $$iPems.lang.Role.OperateTitle,
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
              Ext.getCmp('saveResult').setTextWithIcon('', '');

              var form = Ext.getCmp('saveForm').getForm();
              if (form.isValid()) {
                  var menus = Ext.getCmp('treeMenus').getChecked();
                  if (menus.length === 0) {
                      Ext.getCmp('saveResult').setTextWithIcon($$iPems.lang.Role.MenuError, 'x-icon-error');
                      return false;
                  }

                  var areas = Ext.getCmp('areaMenus').getChecked();
                  if (menus.length === 0) {
                      Ext.getCmp('saveResult').setTextWithIcon($$iPems.lang.Role.AreaError, 'x-icon-error');
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

                  Ext.getCmp('saveResult').setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
                  form.submit({
                      clientValidation: true,
                      preventWindow: true,
                      url: '../Account/SaveRole',
                      params: {
                          menuIds: menuIds,
                          areaIds: areaIds,
                          operateIds: operateIds,
                          action: saveWnd.opaction
                      },
                      success: function (form, action) {
                          Ext.getCmp('saveResult').setTextWithIcon(action.result.message, 'x-icon-accept');
                          if (saveWnd.opaction == $$iPems.Action.Add)
                              currentStore.loadPage(1);
                          else
                              currentPagingToolbar.doRefresh();
                      },
                      failure: function (form, action) {
                          var message = 'undefined error.';
                          if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                              message = action.result.message;

                          Ext.getCmp('saveResult').setTextWithIcon(message, 'x-icon-error');
                      }
                  });
              } else {
                  Ext.getCmp('saveResult').setTextWithIcon($$iPems.lang.FormError, 'x-icon-error');
              }
          }
      },
      { xtype: 'button', text: $$iPems.lang.Close, handler: function (el, e) { saveWnd.hide(); } }
    ]
});

var editCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    Ext.getCmp('saveForm').getForm().load({
        url: '../Account/GetRole',
        params: { id: record.raw.id, action: $$iPems.Action.Edit },
        reset: true,
        waitMsg: $$iPems.lang.AjaxHandling,
        waitTitle: $$iPems.lang.SysTipTitle,
        success: function (form, action) {
            form.setValues(action.result.data);
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
            saveWnd.setTitle($$iPems.lang.Role.EditTitle);
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
                url: '../Account/DeleteRole',
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
        text: $$iPems.lang.Role.Index,
        dataIndex: 'index',
        width: 60,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Role.ID,
        dataIndex: 'id',
        width: 150,
        align: 'center',
        sortable: false
    }, {
        text: $$iPems.lang.Role.Name,
        dataIndex: 'name',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: $$iPems.lang.Role.Comment,
        dataIndex: 'comment',
        flex: 1,
        align: 'left',
        sortable: true
    }, {
        text: $$iPems.lang.Role.Enabled,
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
            text: $$iPems.lang.Role.AddTitle,
            glyph: 0xf001,
            handler: function (el, e) {
                var form = Ext.getCmp('saveForm').getForm();
                form.load({
                    url: '../Account/GetRole',
                    params: { id: '', action: $$iPems.Action.Add },
                    reset: true,
                    waitMsg: $$iPems.lang.AjaxHandling,
                    waitTitle: $$iPems.lang.SysTipTitle,
                    success: function (form, action) {
                        form.setValues(action.result.data);
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
                        saveWnd.setTitle($$iPems.lang.Role.AddTitle);
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
                $$iPems.download({ url: '../Account/DownloadRoles', params: { condition: currentStore.getProxy().extraParams.condition } });
            }
        }, '-', {
            xtype: 'textfield',
            id: 'namesfield',
            fieldLabel: $$iPems.lang.Role.Name,
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
                    currentPagingToolbar.doRefresh();
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