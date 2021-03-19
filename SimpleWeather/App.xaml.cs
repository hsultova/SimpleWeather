using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleWeather
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		Mutex mutex;
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			string mutexName = "SimpleWeather";
			mutex = new Mutex(false, mutexName, out bool createdNew);
			if(!createdNew)
			{
				Shutdown();
			}
		}
	}
}
