$(document).ready(function () {
    validateForm();
    initBootstrapTable();
});

function initBootstrapTable() {
    $('#tblOwners').bootstrapTable();
}

function actionFormatter(id, row, index) {
    const userId = "'" + id + "'";
    return [
        '<a href="javascript:void(0)" title="Editar dueño" onclick="editOwner(' + userId + ')"',
        '<i class="fa fa-pencil fa-2x"></i>',
        '</a>  ',
        '&nbsp;&nbsp;',
        '<a href="javascript:void(0)" title="Eliminar dueño" onclick="deleteOwner(' + userId + ')">',
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

function addOwner() {
    $('#lblTitleModal').text('Nuevo registro');
    $('#ownerModal').modal('show');
    //clearFields();
}

function closeModal() {
    $('#ownerModal').modal('hide');
}

function addOrEditOwnerAction() {
    if (!$('#ownerModalForm').valid()) {
        return
    }

    const ownerRequest = {
        Id: $('#hdId').val(),
        Firstname: $('#firstname').val(),
        Lastname: $('#lastname').val(),
        Address: $('#address').val(),
        Phone: $('#phone').val(),
        Email: ''
    }

    const petRequest = {
        Name: $('#name').val(),
        Gender: $('#gender option:selected').val(),
        BirthDate: null,
        SpeciesId: $('#specie option:selected').val()
    }

    const request = {
        OwnerRequest: ownerRequest,
        PetRequest: petRequest
    }

    const URL = $('#hdId').val() == '' ? '/Owner/Add' : '/Owner/Update'

    $.ajax({
        type: 'POST',
        url: URL,
        data: request,
        beforeSend: function () {
            console.log(' beforeSend')
        },
        success: function () {
            closeModal();
            $('#tblOwners').bootstrapTable('refresh');
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

function validateForm() {
    $("#ownerModalForm").validate({
        rules: {
            firstname: { required: true },
            lastname: { required: true },
            phone: { required: true },
            name: { required: true },
            gender: { required: true },
            specie: { required: true },
        },
        messages: {
            firstname: "Campo requerido",
            lastname: "Campo requerido",
            phone: "Campo requerido",
            name: "Campo requerido",
            gender: "Campo requerido",
            specie: "Campo requerido"
        },
        errorClass: "invalid-feedback animated fadeInUp",
        errorElement: "div",
        errorPlacement: function (error, element) {
            if ($(element).attr('id') == 'gender' || $(element).attr('id') == 'specie') {
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