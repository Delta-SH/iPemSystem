(function () {
    var gaugeChart = null,
        lineChart = null,
        gaugeOption = {
            tooltip: {
                formatter: '{c} {b}'
            },
            series: [
                {
                    name: '实时测值',
                    type: 'gauge',
                    center: ['50%', 100],
                    radius: '100%',
                    title: {
                        offsetCenter: [0, -30],
                        textStyle: {
                            color: '#005eaa',
                            fontWeight: 'bolder',
                            fontSize: 16
                        }
                    },
                    detail: {
                        offsetCenter: [0, 30],
                        textStyle: {
                            fontSize: 18
                        }
                    },
                    data: [{ value: 0, name: 'kW·h' }]
                }
            ]
        },
        lineOption = {
            tooltip: {
                trigger: 'axis',
                formatter: '{b}: {c} {a}'
            },
            grid: {
                top: 15,
                left: 0,
                right: 5,
                bottom: 0,
                containLabel: true
            },
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                splitLine: { show: false },
                data: []
            }],
            yAxis: [{
                type: 'value'
            }],
            series: [
                {
                    name: 'kW·h',
                    type: 'line',
                    smooth: true,
                    symbol: 'none',
                    sampling: 'average',
                    itemStyle: {
                        normal: {
                            color: '#0892cd'
                        }
                    },
                    areaStyle: { normal: {} },
                    data: []
                }
            ]
        };

    var resetChart = function () {
        gaugeOption.series[0].min = 0;
        gaugeOption.series[0].max = 100;
        gaugeOption.series[0].data[0].value = 0;
        gaugeOption.series[0].data[0].name = 'kW·h';
        gaugeChart.setOption(gaugeOption, true);

        lineOption.series[0].name = 'kW·h';
        lineOption.series[0].data = [];
        lineOption.xAxis[0].data = [];
        lineChart.setOption(lineOption, true);
    };

    var loadChart = function (record) {
        if (record != null) {
            var maxcount = 60,
                timestamp = record.get('timestamp'),
                value = record.get('value'),
                unit = record.get('unit');

            if (value >= 0) {
                if (value <= 100) {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 100;
                } else if (value > 100 && value <= 500) {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 500;
                } else if (value > 500 && value <= 1000) {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 1000;
                } else if (value > 1000 && value <= 5000) {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 5000;
                } else {
                    gaugeOption.series[0].min = 0;
                    gaugeOption.series[0].max = 10000;
                }
            } else {
                if (value >= -100) {
                    gaugeOption.series[0].min = -100;
                    gaugeOption.series[0].max = 0;
                } else if (value < -100 && value >= -500) {
                    gaugeOption.series[0].min = -500;
                    gaugeOption.series[0].max = 0;
                } else if (value < -500 && value >= -1000) {
                    gaugeOption.series[0].min = -1000;
                    gaugeOption.series[0].max = 0;
                } else if (value < -1000 && value >= -5000) {
                    gaugeOption.series[0].min = -5000;
                    gaugeOption.series[0].max = 0;
                } else {
                    gaugeOption.series[0].min = -10000;
                    gaugeOption.series[0].max = 0;
                }
            }

            gaugeOption.series[0].data[0].name = unit;
            gaugeOption.series[0].data[0].value = value;
            gaugeChart.setOption(gaugeOption, true);

            if (lineOption.series[0].data.length > maxcount) {
                lineOption.series[0].data.shift();
                lineOption.xAxis[0].data.shift();
            }

            lineOption.series[0].name = unit;
            lineOption.series[0].data.push(value);
            lineOption.xAxis[0].data.push(timestamp);
            lineChart.setOption(lineOption, true);
        }
    };

    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
			{ name: 'index', type: 'int' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'device', type: 'string' },
            { name: 'point', type: 'string' },
            { name: 'type', type: 'string' },
            { name: 'value', type: 'string' },
            { name: 'unit', type: 'string' },
            { name: 'status', type: 'string' },
            { name: 'time', type: 'string' },
            { name: 'devid', type: 'string' },
            { name: 'pointid', type: 'string' },
            { name: 'typeid', type: 'int' },
            { name: 'statusid', type: 'int' },
            { name: 'level', type: 'int' },
            { name: 'rsspoint', type: 'boolean' },
            { name: 'rssfrom', type: 'boolean' },
            { name: 'timestamp', type: 'string' }
        ],
        idProperty: 'index'
    });

    var change = function (node, pagingtoolbar) {
        var me = pagingtoolbar.store,
            id = node.getId(),
            ids = $$iPems.SplitKeys(id),
            columns = Ext.getCmp('points-grid').columns;

        if (id !== 'root'
            && ids.length === 2
            && parseInt(ids[0]) === $$iPems.Organization.Device) {
            columns[1].hide();
            columns[2].hide();
            columns[3].hide();
            columns[4].hide();
        } else {
            columns[1].show();
            columns[2].show();
            columns[3].show();
            columns[4].show();
        }

        resetChart();
        me.proxy.extraParams.node = node.getId();
        me.loadPage(1);
    };

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'PointModel',
        groupField: 'type',
        groupDir: 'undefined',
        sortOnLoad: false,
        proxy: {
            type: 'ajax',
            url: '/Home/RequestActPoints',
            reader: {
                type: 'json',
                successProperty: 'success',
                messageProperty: 'message',
                totalProperty: 'total',
                root: 'data'
            },
            extraParams: {
                node: 'root',
                types: [$$iPems.Point.DI, $$iPems.Point.AI, $$iPems.Point.AO, $$iPems.Point.DO]
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful && gaugeChart && lineChart) {
                    if (records.length > 0) {
                        var grid = Ext.getCmp('points-grid'),
                            selection = grid.getSelectionModel();

                        if (selection.hasSelection()) {
                            var index = selection.getSelection()[0].get('index');
                            var record = me.findRecord('index', index);
                            if (record != null) loadChart(record);
                        }
                    }

                    $$iPems.Tasks.actPointTask.fireOnStart = false;
                    $$iPems.Tasks.actPointTask.restart();
                }
            }
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var controlWnd = Ext.create('Ext.window.Window', {
        title: '信号遥控',
        height: 250,
        width: 400,
        glyph: 0xf040,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        items: [{
            xtype: 'form',
            itemId: 'controlForm',
            border: false,
            padding: 10,
            items: [
                {
                    itemId: 'device',
                    xtype: 'hiddenfield',
                    name: 'device'
                },
                {
                    itemId: 'point',
                    xtype: 'hiddenfield',
                    name: 'point'
                },
                {
                    itemId: 'controlradio',
                    xtype: 'radiogroup',
                    columns: 1,
                    vertical: true,
                    items: [
                        { boxLabel: '常开控制(0)', name: 'ctrl', inputValue: 0, checked: true },
                        { boxLabel: '常闭控制(1)', name: 'ctrl', inputValue: 1 },
                        { boxLabel: '脉冲控制(2)', name: 'ctrl', inputValue: 2 }
                    ]
                }]
        }],
        buttons: [
          { id: 'controlResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button',
              text: '遥控',
              handler: function (el, e) {
                  var form = controlWnd.getComponent('controlForm'),
                      baseForm = form.getForm(),
                      result = Ext.getCmp('controlResult');

                  result.setTextWithIcon('', '');
                  if (baseForm.isValid()) {
                      result.setTextWithIcon('正在处理...', 'x-icon-loading');
                      baseForm.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/ControlPoint',
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
                  } else {
                      result.setTextWithIcon('表单填写错误', 'x-icon-error');
                  }
              }
          }, {
              xtype: 'button',
              text: '关闭',
              handler: function (el, e) {
                  var form = controlWnd.getComponent('controlForm'),
                      baseForm = form.getForm();

                  baseForm.reset();
                  controlWnd.close();
              }
          }
        ]
    });

    var adjustWnd = Ext.create('Ext.window.Window', {
        title: '信号遥调',
        height: 250,
        width: 400,
        glyph: 0xf028,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        items: [{
            xtype: 'form',
            itemId: 'adjustForm',
            border: false,
            padding: 10,
            items: [
                {
                    itemId: 'device',
                    xtype: 'hiddenfield',
                    name: 'device'
                },
                {
                    itemId: 'point',
                    xtype: 'hiddenfield',
                    name: 'point'
                },
                {
                    itemId: 'adjust',
                    xtype: 'numberfield',
                    name: 'adjust',
                    fieldLabel: '模拟量输出值',
                    value: 0,
                    width: 280,
                    allowOnlyWhitespace:false
                }]
        }],
        buttons: [
          { id: 'adjustResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button',
              text: '遥调',
              handler: function (el, e) {
                  var form = adjustWnd.getComponent('adjustForm'),
                      baseForm = form.getForm(),
                      result = Ext.getCmp('adjustResult');

                  result.setTextWithIcon('', '');
                  if (baseForm.isValid()) {
                      result.setTextWithIcon('正在处理...', 'x-icon-loading');
                      baseForm.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/AdjustPoint',
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
                  } else {
                      result.setTextWithIcon('表单填写错误', 'x-icon-error');
                  }
              }
          }, {
              xtype: 'button',
              text: '关闭',
              handler: function (el, e) {
                  var form = adjustWnd.getComponent('adjustForm'),
                      baseForm = form.getForm();

                  baseForm.reset();
                  adjustWnd.close();
              }
          }
        ]
    });

    var thresholdWnd = Ext.create('Ext.window.Window', {
        title: '设置门限',
        height: 250,
        width: 400,
        glyph: 0xf002,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        items: [{
            xtype: 'form',
            itemId: 'thresholdForm',
            border: false,
            layout: {
                type: 'vbox',
                align: 'center'
            },
            fieldDefaults: {
                labelWidth: 100,
                labelAlign: 'left',
                margin: '15 15 15 15',
                width: '100%'
            },
            items: [{
                xtype: 'hiddenfield',
                itemId: 'point',
                name: 'point'
            }, {
                xtype: 'hiddenfield',
                itemId: 'device',
                name: 'device'
            }, {
                xtype: 'textfield',
                itemId: 'nmid',
                name: 'nmid',
                fieldLabel: '网管告警编号'
            }, {
                xtype: 'AlarmLevelCombo',
                itemId: 'level',
                name: 'level',
                fieldLabel: '告 警 等 级',
                all: false,
                width: '100%'
            }, {
                xtype: 'numberfield',
                itemId: 'threshold',
                name: 'threshold',
                fieldLabel: '告警门限阈值',
                value: -1,
                allowBlank: false,
                allowOnlyWhitespace: false
            }]
        }],
        buttons: [
          { id: 'thresholdResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button',
              text: '保存',
              handler: function (el, e) {
                  var form = thresholdWnd.getComponent('thresholdForm'),
                      baseForm = form.getForm(),
                      device = form.getComponent('device').getValue(),
                      point = form.getComponent('point').getValue(),
                      nmalarmID = form.getComponent('nmid').getValue(),
                      alarmLevel = form.getComponent('level').getValue(),
                      threshold = form.getComponent('threshold').getValue(),
                      result = Ext.getCmp('thresholdResult');

                  result.setTextWithIcon('', '');
                  if (baseForm.isValid()) {
                      result.setTextWithIcon('正在处理...', 'x-icon-loading');
                      baseForm.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/SetThreshold',
                          params: {
                              device: device,
                              point: point,
                              nmalarmID: nmalarmID,
                              alarmLevel: alarmLevel,
                              threshold: threshold
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
                  } else {
                      result.setTextWithIcon('表单填写错误', 'x-icon-error');
                  }
              }
          }, {
              xtype: 'button',
              text: '取消',
              handler: function (el, e) {
                  thresholdWnd.close();
              }
          }
        ]
    });

    Ext.onReady(function () {
        var currentLayout = Ext.create('Ext.panel.Panel', {
            id: 'currentLayout',
            region: 'center',
            layout: 'border',
            border: false,
            items: [
                {
                    id: 'organization',
                    region: 'west',
                    xtype: 'treepanel',
                    title: '系统层级',
                    glyph: 0xf011,
                    width: 220,
                    split: true,
                    collapsible: true,
                    collapsed: false,
                    autoScroll: true,
                    useArrows: false,
                    rootVisible: true,
                    root: {
                        id: 'root',
                        text: '全部',
                        expanded: true,
                        icon: $$iPems.icons.Home
                    },
                    viewConfig: {
                        loadMask: true
                    },
                    store: Ext.create('Ext.data.TreeStore', {
                        autoLoad: false,
                        nodeParam: 'node',
                        proxy: {
                            type: 'ajax',
                            url: '/Component/GetDevices',
                            reader: {
                                type: 'json',
                                successProperty: 'success',
                                messageProperty: 'message',
                                totalProperty: 'total',
                                root: 'data'
                            }
                        }
                    }),
                    listeners: {
                        select: function (me, record, index) {
                            change(record, currentPagingToolbar);
                        }
                    },
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
                            id: 'point-search-button',
                            xtype: 'button',
                            glyph: 0xf005,
                            handler: function () {
                                var tree = Ext.getCmp('organization'),
                                    search = Ext.getCmp('organization-search-field'),
                                    text = search.getRawValue();

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

                                    $$iPems.selectNodePath(tree, paths[index]);
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
                                                    $$iPems.selectNodePath(tree, data.data[0]);
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
                }, {
                    region: 'center',
                    xtype: 'panel',
                    border: false,
                    bodyCls: 'x-border-body-panel',
                    layout: {
                        type: 'vbox',
                        align: 'stretch',
                        pack: 'start'
                    },
                    items: [
                        {
                            xtype: 'panel',
                            glyph: 0xf039,
                            title: '实时图表',
                            collapsible: true,
                            collapseFirst: false,
                            layout: {
                                type: 'hbox',
                                align: 'stretch',
                                pack: 'start'
                            },
                            items: [
                                {
                                    xtype: 'container',
                                    flex: 1,
                                    contentEl: 'gauge-chart'
                                }, {
                                    xtype: 'container',
                                    flex: 3,
                                    contentEl: 'line-chart'
                                }
                            ],
                            listeners: {
                                resize: function (me, width, height, oldWidth, oldHeight) {
                                    if (gaugeChart) gaugeChart.resize();
                                    if (lineChart) lineChart.resize();
                                }
                            }
                        },
                        {
                            id: 'points-grid',
                            xtype: 'grid',
                            collapsible: true,
                            collapseFirst: false,
                            layout: 'fit',
                            margin: '5 0 0 0',
                            flex: 2,
                            header: {
                                glyph: 0xf029,
                                title: '实时测值',
                                items: [
                                    {
                                        xtype: 'checkboxgroup',
                                        width: 240,
                                        items: [
                                            { xtype: 'checkboxfield', boxLabel: '遥信', inputValue: $$iPems.Point.DI, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' },
                                            { xtype: 'checkboxfield', boxLabel: '遥测', inputValue: $$iPems.Point.AI, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' },
                                            { xtype: 'checkboxfield', boxLabel: '遥调', inputValue: $$iPems.Point.AO, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' },
                                            { xtype: 'checkboxfield', boxLabel: '遥控', inputValue: $$iPems.Point.DO, checked: true, boxLabelCls: 'x-label-header x-form-cb-label' },
                                        ],
                                        listeners: {
                                            change: function (me, newValue, oldValue) {
                                                var types = [];
                                                Ext.Object.each(newValue, function (key, value, myself) {
                                                    types.push(value);
                                                });

                                                currentStore.proxy.extraParams.types = types;
                                                currentStore.loadPage(1);
                                            }
                                        }
                                    }
                                    
                                ]
                            },
                            tools: [
                                {
                                    type: 'refresh',
                                    tooltip: '刷新',
                                    handler: function (event, toolEl, panelHeader) {
                                        currentPagingToolbar.doRefresh();
                                    }
                                }
                            ],
                            store: currentStore,
                            viewConfig: {
                                loadMask: false,
                                preserveScrollOnRefresh: true,
                                stripeRows: true,
                                trackOver: true,
                                emptyText: '<h1 style="margin:20px">没有数据记录</h1>',
                                getRowClass: function (record, rowIndex, rowParams, store) {
                                    return $$iPems.GetStateCls(record.get("statusid"));
                                }
                            },
                            features: [{
                                ftype: 'grouping',
                                groupHeaderTpl: '{columnName}: {name} ({rows.length}条)',
                                hideGroupedHeader: false,
                                startCollapsed: false
                            }],
                            columns: [
                                { text: '序号', dataIndex: 'index', width:60 },
                                { text: '所属区域', dataIndex: 'area' },
                                { text: '所属站点', dataIndex: 'station' },
                                { text: '所属机房', dataIndex: 'room' },
                                { text: '所属设备', dataIndex: 'device' },
                                { text: '信号名称', dataIndex: 'point' },
                                { text: '信号类型', dataIndex: 'type' },
                                { text: '信号测值', dataIndex: 'value' },
                                { text: '单位/描述', dataIndex: 'unit' },
                                { text: '信号状态', dataIndex: 'status', tdCls: 'x-status-cell', align: 'center' },
                                { text: '测值时间', dataIndex: 'time', width: 150 },
                                {
                                    xtype: 'actioncolumn',
                                    width: 100,
                                    align: 'center',
                                    menuDisabled: true,
                                    menuText: '操作',
                                    text: '操作',
                                    items: [{
                                        tooltip: '设置门限',
                                        getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                            return (r.get('level') > 0 && $$iPems.ThresholdOperation) ? 'x-cell-icon x-icon-edit' : 'x-cell-icon x-icon-hidden';
                                        },
                                        handler: function (view, rowIndex, colIndex, item, event, record) {
                                            view.getSelectionModel().select(record);
                                            var form = thresholdWnd.getComponent('thresholdForm'),
                                                device = form.getComponent('device'),
                                                point = form.getComponent('point'),
                                                result = Ext.getCmp('thresholdResult');

                                            device.setValue(record.get('devid'));
                                            point.setValue(record.get('pointid'));
                                            result.setTextWithIcon('', '')

                                            form.load({
                                                url: '/Home/GetThreshold',
                                                params: { device: device.getValue(), point: point.getValue() },
                                                waitMsg: '正在处理...',
                                                waitTitle: '系统提示',
                                                success: function (form, action) {
                                                    form.clearInvalid();
                                                    thresholdWnd.show();
                                                }
                                            })
                                        }
                                    }, {
                                        tooltip: '遥控',
                                        getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                            return (r.get('typeid') === $$iPems.Point.DO && $$iPems.ControlOperation) ? 'x-cell-icon x-icon-remote-control' : 'x-cell-icon x-icon-hidden';
                                        },
                                        handler: function (view, rowIndex, colIndex, item, event, record) {
                                            view.getSelectionModel().select(record);
                                            var form = controlWnd.getComponent('controlForm'),
                                                device = form.getComponent('device'),
                                                point = form.getComponent('point'),
                                                result = Ext.getCmp('controlResult');

                                            device.setValue(record.get('devid'));
                                            point.setValue(record.get('pointid'));
                                            result.setTextWithIcon('', '')
                                            controlWnd.show();
                                        }
                                    }, {
                                        tooltip: '遥调',
                                        getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                            return (r.get('typeid') === $$iPems.Point.AO && $$iPems.AdjustOperation) ? 'x-cell-icon x-icon-remote-setting' : 'x-cell-icon x-icon-hidden';
                                        },
                                        handler: function (view, rowIndex, colIndex, item, event, record) {
                                            view.getSelectionModel().select(record);
                                            var form = adjustWnd.getComponent('adjustForm'),
                                                device = form.getComponent('device'),
                                                point = form.getComponent('point'),
                                                result = Ext.getCmp('adjustResult');

                                            device.setValue(record.get('devid'));
                                            point.setValue(record.get('pointid'));
                                            result.setTextWithIcon('', '')
                                            adjustWnd.show();
                                        }
                                    }, {
                                        getTip: function (v, metadata, r, rowIndex, colIndex, store) {
                                            return (r.get('rsspoint') === true) ? '已关注' : '关注';
                                        },
                                        getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                            return (r.get('rssfrom') === false) ? ((r.get('rsspoint') === true) ? 'x-cell-icon x-icon-tick' : 'x-cell-icon x-icon-add') : 'x-cell-icon x-icon-hidden';
                                        },
                                        handler: function (view, rowIndex, colIndex, item, event, record) {
                                            if (record.get('rsspoint')) return false;

                                            Ext.Ajax.request({
                                                url: '/Home/AddRssPoint',
                                                params: { device: record.get('devid'), point: record.get('pointid') },
                                                mask: new Ext.LoadMask({ target: view, msg: '正在处理...' }),
                                                success: function (response, options) {
                                                    var data = Ext.decode(response.responseText, true);
                                                    if (data.success) {
                                                        currentPagingToolbar.doRefresh();
                                                    } else {
                                                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                                    }
                                                }
                                            });
                                        }
                                    }, {
                                        tooltip: '取消关注',
                                        getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                            return (r.get('rssfrom') === true) ? 'x-cell-icon x-icon-delete' : 'x-cell-icon x-icon-hidden';
                                        },
                                        handler: function (view, rowIndex, colIndex, item, event, record) {
                                            Ext.Ajax.request({
                                                url: '/Home/RemoveRssPoint',
                                                params: { device: record.get('devid'), point: record.get('pointid') },
                                                mask: new Ext.LoadMask({ target: view, msg: '正在处理...' }),
                                                success: function (response, options) {
                                                    var data = Ext.decode(response.responseText, true);
                                                    if (data.success) {
                                                        currentPagingToolbar.doRefresh();
                                                    } else {
                                                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                                                    }
                                                }
                                            });
                                        }
                                    }]
                                }
                            ],
                            bbar: currentPagingToolbar,
                            listeners: {
                                selectionchange: function (model, selected) {
                                    resetChart();
                                    if (selected.length > 0) {
                                        loadChart(selected[0]);
                                    }
                                }
                            }
                        }
                    ]
                }
            ]
        });

        $$iPems.Tasks.actPointTask.run = function () {
            currentPagingToolbar.doRefresh();
        };
        $$iPems.Tasks.actPointTask.start();

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(currentLayout);
        }
    });

    Ext.onReady(function () {
        gaugeChart = echarts.init(document.getElementById("gauge-chart"), 'shine');
        lineChart = echarts.init(document.getElementById("line-chart"), 'shine');

        //init charts
        gaugeChart.setOption(gaugeOption);
        lineChart.setOption(lineOption);
    });
})();