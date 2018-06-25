using AeroScenery.Common;
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

        private AIDFileGenerator aidFileGenerator;

        private TMCFileGenerator tmcFileGenerator;


        public AFSFileGenerator()
        {
            aidFileGenerator = new AIDFileGenerator();
            tmcFileGenerator = new TMCFileGenerator();
            this.xmlSerializer = new XmlSerializer(typeof(StitchedImage));
        }

        public async Task GenerateAFSFilesAsync(string stitchedTilesDirectory, IProgress<AFSFileGeneratorProgress> progress)
        {
            await Task.Run(() =>
            {
                var afsFileGeneratorProgress = new AFSFileGeneratorProgress();


                // The number of stiched tiles should always be pretty manageable so we can get a list of filenames

                if (Directory.Exists(stitchedTilesDirectory)) {
                    string[] stitchedImagesAeroFiles = Directory.GetFiles(stitchedTilesDirectory, "*.aero");

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

                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            });


        }
    }
}
