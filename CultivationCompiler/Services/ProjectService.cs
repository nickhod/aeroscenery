using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CultivationCompiler.Models;

namespace CultivationCompiler.Services
{
    public class ProjectService
    {
        public async Task<Project> LoadProject(string filePath)
        {
            return await Task.Run(() =>
            {
                Project project = new Project();
                XmlSerializer serializer = new XmlSerializer(typeof(Project));

                using (StreamReader reader = new StreamReader(filePath))
                {
                    project = (Project)serializer.Deserialize(reader);
                    reader.Close();
                }

                serializer = null;
                return project;
            });


        }

    }
}
