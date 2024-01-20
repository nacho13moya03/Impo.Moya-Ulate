//Sirve para mostrar los mensajes de exito y error con tiempo limitado al momento de registrar un cliente
document.addEventListener("DOMContentLoaded", function () {
    var dangerAlertDiv = document.querySelector(".alert.alert-danger");
    var successAlertDiv = document.querySelector(".alert.alert-success");

    if (dangerAlertDiv) {
        dangerAlertDiv.style.display = "block";
        setTimeout(function () {
            dangerAlertDiv.style.display = "none";
        }, 3000);
    }

    if (successAlertDiv) {
        successAlertDiv.style.display = "block";
        setTimeout(function () {
            successAlertDiv.style.display = "none";
            //Limpiar el formulario
            LimpiarCamposFormulario();
        }, 3000);
    }
});

//Funcion para limpiar el formulario
function LimpiarCamposFormulario() {
    $("#Ced_Cliente").val("");
    $("#Nombre_Cliente").val("");
    $("#Apellido_Cliente").val("");
    $("#Correo_Cliente").val("");
    $("#Contrasenna_Cliente").val("");
}

//Funcion para obtener el nombre y primer apellido basado en la cedula
function ConsultarNombreCliente() {
    let identificacion = $("#Ced_Cliente").val();

    if (identificacion.length > 0) {
        $.ajax({
            url: 'https://apis.gometa.org/cedulas/' + identificacion,
            type: "GET",
            success: function (data) {
                console.log(data);

                if (data && data.results && data.results.length > 0) {
                    // Tomar el elemento del array
                    let resultado = data.results[0];

                    // Asigna los datos correspondientes y les da formato
                    let nombre = capitalizarPrimerLetra(resultado.firstname.toLowerCase()) || "";
                    let apellido = capitalizarPrimerLetra(resultado.lastname1.toLowerCase()) || "";

                    // Asignar los valores a los campos correspondientes
                    $("#Nombre_Cliente").val(nombre);
                    $("#Apellido_Cliente").val(apellido);
                } else {
                    console.error('La respuesta no contiene los datos esperados o no hay resultados.');
                }
            },
            error: function (error) {
                console.error('Error en la solicitud AJAX:', error);
            }
        });
    } else {
        $("#Nombre_Cliente").val("");
        $("#Apellido_Cliente").val("");
    }
}

// Función para dar formato de primera letra en mayuscula y el resto en minuscula 
function capitalizarPrimerLetra(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}
