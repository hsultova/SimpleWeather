using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SimpleWeather.Utils.Events;

namespace SimpleWeather.ViewModels.Base
{
	public class BaseViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = "") 
			=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		#endregion

		public event EventHandler<DisplayMessageEventArgs> DisplayMessageHandler;
		protected void OnDisplayMessage(DisplayMessageEventArgs args) => DisplayMessageHandler?.Invoke(this, args);
	}
}
