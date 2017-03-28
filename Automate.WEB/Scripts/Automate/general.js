$(document).ready(function () {
    
    $('#addImg').click(function () {
        Content.openWindowRightAdmin();
    });

    $('#fond').click(function () {
        Content.closeWindowRight();
    });

    $('#contentRight').on('click', 'img', function () {
        $('#PictureId').val($(this).attr('id'));
        $('.attachImg').attr('src', $(this).attr('src'));
    });

    $('#inputMoney').click(function () {
        Content.openWindowRightUser();
    });

    $('#contentRight').on('click', '.btn', function () {
        Content.inputMoney($(this).data('index'), $(this).val(), $(this).data('number'));
        $(this).data('number', Number($(this).data('number')) + 1);
    });

    $('.imgDrink').click(function () {

        var idDrink = $(this).data('index');
        var nameDrink=$(this).siblings('.drinkName').html();
        var numberDrink = $(this).siblings('.drinkNumber').html();
        var priceDrink = $(this).siblings('.drinkPrice').html();
        var pictureId = $(this).data('picid');

        if ($(this).siblings('.drinkNumber').html() !== "0" && Number(Content.getSum()) >= Number(priceDrink)){
            $(this).siblings('.drinkNumber').html(numberDrink - 1);
            Content.inputMoney(0, -priceDrink, 0);

            Content.selectDrink(idDrink, nameDrink, pictureId, numberDrink, priceDrink);
        }
    });

    $('#getDrinks').click(function () {
        Content.takeDrinks();
    });
});

Content = {
    openWindowRightAdmin: function () {
        $.ajax({
            type: 'GET',
            url: '/Admin/Pictures',
            async: true,
            success: function (html) {
                $('#contentRight').html(html);
                $('#contentRight').show('slide', { direction: 'right' }, 500);
                $('#fond').show('fade', 500);
            },
            error: function () { }
        });
    },

    openWindowRightUser: function () {
        $.ajax({
            type: 'GET',
            url: '/Home/InputMoney',
            async: true,
            success: function (html) {
                $('#contentRight').html(html);
                Content.inputMoney(0, 0, 0);
                $('#contentRight').show('slide', { direction: 'right' }, 500);
                $('#fond').show('fade', 500);
            },
            error: function () { }
        });
    },

    closeWindowRight: function () {

        $('#contentRight').hide('slide', { direction: 'right' }, 500);
        $('#fond').hide('fade', 500);
    },

    inputMoney: function (id, nominal, number) {
        $.ajax({
            type: 'POST',
            url: '/Home/InputMoney',
            data: {id: id, nominal: nominal, number: number, blocked: false},
            async: true,
            success: function (html) {
                $('#sum').html(html);
            },
            error: function () { }
        });
    },

    getSum: function () {
        var sum;
        $.ajax({
            type: 'POST',
            url: '/Home/InputMoney',
            data: { id: 0, nominal: 0, number: 0, blocked: false  },
            async:false,
            success: function (html) {
                sum = html;
            },
            error: function () { }
        });
        return sum;
    },

    selectDrink: function (id, name, pictureId, number, price) {
        $.ajax({
            type: 'POST',
            url: '/Home/SelectDrink',
            async: true,
            data:{ id: id, name: name, pictureId: pictureId, number: number, price: price },
            success: function () { },
            error: function () { }
        });
    },

    takeDrinks: function () {
        $.ajax({
            type: 'POST',
            url: '/Home/TakeDrinks',
            async: true,
            success: function () { },
            error: function () { }
        });
    }
}
