$(document).ready(function () {
    //validateForm();
    initBootstrapTable();
});

function initBootstrapTable() {
    $('#tblVets').bootstrapTable();
}

function ajaxRequest(params) {
    const url = '/Vet/GetAll'

    console.log('params.data >', params.data)
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
    })
}

function actionFormatter(id, row, index) {
    const vetId = "'" + id + "'";
    return [
        '<a href="javascript:void(0)" title="Editar doctor" onclick="editVet(' + vetId + ')"',
        '<i class="fa fa-pencil fa-2x"></i>',
        '</a>  ',
        '&nbsp;&nbsp;',
        '<a href="javascript:void(0)" title="Eliminar doctor" onclick="deleteUser(' + vetId + ')">',
        '<i class="fa fa-trash fa-2x"></i>',
        '</a>'
    ].join('')
}

function addVet() {
    openModal('')
}

function editVet(id) {
    openModal(id)
}

function openModal(id) {
    $.ajax({
        type: "POST",
        url: "/Vet/AddOrUpdate",
        data: { "id": id },
        success: function (response) {
            $("#vetModal").html(response);
            $('#vetModal').modal('show');
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}