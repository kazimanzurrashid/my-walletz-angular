app.controller('AccountEdit', function ($scope
        , $routeParams
        , $location
        , ServerAPI
        , Resource
        , Validation
        , context
        , events) {

    var id = parseInt($routeParams.id, 10);
    var account = _.find(context.accounts, function(x) {
         return x.id === id;
    });

    if (!account) {
        $location.path('/404');
    }

    var title = account.title;
    $scope.account = Resource.copy(account);

    $scope.submit = function () {
        $scope.modelErrors = void(0);
        Resource.merge($scope.account, account);

        account
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