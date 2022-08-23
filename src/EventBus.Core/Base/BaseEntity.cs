using EventBus.Abstractions.IModels;
using EventBus.Storage.Abstractions.IRepositories;

namespace EventBus.Core.Base
{
    internal abstract class BaseEntity<TEntity> : IBaseModel, IEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        public static TEntity[] EmptyArray => Array.Empty<TEntity>();
    }
}
