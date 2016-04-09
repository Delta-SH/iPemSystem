(function ($) {
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
     * The loading message for ajax.
     *
     * @final
     * @private
     * @property _ajax_loading_message
     * @type String
     **/
    var _ajax_loading_message = '<strong>系统提示:</strong> 正在处理，请稍后...';

    /**
     * The ok message for ajax.
     *
     * @final
     * @private
     * @property _ajax_ok_message
     * @type String
     **/
    var _ajax_ok_message = '<strong>系统提示:</strong> 请求已完成，执行成功。';

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

    $.ajaxSetup({ type: "POST", cache: false, timeout: 60000 });

    var status_loading = function (form, message) {
        var test = form.find('#testbtn'),
            save = form.find('#savebtn'),
            loading = form.find('#form-loading'),
            error = form.find('#form-error'),
            ok = form.find('#form-ok'),
            loadingtips = loading.children("span").eq(2),
            errortips = error.children("span").eq(2),
            oktips = ok.children("span").eq(2);

        loadingtips.html(message || _ajax_loading_message);
        errortips.html('');
        oktips.html('');

        loading.show();
        error.hide();
        ok.hide();

        test.attr('disabled', 'disabled');
        save.attr('disabled', 'disabled');
    };

    var status_ok = function (form, message) {
        var test = form.find('#testbtn'),
            save = form.find('#savebtn'),
            loading = form.find('#form-loading'),
            error = form.find('#form-error'),
            ok = form.find('#form-ok'),
            loadingtips = loading.children("span").eq(2),
            errortips = error.children("span").eq(2),
            oktips = ok.children("span").eq(2);

        loadingtips.html('');
        errortips.html('');
        oktips.html(message || _ajax_ok_message);

        loading.hide();
        error.hide();
        ok.show();
    };

    var status_error = function (form, message) {
        var test = form.find('#testbtn'),
            save = form.find('#savebtn'),
            loading = form.find('#form-loading'),
            error = form.find('#form-error'),
            ok = form.find('#form-ok'),
            loadingtips = loading.children("span").eq(2),
            errortips = error.children("span").eq(2),
            oktips = ok.children("span").eq(2);

        loadingtips.html('');
        errortips.html(message || _ajax_error_message);
        oktips.html('');

        loading.hide();
        error.show();
        ok.hide();
    };

    var request_completed = function (form) {
        var test = form.find('#testbtn'),
            save = form.find('#savebtn');

        test.removeAttr('disabled');
        save.removeAttr('disabled');
    };

    var jump = function (seconds, display, url) {
        window.setTimeout(function () {
            seconds--;
            if (seconds > 0) {
                display.html(seconds);
                jump(seconds, display, url);
            } else {
                location.href = url;
            }
        }, 1000);
    };

    $(document).ready(function () {
        $('#csForm').validate({
            rules: {
                IP: {
                    ipv4: true
                },
                Port: {
                    required: true,
                    digits: true,
                    max: 65535,
                    min: 1
                }
            },
            messages: {
                IP: {
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

        $('#hsForm').validate({
            rules: {
                IP: {
                    ipv4: true
                },
                Port: {
                    required: true,
                    digits: true,
                    max: 65535,
                    min: 1
                }
            },
            messages: {
                IP: {
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

        $('#rsForm').validate({
            rules: {
                IP: {
                    ipv4: true
                },
                Port: {
                    required: true,
                    digits: true,
                    max: 65535,
                    min: 1
                }
            },
            messages: {
                IP: {
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

        $('#confirmForm').validate({
            rules: {
                'password': {
                    required: true
                }
            },
            highlight: function (element) {
                var pt = $(element).closest('.input-group');
                if (!pt.hasClass('has-error'))
                    pt.addClass('has-error');
            },
            errorPlacement: function (error, element) {
            },
            success: function (label, element) {
                var pt = $(element).closest('.input-group');
                if (pt.hasClass('has-error'))
                    pt.removeClass('has-error');
            }
        });

        $("#csForm button[id=testbtn]").click(function () {
            var form = $('#csForm');
            if (form.valid()) {
                $.ajax({
                    url: "../Installation/DbTest",
                    data: form.serializeArray(),
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        status_loading(form);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                status_ok(form, data.message);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            status_error(form, ex.message);
                        }
                    }
                }).always(function () {
                    request_completed(form);
                });;
            }
        });

        $("#csForm button[id=savebtn]").click(function () {
            var form = $('#csForm');
            if (form.valid()) {
                $.ajax({
                    url: "../Installation/SaveCs",
                    data: form.serializeArray(),
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        status_loading(form);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                status_ok(form, data.message);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            status_error(form, ex.message);
                        }
                    }
                }).always(function () {
                    request_completed(form);
                });;
            }
        });

        $("#hsForm button[id=testbtn]").click(function () {
            var form = $('#hsForm');
            if (form.valid()) {
                $.ajax({
                    url: "../Installation/DbTest",
                    data: form.serializeArray(),
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        status_loading(form);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                status_ok(form, data.message);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            status_error(form, ex.message);
                        }
                    }
                }).always(function () {
                    request_completed(form);
                });;
            }
        });

        $("#hsForm button[id=savebtn]").click(function () {
            var form = $('#hsForm');
            if (form.valid()) {
                $.ajax({
                    url: "../Installation/SaveHs",
                    data: form.serializeArray(),
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        status_loading(form);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                status_ok(form, data.message);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            status_error(form, ex.message);
                        }
                    }
                }).always(function () {
                    request_completed(form);
                });;
            }
        });

        $("#rsForm button[id=testbtn]").click(function () {
            var form = $('#rsForm');
            if (form.valid()) {
                $.ajax({
                    url: "../Installation/DbTest",
                    data: form.serializeArray(),
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        status_loading(form);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                status_ok(form, data.message);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            status_error(form, ex.message);
                        }
                    }
                }).always(function () {
                    request_completed(form);
                });;
            }
        });

        $("#rsForm button[id=savebtn]").click(function () {
            var form = $('#rsForm');
            if (form.valid()) {
                $.ajax({
                    url: "../Installation/SaveRs",
                    data: form.serializeArray(),
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        status_loading(form);
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                status_ok(form, data.message);
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            status_error(form, ex.message);
                        }
                    }
                }).always(function () {
                    request_completed(form);
                });;
            }
        });

        $("#cleanForm button[id=deletebtn]").click(function () {
            var form = $('#confirmForm');
                form[0].reset();
        });

        $("#confirmForm button[id=cleanbtn]").click(function () {
            var base = $('#cleanForm'),
                form = $('#confirmForm'),
                delbtn = base.find('#deletebtn'),
                loading = base.find('#form-loading'),
                error = base.find('#form-error'),
                ok = base.find('#form-ok'),
                loadingtips = loading.children("span").eq(2),
                errortips = error.children("span").eq(2),
                oktips = ok.children("span").eq(2);

            if (form.valid()) {
                $.ajax({
                    url: "../Installation/DbClean",
                    data: form.serializeArray(),
                    dataType: 'json',
                    beforeSend: function (jqXHR) {
                        loadingtips.html(_ajax_loading_message);
                        errortips.html('');
                        oktips.html('');

                        loading.show();
                        error.hide();
                        ok.hide();

                        delbtn.attr('disabled', 'disabled');
                        $('#confirmModal').modal('hide');
                    },
                    success: function (data, textStatus, jqXHR) {
                        try {
                            if (typeof data.code === 'undefined' || typeof data.message === 'undefined') {
                                throw new Error(_ajax_data_error_message);
                            }

                            if (data.code == _ajax_return_ok_code) {
                                loadingtips.html('');
                                errortips.html('');
                                oktips.html(data.message);

                                loading.hide();
                                error.hide();
                                ok.show();
                                jump(5, base.find('#leftseconds'), '/Installation');
                            } else {
                                throw new Error(data.message);
                            }
                        } catch (ex) {
                            loadingtips.html('');
                            errortips.html(ex.message);
                            oktips.html('');

                            loading.hide();
                            error.show();
                            ok.hide();
                        }
                    }
                }).always(function () {
                    delbtn.removeAttr('disabled');
                });
            }
        });
    });
})(jQuery);