using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace RouteSample
{
    public class ProductsLinkMiddleware
    {
        private readonly LinkGenerator _linkGenerator;

        public ProductsLinkMiddleware(RequestDelegate next,LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var url = _linkGenerator.GetPathByAction("Index", "Home");

            httpContext.Response.ContentType = "text/plain";

            await httpContext.Response.WriteAsync($"Go to {url} to see our products.");
        }
    }
}