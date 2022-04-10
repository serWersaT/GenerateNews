var countSelect = 0;
var ConfigWordId = 0;
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

    if (URL.indexOf('ConfigWords') > 0) {
        ConfigWordCount();
        SelectConfigWord('', '');
    }
});

$('.FindConfWord').change(function () {    
    var ob = new Object();
    ob.RowNum = $('#RowFind').val();
    ob.strContains = $('#ContainsWordFind').val();
    ob.str = $('#WordFindFind').val();
    ob.TypeWord = $('#TypeWordFind').val();
    ob.GenderForm = $('#GenderWordFind').val();
    ob.FormWord = $('#FormWordFind').val();
    ob.EmotionLevel = $('#EmotionLevelFind').val();
    ob.strMixContains = $('#ContainsMixWordFind').val();
    ob.Mix = $('#WordMixFindFind').val();
    ob.strNotMixContains = $('#ContainsNotMixWordFind').val();
    ob.Not_Mix = $('#WordNotMixFind').val();
    ob.NotMixTypeWord = $('#WordNotMixTypeFind').val();
    ob.NotMixGenderForm = $('#WordNotMixGenderFind').val();
    ob.NotMixFormWord = $('#WordNotMixFormFind').val();
    ob.MixTypeWord = $('#WordMixTypeFind').val();
    ob.MixGenderForm = $('#WordMixGenderFind').val();
    ob.MixFormWord = $('#WordMixFormFind').val();
    ob.Grouped = $('#GroupedFind').val();
    ob.topmax = Number($('#RowCount option:selected').val());
    $.ajax({
        type: "POST",
        url: "/Main/FindConfWord",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            alert(data);
            $('.SelectTableRow').detach();
            $.each(data, function (i, v) {
                alert(this);
                $('#ConfigWordSelectTable').append(
                    "<tr class='SelectTableRow' value=" + this.Id + ">"
                    + "<td>" + this.RowNum + "</td>"
                    + "<td>" + this.strContains + "</td>"
                    + "<td>" + this.str + "</td>"
                    + "<td>" + this.TypeWord + "</td>"
                    + "<td>" + this.GenderForm + "</td>"
                    + "<td>" + this.FormWord + "</td>"
                    + "<td>" + this.EmotionLevel + "</td>"
                    + "<td>" + this.strMixContains + "</td>"
                    + "<td>" + this.Mix + "</td>"
                    + "<td>" + this.strNotMixContains + "</td>"
                    + "<td>" + this.Not_Mix + "</td>"
                    + "<td>" + this.NotMixTypeWord + "</td>"
                    + "<td>" + this.NotMixGenderForm + "</td>"
                    + "<td>" + this.NotMixFormWord + "</td>"
                    + "<td>" + this.MixTypeWord + "</td>"
                    + "<td>" + this.MixGenderForm + "</td>"
                    + "<td>" + this.MixFormWord + "</td>"
                    + "<td>" + this.Grouped + "</td>"
                    + "<td><input type='checkbox' disabled = 'disabled' align='center' checked='" + this.Active + "'></td>"
                    + "</tr>"
                );
            });
        }
    });

});

function tgAddTable() {
    $('.tbAddTable').toggle('slow');
};

$('#RowCount').change(function () {
    var topmax = Number($('#RowCount option:selected').val());
    SelectConfigWord(0, topmax);
});


$('#refNext').click(function () {   
    if (URL.indexOf('ConfigWords') > 0) {
        var topmin = Number($('#ConfigWordSelectTable tr:last td:eq(0)').text()) + 1;
        var topmax = topmin + Number($('#RowCount option:selected').val()) - 1;
        var step = $('#ConfigWordSelectTable tr:last').children('td').eq(0).text();
        var ind = Number($('#refCurrent').text()) + 1;
        if (step < countSelect) {
            $('#refCurrent').text(ind);
            SelectConfigWord(topmin, topmax);
        }
    }  
});

$('#refPrevious').click(function () {    
    if (URL.indexOf('ConfigWords') > 0) {
        var topmin = $('#ConfigWordSelectTable tr:eq(2)').children('td').eq(0).text() - Number($('#RowCount option:selected').val());
        var topmax = Number($('#ConfigWordSelectTable tr:eq(2) td:eq(0)').text()) - 1;
        if (topmin > 0) { SelectConfigWord(topmin, topmax); }
        var step = $('#ConfigWordSelectTable tr:last').children('td').eq(0).text();
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
    if (URL.indexOf('ConfigWords') > 0) { SelectConfigWord('', ''); }
});

$('#refFinish').click(function () {   
    if (URL.indexOf('ConfigWords') > 0) {
        var topmin = countSelect - Number($('#RowCount option:selected').val());
        var topmax = countSelect;
        SelectConfigWord(topmin, topmax);
    }
    var ind = Math.ceil(Number(countSelect) / Number($('#RowCount option:selected').val()));
    $('#refCurrent').text(ind);
});

$('#ConfigWordSelectTable').on('click', 'tr', function () {
    if ($(this).closest('tr').index() != 0 && $(this).closest('tr').index() != 1)
    {
        $('.popup-fade').show();
    }
    if (URL.indexOf('ConfigWord') > 0) {
        ConfigWordId = $(this).attr('value');
        $('#txtContainsWord').val($(this).children('td').eq(1).text());
        $('#txtWord').val($(this).children('td').eq(2).text());
        $('#txtTypeWord').val($(this).children('td').eq(3).text());
        $('#txtGenderWord').val($(this).children('td').eq(4).text());
        $('#txtFormWord').val($(this).children('td').eq(5).text());
        $('#txtEmotionLevel').val($(this).children('td').eq(6).text());
        $('#txtConfMixWord').val($(this).children('td').eq(7).text());
        $('#txtMixWord').val($(this).children('td').eq(8).text());
        $('#txtConfNotMixWord').val($(this).children('td').eq(9).text());
        $('#txtNotMixWord').val($(this).children('td').eq(10).text());
        $('#txtNotMixType').val($(this).children('td').eq(11).text());
        $('#txtNotMixGender').val($(this).children('td').eq(12).text());
        $('#txtNotMixForm').val($(this).children('td').eq(13).text());
        $('#txtMixType').val($(this).children('td').eq(14).text());
        $('#txtMixGender').val($(this).children('td').eq(15).text());
        $('#txtMixForm').val($(this).children('td').eq(16).text());
        $('#txtGrouped').val($(this).children('td').eq(17).text());
        $('#txtActiveWordConfig').attr('checked', $(this).children('td').eq(18).val());
    }
});

function UpdateConfigWord() {
    var obj = new Object();
    obj.Id = ConfigWordId;
    obj.strContains = $('#txtContainsWord').val();
    obj.str = $('#txtWord').val();
    obj.TypeWord = $('#txtTypeWord').val();
    obj.GenderForm = $('#txtGenderWord').val();
    obj.FormWord = $('#txtFormWord').val();
    obj.EmotionLevel = $('#txtEmotionLevel').val();
    obj.strMixContains = $('#txtConfMixWord').val();
    obj.Mix = $('#txtMixWord').val();
    obj.strNotMixContains = $('#txtConfNotMixWord').val();
    obj.Not_Mix = $('#txtNotMixWord').val();
    obj.NotMixTypeWord = $('#txtNotMixType').val();
    obj.NotMixGenderForm = $('#txtNotMixGender').val();
    obj.NotMixFormWord = $('#txtNotMixForm').val();
    obj.MixTypeWord = $('#txtMixType').val();
    obj.MixGenderForm = $('#txtMixGender').val();
    obj.MixFormWord = $('#txtMixFor').val();
    obj.Grouped = $('#txtGrouped').val();
    obj.Active = $('#txtActiveWordConfig').val();

    $.ajax({
        type: "POST",
        url: "/Main/UpdateConfigWord",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            var topmin = Number($('#ConfigWordSelectTable tr:eq(2) td:eq(0)').text());
            var topmax = topmin + Number($('#RowCount option:selected').val());
            SelectConfigWord(topmin, topmax - 1);
        }
    });
}

function SelectConfigWord(tm, tx) {
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
        url: "../Main/SelectConfigWord",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "false",
        success: function (data) {
            $('.SelectTableRow').detach();
            $.each(data, function (i, v) {
                $('#ConfigWordSelectTable').append(
                    "<tr class='SelectTableRow' value=" + this.Id + ">"
                    + "<td>" + this.RowNum + "</td>"
                    + "<td>" + this.strContains + "</td>"
                    + "<td>" + this.str + "</td>"
                    + "<td>" + this.TypeWord + "</td>"
                    + "<td>" + this.GenderForm + "</td>"
                    + "<td>" + this.FormWord + "</td>"
                    + "<td>" + this.EmotionLevel + "</td>"
                    + "<td>" + this.strMixContains + "</td>"
                    + "<td>" + this.Mix + "</td>"
                    + "<td>" + this.strNotMixContains + "</td>"
                    + "<td>" + this.Not_Mix + "</td>"
                    + "<td>" + this.NotMixTypeWord + "</td>"
                    + "<td>" + this.NotMixGenderForm + "</td>"
                    + "<td>" + this.NotMixFormWord + "</td>"
                    + "<td>" + this.MixTypeWord + "</td>"
                    + "<td>" + this.MixGenderForm + "</td>"
                    + "<td>" + this.MixFormWord + "</td>"
                    + "<td>" + this.Grouped + "</td>"
                    + "<td><input type='checkbox' disabled = 'disabled' align='center' checked='" + this.Active + "'></td>"
                    + "</tr>"
                );
            });
        }
    });
};

function InsertConfigWord() {
    var obj = new Object();
    obj.strContains = $('#strWordContainsBox').val();
    obj.str = $('#strWordBox').val();
    obj.TypeWord = $('#strWordTypeBox').val();
    obj.GenderForm = $('#strWordGenderBox').val();
    obj.FormWord = $('#strWordFormBox').val();
    obj.EmotionLevel = $('#EmoutionBox').val();
    obj.strMixContains = $('#strMixWordContainsBox').val();
    obj.Mix = $('#strMixWordBox').val();
    obj.strNotMixContains = $('#strNotMixWordContainsBox').val();
    obj.Not_Mix = $('#strNotMixWordBox').val();
    obj.NotMixTypeWord = $('#strNotMixWordTypeBox option:selected').val();
    obj.NotMixGenderForm = $('#strNotMixWordGenderBox option:selected').val();
    obj.NotMixFormWord = $('#strNotMixWordFormBox option:selected').val();
    obj.MixTypeWord = $('#strMixWordTypeBox option:selected').val();
    obj.MixGenderForm = $('#strMixWordGenderBox option:selected').val();
    obj.MixFormWord = $('#strMixWordFormBox option:selected').val();
    obj.Grouped = $('#GroupedBox').val();
    $.ajax({
        type: "POST",
        url: "/Main/InsertConfigWord",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            alert(data);
        }
    });
};

function ConfigWordCount() {
    $.ajax({
        type: "POST",
        url: "/Main/ConfigWordCount",
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