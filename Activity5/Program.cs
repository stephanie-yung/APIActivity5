//Stephanie Yung
//Activity 5
//Intro to APIs

using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Activity5
{
    class Weather
    {
        [JsonProperty("temperature")] public string? Temperature { get; set; }

        [JsonProperty("wind")] public string? Wind { get; set; }

        [JsonProperty("description")] public string? Description { get; set; }

    }

    //everything has to be in class
    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }

        private static async Task ProcessRepositories()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Enter a city name. Press Enter without writing a city name to quit the program.");

                    var city = Console.ReadLine();

                    if (string.IsNullOrEmpty(city))
                    {
                        break;
                    }

                    var url = "https://goweather.herokuapp.com/weather/" + city.ToLower();

                    var result = await client.GetAsync(url);

                    var resultRead = await result.Content.ReadAsStringAsync();

                    var cityData = JsonConvert.DeserializeObject<Weather>(resultRead);

                    Console.WriteLine("---");
                    Console.WriteLine("Temperature: " + cityData!.Temperature);
                    Console.WriteLine("Wind: " + cityData.Wind);
                    Console.WriteLine("Description : " + cityData.Description);
                    Console.WriteLine("\n---");

                }
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR. Please enter a valid City name!");
            }

        }
    }

}
