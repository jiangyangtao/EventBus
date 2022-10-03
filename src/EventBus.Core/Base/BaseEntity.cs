using EventBus.Abstractions.IModels;
using EventBus.Storage.Abstractions.IRepositories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace EventBus.Core.Base
{
    public abstract class BaseEntity<TEntity> : IBaseModel, IEntity
    {
        private static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

        public BaseEntity()
        {
            Id = Create(SequentialGuidType.SequentialAsString);
        }

        private static Guid Create(SequentialGuidType guidType)
        {
            var randomBytes = new byte[10];
            RandomNumberGenerator.GetBytes(randomBytes);

            var timestamp = DateTime.UtcNow.Ticks / 10000L;
            var timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:

                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }

                    break;

                case SequentialGuidType.SequentialAtEnd:

                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }

        [Key]
        public Guid Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        [NotMapped]
        public static TEntity[] EmptyArray => Array.Empty<TEntity>();
    }

    /// <summary>
    /// Sequential Guid Type
    /// </summary>
    internal enum SequentialGuidType
    {
        /// <summary>
        /// Sequential As String
        /// </summary>
        SequentialAsString,
        /// <summary>
        /// Sequential As Binary
        /// </summary>
        SequentialAsBinary,
        /// <summary>
        /// Sequential A tEnd
        /// </summary>
        SequentialAtEnd
    }
}
