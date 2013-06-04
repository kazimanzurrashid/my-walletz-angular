app.config(function($routeProvider) {
    $routeProvider.
        when('/', {
            templateUrl: 'home.html',
            controller: 'Page'
        }).
        when('/about', {
            templateUrl: 'about.html',
            controller: 'Page'
        }).
        when('/passwords/reset', {
            templateUrl: 'password-reset.html',
            controller: 'ForgotPassword'
        }).
        when('/sessions/new', {
            templateUrl: 'session-create.html',
            controller: 'SignIn'
        }).
        when('/users/new', {
            templateUrl: 'user-create.html',
            controller: 'SignUp'
        }).
        otherwise({
            templateUrl: 'not-found.html',
            controller: 'Page'
        });
});