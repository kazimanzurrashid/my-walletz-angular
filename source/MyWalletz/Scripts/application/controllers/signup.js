app.controller('SignUp', function($scope, $location, ServerAPI, Validation, events) {
    $scope.user = new ServerAPI.User;

    $scope.submit = function() {
        $scope.modelErrors = void(0);
        $scope.user
            .$save(function () {
                events.trigger('signedUp');
                $location.path('/');
            }, function(response) {
                if (Validation.hasModelErrors(response)) {
                    $scope.modelErrors = Validation.getModelErrors(response);
                    return;
                }
                $scope.modelErrors = ['An unexpected error has occurred ' +
                    'while signing up.'];
            });
    };
});