using System;
using SimpleWeather.Utils.Events;
using SimpleWeather.ViewModels;
using SimpleWeather.ViewModels.Base;

namespace SimpleWeather
{
	public class MainWindowViewModel :BaseViewModel
	{
		public MainWindowViewModel()
		{
			CurrentViewModel = new ForecastViewModel();
			CurrentViewModel.DisplayMessageHandler += OnDisplayError;
		}

		private const string DayBackgroundPath = @"Images/background_day.png";
		private const string NightBackgroundPath = @"Images/background_night.png";

		private void OnDisplayError(object sender, DisplayMessageEventArgs e)
		{
			Error = e.Message;
		}

		public ForecastViewModel CurrentViewModel { get; set; }

		private string _error;
		public string Error
		{
			get
			{
				return _error;
			}
			set
			{
				_error = value;
				OnPropertyChanged(nameof(Error));
			}
		}

		public string BackgroundPath => CurrentViewModel.CurrentConditions.IsDayTime ? DayBackgroundPath : NightBackgroundPath;
	}
}
