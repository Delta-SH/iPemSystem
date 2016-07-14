Ext.define("Ext.ux.DeviceMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DeviceMultiPicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.DeviceTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "DevicePicker",
    fieldLabel: '设备名称',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectAll: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetDevices';
        me.searchUrl = '/Component/FilterRoomPath';
        me.queryUrl = '/Component/GetDevicePath';
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