using SimpleWeather.ViewModels;

namespace SimpleWeather
{
	public class MainWindowViewModel
	{
		public MainWindowViewModel()
		{
			CurrentViewModel = new ForecastViewModel();
		}

		public object CurrentViewModel { get; set; }
	}
}
