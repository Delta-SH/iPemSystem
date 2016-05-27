Ext.define('AppointmentModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'id', type: 'string' },
        { name: 'startTime', type: 'string' },
        { name: 'endTime', type: 'string' },
        { name: 'projectName', type: 'string' },
        { name: 'projectId', type: 'string' },
        { name: 'creator', type: 'string' },
        { name: 'createdTime', type: 'string' },
        { name: 'comment', type: 'string' },
        { name: 'enabled', type: 'boolean' }
    ],
    idProperty: 'id'
});

var currentStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 20,
    model: 'AppointmentModel',
    proxy: {
        type: 'ajax',
        url: '/Project/GetAppointments',
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

var typeStore = Ext.create('Ext.data.Store', {
    fields: [
         { name: 'id', type: 'int' },
         { name: 'name', type: 'string' }
    ],
    data: [
        { "id": -1, "name": $$iPems.lang.Appointment.ToolBar.ProjectType },
        { "id": $$iPems.Organization.Area, "name": $$iPems.lang.Appointment.ToolBar.AreaType },
        { "id": $$iPems.Organization.Station, "name": $$iPems.lang.Appointment.ToolBar.StationType },
        { "id": $$iPems.Organization.Room, "name": $$iPems.lang.Appointment.ToolBar.RoomType },
        { "id": $$iPems.Organization.Device, "name": $$iPems.lang.Appointment.ToolBar.DeviceType }
    ]
});

var projectStore = Ext.create('Ext.data.Store', {
    autoLoad: false,
    pageSize: 1024,
    fields: [
        { name: 'id', type: 'string' },
        { name: 'text', type: 'string' },
        { name: 'comment', type: 'string' }
    ],
    proxy: {
        type: 'ajax',
        url: '/Project/GetComboProjects',
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
            if (successful && !Ext.isEmpty(records) && records.length > 0)
                Ext.getCmp('projectId').select(records[0]);
        }
    }
});

var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

var submit = function (form, nodes, result) {
    result.setTextWithIcon($$iPems.lang.AjaxHandling, 'x-icon-loading');
    form.submit({
        submitEmptyText: false,
        clientValidation: true,
        preventWindow: true,
        url: '/Project/SaveAppointment',
        params: {
            nodes: nodes,
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
};

var detailWnd = Ext.create('Ext.window.Window', {
    title: $$iPems.lang.Appointment.Window.DetailTitle,
    glyph: 0xf045,
    height: 250,
    width: 400,
    modal: true,
    border: false,
    hidden: true,
    closeAction: 'hide',
    bodyPadding: 10,
    layout: 'form',
    defaultType: 'displayfield',
    items: [{
        itemId: 'detail_area',
        labelWidth: 60,
        fieldLabel: $$iPems.lang.Appointment.Window.DetailArea
    }, {
        itemId: 'detail_station',
        labelWidth: 60,
        fieldLabel: $$iPems.lang.Appointment.Window.DetailStation
    }, {
        itemId: 'detail_room',
        labelWidth: 60,
        fieldLabel: $$iPems.lang.Appointment.Window.DetailRoom
    }, {
        itemId: 'detail_device',
        labelWidth: 60,
        fieldLabel: $$iPems.lang.Appointment.Window.DetailDevice
    }],
    buttonAlign: 'right',
    buttons: [{
        xtype: 'button',
        text: $$iPems.lang.Close,
        handler: function (el, e) {
            detailWnd.hide();
        }
    }]
});

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
        flex: 2,
        border: false,
        defaultType: 'textfield',
        defaults: {
            anchor: '100%'
        },
        fieldDefaults: {
            labelWidth: 60,
            labelAlign: "top",
            margin: 10
        },
        items: [
            {
                id: 'id',
                name: 'id',
                xtype: 'hiddenfield',
                value: ''
            },
            {
                id: 'projectId',
                name: 'projectId',
                xtype: 'combobox',
                fieldLabel: $$iPems.lang.Appointment.Window.Name,
                store: projectStore,
                displayField: 'text',
                valueField: 'id',
                typeAhead: true,
                queryMode: 'local',
                triggerAction: 'all',
                selectOnFocus: true,
                forceSelection: true,
                allowBlank: false
            },
            {
                id: 'startTime',
                name: 'startTime',
                xtype: 'datetimepicker',
                fieldLabel: $$iPems.lang.Appointment.Window.StartTime,
                editable: false,
                allowBlank: false
            },
            {
                id: 'endTime',
                name: 'endTime',
                xtype: 'datetimepicker',
                fieldLabel: $$iPems.lang.Appointment.Window.EndTime,
                editable: false,
                allowBlank: false
            },
            {
                id: 'comment',
                name: 'comment',
                xtype: 'textareafield',
                fieldLabel: $$iPems.lang.Appointment.Window.Comment,
                height: 70
            },
            {
                id: 'enabled',
                name: 'enabled',
                xtype: 'checkboxfield',
                fieldLabel: $$iPems.lang.Appointment.Window.Enabled,
                boxLabel: $$iPems.lang.Appointment.Window.EnabledLabel,
                inputValue: true,
                checked: false
            }
        ]
    },
    {
        id: 'node-organization',
        xtype: 'treepanel',
        flex: 3,
        margin: '10 10 10 0',
        glyph: 0xf011,
        autoScroll: true,
        useArrows: false,
        rootVisible: false,
        root: {
            id: 'root',
            text: $$iPems.lang.All,
            icon: $$iPems.icons.Home,
            expanded: true
        },
        viewConfig: {
            loadMask: true
        },
        store: Ext.create('Ext.data.TreeStore', {
            autoLoad: false,
            proxy: {
                type: 'ajax',
                url: '/Project/GetOrganization',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                }
            }
        }),
        tbar: [
                {
                    id: 'node-search-field',
                    xtype: 'textfield',
                    emptyText: $$iPems.lang.PlsInputEmptyText,
                    flex: 1,
                    listeners: {
                        change: function (me, newValue, oldValue, eOpts) {
                            delete me._filterData;
                            delete me._filterIndex;
                        }
                    }
                },
                {
                    id: 'node-search-button',
                    xtype: 'button',
                    glyph: 0xf005,
                    handler: function () {
                        var tree = Ext.getCmp('node-organization'),
                            search = Ext.getCmp('node-search-field'),
                            text = search.getRawValue(),
                            separator = '/',
                            root = tree.getRootNode();

                        if (Ext.isEmpty(text, false)) {
                            return;
                        }

                        if (text.length < 2) {
                            return;
                        }

                        if (search._filterData != null
                            && search._filterIndex != null) {
                            var index = search._filterIndex + 1;
                            var paths = search._filterData;
                            if (index >= paths.length) {
                                index = 0;
                                Ext.Msg.show({ title: $$iPems.lang.SysTipTitle, msg: $$iPems.lang.SearchEndText, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                            }

                            var nodes = Ext.Array.from(paths[index]);
                            var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                            tree.selectPath(path);
                            search._filterIndex = index;
                        } else {
                            Ext.Ajax.request({
                                url: '/Project/SearchOrganization',
                                params: { text: text },
                                mask: new Ext.LoadMask({ target: tree, msg: $$iPems.lang.AjaxHandling }),
                                success: function (response, options) {
                                    var data = Ext.decode(response.responseText, true);
                                    if (data.success) {
                                        var len = data.data.length;
                                        if (len > 0) {
                                            var nodes = Ext.Array.from(data.data[0]);
                                            var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                                            tree.selectPath(path);

                                            search._filterData = data.data;
                                            search._filterIndex = 0;
                                        } else {
                                            Ext.Msg.show({ title: $$iPems.lang.SysTipTitle, msg: Ext.String.format($$iPems.lang.SearchEmptyText, text), buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                        }
                                    } else {
                                        Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                    }
                                }
                            });
                        }
                    }
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
              var form = Ext.getCmp('saveForm').getForm(),
                  result = Ext.getCmp('saveResult'),
                  tree = Ext.getCmp('node-organization'),
                  nodes = [];

              var start = $$iPems.datetimeParse(Ext.getCmp('startTime').getValue()),
                  starttime = start.getTime(),
                  starttime_30 = Ext.Date.subtract(start, Ext.Date.MINUTE, 30).getTime(),
                  nowtime = Ext.Date.now();

              result.setTextWithIcon('', '');
              if (!form.isValid()) {
                  result.setTextWithIcon($$iPems.lang.FormError, 'x-icon-error');
                  return false;
              }

              var ckNodes = tree.getChecked();
              if (ckNodes.length === 0) {
                  result.setTextWithIcon($$iPems.lang.Appointment.NodesError, 'x-icon-error');
                  return false;
              }

              ckNodes.forEach(function (c) {
                  nodes.push(c.getId());
              });

              if (starttime < nowtime) {
                  Ext.Msg.confirm($$iPems.lang.ConfirmWndTitle, $$iPems.lang.Appointment.ConfirmContinue, function (buttonId, text) {
                      if (buttonId === 'yes') {
                          submit(form, nodes, result);
                      }
                  });
              } else if (starttime_30 < nowtime) {
                  Ext.Msg.confirm($$iPems.lang.ConfirmWndTitle, $$iPems.lang.Appointment.ConfirmContinue2, function (buttonId, text) {
                      if (buttonId === 'yes') {
                          submit(form, nodes, result);
                      }
                  });
              } else {
                  submit(form, nodes, result);
              }
          }
      },
      {
          xtype: 'button',
          text: $$iPems.lang.Close,
          handler:function (el, e) {
              saveWnd.close();
          }
      }
    ]
});

var detailCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    Ext.Ajax.request({
        url: '/Project/GetAppointmentDetails',
        Method: 'POST',
        params:{id: record.data.id},
        success: function (response, options) {
            var data = Ext.decode(response.responseText, true);
            if (data.success) {
                var detailarea = detailWnd.getComponent('detail_area');
                var detailstation = detailWnd.getComponent('detail_station');
                var detailroom = detailWnd.getComponent('detail_room');
                var detaildevice = detailWnd.getComponent('detail_device');

                detailarea.setValue(data.data.areas);
                detailstation.setValue(data.data.stations);
                detailroom.setValue(data.data.rooms);
                detaildevice.setValue(data.data.devices);
                detailWnd.show();
            } else {
                Ext.Msg.show({ title: $$iPems.lang.SysErrorTitle, msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
    });
}

var editCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    var basic = saveWnd.getComponent('saveForm').getForm(),
        tree = Ext.getCmp('node-organization'),
        root = tree.getRootNode();

    if (root.hasChildNodes()) {
        root.eachChild(function (c) {
            c.cascadeBy(function (n) {
                n.set('checked', false);
                n.collapse();
            });
        });
    }

    basic.load({
        url: '/Project/GetAppointment',
        params:{id: record.data.id,action: $$iPems.Action.Edit},
        waitMsg: $$iPems.lang.AjaxHandling,
        waitTitle: $$iPems.lang.SysTipTitle,
        success: function (form, action) {
            var separator = '/',
                nodes = action.result.data.nodes;

            if (nodes && nodes.length > 0) {
                Ext.Ajax.request({
                    url: '/Project/GetOrganizationPath',
                    params: { nodes: nodes },
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success) {
                            Ext.defer(function () {
                                Ext.Array.each(data.data, function (item, index, all) {
                                    item = Ext.Array.from(item);

                                    var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), item.join(separator));
                                    tree.selectPath(path, null, null, function (success, lastNode) {
                                        if (success)
                                            lastNode.set('checked', true);
                                    });
                                });
                            }, 100);
                        }
                    }
                });
            }

            form.clearInvalid();
            Ext.getCmp('saveResult').setTextWithIcon('', '');
            saveWnd.setGlyph(0xf002);
            saveWnd.setTitle($$iPems.lang.Appointment.Window.EditTitle);
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
                url: '/Project/DeleteAppointment',
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

var appointmentGridPanel = Ext.create('Ext.grid.Panel', {
    glyph: 0xf045,
    title: $$iPems.lang.Appointment.Title,
    region: 'center',
    store: currentStore,
    columnLines: true,
    disableSelection: false,
    loadMask: true,
    forceFit: false,
    viewConfig: {
        forceFit: true,
        trackOver: true,
        stripeRows: true,
        emptyText: $$iPems.lang.GridEmptyText,
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: $$iPems.lang.Appointment.Columns.Index,
        dataIndex: 'index',
        width: 60,
        align: 'center',
        sortable: true
    }, {
        text: $$iPems.lang.Appointment.Columns.Id,
        dataIndex: 'id',
        width: 150,
        align: 'center',
        sortable: false
    }, {
        text: $$iPems.lang.Appointment.Columns.StartTime,
        dataIndex: 'startTime',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: $$iPems.lang.Appointment.Columns.EndTime,
        dataIndex: 'endTime',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: $$iPems.lang.Project.Columns.Name,
        dataIndex: 'projectName',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: $$iPems.lang.Appointment.Columns.Creator,
        dataIndex: 'creator',
        align: 'center',
        width: 100,
        sortable: true
    }, {
        text: $$iPems.lang.Appointment.Columns.CreatedTime,
        dataIndex: 'createdTime',
        align: 'center',
        width: 150,
        sortable: true
    }, {
        text: $$iPems.lang.Appointment.Columns.Comment,
        dataIndex: 'comment',
        align: 'center',
        width: 250,
        sortable: true
    }, {
        text: $$iPems.lang.Appointment.Columns.Enabled,
        dataIndex: 'enabled',
        align: 'center',
        width: 80,
        sortable: true,
        renderer: function (value) { return value ? $$iPems.lang.StatusTrue : $$iPems.lang.StatusFalse; }
    }, {
        xtype: 'actioncolumn',
        width: 120,
        align: 'center',
        menuDisabled: true,
        menuText: $$iPems.lang.Operate,
        text: $$iPems.lang.Operate,
        items: [{
                    iconCls: 'x-cell-icon x-icon-detail',
                    handler: function (grid, rowIndex, colIndex) {
                        detailCellClick(grid, rowIndex, colIndex);
                    }
                }, {
                    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                        return (r.get('creator') === $$iPems.associatedEmployee) ? 'x-cell-icon x-icon-edit' : 'x-cell-icon x-icon-hidden';
                    },
                    handler: function (grid, rowIndex, colIndex) {
                        editCellClick(grid, rowIndex, colIndex);
                    }
                }
                //此处不是软删除，先屏蔽
                //{
                //    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                //        return (r.get('creator') === $$iPems.associatedEmployee) ? 'x-cell-icon x-icon-delete' : 'x-cell-icon x-icon-hidden';
                //    },
                //    handler: function (grid, rowIndex, colIndex) {
                //        deleteCellClick(grid, rowIndex, colIndex);
                //    }
                //}
        ]
    }],
    dockedItems: [{
        xtype: 'panel',
        dock: 'top',
        items: [
            Ext.create('Ext.toolbar.Toolbar', {
                border: false,
                items: [
                    {
                        id: 'start-datefield',
                        xtype: 'datefield',
                        fieldLabel: $$iPems.lang.Appointment.ToolBar.StartTime,
                        labelWidth: 60,
                        width: 230,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -7),
                        editable: false,
                        allowBlank: false
                    }, {
                        id: 'end-datefield',
                        xtype: 'datefield',
                        fieldLabel: $$iPems.lang.Appointment.ToolBar.EndTime,
                        labelWidth: 60,
                        width: 230,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, +7),
                        editable: false,
                        allowBlank: false
                    }, {
                        xtype: 'button',
                        text: $$iPems.lang.Appointment.ToolBar.AddTitle,
                        glyph: 0xf001,
                        handler: function (el, e) {
                            var basic = saveWnd.getComponent('saveForm').getForm(),
                                tree = Ext.getCmp('node-organization'),
                                root = tree.getRootNode();

                            if (root.hasChildNodes()) {
                                root.eachChild(function (c) {
                                    c.cascadeBy(function (n) {
                                        n.set('checked', false);
                                        n.collapse();
                                    });
                                });
                            }

                            basic.load({
                                url: '/Project/GetAppointment',
                                params:{ action: $$iPems.Action.Add },
                                waitMsg: $$iPems.lang.AjaxHandling,
                                waitTitle: $$iPems.lang.SysTipTitle,
                                success: function (form, action) {
                                    var combo = Ext.getCmp('projectId'),
                                        comboStore = combo.getStore();

                                    if (comboStore.getCount() > 0)
                                        combo.select(comboStore.getAt(0));

                                    form.clearInvalid();
                                    Ext.getCmp('projectId').setReadOnly(false);
                                    Ext.getCmp('saveResult').setTextWithIcon('', '');
                                    saveWnd.setGlyph(0xf001);
                                    saveWnd.setTitle($$iPems.lang.Appointment.Window.AddTitle);
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
                                url: '/Project/DownloadAppointments',
                                params: {
                                    startTime: params.startTime,
                                    entTime: params.endTime,
                                    type: params.type,
                                    keyWord: params.keyWord
                                }
                            });
                        }
                    }]
            }),
            Ext.create('Ext.toolbar.Toolbar', {
                border: false,
                items: [
                    {
                        id: 'type-combobox',
                        xtype: 'combobox',
                        fieldLabel: $$iPems.lang.Appointment.ToolBar.Type,
                        labelWidth: 60,
                        width: 230,
                        store: typeStore,
                        align: 'center',
                        value: -1,
                        editable: false,
                        displayField: 'name',
                        valueField: 'id',
                    }, {
                        id: 'keyword-textbox',
                        xtype: 'textfield',
                        labelWidth: 50,
                        labelAlign: 'center',
                        width: 230,
                        maxLength: 100,
                        emptyText: $$iPems.lang.MultiConditionEmptyText,
                    }, {
                        xtype: 'button',
                        text: $$iPems.lang.Query,
                        glyph: 0xf005,
                        handler: function (el, e) {
                            var begin = Ext.getCmp('start-datefield').getRawValue(),
                                    end = Ext.getCmp('end-datefield').getRawValue(),
                                    type = Ext.getCmp('type-combobox').getValue(),
                                    keyWord = Ext.getCmp('keyword-textbox').getRawValue();

                            var proxy = currentStore.getProxy();
                            proxy.extraParams.startTime = begin;
                            proxy.extraParams.endTime = end;
                            proxy.extraParams.type = type;
                            proxy.extraParams.keyWord = keyWord;
                            currentStore.loadPage(1);
                        }
                    }
                ]
            })
        ]
    }],
    bbar: currentPagingToolbar
});

Ext.onReady(function () {
    /*add components to viewport panel*/
    var pageContentPanel = Ext.getCmp('center-content-panel-fw');
    if (!Ext.isEmpty(pageContentPanel)) {
        pageContentPanel.add(appointmentGridPanel);

        projectStore.load();
        currentStore.load();
    }
});