namespace ApiClient;

public static class ApiClient
{
    public static HttpClient HttpClient { get; set; }

    public static void SetHttpClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }
}