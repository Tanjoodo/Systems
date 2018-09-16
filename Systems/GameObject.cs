using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Systems
{
    /// <summary>
    /// Basic game object, with a texture and a position.
    /// </summary>
    public class GameObject
    {
        protected Texture2D _texture;
        protected Vector2 _position;

        /// <summary>
        /// Gets or sets the object's position.
        /// </summary>
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        /// <summary>
        /// Gets or sets the object's texture.
        /// </summary>
        public Texture2D Texture
        {
            get => _texture;
            set => _texture = value;
        }

        /// <summary>
        /// Gets object's axis aligned bounding box
        /// </summary>
        public Rectangle Aabb => new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        

        /// <summary>
        /// Loads texture for object
        /// </summary>
        /// <param name="contentManager">The used content manager for this object.</param>
        /// <param name="spriteName">A string containing the name of the resource.</param>
        public void Load(ContentManager contentManager, string spriteName)
        {
            _texture = contentManager.Load<Texture2D>(spriteName);
        }

        /// <summary>
        /// Initialization logic.
        /// </summary>
        /// <param name="position">Starting position of the object.</param>
        public void Initialize(Vector2 position)
        {
            position = _position;
        }
    }
}
