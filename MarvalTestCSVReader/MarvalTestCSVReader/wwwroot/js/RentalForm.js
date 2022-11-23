
$(document).ready(function () {
    var vm = {
        bookIds: []
    };

    var users = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/customers?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#customer').typeahead({
        minLength: 3,
        highlight: true
    }, {
        name: 'customers',
        display: 'name',
        source: customers
    }).on("typeahead:select", function (e, customer) {
        vm.customerId = customer.id;
    });

    var books = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/movies?query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('#book').typeahead({
        minLength: 3,
        highlight: true
    }, {
        name: 'books',
        display: 'name',
        source: movies
    }).on("typeahead:select", function (e, movie) {
        $("#books").append("<li class='list-group-item'>" + movie.name + "</li>");

        $("#book").typeahead("val", "");

        vm.movieIds.push(movie.id);
    });
    $.validator.addMethod("validCustomer", function () {
        return vm.customerId && vm.customerId !== 0;
    }, "Please select a valid customer.");

    $.validator.addMethod("atLeastOneBook", function () {
        return vm.movieIds.length > 0;
    }, "Please select at least one book.");

    var validator = $("#newRental").validate({
        submitHandler: function () {
            $.ajax({
                url: "/api/newRentals",
                method: "post",
                data: vm
            })
                .done(function () {
                    toastr.success("Rentals successfully recorded.");

                    $("#customer").typeahead("val", "");
                    $("#book").typeahead("val", "");
                    $("#books").empty();

                    vm = { movieIds: [] };

                    validator.resetForm();
                })
                .fail(function () {
                    toastr.error("Something unexpected happened.");
                });

            return false;
        }
    });
});

////$("#NewRentalForm").validate({
//    submitHandler: function (form) {
//        e.preventDefault();
//        rules: {
//            BookIds: "required",
//            FirstName: "required",
//            LastName: "required",
//                Email: {
//                required: true,
//                    email: true
//            },
//                    Date: "required",
//        },
//        debugger
//        messages: {
//            FirstName: "Please specify your first name",
//                LastName: "Please specify your last name",
//                Email:
//                {
//                    required: "We need your email address to contact you",
//                    email: "Your email address must be in the format of name@domain.com"
//            },
//            Date: "Please specify a date",
//            }

//        form.submit();
//    }
//});
/*});*/