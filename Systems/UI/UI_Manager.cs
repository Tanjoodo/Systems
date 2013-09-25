using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Systems.UI
{
    public class UI_Manager
    {
        SortedList<UI_Element, int> children = new SortedList<UI_Element, int>();

        /// <summary>
        /// Initializes a new instance of UI_Manager
        /// </summary>
        /// <param name="Children">Array of child UI elements to be managed</param>
        public UI_Manager(UI_Element[] Children)
        {
            //Sort list by index first, and by time of insertion second.
            foreach (UI_Element child in Children)
            {
                children.Add(child, child.GetIndex());
            }
        }

        public void Load(ContentManager Content)
        {
            foreach (UI_Element child in children.Keys)
            {
                child.Load(Content);
            }
        }

        
        public void Update(MouseState CurrentMouseState, KeyboardState CurrentKeyboardState)
        {
            foreach (UI_Element child in children.Keys)
            {
                child.Update(CurrentMouseState, CurrentKeyboardState);
            }
        }

        public void Draw(SpriteBatch CurrentSpriteBatch)
        {
            foreach (var child in children.Keys)
            {
                child.Draw(CurrentSpriteBatch);
            }
        }

    }
}
