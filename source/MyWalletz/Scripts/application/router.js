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
        when('/accounts/new', {
            templateUrl: 'account-create.html',
            controller: 'AccountCreate',
            secured: true
        }).
        when('/accounts/:sortAttribute/:sortOrder', {
            templateUrl: 'account-list.html',
            controller: 'AccountList',
            secured: true
        }).
        when('/accounts/:sortAttribute', {
            templateUrl: 'account-list.html',
            controller: 'AccountList',
            secured: true
        }).
        when('/accounts', {
            templateUrl: 'account-list.html',
            controller: 'AccountList',
            secured: true
        }).
        when('/forgot-password', {
            templateUrl: 'forgot-password.html',
            controller: 'ForgotPassword'
        }).
        when('/sign-in', {
            templateUrl: 'session-create.html',
            controller: 'SignIn'
        }).
        when('/sign-up', {
            templateUrl: 'user-create.html',
            controller: 'SignUp'
        }).
        otherwise({
            templateUrl: 'not-found.html',
            controller: 'Page'
        });
});