using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace weather_consumer.Pages;

public class SubscribeModel : PageModel
{
    private readonly ILogger<SubscribeModel> _logger;

    public SubscribeModel(ILogger<SubscribeModel> logger)
    {
        _logger = logger;
    }

    public async void OnPost(string? email)
    {
        if (email is null)
        {
            return;
        }
        RegisterEmail(email);
    }
    public async void RegisterEmail(string? email)
    {
        var emails = GetEmails();
        emails.Add(email);
        SaveEmails(emails);
    }

    private List<string> GetEmails()
    {
        List<string> emails = (List<string>)InternalGetEmails().Result.Value;
        return emails;
    }


    private async Task<JsonResult> InternalGetEmails()
    {
        List<string> emails = new List<string>();
        using (var httpClient = new HttpClient())
        {
            using (HttpResponseMessage response = await httpClient.GetAsync("STORAGE"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                emails = JsonConvert.DeserializeObject<List<string>>(apiResponse);
            }
        }
        return new JsonResult(emails);
    }

    private async void SaveEmails(List<string> emails)
    {

    }
}
