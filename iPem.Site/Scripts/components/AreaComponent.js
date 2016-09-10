
/* ========================================================================
 * Components: AreaComponent.js
 * /Scripts/components/AreaComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaMultiPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "AreaPicker",
    fieldLabel: '所属区域',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetAreas';
        me.searchUrl = '/Component/FilterAreaPath';
        me.queryUrl = '/Component/GetAreaPath';
        me.rootVisible = me.selectAll;

        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (!me.selectAll) return;
                me.setValue('root');
                var root = me.findRoot();
                if (root) root.expand();
            }
        });
    }
});
