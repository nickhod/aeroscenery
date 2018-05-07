using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.Data;
using AeroScenery.Download;
using AeroScenery.ImageProcessing;
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
        private GoogleOrthophotoSource googleOrthophotoSource;
        private USGSOrthophotoSource usgsOrthophotoSource;

        private DownloadManager downloadManager;

        private AIDFileGenerator aidFileGenerator;

        private TMCFileGenerator tmcFileGenerator;

        private GeoConvertManager geoConvertManager;

        private DownloadFailedForm downloadFailedForm;

        private TileStitcher tileStitcher;

        private static AeroSceneryManager aeroSceneryManager;

        private ImageTileService imageTileService;

        private Common.Settings settings;


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
            googleOrthophotoSource = new GoogleOrthophotoSource();
            usgsOrthophotoSource = new USGSOrthophotoSource();


            downloadManager = new DownloadManager();
            aidFileGenerator = new AIDFileGenerator();
            tmcFileGenerator = new TMCFileGenerator();
            geoConvertManager = new GeoConvertManager();
            imageTileService = new ImageTileService();
            tileStitcher = new TileStitcher();

            settings = new Common.Settings();
            settings.OrthophotoSource = OrthophotoSource.Bing;

            this.mainForm.Initialize();
            Application.Run(this.mainForm);
        }

        private string GetTileDownloadDirectory(string afsGridSquareDirectory)
        {
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

            return tileDownloadDirectory;
        }

        public async Task StartSceneryGenerationProcessAsync(object sender, EventArgs e)
        {
            int i = 0;
            foreach (AFS2GridSquare afs2GridSquare in this.mainForm.SelectedAFS2GridSquares.Values.Select(x => x.Item1))
            {
                this.mainForm.UpdateParentTaskLabel(String.Format("Working on AFS Grid Square {0} of {1}", i+1, this.mainForm.SelectedAFS2GridSquares.Count()));

                // Do we have a directory for this afs grid square in our working directory?
                var afsGridSquareDirectory = this.settings.WorkingDirectory + afs2GridSquare.Name;

                if (!Directory.Exists(this.settings.WorkingDirectory + afs2GridSquare.Name)) {
                    Directory.CreateDirectory(this.settings.WorkingDirectory + afs2GridSquare.Name);
                }

                var tileDownloadDirectory = GetTileDownloadDirectory(afsGridSquareDirectory);

                // Do we have a directory for the afs grid square and this orthophoto source
                if (!Directory.Exists(tileDownloadDirectory))
                {
                    Directory.CreateDirectory(tileDownloadDirectory);
                }

                List<ImageTile> imageTiles = null;


                // Download Imamge Tiles
                if (this.Settings.DownloadImageTiles)
                {
                    this.mainForm.UpdateChildTaskLabel("Downloading Image Tiles");

                    // Get a list of all the image tiles we need to download
                    switch (settings.OrthophotoSource)
                    {
                        case OrthophotoSource.Bing:
                            imageTiles = bingOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel);
                            break;
                        case OrthophotoSource.Google:
                            imageTiles = googleOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel);
                            break;
                        case OrthophotoSource.USGS:
                            imageTiles = usgsOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel);
                            break;
                    }

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
                    this.mainForm.UpdateChildTaskLabel("Stitching Image Tiles");

                    // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                    if (imageTiles == null)
                    {
                        imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                    }

                    await this.tileStitcher.StitchImageTilesAsync(imageTiles, tileDownloadDirectory, true);
                }

                // Generate AID and TMC Files
                if (this.Settings.GenerateAIDAndTMCFiles)
                {
                    this.mainForm.UpdateChildTaskLabel("Generating AFS Metadata Files");

                    // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                    if (imageTiles == null)
                    {
                        imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                    }

                    // Generate AID files for the image tiles
                    //await aidFileGenerator.GenerateAIDFilesAsync(imageTiles);
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
                i++;

                // We assume that the geoconvert process and the download process will take equal time. We don't know, but can't do much else.
                // At this point we should therefore be at (100 / nubmer of grid quares) / 2
                var progress = (int)Math.Floor((double)(100 / this.mainForm.SelectedAFS2GridSquares.Count()) / 2);

                this.mainForm.UpdateProgress(progress);
            }

            // Move on to running Geoconvert for each tile
            await this.StartGeoConvertProcessAsync();
        }

        public async Task StartGeoConvertProcessAsync()
        {
            int i = 0;
            // Run the Geoconvert process for each selected gride square
            foreach (AFS2GridSquare afs2GridSquare in this.mainForm.SelectedAFS2GridSquares.Values.Select(x => x.Item1))
            {
                this.mainForm.UpdateParentTaskLabel(String.Format("Working on AFS Grid Square {0} of {1}", i + 1, this.mainForm.SelectedAFS2GridSquares.Count()));

                List<ImageTile> imageTiles = null;

                // Do we have a directory for this afs grid square in our working directory?
                var afsGridSquareDirectory = this.settings.WorkingDirectory + afs2GridSquare.Name;

                if (Directory.Exists(afsGridSquareDirectory))
                {
                    var tileDownloadDirectory = GetTileDownloadDirectory(afsGridSquareDirectory);

                    if (Directory.Exists(tileDownloadDirectory))
                    {
                        // Run GeoConvert
                        if (this.Settings.RunGeoConvert)
                        {
                            this.mainForm.UpdateChildTaskLabel("Running GeoConvert");

                            // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                            if (imageTiles == null)
                            {
                                imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                            }

                        }

                        // Delete Stitched Immage Tiles
                        if (this.Settings.DeleteStitchedImageTiles)
                        {
                            this.mainForm.UpdateChildTaskLabel("Deleting Stitched Image Tiles");

                            // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                            if (imageTiles == null)
                            {
                                imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                            }

                        }

                        // Install Scenery
                        if (this.Settings.InstallScenery)
                        {
                            this.mainForm.UpdateChildTaskLabel("Prompting To Install Scenery");

                        }

                    }
                    else
                    {
                        // Tile download directory does not exist
                    }
                }
                else
                {
                    // Working directory does not exist
                }

                // We assume that the geoconvert process and the download process will take equal time. We don't know, but can't do much else.
                // At this point we should therefore be at (100 / nubmer of grid quares) / 2
                var progress = (int)Math.Floor((double)(100 / this.mainForm.SelectedAFS2GridSquares.Count()) / 2);

                this.mainForm.UpdateProgress(progress);

                i++;
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

        public void SaveSettings()
        {

        }

    }
}
