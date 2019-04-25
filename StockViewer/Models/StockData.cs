using System;

namespace StockViewer.Models
{
	public class StockData
	{
		public string Ticker { get; set; }
		public DateTime Date { get; set; }
		public double Open { get; set; }
		public double High { get; set; }
		public double Low { get; set; }
		public double Close { get; set; }
		public double CloseAdj { get; set; }
		public double Volume { get; set; }
	}
}