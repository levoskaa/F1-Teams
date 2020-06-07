toastr.options.timeOut = 2000;
toastr.options.timeOut = 1000;

function Delete(url) {
    swal({
        title: "Are you sure?",
        text: "Deleted items cannot be recovered.",
        icon: "warning",
        buttons: {
            cancel: true,
            confirm: true
        },
        dangerMode: "true"
    }).then((result) => {
        if (result) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (result) {
                    if (result.success) {
                        toastr.options.onHidden = function () {
                            window.location.reload(true);
                        }
                        toastr.success(result.message);
                    } else {
                        toastr.error(result.message);
                    }
                }
            });
        }
    });
}