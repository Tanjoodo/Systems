using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Systems.TileMap
{
    public class TileMap
    {
        Vector2 origin;

        Exception parse_exception = new Exception("Parse error, wrong syntax");

        Dictionary<string, Texture2D> texture_dictionary = new Dictionary<string,Texture2D>();
        List<Tile> tile_map = new List<Tile>();

        int largest_z_index = 0;
        public TileMap(Vector2 Origin, String MapFileLocation, ContentManager ContentManager, string RootTileDirectory = "")
        {
            

            this.origin = Origin;

            // Declare delimiter arrays to split lines and parse them
            char[] line_delimiters = { '\n' };
            char[] item_delimiters = { ' ' };

            // Read map file at location given
            System.IO.StreamReader stream_reader = new System.IO.StreamReader(MapFileLocation);
            string MapFile = stream_reader.ReadToEnd();
            stream_reader.Close();
            

            // Split file into an array, each element of the array is one line
            string[] lines = MapFile.Split(line_delimiters);

            // Remove comments from the lines array. Also, clean the lines from extra whitespace and other things
            var lines_no_comments =
                from line in lines
                where !line.StartsWith("#") && line.Replace("\r", "") != ""
                select line.Replace("\r", "").Trim().ToLower();
            // Group all the tile declarations together
            var tile_declarations =
                from line in lines_no_comments
                where line.ToLower().StartsWith("tile")
                select line.ToLower();
            // Group all the textured rectangle declarations together
            var rect_declarations =
                from line in lines_no_comments
                where line.ToLower().StartsWith("rect")
                select line.ToLower();
            // Group all tile placement instructions together
            var placements = 
                from line in lines_no_comments
                where line.ToLower().StartsWith("place")
                select line.ToLower();

            // Populate the texture_dictionary
            foreach (string line in tile_declarations)
            {
                // Split the line
                string[] items = line.Split(item_delimiters);

                // Example line: tile <tile_name> <texture_file_name>
                // Indexes:        0       1           2
                // Each line should only contain three items.
                if (items.Length != 3) throw parse_exception;

                // This reserves the first declaration if multiple declarations of the same tile were made.
                if (!texture_dictionary.ContainsKey(items[1]))
                {
                    texture_dictionary.Add(items[1], 
                        ContentManager.Load<Texture2D>(RootTileDirectory + items[2]));
                }
                
            }

            //TODO Do rectangle declarations

            // Populate the tile_map
            foreach (string line in placements)
            {
                // Split the line
                string[] items = line.Split(item_delimiters);

                // Example line: place <tile> <x> <y> <z>
                // Indexes:        0      1    2   3   4
                // Each line should only contain five items
                if (items.Length != 5) throw parse_exception;

                
                tile_map.Add(new Tile(items[1], (float)Convert.ToInt16(items[2]), (float)Convert.ToInt16(items[3]),
                    Convert.ToInt16(items[4])));

                if (Convert.ToInt16(items[4]) > largest_z_index) largest_z_index = Convert.ToInt16(items[4]);
            }

            // Turn the sorted list into an array (changes by this point are at best very limited)


        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            //TODO better way of zindexing
            for (int i = 0; i <= largest_z_index; i++)
            {
                foreach (Tile tile in tile_map)
                {
                    if (tile.Z == i)
                    {
                        Texture2D texture;
                        texture_dictionary.TryGetValue(tile.Name, out texture);

                        SpriteBatch.Draw(texture, tile.Location, Color.White);
                    }
                }
            }


            //TODO do rectangle drawing
        }
    }

}
