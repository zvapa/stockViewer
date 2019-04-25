using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YahooFinanceApi;

namespace StockViewer.Models
{
	/// <summary>
	/// A class designed to retrieve historical stock price data from Yahoo Finance.
	/// </summary>
	public class YahooStockData : IStockDataProvider
	{
		/// <summary>
		/// Gets historical prices from Yahoo Finance given the specified configuration.
		/// </summary>
		/// <param name="config">Configuration parameters used to retrieve historical price data.</param>
		/// <returns></returns>
		public async Task<List<StockData>> GetPricesAsync(IHistoryConfig config)
		{
			string yahooSymbol = config.Ticker.ToString();
			Period yahooPeriod = YahooHistoryConfig.GetYahooPeriod(config.TimeFrame);

			IReadOnlyList<Candle> hist = await Yahoo.GetHistoricalAsync(
				yahooSymbol, config.DateFrom, config.DateTo, yahooPeriod);

			List<StockData> prices = new List<StockData>();

			foreach (Candle candle in hist)
			{
				prices.Add(new StockData()
				{
					Ticker = yahooSymbol,
					Date = candle.DateTime,
					Open = (double)candle.Open,
					High = (double)candle.High,
					Low = (double)candle.Low,
					Close = (double)candle.Close,
					CloseAdj = (double)candle.AdjustedClose,
					Volume = candle.Volume
				});
			}
			return prices;
		}
	}
}
