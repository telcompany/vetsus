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

function closeModal() {
    $('#vetModal').modal('hide');
}

function addOrEditVetAction() {
    var formData = $('#vetModalForm').serializeArray()

    const id = formData.find(x => x.name == 'Id').value;
    const URL = id == '' ? '/Vet/Add' : '/Vet/Update'
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