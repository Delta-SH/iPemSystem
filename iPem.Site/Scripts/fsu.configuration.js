(function () {
    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'fsuid', type: 'string' },
            { name: 'fsucode', type: 'string' },
            { name: 'fsu', type: 'string' },
            { name: 'vendor', type: 'string' },
			{ name: 'deviceid', type: 'string' },
            { name: 'devicecode', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'pointid', type: 'string' },
            { name: 'pointcode', type: 'string' },
            { name: 'pointnumber', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'typeid', type: 'int' },
            { name: 'type', type: 'string' },
            { name: 'absolute', type: 'string' },
            { name: 'relative', type: 'string' },
            { name: 'interval', type: 'string' },
            { name: 'reftime', type: 'string' },
            { name: 'remote', type: 'boolean' }
        ],
        idProperty: 'index'
    });

    Ext.define('AlarmModel', {
        extend: 'Ext.data.Model',
        fields: [
            { name: 'index', type: 'int' },
            { name: 'fsuid', type: 'string' },
            { name: 'fsucode', type: 'string' },
            { name: 'fsu', type: 'string' },
            { name: 'vendor', type: 'string' },
			{ name: 'deviceid', type: 'string' },
            { name: 'devicecode', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'pointid', type: 'string' },
            { name: 'pointcode', type: 'string' },
            { name: 'pointnumber', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'typeid', type: 'int' },
            { name: 'type', type: 'string' },
            { name: 'threshold', type: 'string' },
            { name: 'level', type: 'string' },
            { name: 'nmid', type: 'string' },
            { name: 'remote', type: 'boolean' }
        ],
        idProperty: 'index'
    });

    var pointStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'PointModel',
        downloadURL: '/Fsu/DownloadPoints',
        proxy: {
            type: 'ajax',
            url: '/Fsu/RequestPoints',
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

    var alarmStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'AlarmModel',
        downloadURL: '/Fsu/DownloadAlarmPoints',
        proxy: {
            type: 'ajax',
            url: '/Fsu/RequestAlarmPoints',
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

    var pointPagingToolbar = $$iPems.clonePagingToolbar(pointStore);
    var alarmPagingToolbar = $$iPems.clonePagingToolbar(alarmStore);

    var pointGrid = Ext.create('Ext.grid.Panel', {
        title: '存储规则',
        glyph: 0xf062,
        selType: 'cellmodel',
        border: false,
        store: pointStore,
        bbar: pointPagingToolbar,
        pager: pointPagingToolbar,
        plugins: [
            Ext.create('Ext.grid.plugin.CellEditing', {
                clicksToEdit: 1
            })
        ],
        viewConfig: {
            loadMask: true,
            stripeRows: true,
            trackOver: false,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
        },
        columns: [
            {
                text: '序号',
                dataIndex: 'index',
                width: 60
            },
            {
                text: 'FSU编码',
                dataIndex: 'fsucode',
                align: 'left',
                width: 150
            },
            {
                text: '所属FSU',
                dataIndex: 'fsu',
                align: 'left',
                width: 150
            },
            {
                text: '所属厂家',
                dataIndex: 'vendor',
                align: 'left',
                width: 150
            },
            {
                text: '所属设备',
                dataIndex: 'device',
                align: 'left',
                width: 150
            },
            {
                text: '信号名称',
                dataIndex: 'point',
                align: 'left',
                width: 200
            },
            {
                text: '信号类型',
                dataIndex: 'type',
                align: 'center',
                width: 100
            },
            {
                text: '绝对阀值',
                dataIndex: 'absolute',
                align: 'center',
                width: 150,
                editor: {
                    xtype: 'numberfield',
                    decimalPrecision: 3,
                    allowBlank: false
                }
            },
            {
                text: '百分比阀值',
                dataIndex: 'relative',
                align: 'center',
                width: 150,
                editor: {
                    xtype: 'numberfield',
                    decimalPrecision: 3,
                    allowBlank: false
                }
            },
            {
                text: '存储时间间隔（分钟）',
                dataIndex: 'interval',
                align: 'center',
                width: 150,
                editor: {
                    xtype: 'numberfield',
                    allowDecimals: false,
                    allowBlank: false
                }
            },
            {
                text: '存储参考时间',
                dataIndex: 'reftime',
                align: 'center',
                width: 150,
                editor: {
                    xtype: 'textfield',
                    allowBlank: false
                }
            }
        ],
        listeners: {
            beforeedit: function (editor, e) {
                if (e.record.get('remote') !== true)
                    return false;
            }
        },
        dockedItems: [
            {
                xtype: 'panel',
                dock: 'top',
                items: [
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [
                            {
                                id: 'point-getting',
                                xtype: 'button',
                                glyph: 0xf060,
                                text: '读取配置',
                                disabled: true,
                                handler: function (me, event) {
                                    gettingPoints();
                                }
                            }, '-',
                            {
                                id: 'point-setting',
                                xtype: 'button',
                                glyph: 0xf059,
                                text: '下发配置',
                                disabled: true,
                                handler: function (me, event) {
                                    settingPoints();
                                }
                            }, '-',
                            {
                                id: 'point-all-setting',
                                xtype: 'button',
                                glyph: 0xf061,
                                text: '批量设置',
                                disabled: true,
                                handler: function (me, event) {
                                    pointWnd.show();
                                }
                            }, '-',
                            {
                                id: 'point-export',
                                xtype: 'button',
                                glyph: 0xf010,
                                text: '数据导出',
                                disabled: true,
                                handler: function (me, event) {
                                    print(pointStore);
                                }
                            }
                        ]
                    }
                ]
            }
        ]
    });

    var alarmGrid = Ext.create('Ext.grid.Panel', {
        title: '告警门限',
        glyph: 0xf028,
        selType: 'cellmodel',
        border: false,
        store: alarmStore,
        bbar: alarmPagingToolbar,
        pager: alarmPagingToolbar,
        plugins: [
            Ext.create('Ext.grid.plugin.CellEditing', {
                clicksToEdit: 1
            })
        ],
        viewConfig: {
            loadMask: true,
            stripeRows: true,
            trackOver: false,
            emptyText: '<h1 style="margin:20px">没有数据记录</h1>'
        },
        columns: [
            {
                text: '序号',
                dataIndex: 'index',
                width: 60
            },
            {
                text: 'FSU编码',
                dataIndex: 'fsucode',
                align: 'left',
                width: 150
            },
            {
                text: '所属FSU',
                dataIndex: 'fsu',
                align: 'left',
                width: 150
            },
            {
                text: '所属厂家',
                dataIndex: 'vendor',
                align: 'left',
                width: 150
            },
            {
                text: '所属设备',
                dataIndex: 'device',
                align: 'left',
                width: 150
            },
            {
                text: '信号名称',
                dataIndex: 'point',
                align: 'left',
                width: 200
            },
            {
                text: '信号类型',
                dataIndex: 'type',
                align: 'center',
                width: 100
            },
            {
                text: '告警门限',
                dataIndex: 'threshold',
                align: 'center',
                width: 150,
                editor: {
                    xtype: 'numberfield',
                    decimalPrecision: 3,
                    allowBlank: false
                }
            },
            {
                text: '告警等级',
                dataIndex: 'level',
                align: 'center',
                width: 150,
                editor: {
                    xtype: 'BIAlarmLevelCombo',
                    fieldLabel: null,
                }
            },
            {
                text: '网管告警编号',
                dataIndex: 'nmid',
                align: 'center',
                width: 150,
                editor: {
                    xtype: 'textfield',
                    allowBlank: false
                }
            }
        ],
        listeners: {
            beforeedit: function (editor, e) {
                if (e.record.get('remote') !== true)
                    return false;
            }
        },
        dockedItems: [
            {
                xtype: 'panel',
                dock: 'top',
                items: [
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [
                            {
                                id: 'almpoint-getting',
                                xtype: 'button',
                                glyph: 0xf060,
                                text: '读取配置',
                                disabled: true,
                                handler: function (me, event) {
                                    gettingAlmPoints();
                                }
                            }, '-',
                            {
                                id: 'almpoint-setting',
                                xtype: 'button',
                                glyph: 0xf059,
                                text: '下发配置',
                                disabled: true,
                                handler: function (me, event) {
                                    settingAlmPoints();
                                }
                            }, '-',
                            {
                                id: 'almpoint-all-setting',
                                xtype: 'button',
                                glyph: 0xf061,
                                text: '批量设置',
                                disabled: true,
                                handler: function (me, event) {
                                    alarmWnd.show();
                                }
                            }, '-',
                            {
                                id: 'almpoint-export',
                                xtype: 'button',
                                glyph: 0xf010,
                                text: '数据导出',
                                disabled: true,
                                handler: function (me, event) {
                                    print(alarmStore);
                                }
                            }
                        ]
                    }
                ]
            }
        ]
    });

    var currentTab = Ext.create('Ext.tab.Panel', {
        xtype: 'tabpanel',
        margin: '5 0 0 0',
        flex: 1,
        items: [pointGrid, alarmGrid]
    });

    var currentLayout = Ext.create('Ext.panel.Panel', {
        region: 'center',
        border: false,
        bodyCls: 'x-border-body-panel',
        layout: {
            type: 'vbox',
            align: 'stretch',
            pack: 'start'
        },
        dockedItems: [
            {
                xtype: 'panel',
                glyph: 0xf034,
                title: '筛选条件(仅支持具体设备或某类信号名称查询)',
                collapsible: true,
                collapsed: false,
                dock: 'top',
                items: [
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [
                            {
                                id: 'rangePicker',
                                xtype: 'DevicePicker',
                                fieldLabel: '查询范围',
                                emptyText: '默认全部',
                                labelWidth: 60,
                                width: 280
                            }, {
                                id: 'vendor-multicombo',
                                xtype: 'VendorMultiCombo',
                                emptyText: '默认全部',
                                width: 280
                            },
                            {
                                xtype: 'button',
                                glyph: 0xf005,
                                text: '数据查询',
                                handler: function (me, event) {
                                    query();
                                }
                            }
                        ]
                    },
                    {
                        xtype: 'toolbar',
                        border: false,
                        items: [
                            {
                                id: 'point-type-multicombo',
                                xtype: 'PointTypeMultiCombo',
                                emptyText: '默认全部',
                                width: 280
                            },
                            {
                                id: 'point-multipicker',
                                xtype: 'PointMultiPicker',
                                emptyText: '默认全部'
                            }
                        ]
                    }
                ]
            }
        ],
        items: [currentTab]
    });

    var pointWnd = Ext.create('Ext.window.Window', {
        title: '批量设置[存储规则]',
        height: 350,
        width: 500,
        modal: true,
        border: false,
        hidden: true,
        glyph: 0xf061,
        closeAction: 'hide',
        items: [{
            id: 'pointForm',
            xtype: 'form',
            border: false,
            defaultType: 'textfield',
            fieldDefaults: {
                labelAlign: 'top',
                margin: '15 15 15 15',
                anchor: '100%'
            },
            items: [
                {
                    id: 'AbsoluteVal',
                    name: 'AbsoluteVal',
                    xtype: 'numberfield',
                    decimalPrecision: 3,
                    fieldLabel: '绝对阀值',
                    allowBlank: false,
                    margin: '15 15 0 15'
                },
                {
                    id: 'RelativeVal',
                    name: 'RelativeVal',
                    xtype: 'numberfield',
                    decimalPrecision: 3,
                    fieldLabel: '百分比阀值',
                    allowBlank: false,
                    margin: '15 15 0 15'
                }, {
                    id: 'StorageInterval',
                    name: 'StorageInterval',
                    xtype: 'numberfield',
                    allowDecimals: false,
                    fieldLabel: '存储时间间隔（分钟）',
                    allowBlank: false,
                    margin: '15 15 0 15'
                }, {
                    id: 'StorageRefTime',
                    name: 'StorageRefTime',
                    xtype: 'textfield',
                    fieldLabel: '存储参考时间',
                    allowBlank: false,
                    margin: '15 15 0 15'
                }
            ]
        }],
        buttons: [
          { id: 'pointResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button', text: '保存', handler: function (el, e) {
                  var form = Ext.getCmp('pointForm').getForm(),
                      result = Ext.getCmp('pointResult');

                  result.setTextWithIcon('', '');
                  if (form.isValid()) {
                      Ext.Msg.confirm('确认对话框', '您确认要批量下发配置吗？', function (buttonId, text) {
                          if (buttonId === 'yes') {
                              result.setTextWithIcon('正在处理...', 'x-icon-loading');
                              form.submit({
                                  submitEmptyText: false,
                                  clientValidation: true,
                                  preventWindow: true,
                                  url: '/Fsu/SetRemoteAllPoints',
                                  params: pointStore.getProxy().extraParams,
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
                  }
              }
          },
          {
              xtype: 'button', text: '关闭', handler: function (el, e) {
                  pointWnd.close();
              }
          }
        ]
    });

    var alarmWnd = Ext.create('Ext.window.Window', {
        title: '批量设置[告警门限]',
        height: 350,
        width: 500,
        modal: true,
        border: false,
        hidden: true,
        glyph: 0xf061,
        closeAction: 'hide',
        items: [{
            id: 'alarmForm',
            xtype: 'form',
            border: false,
            defaultType: 'textfield',
            fieldDefaults: {
                labelAlign: 'top',
                margin: '15 15 15 15',
                anchor: '100%'
            },
            items: [
                {
                    id: 'Threshold',
                    name: 'Threshold',
                    xtype: 'numberfield',
                    decimalPrecision: 3,
                    fieldLabel: '告警门限值',
                    allowBlank: false,
                    margin: '15 15 0 15'
                },
                {
                    id: 'AlarmLevel',
                    name: 'AlarmLevel',
                    xtype: 'BIAlarmLevelCombo',
                    margin: '15 15 0 15'
                }, {
                    id: 'NMAlarmID',
                    name: 'NMAlarmID',
                    xtype: 'textfield',
                    fieldLabel: '网管告警编号',
                    allowBlank: false,
                    margin: '15 15 0 15'
                }
            ]
        }],
        buttons: [
          { id: 'alarmResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button', text: '保存', handler: function (el, e) {
                  var form = Ext.getCmp('alarmForm').getForm(),
                      result = Ext.getCmp('alarmResult');

                  result.setTextWithIcon('', '');
                  if (form.isValid()) {
                      Ext.Msg.confirm('确认对话框', '您确认要批量下发配置吗？', function (buttonId, text) {
                          if (buttonId === 'yes') {
                              result.setTextWithIcon('正在处理...', 'x-icon-loading');
                              form.submit({
                                  submitEmptyText: false,
                                  clientValidation: true,
                                  preventWindow: true,
                                  url: '/Fsu/SetRemoteAllAlmPoints',
                                  params: alarmStore.getProxy().extraParams,
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
                  }
              }
          },
          {
              xtype: 'button', text: '关闭', handler: function (el, e) {
                  alarmWnd.close();
              }
          }
        ]
    });

    var query = function () {
        var parent = Ext.getCmp('rangePicker').getValue(),
            points = Ext.getCmp('point-multipicker').getValue(),
            types = Ext.getCmp('point-type-multicombo').getSelectedValues(),
            vendors = Ext.getCmp('vendor-multicombo').getSelectedValues(),
            proxy1 = pointStore.getProxy(),
            proxy2 = alarmStore.getProxy();

        proxy1.extraParams.parent = proxy2.extraParams.parent = parent;
        proxy1.extraParams.points = proxy2.extraParams.points = points;
        proxy1.extraParams.types = proxy2.extraParams.types = types;
        proxy1.extraParams.vendors = proxy2.extraParams.vendors = vendors;
        proxy1.extraParams.cache = proxy2.extraParams.cache = false;

        Ext.getCmp('point-setting').setDisabled(true);
        Ext.getCmp('almpoint-setting').setDisabled(true);

        pointStore.loadPage(1, {
            callback: function (records, operation, success) {
                proxy1.extraParams.cache = success;
                var disabled = !(records.length > 0);
                Ext.getCmp('point-getting').setDisabled(disabled);
                Ext.getCmp('point-all-setting').setDisabled(disabled);
                Ext.getCmp('point-export').setDisabled(disabled);
            }
        });

        alarmStore.loadPage(1, {
            callback: function (records, operation, success) {
                proxy2.extraParams.cache = success;
                var disabled = !(records.length > 0);
                Ext.getCmp('almpoint-getting').setDisabled(disabled);
                Ext.getCmp('almpoint-all-setting').setDisabled(disabled);
                Ext.getCmp('almpoint-export').setDisabled(disabled);
            }
        });
    };

    var print = function (store) {
        $$iPems.download({
            url: store.downloadURL,
            params: store.getProxy().extraParams
        });
    };

    var gettingPoints = function () {
        Ext.Msg.confirm('确认对话框', '您确认要读取配置吗？', function (buttonId, text) {
            if (buttonId === 'yes') {
                Ext.Ajax.request({
                    url: '/Fsu/RequestRemotePoints',
                    params: pointStore.getProxy().extraParams,
                    mask: new Ext.LoadMask(pointGrid, { msg: '正在处理...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success) {
                            Ext.getCmp('point-setting').setDisabled(false);
                            pointPagingToolbar.doRefresh();
                        } else {
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                        }
                    }
                });
            }
        });
    };

    var settingPoints = function () {
        var records = pointStore.getModifiedRecords();
        if (records.length == 0) {
            Ext.Msg.show({ title: '系统警告', msg: '您未修改任何配置，无需下发。', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
            return false;
        }

        Ext.Msg.confirm('确认对话框', '您确认要下发配置吗？', function (buttonId, text) {
            if (buttonId === 'yes') {
                var settings = [];
                Ext.Array.each(records, function (item, index) {
                    settings.push(item.getData());
                });

                Ext.Ajax.request({
                    url: '/Fsu/SetRemotePoints',
                    method: 'POST',
                    jsonData: settings,
                    mask: new Ext.LoadMask(pointGrid, { msg: '正在处理...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success) {
                            Ext.Msg.show({ title: '系统提示', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                        } else {
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                        }
                    }
                });
            }
        });
    };

    var gettingAlmPoints = function () {
        Ext.Msg.confirm('确认对话框', '您确认要读取配置吗？', function (buttonId, text) {
            if (buttonId === 'yes') {
                Ext.Ajax.request({
                    url: '/Fsu/RequestRemoteAlmPoints',
                    params: alarmStore.getProxy().extraParams,
                    mask: new Ext.LoadMask(alarmGrid, { msg: '正在处理...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success) {
                            Ext.getCmp('almpoint-setting').setDisabled(false);
                            alarmPagingToolbar.doRefresh();
                        } else {
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                        }
                    }
                });
            }
        });
    };

    var settingAlmPoints = function () {
        var records = alarmStore.getModifiedRecords();
        if (records.length == 0) {
            Ext.Msg.show({ title: '系统警告', msg: '您未修改任何配置，无需下发。', buttons: Ext.Msg.OK, icon: Ext.Msg.WARNING });
            return false;
        }

        Ext.Msg.confirm('确认对话框', '您确认要下发配置吗？', function (buttonId, text) {
            if (buttonId === 'yes') {
                var settings = [];
                Ext.Array.each(records, function (item, index) {
                    settings.push(item.getData());
                });

                Ext.Ajax.request({
                    url: '/Fsu/SetRemoteAlmPoints',
                    method: 'POST',
                    jsonData: settings,
                    mask: new Ext.LoadMask(alarmGrid, { msg: '正在处理...' }),
                    success: function (response, options) {
                        var data = Ext.decode(response.responseText, true);
                        if (data.success) {
                            Ext.Msg.show({ title: '系统提示', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.INFO });
                        } else {
                            Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                        }
                    }
                });
            }
        });
    };

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);

            //load data
            //Ext.defer(query, 2000);
        }
    });
})();