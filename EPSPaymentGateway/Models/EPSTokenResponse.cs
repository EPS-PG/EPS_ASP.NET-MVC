using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPSPaymentGateway.Models
{
    public class EPSTokenResponse
    {
        public string Token { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string ErrorMessage { get; set; }
        public int? ErrorCode { get; set; }
    }
}