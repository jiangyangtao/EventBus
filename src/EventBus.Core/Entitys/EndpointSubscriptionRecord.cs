using EventBus.Abstractions.Enums;
using EventBus.Abstractions.IModels;
using EventBus.Core.Base;
using EventBus.Extensions;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Entitys
{
    public class EndpointSubscriptionRecord : BaseEntity<EndpointSubscriptionRecord>, IEndpointSubscriptionRecord
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid EventRecordSubscriptionId { set; get; }

        public SubscriptionType SubscriptionType { get; set; } = SubscriptionType.Automatic;

        public DateTime SubscriptionTime { set; get; }

        public DateTime ResponseTime { set; get; }

        public bool IsSuccessStatusCode { set; get; }

        public string ResponseStatus { set; get; }

        [NotMapped]
        public IDictionary<string, string> ResponseHeaders
        {
            set
            {
                if (value.NotNullAndEmpty()) ResponseHeadersContent = JsonConvert.SerializeObject(value, Formatting.Indented);
                else ResponseHeadersContent = string.Empty;
            }
            get
            {
                if (ResponseHeadersContent.IsNullOrEmpty()) return null;

                return JsonConvert.DeserializeObject<IDictionary<string, string>>(ResponseHeadersContent);
            }
        }

        public string ResponseHeadersContent { set; get; }

        public string ResponseStatusCode { set; get; }

        public string ResponseContent { set; get; }

        public long UsageTime { set; get; }

        /// <summary>
        /// 订阅
        /// </summary>
        [NotMapped]
        public ISubscription Subscription { set; get; }
    }
}
