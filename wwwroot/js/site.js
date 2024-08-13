

let deleteTodoItem = (id) => {
    if (confirm("Do you want to delete the row with ID: " + id + "?")) {
        $.ajax({
            url: `/Todo/Remove?id=${id}`,
            type: 'GET',
            success: function (result) {

                $(`#todo-${id}`).remove();
            },
            error: function (err) {
                alert("Error deleting item");
            }
        });
    }
}
