using System.Globalization;
using SimpleWeather.Models;
using SimpleWeather.ViewModels.Base;

namespace SimpleWeather.ViewModels
{
	public class DailyForecastViewModel : BaseViewModel
	{
		public DailyForecastViewModel(DailyForecast model, CurrentConditions conditions)
		{
			DailyForecast = model;
			_currentConditions = conditions;
		}

		private CurrentConditions _currentConditions;
		public DailyForecast DailyForecast { get; set; }

		public string DailyForecastDate => DailyForecast.Date.ToString("ddd d", CultureInfo.InvariantCulture);

		public string IconPath
		{
			get
			{
				if(_currentConditions.IsDayTime)
				{
					return "/SimpleWeather;component/Images/" + DailyForecast.Day.Icon + "-s.png"; 
				}
				else
				{
					return "/SimpleWeather;component/Images/" + DailyForecast.Night.Icon + "-s.png";
				}
			}
		}

		public string Description => _currentConditions.IsDayTime ? DailyForecast.Day.IconPhrase : DailyForecast.Night.IconPhrase;
	}
}
