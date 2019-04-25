using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StockViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockViewer.Pages
{
	public class StockViewerModel : PageModel
	{
		private readonly IStockDataProvider _stockData;
		private readonly IHtmlHelper _htmlHelper;

		//test
		public List<StockData> _prices;

		//test
		public List<List<double>> gg = new List<List<double>>()
		{
			new List<double>() {1492435800000, 141.48, 141.88, 140.87, 141.83},
			new List<double>() {1492522200000, 141.41, 142.04, 141.11, 141.2},
		};

		//test
		public double[][] myA =
			{
				new double[] {1492435800000, 141.48, 141.88, 140.87, 141.83},
				new double[] {1492522200000, 141.41, 142.04, 141.11, 141.2}
			};

		public string ChartSeriesData;

		[BindProperty]
		public YahooHistoryConfig Yh { get; set; }

		public IEnumerable<SelectListItem> Periods { get; set; } // used for dropdown select options
		public IEnumerable<SelectListItem> Tickers { get; set; } // used for dropdown select options

		public StockViewerModel(IHtmlHelper htmlHelper, IStockDataProvider stockDataProvider)
		{
			this._stockData = stockDataProvider;
			this._htmlHelper = htmlHelper;
			this.Yh = new YahooHistoryConfig();
		}

		#region requests
		public void OnGet()
		{
			this.ResetFormEnumLists();
			this._prices = this._stockData.GetPricesAsync(this.Yh).Result;
			//this.ChartSeriesData = JsonConvert.SerializeObject(this.gg); // test
			this.ChartSeriesData = GetChartSeriesData(_prices);
		}

		public IActionResult OnPost()
		{
			this.ResetFormEnumLists();
			this._prices = this._stockData.GetPricesAsync(this.Yh).Result;

			this.gg = new List<List<double>>()
			{
				new List<double>() {1492435800000, 141.48, 150.88, 140.87, 141.83},
				new List<double>() {1492522200000, 141.41, 142.04, 141.11, 141.2},
			};
			//this.ChartSeriesData = JsonConvert.SerializeObject(this.gg); // test
			this.ChartSeriesData = GetChartSeriesData(_prices);
			return this.Page();
		}
		#endregion

		#region asyncVersion
		//public async void OnGet()
		//{
		//	this.ResetFormEnumLists();
		//	this._prices = await this._stockData.GetPricesAsync(this.Yh);
		//	this.ChartSeriesData = JsonConvert.SerializeObject(this.gg); // test
		//}

		//public async Task<IActionResult> OnPost()
		//{
		//	this.ResetFormEnumLists();
		//	this._prices = await this._stockData.GetPricesAsync(this.Yh);

		//	this.gg = new List<List<double>>()
		//	{
		//		new List<double>() {1492435800000, 141.48, 150.88, 140.87, 141.83},
		//		new List<double>() {1492522200000, 141.41, 142.04, 141.11, 141.2},
		//	};
		//	this.ChartSeriesData = JsonConvert.SerializeObject(this.gg); // test

		//	return this.Page();
		//}
		#endregion

		private void ResetFormEnumLists()
		{
			this.Periods = this._htmlHelper.GetEnumSelectList<TimeFrame>();
			this.Tickers = this._htmlHelper.GetEnumSelectList<Ticker>();
		}

		// gets chart data series in json
		private string GetChartSeriesData(List<StockData> stockData)
		{
			List<List<double>> stockEntries = new List<List<double>>();

			foreach (StockData e in stockData)
			{
				stockEntries.Add(
					new List<double> {
						((DateTimeOffset)e.Date).ToUnixTimeMilliseconds(),
						e.Open,
						e.High,
						e.Low,
						e.Close});
			}

			return JsonConvert.SerializeObject(stockEntries);
		}


	}
}