﻿var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblArticuloEtiquetas").DataTable({
        "ajax": {
            "url": "/admin/Etiquetas/GetArticulosEtiquetas",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idArticulo", "width": "30%" },
            { "data": "titulo", "width": "30%" },
            { "data": "etiqueta.[0].nombreEtiqueta", "width": "30%" },            
            ]
        });
}

function Delete(url) {
    swal({
        title: "Esta seguro de borrar?",
        text: "Este contenido no se puede recuperar!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Si, borrar!",
        closeOnconfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    dataTable.ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    });
}
