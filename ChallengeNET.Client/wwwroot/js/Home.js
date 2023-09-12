$(document).ready(function () {
    $('#btn-exit').hide();
    $('.table_teclado tr td').on('click', function () {
        var input = $('#campo');
        var num = input.val().replace(/\s/g, '').replace(/\D/g, ''); // Eliminar espacios y caracteres que no sean dígitos
        var formattedNum = '';
        for (var i = 0; i < num.length; i++) {
            if (i > 0 && i % 4 === 0) {
                formattedNum += '-';
            }
            formattedNum += num[i];
        }
        input.val(formattedNum);
    });
});
$('#btn-submit').on('click', {}, function () {
    let campoValue = $('#campo').val();
    if (campoValue === '') {
        alert("Por favor, ingresa los dígitos antes de continuar.")
        return;
    }

    $.post("Home/ValidateTarjeta", { nro_tarjeta: campoValue }
    ).done(function (response) {
        window.location.href = "/Home/IngresaPinView";
    }).fail(function (xhr, status, error) {
        window.location.href = "/Home/Error";
    });
});