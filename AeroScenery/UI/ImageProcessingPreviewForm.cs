using AeroScenery.ImageProcessing;
using AForge;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.UI
{
    public partial class ImageProcessingPreviewForm : Form
    {
        // The orignal sample bitmap without any adjustments
        private Bitmap originalPreviewImage;

        /*Semi Rural Green
Semi Rual Arid
Rural Green
Rural Arid
Urban 1
Urban 2*/

        public ImageProcessingPreviewForm()
        {
            InitializeComponent();
        }

        public async Task UpdateImage(ImageProcessingSettings imageProcessingSettings)
        {

            Bitmap tempBitmap = tempBitmap = (Bitmap)this.originalPreviewImage.Clone(); ;

            await Task.Run(() =>
            {
                ImageProcessingFilters imageProcessingFilters = new ImageProcessingFilters();

                var filters = imageProcessingFilters.GetFilterList(imageProcessingSettings);

                foreach (IInPlaceFilter filter in filters)
                {
                    lock(tempBitmap)
                    {
                        filter.ApplyInPlace(tempBitmap);
                    }
                }

                this.previewPictureBox.Image = tempBitmap;
            });

        }

        private void ImageProcessingPreviewForm_Load(object sender, EventArgs e)
        {
            var applicationPath = AeroSceneryManager.Instance.ApplicationPath;
            var pathToImage = String.Format("{0}{1}Resources{1}SampleImages{1}semi_rural_green.jpg", applicationPath, Path.DirectorySeparatorChar);

            if (this.originalPreviewImage != null)
            {
                this.originalPreviewImage.Dispose();
                this.originalPreviewImage = null;

            }

            this.originalPreviewImage = new Bitmap(pathToImage);

            this.previewPictureBox.Image = this.originalPreviewImage;
        }
    }
}
