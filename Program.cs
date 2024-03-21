public class Program
{
    public const string WEATHER_API_PROVIDER = "https://provider.weather.shonx.net/";
    public const string WEATHER_BACKEND_PROVIDER = "https://backend.weather.shonx.net/";

    public const int REVISION = 3;
    public static void Main(string[] args)
    {
        Console.WriteLine("Running revision " + REVISION);
        new WebHandler(args);
    }
}