namespace net.shonx.weather.frontend.Pages;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using net.shonx.weather.backend;

public class IndexModel : PageModel
{
    public WeatherForecast? Forecast { get; set; }
    private readonly HttpClient httpClient = new();

    public async Task OnGet(string? zipcode)
    {
        if (zipcode is null)
        {
            return;
        }
        int iZip;
        try
        {
            iZip = int.Parse(zipcode);
        }
        catch (FormatException) { return; }
        Forecast = await HtmlHandler.GetWeather(iZip);
    }

    public IActionResult OnPost(int zipcode)
    {
        return RedirectToPage("./Index", new { zipcode = zipcode });
    }
}
