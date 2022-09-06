using EventBus.Abstractions.IModels;
using System.ComponentModel.DataAnnotations;

namespace EventBus.Application.Dto
{
    public class ApplicationDtoBase
    {
        [Required]
        public Guid applicationId { get; set; }
    }

    public class ApplicationDto
    {
        [Required, MaxLength(50)]
        public string applicationName { get; set; }
    }

    public class ApplicationQueryDto : PagingParameter
    {
        public string applicationName { get; set; }
    }

    public class ApplicationResult : ApplicationDto
    {
        public ApplicationResult(IApplication application)
        {
            applicationId = application.Id;
            applicationName = application.ApplicationName;
        }

        [Required]
        public Guid applicationId { get; set; }
    }

    public class ApplicationPaginationResult : PaginationResult<ApplicationResult>
    {
        public ApplicationPaginationResult(long count, IApplication[] applications) : base(count)
        {
            List = applications.Select(a => new ApplicationResult(a)).ToArray();
        }
    }
}
