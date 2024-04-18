using net.shonx.weather.frontend.web;

public class Program
{
    public const string WEATHER_BACKEND_PROVIDER = "https://backend.weather.shonx.net/";

    public static void Main(string[] args)
    {
        WebHandler web = new(args);
        web.Run();
    }
}