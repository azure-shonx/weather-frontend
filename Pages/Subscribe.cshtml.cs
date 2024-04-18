namespace net.shonx.weather.frontend.Pages;

using Microsoft.AspNetCore.Mvc.RazorPages;
using net.shonx.weather.backend;

public class SubscribeModel(ILogger<SubscribeModel> logger) : PageModel
{
    private readonly ILogger<SubscribeModel> _logger = logger;
    public bool success;
    public string? email;
    public int zipcode;
    
    public async Task OnPost(string email, int zipcode)
    {
        success = false;
        if (email is null)
        {
            return;
        }
        if (zipcode <= 0)
        {
            return;
        }
        this.email = email;
        this.zipcode = zipcode;
        success = await HtmlHandler.WriteEmail(new Email(email, zipcode), true);
    }
}
