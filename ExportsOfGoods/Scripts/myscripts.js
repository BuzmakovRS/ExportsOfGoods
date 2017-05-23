
$(function () {
    $.datetimepicker.setLocale('ru');
    $(".datetimepicker").datetimepicker({
        dayOfWeekStart: 1,
        format: 'd.m.Y H:i',
        mask: true,
        minDate: '0',
        maxDate: '+1970/01/14',      
        step: 30
    });
});
