using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public class SubscribeModel : SharedEmailPage
{
    private readonly ILogger<SubscribeModel> _logger;

    public string? email;
    public int zipcode;

    public SubscribeModel(ILogger<SubscribeModel> logger)
    {
        _logger = logger;
    }

    public async void OnPost(string email, int zipcode)
    {
        if (email is null)
            return;
        this.email = email;
        this.zipcode = zipcode;
        SaveEmail(new Email(email, zipcode));
    }
}
