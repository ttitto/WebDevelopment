namespace DistanceCalculator.WcfService.ConsoleClient
{
    using System;

    using DistanceCalculator.Models;
    using DistanceCalculator.WcfService.ConsoleClient.DistanceCalculatorService;

    class DistanceCalculatorWcfServiceConsoleClient
    {
        static void Main(string[] args)
        {
            DistanceCalculatorServiceClient client = new DistanceCalculatorServiceClient();
            Point startPoint = new Point() { X = 2, Y = 3 };
            Point endPoint = new Point() { X = 4, Y = 5 };
            Console.WriteLine("The calculated distance is {0:0.00}", client.CalcDistance(startPoint, endPoint));
        }
    }
}
