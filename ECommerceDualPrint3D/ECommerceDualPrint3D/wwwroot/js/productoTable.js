$(document).ready(function () {
    $("#productosTables").DataTable({
        language: {
            url: "//cdn.datatables.net/plug-ins/2.3.4/i18n/es-ES.json"// Traduce en español
        },
        pageLength: 10, // Número de filas por pagínas
        ordering: true, // Habilitar ordenamiento
        searching: true, // Habilitar búsqueda
        columnDefs: [
            { orderable: false, targets: [3, 8] } // Deshabilitar ordenamiento en las columnas de imagen y acciones
        ]
    });
});