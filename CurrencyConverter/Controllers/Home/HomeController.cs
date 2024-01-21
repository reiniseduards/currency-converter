using System;
using System.Configuration;
using System.Net;
using System.Web.Mvc;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Models;
using Newtonsoft.Json;

namespace CurrencyConverter.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebClientWrapper webClient;

        public HomeController()
        {
        }

        public HomeController(IWebClientWrapper webClient)
        {
            this.webClient = webClient;
        }

        // Action method for the default view
        public ActionResult Index()
        {
            // Populate ViewBag with currency keys for dropdown list
            ViewBag.CurrencyList = Currency.GetCurrencies().Keys;
            return View();
        }

        // Action method for handling currency conversion
        public ActionResult TryConvert(decimal amount, string fromCurrency, string toCurrency)
        {
            try
            {
                // Retrieve API URL from configuration
                string apiUrl = ConfigurationManager.AppSettings["ExchangeRateApiUrl"];

                // Build API request URL
                string url = $"{apiUrl}/{fromCurrency}/{toCurrency}/{amount}";

                // Use the injected webClient instance
                string json = webClient.DownloadString(url);
                Response response = JsonConvert.DeserializeObject<Response>(json);

                // Update response fields with additional information
                UpdateResponseFields(response, fromCurrency, toCurrency, amount);

                // Return JSON response to the client
                return Json(response);
            }
            catch (Exception)
            {
                // Handle exceptions by returning an error response
                Response response = new Response() { result = "error" };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        // Helper method to update response fields with additional information
        private void UpdateResponseFields(Response response, string fromCurrency, string toCurrency, decimal amount)
        {
            response.base_name = GetCurrencyName(fromCurrency);
            response.target_name = GetCurrencyName(toCurrency);
            response.base_symbol = GetCurrencySymbol(fromCurrency);
            response.target_symbol = GetCurrencySymbol(toCurrency);
            response.amount = amount;
        }

        // Helper method to get the name of a currency based on its code
        public string GetCurrencyName(string code)
        {
            Currency.GetCurrencies().TryGetValue(code, out Currency currency);
            return currency?.Name;
        }

        // Helper method to get the symbol of a currency based on its code
        public string GetCurrencySymbol(string code)
        {
            Currency.GetCurrencies().TryGetValue(code, out Currency currency);
            return currency?.Symbol;
        }
    }
}
