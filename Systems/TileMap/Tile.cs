using Microsoft.Xna.Framework;

namespace Systems.TileMap
{
    struct Tile
    {
        public string  Name;
        public Vector2 Location;
        public int     Z;

        public Tile(string Name, float X, float Y, int Z)
        {

            this.Name = Name;
            this.Location = new Vector2(X, Y);
            this.Z = Z;
            
        }
    }
}