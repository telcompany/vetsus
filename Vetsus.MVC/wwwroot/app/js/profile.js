$(document).ready(function () {
    getUserInfo();
    validateSecurityForm();
});

function onChangePassword() {
    if (!$('#securityForm').valid()) {
        return
    }

    const request = {
        Id: $('#hdId').val(),
        CurrentPassword: $('#CurrentPassword').val(),
        NewPassword: $('#NewPassword').val(),
    }

    $.ajax({
        type: 'POST',
        url: '/User/ChangePassword',
        data: request,
        beforeSend: function () {
            console.log(' beforeSend')
        },
        success: function () {
            clearFields()
            alert('Contraseña cambiada con éxito')
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

function getUserInfo() {
    const id = $('#hdId').val()
    $.ajax({
        type: 'GET',
        url: '/User/GetById',
        data: { id },
        beforeSend: function () {
            console.log(' beforeSend')
        },
        success: function (response) {
            const data = response.data;
            $('#Email').val(data.email)
            $('#Username').val(data.username)
            $('#Firstname').val(data.firstName)
            $('#Lastname').val(data.lastName)
            $('#Rol').val(data.role)
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

function clearFields() {
    $('#CurrentPassword').val('')
    $('#NewPassword').val('')
    $('#ConfirmPassword').val('')
    $('#securityForm').validate().resetForm()
}

function validateSecurityForm() {
    $("#securityForm").validate({
        rules: {
            CurrentPassword: { required: true },
            NewPassword: { required: true },
            ConfirmPassword: { required: true }
        },
        messages: {
            CurrentPassword: "Campo requerido",
            NewPassword: "Campo requerido",
            ConfirmPassword: "Campo requerido"
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