public class Program
{
    public const string WEATHER_API_PROVIDER = "http://provider.weather.shonx.net";
    public const string WEATHER_BACKEND_PROVIDER = "http://backend.weather.shonx.net"; 
    public static void Main(string[] args) {
        new WebHandler(args);
    }
}