using AeroScenery.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AeroScenery.Data
{
    public class ImageTileService
    {
        public async Task SaveImageTilesAsync(List<ImageTile> imageTiles, string directory)
        {
            await Task.Run(() =>
            {
                XmlSerializer xs = new XmlSerializer(typeof(ImageTile));

                foreach (ImageTile imageTile in imageTiles)
                {
                    using (TextWriter tw = new StreamWriter(directory + imageTile.FileName + ".aero"))
                    {
                        xs.Serialize(tw, imageTile);
                    }
                }

                xs = null;
            });      
        }

        public async Task<List<ImageTile>> LoadImageTilesAsync(string directory)
        {
            return await Task.Run(() =>
            {
                List<ImageTile> imageTiles = new List<ImageTile>();
                XmlSerializer serializer = new XmlSerializer(typeof(ImageTile));

                var aeroFiles = Directory.EnumerateFiles(directory, "*.aero");

                foreach (string filePath in aeroFiles)
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        var imageTile = (ImageTile)serializer.Deserialize(reader);
                        reader.Close();
                        imageTiles.Add(imageTile);
                    }

                }

                serializer = null;
                return imageTiles;
            });


        }
    }
}
