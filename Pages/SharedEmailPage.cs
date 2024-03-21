using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public abstract class SharedEmailPage : PageModel
{

    protected readonly ILogger _logger;

    public SharedEmailPage(ILogger logger)
    {
        _logger = logger;
    }

    private static readonly HttpClient httpClient = new HttpClient();

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
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        _logger.LogInformation("Sending payload of " + data.ToString());
        _logger.LogInformation("Raw json is " + json);

        string URL = isSubbing ? Program.WEATHER_BACKEND_PROVIDER + "emails/add/" : Program.WEATHER_BACKEND_PROVIDER + "emails/remove/";

        using (HttpResponseMessage response = await httpClient.PutAsync(URL, data))
        {
            var resC = response.Content;
            var statusCode = response.StatusCode;
            if (resC is not null)
            {
                _logger.LogInformation((await resC.ReadAsStringAsync()));
                return false;
            }
            if ((int)statusCode != 200)
            {
                _logger.LogInformation("Got status code " + statusCode + ".");
                return false;
            }
        }
        return true;
    }
}