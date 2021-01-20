using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleWeather.Managers.Abstract
{
	public interface IWeatherAPI
	{
		/// <summary>
		/// Gets cities by query string
		/// </summary>
		/// <param name="query">Name of the city</param>
		/// <returns>Returns all the cities with that or similar name</returns>
		Task<HttpResponseMessage> GetCities(string query);

		/// <summary>
		/// Gets one day of daily forecast data for a specific location
		/// </summary>
		/// <param name="key">Location key</param>
		/// <returns>Returns daily forecast data for a specific location</returns>
		Task<HttpResponseMessage> GetDailyForecastOneDay(string key);
	}
}
