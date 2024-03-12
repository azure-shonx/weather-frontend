using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public class UnsubscribeModel : SharedEmailPage
{
    private readonly ILogger<UnsubscribeModel> _logger;

    public string? email;

    public UnsubscribeModel(ILogger<UnsubscribeModel> logger)
    {
        _logger = logger;
    }

    public async void OnPost(string? email)
    {
        if (email is null)
        {
            return;
        }
        RemoveEmail(new Email(email));
    }


}
