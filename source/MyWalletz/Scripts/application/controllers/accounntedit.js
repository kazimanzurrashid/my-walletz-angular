app.controller('AccountEdit', function ($scope
        , $routeParams
        , $location
        , ServerAPI
        , Validation
        , context
        , events) {

    var id = parseInt($routeParams.id, 10);

    $scope.account = null;

    angular.forEach(context.accounts, function(value) {
        if (!$scope.account && value.id == id) {
            $scope.account = value;
        }
    });

    if (!$scope.account) {
        $location.path('/404');
    }

    var title = $scope.account.title;

    $scope.submit = function() {
        $scope.modelErrors = void(0);
        $scope.account
            .$save(function() {
                events.trigger('flash:success', {
                    message: '\"' + title + '\" account updated.'
                });
                $location.path('/accounts');
            }, function(response) {
                if (Validation.hasModelErrors(response)) {
                    $scope.modelErrors = Validation.getModelErrors(response);
                    return;
                }
                $scope.modelErrors = ['An unexpected error has occurred while ' +
                        'updating \"' + title + '\" account.'];
            });
    };
});