using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace SiteNinja.Middleware
{
    public static class ExceptionHandlingMiddleware
    {
        public static Task<IActionResult> Execute<T>(Func<Task<T>> operation)
        {
            return ExecuteInner(async () =>
            {
                var result = await operation();
                return new OkObjectResult(result);
            });
        }

        public static Task<IActionResult> Execute(Func<Task> operation)
        {
            return ExecuteInner(async () =>
            {
                await operation();
                return new OkResult();
            });
        }

        private static async Task<IActionResult> ExecuteInner<T>(Func<Task<T>> operation) where T : IActionResult
        {
            try
            {
                return await operation();

            }

            // 400 Bad Request - Used for validation of request headers, queries and body.
            catch (ValidationException exception)
            {
                return new BadRequestObjectResult(AsErrorResponse(exception));
            }

            // 422 Unprocessable Content - Used for validation of request headers, queries and body.
            catch (NotImplementedException exception)
            {
                return new UnprocessableEntityObjectResult(AsErrorResponse(exception));
            }

            catch (ArgumentException exception)
            {
                return new BadRequestObjectResult(AsErrorResponse(exception));
            }

            // 500 Internal Server Error
            catch (Exception exception)
            {
                return new ObjectResult(AsErrorResponse(exception))
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        private static ErrorResponse AsErrorResponse(Exception ex)
        => new(
            message: ex.Message,
            userMessage: ex.Message,
            errorDetails: ex.StackTrace);
    }
}
