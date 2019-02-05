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

            // Bind combobox to dictionary
            var previewImages = new Dictionary<int, string>();

            previewImages.Add(1, "Semi Rural Green");
            previewImages.Add(2, "Semi Rual Arid");
            previewImages.Add(3, "Rural Green");
            previewImages.Add(4, "Rural Arid");
            previewImages.Add(5, "Urban 1");
            previewImages.Add(6, "Urban 2");

            this.previewImageCombo.DataSource = new BindingSource(previewImages, null);
            previewImageCombo.DisplayMember = "Value";
            previewImageCombo.ValueMember = "Key";

            // Get combobox selection (in handler)

            this.previewImageCombo.SelectedIndex = 0;
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

                lock (this.previewPictureBox)
                {
                    this.previewPictureBox.Image = tempBitmap;
                }
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

        private void previewImageCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var seletedItem = (KeyValuePair<int, string>)previewImageCombo.SelectedItem;
            int key = seletedItem.Key;

            var applicationPath = AeroSceneryManager.Instance.ApplicationPath;

            var imageName = "";

            switch (key)
            {
                //Semi Rural Green
                case 1:
                    imageName = "semi_rural_green.jpg";
                    break;
                //Semi Rual Arid
                case 2:
                    imageName = "semi_rural_arid.jpg";
                    break;
                //Rural Green
                case 3:
                    imageName = "rural_green.jpg";
                    break;
                //Rural Arid
                case 4:
                    imageName = "rural_arid.jpg";
                    break;
                //Urban 1
                case 5:
                    imageName = "urban_1.jpg";
                    break;
                //Urban 2
                case 6:
                    imageName = "urban_2.jpg";
                    break;
            }

            var pathToImage = String.Format("{0}{1}Resources{1}SampleImages{1}{2}", applicationPath, Path.DirectorySeparatorChar, imageName);

            if (this.originalPreviewImage != null)
            {
                this.originalPreviewImage.Dispose();
                this.originalPreviewImage = null;
            }

            this.originalPreviewImage = new Bitmap(pathToImage);

            this.previewPictureBox.Image = this.originalPreviewImage;

        }

        private void openImageButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Title = "Open O,age File";
            this.openFileDialog1.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = AeroScenery.AeroSceneryManager.Instance.Settings.AFS2Directory;
            this.openFileDialog1.CheckFileExists = true;
            this.openFileDialog1.CheckPathExists = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (this.originalPreviewImage != null)
                {
                    this.originalPreviewImage.Dispose();
                    this.originalPreviewImage = null;

                }

                this.originalPreviewImage = new Bitmap(openFileDialog1.FileName);

                this.previewPictureBox.Image = this.originalPreviewImage;
            }
        }
    }
}
