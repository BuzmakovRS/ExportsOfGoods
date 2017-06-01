
$(function () {
    $.datetimepicker.setLocale('ru');
    $(".datetimepicker").datetimepicker({
        dayOfWeekStart: 1,
        format: 'd.m.Y H:i',
        mask: true,
        minTime: 0,
        minDate: '0',
        maxDate: '+1970/01/14',
        step: 30
    });
});


function GetTimeEnd(timeBeg, pId) {
    $.ajax({
        url: '/CustomsQueues/GetInspTimeEnd',
        type: "POST",
        data: {
            tb: timeBeg,
            pId: pId
        },
        dataType: "text",
        success: function (data) {
            $("#timeEnd").val(data);
        }
    });
}

$("#timeBeg").blur(function () {

    if ($("#timeBeg").val() && $("#partiId").val()) {
        var timeEnd = GetTimeEnd($("#timeBeg").val(), $("#partiId").val());
    }
});