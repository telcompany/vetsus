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