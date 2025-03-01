namespace Application.Enums
{
    public enum Operation
    {
        // Notifications
        EmailRegistrationLink,
        Subscription,
        SubscriptionExpiration,
        SubscriptionRenewal,
        SubscriptionCancellation,
        SubscriptionPaymentFailed,
        SubscriptionPaymentSuccessful,

        // Messaging providers
        SendByEmail,
        /// add more ,
    }
}