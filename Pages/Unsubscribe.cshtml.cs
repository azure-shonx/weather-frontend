using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public class UnsubscribeModel : SharedEmailPage
{
    public bool success;

    public string? email;

    public UnsubscribeModel(ILogger<UnsubscribeModel> logger) : base(logger)
    {
    }

    public async void OnPost(string? email)
    {
        success = false;
        if (email is null)
        {
            return;
        }
        success = await RemoveEmail(new Email(email, 0));
    }


}
