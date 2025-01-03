﻿$(document).ready(function () {
    validateForm();
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
        '<a href="javascript:void(0)" title="Editar usuario" onclick="editUser(' + userId + ')"',
        '<i class="fa fa-pencil fa-2x"></i>',
        '</a>  ',
        '&nbsp;&nbsp;',
        '<a href="javascript:void(0)" title="Eliminar usuario" onclick="deleteUser(' + userId + ')">',
        '<i class="fa fa-trash fa-2x"></i>',
        '</a>'
    ].join('')
}

function addUser() {
    $('#lblTitleModal').text('Nuevo usuario');
    $('#userModal').modal('show');
    $('#fgPwd').show();
    clearFields();
}

function addOrEditUserAction() {
    if (!$('#userModalForm').valid()) {
        return
    }

    const request = {
        Id: $('#hdUserId').val(),
        Username: $('#username').val(),
        Firstname: $('#firstname').val(),
        Lastname: $('#lastname').val(),
        Email: $('#email').val(),
        Password: $('#password').val(),
        Role: $('#ddRole option:selected').val()
    }

    const URL = $('#hdUserId').val() == '' ? '/User/Add' : '/User/Update'

    $.ajax({
        type: 'POST',
        url: URL,
        data: request,
        beforeSend: function () {
            console.log(' beforeSend')
        },
        success: function () {
            closeModal();
            $('#tblUsers').bootstrapTable('refresh');
        },
        error: function (response) {
            const data = response.responseJSON;
            alert(data.Message)
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
    $('#fgPwd').hide();
    clearFields()

    $.ajax({
        type: 'GET',
        url: '/User/GetById',
        data: { id },
        beforeSend: function () {
            console.log(' beforeSend')
        },
        success: function (response) {
            const data = response.data;
            $('#hdUserId').val(data.id)
            $('#firstname').val(data.firstName)
            $('#lastname').val(data.lastName)
            $('#username').val(data.username)
            $('#email').val(data.email)
            $('#ddRole').val(data.role).change()
        },
        error: function (response) {
            const data = response.responseJSON;
            alert(data.Message)
        },
        complete: function (response) {
            //Hide loader
            console.log(' complete - response >', response)
        }
    });
}

function deleteUser(id) {
    let result = confirm('¿Estás seguro(a) de eliminar este usuario?')
    if (result) {
        $.ajax({
            type: 'DELETE',
            url: '/User/Delete',
            data: { id },
            beforeSend: function () {
                console.log(' beforeSend')
            },
            success: function () {
                alert('Usuario eliminado correctamente')
                $('#tblUsers').bootstrapTable('refresh')
            },
            error: function (response) {
                const data = response.responseJSON;
                alert(data.Message)
            },
            complete: function (response) {
                //Hide loader
                console.log(' complete - response >', response)
            }
        });
    }
}

function clearFields() {
    $('#hdUserId').val('')
    $('#firstname').val('')
    $('#lastname').val('')
    $('#username').val('')
    $('#email').val('')
    $('#password').val('')
    $('#ddRole').val('').change()
    $('#userModalForm').validate().resetForm()
}

function validateForm() {
    $("#userModalForm").validate({
        rules: {
            email: { required: true, email: true },
            username: { required: true },
            firstname: { required: true },
            lastname: { required: true },
            ddRole: { required: true },
            password: {
                required: function () {
                    return $('#hdUserId').val() == '';
                }
            }
        },
        messages: {
            email: {
                required: "Campo requerido",
                email: "Formato de email incorrecto"
            },
            username: "Campo requerido",
            firstname: "Campo requerido",
            lastname: "Campo requerido",
            ddRole: "Campo requerido",
            password: "Campo requerido",
        },
        errorClass: "invalid-feedback animated fadeInUp",
        errorElement: "div",
        errorPlacement: function (error, element) {
            if ($(element).attr('id') == 'ddRole') {
                $(element).parent().parent().append(error)
            } else {
                $(element).parent().append(error)
            }
        },
        highlight: function (element) {
            $(element).closest(".form-group").removeClass("is-invalid").addClass("is-invalid")
        },
    });
}