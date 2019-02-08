using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Timers;
using System.Threading;
using System.IO;

namespace GeoConvertWrapper
{
    public class GeoConvertManager
    {
        public Process CurrentGeoConvertProcess { get; set; }
        private System.Timers.Timer screenshotTimer;
        private ScreenCapture screenCapture;

        private readonly int successfulDetectionThreshold = 3;
        private int successfulDetectionCount = 0;

        public GeoConvertManager()
        {
            screenCapture = new ScreenCapture();
        }

        public void WrapGeoConvert(string geoconvertPath, string tmcFilename)
        {
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("-                            GeoConvert Wrapper                                -");
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Starting GeoConvert");

            successfulDetectionCount = 0;

            string geoconvertFilename = String.Format("{0}\\aerofly_fs_2_geoconvert.exe", geoconvertPath);

            bool allFilesExist = true;

            if (!File.Exists(geoconvertFilename))
            {
                allFilesExist = false;
                Console.WriteLine(String.Format("GeoConvert not found at {0}", geoconvertFilename));
                Console.ReadKey();
            }

            if (!File.Exists(tmcFilename))
            {
                allFilesExist = false;
                Console.WriteLine(String.Format("TMC file not found at {0}", tmcFilename));
                Console.ReadKey();
            }


            if (allFilesExist)
            {
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

                this.CurrentGeoConvertProcess = proc;

                this.StartScreenshotMonitoring();
                proc.WaitForExit();
            }


        }

        private void StartScreenshotMonitoring()
        {
            Console.WriteLine("Starting GeoConvert monitoring");

            screenshotTimer = new System.Timers.Timer();
            screenshotTimer.Elapsed += new ElapsedEventHandler(OnScreenshotTimerElasped);
            screenshotTimer.Interval = 1 * 1000;
            screenshotTimer.Start();
        }

        private void OnScreenshotTimerElasped(object source, ElapsedEventArgs e)
        {
            screenshotTimer.Stop();
            Console.WriteLine("Waiting for completed text");

            WindowHelper.BringProcessToFront(this.CurrentGeoConvertProcess);
            // Wait for the app to restore to the foreground if it is backgrounded
            Thread.Sleep(1500);

            var bitmap = screenCapture.CaptureProcessScreenshot(this.CurrentGeoConvertProcess);

            var geoConvertComplete = DetectGeoConvertComplete(bitmap);

            if (geoConvertComplete)
            {
                successfulDetectionCount++;

                // We need several successful detections before we are satisfied that
                // GeoConvert is complete.
                // The screenshot could have caught some desktop background if GeoConvert
                // was minimised leading to a false positive.
                if (successfulDetectionCount >= this.successfulDetectionThreshold)
                {
                    Console.WriteLine("GeoConvert Complete");
                    this.CurrentGeoConvertProcess.Kill();
                }
                else
                {
                    screenshotTimer.Start();
                }

            }
            else
            {
                screenshotTimer.Start();
            }

        }

        private bool DetectGeoConvertComplete(Bitmap bmp)
        {
            bool geoConvertComplete = false;

            // Roughly crop the bitmap so that the window's title and border
            // don't interfere with detection
            var cropRect = new Rectangle(30, 30, bmp.Width-60, bmp.Height-60);
            var croppedBitmap = bmp.Clone(cropRect, bmp.PixelFormat);


            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, croppedBitmap.Width, croppedBitmap.Height);
            BitmapData bmpData = croppedBitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * croppedBitmap.Height;
            byte[] rgbValues = new byte[bytes];
            byte[] r = new byte[bytes / 3];
            byte[] g = new byte[bytes / 3];
            byte[] b = new byte[bytes / 3];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            int count = 0;
            int stride = bmpData.Stride;

            for (int column = 0; column < bmpData.Height; column++)
            {
                if (!geoConvertComplete)
                {
                    for (int row = 0; row < bmpData.Width; row++)
                    {
                        b[count] = (byte)(rgbValues[(column * stride) + (row * 3)]);
                        int blue = (int)b[count];


                        g[count] = (byte)(rgbValues[(column * stride) + (row * 3) + 1]);
                        int green = (int)g[count];


                        r[count] = (byte)(rgbValues[(column * stride) + (row * 3) + 2]);
                        int red = (int)r[count];

                        count++;

                        // Search for the green completed text in the window
                        if (green > 230 && green < 250 && 
                            red > 0 && red < 25 &&
                            blue > 0 && blue < 25)
                        {
                            geoConvertComplete = true;
                            break;
                        }

                    }
                }

            }

            return geoConvertComplete;
        }

    }
}
