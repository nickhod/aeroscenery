using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.AFS2
{
    public class GeoConvertManager
    {
        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        public void RunGeoConvert(string stitchedTilesDirectory, MainForm mainForm)
        {
            if (Directory.Exists(stitchedTilesDirectory))
            {
                var tmcFiles = Directory.EnumerateFiles(stitchedTilesDirectory, "*.tmc").ToList();

                foreach (string tmcFilename in tmcFiles)
                {
                    string geoconvertPath = String.Format("{0}\\aerofly_fs_2_geoconvert", AeroSceneryManager.Instance.Settings.AFS2SDKDirectory);
                    string geoconvertFilename = String.Format("{0}\\aerofly_fs_2_geoconvert.exe", geoconvertPath);

                    if (!File.Exists(geoconvertFilename))
                    {
                        log.Error("Could not find GeoConvert");

                        DialogResult result = MessageBox.Show("Could not find GeoConvert",
                            "AeroScenery",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        mainForm.UpdateChildTaskLabel("Running GeoConvert");
                        log.Info("Running GeoConvert");

                        var proc = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = geoconvertFilename,
                                Arguments = tmcFilename,
                                UseShellExecute = true,
                                RedirectStandardOutput = false,
                                CreateNoWindow = false,
                                WorkingDirectory = geoconvertPath
                            }
                        };

                        proc.Start();

                    }

                }

            }
            else
            {
                // Tile download directory does not exist
            }
        }
    }
}
