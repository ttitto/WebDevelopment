namespace DistanceCalculator.WcfService.Host
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Description;

    class DistanceCalculatorWcfServiceHost
    {
        static void Main(string[] args)
        {
            Uri serviceAddress = new Uri("http://localhost:4321/calc");
            ServiceHost distanceCalculatorSelfHost = new ServiceHost(typeof(DistanceCalculatorService), serviceAddress);
            ServiceMetadataBehavior distanceCalculatorSmb = new ServiceMetadataBehavior();

            distanceCalculatorSmb.HttpGetEnabled = true;
            distanceCalculatorSelfHost.Description.Behaviors.Add(distanceCalculatorSmb);

            using (distanceCalculatorSelfHost)
            {
                distanceCalculatorSelfHost.Open();
                Console.WriteLine("The distance calulator service was started at endpoint {0}", serviceAddress);

                Console.WriteLine("Press [Enter] to exit:");
                Console.ReadLine();
            }
        }
    }
}
