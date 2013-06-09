app.controller('Navigation', function($scope, $location, $timeout, context, events) {
    $scope.accounts = context.accounts;

    $scope.myAccount = function() {
        var path = context.isUserSignedIn() ?
            '/my-account' :
            '/sign-in';
        $location.path(path);
    };

    events.on('signedIn', sync);
    events.on('signedOut', sync);

    function sync() {
        $timeout(function() {
            $scope.accounts = context.accounts;
            if (!$scope.$$phase) {
                $scope.$apply();
            }
        }, 0);
    }
});