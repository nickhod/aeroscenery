using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.FileManagement
{
    public class SceneryInstaller
    {
        public async Task<bool> DuplicateTTCFilesFoundAsync(string sourceDirectory)
        {
            var sdf = "sdf";

            var task = Task.Run(() =>
            {

                sdf = "sdf";

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

            await task;
            return false;

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
    }
}
