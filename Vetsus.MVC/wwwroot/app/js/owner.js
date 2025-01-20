$(document).ready(function () {
    validateForm();
    validatePetForm();
    initBootstrapTable();
});

const URL_GETALL = '/Owner/GetAll';
const URL_DELETE = '/Owner/Delete';
const URL_ADD = '/Owner/Add';
const URL_UPDATE = '/Owner/Update';
const URL_ADD_PET = '/Pet/Add';

function initBootstrapTable() {
    $('#tblOwners').bootstrapTable();
}

function actionFormatter(id, row, index) {
    const userId = "'" + id + "'";
    return [
        '<a href="javascript:void(0)" title="Nueva mascota" onclick="addPet('+ userId +')"',
        '<i class="fa fa-plus-circle fa-lg"></i>',
        '</a>  ',
        '&nbsp;&nbsp;',
        '<a href="javascript:void(0)" title="Editar dueño" onclick="editOwner(' + userId + ')"',
        '<i class="fa fa-pencil fa-lg"></i>',
        '</a>  ',
        '&nbsp;&nbsp;',
        '<a href="javascript:void(0)" title="Eliminar dueño" onclick="deleteOwner(' + userId + ')">',
        '<i class="fa fa-trash fa-lg"></i>',
        '</a>'
    ].join('')
}

function ajaxRequest(params) {
    console.log('params.data >', params.data)
    $.get(URL_GETALL + '?' + $.param(params.data)).then(function (res) {
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

function closePetModal() {
    $('#petModal').modal('hide');
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

    const URL = $('#hdId').val() == '' ? URL_ADD : URL_UPDATE

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

function detailFormatter(index, row, $detail) {
    $detail.html('Cargando...');

    const url = `/Owner/GetPetsByOwnerId?ownerId=${row.id}`
    $.get(url).then(function (res) {
        const data = res.data
        let records = []
        records = data.map(value => `
                <tr>
                    <td>${value.name}</td>
                    <td>${value.gender}</td>
                    <td>${value.species}</td>
                    <td>${value.birthDate}</td>
                    <td>
                        <a href="javascript:void(0)" title="Ver historia clínica" onclick="getPetDetail('${value.petId}')"
                            <i class="fa fa-eye fa-lg"></i>
                        </a>
                    </td>
                </tr>
             `).join('')

        const template = records.length > 0 ? 
            `<table style="width:100%">
                <tr>
                    <th>Mascota</th>
                    <th>Género</th>
                    <th>Especie</th>
                    <th>Fecha nacimiento</th>
                    <th>Acciones</th>
                </tr>
                ${records}
            </table>` : '<p>No se encontraron registros para mostrar</p>'

        $detail.html(template);
    })
}

function deleteOwner(id) {
    let result = confirm('¿Estás seguro(a) de eliminar este dueño?')
    if (result) {
        $.ajax({
            type: 'DELETE',
            url: URL_DELETE,
            data: { id },
            beforeSend: function () {
                console.log(' beforeSend')
            },
            success: function () {
                alert('Dueño eliminado correctamente')
                $('#tblOwners').bootstrapTable('refresh')
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

function getPetDetail(petId) {
    alert(petId)
}

function addPet() {
    clearPetForm()
    $('#petModal').modal('show');
}

function validatePetForm() {
    $("#petModalForm").validate({
        rules: {
            petName: { required: true },
            petGender: { required: true },
            petSpecie: { required: true },
        },
        messages: {
            petName: "Campo requerido",
            petGender: "Campo requerido",
            petSpecie: "Campo requerido"
        },
        errorClass: "invalid-feedback animated fadeInUp",
        errorElement: "div",
        errorPlacement: function (error, element) {
            if ($(element).attr('id') == 'petGender' || $(element).attr('id') == 'petSpecie') {
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

function addPetAction() {
    if (!$('#petModalForm').valid()) {
        return
    }

    const payload = {
        OwnerId: '',
        Name: $('#petName').val(),
        Gender: $('#petGender option:selected').val(),
        BirthDate: null,
        SpeciesId: $('#petSpecie option:selected').val()
    }

    $.ajax({
        type: 'POST',
        url: URL_ADD_PET,
        data: payload,
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

function clearPetForm() {
    $('#petName').val('')
    $('#petSpecie').val('').change()
    $('#petGender').val('').change()
    $('#petBirthdate').val('')
    $('#petModalForm').validate().resetForm()
}