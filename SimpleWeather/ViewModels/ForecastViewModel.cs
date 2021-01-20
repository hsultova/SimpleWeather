using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SimpleWeather.Managers;
using SimpleWeather.Managers.Abstract;
using SimpleWeather.Models;
using SimpleWeather.Utils.General;
using SimpleWeather.ViewModels.Base;

namespace SimpleWeather.ViewModels
{
	public class ForecastViewModel : BaseViewModel
	{
		public ForecastViewModel()
		{
			_weatherManager = new AccuWeatherApiManager();
		}

		private IWeatherAPI _weatherManager;

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
			await FillForecastData();

			IconPath = "/SimpleWeather;component/Images/" + DailyForecast.Day.Icon + "-s.png";
		}

		private async Task FillForecastData()
		{
			//var response = await _weatherManager.GetDailyForecastOneDay(SelectedCity?.Key);
			//if (response.StatusCode != System.Net.HttpStatusCode.OK)
			//{
			//	OnDisplayMessage(new DisplayMessageEventArgs { Message = response.ReasonPhrase, Тype = DisplayMessageType.Error });
			//}

			//var json = await response.Content.ReadAsStringAsync();
			//var forecast = JsonConvert.DeserializeObject<DailyForecastOneDay>(json);

			DailyForecastOneDay forecast;
			using (System.IO.StreamReader r = new System.IO.StreamReader("forecast_json.json"))
			{
				var json = r.ReadToEnd();
				forecast = JsonConvert.DeserializeObject<DailyForecastOneDay>(json);
			}

			Headline = forecast.Headline;
			DailyForecast = forecast.DailyForecasts.FirstOrDefault();
		}

		private async Task FillCityData()
		{
			//var response = await _weatherManager.GetCities(Query);
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
	}
}
