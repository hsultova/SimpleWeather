namespace SimpleWeather.Models
{
	public class Temperature
	{
		public TemperatureScale Metric { get; set; }
		public TemperatureScale Imperial { get; set; }
		public TemperatureScale Minimum { get; set; }
		public TemperatureScale Maximum { get; set; }
	}
}
