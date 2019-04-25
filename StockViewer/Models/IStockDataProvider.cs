using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockViewer.Models
{
	/// <summary>
	/// A stock data provider used to retrieve historical price data from various sources (e.g. Yahoo Finance).
	/// </summary>
	public interface IStockDataProvider
	{
		Task<List<StockData>> GetPricesAsync(IHistoryConfig config);
	}
}
