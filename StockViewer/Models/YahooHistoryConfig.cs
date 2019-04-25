using System;


namespace StockViewer.Models
{
	/// <summary>
	/// Holds the configuration parameters used to retrieve historical stock prices from Yahoo Finance. 
	/// </summary>
	public class YahooHistoryConfig : IHistoryConfig
	{
		public Ticker Ticker { get; set; }
		public TimeFrame TimeFrame { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }

		public YahooHistoryConfig()
		{
			Ticker = Ticker.AMZN;
			TimeFrame = TimeFrame.Daily;
			DateFrom = new DateTime(2018, 1, 1);
			DateTo = new DateTime(2018, 12, 31);
		}

		/// <summary>
		/// Converts the specified time frame to Yahoo Finance time frame
		/// </summary>
		/// <param name="period"></param>
		/// <returns></returns>
		public static YahooFinanceApi.Period GetYahooPeriod(TimeFrame timeFrame)
		{
			return Enum.TryParse(timeFrame.ToString(), true, out YahooFinanceApi.Period p)
				? p : YahooFinanceApi.Period.Daily;
		}
	}
}