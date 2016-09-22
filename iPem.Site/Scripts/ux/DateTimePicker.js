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