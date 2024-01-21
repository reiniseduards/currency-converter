using System.Collections.Generic;

namespace CurrencyConverter.Models
{
    public class Currency
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public Currency(string symbol, string name)
        {
            Symbol = symbol;
            Name = name;
        }

        public static Dictionary<string, Currency> GetCurrencies()
        {
            return new Dictionary<string, Currency>()
        {
            {"EUR", new Currency("€", "Euro")},
            {"USD", new Currency("$", "US Dollar")},
            {"GBP", new Currency("£", "British Pound")},
            {"JPY", new Currency("¥", "Japanese Yen")},
            {"AUD", new Currency("$", "Australian dollar")}
        };
        }
    }
}