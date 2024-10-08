$(document).ready(function () {
    //validateForm();
    initBootstrapTable();
});

function initBootstrapTable() {
    $('#tblOwners').bootstrapTable();
}

function actionFormatter(id, row, index) {
    const userId = "'" + id + "'";
    return [
        '<a href="javascript:void(0)" title="Editar due�o" onclick="editOwner(' + userId + ')"',
        '<i class="fa fa-pencil fa-2x"></i>',
        '</a>  ',
        '&nbsp;&nbsp;',
        '<a href="javascript:void(0)" title="Eliminar due�o" onclick="deleteOwner(' + userId + ')">',
        '<i class="fa fa-trash fa-2x"></i>',
        '</a>'
    ].join('')
}

function ajaxRequest(params) {
    const url = '/Owner/GetAll'

    console.log('params.data >', params.data)
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
    })
}