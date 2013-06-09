app.directive('fieldGroup', function() {
    return {
        restrict: 'E',
        require: '^form',
        transclude: true,
        replace: true,
        template: '<div class="control-group" ng-transclude></div>',
        link: function ($scope, el, attrs, ctrl) {
            var formName = ctrl.$name;
            var fieldName = attrs['for'];
            var fieldExpression = formName + '.' + fieldName;
            var watchExpression = fieldExpression + '.$error';

            $scope.$watch(watchExpression, function() {
                var field = $scope[formName][fieldName];
                if (field.$pristine) {
                    return;
                }
                var hasError = false;
                var errors = field.$error;
                for (var prop in errors) {
                    if (errors.hasOwnProperty(prop)) {
                        if (errors[prop]) {
                            hasError = true;
                            break;
                        }
                    }
                }
                if (hasError) {
                    el.addClass('error');
                }
                else {
                    el.removeClass('error');
                }
            }, true);
        }
    };
});

app.directive('validationMessage', function () {
    return {
        restrict: 'E',
        require: '^form',
        replace: true,
        template: '<div class="help-block"></div>',
        link: function ($scope, el, attrs, ctrl) {
            var formName = ctrl.$name;
            var fieldName = attrs['for'];

            var watchExpression = formName + '.' + fieldName + '.$error';

            $scope.$watch(watchExpression, function() {
                var field = $scope[formName][fieldName];
                var show = field.$invalid && field.$dirty;
                el.css('display', show ? '' : 'none');
                var html = '';
                if (show) {
                    var errors = field.$error;
                    for (var prop in errors) {
                        if (errors.hasOwnProperty(prop)) {
                            if (errors[prop] && attrs[prop]) {
                                html += '<span>' + attrs[prop] + '</span>';
                            }
                        }
                    }
                }
                el.html(html);
            }, true);
        }
    };
});

app.directive('submitButton', function() {
    return {
        restrict: 'E',
        require: '^form',
        transclude: true,
        replace: true,
        template: '<button type="submit" class="btn btn-primary" ng-transclude></button>',
        link: function ($scope, el, attrs, ctrl) {
            var watchExpression = ctrl.$name + '.$invalid';
            $scope.$watch(watchExpression, function(value) {
                attrs.$set('disabled', !!value);
            });
        }
    };
});