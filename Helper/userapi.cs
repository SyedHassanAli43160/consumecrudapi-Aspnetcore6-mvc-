namespace consumeapi.Helper
{
    public class userapi
    {
       public HttpClient initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5087");
            return client;
        }
    }
}
