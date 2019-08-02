namespace MiniWebServer.Context
{
    public class HttpContext
    {
        public HttpRequest Request { get; set; }

        public HttpResponse Response { get; set; }

        public HttpContext(string requestText)
        {
            Request = new HttpRequest(requestText);
            Response = new HttpResponse();
        }
    }
}
