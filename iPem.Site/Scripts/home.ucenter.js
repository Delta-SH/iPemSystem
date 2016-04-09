(function () {
    Ext.apply(Ext.form.VTypes, {
        password: function (val, field) {
            if (field.confirmTo) {
                var confirm = field.ownerCt.getComponent(field.confirmTo);
                if (!Ext.isEmpty(confirm))
                    return (val == confirm.getValue());
            }
            return true;
        }
    });

    var baseInfo = Ext.create('Ext.tab.Panel', {
        border: true,
        flex: 1,
        cls: 'x-custom-panel',
        plain: true,
        loader: {
            url: '../Account/GetCurrentUser',
            autoLoad: false,
            loadMask: true,
            renderer: function (loader,response,request) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    var bil = Ext.getCmp('base-info-left'),
                        bir = Ext.getCmp('base-info-right'),
                        cfm = Ext.getCmp('changeForm');

                    var result = data.data;
                    if (!Ext.isEmpty(result) && Ext.isObject(result)) {

                        bil.getComponent('uid').setValue(result.uid);
                        bil.getComponent('empName').setValue(result.empName);
                        bil.getComponent('sexName').setValue(result.sexName);
                        bil.getComponent('email').setValue(result.email);
                        bil.getComponent('limited').setValue(result.limited);
                        bil.getComponent('lastLockedout').setValue(result.lastLockedout);
                        bil.getComponent('comment').setValue(result.comment);

                        bir.getComponent('roleName').setValue(result.roleName);
                        bir.getComponent('empNo').setValue(result.empNo);
                        bir.getComponent('mobile').setValue(result.mobile);
                        bir.getComponent('created').setValue(result.created);
                        bir.getComponent('isLockedOut').setValue(result.isLockedOut ? window.$$iPems.lang.StatusLocked : window.$$iPems.lang.StatusUnlocked);
                        bir.getComponent('lastPasswordChanged').setValue(result.lastPasswordChanged);

                        cfm.getComponent('id').setValue(result.id);
                        cfm.getComponent('uid').setValue(result.uid);
                    }
                }
            }
        },
        items: [{
            title: $$iPems.lang.UCenter.BaseTitle,
            glyph: 0xf012,
            layout: 'hbox',
            items: [{
                xtype: 'container',
                id:'base-info-left',
                flex: 1,
                layout: 'anchor',
                defaults: {
                    anchor: '90%',
                    labelWidth: 100,
                    labelStyle: 'font-weight:bold',
                    margin: '20 25 20 25'
                },
                items: [
                    {
                        xtype: 'displayfield',
                        itemId: 'uid',
                        fieldLabel: $$iPems.lang.User.Name
                    }, {
                        xtype: 'displayfield',
                        itemId: 'empName',
                        fieldLabel: $$iPems.lang.User.EmpName
                    }, {
                        xtype: 'displayfield',
                        itemId: 'sexName',
                        fieldLabel: $$iPems.lang.User.Sex
                    }, {
                        xtype: 'displayfield',
                        itemId: 'email',
                        fieldLabel: $$iPems.lang.User.Email
                    }, {
                        xtype: 'displayfield',
                        itemId: 'limited',
                        fieldLabel: $$iPems.lang.User.Limited
                    }, {
                        xtype: 'displayfield',
                        itemId: 'lastLockedout',
                        fieldLabel: $$iPems.lang.User.LastLockedout
                    }, {
                        xtype: 'displayfield',
                        itemId: 'comment',
                        fieldLabel: $$iPems.lang.User.Comment
                    }
                ]
            }, {
                xtype: 'container',
                id: 'base-info-right',
                flex: 1,
                layout: 'anchor',
                defaults: {
                    anchor: '90%',
                    labelWidth: 100,
                    labelStyle: 'font-weight:bold',
                    margin: '20 25 20 25'
                },
                items: [
                    {
                        xtype: 'displayfield',
                        itemId: 'roleName',
                        fieldLabel: $$iPems.lang.User.Role
                    }, {
                        xtype: 'displayfield',
                        itemId: 'empNo',
                        fieldLabel: $$iPems.lang.User.EmpNo
                    }, {
                        xtype: 'displayfield',
                        itemId: 'mobile',
                        fieldLabel: $$iPems.lang.User.Mobile
                    }, {
                        xtype: 'displayfield',
                        itemId: 'created',
                        fieldLabel: $$iPems.lang.User.Created
                    }, {
                        xtype: 'displayfield',
                        itemId: 'isLockedOut',
                        fieldLabel: $$iPems.lang.User.IsLockedOut
                    }, {
                        xtype: 'displayfield',
                        itemId: 'lastPasswordChanged',
                        fieldLabel: $$iPems.lang.User.LastPasswordChanged
                    },
                ]
            }]
        }, {
            title: $$iPems.lang.UCenter.ChangeTitle,
            glyph: 0xf022,
            items: [{
                xtype: 'form',
                id: 'changeForm',
                border: false,
                defaultType: 'textfield',
                fieldDefaults: {
                    labelWidth: 60,
                    labelAlign: 'left',
                    labelStyle: 'font-weight:bold',
                    margin: '25 25 25 25',
                    width: 360
                },
                items: [
                    {
                        itemId: 'id',
                        xtype: 'hiddenfield',
                        value: ''
                    },
                    {
                        itemId: 'uid',
                        xtype: 'textfield',
                        readOnly: true,
                        fieldLabel: $$iPems.lang.User.Name,
                        allowBlank: false
                    },
                    {
                        itemId: 'origin',
                        xtype: 'textfield',
                        inputType: 'password',
                        fieldLabel: $$iPems.lang.UCenter.OriginPassword,
                        allowBlank: false
                    },
                    {
                        itemId: 'password',
                        xtype: 'textfield',
                        inputType: 'password',
                        fieldLabel: $$iPems.lang.User.Password,
                        allowBlank: false
                    },
                    {
                        itemId: 'confirmPassword',
                        xtype: 'textfield',
                        inputType: 'password',
                        fieldLabel: $$iPems.lang.User.Confirm,
                        allowBlank: false,
                        vtype: 'password',
                        vtypeText: $$iPems.lang.User.ConfirmError,
                        confirmTo: 'password'
                    }
                ]
            }],
            buttonAlign:'left',
            buttons: [
                {
                    xtype: 'button',
                    text: $$iPems.lang.UCenter.SavePassword,
                    scale: 'medium',
                    handler: function () {
                        var form = Ext.getCmp('changeForm'),
                            baseForm = form.getForm(),
                            id = form.getComponent('id').getValue(),
                            result = Ext.getCmp('changeResult');

                        result.setTextWithIcon('', '');
                        if (baseForm.isValid() && !Ext.isEmpty(id)) {
                            Ext.Msg.confirm(window.$$iPems.lang.ConfirmWndTitle, window.$$iPems.lang.ConfirmChangePassword, function (buttonId, text) {
                                if (buttonId === 'yes') {
                                    result.setTextWithIcon(window.$$iPems.lang.AjaxHandling, 'x-icon-loading');
                                    var origin = form.getComponent('origin').getValue();
                                    var password = form.getComponent('password').getValue();
                                    baseForm.submit({
                                        submitEmptyText: false,
                                        clientValidation: true,
                                        preventWindow: true,
                                        url: '../Account/ChangePassword',
                                        params: {
                                            id: id,
                                            origin: origin,
                                            password: password
                                        },
                                        success: function (form, action) {
                                            result.setTextWithIcon(action.result.message, 'x-icon-accept');
                                        },
                                        failure: function (form, action) {
                                            var message = 'undefined error.';
                                            if (!Ext.isEmpty(action.result) && !Ext.isEmpty(action.result.message))
                                                message = action.result.message;

                                            result.setTextWithIcon(message, 'x-icon-error');
                                        }
                                    });
                                }
                            });
                        } else {
                            result.setTextWithIcon(window.$$iPems.lang.FormError, 'x-icon-error');
                        }
                    }
                },
                { xtype: 'tbfill' },
                { id: 'changeResult', xtype: 'iconlabel', text: '' }
            ]
        }]
    });

    var logout = Ext.create('Ext.panel.Panel', {
        title: $$iPems.lang.UCenter.LogoutTitle,
        collapsible: true,
        cls: 'x-danger-panel',
        margin: '10 0 0 0',
        glyph: 0xf026,
        border: true,
        bodyPadding: 10,
        items: [
            {
                xtype: 'label',
                contentEl: 'logout-tips'
            },
            {
                xtype: 'button',
                text: $$iPems.lang.Logout,
                scale: 'medium',
                href: '/Account/LogOut',
                hrefTarget:'_self'
            }
        ]
    });

    var hcontent = Ext.create('Ext.panel.Panel', {
        layout: {
            type: 'vbox',
            align: 'stretch',
            pack: 'start',
        },
        region: 'center',
        border: false,
        items: [baseInfo, logout]
    });

    Ext.onReady(function () {
        /*add components to viewport panel*/
        var pageContentPanel = Ext.getCmp('center-content-panel-fw');
        if (!Ext.isEmpty(pageContentPanel)) {
            pageContentPanel.add(hcontent);

            //load data
            baseInfo.getLoader().load();
        }
    });
})();