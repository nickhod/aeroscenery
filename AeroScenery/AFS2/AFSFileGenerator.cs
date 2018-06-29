using AeroScenery.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AeroScenery.AFS2
{
    public class AFSFileGenerator
    {
        // The prefix of each image tile and aero file, e.g. g_19_
        private string filenamePrefix;

        private XmlSerializer xmlSerializer;

        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        public AFSFileGenerator()
        {
            this.xmlSerializer = new XmlSerializer(typeof(StitchedImage));
        }

        public async Task GenerateAFSFilesAsync(AFS2GridSquare afs2GridSquare, string stitchedTilesDirectory, string afsGridSquareDirectory, IProgress<AFSFileGeneratorProgress> progress)
        {
            await Task.Run(() =>
            {
                var afsFileGeneratorProgress = new AFSFileGeneratorProgress();

                StitchedImage firstStitchedImageAeroFile = null;

                // The number of stiched tiles should always be pretty manageable so we can get a list of filenames

                if (Directory.Exists(stitchedTilesDirectory))
                {
                    string[] stitchedImagesAeroFiles = Directory.GetFiles(stitchedTilesDirectory, "*.aero");

                    int i = 0;

                    foreach (string aeroFilename in stitchedImagesAeroFiles)
                    {
                        try
                        {
                            StitchedImage stitchedImageAeroFile;

                            using (StreamReader reader = new StreamReader(aeroFilename))
                            {
                                stitchedImageAeroFile = (StitchedImage)xmlSerializer.Deserialize(reader);
                                reader.Close();
                            }

                            if (i == 0)
                            {
                                firstStitchedImageAeroFile = stitchedImageAeroFile;
                            }

                            var stepsPerPixelX = Math.Abs((stitchedImageAeroFile.WestLongitude - stitchedImageAeroFile.EastLongitude) / stitchedImageAeroFile.Width);
                            var stepsPerPixelY = -Math.Abs((stitchedImageAeroFile.NorthLatitude - stitchedImageAeroFile.SouthLatitude) / stitchedImageAeroFile.Height);

                            var aidFile = new AIDFile();

                            aidFile.ImageFile = stitchedImageAeroFile.FileName + "." + stitchedImageAeroFile.ImageExtension;
                            aidFile.FlipVertical = false;
                            aidFile.StepsPerPixelX = stepsPerPixelX;
                            aidFile.StepsPerPixelY = stepsPerPixelY;
                            aidFile.X = stitchedImageAeroFile.WestLongitude;
                            aidFile.Y = stitchedImageAeroFile.NorthLatitude;

                            var aidFileStr = aidFile.ToString();

                            string path = stitchedTilesDirectory + stitchedImageAeroFile.FileName + ".aid";

                            log.InfoFormat("Writing AID file {0}", path);
                            File.WriteAllText(path, aidFileStr);
                        }
                        catch (Exception ex)
                        {

                        }

                        i++;
                    }

                    this.GenerateTMCFile(afs2GridSquare, stitchedTilesDirectory, afsGridSquareDirectory, firstStitchedImageAeroFile);


                }
            });


        }

        private void GenerateTMCFile(AFS2GridSquare afs2GridSquare, string stitchedTilesDirectory, string afsGridSquareDirectory, StitchedImage firstStitchedImageAeroFile)
        {
            // Create directories for Geoconvert output if they do not exist.
            // Better to do this here in case anyone wants to run Geoconvert manually
            var geoConvertRawDirectory = String.Format("{0}-geoconvert-raw\\", firstStitchedImageAeroFile.ZoomLevel);
            var geoConvertTTCDirectory = String.Format("{0}-geoconvert-ttc\\", firstStitchedImageAeroFile.ZoomLevel);
            var geoConvertRawPath = afsGridSquareDirectory + geoConvertRawDirectory;
            var geoConvertTTCPath = afsGridSquareDirectory + geoConvertTTCDirectory;

            if (!Directory.Exists(geoConvertRawPath))
            {
                Directory.CreateDirectory(geoConvertRawPath);
            }

            if (!Directory.Exists(geoConvertTTCPath))
            {
                Directory.CreateDirectory(geoConvertTTCPath);
            }

            var tmcFile = new TMCFile();

            tmcFile.AlwaysOverwrite = true;
            tmcFile.DoHeightmaps = false;
            tmcFile.FolderDestinationRaw = geoConvertRawPath;
            tmcFile.FolderDestinationTTC = geoConvertTTCPath;
            tmcFile.FolderSourceFiles = stitchedTilesDirectory;
            tmcFile.WriteImagesWithMask = true;
            tmcFile.WriteRawFiles = true;
            tmcFile.WriteTTCFiles = true;

            // All TMC regions will have the same lat / lon max and min
            // Create a template Region here to base other regions off
            TMCRegion tmcRegionTemplate = new TMCRegion();

            // Really NW Corner
            tmcRegionTemplate.LatMin = afs2GridSquare.NorthLatitude;
            tmcRegionTemplate.LonMin = afs2GridSquare.WestLongitude;

            // Realy SE Corner
            tmcRegionTemplate.LatMax = afs2GridSquare.SouthLatitude;
            tmcRegionTemplate.LonMax = afs2GridSquare.EastLongitude;

            tmcFile.Regions = this.GenerateTMCFileRegions(tmcRegionTemplate);

            var tmcFileStr = tmcFile.ToString();

            var filenameParts = firstStitchedImageAeroFile.FileName.Split('_');
            var tmcFilename = String.Format("{0}_{1}_{2}", filenameParts[0], filenameParts[1], filenameParts[2]);

            string path = String.Format("{0}{1}.tmc", stitchedTilesDirectory, tmcFilename);

            File.WriteAllText(path, tmcFileStr);
        }

        private List<TMCRegion> GenerateTMCFileRegions(TMCRegion tmcRegionTemplate)
        {
            var settings = AeroSceneryManager.Instance.Settings;

            List<TMCRegion> regions = new List<TMCRegion>();

            foreach (int afsLevel in settings.AFSLevelsToGenerate)
            {
                var region = new TMCRegion();
                region.LatMax = tmcRegionTemplate.LatMax;
                region.LonMax = tmcRegionTemplate.LonMax;
                region.LatMin = tmcRegionTemplate.LatMin;
                region.LonMin = tmcRegionTemplate.LonMin;
                region.Level = afsLevel;
                region.WriteImagesWithMask = true;
                regions.Add(region);
            }

            return regions;

        }
    }
}
