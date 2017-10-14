/* ========================================================================
 * Components: DepartmentComponent.js
 * /Scripts/components/DepartmentComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.DepartmentMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "DepartmentMultiCombo",
    fieldLabel: '所属部门',
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
        me.storeUrl = '/Component/GetDepartments';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DepartmentComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "DepartmentCombo",
    fieldLabel: '所属部门',
    valueField: 'id',
    displayField: 'text',
    typeAhead: true,
    queryMode: 'local',
    triggerAction: 'all',
    selectOnFocus: true,
    forceSelection: true,
    labelWidth: 60,
    width: 220,
    initComponent: function () {
        var me = this;
        me.storeUrl = '/Component/GetDepartments';
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