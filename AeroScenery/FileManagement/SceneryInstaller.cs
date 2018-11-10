using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.Controls;
using AeroScenery.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.FileManagement
{
    public class SceneryInstaller
    {
        private AFS2Grid afsGrid;


        public SceneryInstaller()
        {
            this.afsGrid = new AFS2Grid();
        }

        public DialogResult ConfirmSceneryInstallation(AFS2GridSquare afs2GridSquare)
        {
            var gridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + afs2GridSquare.Name;

            DialogResult result = DialogResult.No;

            // Does this grid square exist
            if (Directory.Exists(gridSquareDirectory))
            {
                // Do we have an Aerofly folder to install into?
                string afsSceneryInstallDirectory = DirectoryHelper.FindAFSSceneryInstallDirectory(AeroSceneryManager.Instance.Settings);

                if (afsSceneryInstallDirectory != null)
                {
                    // Confirm that the user does want to install scenery
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("Are you sure you want to install all scenery for this grid square?");
                    sb.AppendLine("Any existing files in the same destination folder will be overwritten.");
                    sb.AppendLine("");
                    sb.AppendLine(String.Format("Destination: {0}", afsSceneryInstallDirectory));

                    var messageBox = new CustomMessageBox(sb.ToString(),
                        "AeroScenery",
                        MessageBoxIcon.Question);

                    messageBox.SetButtons(
                        new string[] { "Yes", "No" },
                        new DialogResult[] { DialogResult.Yes, DialogResult.No });

                    result = messageBox.ShowDialog();
                }
            }
            else
            {

            }

            return result;
        }

        public DialogResult? CheckForDuplicateTTCFiles(AFS2GridSquare afs2GridSquare, out List<string> ttcFiles)
        {
            // A null dialog result means that there are no duplicates
            DialogResult? result = null;

            var gridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + afs2GridSquare.Name;

            ttcFiles = this.EnumerateFilesRecursive(gridSquareDirectory, "*.ttc").ToList();

            // Check for duplicate ttc files
            if (ttcFiles.Count != ttcFiles.Distinct().Count())
            {
                StringBuilder sbDuplicates = new StringBuilder();

                sbDuplicates.AppendLine(String.Format("Duplicate ttc files were found in the folder for grid square ({0})", afs2GridSquare.Name));
                sbDuplicates.AppendLine("This may be because you have downloaded this grid square with multiple map image providers.");
                sbDuplicates.AppendLine("If you continue with the install you may get a mismatched set of ttc files.");

                var duplicatesMessageBox = new CustomMessageBox(sbDuplicates.ToString(),
                    "AeroScenery",
                    MessageBoxIcon.Warning);

                duplicatesMessageBox.SetButtons(
                    new string[] { "OK", "Cancel" },
                    new DialogResult[] { DialogResult.OK, DialogResult.Cancel });

                DialogResult duplicatesResult = duplicatesMessageBox.ShowDialog();

                result = duplicatesMessageBox.ShowDialog();

            }

            return result;
        }

        public async Task InstallSceneryAsync(AFS2GridSquare afs2GridSquare, List<string> ttcFiles)
        {
            var task = Task.Run(() =>
            {
                var gridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + afs2GridSquare.Name;

                // Does this grid square exist
                if (Directory.Exists(gridSquareDirectory))
                {
                    // Do we have an Aerofly folder to install into?
                    string afsSceneryInstallDirectory = DirectoryHelper.FindAFSSceneryInstallDirectory(AeroSceneryManager.Instance.Settings);

                    if (afsSceneryInstallDirectory != null)
                    {
                        // We install ttc files into a folder of the level 9 grid square containing the selected grid square
                        var level9GridSquare = afs2GridSquare;

                        if (afs2GridSquare.Level != 9)
                        {
                            level9GridSquare = this.afsGrid.GetGridSquareAtLatLon(afs2GridSquare.GetCenter().Lat, afs2GridSquare.GetCenter().Lng, 9);
                        }

                        // This is now the level9 grid square that contains the selected grid square
                        var afsSceneryFinalInstallDirectory = String.Format(@"{0}\{1}", afsSceneryInstallDirectory, afs2GridSquare.Name);

                        if (!Directory.Exists(afsSceneryFinalInstallDirectory))
                        {
                            Directory.CreateDirectory(afsSceneryFinalInstallDirectory);
                        }

                        // Copy the files over
                        foreach(var ttcFilePath in ttcFiles)
                        {
                            var filename = Path.GetFileName(ttcFilePath);
                            var destinationPath = String.Format(@"{0}/{1}", afsSceneryFinalInstallDirectory, filename);
                            File.Copy(ttcFilePath, destinationPath);
                        }

                    }

                }

            });

            await task;
        }

        /// <summary>
        /// Recursively enumerates files. Silently fails if it doesn't have access to any files.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private IEnumerable<string> EnumerateFilesRecursive(string root, string pattern = "*")
        {
            var todo = new Queue<string>();
            todo.Enqueue(root);
            while (todo.Count > 0)
            {
                string dir = todo.Dequeue();
                string[] subdirs = new string[0];
                string[] files = new string[0];
                try
                {
                    subdirs = Directory.GetDirectories(dir);
                    files = Directory.GetFiles(dir, pattern);
                }
                catch (IOException)
                {
                }
                catch (System.UnauthorizedAccessException)
                {
                }

                foreach (string subdir in subdirs)
                {
                    todo.Enqueue(subdir);
                }
                foreach (string filename in files)
                {
                    yield return filename;
                }
            }
        }


    }
}
