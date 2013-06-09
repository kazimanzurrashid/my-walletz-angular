app.service('Resource', function () {
    this.copy = function (source, prefix) {
        if (_.isUndefined(prefix)) {
            prefix = '$';
        }

        var result = {};

        _.chain(source).
           keys().
           reject(function (key) { return key.indexOf(prefix) === 0; }).
           each(function (key) { result[key] = source[key]; });

       return result;
    };

    this.merge = function (source, target) {
        if (_.isUndefined(target)) {
            target = {};
        }

        for (var prop in source) {
            if (source.hasOwnProperty(prop)) {
                target[prop] = source[prop];
            }
        }

        return target;
    };
});