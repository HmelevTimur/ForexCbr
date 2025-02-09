using System.Text;
using System.Xml.Linq;
using ForexCbr.Commands.Interfaces;

namespace ForexCbr.Commands.Implementations;

public class GetCurrencyRateCommand(string currencyCode, string cbrUrl, HttpClient client) : ICommand
{
    public async Task ExecuteAsync()
    {
        try
        {
            byte[] responseBytes = await client.GetByteArrayAsync(cbrUrl);
            string response = Encoding.GetEncoding("windows-1251").GetString(responseBytes);
            XDocument xml = XDocument.Parse(response);

            var currency = xml.Descendants("Valute")
                .FirstOrDefault(v => v.Element("CharCode")?.Value == currencyCode);

            if (currency != null)
            {
                string name = currency.Element("Name")?.Value;
                string value = currency.Element("Value")?.Value;
                Console.WriteLine($"{name}: {value} RUB");
            }
            else
            {
                Console.WriteLine("Валюта не найдена.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения данных: {ex.Message}");
        }
    }
}