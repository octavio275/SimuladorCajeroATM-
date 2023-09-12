$(document).ready(function () {
    $('.table_teclado tr td').click(function () {
        var number = $(this).text();

        if (number == 'Limpiar') {
            $('#campo').val('');
        }
        else {
            $('#campo').val($('#campo').val() + number).focus();
        }

    });
});
$('#btn-exit').click(function () {
    document.cookie = 'nro_tarjeta=;expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
});