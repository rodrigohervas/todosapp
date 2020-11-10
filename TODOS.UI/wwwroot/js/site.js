

/**
 * Functions executed on document.ready()
 */
$(document).ready(function () {

    //get the placeholder div for the modals
    var placeHolderElement = $('#modal-placeholder');

    /**
     * add eventListener to modal open buttons to handle click events
     * using $(document).on() makes sure that the event handler for the modal open buttons binds again after replacing the cards for the updated ones
     */
    $(document).on('click', 'button[data-toggle="ajax-modal"]', function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeHolderElement.html(data);
            placeHolderElement.find('.modal').modal('show');
        });
    });

    /**
     * add eventListener to modal-save buttons to handle click events
     * to process modal form action attach onclick eventhandler to the submit button of the loaded modal
     */
    placeHolderElement.on('click', '[data-save="modal"]', function (event) {
        //prevent request to be sent to server
        event.preventDefault();

        //manage request via jQuery
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        /**
         * send request to controller action
         */
        $.post(actionUrl, dataToSend).done(function (data) {
            //show response on modal new body
            var newBody = $('.modal-body', data);
            placeHolderElement.find('.modal-body').replaceWith(newBody);

            //hide modal if no errors in response (if hidden IsValid input is true)
            var isValid = newBody.find('[name="IsValid"]').val() == 'True';
            if (isValid) {
                //hide modal placeholder
                placeHolderElement.find('.modal').modal('hide');

                //get the url to request all todos
                var todosUrl = $('#main-container').find('.todos-list').data('url');
                /**
                 * get all todos from controller to refresh cards
                 */
                $.get(todosUrl).done(function (_View) {
                    var todosList = $('#main-container', _View).find('.todoCardsContainer').html();
                    $('.todoCardsContainer').html(todosList);
                });
            }
        });
    });    
});

