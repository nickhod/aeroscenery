using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.UI
{
    public class ImageComboItem
    {
        public ImageComboItem()
        {
            this.ForeColor = Color.FromKnownColor(KnownColor.Transparent);
        }

        public ImageComboItem(string text)
        {
            this.Text = text;
            this.ForeColor = Color.FromKnownColor(KnownColor.Transparent);
        }

        public ImageComboItem(string text, int imageIndex)
        {
            this.Text = text;
            this.ImageIndex = imageIndex;
            this.ForeColor = Color.FromKnownColor(KnownColor.Transparent);
        }

        public ImageComboItem(string text, int imageIndex, bool bold)
        {
            this.Text = text;
            this.ImageIndex = imageIndex;
            this.Bold = bold;
            this.ForeColor = Color.FromKnownColor(KnownColor.Transparent);
        }

        public ImageComboItem(string text, int imageIndex, bool bold, Color foreColor)
        {
            this.Text = text;
            this.ImageIndex = imageIndex;
            this.Bold = bold;
            this.ForeColor = foreColor;
        }

        public ImageComboItem(string text, int imageIndex, bool bold, Color foreColor, object tag)
        {
            this.Text = text;
            this.ImageIndex = imageIndex;
            this.Bold = bold;
            this.ForeColor = foreColor;
            this.Tag = tag;
        }

        public object Value { get; set; }

        public Color ForeColor { get; set; }

        public int ImageIndex { get; set; }

        public bool Bold { get; set; }

        public object Tag { get; set;  }
        public string Text { get; set; }       

        public override string ToString()
        {
            return this.Text;
        }

    }
}
