using EventBus.Abstractions.IModels;
using EventBus.Storage.Abstractions.IRepositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventBus.Core.Base
{
    internal abstract class BaseEntity<TEntity> : IBaseModel, IEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        [NotMapped]
        public static TEntity[] EmptyArray => Array.Empty<TEntity>();
    }
}
