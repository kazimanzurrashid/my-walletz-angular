app.controller('ForgotPassword', function ($scope
        , $location
        , ServerAPI
        , events) {

    $scope.password = {
        email: null
    };

    $scope.submit = function () {
        $scope.modelErrors = void(0);
        ServerAPI.Password.forgot($scope.password).
            success(function() {
                events.trigger('passwordResetRequested');
                $location.path('/');
            }).
            error(function() {
                $scope.modelErrors = ['An unexpected error has occurred ' +
                    'while requesting password reset.'];
            });
    };
});