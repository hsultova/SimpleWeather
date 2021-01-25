namespace SimpleWeather.Utils
{
	/// <summary>
	/// Used for deserialization of the response when the status code is different from OK/success
	/// </summary>
	public class WeatherAPIHttpResponse
	{
		public string Code { get; set; }
		public string Message { get; set; }
	}
}
