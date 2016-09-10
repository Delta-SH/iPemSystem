
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
        return this.rendered ? encode === true ? Ext.util.Format.htmlEncode(this.textEl.dom.innerHTML) : this.textEl.dom.innerHTML : this.text;
    },

    getDisplayText: function (text, encode) {
        var t = text || this.text || this.html || "",
            x = encode !== false ? Ext.util.Format.htmlEncode(t) : t;

        return (Ext.isEmpty(t) && !Ext.isEmpty(this.emptyText)) ? this.emptyText : !Ext.isEmpty(this.format) ? Ext.net.StringUtils.format(this.format, x) : x
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

    // Appends the specified string to the label's innerHTML.
    // Options:
    //      text - a string to append.
    //      (optional) appendLine - appends a new line if true. Defaults to false.
    append: function (text, appendLine) {
        this.setText([this.getText(), text, appendLine === true ? "<br/>" : ""].join(""), false);
    },

    // Appends the specified string and a new line to the label's innerHTML.
    // Options:
    //      text - a string to append.
    appendLine: function (text) {
        this.append(text, true);
    }
});
