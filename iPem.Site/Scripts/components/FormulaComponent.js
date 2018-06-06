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

Ext.define("Ext.ux.PeriodComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "PeriodCombo",
    fieldLabel: '统计周期',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    year: true,
    month: true,
    week: true,
    day: true,
    hour: true,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetPeriods';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { year: me.year, month: me.month, week: me.week, day: me.day, hour: me.hour },
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.EnergyMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "EnergyMultiCombo",
    fieldLabel: '能耗分类',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    tt: false,
    pue: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetEnergys';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { tt: me.tt, pue: me.pue }
        });
    }
});

Ext.define("Ext.ux.EnergyComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "EnergyComboBox",
    fieldLabel: '能耗分类',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    tt: false,
    pue: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetEnergys';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { tt: me.tt, pue: me.pue },
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});