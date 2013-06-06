app.controller('AccountCreate', function ($scope
        , $location
        , ServerAPI
        , Validation
        , context
        , events) {

    $scope.account = new ServerAPI.Account;

    $scope.submit = function() {
        $scope.modelErrors = void(0);
        $scope.account.createdAt = new Date;

        $scope.account
            .$save(function() {
                context.accounts.push($scope.account);
                events.trigger('flash:success', {
                    message: 'New account created.'
                });
                $location.path('/accounts');
            }, function(response) {
                if (Validation.hasModelErrors(response)) {
                    $scope.modelErrors = Validation.getModelErrors(response);
                    return;
                }
                $scope.modelErrors = ['An unexpected error has occurred ' +
                    'while creating new account.'];
            });
    };
});