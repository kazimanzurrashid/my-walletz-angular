app.controller('Navigation', function($scope, $location, context) {
    $scope.accounts = context.getAccounts();

    $scope.myAccount = function () {
        var path = context.isUserSignedIn() ?
            '/my-account' :
            '/sign-in';
        $location.path(path);
    };
});