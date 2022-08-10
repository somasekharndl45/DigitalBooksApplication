using CommonUtility.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Services;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public ActionResult PurchaseBook([FromBody] Buyer buyer)
        {
            try
            {
                string result = _paymentService.BuyBook(buyer);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult VieworDownloadInvoice([FromBody] long paymentId)
        {
            try
            {
                var result = _paymentService.GetInvoice(paymentId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            
        }        

        [HttpGet]
        public ActionResult ViewPaymentHistory([FromBody] string emailID)
        {
            try
            {
                var result = _paymentService.GetPaymentHistory(emailID);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return Ok("You have not purchased any books");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult AskForRefund([FromBody] long paymentId)
        {
            try
            {
                var result = _paymentService.GetRefund(paymentId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }           
        }

    }
}
