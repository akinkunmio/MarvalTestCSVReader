//$(function () {
//    $('#editForm').on('submit', function (evt) {
//    evt.preventDefault();
//    debugger;
//    //var data = new FormData();
//    var form = $('#editForm')[0];
//    console.log($(this).serialize());
//    // Create an FormData object
//    var data = new FormData(form);
//    //var files = $("#fileUpload").get(0).files;
//    //if (files.length > 0) {
//    //    data.append("File", files[0]);
//    //}

//    $.ajax({
//        "url": "/api/Persons/edit",
//        "data": data,
//        "type": "POST",
//        //"enctype": 'multipart/form-data',
//        "dataType": "json",
//        "processData": true,
//        "cache": false,
//        "timeout": 800000,
//        "success": function () {
//            debugger
//            swal('Uploaded!', 'Success updates!', 'success');
//        },
//        "error": function (error) {
//            swal(
//                'Record not deleted!',
//                'The record couldnt be updated.' + error.responseText,
//                'info',
//            );
//        }
//    });
//});
//});