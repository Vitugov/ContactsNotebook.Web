namespace ContactsNotebook.Web.Middlewares
{
    public class RequestHiddenPropertiesSupportMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestHiddenPropertiesSupportMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post && context.Request.Form.ContainsKey("_method"))
            {
                var method = context.Request.Form["_method"].ToString().ToUpper();
                if (HttpMethods.IsPut(method) || HttpMethods.IsDelete(method) || HttpMethods.IsPatch(method))
                {
                    context.Request.Method = method;
                }
            }

            await _next(context);
        }
    }

    public static class RequestHiddenPropertiesSupportMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestHiddenPropertiesSupport(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestHiddenPropertiesSupportMiddleware>();
        }
    }
}
