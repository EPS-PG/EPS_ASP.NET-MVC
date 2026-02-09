using EPSPaymentGateway.Models;
using EPSPaymentGateway.Repo;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPSPaymentGateway.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var salesDetails = new SalesDetails() { Amount = 50 };
            return View(salesDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Amount")] SalesDetails salesDetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var epsRepository = new EPSRepository();
                    var token = epsRepository.GetEPSToken();

                    if (!string.IsNullOrWhiteSpace(token.ErrorMessage))
                    {
                        ModelState.AddModelError(string.Empty, token.ErrorMessage);
                        return View(salesDetails);
                    }
                    var initializeData = epsRepository.InitializeEPS(token.Token, salesDetails.Amount);

                    if (initializeData.ErrorCode > 0)
                    {
                        ModelState.AddModelError(string.Empty, initializeData.ErrorMessage);
                        return View(salesDetails);
                    }
                    else
                    {
                        return Redirect(initializeData.RedirectURL);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return View(salesDetails);
        }

        public ActionResult EPSSuccessCallBack(string Status, string MerchantTransactionId, string ErrorCode, string ErrorMessage)
        {
            try
            {
                TempData["Message"] = "Payment Successful";
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }

        public ActionResult EPSFailCallBack(string Status, string MerchantTransactionId, string ErrorCode, string ErrorMessage)
        {
            try
            {
                TempData["Message"] = ErrorMessage;
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }

        public ActionResult EPSCancelCallBack(string Status, string MerchantTransactionId, string ErrorCode, string ErrorMessage)
        {
            try
            {
                TempData["Message"] = ErrorMessage;
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }

        public ActionResult EPSCheckPaymentStatus()
        {
            try
            {
                var epsRepository = new EPSRepository();
                var token = epsRepository.GetEPSToken();
                if (!string.IsNullOrWhiteSpace(token.ErrorMessage))
                {
                    return RedirectToAction("Index");
                }
                var response = epsRepository.EPSCheckPaymentStatus("20230110053837079", token.Token);

            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("Index");
        }

    }
}