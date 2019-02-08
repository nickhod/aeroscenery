using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GeoConvertWrapper
{
    class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        private static GeoConvertManager geoConvertManager;

        private enum EventType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }


        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                geoConvertManager = new GeoConvertManager();
                geoConvertManager.WrapGeoConvert(args[0], args[1]);
            }
            else
            {
                Console.WriteLine("Usage: {path to GeoConvert} {tmc file}");
                Console.ReadKey();
            }
        }

        static bool ConsoleEventCallback(EventType eventType)
        {
            switch (eventType)
            {
                case EventType.CTRL_C_EVENT:
                case EventType.CTRL_CLOSE_EVENT:
                default:

                    // Kill off any open GeoConverts if the user exits before
                    // GeoConvert is done
                    if (geoConvertManager.CurrentGeoConvertProcess != null)
                    {
                        geoConvertManager.CurrentGeoConvertProcess.Kill();
                    }

                    return false;
            }

        }
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected

        private delegate bool ConsoleEventDelegate(EventType eventType);
    }
}
