using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Systems.TileMap
{
    public class TileMap
    {
        Vector2 _origin;

        Exception _parseException = new Exception("Parse error, wrong syntax");

        Dictionary<string, Texture2D> _textureDictionary = new Dictionary<string, Texture2D>();
        List<Tile> _tileMap = new List<Tile>();

        int _largestZIndex = 0;
        public TileMap(Vector2 origin, string mapFileLocation, ContentManager contentManager, string rootTileDirectory = "")
        {
            _origin = origin;

            if (rootTileDirectory != "" && !rootTileDirectory.EndsWith("\\"))
            {
                rootTileDirectory += @"\";
            }

            // Declare delimiter arrays to split lines and parse them
            char[] lineDelimiters = { '\r', '\n' };
            char[] itemDelimiters = { ' ' };

            string mapFile;

            // Read map file at location given
            using (var stream_reader = new StreamReader(mapFileLocation))
            {
                mapFile = stream_reader.ReadToEnd();
            }


            // Split file into an array, each element of the array is one line
            string[] lines = mapFile.Split(lineDelimiters);

            // Remove comments from the lines array. Also, clean the lines from extra whitespace and other things
            var linesNoComments =
                from line in lines
                where !line.StartsWith("#")
                select line.Trim().ToLower();
            // Group all the tile declarations together
            var tileDeclarations =
                from line in linesNoComments
                where line.ToLower().StartsWith("tile")
                select line.ToLower();
            // Group all the textured rectangle declarations together
            var rectDeclarations =
                from line in linesNoComments
                where line.ToLower().StartsWith("rect")
                select line.ToLower();
            // Group all tile placement instructions together
            var placements =
                from line in linesNoComments
                where line.ToLower().StartsWith("place")
                select line.ToLower();

            // Populate the _textureDictionary
            foreach (string line in tileDeclarations)
            {
                // Split the line
                string[] items = line.Split(itemDelimiters);

                // Example line: tile <tile_name> <texture_file_name>
                // Indexes:        0       1           2
                // Each line should only contain three items.
                if (items.Length != 3) throw _parseException;

                // This reserves the first declaration if multiple declarations of the same tile were made.
                if (!_textureDictionary.ContainsKey(items[1]))
                {
                    _textureDictionary.Add(items[1],
                        contentManager.Load<Texture2D>(rootTileDirectory + items[2]));
                }
            }

            //TODO Do rectangle declarations

            // Populate the tile_map
            foreach (string line in placements)
            {
                // Split the line
                string[] items = line.Split(itemDelimiters);

                // Example line: place <tile> <x> <y> <z>
                // Indexes:        0      1    2   3   4
                // Each line should only contain five items
                if (items.Length != 5) throw _parseException;


                if (Int32.TryParse(items[2], out int x) && Int32.TryParse(items[3], out int y) && Int32.TryParse(items[4], out int zIndex))
                {
                    _tileMap.Add(new Tile(items[1], new Point(x, y), zIndex));

                    if (zIndex > _largestZIndex)
                    {
                        _largestZIndex = zIndex;
                    }
                }
                else
                {
                    throw _parseException;
                }
            }

            // Turn the sorted list into an array (changes by this point are at best very limited)
        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            //TODO better way of zindexing
            for (int i = 0; i <= _largestZIndex; i++)
            {
                foreach (Tile tile in _tileMap)
                {
                    if (tile.Z == i)
                    {
                        _textureDictionary.TryGetValue(tile.Name, out Texture2D texture);

                        SpriteBatch.Draw(texture, tile.Location, Color.White);
                    }
                }
            }

            //TODO do rectangle drawing
        }
    }
}
