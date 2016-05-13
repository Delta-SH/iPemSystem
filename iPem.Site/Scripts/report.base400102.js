(function () {
    var layout = Ext.create('Ext.tab.Panel', {
        region: 'center',
        cls: 'x-custom-panel',
        border: true,
        plain: true,
        items: [
            {
                title: '报表参数',
                glyph: 0xf044,
                items: [
                    {
                        id: 'test1',
                        xtype: 'station.multi.treepanel',
                        selectOnLeaf: true
                    },
                    {
                        id: 'test2',
                        xtype: 'station.treepanel',
                        selectOnLeaf: true
                    }, {
                        xtype: 'button',
                        text: 'Click me',
                        handler: function () {
                            var t1 = Ext.getCmp('test1'),
                                t2 = Ext.getCmp('test2');

                            t1.setValue(['0┆110228', '0┆110105', '0┆110115', '1┆01', '1┆03', '1┆06', '1┆10']);
                            t2.setValue(['1┆10']);
                        }
                    }
                ]
            },
            {
                title: '指标参数',
                glyph: 0xf044,
            },
            {
                title: '能耗分类字典',
                glyph: 0xf044,
            }
        ]
    });

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(layout);
        }
    });
})();