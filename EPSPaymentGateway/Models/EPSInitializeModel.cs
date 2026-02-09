using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPSPaymentGateway.Models
{
    public class EPSInitializeModel
    {
        //Required Fields Start
        public string StoreId { get; set; }
        public string MerchantTransactionId { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal TotalAmount { get; set; }
        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }
        public string CancelUrl { get; set; }
        public int DeviceTypeId { get; set; }
        //Required Fields End

        //Optional Fields Start
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerPostcode { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerPhone { get; set; }
        public string ShipmentName { get; set; }
        public string ShipmentAddress { get; set; }
        public string ShipmentAddress2 { get; set; }
        public string ShipmentCity { get; set; }
        public string ShipmentState { get; set; }
        public string ShipmentPostcode { get; set; }
        public string ShipmentCountry { get; set; }
        public string ValueA { get; set; }
        public string ValueB { get; set; }
        public string ValueC { get; set; }
        public string ValueD { get; set; }
        public string ShippingMethod { get; set; }
        public string NoOfItem { get; set; }
        public string ProductName { get; set; }
        public string ProductProfile { get; set; }
        public string ProductCategory { get; set; }
        public bool? IsSimplifiedPG { get; set; }
        //Optional Fields End
    }
}