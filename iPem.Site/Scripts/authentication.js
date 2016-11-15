$(document).ready(function () {
    $('#validateForm').validate({
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
});