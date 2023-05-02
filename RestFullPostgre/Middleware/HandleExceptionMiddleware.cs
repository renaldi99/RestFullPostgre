using RestFullPostgre.Message;
using System.Net;

namespace RestFullPostgre.Middleware
{
    public class HandleExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (Exception e)
			{

				await HandleExceptionAsync(context, e);
			}
        }

		private Task HandleExceptionAsync(HttpContext context, Exception e)
		{
			var error = new DefaultMessage
			{
				isSuccess = false,
				message = e.Message,
			};

			switch (e)
			{
				case Exception:
					error.statusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
					break;
			}

			return context.Response.WriteAsJsonAsync(error);
		}
    }
}
