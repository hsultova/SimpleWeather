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
		private const string CurrentConditionsEndpoint = @"/currentconditions/v1/{0}?apikey={1}";
		private const string DailyForecastsFiveDays = @"/forecasts/v1/daily/5day/{0}?apikey={1}&metric=true";

		public string GetCitiesUrl(string query)
		{
			return BaseUrl + string.Format(AutoCompleteEndpoint, APIKey, query);
		}

		public string GetDailyForecastOneDayUrl(string key)
		{
			return BaseUrl + string.Format(DailyForecastsEndpoint, key, APIKey);
		}

		public string GetDailyForecastFiveDaysUrl(string key)
		{
			return BaseUrl + string.Format(DailyForecastsFiveDays, key, APIKey);
		}

		public string GetCurrentConditionsUrl(string key)
		{
			return BaseUrl + string.Format(CurrentConditionsEndpoint, key, APIKey);
		}

		public async Task<HttpResponseMessage> GetRequest(string url)
		{
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(url);
				return response;
			}
		}
	}
}
