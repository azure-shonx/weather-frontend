using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public abstract class SharedEmailPage : PageModel
{

    private static readonly HttpClient httpClient = new HttpClient();

    public async void SaveEmail(Email email)
    {
        WriteEmail(email, true);
    }

    public async void RemoveEmail(Email email)
    {
        WriteEmail(email, false);
    }

    private async void WriteEmail(Email email, bool isSubbing)
    {
        string json = JsonConvert.SerializeObject(email);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        string URL = isSubbing ? Program.WEATHER_BACKEND_PROVIDER + "/emails/add/" : Program.WEATHER_BACKEND_PROVIDER + "/emails/remove/";

        using (HttpResponseMessage response = await httpClient.PutAsync(URL, data))
        {
            var resC = response.Content;
            var statusCode = response.StatusCode;
            if (resC is not null)
            {
                Console.Write((await resC.ReadAsStringAsync()));
            }
            if ((int)statusCode != 200)
            {
                Console.Write("Got status code " + statusCode + ".");
            }
        }
    }
}