﻿var dataTable;

$(document).ready(function () {
    cargarDatatable();
});

function cargarDatatable() {
    dataTable = $("#tblEtiquetas").DataTable({
        "ajax": {
            "url": "/admin/etiquetas/GetEtiquetas",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "idEtiqueta", "width": "10%" },
            { "data": "nombreEtiqueta", "width": "30%" },
            { "data": "fechaCreacion", "width": "30%" },
            {
                "data": "idEtiqueta",
                "render": function (data) {
                    return ` <div class="text-center">
                                <a href="/admin/etiquetas/editar/${data}" class="btn btn-success text-white" style="cursor:pointer; width:100px">
                                <i class="bi bi-pencil-square"></i> Editar
                                </a>                                
                                &nbsp;
                                <a onclick=Delete("/admin/etiquetas/BorrarEtiqueta/${data}") class="btn btn-danger text-white" style="cursor:pointer; width:100px;">
                                    <i class="bi bi-x-square"></i> Borrar
                                </a>
                               </div>
                           `;
                }, "width": "30%"
            }
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
