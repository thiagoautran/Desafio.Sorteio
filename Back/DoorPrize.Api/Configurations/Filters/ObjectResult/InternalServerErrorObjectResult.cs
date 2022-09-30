namespace DoorPrize.Api.Configurations.Filters.ObjectResult
{
    public class InternalServerErrorObjectResult : Microsoft.AspNetCore.Mvc.ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        public InternalServerErrorObjectResult(object value) : base(value) =>
            StatusCode = DefaultStatusCode;
    }
}