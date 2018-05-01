using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.Data;
using AeroScenery.Download;
using AeroScenery.OrthophotoSources;
using AeroScenery.OrthoPhotoSources;
using AeroScenery.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        private ImageTileService imageTileService;

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
            imageTileService = new ImageTileService();

            settings = new Settings();

            Application.Run(this.mainForm);
        }



        public async Task StartSceneryGenerationProcessAsync(object sender, EventArgs e)
        {
            foreach (AFS2GridSquare afs2GridSquare in this.mainForm.SelectedAFS2GridSquares.Values.Select(x => x.Item1))
            {
                // Do we have a directory for this afs grid square in our working directory?
                var afsGridSquareDirectory = this.settings.WorkingDirectory + afs2GridSquare.Name;

                if (!Directory.Exists(this.settings.WorkingDirectory + afs2GridSquare.Name)) {
                    Directory.CreateDirectory(this.settings.WorkingDirectory + afs2GridSquare.Name);
                }

                var tileDownloadDirectory = afsGridSquareDirectory;

                switch (this.settings.OrthophotoSource)
                {
                    case OrthophotoSource.Bing:
                        tileDownloadDirectory += @"/b/";
                        break;
                    case OrthophotoSource.Google:
                        tileDownloadDirectory += @"/g/";
                        break;
                }

                // Do we have a directory for the afs grid square and this orthophoto source
                if (!Directory.Exists(tileDownloadDirectory))
                {
                    Directory.CreateDirectory(tileDownloadDirectory);
                }


                List<ImageTile> imageTiles = null;

                // Download Imamge Tiles
                if (this.Settings.DownloadImageTiles)
                {
                    // Get a list of all the image tiles we need to download
                    imageTiles = bingOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare);

                    // Capture the progress of each thread
                    var downloadThreadProgress = new Progress<DownloadThreadProgress>();
                    downloadThreadProgress.ProgressChanged += DownloadThreadProgress_ProgressChanged;

                    // Send the image tiles to the download manager
                    await downloadManager.DownloadImageTiles(imageTiles, downloadThreadProgress, tileDownloadDirectory);


                    // Save aero files for these tiles so we can do things with them in a later pass
                    await this.imageTileService.SaveImageTilesAsync(imageTiles, tileDownloadDirectory);
                }

                // Stitch Image Tiles
                if (this.Settings.StitchImageTiles)
                {
                    // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                    if (imageTiles == null)
                    {
                        imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                    }


                }

                // Generate AID and TMC Files
                if (this.Settings.GenerateAIDAndTMCFiles)
                {
                    // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                    if (imageTiles == null)
                    {
                        imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                    }

                    // Generate AID files for the image tiles
                    //await aidFileGenerator.GenerateAIDFilesAsync(imageTiles);
                }

                // Run GeoConvert
                if (this.Settings.RunGeoConvert)
                {

                    // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                    if (imageTiles == null)
                    {
                        imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                    }

                }

                // Delete Stitched Immage Tiles
                if(this.Settings.DeleteStitchedImageTiles)
                {
                    // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                    if (imageTiles == null)
                    {
                        imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                    }

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
