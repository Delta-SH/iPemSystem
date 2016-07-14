(function () {
    Ext.define('PointModel', {
        extend: 'Ext.data.Model',
        fields: [
			{ name: 'key', type: 'string' },
            { name: 'area', type: 'string' },
            { name: 'station', type: 'string' },
			{ name: 'room', type: 'string' },
            { name: 'devType', type: 'string' },
            { name: 'devId', type: 'string' },
            { name: 'devName', type: 'string' },
            { name: 'logic', type: 'string' },
            { name: 'id', type: 'string' },
            { name: 'name', type: 'string' },
            { name: 'type', type: 'int' },
            { name: 'typeDisplay', type: 'string' },
            { name: 'value', type: 'float' },
            { name: 'valueDisplay', type: 'string' },
            { name: 'status', type: 'int' },
            { name: 'statusDisplay', type: 'string' },
            { name: 'timestamp', type: 'string' }
        ],
        idProperty: 'key'
    });

    var selectPath = function (tree, ids, callback) {
        var root = tree.getRootNode(),
            field = 'id',
            separator = '/',
            path = ids.join(separator);

        path = separator + root.get(field) + separator + path;
        tree.selectPath(path, field, separator, callback || Ext.emptyFn);
    };

    var resetchart = function (gauge, line) {
        gauge = gauge || Ext.getCmp('chartGauge');
        line = line || Ext.getCmp('chartLine');

        gauge.store.loadData([{ 'name': 'NoData', 'value': 0, 'comment': '' }], false);
        line.store.removeAll();
    };

    var loadchart = function (record, gauge, line, append, maxcount) {
        gauge = gauge || Ext.getCmp('chartGauge');
        line = line || Ext.getCmp('chartLine');
        append = append || false;
        maxcount = maxcount || 60;

        var name = record.get('timestamp'),
            value = record.get('value'),
            comment = record.get('valueDisplay');

        var abs = Math.abs(value);
        if (abs <= 100) {
            gauge.axes.items[0].minimum = -100;
            gauge.axes.items[0].maximum = 100;
        } else if (abs > 100 && abs <= 500) {
            gauge.axes.items[0].minimum = -500;
            gauge.axes.items[0].maximum = 500;
        } else if (abs > 500 && abs <= 1000) {
            gauge.axes.items[0].minimum = -1000;
            gauge.axes.items[0].maximum = 1000;
        } else if (abs > 1000) {
            gauge.axes.items[0].minimum = -10000;
            gauge.axes.items[0].maximum = 10000;
        }

        if (abs <= 10000) gauge.store.loadData([{ 'name': name, 'value': value, 'comment': comment }]);
        if (line.store.count() > maxcount) line.store.removeAt(0);
        line.store.loadData([{ 'name': name, 'value': value, 'comment': comment }], append);
    };

    var change = function (node, pagingtoolbar, layout) {
        Ext.Ajax.request({
            url: '/Home/RequestRemoveRssPointsCache',
            mask: new Ext.LoadMask(layout || Ext.getCmp('currentLayout'), { msg: '正在处理，请稍后...' }),
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    var me = pagingtoolbar.store,
                        attributes = node.raw.attributes;

                    if (!Ext.isEmpty(attributes) && attributes.length > 0) {
                        var id = Ext.Array.findBy(attributes, function (item, index) {
                            return item.key === 'id';
                        });

                        if (!Ext.isEmpty(id))
                            me.proxy.extraParams.nodeid = id.value;

                        var type = Ext.Array.findBy(attributes, function (item, index) {
                            return item.key === 'type';
                        });

                        if (!Ext.isEmpty(type)) {
                            me.proxy.extraParams.nodetype = type.value;

                            var columns = Ext.getCmp('active-point-grid').columns;
                            if (parseInt(type.value) === $$iPems.Organization.Device) {
                                columns[0].hide();
                                columns[1].hide();
                                columns[2].hide();
                                columns[3].hide();
                                columns[4].hide();
                                columns[5].hide();
                            } else {
                                columns[0].show();
                                columns[1].show();
                                columns[2].show();
                                columns[3].show();
                                columns[4].show();
                                columns[5].show();
                            }
                        }
                    }

                    resetchart();
                    me.loadPage(1);
                } else {
                    Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                }
            }
        });
    };

    var chartGauge = Ext.create('Ext.chart.Chart', {
        id: 'chartGauge',
        xtype: 'chart',
        animate: {
            easing: 'elasticIn',
            duration: 500
        },
        insetPadding: 5,
        flex: 1,
        axes: [{
            type: 'gauge',
            position: 'gauge',
            minimum: -100,
            maximum: 100,
            steps: 10,
            margin: -10
        }],
        series: [{
            type: 'gauge',
            field: 'value',
            donut: 30,
            colorSet: ['#157fcc', '#ddd']
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment'],
            data: $$iPems.ChartEmptyDataGauge
        })
    });

    var chartLine = Ext.create('Ext.chart.Chart', {
        id: 'chartLine',
        xtype: 'chart',
        flex: 3,
        axes: [{
            type: 'Numeric',
            position: 'left',
            fields: ['value'],
            minorTickSteps: 1,
            title: false,
            grid: true
        }, {
            type: 'Category',
            position: 'bottom',
            fields: 'name',
            title: false,
            minorTickSteps: 3,
            label: {
                rotate: {
                    degrees: 0
                }
            }
        }],
        series: [{
            type: 'line',
            smooth: true,
            axis: ['left', 'bottom'],
            xField: 'name',
            yField: 'value',
            highlightLine: false,
            label: {
                display: 'under',
                field: 'comment'
            },
            tips: {
                trackMouse: true,
                minWidth: 80,
                minHeight: 40,
                renderer: function (storeItem, item) {
                    this.setTitle(storeItem.get('name'));
                    this.update(storeItem.get('comment'));
                }
            },
            style: {
                fill: '#157fcc',
                stroke: '#157fcc',
                'stroke-width': 2,
                opacity: 1
            },
            markerConfig: {
                type: 'circle',
                size: 3,
                radius: 3,
                fill: '#fff',
                stroke: '#157fcc',
                'stroke-width': 2
            },
            highlight: {
                size: 5,
                radius: 5,
                'stroke-width': 4
            }
        }],
        store: Ext.create('Ext.data.Store', {
            autoLoad: false,
            fields: ['name', 'value', 'comment'],
            data: $$iPems.ChartEmptyDataLine
        })
    });

    var currentStore = Ext.create('Ext.data.Store', {
        autoLoad: false,
        pageSize: 20,
        model: 'PointModel',
        groupField: 'typeDisplay',
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
                nodeid: 'root',
                nodetype: $$iPems.Organization.Area,
                types: [$$iPems.Point.DI, $$iPems.Point.AI, $$iPems.Point.AO, $$iPems.Point.DO]
            },
            simpleSortMode: true
        },
        listeners: {
            load: function (me, records, successful) {
                if (successful) {
                    if (records.length > 0) {
                        var grid = Ext.getCmp('active-point-grid');
                        if (grid.getSelectionModel().hasSelection()) {
                            var key = grid.getSelectionModel().getSelection()[0].get('key');
                            var record = me.findRecord('key', key);
                            if (record != null) {
                                loadchart(record, chartGauge, chartLine, true);
                            }
                        }
                    }

                    $$iPems.Tasks.actPointTask.fireOnStart = false;
                    $$iPems.Tasks.actPointTask.restart();
                }
            }
        }
    });

    var currentPagingToolbar = $$iPems.clonePagingToolbar(currentStore);

    var pointRssWnd = Ext.create('Ext.window.Window', {
        title: '信号订阅',
        height: 300,
        width: 600,
        glyph: 0xf041,
        modal: true,
        border: false,
        hidden: true,
        closeAction: 'hide',
        layout: {
            type: 'vbox',
            align: 'stretch'
        },
        items: [{
            xtype: 'form',
            id: 'pointRssForm',
            border: false,
            defaultType: 'textfield',
            flex: 1,
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
                                    id: 'station-type-multicombo',
                                    name: 'stationtypes',
                                    xtype: 'StationTypeMultiCombo',
                                    emptyText: '默认全部'
                                },
                                {
                                    id: 'device-type-multicombo',
                                    name: 'devicetypes',
                                    xtype: 'DeviceTypeMultiCombo',
                                    emptyText: '默认全部'
                                },
                                {
                                    id: 'point-type-multicombo',
                                    name: 'pointtypes',
                                    xtype: 'PointTypeMultiCombo',
                                    emptyText: '默认全部'
                                }
                            ]
                        },
                        {
                            xtype: 'container',
                            flex: 1,
                            layout: 'anchor',
                            items: [
                                {
                                    id: 'room-type-multicombo',
                                    name: 'roomtypes',
                                    xtype: 'RoomTypeMultiCombo',
                                    emptyText: '默认全部'
                                },
                                {
                                    id: 'logic-type-multicombo',
                                    name: 'logictypes',
                                    xtype: 'LogicTypeMultiCombo',
                                    emptyText: '默认全部'
                                },
                                {
                                    id: 'point-name-textfield',
                                    name: 'pointnames',
                                    xtype: 'textfield',
                                    fieldLabel: '信号名称',
                                    emptyText: '多条件请以;分隔，例: A;B;C'
                                }
                            ]
                        }
                    ]
                }
            ]
        }, {
            xtype: 'iconlabel',
            text: '请尽量精确订阅条件，避免因数据过多导致网页响应迟缓，影响其他操作。',
            margin: '15 15 15 15',
            iconCls: 'x-icon-tips'
        }],
        buttons: [
          { id: 'pointRssResult', xtype: 'iconlabel', text: '' },
          { xtype: 'tbfill' },
          {
              xtype: 'button',
              text: '订阅',
              handler: function (el, e) {
                  Ext.getCmp('pointRssResult').setTextWithIcon('', '');

                  var form = Ext.getCmp('pointRssForm'),
                      baseForm = form.getForm();

                  if (baseForm.isValid()) {
                      Ext.getCmp('pointRssResult').setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
                      baseForm.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/SavePointRss',
                          success: function (form, action) {
                              Ext.getCmp('pointRssResult').setTextWithIcon(action.result.message, 'x-icon-accept');
                              currentStore.loadPage(1);
                          },
                          failure: function (form, action) {
                              var message = 'undefined error.';
                              if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                  message = action.result.message;

                              Ext.getCmp('pointRssResult').setTextWithIcon(message, 'x-icon-error');
                          }
                      });
                  } else {
                      Ext.getCmp('pointRssResult').setTextWithIcon('表单填写错误', 'x-icon-error');
                  }
              }
          }, {
              xtype: 'button',
              text: '关闭',
              handler: function (el, e) {
                  pointRssWnd.close();
              }
          }
        ]
    });

    var controlWnd = Ext.create('Ext.window.Window', {
        title: '信号遥控参数',
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
                      result.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
                      baseForm.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/ControlPoint',
                          success: function (form, action) {
                              result.setTextWithIcon(action.result.message, 'x-icon-accept');
                              //currentPagingToolbar.doRefresh();
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
        title: '信号遥调参数',
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
                      result.setTextWithIcon('正在处理，请稍后...', 'x-icon-loading');
                      baseForm.submit({
                          submitEmptyText: false,
                          clientValidation: true,
                          preventWindow: true,
                          url: '/Home/AdjustPoint',
                          success: function (form, action) {
                              result.setTextWithIcon(action.result.message, 'x-icon-accept');
                              //currentPagingToolbar.doRefresh();
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

    Ext.onReady(function () {
        var currentLayout = Ext.create('Ext.panel.Panel', {
            id: 'currentLayout',
            region: 'center',
            layout: 'border',
            border: false,
            items: [
                {
                    id: 'point-organization',
                    region: 'west',
                    xtype: 'treepanel',
                    title: '系统设备列表',
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
                        icon: $$iPems.icons.Home,
                        attributes: [
                            { key: 'id', value: 'root' },
                            { key: 'type', value: $$iPems.Organization.Area }
                        ]
                    },
                    viewConfig: {
                        loadMask: true
                    },
                    store: Ext.create('Ext.data.TreeStore', {
                        autoLoad: false,
                        nodeParam: 'node',
                        proxy: {
                            type: 'ajax',
                            url: '/Home/GetOrganization',
                            extraParams: {
                                id: '',
                                type: -1
                            },
                            reader: {
                                type: 'json',
                                successProperty: 'success',
                                messageProperty: 'message',
                                totalProperty: 'total',
                                root: 'data'
                            }
                        },
                        listeners: {
                            beforeexpand: function (node) {
                                var me = this, attributes = node.raw.attributes;
                                if (!Ext.isEmpty(attributes) && attributes.length > 0) {
                                    var id = Ext.Array.findBy(attributes, function (item, index) {
                                        return item.key === 'id';
                                    });

                                    if (!Ext.isEmpty(id))
                                        me.proxy.extraParams.id = id.value;

                                    var type = Ext.Array.findBy(attributes, function (item, index) {
                                        return item.key === 'type';
                                    });

                                    if (!Ext.isEmpty(type))
                                        me.proxy.extraParams.type = type.value;
                                }
                            }
                        }
                    }),
                    listeners: {
                        select: function (me, record, index) {
                            change(record, currentPagingToolbar, currentLayout);
                        }
                    },
                    tbar: [
                        {
                            id: 'point-search-field',
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
                                var tree = Ext.getCmp('point-organization'),
                                    search = Ext.getCmp('point-search-field'),
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

                                    selectPath(tree, paths[index]);
                                    search._filterIndex = index;
                                } else {
                                    Ext.Ajax.request({
                                        url: '/Home/SearchOrganization',
                                        params: { text: text },
                                        mask: new Ext.LoadMask({ target: tree, msg: '正在处理，请稍后...' }),
                                        success: function (response, options) {
                                            var data = Ext.decode(response.responseText, true);
                                            if (data.success) {
                                                var len = data.data.length;
                                                if (len > 0) {
                                                    selectPath(tree, data.data[0]);
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
                    id: 'point-dashboard',
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
                            title: '信号实时图表',
                            collapsible: true,
                            collapseFirst: false,
                            flex: 1,
                            layout: {
                                type: 'hbox',
                                align: 'stretch',
                                pack: 'start'
                            },
                            items: [chartGauge, chartLine]
                        },
                        {
                            id: 'active-point-grid',
                            xtype: 'grid',
                            collapsible: true,
                            collapseFirst: false,
                            layout: 'fit',
                            margin: '5 0 0 0',
                            flex: 2,
                            header: {
                                glyph: 0xf029,
                                title: '信号实时测值',
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
                                },
                                {
                                    type: 'gear',
                                    tooltip: '信号订阅',
                                    handler: function (event, toolEl, panelHeader) {
                                        var basic = Ext.getCmp('pointRssForm').getForm();
                                        basic.load({
                                            url: '/Home/GetPointRss',
                                            waitMsg: '正在处理，请稍后...',
                                            waitTitle: '系统提示',
                                            success: function (form, action) {
                                                form.clearInvalid();

                                                Ext.getCmp('pointRssResult').setTextWithIcon('', '');
                                                pointRssWnd.show();
                                            }
                                        });
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
                                    return $$iPems.GetPointStatusCls(record.get("status"));
                                }
                            },
                            features: [{
                                ftype: 'grouping',
                                groupHeaderTpl: '{columnName}: {name} ({rows.length}条)',
                                hideGroupedHeader: true,
                                startCollapsed: false
                            }],
                            columns: [
                                { text: '所属区域', dataIndex: 'area' },
                                { text: '所属站点', dataIndex: 'station' },
                                { text: '所属机房', dataIndex: 'room' },
                                { text: '设备类型', dataIndex: 'devType' },
                                { text: '设备名称', dataIndex: 'devName' },
                                { text: '逻辑分类', dataIndex: 'logic' },
                                { text: '信号ID', dataIndex: 'id' },
                                { text: '信号名称', dataIndex: 'name' },
                                { text: '信号类型', dataIndex: 'typeDisplay', align: 'center' },
                                { text: '信号测值', dataIndex: 'valueDisplay' },
                                { text: '信号状态', dataIndex: 'statusDisplay', tdCls: 'x-status-cell', align: 'center' },
                                {
                                    xtype: 'actioncolumn',
                                    width: 80,
                                    align: 'center',
                                    menuDisabled: true,
                                    menuText: '操作',
                                    text: '操作',
                                    items: [{
                                        tooltip: '遥控',
                                        getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                            return (r.get('type') === $$iPems.Point.DO && $$iPems.ControlOperation) ? 'x-cell-icon x-icon-remote-control' : 'x-cell-icon x-icon-hidden';
                                        },
                                        handler: function (view, rowIndex, colIndex, item, event, record) {
                                            view.getSelectionModel().select(record);
                                            var form = controlWnd.getComponent('controlForm'),
                                                device = form.getComponent('device'),
                                                point = form.getComponent('point'),
                                                result = Ext.getCmp('controlResult');

                                            device.setValue(record.get('devId'));
                                            point.setValue(record.get('id'));
                                            result.setTextWithIcon('', '')
                                            controlWnd.show();
                                        }
                                    }, {
                                        tooltip: '遥调',
                                        getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                            return (r.get('type') === $$iPems.Point.AO && $$iPems.AdjustOperation) ? 'x-cell-icon x-icon-remote-setting' : 'x-cell-icon x-icon-hidden';
                                        },
                                        handler: function (view, rowIndex, colIndex, item, event, record) {
                                            view.getSelectionModel().select(record);
                                            var form = adjustWnd.getComponent('adjustForm'),
                                                device = form.getComponent('device'),
                                                point = form.getComponent('point'),
                                                result = Ext.getCmp('adjustResult');

                                            device.setValue(record.get('devId'));
                                            point.setValue(record.get('id'));
                                            result.setTextWithIcon('', '')
                                            adjustWnd.show();
                                        }
                                    }]
                                }
                            ],
                            bbar: currentPagingToolbar,
                            listeners: {
                                selectionchange: function (model, selected) {
                                    if (selected.length > 0)
                                        loadchart(selected[0], chartGauge, chartLine, false);
                                    else
                                        resetchart(chartGauge, chartLine);
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
})();