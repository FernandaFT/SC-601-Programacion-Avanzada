function AgregarAlCarrito(button) {
  const servicioId = button.getAttribute('data-servicio');
  const select = document.querySelector(`select[data-servicio="${servicioId}"]`);
  const selectedOption = select.options[select.selectedIndex];

  if (!selectedOption.value) {
    alert("Por favor selecciona un horario");
    return;
  }

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
      alert(response);
      location.reload();
    }
  });

}