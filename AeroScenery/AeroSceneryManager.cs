using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.Download;
using AeroScenery.OrthophotoSources;
using AeroScenery.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery
{
    public class AeroSceneryManager
    {
        public MainForm mainForm;

        private BingOrthophotoSource bingOrthophotoSource;

        private DownloadManager downloadManager;

        private AIDFileGenerator aidFileGenerator;

        private TMCFileGenerator tmcFileGenerator;

        private GeoConvertManager geoConvertManager;

        private DownloadFailedForm downloadFailedForm;

        private static AeroSceneryManager aeroSceneryManager;

        private Settings settings;


        public Settings Settings
        {
            get
            {
                return this.settings;
            }
        }

        public static AeroSceneryManager Instance
        {
            get
            {
                if (AeroSceneryManager.aeroSceneryManager == null)
                {
                    aeroSceneryManager = new AeroSceneryManager();
                }

                return aeroSceneryManager;
            }
        }

        public void Initialize()
        {
            this.mainForm = new MainForm();
            this.mainForm.StartClicked += async (sender, eventArgs) =>
            {
                await StartSceneryGenerationProcessAsync(sender, eventArgs);
            };


            bingOrthophotoSource = new BingOrthophotoSource();
            downloadManager = new DownloadManager();
            aidFileGenerator = new AIDFileGenerator();
            tmcFileGenerator = new TMCFileGenerator();
            geoConvertManager = new GeoConvertManager();

            settings = new Settings();

            Application.Run(this.mainForm);
        }



        public async Task StartSceneryGenerationProcessAsync(object sender, EventArgs e)
        {
            foreach (AFS2GridSquare afs2GridSquare in this.mainForm.SelectedAFS2GridSquares.Values.Select(x => x.Item1))
            {
                // Download Imamge Tiles
                if (this.Settings.DownloadImageTiles)
                {
                    // Get a list of all the image tiles we need to download
                    var imageTiles = bingOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare);

                    // Capture the progress of each thread
                    var downloadThreadProgress = new Progress<DownloadThreadProgress>();
                    downloadThreadProgress.ProgressChanged += DownloadThreadProgress_ProgressChanged;

                    // Send the image tiles to the download manager
                    await downloadManager.DownloadImageTiles(imageTiles, downloadThreadProgress);
                }

                // Stitch Image Tiles
                if (this.Settings.StitchImageTiles)
                {

                }

                // Generate AID and TMC Files
                if (this.Settings.GenerateAIDAndTMCFiles)
                {
                    // Generate AID files for the image tiles
                    //await aidFileGenerator.GenerateAIDFilesAsync(imageTiles);
                }

                // Run GeoConvert
                if (this.Settings.RunGeoConvert)
                {

                }

                // Delete Stitched Immage Tiles
                if(this.Settings.DeleteStitchedImageTiles)
                {

                }

                // Install Scenery
                if (this.Settings.InstallScenery)
                {

                }



                //// Have all image tiles been downloaded by the download manager
                //if (AllImageTilesDownloaded(imageTiles))
                //{
                //    // Generate the TMC File
                //    tmcFileGenerator.GenerateTMCFile(this.mainForm.SelectedAFS2GridSquares);

                //    // Run Geoconvert
                //    //geoConvertManager.RunGeoConvert();
                //}
                //else
                //{
                //    // The jpg to AID count doesn't match, something is wrong, show the dialog
                //    downloadFailedForm = new DownloadFailedForm();
                //    downloadFailedForm.ShowDialog();
                //}

            }

        }

        private void DownloadThreadProgress_ProgressChanged(object sender, DownloadThreadProgress progress)
        {
            var progressControl = this.mainForm.GetDownloadThreadProgressControl(progress.DownloadThreadNumber);
            var percentageProgress = (int)Math.Floor(((double)progress.FilesDownloaded / (double)progress.TotalFiles) * 100);

            progressControl.SetProgressPercentage(percentageProgress);

            progressControl.SetImageTileCount(progress.FilesDownloaded, progress.TotalFiles);
        }

        public bool AllImageTilesDownloaded(List<ImageTile> imageTiles)
        {
            return true;
        }
    }
}
