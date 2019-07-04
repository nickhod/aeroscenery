using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.UI
{
    public class ImageComboBox : ComboBox
    {
        private ImageList lstImages = new ImageList();

        private int imageLeftPadding = 5;
        private int imageRightPadding = 5;

        public ImageComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }

        public ImageList ImageList { get; set; }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            if (e.Index < 0)
            {
                e.Graphics.DrawString(this.Text, e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + lstImages.ImageSize.Width, e.Bounds.Top);
            }
            else
            {
                if (this.Items[e.Index].GetType() == typeof(ImageComboItem))
                {
                    if (this.ImageList == null)
                    {
                        throw new Exception("ImageList must be set");
                    }
                    else
                    {
                        ImageComboItem item = (ImageComboItem)this.Items[e.Index];

                        Color forecolor = (item.ForeColor != Color.FromKnownColor(KnownColor.Transparent)) ? item.ForeColor : e.ForeColor;

                        Font font = item.Bold ? new Font(e.Font, FontStyle.Bold) : e.Font;

                        if (item.ImageIndex != -1)
                        {
                            int imageTop = e.Bounds.Top + ((e.Bounds.Height - lstImages.ImageSize.Height) / 2);

                            this.ImageList.Draw(e.Graphics, e.Bounds.Left + imageLeftPadding, imageTop, item.ImageIndex);
                            e.Graphics.DrawString(item.Text, font, new SolidBrush(forecolor), e.Bounds.Left + lstImages.ImageSize.Width + imageLeftPadding + imageRightPadding, e.Bounds.Top);
                        }
                        else
                        {
                            e.Graphics.DrawString(item.Text, font, new SolidBrush(forecolor), e.Bounds.Left + lstImages.ImageSize.Width + imageLeftPadding + imageRightPadding, e.Bounds.Top);
                        }
                    }

                }
                else
                {
                    e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + lstImages.ImageSize.Width, e.Bounds.Top);
                }
            }

            base.OnDrawItem(e);
        }


    }


}
