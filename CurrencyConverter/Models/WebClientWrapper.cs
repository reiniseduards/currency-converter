using CurrencyConverter.Interfaces;
using System;
using System.Net;

namespace CurrencyConverter.Models
{
    public class WebClientWrapper : IWebClient, IDisposable
    {
        private readonly WebClient webClient;

        public WebClientWrapper()
        {
            webClient = new WebClient();
        }

        public string DownloadString(string address)
        {
            return webClient.DownloadString(address);
        }

        public void Dispose()
        {
            webClient.Dispose();
        }
    }
}