using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SimpleWeather.Managers
{
	/// <summary>
	/// Represents the theme object used to store corresponding ResourceDictionary
	/// </summary>
	public class Theme
	{
		public string Name { get; set; }
		public ResourceDictionary Resource { get; set; }
	}

	/// <summary>
	/// Manages actions with themes. Registering of new theme, changing current theme, etc.
	/// </summary>
	public class ThemeManager
	{
		public ThemeManager()
		{
			//Register themes
			var dayTheme = new Theme { 
				Name = "DayTheme", 
				Resource = new ResourceDictionary { Source = new Uri("Resources/Themes/DayTheme.xaml", UriKind.Relative) } };
			RegisterTheme(dayTheme);

			var nightTheme = new Theme { 
				Name = "NightTheme", 
				Resource = new ResourceDictionary { Source = new Uri("Resources/Themes/NightTheme.xaml", UriKind.Relative) } };
			RegisterTheme(nightTheme);
		}

		private static readonly List<Theme> _themes = new List<Theme>();

		//Current loaded theme
		public Theme CurrentTheme { get; set; }

		/// <summary>
		/// Change applied theme
		/// </summary>
		/// <param name="theme">New theme to be applied</param>
		public void ChangeTheme(Theme theme)
		{
			if(theme == null)
			{
				//Log error that theme is null
				return;
			}

			if (CurrentTheme == theme)
			{
				//Nothing to do when the theme is the same.
				return;
			}

			//Remove previous theme before loading the new one
			App.Current.Resources.MergedDictionaries.Remove(CurrentTheme?.Resource);
			App.Current.Resources.MergedDictionaries.Add(theme.Resource);
			CurrentTheme = theme;
		}

		/// <summary>
		/// Registers a new theme 
		/// </summary>
		/// <param name="theme">New theme to be registered</param>
		public void RegisterTheme(Theme theme)
		{
			_themes.Add(theme);
		}

		/// <summary>
		/// Gets the theme by name
		/// </summary>
		/// <param name="name">Name of the theme</param>
		/// <returns>Theme object if registered, otherwise null</returns>
		public Theme GetTheme(string name)
		{
			return _themes.FirstOrDefault(t => t.Name == name);
		}
	}
}
