using System.Collections.Generic;

namespace SimpleWeather.Models
{
	public class DailyForecastOneDay
	{
		public Headline Headline { get; set; }
		public List<DailyForecast> DailyForecasts { get; set; }
	}
}
