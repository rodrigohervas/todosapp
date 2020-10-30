
/**
 * Functions executed on document.ready()
 */
$(document).ready(function () {

    /**
     * sets the modal body content, on modal load, for the Add Modal
     * called from the Add button in _TodosList
     */
    $('#addTodoModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Get button that triggered the modal
        var url = button.data('url') // Extract info from data-url

        $.get(url, function (_View) {
            $('.modal-body').html($(_View).find(".modal-body").html()); //load html into modal body
        });
    });

    /**
    * sets the modal body content, on modal load, for the Details Modal
    * called from the Details button in _TodosList
    */
    $('#detailsTodoModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Get button that triggered the modal
        var url = button.data('url') // Extract info from data-url
        SetModalContent(url);
    });

    /**
    * sets the modal body content, on modal load, for the Update Modal
    * called from the Update button in _TodosList
    */
    $('#updateTodoModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Get button that triggered the modal
        var url = button.data('url') // Extract info from data-url
        SetModalContent(url);
    });

    /**
    * sets the modal body content, on modal load, for the Delete Modal
    * called from the Delete button in _TodosList
    */
    $('#deleteTodoModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget) // Get button that triggered the modal
        var url = button.data('url') // Extract info from data-url
        SetModalContent(url);
    });

    /**
    * gets and sets the html content into a modal-body div
    */
    function SetModalContent(url) {
        $.get(url, function (_View) {
            $('.modal-body').html($(_View).find(".modal-body").html());
        });
    };

    /**
    * sets the onclick functionality for the Delete button in _TodosList partial view:
    * - calls TodosController._DeleteTodo() with a jQuery post request, then 
    * - hides the _DeleteTodo modal
    * called from the Delete button in _TodosList
    */
    $('#deleteTodoModal').on('click', '#deleteButton', function (event) {
        var $modalDiv = $(event.delegateTarget); // deleteModal div
        var url = $(this).data('url'); // Extract info from data-url
        var id = $modalDiv.find("#id").val();

        var item = { "id": id };

        $.post(url, item)
            .then(function (resp) {
                $modalDiv.modal('hide');
            });
    });

});