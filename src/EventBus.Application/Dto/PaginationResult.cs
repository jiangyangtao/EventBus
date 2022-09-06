﻿using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Dto
{
    public class PaginationResult<T>
    {
        public PaginationResult(long count)
        {
            TotalCount = count;
        }

        /// <summary>
        /// 数据总量
        /// </summary>
        [Required]
        public long TotalCount { get; }

        public T[] List { set; get; }
    }
}
