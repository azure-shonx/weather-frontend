public class Program
{
    public const string WEATHER_API_PROVIDER = "https://provider.weather.shonx.net/";
    public const string WEATHER_BACKEND_PROVIDER = "https://backend.weather.shonx.net/";
    public static void Main(string[] args)
    {
        new WebHandler(args);
    }
}