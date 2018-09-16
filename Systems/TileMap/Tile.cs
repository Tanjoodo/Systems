using Microsoft.Xna.Framework;

namespace Systems.TileMap
{
    struct Tile
    {
        public string Name { get; set; }
        public Vector2 Location { get; set; }
        public int Z { get; set; }

        public Tile(string name, Point location, int z)
        {
            Name = name;
            Location = location.ToVector2();
            Z = z;
        }
    }
}