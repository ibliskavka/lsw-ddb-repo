using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Lsw.Ddb.Repo.Tests.Model
{
    public class ShopUsage : IBaseModel
    {
        public string Pk { get; private set; }
        public string Sk { get; private set; }

        public string ShopId { get; set; }
        public string OrderId { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Amazon Connect Contact Id
        /// </summary>
        public string ContactId { get; set; }

        /// <summary>
        /// Shopify Webhook Body
        /// </summary>
        public object Request { get; set; }

        /// <summary>
        /// AWS Connect CTR
        /// </summary>
        public object Response { get; set; }


        public decimal OrderTotal { get; set; }

        public ShopUsage()
        {
        }

        public ShopUsage(string shopId, long orderId, string phoneNumber)
            : this(shopId, orderId.ToString(), phoneNumber)
        {

        }


        public ShopUsage(string shopId, string orderId, string phoneNumber)
        {
            Pk = BuildPartitionKey(DateTime.Today.ToString("yyyyMMdd"));
            Sk = BuildSortKey(shopId, orderId);

            ShopId = shopId;
            OrderId = orderId;
            PhoneNumber = phoneNumber;

            CreatedOn = DateTime.UtcNow;
        }

        public static string BuildPartitionKey(string billingDate)
        {
            if (billingDate == null)
            {
                throw new ArgumentNullException(nameof(billingDate));
            }

            return $"usage#{billingDate}";
        }

        public static string BuildSortKey(string shopId, long id)
        {
            return BuildSortKey(shopId, id.ToString());
        }

        public static string BuildSortKey(string shopId, string id)
        {
            if (string.IsNullOrWhiteSpace(shopId))
            {
                throw new ArgumentNullException(nameof(shopId));
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            return $"{shopId}#{id}";
        }

        public bool IsValid()
        {
            return true;
        }
    }
}