/* ========================================================================
 * Components: VendorComponent.js
 * /Scripts/components/VendorComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.VendorMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "VendorMultiCombo",
    fieldLabel: '所属厂家',
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
        me.storeUrl = '/Component/GetVendors';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.VendorComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "VendorCombo",
    fieldLabel: '所属厂家',
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
        me.storeUrl = '/Component/GetVendors';
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

Ext.define("Ext.ux.FsuEventMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "FsuEventMultiCombo",
    fieldLabel: '日志类型',
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
        me.storeUrl = '/Component/GetFsuEvents';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.FsuEventComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "FsuEventCombo",
    fieldLabel: '日志类型',
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
        me.storeUrl = '/Component/GetFsuEvents';
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