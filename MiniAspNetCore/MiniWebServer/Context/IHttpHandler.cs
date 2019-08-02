namespace MiniWebServer.Context
{
    public interface IHttpHandler
    {
        void ProcessRequest(HttpContext context);
    }
}
