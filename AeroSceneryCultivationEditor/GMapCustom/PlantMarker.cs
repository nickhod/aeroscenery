using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.CultivationEditor.GMapCustom
{
    public class PlantMarker : GMapMarker
    {
        // https://stackoverflow.com/questions/42317455/updating-gmap-net-marker-image-in-c-sharp

        private GMapControl gmap;

        public PlantMarker(PointLatLng p, GMapControl gmap)
            : base(p)
        {
            //this.Bitmap = Bitmap;
            this.gmap = gmap;
            //Size = new System.Drawing.Size(8, 8);
            //Offset = new Point(-Size.Width / 2, -Size.Height);
        }

        public override void OnRender(Graphics g)
        {
            var size = (int)(this.gmap.Zoom / 4);
            Size = new System.Drawing.Size(size, size);
            Offset = new Point(-Size.Width / 2, -Size.Height);

            var brush = new SolidBrush(Color.Red);
            var pen = new Pen(Color.Red);
            g.DrawRectangle(pen, new Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height));
            //this.
            //g.DrawImage(Bitmap, LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);
        }
    }

}
