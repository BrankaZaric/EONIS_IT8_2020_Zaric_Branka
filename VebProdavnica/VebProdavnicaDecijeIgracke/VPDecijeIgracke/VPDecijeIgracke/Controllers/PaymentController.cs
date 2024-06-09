using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using VPDecijeIgracke.Data;
using VPDecijeIgracke.Models.PorudzbinaModel;

namespace VPDecijeIgracke.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : Controller
    {
        private IPaymentService paymentService;
        private ILogger<PaymentController> logger;
        private const string WhSecret = "whsec_7fe99c1df2abf590c7f88b871d8e6c7209dbe6d5d11457a8d59e15208a4d8927";

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            this.paymentService = paymentService;
            this.logger = logger;
        }

        //[Authorize]
        [HttpPost("{porudzbinaId}")]
        public async Task<ActionResult<PorudzbinaPaymentDTO>> CreateOrUpdatePaymentIntent(int porudzbinaId)
        {
            return await paymentService.CreateOrUpdatePaymentIntent(porudzbinaId);
        }

        [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            Porudzbina porudzbina;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment succeeded: ", intent.Id);
                    porudzbina = await paymentService.UpdatePorudzbinaPaymentSucceeded(intent.Id);
                    logger.LogInformation("Order updated to payment received: ", porudzbina.PorudzbinaID);
                    
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    logger.LogInformation("Payment failed: ", intent.Id);
                    porudzbina = await paymentService.UpdatePorudzbinaPaymentFailed(intent.Id);
                    logger.LogInformation("Order updated to payment failed: ", porudzbina.PorudzbinaID);
                    break;
            }

            return new EmptyResult();
        }
    }
}
