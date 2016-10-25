(function () {
    Ext.onReady(function () {
        var iframe = Ext.create('Ext.ux.IFrame', {
            flex: 1,
            loadMask: '正在处理...'
        });

        var hcontent = Ext.create('Ext.panel.Panel', {
            region: 'center',
            bodyCls: 'x-border-body-panel',
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            items: [iframe]
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(hcontent);
            iframe.load('/Configuration/CfgIFrame');
        }
    });
})();