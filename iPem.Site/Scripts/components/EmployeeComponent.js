Ext.define("Ext.ux.EmployeeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeeMultiPicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.EmployeeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "EmployeePicker",
    fieldLabel: '隶属员工',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetEmployees';
        me.searchUrl = '/Component/FilterEmployeePath';
        me.queryUrl = '/Component/GetEmployeePath';

        me.callParent(arguments);
        me.store.load();
    }
});