using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleWeather.Managers.Abstract
{
	public interface IWeatherAPI
	{
		/// <summary>
		/// Sends a Get request to the specified url.
		/// </summary>
		/// <param name="url">Url of the request.</param>
		/// <returns>Http response messege of the request. Contains status code and data.</returns>
		Task<HttpResponseMessage> GetRequest(string url);

		/// <summary>
		/// Gets cities by query string.
		/// </summary>
		/// <param name="query">Name of the city.</param>
		/// <returns>Returns all the cities with that or similar name.</returns>
		string GetCitiesUrl(string query);

		/// <summary>
		/// Gets one day of daily forecast data for a specific location.
		/// </summary>
		/// <param name="key">Location key.</param>
		/// <returns>Returns daily forecast data for a specific location.</returns>
		string GetDailyForecastOneDayUrl(string key);

		/// <summary>
		/// Gets daily forecasts for the next 5 days for a specific location.
		/// </summary>
		/// <param name="key">Location key.</param>
		/// <returns>Returns HttpResponseMessage of the daily forecast data for a specific location.</returns>
		string GetDailyForecastFiveDaysUrl(string key);

		/// <summary>
		/// Gets current conditions data for a specific location.
		/// </summary>
		/// <param name="key">Location key.</param>
		/// <returns>Returns HttpResponseMessage of the current conditions data for a specific location.</returns>
		string GetCurrentConditionsUrl(string key);
	}
}
