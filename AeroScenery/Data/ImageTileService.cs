using AeroScenery.Common;
using System;
using System.Collections.Generic;
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

                foreach (string filePath in Directory.EnumerateFiles(directory, "*.aero"))
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
