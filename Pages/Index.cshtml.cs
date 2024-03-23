using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace weather_consumer.Pages;

public class IndexModel : PageModel
{
    public WeatherForecast Forecast { get; set; }
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }


    public void OnGet(string? zipcode)
    {
        if (zipcode is null)
        {
            return;
        }
        int iZip;
        try
        {
            iZip = Int32.Parse(zipcode);
        }
        catch (FormatException e)
        {
            return;
        }
        Forecast = GetWeather(iZip);
    }

    public async Task<IActionResult> OnPost(int zipcode)
    {
        return RedirectToPage("./Index", new { zipcode = zipcode });
    }
    public WeatherForecast GetWeather(int zipcode)
    {
        return (WeatherForecast)(GetWeather0(zipcode).Result);
    }


    async Task<WeatherForecast> GetWeather0(int zipcode)
    {
        using (var httpClient = new HttpClient())
        {
            using (HttpResponseMessage response = await httpClient.GetAsync(Program.WEATHER_BACKEND_PROVIDER + "weather/get/" + zipcode))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (String.IsNullOrEmpty(apiResponse))
                    throw new NullReferenceException("Provider response is empty. Is it online?");
                return JsonConvert.DeserializeObject<WeatherForecast>(apiResponse);
            }
        }
    }

}
