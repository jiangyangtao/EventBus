﻿using EventBus.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Abstractions.IModels
{
    /// <summary>
    /// 订阅记录
    /// </summary>
    public interface ISubscriptionRecord : IBaseModel
    {
        /// <summary>
        /// 接入点名称
        /// </summary>
        public string EndpointName { get; }

        /// <summary>
        /// 接入点地址
        /// </summary>
        public Uri EndpointUrl { get; }

        /// <summary>
        /// 通知协议
        /// </summary>
        public ProtocolType NoticeProtocol { get; }

        /// <summary>
        /// 失败的重试策略
        /// </summary>
        public IRetryPolicy[] FailedRetryPolicy { get; }

        /// <summary>
        /// 订阅结果，true 成功，false 失败
        /// </summary>
        public bool SubscriptionResult { get; }

        /// <summary>
        /// 接入点的订阅记录
        /// </summary>
        public IEndpointSubscriptionRecord[] EndpointSubscriptionRecords { get; }

        /// <summary>
        /// 获取重试策略
        /// </summary>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public IRetryPolicy GetRetryPolicy(int retryCount = 1);
    }
}
