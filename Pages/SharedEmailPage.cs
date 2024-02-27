using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public abstract class SharedEmailPage : PageModel
{

    private static readonly HttpClient httpClient = new HttpClient();


    public readonly string BLOB_URL = "https://weathertrackerb951.blob.core.windows.net/emails/emails.json?si=weather-consumer&spr=https&sv=2022-11-02&sr=b&sig=5mlLJgTnkDpL0AB27%2B9QfEu6dOGsv49WlIt%2BaBRUyXw%3D";

    public List<string> GetEmails()
    {
        return (List<string>)GetEmails0().Result.Value;
    }


    private async Task<JsonResult> GetEmails0()
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

    public async void SaveEmails(List<string> emails)
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