using AeroScenery.SceneryEditor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AeroScenery.SceneryEditor.Project
{
    public class ProjectService
    {
        public async Task SaveProject(SceneryEditorProject project, string filePath)
        {
            await Task.Run(() =>
            {
                XmlSerializer xs = new XmlSerializer(typeof(SceneryEditorProject));

                using (TextWriter tw = new StreamWriter(filePath))
                {
                    xs.Serialize(tw, project);
                }

                xs = null;
            });
        }

        public async Task<SceneryEditorProject> LoadProject(string filePath)
        {
            return await Task.Run(() =>
            {
                SceneryEditorProject project = new SceneryEditorProject();
                XmlSerializer serializer = new XmlSerializer(typeof(SceneryEditorProject));

                using (StreamReader reader = new StreamReader(filePath))
                {
                    project = (SceneryEditorProject)serializer.Deserialize(reader);
                    reader.Close();
                }

                serializer = null;
                return project;
            });


        }
    }
}
