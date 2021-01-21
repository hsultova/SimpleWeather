using System;

namespace SimpleWeather.Models
{
	public class CurrentConditions
	{
		public DateTime LocalObservationDateTime { get; set; }
		public long EndEpochTime { get; set; }
		public string WeatherText { get; set; }
		public int WeatherIcon { get; set; }
		public bool HasPrecipitation { get; set; }
		public string PrecipitationType { get; set; }
		public bool IsDayTime { get; set; }
		public Temperature Temperature { get; set; }
	}
}
