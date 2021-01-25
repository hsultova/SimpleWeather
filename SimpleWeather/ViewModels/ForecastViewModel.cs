using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using SimpleWeather.Managers;
using SimpleWeather.Managers.Abstract;
using SimpleWeather.Models;
using SimpleWeather.Utils;
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

			//TODO Refactor this
			var themeManager = new ThemeManager();

			if (CurrentConditions == null)
			{
				//Use this theme as default.
				var theme = themeManager.GetTheme("DayTheme");
				themeManager.ChangeTheme(theme);
				return;
			}

			if (CurrentConditions.IsDayTime)
			{
				var theme = themeManager.GetTheme("DayTheme");
				themeManager.ChangeTheme(theme);
			}
			else
			{
				var theme = themeManager.GetTheme("NightTheme");
				themeManager.ChangeTheme(theme);
			}
		}

		private IWeatherAPI _weatherManager;

		private ObservableCollection<DailyForecastViewModel> _dailyForecasts;
		public ObservableCollection<DailyForecastViewModel> DailyForecasts
		{
			get
			{
				return _dailyForecasts;
			}
			set
			{
				_dailyForecasts = value;
				OnPropertyChanged(nameof(DailyForecasts));
			}
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

			Query = string.Empty;
		}

		private async Task FillForecastData()
		{
			var url = _weatherManager.GetDailyForecastFiveDaysUrl(SelectedCity?.Key);
			var response = await _weatherManager.GetRequest(url);
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				string responeJson = await response.Content.ReadAsStringAsync();
				DisplayError(responeJson);
				return;
			}

			var json = await response.Content.ReadAsStringAsync();
			var forecastDays = JsonConvert.DeserializeObject<DailyForecastDays>(json);

			List<DailyForecastViewModel> forecastsViewModel = new List<DailyForecastViewModel>();
			if (CurrentConditions == null)
			{
				return;
			}

			foreach (var forecast in forecastDays.DailyForecasts)
			{
				forecastsViewModel.Add(new DailyForecastViewModel(forecast, CurrentConditions));
			}

			Headline = forecastDays.Headline;
			DailyForecasts = new ObservableCollection<DailyForecastViewModel>(forecastsViewModel);
		}

		private async Task FillCityData()
		{
			var url = _weatherManager.GetCitiesUrl(Query);
			var response = await _weatherManager.GetRequest(url);
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				string responeJson = await response.Content.ReadAsStringAsync();
				DisplayError(responeJson);
				return;
			}

			string json = await response.Content.ReadAsStringAsync();
			var cities = JsonConvert.DeserializeObject<List<City>>(json);

			SelectedCity = cities.FirstOrDefault();
		}

		private void DisplayError(string response)
		{
			var deserializedResponse = JsonConvert.DeserializeObject<WeatherAPIHttpResponse>(response);
			OnDisplayMessage(new DisplayMessageEventArgs { Message = $"{deserializedResponse.Message} Code: {deserializedResponse.Code}", Тype = DisplayMessageType.Error });
		}

		private async Task FillCurrentConditions()
		{
			var url = _weatherManager.GetCurrentConditionsUrl(SelectedCity?.Key);
			var response = await _weatherManager.GetRequest(url);
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				string responeJson = await response.Content.ReadAsStringAsync();
				DisplayError(responeJson);
				return;
			}

			string json = await response.Content.ReadAsStringAsync();
			var conditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(json);

			CurrentConditions = conditions.FirstOrDefault();
			IconPath = "/SimpleWeather;component/Images/" + CurrentConditions.WeatherIcon + "-s.png";
		}
	}
}
