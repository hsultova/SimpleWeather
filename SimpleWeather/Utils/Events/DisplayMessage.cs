namespace SimpleWeather.Utils.Events
{
	public enum DisplayMessageType
	{
		None,
		Error,
		Info
	}
	public class DisplayMessageEventArgs
	{
		public string Message { get; set; }
		public DisplayMessageType Тype { get; set; }
	}

}
