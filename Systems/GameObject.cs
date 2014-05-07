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

	/// <summary>
	/// Sets the object's position
	/// </summary>
	/// <param name="Position">Position to be set to</param>
        public void SetPosition(Vector2 Position)
        {
            this.position = Position;
        }

	/// <summary>
	/// Gets the position of the object
	/// </summary>
	/// <returns></returns>
        public Vector2 GetPosition()
        {
            return this.position;
        }

	/// <summary>
	/// Sets the object's texture
	/// </summary>
	/// <param name="Texture">Texture to be set</param>
        public void SetTexture(Texture2D Texture)
        {
            this.texture = Texture;
        }

	/// <summary>
	/// Gets the object's texture
	/// </summary>
	/// <returns>Returns object's texture.</returns>
        public Texture2D GetTexture()
        {
            return this.texture;
        }

	/// <summary>
	/// Gets object's axis aligned bounding box
	/// </summary>
	/// <returns>Returns a rectangle.</returns>
        public Rectangle GetAABB()
        {
            return new Rectangle((int)this.position.X, (int)this.position.Y,
                this.texture.Width, this.texture.Height);
        }
         
    }
}
