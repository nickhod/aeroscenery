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
        public async Task StartSceneryInstallation(AFS2GridSquare afs2GridSquare)
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

                        DialogResult result = messageBox.ShowDialog();

                        // Proceed with scenery install
                        if (result == DialogResult.Yes)
                        {

                            var ttcFiles = this.EnumerateFilesRecursive(gridSquareDirectory, "*.ttc").ToList();

                            var duplictesTTCFilesFound = this.CheckForDuplicateTTCFiles(ttcFiles);

                            if (duplictesTTCFilesFound)
                            {
                                var duplicatesMessageBox = new CustomMessageBox(String.Format("There is no image folder yet for grid square {0}", afs2GridSquare.Name),
                                    "AeroScenery",
                                    MessageBoxIcon.Information);

                                messageBox.ShowDialog();
                            }
                            else
                            {

                                // We always install into a level 9 grid square to prevent duplicates
                                //AFS2GridSquare level9GridSquare = null;

                                //if (afs2GridSquare.Level == 9)
                                //{
                                //    level9GridSquare = afs2GridSquare;
                                //}
                                //else
                                //{

                                //}



                                var fileOperationProgressForm = new FileOperationProgressForm();
                                fileOperationProgressForm.MessageText = "Deleting Files";
                                fileOperationProgressForm.Title = "Deleting Files";

                                fileOperationProgressForm.FileOperationTask = deleteTask;
                                await fileOperationProgressForm.DoTaskAsync();
                                fileOperationProgressForm = null;

                                // No duplicates proceed with install
                                this.CopyTTCFilesToAFSSceneryDirectory(afsSceneryInstallDirectory, ttcFiles)
                            }

                        }



                    }



                }



            });


        }

        private bool CheckForDuplicateTTCFiles(IList<string> ttcFiles)
        {
            return false;
        }

        private void CopyTTCFilesToAFSSceneryDirectory(string afsSceneryInstallDirectory, IList<string> ttcFiles)
        {

        }



        public void InstallScenery(string gridSquareName, string sourceDirectory, string destinationDirectory)
        {
            //System.IO.DirectoryInfo di = new DirectoryInfo(gridSquareDirectory);

            Task.Run(() =>
            {
                var ttcFiles = this.EnumerateFilesRecursive(sourceDirectory, "*.ttc");

                //foreach (DirectoryInfo dir in di.GetDirectories())
                //{
                //    // Fun C# bug where it refuses to delete a directory because it's not
                //    // empty, even though we are doing recursive
                //    // We just wait a bit and try again
                //    try
                //    {
                //        dir.Delete(true);
                //    }
                //    catch (IOException)
                //    {
                //        Thread.Sleep(100);
                //        dir.Delete(true);
                //    }
                //}

                //foreach (FileInfo file in di.GetFiles())
                //{
                //    file.Delete();
                //}

            });
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


        //public async Task<bool> DuplicateTTCFilesFoundAsync(string sourceDirectory)
        //{
        //    var sdf = "sdf";

        //    var task = Task.Run(() =>
        //    {

        //        sdf = "sdf";

        //        var ttcFiles = this.EnumerateFilesRecursive(sourceDirectory, "*.ttc");

        //        //foreach (DirectoryInfo dir in di.GetDirectories())
        //        //{
        //        //    // Fun C# bug where it refuses to delete a directory because it's not
        //        //    // empty, even though we are doing recursive
        //        //    // We just wait a bit and try again
        //        //    try
        //        //    {
        //        //        dir.Delete(true);
        //        //    }
        //        //    catch (IOException)
        //        //    {
        //        //        Thread.Sleep(100);
        //        //        dir.Delete(true);
        //        //    }
        //        //}

        //        //foreach (FileInfo file in di.GetFiles())
        //        //{
        //        //    file.Delete();
        //        //}

        //    });

        //    await task;
        //    return false;

        //}
    }
}
