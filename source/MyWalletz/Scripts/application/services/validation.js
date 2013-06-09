app.service('Validation', function() {
    this.hasModelErrors = function(response) {
        return response.status === 400;
    };

    this.getModelErrors = function(response) {
        var errors = [];
        if (response.data.modelState) {
            angular.forEach(response.data.modelState, function(messages) {
                angular.forEach(messages, function(message) {
                    errors.push(message);
                });
            });
        }
        return errors.length ? errors : void(0);
    };
});