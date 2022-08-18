using EventBus.Abstractions.IModels;
using EventBus.Storage.Abstractions.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Infrastructure.Entitys
{
    internal abstract class BaseEntity : IBaseModel, IEntity
    {
        public Guid Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }
    }
}
