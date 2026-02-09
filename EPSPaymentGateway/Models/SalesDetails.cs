using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPSPaymentGateway.Models
{
    public class SalesDetails
    {
        [Required]
        public decimal Amount { get; set; }
    }
}