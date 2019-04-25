using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using StockViewer.Models;
using System;
using System.Collections.Generic;

namespace StockViewer.Pages
{
    public class StockViewerModel : PageModel
    {
        private readonly IStockDataProvider _stockData;
        private readonly IHtmlHelper _htmlHelper;

        /// <summary>
        /// Variable to hold a list of stock price data
        /// </summary>
        private List<StockData> _prices;

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

        public void OnGet()
        {
            this.ResetFormEnumLists();
            this._prices = this._stockData.GetPricesAsync(this.Yh).Result;
            this.ChartSeriesData = GetChartSeriesData(_prices);
        }

        public IActionResult OnPost()
        {
            this.ResetFormEnumLists();
            this._prices = this._stockData.GetPricesAsync(this.Yh).Result;

            this.ChartSeriesData = GetChartSeriesData(_prices);
            return this.Page();
        }

        private void ResetFormEnumLists()
        {
            this.Periods = this._htmlHelper.GetEnumSelectList<TimeFrame>();
            this.Tickers = this._htmlHelper.GetEnumSelectList<Ticker>();
        }

        /// <summary>
        /// Gets chart data series in json
        /// </summary>
        /// <param name="stockData"></param>
        /// <returns></returns>
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