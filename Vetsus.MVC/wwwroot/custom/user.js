$(document).ready(function () {
    initBootstrapTable();
});

function initBootstrapTable() {
    $('#tblUsers').bootstrapTable();
}

function ajaxRequest(params) {
    const url = '/User/GetAll'

    console.log('params.data >', params.data)
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
    })
}

function actionFormatter(id, row, index) {
    if (id == USER_ID) {
        return '-';
    }

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
    $('#userModal').modal('show');
}

function addUserAction() {
    let request = {
        Username: $('#username').val(),
        Email: $('#email').val(),
        Password: $('#password').val(),
        Role: $('#ddRole option:selected').val()
    }

    $.ajax({
        type: 'POST',
        url: '/User/Add',
        data: request,
        beforeSend: function () {
            console.log(' beforeSend')
        },
        success: function (response) {
            console.log(' success - response >', response)
            closeModal();
            $('#tblUsers').bootstrapTable('refresh');
        },
        failure: function (response) {
            console.log(' failure - response >', response)
        },
        error: function (response) {
            console.log(' error - response >', response)
        },
        complete: function (response) {
            //Hide loader
            console.log(' complete - response >', response)
        }
    });
}

function closeModal() {
    $('#userModal').modal('hide');
}

function editUser(id) {
    $('#lblTitleModal').text('Editar usuario')
    $('#userModal').modal('show'); 
    console.log('id >', id);
}

function deleteUser(id) {
    console.log('id >', id);
}