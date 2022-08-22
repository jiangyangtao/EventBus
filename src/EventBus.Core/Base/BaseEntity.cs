using EventBus.Abstractions.IModels;
using EventBus.Storage.Abstractions.IRepositories;

namespace EventBus.Core.Base
{
    internal abstract class BaseEntity<TEntity> : IBaseModel, IEntity
    {
        internal BaseEntity() { }

        internal BaseEntity(IBaseModel model)
        {
            Id = model.Id;
            CreateTime = model.CreateTime;
            UpdateTime = model.UpdateTime;
        }

        public Guid Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        public static TEntity[] EmptyArray => Array.Empty<TEntity>();
    }
}
