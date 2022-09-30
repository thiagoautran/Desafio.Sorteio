using DoorPrize.Api.Configurations.Filters.ObjectResult;
using DoorPrize.ApplicationCore.DTOs.Response;
using DoorPrize.ApplicationCore.Exceptions;
using DoorPrize.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DoorPrize.Api.Configurations.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly IAppLogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(IAppLogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BadRequestException), BadRequestHandleException },
                { typeof(NotFoundException), NotFoundHandleException },
                { typeof(EfEntityException), EfEntityHandleException },
                { typeof(EfQueryException), EfQueryHandleException },
                { typeof(Exception), InternalServerErrorHandleException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();

            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }
        }

        private void BadRequestHandleException(ExceptionContext context)
        {
            var exception = context.Exception as BadRequestException;

            context.Result = new BadRequestObjectResult(new BadRequestResponse
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = exception?.Message
            });

            context.ExceptionHandled = true;
        }

        private void NotFoundHandleException(ExceptionContext context)
        {
            context.Result = new NotFoundObjectResult(new NotFoundResponse
            {
                Status = (int)HttpStatusCode.NotFound
            });

            context.ExceptionHandled = true;
        }

        private void InternalServerErrorHandleException(ExceptionContext context)
        {
            var exception = context.Exception;

            context.Result = new InternalServerErrorObjectResult(new InternalServerErrorResponse
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = exception?.Message
            });

            _logger.LogError($"Message={exception?.Message} - StackTrace={exception?.StackTrace}");

            context.ExceptionHandled = true;
        }

        private void EfEntityHandleException(ExceptionContext context)
        {
            var exception = context.Exception as EfEntityException;

            context.Result = new InternalServerErrorObjectResult(new InternalServerErrorResponse
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = exception?.Message
            });

            _logger.LogError($"Entity:{exception?.Entity} - Message={exception?.Message} - StackTrace={exception?.StackTrace}");

            context.ExceptionHandled = true;
        }

        private void EfQueryHandleException(ExceptionContext context)
        {
            var exception = context.Exception as EfQueryException;

            context.Result = new InternalServerErrorObjectResult(new InternalServerErrorResponse
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Message = exception?.Message
            });

            _logger.LogError($"Query:{exception?.Query} - Message={exception?.Message} - StackTrace={exception?.StackTrace}");

            context.ExceptionHandled = true;
        }
    }
}