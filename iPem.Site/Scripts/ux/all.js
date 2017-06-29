/* ========================================================================
 * Ux: DateTimePicker.js
 * /Scripts/ux/DateTimePicker.js
 * ========================================================================
 */

Ext.ux.DateTime = new function () {
    Ext.apply(this, {
        defaultFormat: 'Y-m-d H:i:s',
        dateFormat: 'Y-m-d',
        nowString: function (format) {
            return Ext.Date.format(this.now(), format || this.defaultFormat);
        },
        todayString: function (format) {
            return Ext.Date.format(this.today(), format || this.dateFormat);
        },
        now: function () {
            return new Date(Date.now());
        },
        today: function () {
            return Ext.Date.clearTime(this.now());
        },
        addSeconds: function (date, value, format) {
            if (!Ext.isDate(date)) return null;

            var newDate = Ext.Date.add(date, Ext.Date.SECOND, value);
            return Ext.Date.format(newDate, format || this.defaultFormat);
        },
        addMinutes: function (date, value, format) {
            if (!Ext.isDate(date)) return null;

            var newDate = Ext.Date.add(date, Ext.Date.MINUTE, value);
            return Ext.Date.format(newDate, format || this.defaultFormat);
        },
        addHours: function (date, value, format) {
            if (!Ext.isDate(date)) return null;

            var newDate = Ext.Date.add(date, Ext.Date.HOUR, value);
            return Ext.Date.format(newDate, format || this.defaultFormat);
        },
        addDays: function (date, value, format) {
            if (!Ext.isDate(date)) return null;

            var newDate = Ext.Date.add(date, Ext.Date.DAY, value);
            return Ext.Date.format(newDate, format || this.defaultFormat);
        },
        addMonths: function (date, value, format) {
            if (!Ext.isDate(date)) return null;

            var newDate = Ext.Date.add(date, Ext.Date.MONTH, value);
            return Ext.Date.format(newDate, format || this.defaultFormat);
        },
        addYears: function (date, value, format) {
            if (!Ext.isDate(date)) return null;

            var newDate = Ext.Date.add(date, Ext.Date.YEAR, value);
            return Ext.Date.format(newDate, format || this.defaultFormat);
        }
    });
};

Ext.define('Ext.ux.DateTimePicker', {
    extend: 'Ext.form.field.Picker',
    xtype: 'datetimepicker',
    format: "yyyy-MM-dd HH:mm:ss",
    editable: false,
    minDate: "1900-01-01 00:00:00",
    maxDate: "2099-12-31 23:59:59",
    doubleCalendar: false,
    triggerCls: Ext.baseCSSPrefix + 'form-date-trigger',
    onTriggerClick: function () {
        var me = this,
            dom = me.inputEl;

        WdatePicker({
            el: dom.id,
            dateFmt: me.format,
            isShowClear: false,
            isShowWeek: true,
            minDate: me.minDate,
            maxDate: me.maxDate,
            doubleCalendar: me.doubleCalendar
        });
    }
});

/* ========================================================================
 * Ux: IFrame.js
 * /Scripts/ux/IFrame.js
 * ========================================================================
 */

/*!
 * Ext JS Library 4.0
 * Copyright(c) 2006-2011 Sencha Inc.
 * licensing@sencha.com
 * http://www.sencha.com/license
 */

/**
 * Barebones iframe implementation. For serious iframe work, see the
 * ManagedIFrame extension
 * (http://www.sencha.com/forum/showthread.php?71961).
 */
Ext.define('Ext.ux.IFrame', {
    extend: 'Ext.Component',

    alias: 'widget.uxiframe',

    loadMask: 'Loading...',

    src: 'about:blank',

    renderTpl: [
        '<iframe src="{src}" name="{frameName}" width="100%" height="100%" frameborder="0"></iframe>'
    ],

    initComponent: function () {
        this.callParent();

        this.frameName = this.frameName || this.id + '-frame';

        this.addEvents(
            'beforeload',
            'load'
        );

        Ext.apply(this.renderSelectors, {
            iframeEl: 'iframe'
        });
    },

    initEvents : function() {
        var me = this;
        me.callParent();
        me.iframeEl.on('load', me.onLoad, me);
    },

    initRenderData: function() {
        return Ext.apply(this.callParent(), {
            src: this.src,
            frameName: this.frameName
        });
    },

    getBody: function() {
        var doc = this.getDoc();
        return doc.body || doc.documentElement;
    },

    getDoc: function() {
        try {
            return this.getWin().document;
        } catch (ex) {
            return null;
        }
    },

    getWin: function() {
        var me = this,
            name = me.frameName,
            win = Ext.isIE
                ? me.iframeEl.dom.contentWindow
                : window.frames[name];
        return win;
    },

    getFrame: function() {
        var me = this;
        return me.iframeEl.dom;
    },

    beforeDestroy: function () {
        this.cleanupListeners(true);
        this.callParent();
    },
    
    cleanupListeners: function(destroying){
        var doc, prop;

        if (this.rendered) {
            try {
                doc = this.getDoc();
                if (doc) {
                    Ext.EventManager.removeAll(doc);
                    if (destroying) {
                        for (prop in doc) {
                            if (doc.hasOwnProperty && doc.hasOwnProperty(prop)) {
                                delete doc[prop];
                            }
                        }
                    }
                }
            } catch(e) { }
        }
    },

    onLoad: function() {
        var me = this,
            doc = me.getDoc(),
            fn = me.onRelayedEvent;

        if (doc) {
            try {
                Ext.EventManager.removeAll(doc);

                // These events need to be relayed from the inner document (where they stop
                // bubbling) up to the outer document. This has to be done at the DOM level so
                // the event reaches listeners on elements like the document body. The effected
                // mechanisms that depend on this bubbling behavior are listed to the right
                // of the event.
                Ext.EventManager.on(doc, {
                    mousedown: fn, // menu dismisal (MenuManager) and Window onMouseDown (toFront)
                    mousemove: fn, // window resize drag detection
                    mouseup: fn,   // window resize termination
                    click: fn,     // not sure, but just to be safe
                    dblclick: fn,  // not sure again
                    scope: me
                });
            } catch(e) {
                // cannot do this xss
            }

            // We need to be sure we remove all our events from the iframe on unload or we're going to LEAK!
            Ext.EventManager.on(this.getWin(), 'beforeunload', me.cleanupListeners, me);

            this.el.unmask();
            this.fireEvent('load', this);

        } else if(me.src && me.src != '') {

            this.el.unmask();
            this.fireEvent('error', this);
        }


    },

    onRelayedEvent: function (event) {
        // relay event from the iframe's document to the document that owns the iframe...

        var iframeEl = this.iframeEl,

            // Get the left-based iframe position
            iframeXY = Ext.Element.getTrueXY(iframeEl),
            originalEventXY = event.getXY(),

            // Get the left-based XY position.
            // This is because the consumer of the injected event (Ext.EventManager) will
            // perform its own RTL normalization.
            eventXY = Ext.EventManager.getPageXY(event.browserEvent);

        // the event from the inner document has XY relative to that document's origin,
        // so adjust it to use the origin of the iframe in the outer document:
        event.xy = [iframeXY[0] + eventXY[0], iframeXY[1] + eventXY[1]];

        event.injectEvent(iframeEl); // blame the iframe for the event...

        event.xy = originalEventXY; // restore the original XY (just for safety)
    },

    load: function (src) {
        var me = this,
            text = me.loadMask,
            frame = me.getFrame();

        if (me.fireEvent('beforeload', me, src) !== false) {
            if (text && me.el) {
                me.el.mask(text);
            }

            frame.src = me.src = (src || me.src);
        }
    }
});

/* ========================================================================
 * Ux: Label.js
 * /Scripts/ux/Label.js
 * ========================================================================
 */

Ext.define("Ext.ux.Label", {
    extend: "Ext.form.Label",
    xtype: 'iconlabel',
    requires: ['Ext.XTemplate'],
    iconAlign: "left",
    baseCls: Ext.baseCSSPrefix + "label",
    renderTpl: [
        '<tpl if="iconAlign == \'left\'">',
           '<img src="{[Ext.BLANK_IMAGE_URL]}" class="' + Ext.baseCSSPrefix + 'label-icon',
           '<tpl if="!Ext.isEmpty(iconCls)"> {iconCls}</tpl>',
           '"/>',
        '</tpl>',
        '<span class="' + Ext.baseCSSPrefix + 'label-value">',
        '<tpl if="!Ext.isEmpty(html)">{html}</tpl>',
        '</span>',
        '<tpl if="iconAlign == \'right\'">',
           '<img src="{[Ext.BLANK_IMAGE_URL]}" class="' + Ext.baseCSSPrefix + 'label-icon',
           '<tpl if="!Ext.isEmpty(iconCls)"> {iconCls}</tpl>',
           '"/>',
        '</tpl>',
    ],

    getElConfig: function () {
        var me = this;
        return Ext.apply(me.callParent(), {
            tag: 'label',
            id: me.id,
            htmlFor: me.forId || ''
        });
    },

    beforeRender: function () {
        var me = this;

        me.callParent();

        Ext.apply(me.renderData, {
            iconAlign: me.iconAlign,
            iconCls: me.iconCls || "",
            html: this.getDisplayText(me.text ? me.text : me.html, !!me.text)
        });

        Ext.apply(me.renderSelectors, {
            imgEl: '.' + Ext.baseCSSPrefix + 'label-icon',
            textEl: '.' + Ext.baseCSSPrefix + 'label-value'
        });

        delete me.html;
    },

    afterRender: function () {
        this.callParent(arguments);

        if (Ext.isEmpty(this.iconCls)) {
            this.imgEl.setDisplayed(false);
        }

        if (this.editor) {
            if (Ext.isEmpty(this.editor.field)) {
                this.editor.field = {
                    xtype: "textfield"
                };
            }

            this.editor.target = this.textEl;
            this.editor = new Ext.Editor(this.editor);
        }
    },

    getContentTarget: function () {
        return this.textEl;
    },

    getText: function (encode) {
        return this.rendered ? encode === true ? Ext.String.htmlEncode(this.textEl.dom.innerHTML) : this.textEl.dom.innerHTML : this.text;
    },

    getDisplayText: function (text, encode) {
        var t = text || this.text || this.html || "",
            x = encode !== false ? Ext.String.htmlEncode(t) : t;

        return (Ext.isEmpty(t) && !Ext.isEmpty(this.emptyText)) ? this.emptyText : !Ext.isEmpty(this.format) ? Ext.String.format(this.format, x) : x
    },

    setText: function (text, encode) {
        encode = encode !== false;

        if (encode) {
            this.text = text;
            delete this.html;
        } else {
            this.html = text;
            delete this.text;
        }

        if (this.rendered) {
            this.textEl.dom.innerHTML = this.getDisplayText(null, encode);
            this.updateLayout();
        }

        return this;
    },

    setIconCls: function (cls) {
        var oldCls = this.iconCls;

        this.iconCls = cls;

        if (this.rendered) {
            this.imgEl.replaceCls(oldCls, this.iconCls);
            this.imgEl.setDisplayed(!Ext.isEmpty(cls));
        }
    },

    setTextWithIcon: function (text, cls, encode){
        this.setIconCls(cls);
        this.setText(text, encode);
    },

    append: function (text, appendLine) {
        this.setText([this.getText(), text, appendLine === true ? "<br/>" : ""].join(""), false);
    },

    appendLine: function (text) {
        this.append(text, true);
    }
});

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

/* ========================================================================
 * Ux: SingleCombo.js
 * /Scripts/ux/SingleCombo.js
 * ========================================================================
 */

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
                    { name: 'id', type: 'string' },
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

/* ========================================================================
 * Ux: TreePicker.js
 * /Scripts/ux/TreePicker.js
 * ========================================================================
 */

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
                mask: new Ext.LoadMask({ target: picker, msg: '正在处理...' }),
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
                            if (n.get('checked') !== null) {
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
                            if (n.get('checked') !== null) {
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
        me.fireEvent('checkchange', me, nodes);
    },

    onExpand: function () {
        var me = this,
            picker = me.picker,
            value = me.value;

        if (me.multiSelect === true) {
            var root = picker.getRootNode();
            if (root && root.hasChildNodes()) {
                root.eachChild(function (c) {
                    c.cascadeBy(function (n) {
                        if (n.get('checked') !== null) {
                            n.set('checked', Ext.Array.contains(value, n.data.id));
                        }
                    });
                });
            }

            var nodes = picker.getChecked();
            Ext.Array.each(nodes, function (item, index, items) {
                picker.selectPath(item.getPath());
            });
        } else {
            value = Ext.isArray(value) ? (value.length > 0 ? value[0] : null) : value;
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