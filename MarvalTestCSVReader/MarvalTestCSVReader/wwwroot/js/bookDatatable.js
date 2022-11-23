
$(function () {
    $('#uploadFile').on('click', function (evt) {
       // evt.preventDefault();
        debugger;
        var data = new FormData();
        var files = $("#fileUpload").get(0).files;
        if (files.length > 0)
        {
            data.append("file", files[0]);
        }
        $.ajax({
            "url": "/api/Persons/upload",
            "data": data,
            "type":"text/csv",
            "contentType": "multipart/form-data",
            "processData": false,
            "success": function () {
                swal('Uploaded!','File successfully uploaded!', 'success');
            },
            "error": function (error) {
                swal(
                    'Record not deleted!',
                    'The record couldnt be deleted.' + error.responseText,
                    'error',
                );
            }
        });
    });
 
});

$(document).ready(function () {
    var table = $("#personsDatatable").DataTable({
        ajax: {
            "url": "/api/persons",
            "datatype": "json"
        },
        "processing": true,
        "serverSide": true,
        "filter": true,
        "columns": [
            {
                "data": "identity", "name": "Id", "autoWidth": true
            },
            {
                "data": "firstName", "name": "FName", "autoWidth": true
            },
            {
                "data": "surname", "name": "SName", "autoWidth": true
            },
            {
                "data": "age", "name": "Age", "autoWidth": true
            },
            {
                "data": "sex", "name": "Sex", "autoWidth": true
            },
            {
                "data": "mobile", "name": "Mobile", "autoWidth": true
            },
            {
                "data": "active", "name": "Active", "autoWidth": true
            },
            {
                "data": "isValid", "name": "Valid", "autoWidth": true
            },
            {
                "data": "identity",
                "render": function (data) {
                    return "<button class='btn btn-warning js-edit' data-person-id=" + data + ">Edit</button>";
                }
            },
            {
                "data": "identity",
                "render": function (data) {
                    return "<button class='btn btn-danger js-delete' data-person-id=" + data + ">Delete</button>";
                }
            }
        ],
       
    });


    $("#personsDatatable").on("click", ".js-delete", function () {
        event.preventDefault();
        var button = $(this);
        debugger
        swal({
            title: 'Are you sure you want to delete this record?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            buttons: true,
            /*dangerMode: true,*/
        }).then((isConfirmed) => {
            debugger;
                if (isConfirmed) {
                    debugger
                    $.ajax({
                        "url": "/api/persons/" + button.attr("data-person-id"),
                        "method": "DELETE",
                        "datatype": "json",
                        "success": function () {
                            table.row(button.parents("tr")).remove().draw();
                            swal(
                                'Deleted!', 
                                'The record has been deleted.',
                                'success'
                            );
                        },
                        error: function (error) {
                            swal(
                                'Record not deleted!',
                                'The record couldnt be deleted.' + error.responseText,
                                'error',
                            );
                        }
                    });
                } else {
               swal("Cancelled", "You have Cancelled Deletion!", "error");
           }
        });
    });

    $("#personsDatatable").on("click", ".js-edit", function () {
        //event.preventDefault();
        var button = $(this);
        var personId = button.attr("data-person-id");
        var email = "";
        var person;
        debugger

       
        window.location.href = '/People/Edit/' + personId;
        

    });

});


    