Ext.define('Ext.ux.TreePicker', {
    extend: 'Ext.form.field.Picker',
    requires: ['Ext.tree.Panel'],
    xtype: 'treepicker',

    /**
     * @cfg {String} [triggerCls='x-form-arrow-trigger']
     * An additional CSS class used to style the trigger button. The trigger will always get the {@link #triggerBaseCls}
     * by default and `triggerCls` will be **appended** if specified.
     */
    triggerCls: Ext.baseCSSPrefix + 'form-arrow-trigger',

    /**
    * @cfg {Boolean} editable
    * False to prevent the user from typing text directly into the field; the field can only have its value set via
    * selecting a value from the picker. In this state, the picker can also be opened by clicking directly on the input
    * field itself.
    */
    editable: false,

    /**
     * @cfg {Ext.data.TreeStore} store (required)
     * The {@link Ext.data.TreeStore Store} the tree should use as its data source.
     */

    /**
     * @cfg {Boolean} multiSelect
     * If set to `true`, allows the combo field to hold more than one value at a time, and allows selecting multiple
     * items from the dropdown list. The combo's text field will show all selected values separated by the
     * {@link #delimiter}.
     */
    multiSelect: false,

    /**
     * @cfg {String} delimiter
     * The character(s) used to separate the {@link #displayField display values} of multiple selected items when
     * `{@link #multiSelect} = true`.
     */
    delimiter: ',',

    /**
     * @cfg {String} displayField
     * The underlying {@link Ext.data.Field#name data field name} to bind to this tree.
     */
    displayField: 'text',

    /**
     * @cfg {Number} pageSize
     * If greater than `0`, a {@link Ext.toolbar.Paging} is displayed in the footer of the dropdown list and the
     * {@link #doQuery filter queries} will execute with page start and {@link Ext.view.BoundList#pageSize limit}
     * parameters. Only applies when `{@link #queryMode} = 'remote'`.
     */
    pageSize: 0,

    /**
    * @cfg {Number} pickerHeight
    * The height of the tree dropdown. Defaults to 300.
    */
    pickerHeight: 300,

    /**
    * @cfg {Boolean} selectOnLeaf
    * Defaults to `false`.
    */
    selectOnLeaf: false,

    /**
    * @cfg {Boolean} rootVisible
    * Defaults to `false`.
    */
    rootVisible: false,

    /**
    * @cfg {Boolean} searchVisible
    * Defaults to `false`.
    */
    searchVisible: false,

    searchChange: Ext.emptyFn,

    initComponent: function () {
        var me = this,
            store = me.store;

        me.mon(store, {
            load: me.onLoad,
            exception: me.onException,
            scope: me
        });

        this.addEvents(
            'select',
            'checkchange',
            'search',
            'syncselect'
        );

        if (!me.displayTpl) {
            me.displayTpl = new Ext.XTemplate(
                '<tpl for=".">' +
                    '{[typeof values === "string" ? values : values["' + me.displayField + '"]]}' +
                    '<tpl if="xindex < xcount">' + me.delimiter + '</tpl>' +
                '</tpl>'
            );
        } else if (Ext.isString(me.displayTpl)) {
            me.displayTpl = new Ext.XTemplate(me.displayTpl);
        }

        me.callParent(arguments);
    },

    onException: function () {
        this.collapse();
    },

    onLoad: function (store, parent, records, success) {
        var me = this,
            picker = me.picker,
            value = me.value,
            node;

        if (success && value) {
            me.setValue(value, false);
            if (!me.multiSelect) {
                value = Ext.isArray(value) ? value[0] : value;
                if (value) {
                    node = Ext.Array.findBy(records, function (item, index) {
                        return item.getId() === value;
                    });

                    if (!Ext.isEmpty(node)) {
                        picker.getSelectionModel().select(node);
                    }
                }
            } else {
                value = Ext.Array.from(value);
                if (parent.hasChildNodes()) {
                    parent.eachChild(function (c) {
                        c.cascadeBy(function (n) {
                            var checked = n.get('checked');
                            if (checked !== null) {
                                n.set('checked', Ext.Array.contains(value, n.data.id));
                            }
                        });
                    });
                }
            }
        }
    },

    createPicker: function () {
        var me = this;

        var search = me.search = Ext.create('Ext.form.field.Text', {
            emptyText: $$iPems.lang.PlsInputEmptyText,
            flex: 1,
            listeners: {
                change: me.searchChange
            }
        });

        var query = Ext.create('Ext.button.Button', {
            glyph: 0xf005,
            handler: function (btn, e) {
                me.fireEvent('search', me, search, search.getRawValue());
            }
        });

        var picker = me.picker = Ext.create('Ext.tree.Panel', {
            shrinkWrapDock: 2,
            floating: true,
            hidden: true,
            store: me.store,
            displayField: me.displayField,
            rootVisible: me.rootVisible,
            focusOnToFront: false,
            pageSize: me.pageSize,
            height: me.pickerHeight,
            shadow: false,
            viewConfig: {
                loadMask: true,
                preserveScrollOnRefresh: true
            },
            tbar: me.searchVisible ? [search, query] : false
        });

        me.mon(picker, {
            itemclick: me.onItemClick,
            checkchange: me.onCheckChange,
            scope: me
        });

        return picker;
    },

    getPicker: function () {
        var me = this;
        return me.picker || (me.picker = me.createPicker());
    },

    alignPicker: function () {
        var me = this,
            picker = me.getPicker(),
            heightAbove = me.getPosition()[1] - Ext.getBody().getScroll().top,
            heightBelow = Ext.Element.getViewHeight() - heightAbove - me.getHeight(),
            space = Math.max(heightAbove, heightBelow);

        // Then ensure that vertically, the dropdown will fit into the space either above or below the inputEl.
        if (picker.getHeight() > space - 5) {
            picker.setHeight(space - 5); // have some leeway so we aren't flush against
        }
        me.callParent();
    },

    resetPicker: function () {
        var me = this,
            picker = me.picker;

        me.value = me.displayTplData = me.valueModels = null;

        if (picker) {
            if (!me.multiSelect) {
                picker.getSelectionModel().deselectAll();
            } else {
                var root = me.findRoot();
                if (root.hasChildNodes()) {
                    root.eachChild(function (c) {
                        c.cascadeBy(function (n) {
                            var checked = n.get('checked');
                            if (checked !== null) {
                                n.set('checked', false);
                            }
                        });
                    });
                }
            }
        }
    },

    onItemClick: function (view, record, node, rowIndex, e) {
        var me = this,
            selection = me.picker.getSelectionModel().getSelection();

        if (!me.multiSelect && selection.length) {
            if (me.selectOnLeaf === true
                && record.get('leaf') === false)
                return false;

            if (record.getId() === selection[0].getId()) {
                // Make sure we also update the display value if it's only partial
                me.displayTplData = [record.data];
                me.setValue(record);
                me.setRawValue(me.getDisplayValue());
                me.collapse();
                me.inputEl.focus();
                me.fireEvent('select', me, record);
            }
        }
    },

    onCheckChange: function (node, checked) {
        var me = this,
            nodes = me.picker.getChecked();

        me.setValue(nodes, false);
        //me.inputEl.focus();
        me.fireEvent('checkchange', me, nodes);
    },

    onExpand: function () {
        var me = this,
            picker = me.picker,
            value = me.value;

        if (value) {
            if (!me.multiSelect) {
                value = Ext.isArray(value) ? value[0] : value;
                if (value) {
                    node = me.findRecord(value);
                }

                if (Ext.isEmpty(node)) {
                    node = me.findRoot();
                }

                picker.selectPath(node.getPath());
            } else {
                value = Ext.Array.from(value);
                node = me.findRoot();
                if (node.hasChildNodes()) {
                    node.eachChild(function (c) {
                        c.cascadeBy(function (n) {
                            var checked = n.get('checked');
                            if (checked !== null) {
                                n.set('checked', Ext.Array.contains(value, n.data.id));
                            }
                        });
                    });
                }
            }
        }

        Ext.defer(function () {
            picker.focus();
        }, 5);
    },

    onCollapse: function () {
    },

    findRecord: function (value) {
        return this.store.getNodeById(value);
    },

    findRoot:function() {
        return this.store.getRootNode();
    },

    setValue: function (value, fireSelection) {
        var me = this,
            inputEl = me.inputEl,
            i, len, record,
            dataObj,
            matchedRecords = [],
            displayTplData = [],
            processedValue = [],
            selection = [];

        if (me.store.loading) {
            me.value = value;
            return me;
        }

        value = Ext.Array.from(value);
        for (i = 0, len = value.length; i < len; i++) {
            record = value[i];
            if (!record || !record.isModel) {
                record = me.findRecord(record);
            }

            if (record) {
                matchedRecords.push(record);
                displayTplData.push(record.data);
                processedValue.push(record.getId());
            } else {
                processedValue.push(value[i]);
                dataObj = {};
                dataObj[me.displayField] = value[i];
                displayTplData.push(dataObj);
                selection.push(value[i]);
            }
        }

        me.value = me.multiSelect ? processedValue : processedValue[0];
        if (!Ext.isDefined(me.value)) {
            me.value = null;
        }
        me.displayTplData = displayTplData;
        me.valueModels = matchedRecords;

        if (inputEl && me.emptyText && !Ext.isEmpty(value)) {
            inputEl.removeCls(me.emptyCls);
        }

        me.setRawValue(me.getDisplayValue());
        me.applyEmptyText();
        if (!(fireSelection === false) && selection.length > 0) {
            me.fireEvent('syncselect', me, selection);
        }

        return me;
    },

    getDisplayValue: function () {
        return this.displayTpl.apply(this.displayTplData);
    },

    getValue: function () {
        var me = this,
            picker = me.picker,
            rawValue = me.getRawValue(), 
            value = me.value;

        if (me.getDisplayValue() !== rawValue) {
            value = rawValue;
            me.resetPicker();
        }

        return value;
    },

    getSubmitValue: function () {
        var value = this.getValue();
        return Ext.isEmpty(value) ? '' : value;
    },

    clearValue: function () {
        this.setValue([]);
    }
});