redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51JWpRMH8V331WN56jBnOtHFt9sMWTUYr06yhA8dI4UXEKefJaWoRcugRPbUMD7I7fAlf3off1w7SmsKLFOarTvw400IWPb5Lqr');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
}