app.controller('SignIn', function($scope, $location, ServerAPI, Validation, events) {
    $scope.session = new ServerAPI.Session;

    $scope.submit = function() {
        $scope.modelErrors = void(0);
        $scope.session
            .$save(function() {
                events.trigger('signedIn');
                $location.path('/');
            }, function(response) {
                var error = Validation.hasModelErrors(response) ?
                    'Invalid credentials.' :
                    'An unexpected error has occurred while signing in.';
                $scope.modelErrors = [error];
            });
    };
});