using AeroScenery.Data.Models;
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
        public int Level { get; set; }

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

        /// <summary>
        /// The latitude of North side of the square
        /// </summary>
        public double NorthLatitude
        {
            get
            {
                return this.Coordinates[0].Lat;
            }
        }

        /// <summary>
        /// The longitude West side of the square
        /// </summary>
        public double WestLongitude
        {
            get
            {
                return this.Coordinates[0].Lng;
            }
        }

        /// <summary>
        /// The latitude of South side of the square
        /// </summary>
        public double SouthLatitude
        {
            get
            {
                return this.Coordinates[3].Lat;            
            }
        }

        /// <summary>
        /// The longitude of the East side of the square
        /// </summary>
        public double EastLongitude
        {
            get
            {
                return this.Coordinates[2].Lng;
            }
        }

        public static AFS2GridSquare FromGridSquare(GridSquare gridSquare)
        {
            var afs2GridSquare = new AFS2GridSquare();

            var pointLatLon1 = new PointLatLng(gridSquare.NorthLatitude, gridSquare.WestLongitude);
            var pointLatLon2 = new PointLatLng(gridSquare.NorthLatitude, gridSquare.EastLongitude);
            var pointLatLon3 = new PointLatLng(gridSquare.SouthLatitude, gridSquare.EastLongitude);
            var pointLatLon4 = new PointLatLng(gridSquare.SouthLatitude, gridSquare.WestLongitude);

            afs2GridSquare.Coordinates.Add(pointLatLon1);
            afs2GridSquare.Coordinates.Add(pointLatLon2);
            afs2GridSquare.Coordinates.Add(pointLatLon3);
            afs2GridSquare.Coordinates.Add(pointLatLon4);

            afs2GridSquare.Name = gridSquare.Name;
            afs2GridSquare.Level = gridSquare.Level;

            return afs2GridSquare;

        }
    }
}
