/* ========================================================================
 * Components: ImageExporterComponent.js
 * /Scripts/components/ImageExporterComponent.js
 * ========================================================================
 */

Ext.define('Ext.ux.ImageExporter', {

    singleton: true,

    defaultUrl: '/Component/SaveCharts',

    formCls: Ext.baseCSSPrefix + 'hide-display',

    save: function (charts) {
        if (!Ext.isArray(charts))
            charts = [charts];

        var svgs = [];
        Ext.Array.each(charts, function (chart, index) {
            svgs.push(Ext.String.htmlEncode(Ext.draw.engine.SvgExporter.generate(chart.surface)));
        });

        $$iPems.download({
            url: this.defaultUrl,
            params: { svgs: svgs }
        });
    }
});