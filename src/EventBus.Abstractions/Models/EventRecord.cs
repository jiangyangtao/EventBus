using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.Models
{
    /// <summary>
    /// 事件记录
    /// </summary>
    public class EventRecord : BaseModel
    {
        /// <summary>
        /// 事件记录 Id
        /// </summary>
        public Guid EventRecordId { get; set; }

        /// <summary>
        /// 查询参数
        /// </summary>
        public string QueryString { set; get; }

        /// <summary>
        /// 头信息
        /// </summary>
        public IDictionary<string, string> Header { set; get; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { set; get; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { set; get; }

        /// <summary>
        /// 订阅完成率
        /// </summary>
        public decimal SubscriptionCompletionRate { set; get; }

        /// <summary>
        /// 事件
        /// </summary>
        public Event Event { set; get; }

        /// <summary>
        /// 事件的订阅记录
        /// </summary>
        public SubscriptionRecord[] ISubscriptionRecords { set; get; }

        public HttpContent BuilderHttpContent()
        {

        }

    }
}
