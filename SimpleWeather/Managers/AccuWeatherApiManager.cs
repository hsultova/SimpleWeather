using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleWeather.Models;

namespace SimpleWeather.Managers
{
	/// <summary>
	/// Manager of the AccuWeather API: https://developer.accuweather.com/ 
	/// </summary>
	public class AccuWeatherApiManager
	{
		private const string APIKey = "Otay7F6VdaA37HnpBTvOw5L6TShqWfQj";
		private const string BaseUrl = @"http://dataservice.accuweather.com";
		private const string AutoCompleteEndpoint = @"/locations/v1/cities/autocomplete?apikey={0}&q={1}";
		private const string DailyForecastsEndpoint = @"/forecasts/v1/daily/1day/{0}?apikey={1}";

		/// <summary>
		/// Gets cities by query string
		/// </summary>
		/// <param name="query">Name of the city</param>
		/// <returns>Returns all the cities with that or similar name</returns>
		public static async Task<List<City>> GetCities(string query)
		{
			List<City> cities = new List<City>();
			using (var client = new HttpClient())
			{
				string url = BaseUrl + string.Format(AutoCompleteEndpoint, APIKey, query);
				var response = await client.GetAsync(url);
				var json = await response.Content.ReadAsStringAsync();
				cities = JsonConvert.DeserializeObject<List<City>>(json);
			}
			return cities;
		}

		/// <summary>
		/// Gets one day of daily forecast data for a specific location
		/// </summary>
		/// <param name="key">Location key</param>
		/// <returns>Returns daily forecast data for a specific location</returns>
		public static async Task<DailyForecastOneDay> GetDailyForecastOneDay(string key)
		{
			DailyForecastOneDay forecast;
			using (var client = new HttpClient())
			{
				string url = BaseUrl + string.Format(DailyForecastsEndpoint, key, APIKey);
				var response = await client.GetAsync(url);
				var json = await response.Content.ReadAsStringAsync();
				forecast = JsonConvert.DeserializeObject<DailyForecastOneDay>(json);
			}
			return forecast;
		}
	}
}
