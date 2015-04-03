namespace DistanceCalculator.WebApiService.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    class DistanceCalculatorWebApiClient
    {
        static void Main(string[] args)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8856/")
            };

            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync("api/distancecalculator?ax=2&ay=3&bx=4&by=5").Result;

            if (response.IsSuccessStatusCode)
            {
                var distance = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(distance);
            }
            else
            {
                Console.WriteLine("{0} {1}", response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }
    }
}
