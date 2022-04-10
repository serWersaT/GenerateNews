var countSelect = 0;
var WordId = 0;
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

    if (URL.indexOf('Words') > 0)
    {      
        WordCount();
        SelectWords('','');
    }
});

function tgAddTable() {
    $('.tbAddTable').toggle('slow');
};

$('#refNext').click(function () {    
    if (URL.indexOf('Words') > 0 && URL.indexOf('ConfigWord') < 0) {
        var topmin = Number($('#WordSelectTable tr:last td:eq(0)').text()) + 1;
        var topmax = topmin + Number($('#RowCount option:selected').val()) - 1;
        var step = $('#WordSelectTable tr:last').children('td').eq(0).text();
        var ind = Number($('#refCurrent').text()) + 1;
        if (step < countSelect) {
            $('#refCurrent').text(ind);
            SelectWords(topmin, topmax);
        }
    }   
});

$('#refPrevious').click(function () {   
    if (URL.indexOf('Words') > 0 && URL.indexOf('ConfigWord') < 0) {
        var topmin = $('#WordSelectTable tr:eq(2)').children('td').eq(0).text() - Number($('#RowCount option:selected').val());
        var topmax = Number($('#WordSelectTable tr:eq(2) td:eq(0)').text()) - 1;
        if (topmin > 0) { SelectWords(topmin, topmax); }
        var step = $('#WordSelectTable tr:last').children('td').eq(0).text();
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
    if (URL.indexOf('Words') > 0 && URL.indexOf('ConfigWord') < 0) { SelectWords('', ''); }
});

$('#refFinish').click(function () {    
    if (URL.indexOf('Words') > 0 && URL.indexOf('ConfigWord') < 0)
    {
        var topmin = countSelect - Number($('#RowCount option:selected').val());
        var topmax = countSelect;
        alert(topmin + ' ' + topmax);
        SelectWords(topmin, topmax);
    }
    var ind = Math.ceil(Number(countSelect) / Number($('#RowCount option:selected').val()));
    $('#refCurrent').text(ind);
});

$('#WordSelectTable').on('click', 'tr', function () {
    if ($(this).closest('tr').index() != 0 && $(this).closest('tr').index() != 1) {
        $('.popup-fade').show();
    }
    if (URL.indexOf('Words') > 0) {
        WordId = $(this).attr('value');
        $('#txtWord').val($(this).children('td').eq(1).text());
        $('#txtTypeWord').val($(this).children('td').eq(2).text());
        $('#txtGenderWord').val($(this).children('td').eq(3).text());
        $('#txtFormWord').val($(this).children('td').eq(4).text());
        $('#txtEmotionLevelWord').val($(this).children('td').eq(5).text());
        $('#txtSynonimWord').val($(this).children('td').eq(6).text());
        $('#txtMixWord').val($(this).children('td').eq(7).text());
        $('#txtNotMixWord').val($(this).children('td').eq(8).text());
        $('#txtActiveWord').attr('checked', $(this).children('td').eq(9).val());
    }
});

function UpdateWord() {
    var obj = new Object();
    obj.Id = WordId;
    obj.Word = $('#txtWord').val();
    obj.Type = $('#txtTypeWord option:selected').val();
    obj.Gender_Type = $('#txtGenderWord option:selected').val();
    obj.FormWord = $('#txtFormWord option:selected').val();
    obj.EmotionLevel = $('#txtEmotionLevelWord option:selected').val();
    obj.Synonim = $('#txtSynonimWord').val();
    obj.Mix = $('#txtMixWord').val();
    obj.Not_Mix = $('#txtNotMixWord').val();
    obj.Active = $('#txtActiveWord').val();

    $.ajax({
        type: "POST",
        url: "/Main/UpdateWord",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var topmin = Number($('#WordSelectTable tr:eq(2) td:eq(0)').text());
            var topmax = topmin + Number($('#RowCount option:selected').val());
            SelectWords(topmin, topmax - 1);
        }
    });
}

function SelectWords(tm, tx) {
    var obj = new Object();
    if (tm == '' && tx == '') {
        obj.topmin = 1;
        obj.topmax = obj.topmin * 1 + $('#RowCount option:selected').val() * 1.0-1;
    }
    else {
        obj.topmin = tm;
        obj.topmax = tx;
    }

    $.ajax({
        type: "POST",
        url: "../Main/SelectWords",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "false",
        success: function (data) {
            $('.SelectTableWord').detach();
            $.each(data, function (i, v) {
                $('#WordSelectTable').append(
                    "<tr class='SelectTableWord' value=" + this.Id + ">"
                    + "<td>" + this.RowNum + "</td>"
                    + "<td>" + this.Word + "</td>"
                    + "<td>" + this.Type + "</td>"
                    + "<td>" + this.Gender_Type + "</td>"
                    + "<td>" + this.FormWord + "</td>"
                    + "<td>" + this.EmotionLevel + "</td>"
                    + "<td>" + this.Synonim + "</td>"
                    + "<td>" + this.Mix + "</td>"
                    + "<td>" + this.Not_Mix + "</td>"
                    + "<td><input type='checkbox' disabled = 'disabled' align='center' checked='" + this.Active + "'></td>"
                    + "</tr>"
                );
            });
        }
    });
};

function InsertWord() {
    var obj = new Object();
    obj.Word = $('#WordBox').val();
    obj.Type = $('#TypeBox').val();
    obj.Gender_Type = $('#GenderBox').val();
    obj.FormWord = $('#FormBox').val();
    obj.EmotionLevel = $('#EmoutionBox').val();
    obj.Synonim = $('#SynonimBox').val();
    obj.Mix = $('#MixBox').val();
    obj.Not_Mix = $('#NotMixBox').val();
    $.ajax({
        type: "POST",
        url: "/Main/InsertWord",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            alert(data);
        }
    });
};

function WordCount() {
    $.ajax({
        type: "POST",
        url: "/Main/WordCount",
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