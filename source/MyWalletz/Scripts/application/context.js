app.service('context', function(ServerAPI) {
    var forEach = angular.forEach;
    var copy = angular.copy;

    var userSignedIn = false;

    this.categories = [];
    this.accounts = [];

    this.isUserSignedIn = function() {
        return userSignedIn;
    };

    this.userSignedIn = function(options) {
        userSignedIn = true;

        if (options && options.load) {
            this.categories = ServerAPI.Category.query();
            this.accounts = ServerAPI.Account.query();
        }
    };

    this.userSignedOut = function() {
        userSignedIn = false;
        this.categories = this.accounts = [];
    };

    this.loadCategories = function(payloads) {
        this.categories = load(payloads, ServerAPI.Category);
    };

    this.loadAccounts = function(payloads) {
        this.accounts = load(payloads, ServerAPI.Account);
    };

    function load(payloads, Factory) {
        var array = [];
        forEach(payloads, function (payload) {
            var item = new Factory;
            copy(payload, item);
            array.push(item);
        });
        return array;
    }
});