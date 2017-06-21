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
            url: '/Account/GetCurrentUser',
            autoLoad: false,
            loadMask: true,
            renderer: function (loader,response,request) {
                var data = Ext.decode(response.responseText, true);
                if (data.success) {
                    var bil = Ext.getCmp('base-info-left'),
                        bir = Ext.getCmp('base-info-right'),
                        cfm = Ext.getCmp('changePassword');

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
                        bir.getComponent('isLockedOut').setValue(result.isLockedOut ? '锁定' : '正常');
                        bir.getComponent('lastPasswordChanged').setValue(result.lastPasswordChanged);

                        cfm.getComponent('id').setValue(result.id);
                        cfm.getComponent('uid').setValue(result.uid);
                    }
                }
            }
        },
        items: [{
            title: '基本信息',
            glyph: 0xf012,
            layout: 'hbox',
            items: [{
                xtype: 'container',
                width: 100,
                items: [{
                    xtype: 'image',
                    height: 60,
                    width: 60,
                    margin: '20 0 0 28',
                    src: '/Account/GetUserPhoto',
                    style: {
                        'border-radius': '60px',
                    }
                }]
            },{
                xtype: 'container',
                id:'base-info-left',
                flex: 1,
                layout: 'anchor',
                defaults: {
                    anchor: '100%',
                    labelWidth: 80,
                    margin: 20
                },
                items: [
                    {
                        xtype: 'displayfield',
                        itemId: 'uid',
                        fieldLabel: '登录用户'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'empName',
                        fieldLabel: '隶属员工'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'sexName',
                        fieldLabel: '性&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;别'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'email',
                        fieldLabel: 'Email'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'limited',
                        fieldLabel: '有效日期'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'lastLockedout',
                        fieldLabel: '锁定日期'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'comment',
                        fieldLabel: '备注信息'
                    }
                ]
            }, {
                xtype: 'container',
                id: 'base-info-right',
                flex: 1,
                layout: 'anchor',
                defaults: {
                    anchor: '100%',
                    labelWidth: 80,
                    margin: '20 25 20 25'
                },
                items: [
                    {
                        xtype: 'displayfield',
                        itemId: 'roleName',
                        fieldLabel: '隶属角色'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'empNo',
                        fieldLabel: '员工工号'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'mobile',
                        fieldLabel: '联系电话'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'created',
                        fieldLabel: '创建日期'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'isLockedOut',
                        fieldLabel: '锁定状态'
                    }, {
                        xtype: 'displayfield',
                        itemId: 'lastPasswordChanged',
                        fieldLabel: '更新密码'
                    },
                ]
            }]
        }, {
            title: '修改密码',
            glyph: 0xf022,
            items: [{
                id: 'changePassword',
                xtype: 'form',
                border: false,
                defaultType: 'textfield',
                fieldDefaults: {
                    labelWidth: 60,
                    labelAlign: 'left',
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
                        fieldLabel: '登录用户',
                        allowBlank: false
                    },
                    {
                        itemId: 'origin',
                        xtype: 'textfield',
                        inputType: 'password',
                        fieldLabel: '原始密码',
                        allowBlank: false
                    },
                    {
                        itemId: 'password',
                        xtype: 'textfield',
                        inputType: 'password',
                        fieldLabel: '新的密码',
                        allowBlank: false
                    },
                    {
                        itemId: 'confirmPassword',
                        xtype: 'textfield',
                        inputType: 'password',
                        fieldLabel: '确认密码',
                        allowBlank: false,
                        vtype: 'password',
                        vtypeText: '密码不一致',
                        confirmTo: 'password'
                    }
                ]
            }],
            buttonAlign:'left',
            buttons: [
                {
                    xtype: 'button',
                    text: '保存密码',
                    cls: 'custom-button custom-success',
                    handler: function () {
                        var form = Ext.getCmp('changePassword'),
                            basic = form.getForm(),
                            id = form.getComponent('id').getValue(),
                            result = Ext.getCmp('changeResult');

                        result.setTextWithIcon('', '');
                        if (basic.isValid() && !Ext.isEmpty(id)) {
                            Ext.Msg.confirm('确认对话框', '您确认要修改密码吗？', function (buttonId, text) {
                                if (buttonId === 'yes') {
                                    result.setTextWithIcon('正在处理...', 'x-icon-loading');
                                    var origin = form.getComponent('origin').getValue();
                                    var password = form.getComponent('password').getValue();
                                    basic.submit({
                                        submitEmptyText: false,
                                        clientValidation: true,
                                        preventWindow: true,
                                        url: '/Account/ChangePassword',
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
                            result.setTextWithIcon('表单填写错误', 'x-icon-error');
                        }
                    }
                },
                { id: 'changeResult', xtype: 'iconlabel', text: '' }
            ]
        }]
    });

    var logout = Ext.create('Ext.panel.Panel', {
        title: '登出系统',
        collapsible: true,
        cls: 'x-danger-panel',
        margin: '10 0 0 0',
        glyph: 0xf026,
        border: true,
        bodyPadding: 10,
        items: [{
            xtype: 'container',
            contentEl: 'logout-content'
        }]
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