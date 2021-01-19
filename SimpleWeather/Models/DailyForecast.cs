using System;

namespace SimpleWeather.Models
{
	public class DailyForecast
	{
		public DateTime Date { get; set; }
		public int EpochDate { get; set; }
		public Temperature Temperature { get; set; }
		public ForecastView Day { get; set; }
		public ForecastView Night { get; set; }
	}
}
