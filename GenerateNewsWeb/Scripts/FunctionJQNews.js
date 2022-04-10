var countSelect = 0;
var NewsId = 0;
var URL = '';
$(document).ready(function () {
    URL = window.location.href;
    $('.popup-close').click(function () {
        $(this).parents('.popup-fade').fadeOut();
        return false;
    });

    // Закрытие по клавише Esc.
    $(document).keydown(function (e) {
        if (e.keyCode === 27) {
            e.stopPropagation();
            $('.popup-fade').fadeOut();
        }
    });

    // Клик по фону, но не по окну.
    $('.popup-fade').click(function (e) {
        if ($(e.target).closest('.popup').length == 0) {
            $(this).fadeOut();
        }
    });

    if (URL.indexOf('News') > 0) {
        NewsCount();
        SelectNews('', '');
    }
});

function tgAddTable() {
    $('.tbAddTable').toggle('slow');
};

$('#refNext').click(function () {    
    if (URL.indexOf('News') > 0) {
        var topmin = Number($('#NewsSelectTable tr:last td:eq(0)').text()) + 1;
        var topmax = topmin + Number($('#RowCount option:selected').val()) - 1;
        var step = $('#NewsSelectTable tr:last').children('td').eq(0).text();
        var ind = Number($('#refCurrent').text()) + 1;
        if (step < countSelect) {
            $('#refCurrent').text(ind);
            SelectNews(topmin, topmax);
        }
    }   
});

$('#refPrevious').click(function () {  
    if (URL.indexOf('News') > 0) {
        var topmin = $('#NewsSelectTable tr:eq(2)').children('td').eq(0).text() - Number($('#RowCount option:selected').val());
        var topmax = Number($('#NewsSelectTable tr:eq(2) td:eq(0)').text()) - 1;
        if (topmin > 0) { SelectNews(topmin, topmax); }
        var step = $('#NewsSelectTable tr:last').children('td').eq(0).text();
        var ind = Number($('#refCurrent').text()) - 1;
        if (ind == 0) ind = 1;
        $('#refCurrent').text(ind);
        if (step <= 0) {
            step = 0;
            $('#refCurrent').text('1');
        }
    }
});

$('#refStart').click(function () {
    $('#refCurrent').text('1');
    if (URL.indexOf('News') > 0) { SelectNews('', ''); }
});

$('#refFinish').click(function () {  
    if (URL.indexOf('News') > 0) {
        var topmin = countSelect - Number($('#RowCount option:selected').val());
        var topmax = countSelect;
        SelectNews(topmin, topmax);
    }
    var ind = Math.ceil(Number(countSelect) / Number($('#RowCount option:selected').val()));
    $('#refCurrent').text(ind);
});

$('#NewsSelectTable').on('click', 'tr', function () {
    $('.popup-fade').show();
    if (URL.indexOf('News') > 0) {
        NewsId = $(this).attr('value');
        $('#txtNews').val($(this).children('td').eq(1).text());
        $('#txtThemeNews').val($(this).children('td').eq(2).text());
        $('#txtEmotionLevelNews').val($(this).children('td').eq(3).text());
        $('#txtActiveNews').attr('checked', $(this).children('td').eq(4).val());
    }
});

function UpdateNews() {
    var obj = new Object();
    obj.Id = NewsId;
    obj.News = $('#txtNews').val();
    obj.Theme = $('#txtThemeNews').val();
    obj.EmotionLevel = $('#txtEmotionLevelNews').val();
    obj.Active = $('#txtActiveNews').val();

    $.ajax({
        type: "POST",
        url: "/Main/UpdateNews",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var topmin = Number($('#NewsSelectTable tr:eq(2) td:eq(0)').text());
            var topmax = topmin + Number($('#RowCount option:selected').val());
            SelectNews(topmin, topmax - 1);
            $('.popup-fade').hide();
        }
    });
}

function SelectNews(tm, tx) {
    var obj = new Object();
    if (tm == '' && tx == '') {
        obj.topmin = 1;
        obj.topmax = obj.topmin * 1 + $('#RowCount option:selected').val() * 1.0 - 1;
    }
    else {
        obj.topmin = tm;
        obj.topmax = tx;
    }

    $.ajax({
        type: "POST",
        url: "../Main/SelectNews",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "false",
        success: function (data) {
            $('.SelectTableRow').detach();
            $.each(data, function (i, v) {
                $('#NewsSelectTable').append(
                    "<tr class='SelectTableRow' value=" + this.Id + ">"
                    + "<td>" + this.RowNum + "</td>"
                    + "<td>" + this.News + "</td>"
                    + "<td>" + this.Theme + "</td>"
                    + "<td>" + this.EmotionLevel + "</td>"
                    + "<td><input type='checkbox' disabled = 'disabled' align='center' checked='" + this.Active + "'></td>"
                    + "</tr>"
                );
            });
        }
    });
};

function InsertNews() {
    var obj = new Object();
    obj.News = $('#NewsBox').val();
    obj.Theme = $('#NewsThemeBox').val();
    obj.EmotionLevel = $('#EmoutionBox').val();
    $.ajax({
        type: "POST",
        url: "/Main/InsertNews",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            alert(data);
        }
    });
};

function NewsCount() {
    $.ajax({
        type: "POST",
        url: "/Main/NewsCount",
        data: JSON.stringify(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $('.cntSelectTable').text('Всего записей: ' + data);
            countSelect = data;
        }
    });
};