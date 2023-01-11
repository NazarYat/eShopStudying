var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    } 
    else if (url.includes("pending")) {
        loadDataTable("pending");
    }
    else if (url.includes("completed")) {
        loadDataTable("completed");
    }
    else if (url.includes("approved")) {
        loadDataTable("approved");
    }
    else {
        loadDataTable("all");
    }
});

function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        ajax: {
            url: "/Admin/Order/GetAll?status=" + status
        },
        columns: [
            { data: "id", width: "5%"},
            { "data": "name", "width": "10%"},
            { "data": "phoneNumber", "width": "10%"},
            { "data": "applicationUser.email", "width": "10%"},
            { "data": "orderStatus", "width": "10%"},
            { "data": "orderTotal", "width": "6%"},
            { 
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="w-100 btn-group" role="group">
                        <a href="/Admin/Order/Details/?orderId=${data}" class="btn btn-primary btn-sm">Details</a>
                    </div>
                    `;
                },
                "width": "6%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
      }).then((result) => {
        if (result.isConfirmed) {
          $.ajax({
            url: url,
            type: "DELETE",
            success: function (data) {
                if (data.success) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
                else toastr.error(data.message);
            }
          })
        }
      });
}