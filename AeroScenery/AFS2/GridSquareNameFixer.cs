using AeroScenery.Common;
using AeroScenery.Controls;
using AeroScenery.Data;
using AeroScenery.Data.Models;
using AeroScenery.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.AFS2
{
    public class GridSquareNameFixer
    {
        private Settings settings;
        private IDataRepository dataRepository;
        private SettingsService settingsService;
        private int failedCount;

        public GridSquareNameFixer(Settings settings, IDataRepository dataRepository, SettingsService settingsService)
        {
            this.settings = settings;
            this.dataRepository = dataRepository;
            this.settingsService = settingsService;
        }

        public void FixGridSquareNames()
        {
            if (!settings.GridSquareNamesFixed.Value)
            {
                var gridSquares = dataRepository.GetAllGridSquares();

                bool hasUnfixedGridSqures = false;

                // Do we have any unfixed grid squares?
                if (gridSquares != null && gridSquares.Count > 0)
                {
                    foreach (GridSquare gridSquare in gridSquares)
                    {
                        if (gridSquare.Fixed == 0)
                        {
                            hasUnfixedGridSqures = true;
                            break;
                        }
                    }
                }

                // If there are no grid squres to fix, we don't need to do anything
                if (!hasUnfixedGridSqures)
                {
                    settings.GridSquareNamesFixed = true;
                    settingsService.SaveSettings(settings);

                }
                else
                {

                    var pleaseWaitBox = new CustomMessageBox("Please Wait.\nThis may take a while if you have lots of existing AeroScenery downloads.",
                        "AeroScenery",
                        MessageBoxIcon.Information);

                    pleaseWaitBox.Show();


                    var sb = new StringBuilder();
                    sb.AppendLine("*** PLEASE READ ***");
                    sb.AppendLine("Previous versions of AeroScenery got the hex value of Aerofly tiles wrong.");
                    sb.AppendLine("");
                    sb.AppendLine("Entries in the AeroScenery database will be renamed.");
                    sb.AppendLine("");
                    sb.AppendLine(string.Format("Relevant directories will be renamed under the folder {0}", settings.WorkingDirectory));
                    sb.AppendLine("Nothing will be changed in your Aerofly install or Aerofly user folders.");
                    sb.AppendLine("");
                    sb.AppendLine("Please make sure all files and Explorer windows are closed relating to the directory");
                    sb.AppendLine(string.Format("{0}", settings.WorkingDirectory));

                    var messageBox = new CustomMessageBox(sb.ToString(),
                        "AeroScenery",
                        MessageBoxIcon.Warning);

                    messageBox.SetButtons(
                        new string[] { "OK", "Cancel" },
                        new DialogResult[] { DialogResult.OK, DialogResult.Cancel });


                    var result = messageBox.ShowDialog();

                    switch (result)
                    {
                        case DialogResult.OK:

                            failedCount = 0;

                            this.DoFirstDirectoryRename(gridSquares);

                            if (failedCount == 0)
                            {
                                var sb2 = new StringBuilder();
                                sb2.AppendLine(string.Format("All directories with incorrect names under {0} have been prefixed with an underscore.", settings.WorkingDirectory));
                                sb2.AppendLine("Click OK to continue with the rename process.");

                                var messageBox2 = new CustomMessageBox(sb2.ToString(),
                                    "AeroScenery",
                                    MessageBoxIcon.Information);

                                var result2 = messageBox2.ShowDialog();

                                if (result2 == DialogResult.OK)
                                {
                                    Thread.Sleep(1000);

                                    this.DoSecondDirectoryRename(gridSquares);

                                    if (failedCount == 0)
                                    {
                                        settings.GridSquareNamesFixed = true;
                                        settingsService.SaveSettings(settings);
                                    }

                                }
                            }


                            pleaseWaitBox.Close();

                            break;
                        case DialogResult.Cancel:

                            Environment.Exit(0);

                            break;
                    }
                }


            }
        }

        private void HandleDirectoryMoveError(Exception ex, string sourceDirectory, string destinationDirectory)
        {
            var errorSb = new StringBuilder();
            errorSb.AppendLine("There was an error renaming the directory");
            errorSb.AppendLine(sourceDirectory);
            errorSb.AppendLine("to");
            errorSb.AppendLine( destinationDirectory);
            errorSb.AppendLine("");
            errorSb.AppendLine("Please close any files in that directory and restart AeroScenery.");
            errorSb.AppendLine("Also check that the destination directory doesn't exist.");
            errorSb.AppendLine("");
            errorSb.AppendLine("If the error presists, try restarting your computer, then restarting AeroScenery.");
            errorSb.AppendLine("If that fails, try the rename manually with Windows Explorer.");
            errorSb.AppendLine("");
            errorSb.AppendLine(string.Format("Error: {0}", ex.Message));

            var errorMessageBox = new CustomMessageBox(errorSb.ToString(),
                "AeroScenery",
                MessageBoxIcon.Error);

            errorMessageBox.ShowDialog();
        }

        private void DoFirstDirectoryRename(List<GridSquare> gridSquares)
        {
            // First we need to move all directories 'out of the way'
            foreach (GridSquare gridSquare in gridSquares)
            {

                if (gridSquare.Fixed == 0)
                {
                    var gridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + gridSquare.Name;
                    var renamedGridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + "_" + gridSquare.Name;

                    try
                    {
                        if (Directory.Exists(gridSquareDirectory))
                        {
                            if (gridSquareDirectory != renamedGridSquareDirectory)
                            {
                                // VB version seems to be more reliable than Directory.Move
                                Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(gridSquareDirectory, renamedGridSquareDirectory);

                                Thread.Sleep(500);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        failedCount++;

                        this.HandleDirectoryMoveError(ex, gridSquareDirectory, renamedGridSquareDirectory);
                    }

                }

            }


        }

        private void DoSecondDirectoryRename(List<GridSquare> gridSquares)
        {
            var afsGrid = new AFS2Grid();

            foreach (GridSquare gridSquare in gridSquares)
            {
                if (gridSquare.Fixed == 0)
                {
                    var afs2GridSquare = AFS2GridSquare.FromGridSquare(gridSquare);
                    var squareCenter = afs2GridSquare.GetCenter();

                    var newGridSquare = afsGrid.GetGridSquareAtLatLon(squareCenter.Lat, squareCenter.Lng, gridSquare.Level);

                    var gridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + "_" + gridSquare.Name;
                    var renamedGridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + newGridSquare.Name;

                    try
                    {
                        if (Directory.Exists(gridSquareDirectory))
                        {
                            if (gridSquareDirectory != renamedGridSquareDirectory)
                            {
                                // VB version seems to be more reliable than Directory.Move
                                Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(gridSquareDirectory, renamedGridSquareDirectory);

                                Thread.Sleep(500);
                            }
                        }

                        // If there is already a grid square with the new name, we need to move it out of the way
                        // by prevising it with an underscore
                        var existingGridSquareWithNewName = dataRepository.FindGridSquare(newGridSquare.Name);

                        if (existingGridSquareWithNewName != null)
                        {
                            existingGridSquareWithNewName.Name = "_" + existingGridSquareWithNewName.Name;
                            dataRepository.UpdateGridSquare(existingGridSquareWithNewName);
                        }

                        // Only update the database once we've renamed the dir
                        gridSquare.Name = newGridSquare.Name;
                        gridSquare.Fixed = 1;

                        dataRepository.UpdateGridSquare(gridSquare);
                    }
                    catch (Exception ex)
                    {
                        failedCount++;

                        this.HandleDirectoryMoveError(ex, gridSquareDirectory, renamedGridSquareDirectory);
                    }
                }
            }
        }

    }
}
