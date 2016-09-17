/* ========================================================================
 * Ux: MultiCombo.js
 * /Scripts/ux/MultiCombo.js
 * ========================================================================
 */

Ext.define("Ext.ux.MultiCombo", {
    extend: "Ext.form.field.ComboBox",
    xtype: "multicombo",
    selectionMode: "checkbox",
    storeUrl: null,
    idType:1,
    multiSelect: true,
    assertValue: function () {
        this.collapse();
    },
    getPicker: function () {
        if (!this.picker) {
            this.listConfig = this.listConfig || {};

            if (!this.listConfig.getInnerTpl) {
                this.listConfig.getInnerTpl = function (displayField) {
                    return '<div class="x-combo-list-item {[this.getItemClass(values)]}">' +
				           '<div class="x-mcombo-text">{' + displayField + '}</div></div>';
                };
            }

            this.listConfig.selModel = { mode: 'SIMPLE' };
            this.picker = this.createPicker();
            this.mon(this.picker.getSelectionModel(), 'select', this.onListSelect, this);
            this.mon(this.picker.getSelectionModel(), 'deselect', this.onListDeselect, this);
            this.picker.tpl.getItemClass = Ext.Function.bind(function (values) {
                var record,
                    searchValue,
                    selected;

                if (this.selectionMode === "selection") return "";

                Ext.each(this.store.getRange(), function (r) {
                    if (r.get(this.valueField) == values[this.valueField]) {
                        record = r;
                        return false;
                    }
                }, this);

                selected = record ? this.picker.getSelectionModel().isSelected(record) : false;

                if (selected) return "x-mcombo-item-checked";

                return "x-mcombo-item-unchecked";
            }, this, [], true);

            this.picker.on("viewready", this.onViewReady, this, { single: true });
        }

        return this.picker;
    },
    onViewReady: function () {
        Ext.each(this.valueModels, function (r) {
            this.selectRecord(r);
        }, this);
    },
    onListSelect: function (model, record) {
        if (!this.ignoreSelection)
            this.selectRecord(record);
    },
    onListDeselect: function (model, record) {
        if (!this.ignoreSelection)
            this.deselectRecord(record);
    },
    initComponent: function () {
        var me = this;

        if (!Ext.isEmpty(me.storeUrl)) {
            me.store = Ext.create('Ext.data.Store', {
                pageSize: 1024,
                fields: [
                    { name: 'id', type: 'auto' },
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

        me.editable = false;
        me.callParent(arguments);
    },
    getDisplayValue: function () {
        var value = this.displayTpl.apply(this.displayTplData);
        return value;
    },
    isSelected: function (record) {
        if (Ext.isNumber(record))
            record = this.store.getAt(record);

        if (Ext.isString(record)) {
            Ext.each(this.store.getRange(), function (r) {
                if (r.get(this.valueField) == record) {
                    record = r;
                    return false;
                }
            }, this);
        }

        return Ext.Array.indexOf(this.valueModels, record) !== -1;
    },
    deselectRecord: function (record) {
        if (!this.picker)
            return;

        switch (this.selectionMode) {
            case "checkbox":
                this.picker.refreshNode(this.store.indexOf(record));
                break;
            case "selection":
                if (this.picker.getSelectionModel().isSelected(record))
                    this.picker.deselect(this.store.indexOf(record));
                break;
            case "all":
                if (this.picker.getSelectionModel().isSelected(record))
                    this.picker.deselect(this.store.indexOf(record));
                this.picker.refreshNode(this.store.indexOf(record));
                break;
        }
    },
    selectRecord: function (record) {
        if (!this.picker)
            return;

        switch (this.selectionMode) {
            case "checkbox":
                this.picker.refreshNode(this.store.indexOf(record));
                break;
            case "selection":
                if (!this.picker.getSelectionModel().isSelected(record))
                    this.picker.select(this.store.indexOf(record), true);
                break;
            case "all":
                if (!this.picker.getSelectionModel().isSelected(record))
                    this.picker.select(this.store.indexOf(record), true);
                this.picker.refreshNode(this.store.indexOf(record));
                break;
        }
    },
    selectAll: function () {
        this.setValue(this.store.getRange());
    },
    deselectItem: function (record) {
        if (Ext.isNumber(record))
            record = this.store.getAt(record);

        if (Ext.isString(record)) {
            Ext.each(this.store.getRange(), function (r) {
                if (r.get(this.valueField) == record) {
                    record = r;
                    return false;
                }
            }, this);
        }

        if (Ext.Array.indexOf(this.valueModels, record) !== -1) {
            this.setValue(Ext.Array.remove(this.valueModels, record));
            this.deselectRecord(record);
        }
    },
    selectItem: function (record) {
        if (Ext.isNumber(record))
            record = this.store.getAt(record);

        if (Ext.isString(record)) {
            Ext.each(this.store.getRange(), function (r) {
                if (r.get(this.valueField) == record) {
                    record = r;
                    return false;
                }
            }, this);
        }

        if (Ext.Array.indexOf(this.valueModels, record) === -1) {
            this.valueModels.push(record);
            this.setValue(this.valueModels);
        }
    },
    getSelectedRecords: function () {
        return this.valueModels;
    },
    getSelectedIndexes: function () {
        var indexes = [];
        Ext.each(this.valueModels, function (record) {
            indexes.push(this.store.indexOf(record));
        }, this);

        return indexes;
    },
    getSelectedValues: function () {
        var values = [];
        Ext.each(this.valueModels, function (record) {
            values.push(record.get(this.valueField));
        }, this);

        return values;
    },
    getSelectedText: function () {
        var text = [];
        Ext.each(this.valueModels, function (record) {
            text.push(record.get(this.displayField));
        }, this);

        return text;
    },
    getSelection: function () {
        var selection = [];
        Ext.each(this.valueModels, function (record) {
            selection.push({
                text: record.get(this.displayField),
                value: record.get(this.valueField),
                index: this.store.indexOf(record)
            });
        }, this);

        return selection;
    },
    setValue: function (value, doSelect) {
        this.callParent(arguments);
        Ext.each(this.valueModels, function (r) {
            this.selectRecord(r);
        }, this);
    },
    reset: function () {
        this.callParent(arguments);
        if (this.picker && this.picker.rendered) {
            this.picker.refresh();
        }
    },
    clearValue: function () {
        this.callParent(arguments);
        if (this.picker && this.picker.rendered) {
            this.picker.refresh();
        }
    }
});