using CommonUtilities.CommonVariables;
using CommonUtilities.ViewModels;
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


        /// <summary>
        /// Make payment to purchase book
        /// </summary>
        /// <param name="buyer"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult PurchaseBook([FromBody] Buyer buyer)
        {
            try
            {
                int result = _paymentService.BuyBook(buyer);
                return Json(result);
            }
            catch(Exception ex)
            {
                return Json(Common.generalError);
            }
        }

        /// <summary>
        /// View Invoice with payment id
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns>Invoice details</returns>
        [HttpGet]
        public ActionResult VieworDownloadInvoice([FromQuery] int paymentId)
        {
            try
            {
                var result = _paymentService.GetInvoice(paymentId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
            
        }        

        /// <summary>
        /// View Payment history
        /// </summary>
        /// <param name="emailID"></param>
        /// <returns>payment history</returns>
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
                    return Ok(Common.paymentHistory);
                }
            }
            catch (Exception ex)
            {
                return Ok(Common.generalError);
            }
        }

        /// <summary>
        /// Provide refund if applicable
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns>Refund message</returns>
        [HttpGet]
        public ActionResult AskForRefund([FromBody] int paymentId)
        {
            try
            {
                var result = _paymentService.GetRefund(paymentId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return Ok(Common.generalError);
            }           
        }

    }
}
