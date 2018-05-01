using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class AFS2GridSquare
    {

        public string Name { get; set; }
        public List<PointLatLng> Coordinates { get; set; }

        public AFS2GridSquare()
        {
            this.Coordinates = new List<PointLatLng>();
        }

        public PointLatLng GetCenter()
        {
            return MidPoint(Coordinates[0], Coordinates[2]);         
        }

        private PointLatLng MidPoint(PointLatLng posA, PointLatLng posB)
        {
            PointLatLng midPoint = new PointLatLng();

            double dLon = DegreesToRadians(posB.Lng - posA.Lng);
            double Bx = Math.Cos(DegreesToRadians(posB.Lat)) * Math.Cos(dLon);
            double By = Math.Cos(DegreesToRadians(posB.Lat)) * Math.Sin(dLon);

            midPoint.Lat = RadiansToDegrees(Math.Atan2(
                         Math.Sin(DegreesToRadians(posA.Lat)) + Math.Sin(DegreesToRadians(posB.Lat)),
                         Math.Sqrt(
                             (Math.Cos(DegreesToRadians(posA.Lat)) + Bx) *
                             (Math.Cos(DegreesToRadians(posA.Lat)) + Bx) + By * By)));

            midPoint.Lng = posA.Lng + RadiansToDegrees(Math.Atan2(By, Math.Cos(DegreesToRadians(posA.Lat)) + Bx));

            return midPoint;
        }

        private double DegreesToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadiansToDegrees(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

    }
}
