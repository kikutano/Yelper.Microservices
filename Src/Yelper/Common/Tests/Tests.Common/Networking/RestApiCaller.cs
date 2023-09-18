using Newtonsoft.Json;

namespace Tests.Common.Networking;

public static class RestApiCaller
{
    public static async Task<RestApiResponse<T>> PostAsync<T>(
        HttpClient client, string url, object request)
    {
        string json = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, httpContent);
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>(content);

        return new RestApiResponse<T>(apiResult, response);
    }

    public static async Task<RestApiResponse> PostAsync(
        HttpClient client, string url, object request)
    {
        string json = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, httpContent);
        var content = await response.Content.ReadAsStringAsync();

        return new RestApiResponse(response);
    }

    public static async Task<RestApiResponse<T>> PostAsync<T>(
        HttpClient client, string url)
    {
        string json = JsonConvert.SerializeObject("");
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync(url, httpContent);
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>(content);

        return new RestApiResponse<T>(apiResult, response);
    }

    public static async Task<RestApiResponse<T>> PutAsync<T>(
        HttpClient client, string url, object request)
    {
        string json = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PutAsync(url, httpContent);
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>(content);

        return new RestApiResponse<T>(apiResult, response);
    }

    public static async Task<RestApiResponse> PutAsync(
        HttpClient client, string url, object request)
    {
        string json = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(
            json, System.Text.Encoding.UTF8, "application/json");

        var response = await client.PutAsync(url, httpContent);
        var content = await response.Content.ReadAsStringAsync();

        return new RestApiResponse(response);
    }

    public static async Task<RestApiResponse<T>> GetAsync<T>(
        HttpClient client, string url)
    {
        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>(content);

        return new RestApiResponse<T>(apiResult!, response);
    }

    public static async Task<RestApiResponse> GetAsync(
        HttpClient client, string url)
    {
        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return new RestApiResponse(response);
    }

    public static async Task<RestApiResponse<T>> DeleteAsync<T>(
        HttpClient client, string url)
    {
        var response = await client.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        var apiResult = JsonConvert.DeserializeObject<T>(content);

        return new RestApiResponse<T>(apiResult, response);
    }

    public static async Task<RestApiResponse> DeleteAsync(
        HttpClient client, string url)
    {
        var response = await client.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        return new RestApiResponse(response);
    }
}
