app.service('context', function (ServerAPI) {
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
});