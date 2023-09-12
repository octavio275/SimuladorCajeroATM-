let intentos = 0;
let intentosMax = 4;
$('#btn-aceptar').on('click', {}, function () {
    let campoValue = $('#campo').val();
    if (campoValue === '') {
        alert("Por favor, ingresa el pin antes de continuar.")
        return;
    }
    if (intentos === 4) {
        $.post("/Home/BloquearTarjeta")
            .done(function (response) {
                window.location.href = "/Home/TarjetaBloqueada";
            }).fail(function (xhr, status, error) {
                window.location.href = "/Home/Error";
            });
        return;
    }

    $.post("/Home/ValidateCVV", { pin_tarjeta: campoValue }
    ).done(function (response) {
        window.location.href = "/Operacion/Operaciones";
    }).fail(function (xhr, status, error) {
        alert('El código ingresado es incorrecto, intentos restantes:' + (intentosMax - intentos));
        intentos++;
    });
});