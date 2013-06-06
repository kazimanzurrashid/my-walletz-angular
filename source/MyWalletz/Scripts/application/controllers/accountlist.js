app.controller('AccountList', function ($scope
        , $routeParams
        , $location
        , context) {

    $scope.sortAttribute = $routeParams.sortAttribute || 'title';
    $scope.descending = $routeParams.sortOrder &&
        $routeParams.sortOrder === 'descending';

    $scope.accounts = context.accounts;

    $scope.sort = function(attribute) {
        var sortOrder = 'ascending';
        if ($scope.sortAttribute === attribute) {
            if (!$scope.descending) {
                sortOrder = 'descending';
            }
        }
        $location.path('/accounts/' + attribute + '/' + sortOrder);
    };

    $scope.destroy = function(account) {
        console.dir(account);
    };
});