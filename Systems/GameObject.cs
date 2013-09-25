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
        protected Texture2D texture;
        protected Vector2 position;
        /// <summary>
        /// Loads texture for object
        /// </summary>
        /// <param name="UsedContentManager">The used content manager for this object.</param>
        /// <param name="SpriteName">A string containing the name of the resource.</param>
        public void Load(ContentManager UsedContentManager, string SpriteName)
        {
            texture = UsedContentManager.Load<Texture2D>(SpriteName);

        }

        /// <summary>
        /// Initialization logic.
        /// </summary>
        /// <param name="Position">Starting position of the object.</param>
        public void initialize(Vector2 Position)
        {
            Position = position;
        }
        public void Update(){} //Filler

        public void Draw(SpriteBatch SpriteBatch){} //Filler as well; as there might be different requirements for different objects.
         
    }
}
