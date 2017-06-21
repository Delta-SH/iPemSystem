/* ========================================================================
 * Components: AlarmLevelComponent.js
 * /Scripts/components/AlarmLevelComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.AlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AlarmLevelMultiCombo",
    fieldLabel: '告警级别',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all }
        });
    }
});

Ext.define("Ext.ux.AlarmLevelComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AlarmLevelCombo",
    fieldLabel: '告警级别',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetAlarmLevels';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all },
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.ConfirmMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "ConfirmMultiCombo",
    fieldLabel: '确认状态',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetConfirms';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all }
        });
    }
});

Ext.define("Ext.ux.ConfirmComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "ConfirmCombo",
    fieldLabel: '确认状态',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetConfirms';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all },
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});

Ext.define("Ext.ux.ReservationMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "ReservationMultiCombo",
    fieldLabel: '工程状态',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetReservations';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all }
        });
    }
});

Ext.define("Ext.ux.ReservationComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "ReservationCombo",
    fieldLabel: '工程状态',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: false,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetReservations';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all },
            callback: function (records, operation, success) {
                if (success && records.length > 0)
                    me.select(records[0]);
            }
        });
    }
});