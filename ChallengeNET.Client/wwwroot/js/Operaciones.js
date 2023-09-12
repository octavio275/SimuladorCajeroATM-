$(document).ready(function () {

    $.post("/Operacion/ShowOperaciones")
        .done(function (response) {
        window.location.href = "/Operacion/Operaciones";
    }).fail(function (xhr, status, error) {
        alert('El código ingresado es incorrecto, intentos restantes:' + (intentosMax - intentos));
        intentos++;
    });
});
