app.controller('FlashMessage', function ($scope, $timeout) {
    $scope.alerts = [];

    function remove(alert) {
        var index = $scope.alerts.indexOf(alert);
        if (index > -1) {
            $scope.alerts.splice(index, 1);
        }
    }

    function add(type, message) {
        var icon;
        switch(type) {
            case 'success':
                icon = 'icon-ok-sign';
                break;
            case 'error':
                icon = 'icon-warning-sign';
                break;
            case 'info':
                icon = 'icon-info-sign';
                break;
            default:
                icon = '';
                break;
        }

        var alert = {
            type: type,
            icon: icon,
            message: message,
            close: function() {
                remove(this);
            }
        };

        $scope.alerts.push(alert);

        $timeout(function() {
            remove(alert);
        }, 1000 * 7);
    }

    $scope.$on('flash:success', function(e, args) {
        add('success', args.message);
    });

    $scope.$on('flash:error', function(e, args) {
        add('error', args.message);
    });

    $scope.$on('flash:info', function(e, args) {
        add('info', args.message);
    });
});