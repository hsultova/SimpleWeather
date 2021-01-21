using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SimpleWeather.Managers;
using SimpleWeather.Managers.Abstract;
using SimpleWeather.Models;
using SimpleWeather.Utils.Events;
using SimpleWeather.Utils.General;
using SimpleWeather.ViewModels.Base;

namespace SimpleWeather.ViewModels
{
	public class ForecastViewModel : BaseViewModel
	{
		public ForecastViewModel()
		{
			_weatherManager = new AccuWeatherApiManager();

			//TODO Find local region and search by it
			Query = "Sofia";
			OnSearch();
		}

		private IWeatherAPI _weatherManager;

		public ObservableCollection<DailyForecastViewModel> DailyForecasts { get; set; }

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

		private Headline _headline;
		public Headline Headline
		{
			get
			{
				return _headline;
			}
			set
			{
				_headline = value;
				OnPropertyChanged(nameof(Headline));
			}
		}

		private CurrentConditions _currentConditions;
		public CurrentConditions CurrentConditions
		{
			get
			{
				return _currentConditions;
			}
			set
			{
				_currentConditions = value;
				OnPropertyChanged(nameof(CurrentConditions));
			}
		}

		private string _iconPath;
		public string IconPath
		{
			get
			{
				return _iconPath;
			}
			set
			{
				_iconPath = value;
				OnPropertyChanged(nameof(IconPath));
			}
		}

		#region Commands
		public ICommand Search { get => new RelayCommand(OnSearch); }

		#endregion

		private async void OnSearch()
		{
			await FillCityData();
			await FillCurrentConditions();
			await FillForecastData();
		}

		private async Task FillForecastData()
		{
			//var url = _weatherManager.GetDailyForecastOneDayUrl(SelectedCity?.Key);
			//var response = await _weatherManager.GetRequest(url);
			//if (response.StatusCode != System.Net.HttpStatusCode.OK)
			//{
			//	OnDisplayMessage(new DisplayMessageEventArgs { Message = response.ReasonPhrase, Тype = DisplayMessageType.Error });
			//}

			//var json = await response.Content.ReadAsStringAsync();
			//var forecast = JsonConvert.DeserializeObject<DailyForecastOneDay>(json);

			DailyForecastDays forecastDays;
			using (System.IO.StreamReader r = new System.IO.StreamReader("forecast_json.json"))
			{
				var json = r.ReadToEnd();
				forecastDays = JsonConvert.DeserializeObject<DailyForecastDays>(json);
			}

			Headline = forecastDays.Headline;
			//DailyForecast = forecast.DailyForecasts.FirstOrDefault();

			List<DailyForecastViewModel> forecastsViewModel = new List<DailyForecastViewModel>();
			foreach(var forecast in forecastDays.DailyForecasts)
			{
				forecastsViewModel.Add(new DailyForecastViewModel(forecast, CurrentConditions));
			}
			DailyForecasts = new ObservableCollection<DailyForecastViewModel>(forecastsViewModel);
		}

		private async Task FillCityData()
		{
			//var url = _weatherManager.GetCitiesUrl(Query);
			//var response = await _weatherManager.GetRequest(url);
			//if (response.StatusCode != System.Net.HttpStatusCode.OK)
			//{
			//	OnDisplayMessage(new DisplayMessageEventArgs { Message = response.ReasonPhrase, Тype = DisplayMessageType.Error });
			//}

			//string json = await response.Content.ReadAsStringAsync();
			//var cities = JsonConvert.DeserializeObject<List<City>>(json);

			List<City> cities;
			using (System.IO.StreamReader r = new System.IO.StreamReader("cities_json.json"))
			{
				var json = r.ReadToEnd();
				cities = JsonConvert.DeserializeObject<List<City>>(json);
			}

			SelectedCity = cities.FirstOrDefault();
		}

		private async Task FillCurrentConditions()
		{
			//var url = _weatherManager.GetCurrentConditionsUrl(SelectedCity?.Key);
			//var response = await _weatherManager.GetRequest(url);
			//if (response.StatusCode != System.Net.HttpStatusCode.OK)
			//{
			//	OnDisplayMessage(new DisplayMessageEventArgs { Message = response.ReasonPhrase, Тype = DisplayMessageType.Error });
			//}

			//string json = await response.Content.ReadAsStringAsync();
			//var conditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json);

			List<CurrentConditions> conditions;
			using (System.IO.StreamReader r = new System.IO.StreamReader("current_conditions.json"))
			{
				var json = r.ReadToEnd();
				conditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json);
			}

			CurrentConditions = conditions.FirstOrDefault();
			IconPath = "/SimpleWeather;component/Images/" + CurrentConditions.WeatherIcon + "-s.png";
		}
	}
}
