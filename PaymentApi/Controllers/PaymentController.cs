
using CommonUtilities.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentApi.Services;

namespace PaymentApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost]
        public JsonResult PurchaseBook([FromBody] BuyerDetails buyerDetails)
        {
            try
            {
                int result = _paymentService.BuyBook(buyerDetails);
                return Json(result);
            }
            catch(Exception)
            {
                return Json("Some error occurred");
            }
        }

       
        [HttpGet]
        public ActionResult VieworDownloadBill([FromQuery] int paymentId)
        {
            try
            {
                var result = _paymentService.GetInvoice(paymentId);
                return Ok(result);
            }
            catch(Exception )
            {
                return NotFound();
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
                    return Ok("No payment history available");
                }
            }
            catch (Exception )
            {
                return Ok("Some error occurred");
            }
        }

        [HttpGet]
        public ActionResult Refund([FromBody] int paymentId)
        {
            try
            {
                var result = _paymentService.GetRefund(paymentId);
                return Ok(result);
            }
            catch(Exception)
            {
                return Ok("Some error occurred");
            }           
        }

    }
}
