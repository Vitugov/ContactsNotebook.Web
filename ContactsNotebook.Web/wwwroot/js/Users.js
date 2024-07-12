var dataTable;
$(document).ready(function () {
    loadDataTable();
})


//function loadDataTable() {
//    dataTable = $('#tblData').DataTable({
//        "ajax": { url: '/getall' },
//        "language": { url: '/lib/DataTables/Translations/ru.js' },
//        "columns": [
//            { data: 'email', width: "50%" },
//            { data: 'isadmin', width: "15%" },
//            {
//                data: 'email',
//                "render": function (data) {
//                    return `
//                        <div class="w-100 btn-group m-0 p-0" role="group">
//                            <a onClick="Delete('/users/delete/${data}')" class="btn btn-danger mx-2 my-0 p-0">
//                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3" viewBox="0 0 16 16">
//                                <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47M8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5"/>
//                            </svg>
//                        </a>
//                        </div>
//                    `;
//                },
//                width: "35%"
//            },
//        ]
//    });
//}

function loadDataTable() {
    dataTable = $('#users').DataTable({
        "ajax": { url: '/users/getall' },
        "language": { url: '/lib/DataTables/Translations/ru.js' },
        "columns": [
            { data: 'email', width: "40%" },
            {
                data: 'isAdmin',
                width: "40%",
                "render": function (data) {
                    return data ? 'Да' : 'Нет';
                }
            },
            {
                data: 'id',
                "render": function (data) {
                    return `
                        <div class="w-100 btn-group m-0 p-0" role="group">
                            <a onClick="Delete('/users/delete/${data}')" class="btn btn-danger mx-2 my-0 p-0">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-trash3" viewBox="0 0 16 16">
                                    <path d="M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47M8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5"/>
                                </svg>
                            </a>
                        </div>
                    `;
                },
                width: "20%"
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
