/* ========================================================================
 * Components: StationTypeComponent.js
 * /Scripts/components/StationTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.StationTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "StationTypeMultiCombo",
    fieldLabel: '站点类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.StationTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "StationTypeCombo",
    fieldLabel: '站点类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetStationTypes';
        me.callParent(arguments);
        me.store.load();
    }
});