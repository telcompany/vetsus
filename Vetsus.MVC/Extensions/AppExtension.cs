using Vetsus.MVC.Middlewares;

namespace Vetsus.MVC.Extensions
{
    public static class AppExtension
    {
        public static void UserErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
