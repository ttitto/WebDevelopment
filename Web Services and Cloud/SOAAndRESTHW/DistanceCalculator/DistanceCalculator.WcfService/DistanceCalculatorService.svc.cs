namespace DistanceCalculator.WcfService
{
    using System;
    using System.ServiceModel;

    using DistanceCalculator.Models;

    public class DistanceCalculatorService : IDistanceCalculatorService
    {
        public double CalcDistance(Point startPoint, Point endPoint)
        {
            double deltaX = endPoint.X - startPoint.X;
            double deltaY = endPoint.Y - startPoint.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}
