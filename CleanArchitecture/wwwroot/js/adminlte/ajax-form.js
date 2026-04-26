$(document).ready(function () {
    $.ajaxSetup({
        headers: {
            "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val()
        }
    });

    $(document).on("submit", ".ajax-form", function (e) {
        e.preventDefault();
        $(".loader-wrapper").show();

        let $this = $(this);
        let formData = new FormData(this);

        $this.find(".has-danger").removeClass("has-error");
        $this.find(".form-errors").remove();
        $this.find(".text-danger").removeClass("text-danger");

        $.ajax({
            type: $this.attr("method"),
            url: $this.attr("action"),
            data: formData,
            contentType: false,
            processData: false,
            cache: false,

            xhr: function () {
                let xhr = new XMLHttpRequest();

                // Listen to the `progress` event to update the progress bar
                xhr.upload.addEventListener("progress", function (e) {
                    if (e.lengthComputable) {
                        let percent = (e.loaded / e.total) * 100;
                        $("#uploadProgress").val(percent); // Update progress bar
                    }
                });

                return xhr;
            },

            success: function (response) {
                successResponseProcess(response);
            },

            error: function (response) {
                errorResponseProcess(response);
            },
        });
    });
});

function successResponseProcess(response) {
    $(".loader-wrapper").hide();
    if (response.status) {
        toastr.success(response.message);
    }
    else {
        toastr.error(response.message);
    }

    let tableName = response.tableName;

    if (
        tableName &&
        $(tableName).length &&
        $.fn.DataTable.isDataTable(tableName)
    ) {
        let table = $(tableName).DataTable();
        table.ajax.reload(null, false);
    }
    else {
        let table = $("#dataGrid").DataTable();
        console.dir(table)
        table.ajax.reload(null, true);
    }
}

function errorResponseProcess(response) {
    $(".loader-wrapper").hide();
    let data = response.responseJSON || JSON.parse(response.responseText);

    if (data && data.data && Object.keys(data.data).length > 0) {
        $.each(data.data, (key, value) => {

            $("[name^=" + key.toLowerCase() + "]")
                .parent()
                .find(".form-errors")
                .remove();

            $("[name^=" + key.toLowerCase() + "]")
                .parent()
                .append(
                    '<small class="text-danger form-errors">' +
                    value[0] +
                    "</small>"
                );
        });
    } else {
        swal("", data.message, "error");
    }
}