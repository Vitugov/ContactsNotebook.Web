var dataTable;
$(document).ready(function () {
    loadDataTable();
})


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/getall' },
        "language": { url: '/lib/DataTables/Translations/ru.js' },
        "columns": [
            { data: 'id', width: "5%" },
            { data: 'lastName', width: "15%" },
            { data: 'firstName', width: "15%" },
            { data: 'patronymic', width: "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `
                    <div class="w-100 btn-group m-0 p-0" role="group">
                        <a href = "/${data}" class="btn btn-success mx-2 my-0 p-0">
                            <i class="bi bi-eyeglasses"></i>
                        </a>
                        <a onClick=Delete('/delete/${data}') class="btn btn-danger mx-2 my-0 p-0">
                            <i class="bi bi-trash-fill"></i>
                        </a>
                    </div>
                `},
                width: "10%"
            },
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Вы уверены?",
        text: "Вы не сможете отменить изменения!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Да, удалить запись!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}
