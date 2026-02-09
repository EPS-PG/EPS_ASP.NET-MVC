using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPSPaymentGateway.Models
{
    public class EPSInitializeResponse
    {
        public string RedirectURL { get; set; }
        public string ErrorMessage { get; set; }
        public int? ErrorCode { get; set; }
    }
}