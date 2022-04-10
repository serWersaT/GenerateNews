var countSelect = 0;
var PhraseId = 0;
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

    if (URL.indexOf('Phrases') > 0 && URL.indexOf('ConfigPhrases') < 0) {
        PhraseCount();
        SelectPhrases('', '');
    }
});

function tgAddTable() {
    $('.tbAddTable').toggle('slow');
};

$('#refNext').click(function () {    
    if (URL.indexOf('Phrases') > 0 && URL.indexOf('ConfigPhrases') < 0) {
        var topmin = Number($('#PhraseSelectTable tr:last td:eq(0)').text()) + 1;
        var topmax = topmin + Number($('#RowCount option:selected').val()) - 1;
        var step = $('#PhraseSelectTable tr:last').children('td').eq(0).text();
        var ind = Number($('#refCurrent').text()) + 1;
        if (step < countSelect) {
            $('#refCurrent').text(ind);
            SelectPhrases(topmin, topmax);
        }
    }
});

$('#refPrevious').click(function () {   
    if (URL.indexOf('Phrases') > 0 && URL.indexOf('ConfigPhrases') < 0)
    {
        var topmin = $('#PhraseSelectTable tr:eq(2)').children('td').eq(0).text() - Number($('#RowCount option:selected').val());
        var topmax = Number($('#PhraseSelectTable tr:eq(2) td:eq(0)').text()) - 1;
        if (topmin > 0) { SelectPhrases(topmin, topmax); }

        var step = $('#PhraseSelectTable tr:last').children('td').eq(0).text();
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
    if (URL.indexOf('Phrases') > 0 && URL.indexOf('ConfigPhrases') < 0) { SelectPhrases('', ''); }
});

$('#refFinish').click(function () {
    var topmin = countSelect - Number($('#RowCount option:selected').val());
    var topmax = countSelect;
    if (URL.indexOf('Phrases') > 0) { SelectPhrases(topmin, topmax); }
    var ind = Math.ceil(Number(countSelect) / Number($('#RowCount option:selected').val()));
    $('#refCurrent').text(ind);
});

$('#PhraseSelectTable').on('click', 'tr', function () {
    $('.popup-fade').show();
    if (URL.indexOf('Phrases') > 0 && URL.indexOf('ConfigPhrases') < 0) {
        PhraseId = $(this).attr('value');
        $('#txtPhrase').val($(this).children('td').eq(1).text());
        $('#txtFormPhrase').val($(this).children('td').eq(2).text());
        $('#txtThemePhrase').val($(this).children('td').eq(3).text());
        $('#txtEmotionLevelPhrase').val($(this).children('td').eq(4).text());
        $('#txtMixPhrase').val($(this).children('td').eq(5).text());
        $('#txtNotMixPhrase').val($(this).children('td').eq(6).text());
        $('#txtActivePhrase').attr('checked', $(this).children('td').eq(7).val());
    }
});

function UpdatePhrase() {
    var obj = new Object();
    obj.Id = PhraseId;
    obj.Phrase = $('#txtPhrase').val();
    obj.Form = $('#txtFormPhrase').val();
    obj.Theme = $('#txtThemePhrase').val();
    obj.EmotionLevel = $('#txtEmotionLevelPhrase').val();
    obj.Mix = $('#txtMixPhrase').val();
    obj.Not_Mix = $('#txtNotMixPhrase').val();
    obj.Active = $('#txtActivePhrase').val();

    $.ajax({
        type: "POST",
        url: "/Main/UpdatePhrase",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var topmin = Number($('#PhraseSelectTable tr:eq(2) td:eq(0)').text());
            var topmax = topmin + Number($('#RowCount option:selected').val());
            SelectPhrases(topmin, topmax - 1);
            $('.popup-fade').hide();
        }
    });
}

function SelectPhrases(tm, tx) {
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
        url: "../Main/SelectPhrases",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "false",
        success: function (data) {
            $('.SelectTableRow').detach();
            $.each(data, function (i, v) {                
                $('#PhraseSelectTable').append(
                    "<tr class='SelectTableRow' value=" + this.Id + ">"
                    + "<td>" + this.RowNum + "</td>"
                    + "<td>" + this.Phrase + "</td>"
                    + "<td>" + this.Form + "</td>"
                    + "<td>" + this.Theme + "</td>"
                    + "<td>" + this.EmotionLevel + "</td>"
                    + "<td>" + this.Mix + "</td>"
                    + "<td>" + this.Not_Mix + "</td>"
                    + "<td><input type='checkbox' disabled = 'disabled' align='center' checked='" + this.Active + "'></td>"
                    + "</tr>"
                );
            });
        }
    });
};

function InsertPhrase() {
    var obj = new Object();
    obj.Phrase = $('#PhraseBox').val();
    obj.Form = $('#TypeBox').val();
    obj.Theme = $('#GenderBox').val();
    obj.EmotionLevel = $('#EmoutionBox').val();
    obj.Mix = $('#MixBox').val();
    obj.Not_Mix = $('#NotMixBox').val();
    $.ajax({
        type: "POST",
        url: "/Main/InsertPhrase",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            alert(data);
        }
    });
};

function PhraseCount() {
    $.ajax({
        type: "POST",
        url: "/Main/PhraseCount",
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