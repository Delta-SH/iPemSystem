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

Ext.define("Ext.ux.AlarmTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "AlarmTypeMultiCombo",
    fieldLabel: '告警类型',
    valueField: 'id',
    displayField: 'text',
    delimiter: $$iPems.Delimiter,
    queryMode: 'local',
    triggerAction: 'all',
    selectionMode: 'all',
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    store: Ext.create('Ext.data.Store', {
        fields: [
             { name: 'id', type: 'int' },
             { name: 'text', type: 'string' }
        ],
        data: [
            { "id": 1, "text": '包含系统告警' },
            { "id": 2, "text": '包含屏蔽告警' }
        ]
    })
});

Ext.define("Ext.ux.AlarmTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "AlarmTypeCombo",
    fieldLabel: '告警类型',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    store: Ext.create('Ext.data.Store', {
        fields: [
             { name: 'id', type: 'int' },
             { name: 'text', type: 'string' }
        ],
        data: [
            { "id": 1, "text": '包含系统告警' },
            { "id": 2, "text": '包含屏蔽告警' }
        ]
    })
});

Ext.define("Ext.ux.BIAlarmLevelMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "BIAlarmLevelMultiCombo",
    fieldLabel: '告警等级',
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
        me.storeUrl = '/Component/GetBIAlarmLevels';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.BIAlarmLevelComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "BIAlarmLevelCombo",
    fieldLabel: '告警等级',
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
        me.storeUrl = '/Component/GetBIAlarmLevels';
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
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetConfirms';
        me.callParent(arguments);
        me.store.load();
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
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetConfirms';
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
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetReservations';
        me.callParent(arguments);
        me.store.load();
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
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetReservations';
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

Ext.define("Ext.ux.SeniorConditionMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "SeniorConditionMultiCombo",
    fieldLabel: '订制条件',
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
        me.storeUrl = '/Component/GetSeniorConditionCombo';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.SeniorConditionComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "SeniorConditionCombo",
    fieldLabel: '订制条件',
    displayField: 'text',
    valueField: 'id',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    all: true,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetSeniorConditionCombo';
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

Ext.define("Ext.ux.MaskingMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "MaskingMultiCombo",
    fieldLabel: '屏蔽类型',
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
        me.storeUrl = '/Component/GetMaskingTypes';
        me.callParent(arguments);
        me.store.load({
            scope: me,
            params: { all: me.all }
        });
    }
});

Ext.define("Ext.ux.MaskingComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "MaskingCombo",
    fieldLabel: '屏蔽类型',
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
        me.storeUrl = '/Component/GetMaskingTypes';
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