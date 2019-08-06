using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AeroScenery.FileManagement
{
    public class FileManager
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DeleteFile(string lpFileName);

        public async Task DeleteGridSquareFilesAsync(string gridSquareDirectory, bool mapTiles, bool stitchedImages, bool rawFiles, bool ttcFiles)
        {
            var task = Task.Run(() =>
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(gridSquareDirectory);

                // Loop through the outer directories for orthophoto sources, "g", "b", "usgs"
                foreach (DirectoryInfo directoryInfo in di.GetDirectories())
                {
                    System.IO.DirectoryInfo innerDirectoryInfo = new DirectoryInfo(directoryInfo.FullName);

                    foreach (DirectoryInfo dir in innerDirectoryInfo.GetDirectories())
                    {
                        var deleteFilesInDirectory = false;

                        if (dir.Name.Contains("-raw") && rawFiles)
                        {
                            deleteFilesInDirectory = true;
                        }

                        if (dir.Name.Contains("-ttc") && ttcFiles)
                        {
                            deleteFilesInDirectory = true;
                        }

                        if (dir.Name.Contains("-stitched") && stitchedImages)
                        {
                            deleteFilesInDirectory = true;
                        }

                        // The map tiles directory is just the zoom level. No other characters
                        if (dir.Name.All(char.IsDigit) && mapTiles)
                        {
                            deleteFilesInDirectory = true;
                        }

                        if (deleteFilesInDirectory)
                        {
                            IEnumerable<string> files = Directory.EnumerateFiles(dir.FullName, "*", SearchOption.AllDirectories);

                            foreach (string file in files)
                            {
                                try
                                {
                                    DeleteFile(file);
                                }
                                catch (IOException)
                                {
                                    Thread.Sleep(100);
                                    DeleteFile(file);
                                }
                            }

                            // Lastly, delete the empty directory
                            try
                            {
                                if (Directory.Exists(dir.FullName))
                                {
                                    Directory.Delete(dir.FullName);
                                }
                            }
                            catch (IOException)
                            {
                                Thread.Sleep(100);
                                if (Directory.Exists(dir.FullName))
                                {
                                    Directory.Delete(dir.FullName);
                                }
                            }

                        }
                    }






                }

            });

            await task;
        }
    }
}
