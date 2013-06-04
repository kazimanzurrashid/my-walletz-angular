app.service('context', function (ServerAPI) {
    var userSignedIn = false;
    var categories = [];
    var accounts = [];

    this.isUserSignedIn = function() {
        return userSignedIn;
    };

    this.userSignedIn = function(options) {
        userSignedIn = true;

        if (options && options.load) {
            categories = ServerAPI.Category.query();
            accounts = ServerAPI.Account.query();
        }
    };

    this.userSignedOut = function() {
        userSignedIn = false;
        categories = accounts = [];
    };

    this.getCategories = function() {
        return categories;
    };

    this.getAccounts = function() {
        return accounts;
    };
});