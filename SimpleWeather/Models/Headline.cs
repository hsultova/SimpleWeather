using System;

namespace SimpleWeather.Models
{
	public class Headline
	{
		public DateTime EffectiveDate { get; set; }
		public int EffectiveEpochDate { get; set; }
		public int Severity { get; set; }
		public string Text { get; set; }
		public string Category { get; set; }
		public object EndDate { get; set; }
		public object EndEpochDate { get; set; }
		public string MobileLink { get; set; }
		public string Link { get; set; }
	}
}
