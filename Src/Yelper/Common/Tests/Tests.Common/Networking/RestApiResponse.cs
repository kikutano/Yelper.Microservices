namespace Tests.Common.Networking;

public class RestApiResponse
{
    public HttpResponseMessage Response { get; }

    public RestApiResponse(HttpResponseMessage response)
    {
        Response = response;
    }
}

public class RestApiResponse<T> : RestApiResponse
{
    public T Value { get; }

    public RestApiResponse(T value, HttpResponseMessage response) : base(response)
    {
        Value = value;
    }
}
