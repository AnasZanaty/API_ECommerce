using Core.Entities.OrderEntities;
using Demo.HandleResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.BasketServices.Dto;
using Services.OrderServices.Dto;
using Services.PaymentServices;
using Stripe;
using Microsoft.Extensions.Logging;


namespace Demo.Controllers
{
    
    public class PaymentController : BaseController
    {
        private readonly IPaymentService paymentService;
        private const string WHsecret = "whsec_937149f52662d7cd518f801841b65c2497fa04b5d71de0434f832653bb91e5bd";

        public PaymentController(IPaymentService paymentService )
        {
            this.paymentService = paymentService;
          
        }


        [HttpPost("basketId")]
      public async  Task <ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForExistingOrder(CustomerBasketDto basket , string basketId)
        {

            var customerBasket = await paymentService.CreateOrUpdatePaymentIntentForExistingOrder(basket);

            if(customerBasket == null )
            {
                return BadRequest(new ApiResponse(400, "Problem with ur basket"));
            }

            return Ok(customerBasket);

        }

        [HttpPost("basketId")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntentForNewOrder(string basketId)
        {

            var customerBasket = await paymentService.CreateOrUpdatePaymentIntentForNewOrder(basketId);

            if (customerBasket == null)
            {
                return BadRequest(new ApiResponse(400, "Problem with ur basket"));
            }

            return Ok(customerBasket);

        }

        [HttpPost]
        public async Task<ActionResult> WebHook(string basketId) //copied from stripe
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json , Request.Headers["Stripe-Signature"], WHsecret);

                PaymentIntent paymentIntent;
                OrderResultDto order;

                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    //logger.LogInformation("Payment failed : ", paymentIntent.Id);
                    order = await paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    //logger.LogInformation("Order Updated to Payment failed: ", order.Id);


                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    //logger.LogInformation("Payment succeeded : ", paymentIntent.Id);
                    order = await paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id, basketId);
                    //logger.LogInformation("Order Updated to Payment succeded: ", order.Id);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }


        }
        
    
    }
}
