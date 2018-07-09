using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.Data
{
    public class VersionService
    {
        public void CheckForNewerVersions()
        {
            Task.Run(() =>
            {

                try
                {
                    using (WebClient client = new WebClient())
                    {
                        string versionInfo = client.DownloadString("https://raw.githubusercontent.com/nickhod/aeroscenery/master/version.json");

                        dynamic dyn = JsonConvert.DeserializeObject(versionInfo);
                        int incrementalVersion = dyn.incrementalVersion;
                        string semanticVersion = dyn.semanicVersion;


                        if (incrementalVersion > AeroSceneryManager.Instance.IncrementalVersion)
                        {
                            DialogResult result = MessageBox.Show("A newer version of AeroScenery is available.",
                                "AeroScenery",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
                catch
                {

                }

            });

        }
    }
}
