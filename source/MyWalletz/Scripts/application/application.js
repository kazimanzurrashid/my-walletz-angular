var app = angular.module(
    'myWalletz',
    ['ngResource', 'ui.validate', 'ui.bootstrap.alert']);


app.run(function(events) {

    function showFlashSuccess(message) {
        events.trigger('flash:success', {
            message: message
        });
    }

    events.on('signedUp', function () {
        showFlashSuccess('Thank you for signing up, an email with a ' +
            'confirmation link has been sent to your email address. ' +
            'Please open the link to activate your account.');
    });

    events.on('passwordResetRequested', function () {
        showFlashSuccess('An email with a password reset link has been ' +
            'sent to your email address. Please open the link to reset ' +
            'your password.');
    });

    events.on('signedIn', function () {
        showFlashSuccess('You are now signed in.');
    });
});