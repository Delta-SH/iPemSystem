Ext.define("Ext.ux.SingleCombo", {
    extend: "Ext.form.field.ComboBox",
    xtype: "singlecombo",
    storeUrl: null,
    initComponent: function () {
        var me = this;

        if (!Ext.isEmpty(me.storeUrl)) {
            me.store = Ext.create('Ext.data.Store', {
                pageSize: 1024,
                fields: [
                    { name: 'id', type: 'int' },
                    { name: 'text', type: 'string' },
                    { name: 'comment', type: 'string' }
                ],
                proxy: {
                    type: 'ajax',
                    url: me.storeUrl,
                    reader: {
                        type: 'json',
                        successProperty: 'success',
                        messageProperty: 'message',
                        totalProperty: 'total',
                        root: 'data'
                    }
                }
            });
        }

        me.callParent(arguments);
    }
});