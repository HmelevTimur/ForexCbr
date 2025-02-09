using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ForexCbr.Commands.Implementations;
using Microsoft.Extensions.Configuration;

namespace ForexCbr
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string cbrUrl = config["CbrUrl"] ?? "https://www.cbr.ru/scripts/XML_daily.asp";

            using HttpClient client = new HttpClient();

            while (true)
            {
                Console.Write("Введите код валюты (например, USD, EUR) или 'exit' для выхода: ");

                var input = Console.ReadLine()?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (input == null || input.Length == 0) 
                {
                    Console.WriteLine("Код валюты не введен.");
                    continue;
                }

                string commandName = input[0].ToLower();

                if (commandName == "exit")
                {
                    Console.WriteLine("Выход из программы.");
                    return; 
                }

                var command = new GetCurrencyRateCommand(commandName.ToUpper(), cbrUrl, client);
                await command.ExecuteAsync();
            }
        }
    }
}