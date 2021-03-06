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
 * Ux: ItemSelector.js & MultiSelect.js
 * /Scripts/ux/ItemSelector.js
 * ========================================================================
 */

Ext.define('Ext.ux.form.MultiSelect', {

    extend: 'Ext.form.FieldContainer',

    mixins: {
        bindable: 'Ext.util.Bindable',
        field: 'Ext.form.field.Field'
    },

    alternateClassName: 'Ext.ux.Multiselect',
    alias: ['widget.multiselectfield', 'widget.multiselect'],

    requires: ['Ext.panel.Panel', 'Ext.view.BoundList', 'Ext.layout.container.Fit'],

    uses: ['Ext.view.DragZone', 'Ext.view.DropZone'],

    layout: 'anchor',

    /**
     * @cfg {String} [dragGroup=""] The ddgroup name for the MultiSelect DragZone.
     */

    /**
     * @cfg {String} [dropGroup=""] The ddgroup name for the MultiSelect DropZone.
     */

    /**
     * @cfg {String} [title=""] A title for the underlying panel.
     */

    /**
     * @cfg {Boolean} [ddReorder=false] Whether the items in the MultiSelect list are drag/drop reorderable.
     */
    ddReorder: false,

    /**
     * @cfg {Object/Array} tbar An optional toolbar to be inserted at the top of the control's selection list.
     * This can be a {@link Ext.toolbar.Toolbar} object, a toolbar config, or an array of buttons/button configs
     * to be added to the toolbar. See {@link Ext.panel.Panel#tbar}.
     */

    /**
     * @cfg {String} [appendOnly=false] `true` if the list should only allow append drops when drag/drop is enabled.
     * This is useful for lists which are sorted.
     */
    appendOnly: false,

    /**
     * @cfg {String} [displayField="text"] Name of the desired display field in the dataset.
     */
    displayField: 'text',

    /**
     * @cfg {String} [valueField="text"] Name of the desired value field in the dataset.
     */

    /**
     * @cfg {Boolean} [allowBlank=true] `false` to require at least one item in the list to be selected, `true` to allow no
     * selection.
     */
    allowBlank: true,

    /**
     * @cfg {Number} [minSelections=0] Minimum number of selections allowed.
     */
    minSelections: 0,

    /**
     * @cfg {Number} [maxSelections=Number.MAX_VALUE] Maximum number of selections allowed.
     */
    maxSelections: Number.MAX_VALUE,

    /**
     * @cfg {String} [blankText="This field is required"] Default text displayed when the control contains no items.
     */
    blankText: 'This field is required',

    /**
     * @cfg {String} [minSelectionsText="Minimum {0}item(s) required"] 
     * Validation message displayed when {@link #minSelections} is not met. 
     * The {0} token will be replaced by the value of {@link #minSelections}.
     */
    minSelectionsText: 'Minimum {0} item(s) required',

    /**
     * @cfg {String} [maxSelectionsText="Maximum {0}item(s) allowed"] 
     * Validation message displayed when {@link #maxSelections} is not met
     * The {0} token will be replaced by the value of {@link #maxSelections}.
     */
    maxSelectionsText: 'Maximum {0} item(s) required',

    /**
     * @cfg {String} [delimiter=","] The string used to delimit the selected values when {@link #getSubmitValue submitting}
     * the field as part of a form. If you wish to have the selected values submitted as separate
     * parameters rather than a single delimited parameter, set this to `null`.
     */
    delimiter: ',',

    /**
     * @cfg String [dragText="{0} Item{1}"] The text to show while dragging items.
     * {0} will be replaced by the number of items. {1} will be replaced by the plural
     * form if there is more than 1 item.
     */
    dragText: '{0} Item{1}',

    /**
     * @cfg {Ext.data.Store/Array} store The data source to which this MultiSelect is bound (defaults to `undefined`).
     * Acceptable values for this property are:
     * <div class="mdetail-params"><ul>
     * <li><b>any {@link Ext.data.Store Store} subclass</b></li>
     * <li><b>an Array</b> : Arrays will be converted to a {@link Ext.data.ArrayStore} internally.
     * <div class="mdetail-params"><ul>
     * <li><b>1-dimensional array</b> : (e.g., <tt>['Foo','Bar']</tt>)<div class="sub-desc">
     * A 1-dimensional array will automatically be expanded (each array item will be the combo
     * {@link #valueField value} and {@link #displayField text})</div></li>
     * <li><b>2-dimensional array</b> : (e.g., <tt>[['f','Foo'],['b','Bar']]</tt>)<div class="sub-desc">
     * For a multi-dimensional array, the value in index 0 of each item will be assumed to be the combo
     * {@link #valueField value}, while the value at index 1 is assumed to be the combo {@link #displayField text}.
     * </div></li></ul></div></li></ul></div>
     */

    ignoreSelectChange: 0,

    /**
     * @cfg {Object} listConfig
     * An optional set of configuration properties that will be passed to the {@link Ext.view.BoundList}'s constructor.
     * Any configuration that is valid for BoundList can be included.
     */

    initComponent: function () {
        var me = this;

        me.bindStore(me.store, true);
        if (me.store.autoCreated) {
            me.valueField = me.displayField = 'field1';
            if (!me.store.expanded) {
                me.displayField = 'field2';
            }
        }

        if (!Ext.isDefined(me.valueField)) {
            me.valueField = me.displayField;
        }
        me.items = me.setupItems();


        me.callParent();
        me.initField();
        me.addEvents('drop');
    },

    setupItems: function () {
        var me = this;

        me.boundList = Ext.create('Ext.view.BoundList', Ext.apply({
            anchor: 'none 100%',
            deferInitialRefresh: false,
            border: 1,
            multiSelect: true,
            store: me.store,
            displayField: me.displayField,
            disabled: me.disabled
        }, me.listConfig));
        me.boundList.getSelectionModel().on('selectionchange', me.onSelectChange, me);

        // Only need to wrap the BoundList in a Panel if we have a title.
        if (!me.title) {
            return me.boundList;
        }

        // Wrap to add a title
        me.boundList.border = false;
        return {
            border: true,
            anchor: 'none 100%',
            layout: 'anchor',
            title: me.title,
            tbar: me.tbar,
            items: me.boundList
        };
    },

    onSelectChange: function (selModel, selections) {
        if (!this.ignoreSelectChange) {
            this.setValue(selections);
        }
    },

    getSelected: function () {
        return this.boundList.getSelectionModel().getSelection();
    },

    // compare array values
    isEqual: function (v1, v2) {
        var fromArray = Ext.Array.from,
            i = 0,
            len;

        v1 = fromArray(v1);
        v2 = fromArray(v2);
        len = v1.length;

        if (len !== v2.length) {
            return false;
        }

        for (; i < len; i++) {
            if (v2[i] !== v1[i]) {
                return false;
            }
        }

        return true;
    },

    afterRender: function () {
        var me = this,
            records;

        me.callParent();
        if (me.selectOnRender) {
            records = me.getRecordsForValue(me.value);
            if (records.length) {
                ++me.ignoreSelectChange;
                me.boundList.getSelectionModel().select(records);
                --me.ignoreSelectChange;
            }
            delete me.toSelect;
        }

        if (me.ddReorder && !me.dragGroup && !me.dropGroup) {
            me.dragGroup = me.dropGroup = 'MultiselectDD-' + Ext.id();
        }

        if (me.draggable || me.dragGroup) {
            me.dragZone = Ext.create('Ext.view.DragZone', {
                view: me.boundList,
                ddGroup: me.dragGroup,
                dragText: me.dragText
            });
        }
        if (me.droppable || me.dropGroup) {
            me.dropZone = Ext.create('Ext.view.DropZone', {
                view: me.boundList,
                ddGroup: me.dropGroup,
                handleNodeDrop: function (data, dropRecord, position) {
                    var view = this.view,
                        store = view.getStore(),
                        records = data.records,
                        index;

                    // remove the Models from the source Store
                    data.view.store.remove(records);

                    index = store.indexOf(dropRecord);
                    if (position === 'after') {
                        index++;
                    }
                    store.insert(index, records);
                    view.getSelectionModel().select(records);
                    me.fireEvent('drop', me, records);
                }
            });
        }
    },

    isValid: function () {
        var me = this,
            disabled = me.disabled,
            validate = me.forceValidation || !disabled;


        return validate ? me.validateValue(me.value) : disabled;
    },

    validateValue: function (value) {
        var me = this,
            errors = me.getErrors(value),
            isValid = Ext.isEmpty(errors);

        if (!me.preventMark) {
            if (isValid) {
                me.clearInvalid();
            } else {
                me.markInvalid(errors);
            }
        }

        return isValid;
    },

    markInvalid: function (errors) {
        // Save the message and fire the 'invalid' event
        var me = this,
            oldMsg = me.getActiveError();
        me.setActiveErrors(Ext.Array.from(errors));
        if (oldMsg !== me.getActiveError()) {
            me.updateLayout();
        }
    },

    /**
     * Clear any invalid styles/messages for this field.
     *
     * __Note:__ this method does not cause the Field's {@link #validate} or {@link #isValid} methods to return `true`
     * if the value does not _pass_ validation. So simply clearing a field's errors will not necessarily allow
     * submission of forms submitted with the {@link Ext.form.action.Submit#clientValidation} option set.
     */
    clearInvalid: function () {
        // Clear the message and fire the 'valid' event
        var me = this,
            hadError = me.hasActiveError();
        me.unsetActiveError();
        if (hadError) {
            me.updateLayout();
        }
    },

    getSubmitData: function () {
        var me = this,
            data = null,
            val;
        if (!me.disabled && me.submitValue && !me.isFileUpload()) {
            val = me.getSubmitValue();
            if (val !== null) {
                data = {};
                data[me.getName()] = val;
            }
        }
        return data;
    },

    /**
     * Returns the value that would be included in a standard form submit for this field.
     *
     * @return {String} The value to be submitted, or `null`.
     */
    getSubmitValue: function () {
        var me = this,
            delimiter = me.delimiter,
            val = me.getValue();

        return Ext.isString(delimiter) ? val.join(delimiter) : val;
    },

    getValue: function () {
        return this.value || [];
    },

    getRecordsForValue: function (value) {
        var me = this,
            records = [],
            all = me.store.getRange(),
            valueField = me.valueField,
            i = 0,
            allLen = all.length,
            rec,
            j,
            valueLen;

        for (valueLen = value.length; i < valueLen; ++i) {
            for (j = 0; j < allLen; ++j) {
                rec = all[j];
                if (rec.get(valueField) == value[i]) {
                    records.push(rec);
                }
            }
        }

        return records;
    },

    setupValue: function (value) {
        var delimiter = this.delimiter,
            valueField = this.valueField,
            i = 0,
            out,
            len,
            item;

        if (Ext.isDefined(value)) {
            if (delimiter && Ext.isString(value)) {
                value = value.split(delimiter);
            } else if (!Ext.isArray(value)) {
                value = [value];
            }

            for (len = value.length; i < len; ++i) {
                item = value[i];
                if (item && item.isModel) {
                    value[i] = item.get(valueField);
                }
            }
            out = Ext.Array.unique(value);
        } else {
            out = [];
        }
        return out;
    },

    setValue: function (value) {
        var me = this,
            selModel = me.boundList.getSelectionModel(),
            store = me.store;

        // Store not loaded yet - we cannot set the value
        if (!store.getCount()) {
            store.on({
                load: Ext.Function.bind(me.setValue, me, [value]),
                single: true
            });
            return;
        }

        value = me.setupValue(value);
        me.mixins.field.setValue.call(me, value);

        if (me.rendered) {
            ++me.ignoreSelectChange;
            selModel.deselectAll();
            selModel.select(me.getRecordsForValue(value));
            --me.ignoreSelectChange;
        } else {
            me.selectOnRender = true;
        }
    },

    clearValue: function () {
        this.setValue([]);
    },

    onEnable: function () {
        var list = this.boundList;
        this.callParent();
        if (list) {
            list.enable();
        }
    },

    onDisable: function () {
        var list = this.boundList;
        this.callParent();
        if (list) {
            list.disable();
        }
    },

    getErrors: function (value) {
        var me = this,
            format = Ext.String.format,
            errors = [],
            numSelected;

        value = Ext.Array.from(value || me.getValue());
        numSelected = value.length;

        if (!me.allowBlank && numSelected < 1) {
            errors.push(me.blankText);
        }
        if (numSelected < me.minSelections) {
            errors.push(format(me.minSelectionsText, me.minSelections));
        }
        if (numSelected > me.maxSelections) {
            errors.push(format(me.maxSelectionsText, me.maxSelections));
        }
        return errors;
    },

    onDestroy: function () {
        var me = this;

        me.bindStore(null);
        Ext.destroy(me.dragZone, me.dropZone);
        me.callParent();
    },

    onBindStore: function (store) {
        var boundList = this.boundList;

        if (boundList) {
            boundList.bindStore(store);
        }
    }

});

Ext.define('Ext.ux.form.ItemSelector', {
    extend: 'Ext.ux.form.MultiSelect',
    alias: ['widget.itemselectorfield', 'widget.itemselector'],
    alternateClassName: ['Ext.ux.ItemSelector'],
    requires: [
        'Ext.button.Button',
        'Ext.ux.form.MultiSelect'
    ],

    /**
     * @cfg {Boolean} [hideNavIcons=false] True to hide the navigation icons
     */
    hideNavIcons:false,

    /**
     * @cfg {Array} buttons Defines the set of buttons that should be displayed in between the ItemSelector
     * fields. Defaults to <tt>['top', 'up', 'add', 'remove', 'down', 'bottom']</tt>. These names are used
     * to build the button CSS class names, and to look up the button text labels in {@link #buttonsText}.
     * This can be overridden with a custom Array to change which buttons are displayed or their order.
     */
    buttons: ['top', 'up', 'add', 'remove', 'down', 'bottom'],

    /**
     * @cfg {Object} buttonsText The tooltips for the {@link #buttons}.
     * Labels for buttons.
     */
    buttonsText: {
        top: "Move to Top",
        up: "Move Up",
        add: "Add to Selected",
        remove: "Remove from Selected",
        down: "Move Down",
        bottom: "Move to Bottom"
    },

    layout: {
        type: 'hbox',
        align: 'stretch'
    },

    initComponent: function() {
        var me = this;

        me.ddGroup = me.id + '-dd';
        me.callParent();

        me.store.on('beforeload', me.cleanStore, me);
        me.store.on('load', me.bindStore, me);
    },

    createList: function(title){
        var me = this;

        return Ext.create('Ext.ux.form.MultiSelect', {
            // We don't want the multiselects themselves to act like fields,
            // so override these methods to prevent them from including
            // any of their values
            submitValue: false,
            getSubmitData: function(){
                return null;
            },
            getModelData: function(){
                return null;    
            },
            flex: 1,
            dragGroup: me.ddGroup,
            dropGroup: me.ddGroup,
            title: title,
            store: {
                model: me.store.model,
                data: []
            },
            displayField: me.displayField,
            valueField: me.valueField,
            disabled: me.disabled,
            listeners: {
                boundList: {
                    scope: me,
                    itemdblclick: me.onItemDblClick,
                    drop: me.syncValue
                }
            }
        });
    },

    setupItems: function() {
        var me = this;

        me.fromField = me.createList(me.fromTitle);
        me.toField = me.createList(me.toTitle);

        return [
            me.fromField,
            {
                xtype: 'container',
                margins: '0 4',
                layout: {
                    type: 'vbox',
                    pack: 'center'
                },
                items: me.createButtons()
            },
            me.toField
        ];
    },

    createButtons: function() {
        var me = this,
            buttons = [];

        if (!me.hideNavIcons) {
            Ext.Array.forEach(me.buttons, function(name) {
                buttons.push({
                    xtype: 'button',
                    tooltip: me.buttonsText[name],
                    handler: me['on' + Ext.String.capitalize(name) + 'BtnClick'],
                    cls: Ext.baseCSSPrefix + 'form-itemselector-btn',
                    iconCls: Ext.baseCSSPrefix + 'form-itemselector-' + name,
                    navBtn: true,
                    scope: me,
                    margin: '4 0 0 0'
                });
            });
        }
        return buttons;
    },

    getSelections: function(list) {
        var store = list.getStore();

        return Ext.Array.sort(list.getSelectionModel().getSelection(), function(a, b) {
            a = store.indexOf(a);
            b = store.indexOf(b);

            if (a < b) {
                return -1;
            } else if (a > b) {
                return 1;
            }
            return 0;
        });
    },

    onTopBtnClick : function() {
        var list = this.toField.boundList,
            store = list.getStore(),
            selected = this.getSelections(list);

        store.suspendEvents();
        store.remove(selected, true);
        store.insert(0, selected);
        store.resumeEvents();
        list.refresh();
        this.syncValue(); 
        list.getSelectionModel().select(selected);
    },

    onBottomBtnClick : function() {
        var list = this.toField.boundList,
            store = list.getStore(),
            selected = this.getSelections(list);

        store.suspendEvents();
        store.remove(selected, true);
        store.add(selected);
        store.resumeEvents();
        list.refresh();
        this.syncValue();
        list.getSelectionModel().select(selected);
    },

    onUpBtnClick : function() {
        var list = this.toField.boundList,
            store = list.getStore(),
            selected = this.getSelections(list),
            rec,
            i = 0,
            len = selected.length,
            index = 0;

        // Move each selection up by one place if possible
        store.suspendEvents();
        for (; i < len; ++i, index++) {
            rec = selected[i];
            index = Math.max(index, store.indexOf(rec) - 1);
            store.remove(rec, true);
            store.insert(index, rec);
        }
        store.resumeEvents();
        list.refresh();
        this.syncValue();
        list.getSelectionModel().select(selected);
    },

    onDownBtnClick : function() {
        var list = this.toField.boundList,
            store = list.getStore(),
            selected = this.getSelections(list),
            rec,
            i = selected.length - 1,
            index = store.getCount() - 1;

        // Move each selection down by one place if possible
        store.suspendEvents();
        for (; i > -1; --i, index--) {
            rec = selected[i];
            index = Math.min(index, store.indexOf(rec) + 1);
            store.remove(rec, true);
            store.insert(index, rec);
        }
        store.resumeEvents();
        list.refresh();
        this.syncValue();
        list.getSelectionModel().select(selected);
    },

    onAddBtnClick : function() {
        var me = this,
            selected = me.getSelections(me.fromField.boundList);

        me.moveRec(true, selected);
        me.toField.boundList.getSelectionModel().select(selected);
    },

    onRemoveBtnClick : function() {
        var me = this,
            selected = me.getSelections(me.toField.boundList);

        me.moveRec(false, selected);
        me.fromField.boundList.getSelectionModel().select(selected);
    },

    moveRec: function(add, recs) {
        var me = this,
            fromField = me.fromField,
            toField   = me.toField,
            fromStore = add ? fromField.store : toField.store,
            toStore   = add ? toField.store   : fromField.store;

        fromStore.suspendEvents();
        toStore.suspendEvents();
        fromStore.remove(recs);
        toStore.add(recs);
        fromStore.resumeEvents();
        toStore.resumeEvents();

        fromField.boundList.refresh();
        toField.boundList.refresh();

        me.syncValue();
    },

    syncValue: function() {
        var me = this; 
        me.mixins.field.setValue.call(me, me.setupValue(me.toField.store.getRange()));
    },

    onItemDblClick: function(view, rec) {
        this.moveRec(view === this.fromField.boundList, rec);
    },

    setValue: function(value) {
        var me = this,
            fromField = me.fromField,
            toField = me.toField,
            fromStore = fromField.store,
            toStore = toField.store,
            selected;

        // Wait for from store to be loaded
        if (!me.fromStorePopulated) {
            me.fromField.store.on({
                load: Ext.Function.bind(me.setValue, me, [value]),
                single: true
            });
            return;
        }

        value = me.setupValue(value);
        me.mixins.field.setValue.call(me, value);

        selected = me.getRecordsForValue(value);

        // Clear both left and right Stores.
        // Both stores must not fire events during this process.
        fromStore.suspendEvents();
        toStore.suspendEvents();
        fromStore.removeAll();
        toStore.removeAll();

        // Reset fromStore
        me.populateFromStore(me.store);

        // Copy selection across to toStore
        Ext.Array.forEach(selected, function(rec){
            // In the from store, move it over
            if (fromStore.indexOf(rec) > -1) {
                fromStore.remove(rec);
            }
            toStore.add(rec);
        });

        // Stores may now fire events
        fromStore.resumeEvents();
        toStore.resumeEvents();

        // Refresh both sides and then update the app layout
        Ext.suspendLayouts();
        fromField.boundList.refresh();
        toField.boundList.refresh();
        Ext.resumeLayouts(true);        
    },

    onBindStore: function(store, initial) {
        var me = this;

        if (me.fromField) {
            me.cleanStore();
            me.populateFromStore(store);
        }
    },

    populateFromStore: function(store) {
        var fromStore = this.fromField.store;

        // Flag set when the fromStore has been loaded
        this.fromStorePopulated = true;

        fromStore.add(store.getRange());

        // setValue waits for the from Store to be loaded
        fromStore.fireEvent('load', fromStore);
    },

    cleanStore: function () {
        var me = this;

        if (me.fromField) {
            me.fromField.store.removeAll()
            me.toField.store.removeAll();
            me.fromStorePopulated = false;
        }
    },

    onEnable: function(){
        var me = this;

        me.callParent();
        me.fromField.enable();
        me.toField.enable();

        Ext.Array.forEach(me.query('[navBtn]'), function(btn){
            btn.enable();
        });
    },

    onDisable: function(){
        var me = this;

        me.callParent();
        me.fromField.disable();
        me.toField.disable();

        Ext.Array.forEach(me.query('[navBtn]'), function(btn){
            btn.disable();
        });
    },

    onDestroy: function(){
        this.bindStore(null);
        this.callParent();
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
    },
    onExpand: function () {
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
    * @cfg {Number} pickerWidth
    * The width of the tree dropdown. Defaults to 215.
    */
    pickerWidth: 215,

    /**
    * @cfg {Boolean} matchFieldWidth
    * Whether the picker dropdown's width should be explicitly set to match the width of the field. Defaults to true.
    */
    matchFieldWidth: false,

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
            width: me.pickerWidth,
            minWidth: me.pickerWidth,
            shadow: false,
            resizable: {
                transparent: true
            },
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