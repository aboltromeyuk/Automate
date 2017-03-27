$(document).ready(function () {
    
    $(".btn").click(function () {
        Content.openWindowRight();
    });

    $('#fond').click(function () {
        Content.closeWindowRight();
    });

    $('#contentRight').on('click', 'img', function () {
        $('#PictureUrl').val($(this).attr('id'));
        $('.attachImg').attr('src', $(this).attr('src'));
    });
});

Content = {
    openWindowRight: function () {
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

    closeWindowRight: function () {

        $('#contentRight').hide('slide', { direction: 'right' }, 500);
        $('#fond').hide('fade', 500);
    },
}
