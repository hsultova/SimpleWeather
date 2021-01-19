using System;
using System.Linq;
using System.Windows.Input;
using SimpleWeather.Managers;
using SimpleWeather.Models;
using SimpleWeather.Utils.General;
using SimpleWeather.ViewModels.Base;

namespace SimpleWeather.ViewModels
{
	public class ForecastViewModel : BaseViewModel
	{
		public ForecastViewModel()
		{

		}

		private string _query;
		public string Query
		{
			get
			{
				return _query;
			}
			set
			{
				_query = value;
				OnPropertyChanged(nameof(Query));
			}
		}

		private City _selectedCity;
		public City SelectedCity
		{
			get
			{
				return _selectedCity;
			}
			set
			{
				_selectedCity = value;
				OnPropertyChanged(nameof(SelectedCity));
			}
		}

		private DailyForecast _dailyForecast;
		public DailyForecast DailyForecast
		{
			get
			{
				return _dailyForecast;
			}
			set
			{
				_dailyForecast = value;
				OnPropertyChanged(nameof(DailyForecast));
			}
		}

		private string _icon;
		public string Icon
		{
			get
			{
				return _icon;
			}
			set
			{
				_icon = value;
				OnPropertyChanged(nameof(Icon));
			}
		}

		#region Commands
		public ICommand Search { get => new RelayCommand(OnSearch); }

		#endregion

		private async void OnSearch()
		{
			var cities = await AccuWeatherApiManager.GetCities(Query);
			SelectedCity = cities.FirstOrDefault();
			var dailyForecastOneDay = await AccuWeatherApiManager.GetDailyForecastOneDay(SelectedCity.Key);
			//Use FirstOrDefault daily forecast to test. Change this.
			Icon = "/SimpleWeather;component/Images/" + dailyForecastOneDay.DailyForecasts.FirstOrDefault().Day.Icon + "-s.png";
		}
	}
}
