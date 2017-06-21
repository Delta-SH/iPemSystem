Ext.define('AppointmentModel', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'index', type: 'int' },
        { name: 'id', type: 'string' },
        { name: 'startDate', type: 'string' },
        { name: 'endDate', type: 'string' },
        { name: 'projectName', type: 'string' },
        { name: 'projectId', type: 'string' },
        { name: 'creator', type: 'string' },
        { name: 'createdTime', type: 'string' },
        { name: 'comment', type: 'string' },
        { name: 'enabled', type: 'boolean' }
    ],
    idProperty: 'id'
});

var query = function (store) {
    var start = Ext.getCmp('start-datefield').getRawValue(),
        end = Ext.getCmp('end-datefield').getRawValue(),
        type = Ext.getCmp('type-combobox').getValue(),
        keyWord = Ext.getCmp('keyword-textbox').getRawValue();

    var proxy = store.getProxy();
    proxy.extraParams.startDate = start;
    proxy.extraParams.endDate = end;
    proxy.extraParams.type = type;
    proxy.extraParams.keyWord = keyWord;
    currentStore.loadPage(1);
};

var download = function (store) {
    var params = store.getProxy().extraParams;
    $$iPems.download({
        url: '/Project/DownloadAppointments',
        params: params
    });
};

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
        listeners: {
            exception: function (proxy, response, operation) {
                Ext.Msg.show({ title: '系统错误', msg: operation.getError(), buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
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
        { "id": -1, "name": '按工程名称' },
        { "id": $$iPems.SSH.Area, "name": '按区域名称' },
        { "id": $$iPems.SSH.Station, "name": '按站点名称' },
        { "id": $$iPems.SSH.Room, "name": '按机房名称' },
        { "id": $$iPems.SSH.Device, "name": '按设备名称' }
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
    result.setTextWithIcon('正在处理...', 'x-icon-loading');
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
    title: '预约详情',
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
        fieldLabel: '预约区域'
    }, {
        itemId: 'detail_station',
        labelWidth: 60,
        fieldLabel: '预约站点'
    }, {
        itemId: 'detail_room',
        labelWidth: 60,
        fieldLabel: '预约机房'
    }, {
        itemId: 'detail_device',
        labelWidth: 60,
        fieldLabel: '预约设备'
    }],
    buttonAlign: 'right',
    buttons: [{
        xtype: 'button',
        text: '关闭',
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
                fieldLabel: '工程名称',
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
                id: 'startDate',
                name: 'startDate',
                xtype: 'datetimepicker',
                fieldLabel: '开始时间',
                editable: false,
                allowBlank: false
            },
            {
                id: 'endDate',
                name: 'endDate',
                xtype: 'datetimepicker',
                fieldLabel: '结束时间',
                editable: false,
                allowBlank: false
            },
            {
                id: 'comment',
                name: 'comment',
                xtype: 'textareafield',
                fieldLabel: '备注信息',
                height: 70
            },
            {
                id: 'enabled',
                name: 'enabled',
                xtype: 'checkboxfield',
                fieldLabel: '预约状态',
                boxLabel: '(勾选表示启用)',
                inputValue: true,
                checked: false
            }
        ]
    },
    {
        id: 'organization',
        xtype: 'treepanel',
        flex: 3,
        margin: '10 10 10 0',
        glyph: 0xf011,
        autoScroll: true,
        useArrows: false,
        rootVisible: false,
        root: {
            id: 'root',
            text: '全部',
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
                url: '/Component/GetDevices',
                reader: {
                    type: 'json',
                    successProperty: 'success',
                    messageProperty: 'message',
                    totalProperty: 'total',
                    root: 'data'
                },
                extraParams: {
                    multiselect: true,
                    leafselect: false
                }
            }
        }),
        tbar: [
                {
                    id: 'organization-search-field',
                    xtype: 'textfield',
                    emptyText: '请输入筛选条件...',
                    flex: 1,
                    listeners: {
                        change: function (me, newValue, oldValue, eOpts) {
                            delete me._filterData;
                            delete me._filterIndex;
                        }
                    }
                },
                {
                    xtype: 'button',
                    glyph: 0xf005,
                    handler: function () {
                        var tree = Ext.getCmp('organization'),
                            search = Ext.getCmp('organization-search-field'),
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
                                Ext.Msg.show({ title: '系统提示', msg: '搜索完毕', buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                            }

                            var nodes = Ext.Array.from(paths[index]);
                            var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                            tree.selectPath(path);
                            search._filterIndex = index;
                        } else {
                            Ext.Ajax.request({
                                url: '/Component/FilterRoomPath',
                                params: { text: text },
                                mask: new Ext.LoadMask({ target: tree, msg: '正在处理...' }),
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
                                            Ext.Msg.show({ title: '系统提示', msg: Ext.String.format('未找到指定内容:<br/>{0}', text), buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                                        }
                                    } else {
                                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
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
          text: '保存',
          handler: function (el, e) {
              var form = Ext.getCmp('saveForm').getForm(),
                  result = Ext.getCmp('saveResult'),
                  tree = Ext.getCmp('organization'),
                  nodes = [];

              var start = $$iPems.datetimeParse(Ext.getCmp('startDate').getValue()),
                  starttime = start.getTime(),
                  starttime_30 = Ext.Date.subtract(start, Ext.Date.MINUTE, 30).getTime(),
                  nowtime = Ext.Date.now();

              result.setTextWithIcon('', '');
              if (!form.isValid()) {
                  result.setTextWithIcon('表单填写错误', 'x-icon-error');
                  return false;
              }

              var ckNodes = tree.getChecked();
              if (ckNodes.length === 0) {
                  result.setTextWithIcon('请勾选需要预约的监控点', 'x-icon-error');
                  return false;
              }

              ckNodes.forEach(function (c) {
                  nodes.push(c.getId());
              });

              if (starttime < nowtime) {
                  Ext.Msg.confirm('确认对话框', '开始时间早于当前时间，您确定要继续吗？', function (buttonId, text) {
                      if (buttonId === 'yes') {
                          submit(form, nodes, result);
                      }
                  });
              } else if (starttime_30 < nowtime) {
                  Ext.Msg.confirm('确认对话框', '开始时间距现在不足30分钟，您确定要继续吗？', function (buttonId, text) {
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
          text: '关闭',
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
                Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
            }
        }
    });
}

var editCellClick = function (grid, rowIndex, colIndex) {
    var record = grid.getStore().getAt(rowIndex);
    if (Ext.isEmpty(record)) return false;

    var basic = saveWnd.getComponent('saveForm').getForm(),
        tree = Ext.getCmp('organization'),
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
        waitMsg: '正在处理...',
        waitTitle: '系统提示',
        success: function (form, action) {
            var separator = '/',
                nodes = action.result.data.nodes;

            if (nodes && nodes.length > 0) {
                Ext.Ajax.request({
                    url: '/Component/GetDevicePath',
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
            saveWnd.setTitle('编辑预约');
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
                url: '/Project/DeleteAppointment',
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

var appointmentGridPanel = Ext.create('Ext.grid.Panel', {
    glyph: 0xf045,
    title: '工程预约管理',
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
        emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
        preserveScrollOnRefresh: true
    },
    columns: [{
        text: '序号',
        dataIndex: 'index',
        width: 60,
        sortable: true
    }, {
        text: '预约编号',
        dataIndex: 'id',
        sortable: false
    }, {
        text: '开始时间',
        dataIndex: 'startDate',
        sortable: true
    }, {
        text: '结束时间',
        dataIndex: 'endDate',
        sortable: true
    }, {
        text: '工程名称',
        dataIndex: 'projectName',
        sortable: true
    }, {
        text: '创建人',
        dataIndex: 'creator',
        sortable: true
    }, {
        text: '创建时间',
        dataIndex: 'createdTime',
        sortable: true
    }, {
        text: '备注信息',
        dataIndex: 'comment',
        sortable: true
    }, {
        text: '预约状态',
        dataIndex: 'enabled',
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
                    iconCls: 'x-cell-icon x-icon-detail',
                    handler: function (grid, rowIndex, colIndex) {
                        detailCellClick(grid, rowIndex, colIndex);
                    }
                }, {
                    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                        return (r.get('creator') === $$iPems.currentEmployee) ? 'x-cell-icon x-icon-edit' : 'x-cell-icon x-icon-hidden';
                    },
                    handler: function (grid, rowIndex, colIndex) {
                        editCellClick(grid, rowIndex, colIndex);
                    }
                }
                //{
                //    getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                //        return (r.get('creator') === $$iPems.currentEmployee) ? 'x-cell-icon x-icon-delete' : 'x-cell-icon x-icon-hidden';
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
                        id: 'type-combobox',
                        xtype: 'combobox',
                        fieldLabel: '筛选类型',
                        labelWidth: 60,
                        width: 250,
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
                        width: 250,
                        maxLength: 100,
                        emptyText: '多条件请以;分隔，例: A;B;C',
                    }, {
                        xtype: 'button',
                        text: '数据查询',
                        glyph: 0xf005,
                        handler: function (el, e) {
                            query(currentStore);
                        }
                    }, '-', {
                        xtype: 'button',
                        text: '数据导出',
                        glyph: 0xf010,
                        handler: function (el, e) {
                            download(currentStore);
                        }
                    }]
            }),
            Ext.create('Ext.toolbar.Toolbar', {
                border: false,
                items: [
                    {
                        id: 'start-datefield',
                        xtype: 'datefield',
                        fieldLabel: '开始时间',
                        labelWidth: 60,
                        width: 250,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, -7),
                        editable: false,
                        allowBlank: false
                    }, {
                        id: 'end-datefield',
                        xtype: 'datefield',
                        fieldLabel: '结束时间',
                        labelWidth: 60,
                        width: 250,
                        value: Ext.Date.add(new Date(), Ext.Date.DAY, +7),
                        editable: false,
                        allowBlank: false
                    }, {
                        xtype: 'button',
                        text: '新增预约',
                        glyph: 0xf001,
                        handler: function (el, e) {
                            var basic = saveWnd.getComponent('saveForm').getForm(),
                                tree = Ext.getCmp('organization'),
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
                                waitMsg: '正在处理...',
                                waitTitle: '系统提示',
                                success: function (form, action) {
                                    form.clearInvalid();
                                    Ext.getCmp('projectId').setReadOnly(false);
                                    Ext.getCmp('saveResult').setTextWithIcon('', '');

                                    var combo = Ext.getCmp('projectId'),
                                        comboStore = combo.getStore();

                                    if (comboStore.getCount() > 0)
                                        combo.select(comboStore.getAt(0));

                                    saveWnd.setGlyph(0xf001);
                                    saveWnd.setTitle('新增预约');
                                    saveWnd.opaction = $$iPems.Action.Add;
                                    saveWnd.show();
                                }
                            });
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

        //load data
        projectStore.load();
        query(currentStore);
    }
});