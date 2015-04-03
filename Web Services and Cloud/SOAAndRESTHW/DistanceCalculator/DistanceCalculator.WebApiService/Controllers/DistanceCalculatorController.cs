namespace DistanceCalculator.WebApiService.Controllers
{
    using DistanceCalculator.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    public class DistanceCalculatorController : ApiController
    {
        [HttpGet]
        public IHttpActionResult CalcDistance(int ax, int ay, int bx, int by)
        {
            Point startPoint = new Point { X = ax, Y = ay };
            Point endPoint = new Point { X = bx, Y = by };

            double deltaX = endPoint.X - startPoint.X;
            double deltaY = endPoint.Y - startPoint.Y;
            double result = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return Ok(result);
        }
    }
}