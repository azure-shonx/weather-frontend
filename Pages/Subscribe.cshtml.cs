using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace weather_consumer.Pages;

public class SubscribeModel : SharedEmailPage
{
    public bool success;
    public string? email;
    public int zipcode;

    public SubscribeModel(ILogger<SubscribeModel> logger) : base(logger)
    {
    }

    public void OnPost(string email, int zipcode)
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
        success = SaveEmail(new Email(email, zipcode)).Result;
    }

}
