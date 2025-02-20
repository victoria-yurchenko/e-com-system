using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Stripe;
using Stripe.Checkout;

namespace Infrastructure.PaymentGateways
{
    public class StripePaymentGateway : IPaymentGateway
    {
        private readonly IPaymentService _paymentService;

        public StripePaymentGateway(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(decimal amount, Domain.Enums.PaymentMethod paymentMethod, Guid userId)
        {
            if (!IsPaymentMethodSupported(paymentMethod))
            {
                return CreateFailureResult("Unsupported payment method. Only card payments are supported.");
            }

            try
            {
                var session = await CreateStripeSessionAsync(amount);
                await _paymentService.RecordTransactionAsync(userId, amount, PaymentStatus.Success.ToString(), paymentMethod.ToString(), PaymentProvider.Stripe.ToString(), session.Id);
                return CreateSuccessResult(session.Url, session.Id);
            }
            catch (StripeException ex)
            {
                return CreateFailureResult($"Stripe error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return CreateFailureResult($"An unexpected error occurred: {ex.Message}");
            }
        }

        private bool IsPaymentMethodSupported(Domain.Enums.PaymentMethod paymentMethod)
        {
            return paymentMethod == Domain.Enums.PaymentMethod.Card;
        }

        private async Task<Session> CreateStripeSessionAsync(decimal amount)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = ConvertToCents(amount),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Subscription Payment"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",   // one time payment
                // TODO set real success and cancel urls (where the user will be redirected after payment)
                //SuccessUrl = "https://yourdomain.com/success",
                //CancelUrl = "https://yourdomain.com/cancel"
            };

            var service = new SessionService();
            return await service.CreateAsync(options);
        }

        private long ConvertToCents(decimal amount)
        {
            return (long)(amount * 100);
        }

        private PaymentResult CreateSuccessResult(string paymentUrl, string transactionId)
        {
            return new PaymentResult
            {
                IsSuccess = true,
                PaymentUrl = paymentUrl,
                TransactionId = transactionId
            };
        }

        private PaymentResult CreateFailureResult(string errorMessage)
        {
            return new PaymentResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };
        }
    }
}
