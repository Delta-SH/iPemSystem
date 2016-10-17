/* ========================================================================
 * Components: LogicTypeComponent.js
 * /Scripts/components/LogicTypeComponent.js
 * ========================================================================
 */

Ext.define("Ext.ux.LogicTypeMultiCombo", {
    extend: "Ext.ux.MultiCombo",
    xtype: "LogicTypeMultiCombo",
    fieldLabel: '逻辑分类',
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
        me.storeUrl = '/Component/GetLogicTypes';
        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeComboBox", {
    extend: "Ext.ux.SingleCombo",
    xtype: "LogicTypeCombo",
    fieldLabel: '逻辑分类',
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
        me.storeUrl = '/Component/GetLogicTypes';
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

Ext.define("Ext.ux.LogicTypeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypeMultiPicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.LogicTypeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "LogicTypePicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetLogicTree';
        me.searchUrl = '/Component/FilterLogicTreePath';
        me.queryUrl = '/Component/GetLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.SubLogicTypeMultiTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "SubLogicTypeMultiPicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: true,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetSubLogicTree';
        me.searchUrl = '/Component/FilterSubLogicTreePath';
        me.queryUrl = '/Component/GetSubLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});

Ext.define("Ext.ux.SubLogicTypeTreePanel", {
    extend: "Ext.ux.TreePicker",
    xtype: "SubLogicTypePicker",
    fieldLabel: '逻辑分类',
    displayField: 'text',
    labelWidth: 60,
    width: 280,
    selectOnLeaf: true,
    multiSelect: false,
    searchVisible: true,
    initComponent: function () {
        var me = this;

        me.storeUrl = '/Component/GetSubLogicTree';
        me.searchUrl = '/Component/FilterSubLogicTreePath';
        me.queryUrl = '/Component/GetSubLogicTreePath';

        me.callParent(arguments);
        me.store.load();
    }
});