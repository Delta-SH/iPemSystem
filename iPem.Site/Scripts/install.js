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
        rs: 0,
        cs: 1,
        sc: 2
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
        rs: { index: 0, status: _install_status_messages.waiting.id },
        cs: { index: 1, status: _install_status_messages.waiting.id },
        sc: { index: 2, status: _install_status_messages.waiting.id },
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
                        case _databases_types.rs:
                            $('#rsServerName').val(item.ipv4);
                            $('#rsPort').val(item.port);
                            $('#rsUid').val(item.uid);
                            $('#rsPwd').val(item.pwd);
                            $('input:radio[name*="Rs"]').eq(item.crdnew).iCheck('check');
                            $('#rsNewDatabaseName').val(item.name);
                            $('#rsNewDatabasePath').val(item.path);
                            $('#rsExistingDatabaseName').val(item.oname);
                            $('#rsDontCheckDatabase').iCheck(item.checked ? 'check' : 'uncheck');
                            break;
                        case _databases_types.cs:
                            $('#csServerName').val(item.ipv4);
                            $('#csPort').val(item.port);
                            $('#csUid').val(item.uid);
                            $('#csPwd').val(item.pwd);
                            $('input:radio[name*="Cs"]').eq(item.crdnew).iCheck('check');
                            $('#csNewDatabaseName').val(item.name);
                            $('#csNewDatabasePath').val(item.path);
                            $('#csExistingDatabaseName').val(item.oname);
                            $('#csDontCheckDatabase').iCheck(item.checked ? 'check' : 'uncheck');
                            break;
                        case _databases_types.sc:
                            $('#scServerName').val(item.ipv4);
                            $('#scPort').val(item.port);
                            $('#scUid').val(item.uid);
                            $('#scPwd').val(item.pwd);
                            $('input:radio[name*="Sc"]').eq(item.crdnew).iCheck('check');
                            $('#scNewDatabaseName').val(item.name);
                            $('#scNewDatabasePath').val(item.path);
                            $('#scExistingDatabaseName').val(item.oname);
                            $('#scDontCheckDatabase').iCheck(item.checked ? 'check' : 'uncheck');
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
                    id: _databases_types.rs,
                    ipv4: $('#rsServerName').val(),
                    port: parseInt($('#rsPort').val(), 0),
                    uid: $('#rsUid').val(),
                    pwd: $('#rsPwd').val(),
                    crdnew: parseInt($('input:radio[name*="Rs"]:checked').val(), 0),
                    name: $('#rsNewDatabaseName').val(),
                    path: $('#rsNewDatabasePath').val(),
                    oname: $('#rsExistingDatabaseName').val(),
                    checked: $('#rsDontCheckDatabase').is(':checked')
                },
                {
                    id: _databases_types.cs,
                    ipv4: $('#csServerName').val(),
                    port: parseInt($('#csPort').val(), 0),
                    uid: $('#csUid').val(),
                    pwd: $('#csPwd').val(),
                    crdnew: parseInt($('input:radio[name*="Cs"]:checked').val(), 0),
                    name: $('#csNewDatabaseName').val(),
                    path: $('#csNewDatabasePath').val(),
                    oname: $('#csExistingDatabaseName').val(),
                    checked: $('#csDontCheckDatabase').is(':checked')
                },
                {
                    id: _databases_types.sc,
                    ipv4: $('#scServerName').val(),
                    port: parseInt($('#scPort').val(), 0),
                    uid: $('#scUid').val(),
                    pwd: $('#scPwd').val(),
                    crdnew: parseInt($('input:radio[name*="Sc"]:checked').val(), 0),
                    name: $('#scNewDatabaseName').val(),
                    path: $('#scNewDatabasePath').val(),
                    oname: $('#scExistingDatabaseName').val(),
                    checked: $('#scDontCheckDatabase').is(':checked')
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
                            case _databases_types.rs:
                                $('#cf-rs-01').html(item.ipv4);
                                $('#cf-rs-02').html(item.port);
                                $('#cf-rs-03').html(item.uid);
                                $('#cf-rs-04').html(item.pwd);
                                $('#cf-rs-05').html(item.crdnew === 0 ? _confirm_messages.created : _confirm_messages.existed);
                                $('#cf-rs-06').html(item.crdnew === 0 ? item.name : item.oname);
                                $('#cf-rs-07').html(item.crdnew === 0 ? item.path : 'undefined');
                                break;
                            case _databases_types.cs:
                                $('#cf-cs-01').html(item.ipv4);
                                $('#cf-cs-02').html(item.port);
                                $('#cf-cs-03').html(item.uid);
                                $('#cf-cs-04').html(item.pwd);
                                $('#cf-cs-05').html(item.crdnew === 0 ? _confirm_messages.created : _confirm_messages.existed);
                                $('#cf-cs-06').html(item.crdnew === 0 ? item.name : item.oname);
                                $('#cf-cs-07').html(item.crdnew === 0 ? item.path : 'undefined');
                                break;
                            case _databases_types.sc:
                                $('#cf-sc-01').html(item.ipv4);
                                $('#cf-sc-02').html(item.port);
                                $('#cf-sc-03').html(item.uid);
                                $('#cf-sc-04').html(item.pwd);
                                $('#cf-sc-05').html(item.crdnew === 0 ? _confirm_messages.created : _confirm_messages.existed);
                                $('#cf-sc-06').html(item.crdnew === 0 ? item.name : item.oname);
                                $('#cf-sc-07').html(item.crdnew === 0 ? item.path : 'undefined');
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
                    install_ss(_install_steps_index.rs.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.cs.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.sc.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.role.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.user.index, _install_status_messages.success.id);
                    install_ss(_install_steps_index.finish.index, _install_status_messages.success.id);

                    finishInstall();
                    showSuccess();
                } else if (status == _install_status_messages.failure.id) {
                    install_ss(_install_steps_index.rs.index, _install_status_messages.failure.id);
                    install_ss(_install_steps_index.cs.index, _install_status_messages.failure.id);
                    install_ss(_install_steps_index.sc.index, _install_status_messages.failure.id);
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

            install_rs(config);
        } catch (ex) {
            showError(ex.message);
        }
    }

    function install_rs(config) {
        if (typeof config.database === 'undefined')
            throw new Error(_cookie_invalid_message);

        if (config.database == null)
            throw new Error(_cookie_invalid_message);

        $.each(config.database, function (index, item) {
            if (item.id === _databases_types.rs) {
                $.ajax({
                    url: "/Installation/InstallRs",
                    data: { type: config.type, data: JSON.stringify(item) },
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        install_ss(_install_steps_index.rs.index, _install_status_messages.install.id);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        install_er(_install_steps_index.rs.index, _ajax_error_message);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                install_ss(_install_steps_index.rs.index, _install_status_messages.success.id);
                                install_cs(config);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            install_er(_install_steps_index.rs.index, ex.message);
                        }
                    }
                });

                return false;
            }
        });
    }

    function install_cs(config) {
        if (typeof config.database === 'undefined')
            throw new Error(_cookie_invalid_message);

        if (config.database == null)
            throw new Error(_cookie_invalid_message);

        $.each(config.database, function (index, item) {
            if (item.id === _databases_types.cs) {
                $.ajax({
                    url: "/Installation/InstallCs",
                    data: { type: config.type, data: JSON.stringify(item) },
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        install_ss(_install_steps_index.cs.index, _install_status_messages.install.id);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        install_er(_install_steps_index.cs.index, _ajax_error_message);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                install_ss(_install_steps_index.cs.index, _install_status_messages.success.id);
                                install_sc(config);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            install_er(_install_steps_index.cs.index, ex.message);
                        }
                    }
                });

                return false;
            }
        });
    }

    function install_sc(config) {
        if (typeof config.database === 'undefined')
            throw new Error(_cookie_invalid_message);

        if (config.database == null)
            throw new Error(_cookie_invalid_message);

        $.each(config.database, function (index, item) {
            if (item.id === _databases_types.sc) {
                $.ajax({
                    url: "/Installation/InstallSc",
                    data: { type: config.type, data: JSON.stringify(item) },
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        install_ss(_install_steps_index.sc.index, _install_status_messages.install.id);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        install_er(_install_steps_index.sc.index, _ajax_error_message);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                install_ss(_install_steps_index.sc.index, _install_status_messages.success.id);
                                install_rl(config);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            install_er(_install_steps_index.sc.index, ex.message);
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
            url: "/Installation/InstallRl",
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
            url: "/Installation/InstallUe",
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
            url: "/Installation/InstallFs",
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
            case _install_steps_index.rs.index:
                install_ss(_install_steps_index.rs.index, _install_status_messages.failure.id);
                install_ss(_install_steps_index.cs.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.sc.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.role.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.user.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.finish.index, _install_status_messages.cancel.id);
                break;
            case _install_steps_index.cs.index:
                install_ss(_install_steps_index.cs.index, _install_status_messages.failure.id);
                install_ss(_install_steps_index.sc.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.role.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.user.index, _install_status_messages.cancel.id);
                install_ss(_install_steps_index.finish.index, _install_status_messages.cancel.id);
                break;
            case _install_steps_index.sc.index:
                install_ss(_install_steps_index.sc.index, _install_status_messages.failure.id);
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

        $('input:radio[name*="Rs"]').on('ifChecked', function (event) {
            var val = $('input:radio[name*="Rs"]:checked').val();
            switch (parseInt(val, 0)) {
                case 0:
                    $('#rsNewDatabaseName').removeAttr('disabled');
                    $('#rsNewDatabasePath').removeAttr('disabled');
                    $('#rsDontCheckDatabase').iCheck('disable');
                    var medn = $('#rsExistingDatabaseName');
                    medn.attr('disabled', 'disabled');
                    var info1 = medn.closest('.input-group');
                    if (info1.hasClass('has-error')) {
                        info1.removeClass('has-error');
                        info1.next().html('');
                    }
                    break;
                case 1:
                    $('#rsExistingDatabaseName').removeAttr('disabled');
                    $('#rsDontCheckDatabase').iCheck('enable');
                    var mndn = $('#rsNewDatabaseName');
                    mndn.attr('disabled', 'disabled');
                    var info2 = mndn.closest('.input-group');
                    if (info2.hasClass('has-error')) {
                        info2.removeClass('has-error');
                        info2.next().html('');
                    }

                    var mndp = $('#rsNewDatabasePath');
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

        $('input:radio[name*="Cs"]').on('ifChecked', function (event) {
            var val = $('input:radio[name*="Cs"]:checked').val();
            switch (parseInt(val, 0)) {
                case 0:
                    $('#csNewDatabaseName').removeAttr('disabled');
                    $('#csNewDatabasePath').removeAttr('disabled');
                    $('#csDontCheckDatabase').iCheck('disable');
                    var medn = $('#csExistingDatabaseName');
                    medn.attr('disabled', 'disabled');
                    var info1 = medn.closest('.input-group');
                    if (info1.hasClass('has-error')) {
                        info1.removeClass('has-error');
                        info1.next().html('');
                    }
                    break;
                case 1:
                    $('#csExistingDatabaseName').removeAttr('disabled');
                    $('#csDontCheckDatabase').iCheck('enable');
                    var mndn = $('#csNewDatabaseName');
                    mndn.attr('disabled', 'disabled');
                    var info2 = mndn.closest('.input-group');
                    if (info2.hasClass('has-error')) {
                        info2.removeClass('has-error');
                        info2.next().html('');
                    }

                    var mndp = $('#csNewDatabasePath');
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

        $('input:radio[name*="Sc"]').on('ifChecked', function (event) {
            var val = $('input:radio[name*="Sc"]:checked').val();
            switch (parseInt(val, 0)) {
                case 0:
                    $('#scNewDatabaseName').removeAttr('disabled');
                    $('#scNewDatabasePath').removeAttr('disabled');
                    $('#scDontCheckDatabase').iCheck('disable');
                    var medn = $('#scExistingDatabaseName');
                    medn.attr('disabled', 'disabled');
                    var info1 = medn.closest('.input-group');
                    if (info1.hasClass('has-error')) {
                        info1.removeClass('has-error');
                        info1.next().html('');
                    }
                    break;
                case 1:
                    $('#scExistingDatabaseName').removeAttr('disabled');
                    $('#scDontCheckDatabase').iCheck('enable');
                    var mndn = $('#scNewDatabaseName');
                    mndn.attr('disabled', 'disabled');
                    var info2 = mndn.closest('.input-group');
                    if (info2.hasClass('has-error')) {
                        info2.removeClass('has-error');
                        info2.next().html('');
                    }

                    var mndp = $('#scNewDatabasePath');
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
                rsServerName: {
                    ipv4: true
                },
                rsPort: {
                    required: true,
                    digits: true,
                    max: 65535,
                    min: 1
                },
                csServerName: {
                    ipv4: true
                },
                csPort: {
                    required: true,
                    digits: true,
                    max: 65535,
                    min: 1
                },
                scServerName: {
                    ipv4: true
                },
                scPort: {
                    required: true,
                    digits: true,
                    max: 65535,
                    min: 1
                }
            },
            messages: {
                rsServerName: {
                    ipv4: _ipv4_error_message
                },
                csServerName: {
                    ipv4: _ipv4_error_message
                },
                scServerName: {
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