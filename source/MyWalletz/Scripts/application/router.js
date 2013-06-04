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
        when('/my-account', {
            templateUrl: 'profile.html',
            controller: 'Profile',
            secured: true
        }).
        when('/accounts', {
            templateUrl: 'account-list.html',
            controller: 'AccountList',
            secured: true
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