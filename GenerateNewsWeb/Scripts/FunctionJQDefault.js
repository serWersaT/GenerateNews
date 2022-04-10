$(document).ready(function () {
    URL = window.location.href;
    GetEmoutionLevel();
    GetFormsWord();
    //GetTypeWord();
    GetTypeWords();
    GetGenderWord();
    GeContains();
    GeFormPhrase();
    //GeThemesNews();
    GetThemesNews();

    $('#RowCount').append('<option>10</option>' +
        '<option>20</option>' +
        '<option>50</option>' +
        '<option>100</option>');
});

function GetEmoutionLevel() {
    $('.EmoutionLevels').append(
        '<option>Выберите</option>'+
        '<option>-10</option>'+
        '<option>-9</option>'+
        '<option>-8</option>'+
        '<option>-7</option>'+
        '<option>-6</option>'+
        '<option>-5</option>'+
        '<option>-4</option>'+
        '<option>-3</option>'+
        '<option>-2</option>'+
        '<option>-1</option>'+
        '<option>0</option>'+
        '<option>1</option>'+
        '<option>2</option>'+
        '<option>3</option>'+
        '<option>4</option>'+
        '<option>5</option>'+
        '<option>6</option>'+
        '<option>7</option>'+
        '<option>8</option>'+
        '<option>9</option>'+
        '<option>10</option>'
    );
}

function GetFormsWord() {
    $('.FormsWord').append(
        '<option>Выберите</option>' +
        '<option>единственная</option>' +
        '<option>множественная</option>' +
        '<option>неопределенная</option>' 
    );
}


//function GetTypeWord() {
//    $('.TypeWord').append(
//        '<option>Выберите</option>' + 
//        '<option>существительное</option>' + 
//        '<option>главное существительное</option>' + 
//        '<option>глагол</option>' +
//        '<option>наречие</option>' +
//        '<option>прилагательное</option>' +
//        '<option>числительное</option>' +
//        '<option>междометие</option>'
//    );
//}

function GetGenderWord() {
    $('.GenderWord').append(
        '<option>Выберите</option>' +
        '<option>женский</option>' +
        '<option>мужской</option>' +
        '<option>средний</option>' +
        '<option>неопределено</option>' 
    );
}

function GeContains() {
    $('.Contains').append(
        '<option>Выберите</option>' +
        '<option>начинается</option>' +
        '<option>содержит</option>' +
        '<option>заканчивается</option>' +
        '<option>равно</option>' 
    );
}

function GeFormPhrase() {
    $('.FormPhrase').append(
        '<option>Выберите</option>' +
        '<option>Утвердительная</option>' +
        '<option>Вопросительная</option>' +
        '<option>Восклицательная</option>'
    );
}

//function GeThemesNews() {
//    $('.ThemesNews').append(
//        '<option>Выберите</option>' +
//        '<option>Политика</option>' +
//        '<option>Культура</option>' +
//        '<option>Спорт</option>' + 
//        '<option>Экономика</option>' + 
//        '<option>Наука</option>'
//    );
//}

function GetThemesNews() {
    var obj = new Object();
    $.ajax({
        type: "POST",
        url: "../Main/GetThemesNews",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "false",
        success: function (data) {
            $('.ThemesNews').empty();
            $.each(data, function (i, v) {
                $('.ThemesNews').append(
                    "<option>" + this + "</option>"
                );
            });
        }
    });
};

function GetTypeWords() {
    var obj = new Object();
    $.ajax({
        type: "POST",
        url: "../Main/GetTypeWords",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: "false",
        success: function (data) {
            $('.TypeWord').empty();
            $.each(data, function (i, v) {
                $('.TypeWord').append(
                    "<option>" + this + "</option>"
                );
            });
        }
    });
};

