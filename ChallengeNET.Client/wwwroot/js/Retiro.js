$('#btn-aceptar').on('click', {}, function () {
    let campoValue = $('#campo').val();
    if (campoValue === '') {
        alert("Por favor, ingresa el monto antes de continuar.")
        return;
    }
    $.post("/Operacion/ValidateMontoRetiro", { monto_retiro: campoValue }
    ).done(function (response) {
        $.post("/Operacion/RetiroDinero", { monto_retiro: campoValue }
        ).done(function (response) {
            window.location.href = "/Operacion/RetiroExitoso";
        }).fail(function (xhr, status, error) {
            window.location.href = "/Home/Error";
        });
    }).fail(function (jqXHR, textStatus) {
        alert('Ha excedido el saldo que tiene en su cuenta, su saldo es: $' + jqXHR.responseText);
        return;
    });


});