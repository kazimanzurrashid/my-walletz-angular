﻿app.controller('AccountList', function ($scope
        , $routeParams
        , $location
        , $dialog
        , Validation
        , context
        , events) {

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
        $dialog.messageBox('Delete?'
            , 'Are you sure you want to delete "' +
                account.title + '" account?', [
            { label: 'Ok', result: true },
            { label: 'Cancel', result: false, cssClass: 'btn-primary' }
        ]).
            open().
            then(function (result) {
                if (result) {
                    destroy(account);
                }
            });
    };

    function destroy(account) {
        var title = account.title;
        var index = context.accounts.indexOf(account);

        account.$delete(function () {
            context.accounts.splice(index, 1);
            events.trigger('flash:success', {
                message: '\"' + title + '\" account deleted.'
            });
        }, function (response) {
            var message = 'An unexpected error has occurred while deleting \"' +
                title + '\" account.';

            if (Validation.hasModelErrors(response)) {
                var modelErrors = Validation.getModelErrors(response);
                if (modelErrors) {
                    message = modelErrors[0];
                }
            }
            events.trigger('flash:error', {
                message: message
            });
        });
    }
});