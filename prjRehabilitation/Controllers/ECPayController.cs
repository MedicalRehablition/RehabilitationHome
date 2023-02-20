
using Commerce.Models;
using Commerce.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using prjRehabilitation.Models.Lin;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace prjRehabilitation.Controllers
{
    public class ECPayController : Controller
    {
        private readonly ILogger<ECPayController> _logger;
        private readonly IConfiguration Config;

        public ECPayController(ILogger<ECPayController> logger)
        {
            _logger = logger;
            Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
        }

        private ICommerce GetPayType(string option)
        {
            switch (option)
            {
                case "newbPay":
                case "newbPayPeriod":
                    return new NewebpayService();
                case "ECPay":
                case "ECPayPeriod":
                    return new ECPayService();

                default: throw new ArgumentException("No Such option");
            }
        }

        private string GetReturnValue(ICommerce service, SendToNewebPayIn inModel)
        {

            switch (inModel.PayOption)
            {
                case "newbPay":
                    return service.GetCallBack(inModel);
                case "newbPayPeriod":
                    return service.GetPeriodCallBack(inModel);
                case "ECPay":
                    return service.GetCallBack(inModel);
                case "ECPayPeriod":
                    return service.GetPeriodCallBack(inModel);

                default: throw new ArgumentException("No Such option");
            }
        }

        public IActionResult Index(int? price,string email,string url)
        {
            ViewData["MerchantOrderNo"] = DateTime.Now.ToString("yyyyMMddHHmmss");  //訂單編號
            ViewData["ExpireDate"] = DateTime.Now.AddDays(3).ToString("yyyyMMdd"); //繳費有效期限
            ViewData["Amt"] = price;
            ViewData["ItemDesc"] = "關心康復之家住民手做愛心商品";
            ViewData["Email"] = email;
            ViewData["ReturnURL"] = url;
            return View();
        }

        public IActionResult SendToNewebPay(SendToNewebPayIn inModel)
        {
            var service = GetPayType(inModel.PayOption);

            return Json(GetReturnValue(service, inModel));
        }

        [HttpPost]
        public async Task<IActionResult> GetReturn(SendToNewebPayIn inModel)
        {
            var obj = await new ECPayService().GetQueryCallBack(inModel.MerchantOrderNo, inModel.Amt);
            return Json(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> UpdatePeriodCallBack(string orderNo, string PeriodN)
        {
            var result = await new NewebpayService().GetUpdatePeriodCallBackAsync(orderNo, PeriodN);

            return result;
        }

        /// <summary>
        /// 支付完成返回網址
        /// </summary>
        /// <returns></returns>
        public IActionResult CallbackReturn(string option)
        {
            var service = GetPayType(option);
            var result = service.GetCallbackResult(Request.Form);
            ViewData["ReceiveObj"] = result.ReceiveObj;
            ViewData["TradeInfo"] = result.TradeInfo;

            return View();
        }


        /// <summary>
        /// 商店取號網址
        /// </summary>
        /// <returns></returns>
        public IActionResult CallbackCustomer(string option)
        {
            var service = GetPayType(option);
            var result = service.GetCallbackResult(Request.Form);
            ViewData["ReceiveObj"] = result.ReceiveObj;
            ViewData["TradeInfo"] = result.TradeInfo;
            return View();
        }
    }
    }

