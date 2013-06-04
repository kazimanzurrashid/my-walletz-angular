app.service('events', function($rootScope) {
    this.trigger = function (name, args) {
        $rootScope.$broadcast(name, args);
    };

    this.on = function(name, handler) {
        $rootScope.$on(name, handler);
    };
});