$(document).ready(function () {

});

function GenerateNewPhrases() {
    var obj = new Object;
    obj.flagmix = $('#checkOnlyMixPhrase').val();
    $.ajax({
        type: "POST",
        url: "/Main/GenerateNewPhrases",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $('#txtGenerateNewPhrase').val(data);
        }
    });
}

function GenerateNewNews() {
    var obj = new Object;
    obj.ThemeNews = $('#strGenerateThemesNewsBox').val();
    $.ajax({
        type: "POST",
        url: "/Main/GenerateNewNews",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $('#txtGenerateNewNews').val(data);
        }
    });
}

function LearnText() {
    var obj = new Object();
    obj.News = $('#txtLearn').val();
    obj.Theme = $('#strThemesNewsBox option:selected').val();
    obj.EmotionLevel = $('#strEmotionLevelBox option:selected').val();
    $.ajax({
        type: "POST",
        url: "/Main/LearnText",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $('#txtGenerateNewNews').val(data);
        }
    });
}

function GetPhrase() {
    var obj = new Object();

    if ($('#checkOnlyMixPhrase').is(':checked')) {
        obj.flagmix = 'on';
    } else {
        obj.flagmix = 'off';
    }
    $.ajax({
        type: "POST",
        url: "/Main/GeneratePhrase",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,
        success: function (data) {
            $('#txtGenerateNewPhrase').val(data);
        }
    });  
}