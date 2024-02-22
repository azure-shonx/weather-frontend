using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace weather_consumer.Pages;

public class IndexModel : PageModel
{
    public WeatherForecast Forecast { get; set;}
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    
    public void OnGet(string? zipcode)
    {
        if(zipcode is null) 
        {
            return;
        }
        int iZip;
        try {
            iZip = Int32.Parse(zipcode);
        } catch (FormatException e) {
            return;
        }
        Forecast = GetWeather(iZip);
    }

    public async Task<IActionResult> OnPost(int zipcode)
   {
        Console.WriteLine("Got zipcode in Post!");
        return RedirectToPage("./Index", new {zipcode = zipcode});
    }
    public WeatherForecast GetWeather(int zipcode)
    {
        return (WeatherForecast)(OnGetWeather(zipcode).Result).Value;
    }


    
    async Task<JsonResult> OnGetWeather(int zipcode)
    {
        WeatherForecast weather;
        using (var httpClient = new HttpClient()) {              
            using (HttpResponseMessage response = await httpClient.GetAsync("https://weatherprovider.thankfulplant-b33b4202.eastus.azurecontainerapps.io/" + zipcode))
            {                   
                string apiResponse = await response.Content.ReadAsStringAsync();
                weather = JsonConvert.DeserializeObject<WeatherForecast>(apiResponse);                   
            }
        }
        return new JsonResult(weather);
    }

    public record WeatherForecast(int zipcode, int temperature, string summary, bool isRainy) {}
}
