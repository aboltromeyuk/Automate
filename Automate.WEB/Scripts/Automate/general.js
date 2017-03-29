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
        Content.inputMoney($(this).val());        
    });

    $('.imgDrink').click(function () {

        var idDrink = $(this).data('index');
        var numberDrink = $(this).siblings('.drinkNumber').html();
        var priceDrink = $(this).siblings('.drinkPrice').html();
        
        if ($(this).siblings('.drinkNumber').html() !== "0" && Number(Content.getSum()) >= Number(priceDrink)){
            $(this).siblings('.drinkNumber').html(numberDrink - 1);
            Content.inputMoney(-priceDrink);

            Content.selectDrink(idDrink);
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
                Content.inputMoney(0);
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

    inputMoney: function (nominal) {
        $.ajax({
            type: 'POST',
            url: '/Home/InputMoney',
            data: {nominal: nominal},
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

    selectDrink: function (id) {
        $.ajax({
            type: 'POST',
            url: '/Home/SelectDrink',
            async: true,
            data:{ id: id },
            success: function () { },
            error: function () { }
        });
    },

    takeDrinks: function () {
        $.ajax({
            type: 'POST',
            url: '/Home/TakeDrinks',
            async: true,
            success: function (html) {
                $('#cart').html(html);
            },
            error: function () { }
        });
    }
}
