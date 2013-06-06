app.factory('ServerAPI', function ($http, $resource) {
    var urlPrefix = '/api';

    return {
        User: $resource(urlPrefix + '/users'),
        Session: $resource(urlPrefix + '/sessions/:id', { id: '@id' }),
        Category: $resource(urlPrefix + '/categories/:id', { id: '@id' }),
        Account: $resource(urlPrefix + '/accounts/:id', { id: '@id' }),
        Password: {
            forgot: function(model) {
                return $http.post(urlPrefix + '/passwords/forgot', model);
            },
            change: function(model) {
                return $http.post(urlPrefix + '/passwords/change', model);
            }
        }
    };
});