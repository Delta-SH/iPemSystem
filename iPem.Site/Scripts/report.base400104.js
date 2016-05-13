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