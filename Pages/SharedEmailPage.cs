using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace weather_consumer.Pages;

public abstract class SharedEmailPage : PageModel
{
    private static readonly HttpClient httpClient = new HttpClient();

    protected readonly ILogger _logger;

    public SharedEmailPage(ILogger logger)
    {
        _logger = logger;
    }


    public Task<bool> SaveEmail(Email email)
    {
        return WriteEmail(email, true);
    }

    public Task<bool> RemoveEmail(Email email)
    {
        return WriteEmail(email, false);
    }

    private async Task<bool> WriteEmail(Email email, bool isSubbing)
    {
        string json = JsonConvert.SerializeObject(email);
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

        string URL = isSubbing ? Program.WEATHER_BACKEND_PROVIDER + "emails/add/" : Program.WEATHER_BACKEND_PROVIDER + "emails/remove/";

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, URL);
        request.Content = data;

        using (HttpResponseMessage response = await httpClient.SendAsync(request))
        {
            var statusCode = response.StatusCode;
            if ((int)statusCode != 200)
            {
                _logger.LogInformation("Got status code " + (int)statusCode + ".");
                return false;
            }
        }
        return true;
    }
}