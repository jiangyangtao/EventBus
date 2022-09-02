using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 事件记录
    /// </summary>
    public interface IEventRecord : IBaseModel
    {
        /// <summary>
        /// 事件记录 Id
        /// </summary>
        public Guid EventRecordId { get;  }

        /// <summary>
        /// 查询参数
        /// </summary>
        public string QueryString {  get; }

        /// <summary>
        /// 头信息
        /// </summary>
        public IDictionary<string, string> Header {  get; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data {get; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime {  get; }

        /// <summary>
        /// 订阅完成率
        /// </summary>
        public decimal SubscriptionCompletionRate {  get; }

        /// <summary>
        /// 事件
        /// </summary>
        public IEvent Event { get; }

        /// <summary>
        /// 事件的订阅记录
        /// </summary>
        public ISubscriptionRecord[] ISubscriptionRecords {  get; }

        public HttpContent BuilderHttpContent()
        {
            var content = string.Empty;
            if (Data != null) content = JsonConvert.SerializeObject(Data);

            return new StringContent(content, Encoding.UTF8, "application/json");
        }

    }
}
