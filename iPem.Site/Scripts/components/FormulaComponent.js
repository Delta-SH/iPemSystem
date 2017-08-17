/* ========================================================================
 * Components: FormulaComponent.js
 * /Scripts/components/FormulaComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.ComputeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "ComputeMultiCombo",
    fieldLabel: '运算方式',
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
        me.storeUrl = '/Component/GetComputes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.ComputeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "ComputeCombo",
    fieldLabel: '运算方式',
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
        me.storeUrl = '/Component/GetComputes';
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