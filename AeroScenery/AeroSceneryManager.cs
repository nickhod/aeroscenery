using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.Controls;
using AeroScenery.Data;
using AeroScenery.Data.Mappers;
using AeroScenery.Data.Models;
using AeroScenery.Download;
using AeroScenery.FSCloudPort;
using AeroScenery.ImageProcessing;
using AeroScenery.OrthophotoSources;
using AeroScenery.OrthophotoSources.Japan;
using AeroScenery.OrthophotoSources.NewZealand;
using AeroScenery.OrthophotoSources.Norway;
using AeroScenery.OrthophotoSources.Spain;
using AeroScenery.OrthophotoSources.Sweden;
using AeroScenery.OrthophotoSources.Switzerland;
using AeroScenery.OrthophotoSources.UnitedStates;
using AeroScenery.OrthoPhotoSources;
using AeroScenery.UI;
using log4net;
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
        private MainForm mainForm;

        private BingOrthophotoSource bingOrthophotoSource;
        private GoogleOrthophotoSource googleOrthophotoSource;
        private USGSOrthophotoSource usgsOrthophotoSource;
        private GSIOrthophotoSource gsiOrthophotoSource;
        private LinzOrthophotoSource linzOrthophotoSource;
        private NorgeBilderOrthophotoSource norgeBilderOrthophotoSource;
        private IDEIBOrthophotoSource ideibOrthophotoSource;
        private IGNOrthophotoSource ignOrthophotoSource;
        private LantmaterietOrthophotoSource lantmaterietOrthophotoSource;
        private GeoportalOrthophotoSource geoportalOrthophotoSource;
        private ArcGISOrthophotoSource arcGISOrthophotoSource;
        private HittaOrthophotoSource hittaOrthophotoSource;

        private DownloadManager downloadManager;

        private GeoConvertManager geoConvertManager;

        private DownloadFailedForm downloadFailedForm;

        private TileStitcher tileStitcher;

        private static AeroSceneryManager aeroSceneryManager;

        private ImageTileService imageTileService;

        private Common.Settings settings;

        private SettingsService settingsService;

        private IDataRepository dataRepository;

        private GridSquareMapper gridSquareMapper;

        private AFSFileGenerator afsFileGenerator;

        private List<ImageTile> imageTiles;
        private readonly ILog log = LogManager.GetLogger("AeroScenery");
        private string version;
        private int incrementalVersion;

        public AeroSceneryManager()
        {
            downloadManager = new DownloadManager();
            geoConvertManager = new GeoConvertManager();
            imageTileService = new ImageTileService();
            tileStitcher = new TileStitcher();
            settingsService = new SettingsService();
            gridSquareMapper = new GridSquareMapper();
            afsFileGenerator = new AFSFileGenerator();
            dataRepository = new SqlLiteDataRepository();

            imageTiles = null;
            version = "1.1.1";
            incrementalVersion = 11;
        }

        public Settings Settings
        {
            get
            {
                return this.settings;
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
        }

        public int IncrementalVersion
        {
            get
            {
                return this.incrementalVersion;
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
            // Create settings if required and read them
            this.settings = settingsService.GetSettings();
            settingsService.LogSettings(this.settings);
            settingsService.CheckConfiguredDirectories(this.settings);

            this.dataRepository.Settings = settings;
            this.dataRepository.UpgradeDatabase();

            var gridSquareNameFixer = new GridSquareNameFixer(settings, this.dataRepository, this.settingsService);
            gridSquareNameFixer.FixGridSquareNames();

            bingOrthophotoSource = new BingOrthophotoSource(settings.OrthophotoSourceSettings.BN_OrthophotoSourceUrlTemplate);
            googleOrthophotoSource = new GoogleOrthophotoSource(settings.OrthophotoSourceSettings.GM_OrthophotoSourceUrlTemplate);
            usgsOrthophotoSource = new USGSOrthophotoSource();
            gsiOrthophotoSource = new GSIOrthophotoSource();
            linzOrthophotoSource = new LinzOrthophotoSource();
            norgeBilderOrthophotoSource = new NorgeBilderOrthophotoSource();
            ideibOrthophotoSource = new IDEIBOrthophotoSource();
            ignOrthophotoSource = new IGNOrthophotoSource();
            lantmaterietOrthophotoSource = new LantmaterietOrthophotoSource();
            geoportalOrthophotoSource = new GeoportalOrthophotoSource();
            arcGISOrthophotoSource = new ArcGISOrthophotoSource();
            hittaOrthophotoSource = new HittaOrthophotoSource();

            this.mainForm = new MainForm();
            this.mainForm.StartStopClicked += async (sender, eventArgs) =>
            {
                if (this.mainForm.ActionsRunning)
                {
                    await StartSceneryGenerationProcessAsync(sender, eventArgs);
                }
                else
                {
                    StopSceneryGenerationProcess(sender, eventArgs);
                }
            };

            this.mainForm.ResetGridSquare += (sender, name) =>
            {
                this.ResetGridSquare(name);
            };

            this.mainForm.Initialize();
            Application.Run(this.mainForm);

        }


        private string GetTileDownloadDirectory(string afsGridSquareDirectory)
        {
            var tileDownloadDirectory = afsGridSquareDirectory;

            switch (this.settings.OrthophotoSource)
            {
                case OrthophotoSource.Bing:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.Bing);
                    break;
                case OrthophotoSource.Google:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.Google);
                    break;
                case OrthophotoSource.ArcGIS:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.ArcGIS);
                    break;
                case OrthophotoSource.US_USGS:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.US_USGS);
                    break;
                case OrthophotoSource.NZ_Linz:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.NZ_Linz);
                    break;
                case OrthophotoSource.ES_IDEIB:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.ES_IDEIB);
                    break;
                case OrthophotoSource.CH_Geoportal:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.CH_Geoportal);
                    break;
                case OrthophotoSource.NO_NorgeBilder:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.NO_NorgeBilder);
                    break;
                case OrthophotoSource.SE_Lantmateriet:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.SE_Lantmateriet);
                    break;
                case OrthophotoSource.ES_IGN:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.ES_IGN);
                    break;
                case OrthophotoSource.JP_GSI:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.JP_GSI);
                    break;
                case OrthophotoSource.SE_Hitta:
                    tileDownloadDirectory += String.Format("\\{0}\\", OrthophotoSourceDirectoryName.SE_Hitta);
                    break;
            }

            return tileDownloadDirectory;
        }

        public void StopSceneryGenerationProcess(object sender, EventArgs e)
        {
            downloadManager.StopDownloads();

            if (this.imageTiles != null)
            {
                this.imageTiles.Clear();
                this.imageTiles = null;
                System.GC.Collect();
            }

        }

        private void ActionsComplete()
        {
            this.mainForm.ActionsComplete();

            if (this.imageTiles != null)
            {
                this.imageTiles.Clear();
                this.imageTiles = null;
                System.GC.Collect();
            }

        }




        public async Task StartSceneryGenerationProcessAsync(object sender, EventArgs e)
        {
            try
            {
                // Set settings on orthophoto sources
                this.linzOrthophotoSource.ApiKey = settings.LinzApiKey;

                int i = 0;
                foreach (AFS2GridSquare afs2GridSquare in this.mainForm.SelectedAFS2GridSquares.Values.Select(x => x.AFS2GridSquare))
                {
                    var currentGrideSquareMessage = String.Format("Working on AFS Grid Square {0} of {1}", i + 1, this.mainForm.SelectedAFS2GridSquares.Count());
                    this.mainForm.UpdateParentTaskLabel(currentGrideSquareMessage);
                    log.Info(currentGrideSquareMessage);

                    // Do we have a directory for this afs grid square in our working directory?
                    var afsGridSquareDirectory = this.settings.WorkingDirectory + afs2GridSquare.Name;

                    if (!Directory.Exists(this.settings.WorkingDirectory + afs2GridSquare.Name))
                    {
                        Directory.CreateDirectory(this.settings.WorkingDirectory + afs2GridSquare.Name);
                    }

                    var tileDownloadDirectory = GetTileDownloadDirectory(afsGridSquareDirectory) + this.settings.ZoomLevel + @"\";
                    var stitchedTilesDirectory = GetTileDownloadDirectory(afsGridSquareDirectory) + this.settings.ZoomLevel + @"-stitched\";

                    // Do we have a directory for the afs grid square and this orthophoto source
                    if (!Directory.Exists(tileDownloadDirectory))
                    {
                        Directory.CreateDirectory(tileDownloadDirectory);
                    }

                    if (!Directory.Exists(stitchedTilesDirectory))
                    {
                        Directory.CreateDirectory(stitchedTilesDirectory);
                    }

                    // Download Imamge Tiles
                    if (this.Settings.DownloadImageTiles.Value && this.mainForm.ActionsRunning)
                    {
                        this.mainForm.UpdateChildTaskLabel("Calculating Image Tiles To Download");
                        log.Info("Calculating Image Tiles To Download");

                        GenericOrthophotoSource orthophotoSourceInstance = null;

                        var imageTilesTask = Task.Run(() => {

                            // Get a list of all the image tiles we need to download
                            switch (settings.OrthophotoSource)
                            {
                                case OrthophotoSource.Bing:
                                    imageTiles = bingOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = bingOrthophotoSource;
                                    break;
                                case OrthophotoSource.Google:
                                    imageTiles = googleOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = googleOrthophotoSource;
                                    break;
                                case OrthophotoSource.ArcGIS:
                                    imageTiles = arcGISOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = arcGISOrthophotoSource;
                                    break;
                                case OrthophotoSource.US_USGS:
                                    imageTiles = usgsOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = usgsOrthophotoSource;
                                    break;
                                case OrthophotoSource.NZ_Linz:
                                    imageTiles = linzOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = linzOrthophotoSource;
                                    break;
                                case OrthophotoSource.ES_IDEIB:
                                    imageTiles = ideibOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = ideibOrthophotoSource;
                                    break;
                                case OrthophotoSource.CH_Geoportal:
                                    imageTiles = geoportalOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = geoportalOrthophotoSource;
                                    break;
                                case OrthophotoSource.NO_NorgeBilder:
                                    imageTiles = norgeBilderOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = norgeBilderOrthophotoSource;
                                    break;
                                case OrthophotoSource.SE_Lantmateriet:
                                    imageTiles = lantmaterietOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = lantmaterietOrthophotoSource;
                                    break;
                                case OrthophotoSource.ES_IGN:
                                    imageTiles = ignOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = ignOrthophotoSource;
                                    break;
                                case OrthophotoSource.JP_GSI:
                                    imageTiles = gsiOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = gsiOrthophotoSource;
                                    break;
                                case OrthophotoSource.SE_Hitta:
                                    imageTiles = hittaOrthophotoSource.ImageTilesForGridSquares(afs2GridSquare, settings.ZoomLevel.Value);
                                    orthophotoSourceInstance = hittaOrthophotoSource;
                                    break;

                            }
                        });

                        await imageTilesTask;

                        this.mainForm.UpdateChildTaskLabel("Downloading Image Tiles");
                        log.Info("Downloading Image Tiles");

                        // Capture the progress of each thread
                        var downloadThreadProgress = new Progress<DownloadThreadProgress>();
                        downloadThreadProgress.ProgressChanged += DownloadThreadProgress_ProgressChanged;

                        // Send the image tiles to the download manager
                        await downloadManager.DownloadImageTiles(settings.OrthophotoSource.Value, imageTiles, downloadThreadProgress, tileDownloadDirectory, orthophotoSourceInstance);

                        // Only finalise if we weren't cancelled
                        if (this.mainForm.ActionsRunning)
                        {
                            var existingGridSquare = this.dataRepository.FindGridSquare(afs2GridSquare.Name);

                            if (existingGridSquare == null)
                            {
                                this.dataRepository.CreateGridSquare(this.gridSquareMapper.ToModel(afs2GridSquare));
                                this.mainForm.AddDownloadedGridSquare(afs2GridSquare);
                            }
                        }


                    }

                    // Stitch Image Tiles
                    if (this.Settings.StitchImageTiles.Value && this.mainForm.ActionsRunning)
                    {
                        this.mainForm.UpdateChildTaskLabel("Stitching Image Tiles");
                        log.Info("Stitching Image Tiles");

                        // Capture the progress of the tile stitcher
                        var tileStitcherProgress = new Progress<TileStitcherProgress>();
                        tileStitcherProgress.ProgressChanged += TileStitcherProgress_ProgressChanged;

                        await this.tileStitcher.StitchImageTilesAsync(tileDownloadDirectory, stitchedTilesDirectory, true, tileStitcherProgress);
                    }

                    // Generate AID and TMC Files
                    if (this.Settings.GenerateAIDAndTMCFiles.Value && this.mainForm.ActionsRunning)
                    {
                        this.mainForm.UpdateChildTaskLabel("Generating AFS Metadata Files");
                        log.Info("Generating AFS Metadata Files");

                        // Capture the progress of the tile stitcher
                        var afsFileGeneratorProgress = new Progress<AFSFileGeneratorProgress>();
                        afsFileGeneratorProgress.ProgressChanged += AFSFileGeneratorProgress_ProgressChanged;


                        // Generate AID files for the image tiles
                        await afsFileGenerator.GenerateAFSFilesAsync(afs2GridSquare, stitchedTilesDirectory, GetTileDownloadDirectory(afsGridSquareDirectory), afsFileGeneratorProgress);
                    }
                    i++;

                }

                // If required Move on to running Geoconvert for each tile
                if (this.settings.RunGeoConvert.Value && this.mainForm.ActionsRunning)
                {
                    this.StartGeoConvertProcess();
                }

                // Delete Stitched Immage Tiles
                //if (this.Settings.DeleteStitchedImageTiles)
                //{
                //    this.mainForm.UpdateChildTaskLabel("Deleting Stitched Image Tiles");

                //    // If we haven't just downloaded image tiles we need to load aero files to get image tile objects
                //    if (imageTiles == null)
                //    {
                //        imageTiles = await this.imageTileService.LoadImageTilesAsync(tileDownloadDirectory);
                //    }

                //}


                // Install Scenery
                //if (this.Settings.InstallScenery)
                //{
                //    this.mainForm.UpdateChildTaskLabel("Prompting To Install Scenery");

                //}

                this.ActionsComplete();

            }
            finally
            {
                if (this.imageTiles != null)
                {
                    this.imageTiles.Clear();
                    this.imageTiles = null;
                    System.GC.Collect();
                }
            }

        }

        public void StartGeoConvertProcess()
        {
            if (this.mainForm.ActionsRunning)
            {
                if (String.IsNullOrEmpty(this.settings.AFS2SDKDirectory))
                {
                    var messageBox = new CustomMessageBox("Please set the location of the Aerofly SDK in Settings before running Geoconvert",
                        "AeroScenery",
                        MessageBoxIcon.Warning);

                    messageBox.ShowDialog();
                }
                else
                {
                    if (settings.AFSLevelsToGenerate.Count == 0)
                    {
                        var messageBox = new CustomMessageBox("Please choose one or more AFS levels to generate before running Geoconvert",
                            "AeroScenery",
                            MessageBoxIcon.Warning);

                        messageBox.ShowDialog();
                    }
                    else
                    {

                        if (this.mainForm.SelectedAFS2GridSquares.Count > 1 && 
                            this.settings.GeoConvertUseWrapper.Value == false)
                        {
                            string message = "When running GeoConvert on multiple squares it's advisable to use GeoCovnert Wrapper.\n";
                            message += "This will make GeoConvert instances run sequentially rather than in parallel.\n";
                            message += "You can enable GeoConvert Wrapper in the GeoConvert tab of the settings form.\n";
                            message += "See the AeroScenery wiki for more information on how this works.\n";


                            var messageBox = new CustomMessageBox(message,
                                "AeroScenery",
                                MessageBoxIcon.Information);

                            messageBox.ShowDialog();

                        }

                        RunGeoConvertProcess();

                    }


                }

            }

        }

        public void RunGeoConvertProcess()
        {
            log.Info("Starting GeoConvert Process");

            int i = 0;

            // Run the Geoconvert process for each selected grid square
            foreach (AFS2GridSquare afs2GridSquare in this.mainForm.SelectedAFS2GridSquares.Values.Select(x => x.AFS2GridSquare))
            {
                if (this.mainForm.ActionsRunning)
                {
                    var currentGrideSquareMessage = String.Format("Working on AFS Grid Square {0} of {1}", i + 1, this.mainForm.SelectedAFS2GridSquares.Count());
                    this.mainForm.UpdateParentTaskLabel(currentGrideSquareMessage);
                    log.Info(currentGrideSquareMessage);

                    // Do we have a directory for this afs grid square in our working directory?
                    var afsGridSquareDirectory = this.settings.WorkingDirectory + afs2GridSquare.Name;

                    if (Directory.Exists(afsGridSquareDirectory))
                    {
                        var stitchedTilesDirectory = GetTileDownloadDirectory(afsGridSquareDirectory) + this.settings.ZoomLevel + @"-stitched\";

                        if (Directory.Exists(stitchedTilesDirectory))
                        {
                            // Create raw and ttc directories if required. They could have been deleted manually.
                            var rawDirectory = GetTileDownloadDirectory(afsGridSquareDirectory) + this.settings.ZoomLevel + @"-geoconvert-raw\";
                            var ttcDirectory = GetTileDownloadDirectory(afsGridSquareDirectory) + this.settings.ZoomLevel + @"-geoconvert-ttc\";

                            if (!Directory.Exists(rawDirectory))
                            {
                                Directory.CreateDirectory(rawDirectory);
                            }

                            if (!Directory.Exists(ttcDirectory))
                            {
                                Directory.CreateDirectory(ttcDirectory);
                            }

                            this.geoConvertManager.RunGeoConvert(stitchedTilesDirectory, this.mainForm, this.settings.GeoConvertUseWrapper.Value);
                        }
                        else
                        {
                            var messageBox = new CustomMessageBox(String.Format("Could not find any stitched images for the grid square {0}", afs2GridSquare.Name),
                                "AeroScenery",
                                MessageBoxIcon.Error);

                            messageBox.ShowDialog();
                        }

                    }
                    else
                    {
                        // Working directory does not exist
                    }

                    i++;
                }

            }
        }

        private void ResetGridSquare(string gridSquareName)
        {
            var existingGridSquare = this.dataRepository.FindGridSquare(gridSquareName);

            if (existingGridSquare != null)
            {
                this.dataRepository.DeleteGridSquare(gridSquareName);
            }
        }


        private void DownloadThreadProgress_ProgressChanged(object sender, DownloadThreadProgress progress)
        {
            if (this.mainForm.ActionsRunning)
            {
                var progressControl = this.mainForm.GetDownloadThreadProgressControl(progress.DownloadThreadNumber);
                var percentageProgress = (int)Math.Floor(((double)progress.FilesDownloaded / (double)progress.TotalFiles) * 100);

                progressControl.SetProgressPercentage(percentageProgress);

                progressControl.SetImageTileCount(progress.FilesDownloaded, progress.TotalFiles);

                var downloadActionProgressPercentage = this.mainForm.CurrentActionProgressPercentage;

                if (percentageProgress > downloadActionProgressPercentage)
                {
                    this.mainForm.CurrentActionProgressPercentage = percentageProgress;
                }
            }

        }


        private void TileStitcherProgress_ProgressChanged(object sender, TileStitcherProgress progress)
        {
            if (this.mainForm.ActionsRunning)
            {

                var currentStitchedImagePercentage = ((double)progress.CurrentStitchedImage / (double)progress.TotalStitchedImages);
                var nextStitchedImagePercentage = ((double)(progress.CurrentStitchedImage + 1) / (double)progress.TotalStitchedImages);

                var tilesPercentage = ((double)(progress.CurrentTilesRenderedForCurrentStitchedImage) / (double)progress.TotalImageTilesForCurrentStitchedImage);

                var percentageIncreaseBetweenThisStitchedImageAndNext = nextStitchedImagePercentage - currentStitchedImagePercentage;

                var finalPercentageDbl = (currentStitchedImagePercentage + (percentageIncreaseBetweenThisStitchedImageAndNext * tilesPercentage)) * 100;
                //Debug.WriteLine(finalPercentageDbl);

                var finalPercentage = (int)Math.Floor(finalPercentageDbl);

                if (finalPercentage > 100)
                {
                    finalPercentage = 100;
                }

                this.mainForm.CurrentActionProgressPercentage = finalPercentage;

            }

        }

        private void AFSFileGeneratorProgress_ProgressChanged(object sender, AFSFileGeneratorProgress progress)
        {
            if (this.mainForm.ActionsRunning)
            {
                var precentDone = ((double)progress.FilesCreated / (double)progress.TotalFiles) * 100;

                this.mainForm.CurrentActionProgressPercentage = (int)precentDone;

            }
        }

        public bool AllImageTilesDownloaded(List<ImageTile> imageTiles)
        {
            return true;
        }

        public void SaveSettings()
        {
            this.settingsService.SaveSettings(this.settings);
        }

        public string ApplicationPath
        {
            get
            {
                var applicationUri = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var applicationLocalPath = new Uri(Path.GetDirectoryName(applicationUri)).LocalPath;
                return applicationLocalPath;

            }
        }


    }
}
