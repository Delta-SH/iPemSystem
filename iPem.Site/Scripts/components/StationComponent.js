/* ========================================================================
 * Components: StationComponent.js
 * /Scripts/components/StationComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationMultiPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "StationPicker",
    fieldLabel: '所属站点',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetStations';
        me.searchUrl = '/Component/FilterStationPath';
        me.queryUrl = '/Component/GetStationPath';
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