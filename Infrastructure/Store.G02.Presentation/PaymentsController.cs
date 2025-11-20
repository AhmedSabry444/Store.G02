using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Store.G02.Services.Abstractions;
using Stripe;
using Stripe.Forwarding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore;  
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;


namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController(IServiceManger _serviceManger) : ControllerBase
    {
        [HttpPost("{basketId}")]
        public async Task<IActionResult> CreatePaymentIntent(string basketId)
        {
            var result = await _serviceManger.PaymentService.CreatePaymentIntentAsync(basketId);
            return Ok(result);
        }


 

   

        [Route("webhook")]
        [HttpPost]
            public async Task<IActionResult> Index()
            {
                var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                const string endpointSecret = "whsec_497b39227451fa3329ef3502454b5ca5264a7cab9e87e1f7b15ca496d8870a5b";
               
                {
                    var stripeEvent = EventUtility.ParseEvent(json);
                    var signatureHeader = Request.Headers["Stripe-Signature"];

                    stripeEvent = EventUtility.ConstructEvent(json,
                            signatureHeader, endpointSecret);

                    // If on SDK version < 46, use class Events instead of EventTypes
                    if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                    {
                    

                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                    {
                        
                    }
                    else
                    {
                        Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                    }
                    return Ok();
                }
   
            }
        }
    }

