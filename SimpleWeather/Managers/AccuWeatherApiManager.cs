using System.Net.Http;
using System.Threading.Tasks;
using SimpleWeather.Managers.Abstract;

namespace SimpleWeather.Managers
{
	/// <summary>
	/// Manager of the AccuWeather API: https://developer.accuweather.com/ 
	/// </summary>
	public class AccuWeatherApiManager : IWeatherAPI
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
		public async Task<HttpResponseMessage> GetCities(string query)
		{
			using (var client = new HttpClient())
			{
				string url = BaseUrl + string.Format(AutoCompleteEndpoint, APIKey, query);
				var response = await client.GetAsync(url);
				return response;
			}
		}

		/// <summary>
		/// Gets one day of daily forecast data for a specific location
		/// </summary>
		/// <param name="key">Location key</param>
		/// <returns>Returns daily forecast data for a specific location</returns>
		public async Task<HttpResponseMessage> GetDailyForecastOneDay(string key)
		{
			using (var client = new HttpClient())
			{
				string url = BaseUrl + string.Format(DailyForecastsEndpoint, key, APIKey);
				var response = await client.GetAsync(url);
				return response;
			}
		}
	}
}
