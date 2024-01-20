document.addEventListener("DOMContentLoaded", function () {
    var dangerAlertDiv = document.querySelector(".alert.alert-danger");
    var successAlertDiv = document.querySelector(".alert.alert-success");

    if (dangerAlertDiv) {
        dangerAlertDiv.style.display = "block";
        setTimeout(function () {
            dangerAlertDiv.style.display = "none";
        }, 8000);
    }

    if (successAlertDiv) {
        successAlertDiv.style.display = "block";
        setTimeout(function () {
            successAlertDiv.style.display = "none";
            //Limpiar el formulario
            LimpiarCamposFormulario();
        }, 8000);
    }
});

//Funcion para limpiar el formulario
function LimpiarCamposFormulario() {
    $("#Nombre_Completo").val("");
    $("#Asunto").val("");
    $("#Mensaje").val("");
    $("#Correo").val("");
}