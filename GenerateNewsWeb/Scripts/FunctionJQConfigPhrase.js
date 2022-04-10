var countSelect = 0;
var ConfigPhraseId = 0;
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

    if (URL.indexOf('ConfigPhrase') > 0) {
        ConfigPhraseCount();
        SelectConfigPhrase('', '');
    }
});

$('#RowCount').change(function () {
    var topmax = Number($('#RowCount option:selected').val());
    SelectConfigPhrase(0, topmax);
});

function tgAddTable() {
    $('.tbAddTable').toggle('slow');
};

$('#refNext').click(function () {
    if (URL.indexOf('ConfigPhrase') > 0) {
        var topmin = Number($('#ConfigPhraseSelectTable tr:last td:eq(0)').text()) + 1;
        var topmax = topmin + Number($('#RowCount option:selected').val()) - 1;
        var step = $('#ConfigPhraseSelectTable tr:last').children('td').eq(0).text();
        var ind = Number($('#refCurrent').text()) + 1;
        if (step < countSelect) {
            $('#refCurrent').text(ind);
            SelectConfigPhrase(topmin, topmax);
        }
    }  
});

$('#refPrevious').click(function () {
    if (URL.indexOf('ConfigPhrase') > 0) {
        var topmin = $('#ConfigPhraseSelectTable tr:eq(2)').children('td').eq(0).text() - Number($('#RowCount option:selected').val());
        var topmax = Number($('#ConfigPhraseSelectTable tr:eq(2) td:eq(0)').text()) - 1;
        if (topmin > 0) { SelectConfigPhrase(topmin, topmax); }
        var step = $('#ConfigPhraseSelectTable tr:last').children('td').eq(0).text();
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
    if (URL.indexOf('ConfigPhrase') > 0) { SelectConfigPhrase('', ''); }
});

$('#refFinish').click(function () {
    if (URL.indexOf('ConfigPhrase') > 0) {
        var topmin = countSelect - Number($('#RowCount option:selected').val());
        var topmax = countSelect;
        alert(topmin + ' ' + topmax);
        SelectConfigPhrase(topmin, topmax);
    }
    var ind = Math.ceil(Number(countSelect) / Number($('#RowCount option:selected').val()));
    $('#refCurrent').text(ind);
});

$('#ConfigPhraseSelectTable').on('click', 'tr', function () {
    if ($(this).closest('tr').index() != 0 && $(this).closest('tr').index() != 1) {
        $('.popup-fade').show();
    }
    if (URL.indexOf('ConfigPhrase') > 0) {
        ConfigPhraseId = $(this).attr('value');
        $('#txtContainsPhrase').val($(this).children('td').eq(1).text());
        $('#txtPartPhrase').val($(this).children('td').eq(2).text());
        $('#txtGeneralPhrase').val($(this).children('td').eq(3).text());
        $('#txtFormPhrase').val($(this).children('td').eq(4).text());
        $('#txtEmotionLevel').val($(this).children('td').eq(5).text());
        $('#txtThemePhrase').val($(this).children('td').eq(6).text());
        $('#txtMixPhrase').val($(this).children('td').eq(7).text());
        $('#txtNotMixPhrase').val($(this).children('td').eq(8).text());
        $('#txtGroupPhrase').val($(this).children('td').eq(9).text());
        $('#txtActiveConfig').attr('checked', $(this).children('td').eq(10).val());
        $('#txtActiveConfig').attr('checked', $(this).children('td').eq(10).val());
    }
});

function UpdateConfigPhrase() {
    var obj = new Object();
    obj.Id = ConfigPhraseId;
    obj.PhraseContains = $('#txtContainsPhrase').val();
    obj.Word = $('#txtGeneralPhrase').val();
    obj.StringTheme = $('#txtThemePhrase').val();
    obj.Form = $('#txtFormPhrase').val();
    obj.EmotionLevel = $('#txtEmotionLevel').val();
    obj.Theme = $('#txtThemePhrase').val();
    obj.MixTheme = $('#txtMixPhrase').val();
    obj.NotMixTheme = $('#txtNotMixPhrase').val();   
    obj.Grouped = $('#txtGroupPhrase').val();
    obj.Active = $('#txtActiveConfig').val();

    $.ajax({
        type: "POST",
        url: "/Main/UpdateConfigPhrase",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var topmin = Number($('#ConfigPhraseSelectTable tr:eq(2) td:eq(0)').text());
            var topmax = topmin + Number($('#RowCount option:selected').val());
            SelectConfigPhrase(topmin, topmax - 1);
        }
    });
}

function SelectConfigPhrase(tm, tx) {
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
        url: "../Main/SelectConfigPhrase",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "false",
        success: function (data) {
            $('.SelectTableRow').detach();
            $.each(data, function (i, v) {
                $('#ConfigPhraseSelectTable').append(
                    "<tr class='SelectTableRow' value=" + this.Id + ">"
                    + "<td>" + this.RowNum + "</td>"
                    + "<td>" + this.PhraseContains + "</td>"
                    + "<td>" + this.Word + "</td>"
                    + "<td>" + this.Theme + "</td>"
                    + "<td>" + this.Form + "</td>"
                    + "<td>" + this.EmotionLevel + "</td>"
                    + "<td>" + this.StringTheme + "</td>"
                    + "<td>" + this.MixTheme + "</td>"
                    + "<td>" + this.NotMixTheme + "</td>"
                    + "<td>" + this.Group + "</td>"
                    + "<td><input type='checkbox' disabled = 'disabled' align='center' checked='" + this.Active + "'></td>"
                    + "</tr>"
                );
            });
        }
    });
};

function InsertConfigPhrase() {
    var obj = new Object();
    obj.PhraseContains = $('#strPhrasesContainsBox option:selected').val();
    obj.Word = $('#strPhraseBox').val();
    obj.Theme = $('#GeneralStrPhraseBox').val();
    obj.Form = $('#strPhraseFormBox option:selected').val();
    obj.EmotionLevel = $('#EmoutionBox option:selected').val();
    obj.StringTheme = $('#ThemePhraseBox option:selected').val();
    obj.MixTheme = $('#MixThemePhraseBox').val();
    obj.NotMixTheme = $('#NotMixThemePhraseBox').val();
    obj.Group = $('#GroupedBox').val();
    $.ajax({
        type: "POST",
        url: "/Main/InsertConfigPhrase",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            alert(data);
        }
    });
};

function ConfigPhraseCount() {
    $.ajax({
        type: "POST",
        url: "/Main/ConfigPhraseCount",
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