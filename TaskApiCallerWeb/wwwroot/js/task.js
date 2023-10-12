$(document).ready(function () {
    $("#btnSaveTask").click(function () {
        var formData = $("#createTaskForm").serialize();
        $.ajax({
            url: "/Task/CreateTask",
            method: "POST",
            data: formData,
            success: function (data) {
                $("#mdlCreateTask").modal("hide");
            },
            error: function (error) {
                alert("Error: " + error.responseText);
            }
        });
    });
});