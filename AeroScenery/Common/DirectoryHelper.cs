using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    public static class DirectoryHelper
    {
        /// <summary>
        /// Gets and checks the existence of the configured directory to install scenery into
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static string FindAFSSceneryInstallDirectory(Settings settings)
        {
            string afsSceneryInstallDirectory = null;

            // Is a custom user directory configured?
            if (!String.IsNullOrEmpty(settings.AFS2UserDirectory))
            {
                string afsUserDirectoryPath = settings.AFS2UserDirectory;

                // Does the root directory exist
                if (Directory.Exists(afsUserDirectoryPath))
                {
                    // If no scenery sub-directory exists, create one
                    string afsUserDirectorySceneryPath = afsUserDirectoryPath + @"scenery\";

                    if (!Directory.Exists(afsUserDirectorySceneryPath))
                    {
                        Directory.CreateDirectory(afsUserDirectorySceneryPath);
                    }

                    // If no images sub-directory exists, create one
                    string afsUserDirectorySceneryImagesPath = afsUserDirectorySceneryPath + @"images\";

                    if (!Directory.Exists(afsUserDirectorySceneryImagesPath))
                    {
                        Directory.CreateDirectory(afsUserDirectorySceneryImagesPath);
                    }

                    afsSceneryInstallDirectory = afsUserDirectorySceneryImagesPath;
                }
            }
            else
            {
                string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string afsMyDocsSceneryPath = myDocumentsPath + @"\Aerofly FS 2\scenery\";

                // Does the My Documents AFS scenery directory exist? (It should)
                if (Directory.Exists(afsMyDocsSceneryPath))
                {
                    // There should already be an images sub-directory, but create one if not
                    string afsMyDocsSceneryImagesPath = afsMyDocsSceneryPath + @"images";

                    // If the images sub directory doesn't exist, create it
                    if (!Directory.Exists(afsMyDocsSceneryImagesPath))
                    {
                        Directory.CreateDirectory(afsMyDocsSceneryImagesPath);
                    }

                    afsSceneryInstallDirectory = afsMyDocsSceneryImagesPath;
                }
            }

            return afsSceneryInstallDirectory;
        }
    }
}
