namespace DistanceCalculator.WcfService
{
    using System.ServiceModel;
    using DistanceCalculator.Models;

    [ServiceContract]
    public interface IDistanceCalculatorService
    {

        [OperationContract]
        double CalcDistance(Point startPoint, Point endPoint);
    }
}
