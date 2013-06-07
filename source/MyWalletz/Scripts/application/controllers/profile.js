app.controller('Profile', function ($scope
        , $location
        , $dialog
        , ServerAPI
        , Validation
        , events) {

    $scope.password = {
        oldPassword: null,
        newPassword: null,
        confirmPassword: null
    };

    $scope.changePassword = function() {
        $scope.modelErrors = void (0);
        ServerAPI.Password.change($scope.password).
            success(function () {
                events.trigger('passwordChanged');
                $location.path('/');
            }).
            error(function (response) {
                if (Validation.hasModelErrors(response)) {
                    $scope.modelErrors = Validation.getModelErrors(response);
                    return;
                }
                $scope.modelErrors = ['An unexpected error has occurred ' +
                    'while changing your password.'];
            });
    };

    $scope.signOut = function () {
        $dialog.messageBox('Sign out?', 'Are you sure you want to sign out?', [
            { label: 'Ok', result: true },
            { label: 'Cancel', result: false, cssClass: 'btn-primary' }
        ]).
            open().
            then(function(result) {
                if (result) {
                    signOut();
                }
            });
    };

    function signOut() {
        var session = new ServerAPI.Session;
        session
            .$delete(function () {
                events.trigger('signedOut');
                $location.path('/');
            }, function () {
                events.trigger('flash:error', {
                    message: 'An unexpected error has occurred while signing out.'
                });
            });
    }
});