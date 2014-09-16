using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSBusiness
{
    
    public class ReportFields
    {

        public int VersionId { get; set; }
        public int CatgoryId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public decimal UniqueVisitors { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrder { get; set; }
        public decimal Conversion { get; set; }
        public decimal RevenuePerVisit { get; set; }   

        public string CategoryTitle { get; set; }
        public int orderId { get; set; }
        public string OrderStatus { get; set; }
        public string Affiliate { get; set; }
        public string AuthorizationCode { get; set; }
        public string TransactionCode { get; set; }

        public string TransactionDate { get; set; }
        public string BillingName { get; set; }

        public int? TnTCampaignId { get; set; }
        public int? TnTExperienceId { get; set; }
        public int OrderStatusId { get; set; }
    }
}
