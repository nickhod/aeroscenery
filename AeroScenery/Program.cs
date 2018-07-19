﻿using AeroScenery.Controls;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            log4net.Config.XmlConfigurator.Configure();

#if RELEASE
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
#endif

            AeroSceneryManager.Instance.Initialize();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException((Exception)e.ExceptionObject);
        }

        static void HandleException(Exception ex)
        {
            if (ex != null)
            {
                var log = LogManager.GetLogger("AeroScenery");

                StringBuilder sb = new StringBuilder();

                log.Error(ex.Message);
                log.Error(ex.StackTrace);

                sb.AppendLine("An unhandled error occurred");
                sb.AppendLine(ex.Message);

                if (ex.InnerException != null)
                {
                    log.Error(ex.InnerException.Message);
                    log.Error(ex.InnerException.StackTrace);

                    sb.AppendLine(ex.InnerException.Message);
                }

                var messageBox = new CustomMessageBox(sb.ToString(),
                    "AeroScenery",
                    MessageBoxIcon.Error);

                messageBox.ShowDialog();
            }

        }
    }
}
