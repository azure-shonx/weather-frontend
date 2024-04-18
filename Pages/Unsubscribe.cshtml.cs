namespace net.shonx.weather.frontend.Pages;

using Microsoft.AspNetCore.Mvc.RazorPages;
using net.shonx.weather.backend;

public class UnsubscribeModel(ILogger<UnsubscribeModel> logger) : PageModel
{
    private readonly ILogger<UnsubscribeModel> _logger = logger;
    public bool success;
    public string? email;

    public async void OnPost(string? email)
    {
        success = false;
        if (email is null)
        {
            return;
        }
        success = await HtmlHandler.WriteEmail(new Email(email, 0), false);
    }
}
