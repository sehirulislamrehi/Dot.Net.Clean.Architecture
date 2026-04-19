$(document).ready(function () {
    $(document).on("click", '[data-bs-toggle="modal"]', function () {
        var url = $(this).data("content");
        var modalTarget = $(this).attr("data-bs-target");
        $(modalTarget + " .modal-content").load(url);
    });
});
