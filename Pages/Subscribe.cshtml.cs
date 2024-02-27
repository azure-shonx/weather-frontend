using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public class SubscribeModel : PageModel
{
    private readonly ILogger<SubscribeModel> _logger;

    static readonly HttpClient httpClient = new HttpClient();

    private readonly string BLOB_URL = "https://weathertrackerb951.blob.core.windows.net/emails/emails.json?si=weather-consumer&spr=https&sv=2022-11-02&sr=b&sig=5mlLJgTnkDpL0AB27%2B9QfEu6dOGsv49WlIt%2BaBRUyXw%3D";

    public string? email;

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
        this.email = email;
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
        return (List<string>)InternalGetEmails().Result.Value;
    }


    private async Task<JsonResult> InternalGetEmails()
    {
        List<string> emails = new List<string>();

        using (HttpResponseMessage response = await httpClient.GetAsync(BLOB_URL))
        {
            response.EnsureSuccessStatusCode();
            string apiResponse = await response.Content.ReadAsStringAsync();
            emails = JsonConvert.DeserializeObject<List<string>>(apiResponse);
        }

        return new JsonResult(emails);
    }

    private async void SaveEmails(List<string> emails)
    {
        string json = JsonConvert.SerializeObject(emails);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        data.Headers.Add("x-ms-blob-type", "BlockBlob");

        using (HttpResponseMessage response = await httpClient.PutAsync(BLOB_URL, data))
        {
            var resC = response.Content;
            if (resC is not null)
            {
                Console.Write((await resC.ReadAsStringAsync()));
            }
        }

    }

}
