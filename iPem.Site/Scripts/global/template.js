/* ========================================================================
 * Global: template.js
 * /Scripts/global/template.js
 * ========================================================================
 */

Ext.onReady(function () {
    Ext.tip.QuickTipManager.init();

    //start tasks
    $$iPems.Tasks.noticeTask.start();
    $$iPems.Tasks.actAlmNoticeTask.start();

    /*home page*/
    var _titlebar = Ext.create('Ext.panel.Panel', {
        border: false,
        contentEl: 'title-bar'
    });

    var _pagebody = Ext.create('Ext.panel.Panel', {
        border: false,
        contentEl: 'page-body-content'
    });

    var _north = Ext.create('Ext.panel.Panel', {
        region: 'north',
        border: false,
        items: [_titlebar, _pagebody]
    });

    var viewport = Ext.create('Ext.container.Viewport', {
        id: 'main-viewport',
        layout: 'border',
        items: [
                {
                    id: 'top-nav-panel-fw',
                    region: 'north',
                    contentEl: 'top-nav-panel',
                    height: 51,
                    border: false
                },
                {
                    id: 'left-nav-panel-fw',
                    region: 'west',
                    title: '我的菜单',
                    xtype: 'treepanel',
                    glyph: 0xf011,
                    width: 220,
                    split: true,
                    collapsible: true,
                    collapsed: false,
                    autoScroll: true,
                    useArrows: false,
                    rootVisible: true,
                    hidden: !$$iPems.menuVisible,
                    margins: '5 0 5 5',
                    contentEl: 'left-nav-panel',
                    bodyCls: 'left-nav',
                    store: new Ext.data.TreeStore({
                        autoLoad: false,
                        root: {
                            id: -10078,
                            text: '系统主页',
                            href: '/Home',
                            icon: $$iPems.icons.Home,
                            root: true
                        },
                        proxy: {
                            type: 'ajax',
                            url: '/Home/GetNavMenus',
                            reader: {
                                type: 'json',
                                successProperty: 'success',
                                messageProperty: 'message',
                                totalProperty: 'total',
                                root: 'data'
                            }
                        }
                    }),
                    listeners: {
                        render: function (el) {
                            el.getStore().load();
                        },
                        load: function (el, node, records, successful) {
                            if (successful) {
                                var me = this,
                                    record = el.getNodeById($$iPems.menuId);

                                if (!Ext.isEmpty(record)) {
                                    me.selectPath(record.getPath());
                                } else {
                                    me.getRootNode().expand(false);
                                }
                            }
                        }
                    }
                },
                {
                    id: 'center-content-panel-fw',
                    region: 'center',
                    layout: 'border',
                    contentEl: 'center-content-panel',
                    border: false,
                    items: [_north],
                    padding: $$iPems.menuVisible ? '5 5 5 0' : 5
                }, {
                    id: 'bottom-speech-panel-fw',
                    region: 'south',
                    height: 0,
                    border: false,
                    layout: {
                        type: 'vbox',
                        align: 'stretch'
                    },
                    items: [
                        Ext.create('Ext.ux.IFrame', {
                            flex: 1,
                            loadMask: '正在处理...',
                            src: '/Home/Speech'
                        })
                    ]
                }
        ]
    });

    /*title tip*/
    var help = Ext.get('help-btn');
    if (!Ext.isEmpty(help)) {
        help.on('click', function (el, e) {
            var arrow = Ext.get('tip-arrow');
            if (Ext.isEmpty(arrow)) {
                return false;
            }

            var tip = Ext.get('help-tip');
            if (Ext.isEmpty(tip)) {
                return false;
            }

            tip.setDisplayed(!tip.isVisible());
            arrow.anchorTo(help, 'tc-bc', [-2, 1]);
            viewport.doLayout();
        });
    }

    /*show page content*/
    Ext.getBody().setDisplayed(true);
});