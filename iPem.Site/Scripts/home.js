(function () {
    Ext.onReady(function () {
        var hcontent = Ext.create('Ext.panel.Panel', {
            title: '系统指标',
            region: 'center',
            border: true
        });

        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(hcontent);
        }
    });
})();