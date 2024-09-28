$(document).ready(function () {
    validateVetForm();
    initBootstrapTable();
});

const URL_ADD = '/Vet/Add';
const URL_UPDATE = '/Vet/Update';
const URL_DELETE = '/Vet/Delete';
const URL_GETALL = '/Vet/GetAll';
const URL_GETBYID = '/Vet/GetById';
const URL_GET_MODAL = '/Vet/AddOrUpdate';

function initBootstrapTable() {
    $('#tblVets').bootstrapTable();
}

function ajaxRequest(params) {
    const url = URL_GETALL

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
        '<a href="javascript:void(0)" title="Eliminar doctor" onclick="deleteVet(' + vetId + ')">',
        '<i class="fa fa-trash fa-2x"></i>',
        '</a>'
    ].join('')
}

function addVet() {
    clearFields();
    $('#lblTitleModal').text('Nuevo doctor');
    $('#vetModal').modal('show');
}

function editVet(id) {
    $('#lblTitleModal').text('Editar doctor')
    $('#vetModal').modal('show');    
    clearFields()

    $.ajax({
        type: 'GET',
        url: URL_GETBYID,
        data: { id },
        beforeSend: function () {
            console.log(' beforeSend')
        },
        success: function (response) {
            const data = response.data;
            $('#Id').val(data.id)
            $('#Firstname').val(data.firstName)
            $('#Lastname').val(data.lastName)
            $('#Phone').val(data.phone)
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

function openModal(id) {
    $.ajax({
        type: "POST",
        url: URL_GET_MODAL,
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

function closeModal() {
    $('#vetModal').modal('hide');
}

function addOrEditVetAction() {
    if (!$('#vetModalForm').valid()) {
        return
    }

    var formData = $('#vetModalForm').serializeArray()

    const id = formData.find(x => x.name == 'Id').value;
    const URL = id == '' ? URL_ADD : URL_UPDATE
    const request = onGenerateJson(formData);

    $.ajax({
        type: 'POST',
        url: URL,
        data: request,
        beforeSend: function () {
            console.log(' beforeSend')
        },
        success: function () {
            closeModal();
            $('#tblVets').bootstrapTable('refresh');
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

function onGenerateJson(formData) {
    var jsonData = {};
    formData.forEach(function (item) {
        var columnName = item.name;
        jsonData[columnName] = item.value;
    });

    return jsonData;
}

function deleteVet(id) {
    let result = confirm('¿Estás seguro(a) de eliminar este doctor?')
    if (result) {
        $.ajax({
            type: 'DELETE',
            url: URL_DELETE,
            data: { id },
            beforeSend: function () {
                console.log(' beforeSend')
            },
            success: function () {
                alert('Doctor eliminado correctamente')
                $('#tblVets').bootstrapTable('refresh')
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

function validateVetForm() {
    $('#vetModalForm').validate({
        rules: {
            Firstname: { required: true },
            Lastname: { required: true },
            Phone: { required: true },
        },
        messages: {
            Firstname: "Campo requerido",
            Lastname: "Campo requerido",
            Phone: "Campo requerido",
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

function clearFields() {
    $('#Id').val('')
    $('#Firstname').val('')
    $('#Lastname').val('')
    $('#Phone').val('')
    $('#vetModalForm').validate().resetForm()
}