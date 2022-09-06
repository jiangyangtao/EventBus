using EventBus.Abstractions.IModels;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Dto
{
    public class ApplicationDtoBase
    {
        [Required]
        public Guid ApplicationId { get; set; }
    }

    public class ApplicationDto
    {
        [Required, MaxLength(50)]
        public string ApplicationName { get; set; }
    }

    public class ApplicationQueryDto : PagingParameter
    {
        public string ApplicationName { get; set; }
    }

    public class ApplicationResult : ApplicationDto
    {
        public ApplicationResult(IApplication application)
        {
            ApplicationId = application.Id;
            ApplicationName = application.ApplicationName;
        }

        [Required]
        public Guid ApplicationId { get; set; }
    }

    public class ApplicationPaginationResult : PaginationResult<ApplicationResult>
    {
        public ApplicationPaginationResult(long count, IApplication[] applications) : base(count)
        {
            List = applications.Select(a => new ApplicationResult(a)).ToArray();
        }
    }
}
