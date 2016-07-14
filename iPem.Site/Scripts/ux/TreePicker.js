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
    * @cfg {Number} minWidth
    * The min width of the tree dropdown. Defaults to 215.
    */
    minWidth: 215,

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

    /**
    * @cfg {Array} remoteValues
    * Defaults to [].
    */
    remoteValues: [],

    /**
    * @cfg {Number} queryDelay
    * Defaults to 300.
    */
    queryDelay: 300,

    /**
     * @cfg {String} storeUrl
     * The URL from which to request the data object.
     */
    storeUrl: null,

    /**
     * @cfg {String} searchUrl
     * The URL from which to request the data object.
     */
    searchUrl: null,

    /**
     * @cfg {String} queryUrl
     * The URL from which to request the data object.
     */
    queryUrl: null,

    searchChange: function (me, newValue, oldValue) {
        delete me._filterData;
        delete me._filterIndex;
    },

    doRawSearch: function (field, text) {
        var me = this;

        if (Ext.isEmpty(me.searchUrl))
            return false;

        var picker = me.getPicker(),
            root = me.findRoot(),
            separator = '/';

        if (Ext.isEmpty(text, false))
            return;

        if (text.length < 2)
            return;

        if (!picker)
            return;

        if (field._filterData != null && field._filterIndex != null) {
            var index = field._filterIndex + 1;
            var paths = field._filterData;
            if (index >= paths.length) {
                index = 0;
            }

            var nodes = Ext.Array.from(paths[index]);
            var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
            picker.selectPath(path);
            field._filterIndex = index;
        } else {
            Ext.Ajax.request({
                url: me.searchUrl,
                params: { text: text },
                mask: new Ext.LoadMask({ target: picker, msg: '正在处理，请稍后...' }),
                success: function (response, options) {
                    var data = Ext.decode(response.responseText, true);
                    if (data.success) {
                        var len = data.data.length;
                        if (len > 0) {
                            var nodes = Ext.Array.from(data.data[0]);
                            var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), nodes.join(separator));
                            picker.selectPath(path);

                            field._filterData = data.data;
                            field._filterIndex = 0;
                        }
                    } else {
                        Ext.Msg.show({ title: '系统错误', msg: data.message, buttons: Ext.Msg.OK, icon: Ext.Msg.ERROR });
                    }
                }
            });
        }
    },

    doRawQuery: function () {
        var me = this;
        if (Ext.isEmpty(me.queryUrl))
            return false;

        var picker = me.getPicker(),
            values = me.remoteValues,
            separator = '/',
            root;

        Ext.Ajax.request({
            url: me.queryUrl,
            params: { nodes: values },
            success: function (response, options) {
                var data = Ext.decode(response.responseText, true);
                if (data.success && picker) {
                    root = picker.getRootNode();
                    Ext.Array.each(data.data, function (item, index, all) {
                        item = Ext.Array.from(item);

                        var path = Ext.String.format("{0}{1}{0}{2}", separator, root.getId(), item.join(separator));
                        picker.expandPath(path);
                    });
                }
            }
        });
    },

    initComponent: function () {
        var me = this;

        if (!Ext.isEmpty(me.storeUrl)) {
            me.store = Ext.create('Ext.data.TreeStore', {
                root: {
                    id: 'root',
                    text: '全部',
                    icon: $$iPems.icons.Home,
                    root: true
                },
                proxy: {
                    type: 'ajax',
                    url: me.storeUrl,
                    extraParams: {
                        multiselect: me.multiSelect,
                        leafselect: me.selectOnLeaf
                    },
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

        me.mon(me.store, {
            load: me.onLoad,
            exception: me.onException,
            scope: me
        });

        this.addEvents(
            'select',
            'checkchange'
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
        me.doQueryTask = new Ext.util.DelayedTask(me.doRawQuery, me);
    },

    onException: function () {
        this.collapse();
    },

    onLoad: function (store, parent, records, success) {
        var me = this,
            picker = me.picker,
            value = me.value;

        if (success && value != null) {
            me.setValue(value);
            if (me.multiSelect === true) {
                value = Ext.Array.from(value);
                if (parent && parent.hasChildNodes()) {
                    parent.eachChild(function (c) {
                        c.cascadeBy(function (n) {
                            var checked = n.get('checked');
                            if (checked !== null) {
                                n.set('checked', Ext.Array.contains(value, n.data.id));
                            }
                        });
                    });
                }
            } else {
                value = Ext.isArray(value) ? value[0] : value;
                if (value) {
                    var node = me.findRecord(value);
                    if (node) picker.getSelectionModel().select(node);
                }
            }
        }
    },

    createPicker: function () {
        var me = this;

        var search = me.search = Ext.create('Ext.form.field.Text', {
            emptyText: '请输入筛选条件...',
            flex: 1,
            listeners: {
                change: me.searchChange
            }
        });

        var query = Ext.create('Ext.button.Button', {
            glyph: 0xf005,
            handler: function (btn, e) {
                var text = me.search.getRawValue();
                me.doRawSearch(me.search, text);
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
            minWidth: me.minWidth,
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

        if (me.multiSelect === true) {
            var nodes = picker.getChecked();
            Ext.Array.each(nodes, function (item, index, items) {
                picker.selectPath(item.getPath());
            });
        } else {
            value = Ext.isArray(value) ? value[0] : value;
            if (value) {
                var node = me.findRecord(value);
                if (node) picker.selectPath(node.getPath());
            }
        }

        if (me.remoteValues.length > 0)
            me.doQueryTask.delay(me.queryDelay);

        Ext.defer(function () {
            me.picker.focus();
        }, 5);
    },

    onCollapse: function () {
    },

    findRecord: function (value) {
        return this.store.getNodeById(value);
    },

    findRoot: function () {
        return this.store.getRootNode();
    },

    setValue: function (value) {
        var me = this,
            inputEl = me.inputEl,
            i, len, record,
            dataObj,
            matchedRecords = [],
            displayTplData = [],
            processedValue = [];

        me.remoteValues = [];
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
                me.remoteValues.push(value[i]);
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