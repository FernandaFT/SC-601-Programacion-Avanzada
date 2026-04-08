function AgregarAlCarrito(button) {
    const servicioId = button.getAttribute('data-servicio');
    const select = document.querySelector(`select[data-servicio="${servicioId}"]`);
    const selectedOption = select.options[select.selectedIndex];
    const horarioId = selectedOption.value;

    $.ajax({
        url: '/Carrito/GuardarServicioCarrito',
        type: 'POST',
        dataType: 'json',
        data: {
            "servicioId": servicioId,
            "horarioId": horarioId
        },
        success: function (response) {

            let icono = "success";
            if (response.codigo == -1) {
                icono = "warning"
            }

            Swal.fire({
                icon: icono,
                title: "Información",
                text: response.mensaje,
                showDenyButton: false,
                showCancelButton: false,
                confirmButtonText: "Ok",
                allowOutsideClick: false,
                allowEscapeKey: false
            }).then((result) => {
                if (result.isConfirmed) {

                    if (response.codigo == 0) {
                        location.reload();
                    }

                }
            });

        }
    });

}