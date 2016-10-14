/* ========================================================================
 * Components: AreaTypeComponent.js
 * /Scripts/components/AreaTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AreaTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AreaTypeMultiCombo",
    fieldLabel: '区域类型',
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
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.AreaTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AreaTypeCombo",
    fieldLabel: '区域类型',
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
        me.storeUrl = '/Component/GetAreaTypes';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});