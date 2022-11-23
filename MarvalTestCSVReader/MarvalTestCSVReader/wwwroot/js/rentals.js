
$(document).ready(function (event) {
    var table = $("#rentalsDatatable").DataTable({
        
        ajax: {
            "url": "/api/rentals",
            "datatype": "json"
        },
        "processing": true,
        "serverSide": true,
        "filter": true,
        "columns": [
            {
                "data": "id", "name": "Id", "autoWidth": true

            },
            {
                "data": "customerUsername", "name": "CustomerUsername", "autoWidth": true
            },
            {
                "data": "bookName", "name": "Book Name", "autoWidth": true
            },
            {
                "data": "dateRented", "name": "Date Rented", "autoWidth": true
            },
            {
                "data": "dueDate", "name": "Due Date", "autoWidth": true
            },
            {
                "data": "id",
                "render": function (data) {
                    return "<button class='btn btn-info js-return' data-rental-id=" + data + ">Return</button>";
                }
            }
        ],

    });



    $("#rentalsDatatable").on("click", ".js-return", function () {
        event.preventDefault;
        var button = $(this);
        debugger
        swal({
            title: 'You about to return this book.',
            text: "Click OK to proceed.",
            icon: 'info',
            buttons: true,
        }).then((isConfirmed) => {
            debugger;
            if (isConfirmed) {
                debugger
                $.ajax({
                    "url": "/api/rentals/" + button.attr("data-rental-id"),
                    "method": "DELETE",
                    "datatype": "json",
                    "success": function () {
                       table.row(button.parents("tr")).remove().draw();
                        swal(
                            'Book returned!',
                            'The record has been updated.',
                            'success'
                        );
                    },
                    error: function (error) {
                        swal(
                            'Book not returned!',
                            'The record couldnt be updated.' + error.responseText,
                            'error',
                        );
                    }

                });
            } else {
                swal("Cancelled", "You have Cancelled Returning A Book.", "error");
            }
        });
    });

    $("#booksDatatable").on("click", ".js-add", function () {
        event.preventDefault();
        var button = $(this);
        data = { bookId: button.attr("data-book-id") };
        debugger
        swal({
            title: 'Do you want to rent this book?!',
            text: "Click the button to continue.",
            icon: 'info',
            buttons: true,
        }).then((isConfirmed) => {
            debugger;
            if (isConfirmed) {
                debugger
                $.ajax({
                    "url": "/api/rentals/" + button.attr("data-book-id"),
                    "method": "POST",
                    "datatype": "json",
                    "data": data,
                    "success": function () {
                        debugger
                        (".js-add").attr('disabled', true);
                        swal(
                            'Book rented!',
                            'Book has been added to your rentals.',
                            'success'
                        );
                    },
                    error: function (error) {
                        debugger
                        swal(
                            'Record not added!',
                            'The record couldnt be added.' + error.responseText,
                            'error',
                        );
                    }

                });
            } else {
                swal("Cancelled", "You have Cancelled Book Rental!", "error");
            }
        });
    });

});


