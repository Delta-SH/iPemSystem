(function () {
    Ext.onReady(function () {
        var gkpanel1_1 = Ext.create('Ext.panel.Panel', {
            title: '图表-1',
            flex: 1,
            margin: '0 0 5 0',
            border: true
        });

        var gkpanel1_2 = Ext.create('Ext.panel.Panel', {
            title: '图表-2',
            flex: 1,
            margin: '0 0 5 5',
            border: true
        });

        var gkpanel1 = Ext.create('Ext.panel.Panel', {
            layout: {
                type: 'hbox',
                align: 'stretch',
                pack: 'start'
            },
            border: false,
            flex: 1,
            items: [gkpanel1_1, gkpanel1_2]
        });

        var gkpanel2 = Ext.create('Ext.panel.Panel', {
            title: '概况列表信息',
            border: true,
            flex: 1
        });

        var hcontent = Ext.create('Ext.panel.Panel', {
            layout: {
                type: 'vbox',
                align: 'stretch',
                pack: 'start',
            },
            region: 'center',
            border: false,
            items: [gkpanel1, gkpanel2]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(hcontent);
        }
    });
})();