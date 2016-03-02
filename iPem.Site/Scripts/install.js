(function ($) {
    /**
     * The key for cookies.
     *
     * @final
     * @private
     * @property _cookie_ipems_install_config
     * @type String
     **/
    var _cookie_ipems_install_config = 'ipems_install_config';

    /**
     * The key for cookies.
     *
     * @final
     * @private
     * @property _cookie_ipems_install_status
     * @type String
     **/
    var _cookie_ipems_install_status = 'ipems_install_status';

    /**
     * The plugin prefix for cookies.
     *
     * @final
     * @private
     * @property _cookiePrefix
     * @type String
     **/
    var _cookiePrefix = "jQu3ry_5teps_St@te_wizard-panel";

    /**
     * The error message for cookie.
     *
     * @final
     * @private
     * @property _cookie_error_message
     * @type String
     **/
    var _cookie_error_message = '<strong>系统错误:</strong> 您的浏览器禁用了Cookie，启用后才能使用本向导。';

    /**
     * The error message for cookie.
     *
     * @final
     * @private
     * @property _cookie_not_existing_message
     * @type String
     **/
    var _cookie_not_existing_message = '<strong>系统错误:</strong> 无法获取Cookie对象。';

    /**
     * The error message for cookie.
     *
     * @final
     * @private
     * @property _cookie_invalid_message
     * @type String
     **/
    var _cookie_invalid_message = '<strong>系统错误:</strong> Cookie对象格式错误。';

    /**
     * The error message for ipv4.
     *
     * @final
     * @private
     * @property _ipv4_error_message
     * @type String
     **/
    var _ipv4_error_message = 'IPv4地址格式错误';

    /**
     * The error message for ajax.
     *
     * @final
     * @private
     * @property _ajax_error_message
     * @type String
     **/
    var _ajax_error_message = '<strong>系统错误:</strong> ajax请求时发生错误';

    /**
     * The error message for ajax.
     *
     * @final
     * @private
     * @property _ajax_data_error_message
     * @type String
     **/
    var _ajax_data_error_message = '<strong>系统错误:</strong> ajax返回数据格式错误';

    /**
     * The success code message for ajax.
     *
     * @final
     * @private
     * @property _ajax_return_ok_code
     * @type String
     **/
    var _ajax_return_ok_code = 200;

    /**
     * Contains all labels. 
     *
     * @property labels
     * @type Object
     * @for defaults
     **/
    var _steps_labels = {
        finish: '完&nbsp;&nbsp;&nbsp;成',
        next: '下一步',
        previous: '上一步'
    };

    /**
     * Contains all databases type. 
     *
     * @property databases
     * @type Object
     * @for defaults
     **/
    var _databases_types = {
        master: 0,
        history: 1,
        resource: 2
    };

    /**
     * Contains all confirm message. 
     *
     * @property message
     * @type Object
     * @for defaults
     **/
    var _confirm_messages = {
        install: '全新安装系统',
        update: '升级原有系统',
        created: '创建全新数据库',
        existed: '使用现有数据库'
    };

    /**
     * Contains all install status. 
     *
     * @property status
     * @type Object
     * @for defaults
     **/
    var _install_status_messages = {
        waiting: { id: 0, text: '等待' },
        install: { id: 1, text: '执行' },
        success: { id: 2, text: '完成' },
        failure: { id: 3, text: '失败' },
        cancel: { id: 4, text: '取消' }
    };

    /**
     * Contains all steps index. 
     *
     * @property steps index
     * @type Object
     * @for defaults
     **/
    var _install_steps_index = {
        master: { index: 0, status: _install_status_messages.waiting.id },
        history: { index: 1, status: _install_status_messages.waiting.id },
        resource: { index: 2, status: _install_status_messages.waiting.id },
        role: { index: 3, status: _install_status_messages.waiting.id },
        user: { index: 4, status: _install_status_messages.waiting.id },
        finish: { index: 5, status: _install_status_messages.waiting.id },
        success: { index: 6, status: _install_status_messages.waiting.id },
        failure: { index: 7, status: _install_status_messages.waiting.id }
    };

    /**
     * The wizard steps object.
     *
     * @final
     * @private
     * @property _wizard_steps
     * @type String
     **/
    var _wizard_steps = null;

    /**
     * The wizard steps panel.
     *
     * @final
     * @private
     * @property _wizard_steps_panel
     * @type String
     **/
    var _wizard_steps_panel = null;

    /** config demo
        var ipems_install_config = {
            type: 0,
            database:[
                {id:0, ipv4:'192.168.1.1', port:1433, uid:'sa', pwd:'123', crdnew:0, name:'csc', path:'d:\\database\\', oname:'', checked:false},
                {id:1, ipv4:'192.168.1.1', port:1433, uid:'sa', pwd:'123', crdnew:0, name:'csc', path:'d:\\database\\', oname:'', checked:false},
                {id:2, ipv4:'192.168.1.1', port:1433, uid:'sa', pwd:'123', crdnew:0, name:'csc', path:'d:\\database\\', oname:'', checked:false}
            ],
            account:{role: {name:'Administrator', code:'ADMIN', comment:''}, user:{name:'system', pwd:'system', comment:'' } }
        };
    **/

    $.ajaxSetup({ type: "POST", cache: false, timeout: 60000 });

    function initPanel() {
        if (_wizard_steps_panel.length == 0)
            return;

        if (_wizard_steps.length == 0)
            return;

        switch (_wizard_steps.steps('getCurrentIndex')) {
            case 0:
                getStepCookie_0();
                break;
            case 1:
                getStepCookie_1();
                break;
            case 2:
                getStepCookie_2();
                break;
            case 3:
                getStepCookie_3();
                break;
            case 4:
                showPreviousButton(false);
                getStepCookie_4();
                break;
            default:
                break;
        }
    }

    function getStepCookie_0() {
        if ($.cookie) {
            $.cookie.json = true;
            var config = $.cookie(_cookie_ipems_install_config);
            if (typeof config !== 'undefined' && typeof config === 'object') {
                $('input:radio[name*="welcome_iCheck"]').eq(config.type).iCheck('check');
            }
        }
    }

    function setStepCookie_0() {
        var wchecked = parseInt($('input:radio[name*="welcome_iCheck"]:checked').val(), 0);

        if ($.cookie) {
            $.cookie.json = true;
            var config = $.cookie(_cookie_ipems_install_config);
            if (typeof config !== 'undefined' && typeof config === 'object') {
                config.type = wchecked;
            } else {
                var config = {
                    type: wchecked,
                    database: null,
                    account: null
                };
            }

            $.cookie.json = true;
            $.cookie(_cookie_ipems_install_config, config);
            return true;
        }

        return false;
    }

    function getStepCookie_1() {
        if ($.cookie) {
            $.cookie.json = true;
            var config = $.cookie(_cookie_ipems_install_config);
            if (typeof config !== 'undefined'
                && typeof config === 'object'
                && config.database != null) {
                $.each(config.database, function (index, item) {
                    switch (item.id) {
                        case _databases_types.master:
                            $('#masterServerName').val(item.ipv4);
                            $('#masterPort').val(item.port);
                            $('#masterUid').val(item.uid);
                            $('#masterPwd').val(item.pwd);
                            $('input:radio[name*="Master"]').eq(item.crdnew).iCheck('check');
                            $('#masterNewDatabaseName').val(item.name);
                            $('#masterNewDatabasePath').val(item.path);
                            $('#masterExistingDatabaseName').val(item.oname);
                            $('#masterDontCheckDatabase').iCheck(item.checked ? 'check' : 'uncheck');
                            break;
                        case _databases_types.history:
                            $('#hisServerName').val(item.ipv4);
                            $('#hisPort').val(item.port);
                            $('#hisUid').val(item.uid);
                            $('#hisPwd').val(item.pwd);
                            $('input:radio[name*="History"]').eq(item.crdnew).iCheck('check');
                            $('#hisNewDatabaseName').val(item.name);
                            $('#hisNewDatabasePath').val(item.path);
                            $('#hisExistingDatabaseName').val(item.oname);
                            $('#hisDontCheckDatabase').iCheck(item.checked ? 'check' : 'uncheck');
                            break;
                        case _databases_types.resource:
                            $('#resServerName').val(item.ipv4);
                            $('#resPort').val(item.port);
                            $('#resUid').val(item.uid);
                            $('#resPwd').val(item.pwd);
                            $('input:radio[name*="Resource"]').eq(item.crdnew).iCheck('check');
                            $('#resNewDatabaseName').val(item.name);
                            $('#resNewDatabasePath').val(item.path);
                            $('#resExistingDatabaseName').val(item.oname);
                            $('#resDontCheckDatabase').iCheck(item.checked ? 'check' : 'uncheck');
                            break;
                        default:
                            break;
                    }
                });
            }
        }
    }

    function setStepCookie_1() {
        var isValid = $('#insform').valid();
        if (isValid && $.cookie) {
            var databases = [
                {
                    id: _databases_types.master,
                    ipv4: $('#masterServerName').val(),
                    port: parseInt($('#masterPort').val(), 0),
                    uid: $('#masterUid').val(),
                    pwd: $('#masterPwd').val(),
                    crdnew: parseInt($('input:radio[name*="Master"]:checked').val(), 0),
                    name: $('#masterNewDatabaseName').val(),
                    path: $('#masterNewDatabasePath').val(),
                    oname: $('#masterExistingDatabaseName').val(),
                    checked: $('#masterDontCheckDatabase').is(':checked')
                },
                {
                    id: _databases_types.history,
                    ipv4: $('#hisServerName').val(),
                    port: parseInt($('#hisPort').val(), 0),
                    uid: $('#hisUid').val(),
                    pwd: $('#hisPwd').val(),
                    crdnew: parseInt($('input:radio[name*="History"]:checked').val(), 0),
                    name: $('#hisNewDatabaseName').val(),
                    path: $('#hisNewDatabasePath').val(),
                    oname: $('#hisExistingDatabaseName').val(),
                    checked: $('#hisDontCheckDatabase').is(':checked')
                },
                {
                    id: _databases_types.resource,
                    ipv4: $('#resServerName').val(),
                    port: parseInt($('#resPort').val(), 0),
                    uid: $('#resUid').val(),
                    pwd: $('#resPwd').val(),
                    crdnew: parseInt($('input:radio[name*="Resource"]:checked').val(), 0),
                    name: $('#resNewDatabaseName').val(),
                    path: $('#resNewDatabasePath').val(),
                    oname: $('#resExistingDatabaseName').val(),
                    checked: $('#resDontCheckDatabase').is(':checked')
                }
            ];

            $.cookie.json = true;
            var config = $.cookie(_cookie_ipems_install_config);
            if (typeof config !== 'undefined' && typeof config === 'object') {
                config.database = databases;
            } else {
                var config = {
                    type: 0,
                    database: databases,
                    account: null
                };
            }

            $.cookie.json = true;
            $.cookie(_cookie_ipems_install_config, config);
            return true;
        }

        return false;
    }

    function getStepCookie_2() {
        if ($.cookie) {
            $.cookie.json = true;
            var config = $.cookie(_cookie_ipems_install_config);
            if (typeof config !== 'undefined'
                && typeof config === 'object'
                && config.account != null) {
                $('#sysRoleName').val(config.account.role.name);
                $('#sysRoleCode').val(config.account.role.code);
                $('#sysRoleComment').val(config.account.role.comment);

                $('#sysUserName').val(config.account.user.name);
                $('#sysUserPwd').val(config.account.user.pwd);
                $('#sysUserComment').val(config.account.user.comment);
            }
        }
    }

    function setStepCookie_2() {
        var isValid = $('#insform').valid();
        if (isValid && $.cookie) {
            var accounts = {
                role:{
                    name: $('#sysRoleName').val(),
                    code: $('#sysRoleCode').val(),
                    comment: $('#sysRoleComment').val()
                },
                user: {
                    name: $('#sysUserName').val(),
                    pwd: $('#sysUserPwd').val(),
                    comment: $('#sysUserComment').val()
                }
            };

            $.cookie.json = true;
            var config = $.cookie(_cookie_ipems_install_config);
            if (typeof config !== 'undefined' && typeof config === 'object') {
                config.account = accounts;
            } else {
                var config = {
                    type: 0,
                    database: null,
                    account: accounts
                };
            }

            $.cookie.json = true;
            $.cookie(_cookie_ipems_install_config, config);
            return true;
        }

        return false;
    }

    function getStepCookie_3() {
        if ($.cookie) {
            $.cookie.json = true;
            var config = $.cookie(_cookie_ipems_install_config);
            if (typeof config !== 'undefined' && typeof config === 'object') {
                if (config.type != null) {
                    $('#cf-wc-01').html(config.type === 0 ? _confirm_messages.install : _confirm_messages.update);
                }

                if (config.database != null) {
                    $.each(config.database, function (index, item) {
                        switch (item.id) {
                            case _databases_types.master:
                                $('#cf-cs-01').html(item.ipv4);
                                $('#cf-cs-02').html(item.port);
                                $('#cf-cs-03').html(item.uid);
                                $('#cf-cs-04').html(item.pwd);
                                $('#cf-cs-05').html(item.crdnew === 0 ? _confirm_messages.created : _confirm_messages.existed);
                                $('#cf-cs-06').html(item.crdnew === 0 ? item.name : item.oname);
                                $('#cf-cs-07').html(item.crdnew === 0 ? item.path : 'undefined');
                                break;
                            case _databases_types.history:
                                $('#cf-hs-01').html(item.ipv4);
                                $('#cf-hs-02').html(item.port);
                                $('#cf-hs-03').html(item.uid);
                                $('#cf-hs-04').html(item.pwd);
                                $('#cf-hs-05').html(item.crdnew === 0 ? _confirm_messages.created : _confirm_messages.existed);
                                $('#cf-hs-06').html(item.crdnew === 0 ? item.name : item.oname);
                                $('#cf-hs-07').html(item.crdnew === 0 ? item.path : 'undefined');
                                break;
                            case _databases_types.resource:
                                $('#cf-rs-01').html(item.ipv4);
                                $('#cf-rs-02').html(item.port);
                                $('#cf-rs-03').html(item.uid);
                                $('#cf-rs-04').html(item.pwd);
                                $('#cf-rs-05').html(item.crdnew === 0 ? _confirm_messages.created : _confirm_messages.existed);
                                $('#cf-rs-06').html(item.crdnew === 0 ? item.name : item.oname);
                                $('#cf-rs-07').html(item.crdnew === 0 ? item.path : 'undefined');
                                break;
                            default:
                                break;
                        }
                    });
                }

                if (config.account != null) {
                    $('#cf-rl-01').html(config.account.role.name);
                    $('#cf-rl-02').html(config.account.role.code);
                    $('#cf-rl-03').html(config.account.role.comment);

                    $('#cf-ue-01').html(config.account.user.name);
                    $('#cf-ue-02').html(config.account.user.pwd);
                    $('#cf-ue-03').html(config.account.user.comment);
                }
            }
        }
    }

    function getStepCookie_4() {
        if ($.cookie) {
            var status = parseInt($.cookie(_cookie_ipems_install_status), 0);
            if (!isNaN(status)) {
                if (status == _install_status_messages.success.id) {
                    install_ss(_install_steps_index.master.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.history.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.resource.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.role.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.user.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.finish.index, _install_status_messages.success.id);

                    finishInstall();
                    showSuccess();
                } else if (status == _install_status_messages.failure.id) {
                    install_ss(_install_steps_index.master.index, _install_status_messages.failure.id);
                    install_ss(_install_steps_index.history.index, _install_status_messages.failure.id);
                    install_ss(_install_steps_index.resource.index, _install_status_messages.failure.id);
                    install_ss(_install_steps_index.role.index, _install_status_messages.failure.id);
                    install_ss(_install_steps_index.user.index, _install_status_messages.failure.id);
                    install_ss(_install_steps_index.finish.index, _install_status_messages.failure.id);

                    finishInstall();
                    showFailure();
                }
            }
        }
    }

    function install() {
        try {
            if (!$.cookie)
                throw new Error(_cookie_error_message);

            $.cookie.json = true;
            var config = $.cookie(_cookie_ipems_install_config);

            if (typeof config === 'undefined')
                throw new Error(_cookie_not_existing_message);

            if (typeof config !== 'object')
                throw new Error(_cookie_invalid_message);

            install_cs(config);
        } catch (ex) {
            showError(ex.message);
        }
    }

    function install_cs(config) {
        if (typeof config.database === 'undefined')
            throw new Error(_cookie_invalid_message);

        if (config.database == null)
            throw new Error(_cookie_invalid_message);

        $.each(config.database, function (index, item) {
            if (item.id === _databases_types.master) {
                $.ajax({
                    url: "Installation/InstallCs",
                    data: { type: config.type, data: JSON.stringify(item) },
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        install_ss(_install_steps_index.master.index, _install_status_messages.install.id);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        install_er(_install_steps_index.master.index, _ajax_error_message);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                install_ss(_install_steps_index.master.index, _install_status_messages.success.id);
                                install_hs(config);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            install_er(_install_steps_index.master.index, ex.message);
                        }
                    }
                });

                return false;
            }
        });
    }

    function install_hs(config) {
        if (typeof config.database === 'undefined')
            throw new Error(_cookie_invalid_message);

        if (config.database == null)
            throw new Error(_cookie_invalid_message);

        $.each(config.database, function (index, item) {
            if (item.id === _databases_types.history) {
                $.ajax({
                    url: "Installation/InstallHs",
                    data: { type: config.type, data: JSON.stringify(item) },
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        install_ss(_install_steps_index.history.index, _install_status_messages.install.id);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        install_er(_install_steps_index.history.index, _ajax_error_message);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                install_ss(_install_steps_index.history.index, _install_status_messages.success.id);
                                install_rs(config);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            install_er(_install_steps_index.history.index, ex.message);
                        }
                    }
                });

                return false;
            }
        });
    }

    function install_rs(config) {
        if (typeof config.database === 'undefined')
            throw new Error(_cookie_invalid_message);

        if (config.database == null)
            throw new Error(_cookie_invalid_message);

        $.each(config.database, function (index, item) {
            if (item.id === _databases_types.resource) {
                $.ajax({
                    url: "Installation/InstallRs",
                    data: { type: config.type, data: JSON.stringify(item) },
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        install_ss(_install_steps_index.resource.index, _install_status_messages.install.id);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        install_er(_install_steps_index.resource.index, _ajax_error_message);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                install_ss(_install_steps_index.resource.index, _install_status_messages.success.id);
                                install_rl(config);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            install_er(_install_steps_index.resource.index, ex.message);
                        }
                    }
                });

                return false;
            }
        });
    }

    function install_rl(config) {
        if (typeof config.account === 'undefined')
            throw new Error(_cookie_invalid_message);

        if (config.account == null)
            throw new Error(_cookie_invalid_message);

        $.ajax({
            url: "Installation/InstallRl",
            data: { type: config.type, data: JSON.stringify(config.account.role) },
            dataType: 'json',
            beforeSend: function (jqXHR) {
                install_ss(_install_steps_index.role.index, _install_status_messages.install.id);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                install_er(_install_steps_index.role.index, _ajax_error_message);
            },
            success: function (data, textStatus, jqXHR) {
                try {
                    if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                        throw new Error(_ajax_data_error_message);
                    }

                    if (data.code == _ajax_return_ok_code) {
                        install_ss(_install_steps_index.role.index, _install_status_messages.success.id);
                        install_ue(config);
                    } else {
                        throw new Error(data.message);
                    }
                } catch (ex) {
                    install_er(_install_steps_index.role.index, ex.message);
                }
            }
        });
    }

    function install_ue(config) {
        if (typeof config.account === 'undefined')
            throw new Error(_cookie_invalid_message);

        if (config.account == null)
            throw new Error(_cookie_invalid_message);

        $.ajax({
            url: "Installation/InstallUe",
            data: { type: config.type, data: JSON.stringify(config.account.user) },
            dataType: 'json',
            beforeSend: function (jqXHR) {
                install_ss(_install_steps_index.user.index, _install_status_messages.install.id);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                install_er(_install_steps_index.user.index, _ajax_error_message);
            },
            success: function (data, textStatus, jqXHR) {
                try {
                    if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                        throw new Error(_ajax_data_error_message);
                    }

                    if (data.code == _ajax_return_ok_code) {
                        install_ss(_install_steps_index.user.index, _install_status_messages.success.id);
                        install_fs(config);
                    } else {
                        throw new Error(data.message);
                    }
                } catch (ex) {
                    install_er(_install_steps_index.user.index, ex.message);
                }
            }
        });
    }

    function install_fs(config) {
        $.ajax({
            url: "Installation/InstallFs",
            data: { type: config.type, data: '' },
            dataType: 'json',
            beforeSend: function (jqXHR) {
                install_ss(_install_steps_index.finish.index, _install_status_messages.install.id);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                install_er(_install_steps_index.finish.index, _ajax_error_message);
            },
            success: function (data, textStatus, jqXHR) {
                try {
                    if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                        throw new Error(_ajax_data_error_message);
                    }

                    if (data.code == _ajax_return_ok_code) {
                        install_ss(_install_steps_index.finish.index, _install_status_messages.success.id);
                        finishInstall();
                        showSuccess();
                    } else {
                        throw new Error(data.message);
                    }
                } catch (ex) {
                    install_er(_install_steps_index.finish.index, ex.message);
                }
            }
        });
    }

    function install_ss(index, code) {
        try {
            if (index > _install_steps_index.finish.index)
                throw new Error('Index out of range.');

            var template_0 = '<span class="label label-default"><span class="glyphicon glyphicon-time" aria-hidden="true"></span> ' + _install_status_messages.waiting.text + '</span>',
                template_1 = '<span class="label label-primary"><span class="glyphicon glyphicon-hourglass" aria-hidden="true"></span> ' + _install_status_messages.install.text + '</span>',
                template_2 = '<span class="label label-success"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span> ' + _install_status_messages.success.text + '</span>',
                template_3 = '<span class="label label-danger"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span> ' + _install_status_messages.failure.text + '</span>',
                template_4 = '<span class="label label-warning"><span class="glyphicon glyphicon-ban-circle" aria-hidden="true"></span> ' + _install_status_messages.cancel.text + '</span>';

            var status_th = $('#install-status tbody tr:eq(' + index + ') th');
            if (status_th.length > 0) {
                switch (code) {
                    case _install_status_messages.waiting.id:
                        status_th.html(template_0);
                        break;
                    case _install_status_messages.install.id:
                        status_th.html(template_1);
                        break;
                    case _install_status_messages.success.id:
                        status_th.html(template_2);
                        break;
                    case _install_status_messages.failure.id:
                        status_th.html(template_3);
                        break;
                    case _install_status_messages.cancel.id:
                        status_th.html(template_4);
                        break;
                    default:
                        break;
                }
            }
        } catch (ex) {
            showError(ex.message);
        }
    }

    function install_er(index, message) {
        switch (index) {
            case _install_steps_index.master.index:
                install_ss(_install_steps_index.master.index, _install_status_messages.failure.id);
                install_ss(_install_steps_index.history.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.resource.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.role.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.user.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.finish.index, _install_status_messages.cancel.id);
                break;
            case _install_steps_index.history.index:
                install_ss(_install_steps_index.history.index, _install_status_messages.failure.id);
                install_ss(_install_steps_index.resource.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.role.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.user.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.finish.index, _install_status_messages.cancel.id);
                break;
            case _install_steps_index.resource.index:
                install_ss(_install_steps_index.resource.index, _install_status_messages.failure.id);
                install_ss(_install_steps_index.role.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.user.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.finish.index, _install_status_messages.cancel.id);
                break;
            case _install_steps_index.role.index:
                install_ss(_install_steps_index.role.index, _install_status_messages.failure.id);
                install_ss(_install_steps_index.user.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.finish.index, _install_status_messages.cancel.id);
                break;
            case _install_steps_index.user.index:
                install_ss(_install_steps_index.user.index, _install_status_messages.failure.id);
                install_ss(_install_steps_index.finish.index, _install_status_messages.cancel.id);
                break;
            case _install_steps_index.finish.index:
                install_ss(_install_steps_index.finish.index, _install_status_messages.failure.id);
                break;
            default:
                break;
        }

        showError(message);
        showFailure();
        finishInstall();
        removeCookie();
    }

    function showPreviousButton(show) {
        if (_wizard_steps_panel.length == 0)
            return;

        var previous = _wizard_steps_panel.find(".actions a[href$='#previous']").parent();
        previous._showAria(show);
    }

    function showNextButton(show) {
        if (_wizard_steps_panel.length == 0)
            return;

        var next = _wizard_steps_panel.find(".actions a[href$='#next']").parent();
        next._showAria(show);
    }

    function showError(message) {
        $('#words').html(message);
        $('#alert').fadeIn(500);
    }

    function hideError() {
        $('#words').html('');
        $('#alert').fadeOut(500);
    }

    function showSuccess() {
        $('#success-panel').fadeIn(500);
        if ($.cookie) {
            $.cookie(_cookie_ipems_install_status, _install_status_messages.success.id);
        }
    }

    function showFailure() {
        $('#failure-panel').fadeIn(500);
        if ($.cookie) {
            $.cookie(_cookie_ipems_install_status, _install_status_messages.failure.id);
        }
    }

    function removeCookie() {
        if ($.cookie) {
            //$.removeCookie(_cookie_ipems_install_config);
            $.removeCookie(_cookie_ipems_install_status);
            $.removeCookie(_cookiePrefix);
        }
    }

    function finishInstall() {
        if (_wizard_steps.length == 0)
            return;

        _wizard_steps.steps('finish');
    }

    $(document).ready(function () {
        //disabled context menu.
        $(document).bind("contextmenu", function (e) {
            return false;
        });

        //disabled F5.
        $(document).bind("keydown", function (e) {
            e = window.event || e;
            if (e.keyCode == 116) {
                e.keyCode = 0;
                return false;
            }
        });

        _wizard_steps_panel = $('#wizard-panel');
        _wizard_steps = _wizard_steps_panel.steps({
            headerTag: 'h1',
            bodyTag: 'section',
            transitionEffect: 'fade',
            enablePagination: true,
            enableFinishButton: false,
            autoFocus: true,
            saveState: true,
            forceMoveForward: false,
            labels: {
                finish: _steps_labels.finish,
                next: _steps_labels.next,
                previous: _steps_labels.previous
            },
            onStepChanging: function (event, currentIndex, newIndex) {
                if (currentIndex === 4) {
                    return false;
                }

                showPreviousButton(newIndex < 4);

                if (currentIndex > newIndex) {
                    return true;
                }

                if (!$.cookie) {
                    showError(_cookie_error_message);
                    return false;
                }

                hideError();
                switch (currentIndex) {
                    case 0:
                        return setStepCookie_0();
                    case 1:
                        return setStepCookie_1();
                    case 2:
                        return setStepCookie_2();
                    default:
                        break;
                }

                return true;
            },
            onStepChanged: function (event, currentIndex, priorIndex) {
                switch (currentIndex) {
                    case 0:
                        getStepCookie_0();
                        break;
                    case 1:
                        getStepCookie_1();
                        break;
                    case 2:
                        getStepCookie_2();
                        break;
                    case 3:
                        getStepCookie_3();
                        break;
                    case 4:
                        install();
                        break;
                    default:
                        break;
                }
            },
            onCanceled: function (event) { },
            onFinishing: function (event, currentIndex) { return true; },
            onFinished: function (event, currentIndex) { }
        });

        $('.icheck-radio-list input').each(function () {
            var self = $(this),
            label = self.next(),
            label_text = label.text();

            label.remove();
            self.iCheck({
                checkboxClass: 'icheckbox_line-blue',
                radioClass: 'iradio_line-blue',
                insert: '<div class="icheck_line-icon"></div>' + label_text
            });
        });

        $('.icheck-radio-group input').each(function () {
            $(this).iCheck({
                checkboxClass: 'icheckbox_flat-blue',
                radioClass: 'iradio_flat-blue'
            });
        });

        $('input:radio[name*="Master"]').on('ifChecked', function (event) {
            var val = $('input:radio[name*="Master"]:checked').val();
            switch (parseInt(val, 0)) {
                case 0:
                    $('#masterNewDatabaseName').removeAttr('disabled');
                    $('#masterNewDatabasePath').removeAttr('disabled');
                    $('#masterDontCheckDatabase').iCheck('disable');
                    var medn = $('#masterExistingDatabaseName');
                    medn.attr('disabled', 'disabled');
                    var info1 = medn.closest('.input-group');
                    if (info1.hasClass('has-error')) {
                        info1.removeClass('has-error');
                        info1.next().html('');
                    }
                    break;
                case 1:
                    $('#masterExistingDatabaseName').removeAttr('disabled');
                    $('#masterDontCheckDatabase').iCheck('enable');
                    var mndn = $('#masterNewDatabaseName');
                    mndn.attr('disabled', 'disabled');
                    var info2 = mndn.closest('.input-group');
                    if (info2.hasClass('has-error')) {
                        info2.removeClass('has-error');
                        info2.next().html('');
                    }

                    var mndp = $('#masterNewDatabasePath');
                    mndp.attr('disabled', 'disabled');
                    var info3 = mndp.closest('.input-group');
                    if (info3.hasClass('has-error')) {
                        info3.removeClass('has-error');
                        info3.next().html('');
                    }
                    break;
                default:
                    break;
            }
        });

        $('input:radio[name*="History"]').on('ifChecked', function (event) {
            var val = $('input:radio[name*="History"]:checked').val();
            switch (parseInt(val, 0)) {
                case 0:
                    $('#hisNewDatabaseName').removeAttr('disabled');
                    $('#hisNewDatabasePath').removeAttr('disabled');
                    $('#hisDontCheckDatabase').iCheck('disable');
                    var medn = $('#hisExistingDatabaseName');
                    medn.attr('disabled', 'disabled');
                    var info1 = medn.closest('.input-group');
                    if (info1.hasClass('has-error')) {
                        info1.removeClass('has-error');
                        info1.next().html('');
                    }
                    break;
                case 1:
                    $('#hisExistingDatabaseName').removeAttr('disabled');
                    $('#hisDontCheckDatabase').iCheck('enable');
                    var mndn = $('#hisNewDatabaseName');
                    mndn.attr('disabled', 'disabled');
                    var info2 = mndn.closest('.input-group');
                    if (info2.hasClass('has-error')) {
                        info2.removeClass('has-error');
                        info2.next().html('');
                    }

                    var mndp = $('#hisNewDatabasePath');
                    mndp.attr('disabled', 'disabled');
                    var info3 = mndp.closest('.input-group');
                    if (info3.hasClass('has-error')) {
                        info3.removeClass('has-error');
                        info3.next().html('');
                    }
                    break;
                default:
                    break;
            }
        });

        $('input:radio[name*="Resource"]').on('ifChecked', function (event) {
            var val = $('input:radio[name*="Resource"]:checked').val();
            switch (parseInt(val, 0)) {
                case 0:
                    $('#resNewDatabaseName').removeAttr('disabled');
                    $('#resNewDatabasePath').removeAttr('disabled');
                    $('#resDontCheckDatabase').iCheck('disable');
                    var medn = $('#resExistingDatabaseName');
                    medn.attr('disabled', 'disabled');
                    var info1 = medn.closest('.input-group');
                    if (info1.hasClass('has-error')) {
                        info1.removeClass('has-error');
                        info1.next().html('');
                    }
                    break;
                case 1:
                    $('#resExistingDatabaseName').removeAttr('disabled');
                    $('#resDontCheckDatabase').iCheck('enable');
                    var mndn = $('#resNewDatabaseName');
                    mndn.attr('disabled', 'disabled');
                    var info2 = mndn.closest('.input-group');
                    if (info2.hasClass('has-error')) {
                        info2.removeClass('has-error');
                        info2.next().html('');
                    }

                    var mndp = $('#resNewDatabasePath');
                    mndp.attr('disabled', 'disabled');
                    var info3 = mndp.closest('.input-group');
                    if (info3.hasClass('has-error')) {
                        info3.removeClass('has-error');
                        info3.next().html('');
                    }
                    break;
                default:
                    break;
            }
        });


        $('#insform').validate({
            rules: {
                masterServerName: {
                    ipv4: true
                },
                masterPort: {
                    digits: true,
                    max: 65535,
                    min: 1
                },
                hisServerName: {
                    ipv4: true
                },
                hisPort: {
                    required: true,
                    digits: true,
                    max: 65535,
                    min: 1
                },
                resServerName: {
                    ipv4: true
                },
                resPort: {
                    required: true,
                    digits: true,
                    max: 65535,
                    min: 1
                }
            },
            messages: {
                masterServerName: {
                    ipv4: _ipv4_error_message
                },
                hisServerName: {
                    ipv4: _ipv4_error_message
                },
                resServerName: {
                    ipv4: _ipv4_error_message
                }
            },
            errorElement: 'span',
            highlight: function (element) {
                var pt = $(element).closest('.input-group');
                if (!pt.hasClass('has-error'))
                    pt.addClass('has-error');
            },
            errorPlacement: function (error, element) {
                error.appendTo(element.parent().next());
            },
            success: function (label, element) {
                var pt = $(element).closest('.input-group');
                if (pt.hasClass('has-error'))
                    pt.removeClass('has-error');
            }
        });

        initPanel();
    });
})(jQuery);