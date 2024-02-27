using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public class SubscribeModel : SharedEmailPage
{
    private readonly ILogger<SubscribeModel> _logger;

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

}
