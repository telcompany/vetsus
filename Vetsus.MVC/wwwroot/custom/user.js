$(document).ready(function () {
    initBootstrapTable();
});

function initBootstrapTable() {
    $('#tblUsers').bootstrapTable()
}

function ajaxRequest(params) {
    const url = '/User/GetAll'

    console.log('params.data >', params.data)
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
    })
}

function actionFormatter(id, row, index) {
    var userId = "'" + id + "'";
    return [
        '<a href="javascript:void(0)" title="Editar" onclick="editUser(' + userId + ')"',
        '<i class="fa fa-pencil fa-2x"></i>',
        '</a>  ',
        '&nbsp;&nbsp;',
        '<a href="javascript:void(0)" title="Eliminar" onclick="deleteUser(' + userId + ')">',
        '<i class="fa fa-trash fa-2x"></i>',
        '</a>'
    ].join('')
}

function addUser() {
    $('#lblTitleModal').text('Nuevo usuario');
    $('#addUserModal').modal('show');
}

function closeModal() {
    $('#addUserModal').modal('hide');
}

function editUser(id) {
    $('#lblTitleModal').text('Editar usuario')
    $('#addUserModal').modal('show'); 
    console.log('id >', id);
}

function deleteUser(id) {
    console.log('id >', id);
}