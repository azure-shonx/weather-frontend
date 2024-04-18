namespace net.shonx.weather.frontend;

using System.Text;
using net.shonx.weather.backend;
using Newtonsoft.Json;

internal static class HtmlHandler
{
    private static readonly HttpClient httpClient = new();

    internal static async Task<WeatherForecast?> GetWeather(int zipcode)
    {
        using HttpResponseMessage response = await httpClient.GetAsync(Program.BACKEND + "weather/get/" + zipcode);
        string apiResponse = await response.Content.ReadAsStringAsync();
        if (string.IsNullOrEmpty(apiResponse))
            throw new NullReferenceException("Provider response is empty. Is it online?");
        return JsonConvert.DeserializeObject<WeatherForecast>(apiResponse);
    }

    internal static async Task<bool> WriteEmail(Email email, bool isSubbing)
    {
        string json = JsonConvert.SerializeObject(email);
        StringContent data = new(json, Encoding.UTF8, "application/json");

        string URL = isSubbing ? Program.BACKEND + "emails/add/" : Program.BACKEND + "emails/remove/";

        HttpRequestMessage request = new(HttpMethod.Put, URL)
        {
            Content = data
        };

        using HttpResponseMessage response = await httpClient.SendAsync(request);
        var statusCode = response.StatusCode;
        if ((int)statusCode != 200)
            return false;
        return true;
    }
}