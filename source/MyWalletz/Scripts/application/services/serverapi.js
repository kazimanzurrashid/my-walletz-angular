app.factory('ServerAPI', function ($http, $resource) {
    var urlPrefix = '/api';

    return {
        User: $resource(urlPrefix + '/users'),
        Session: $resource(urlPrefix + '/sessions'),
        Category: $resource(urlPrefix + '/categories/:id'
            , { id: '@id' }
            , { create: { method: 'POST' }, save: { method: 'PUT' } }),
        Account: $resource(urlPrefix + '/accounts/:id'
            , { id: '@id' }
            , { create: { method: 'POST' }, save: { method: 'PUT' } }),
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