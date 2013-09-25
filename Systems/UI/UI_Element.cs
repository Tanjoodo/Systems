#region File description
/* File name: UI_Element.cs
 * 
 * Location: ./Systems/UI.cs
 * 
 * Author: Mohammed "Tanjoodo" Arabiat
 */
#endregion
#region Using statements
using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Systems;
using Systems.Input;
#endregion
namespace Systems.UI
{
    /// <summary>
    /// Parent class of all UI elements, a UI element can contain other UI elements in it.
    /// This class can be inherited.
    /// </summary>
    public class UI_Element
    {
        string label;
        int index;
 
        List<UI_Element> children = new List<UI_Element>();
        /// <summary>
        /// Initializes a new instance of UI_Element. 
        /// </summary>
        /// <param name="Label">Identifying label, for example "save_game_prompt"... etc. Ultimately, each label should be different.</param>
        /// <param name="Index">Priority index. This is used to set priority to updating/rendering</param>
        public UI_Element(string Label, int Index)
        {
            this.label = Label;
            this.index = Index;
        }
        
        /// <summary>
        /// Gets the label of the specified UI element.
        /// </summary>
        /// <returns>Returns a string containing the label of the specified UI element.</returns>
        public string GetLabel()
        {
            return label;
        }
        /// <summary>
        /// Gets the children of the specified UI element.
        /// </summary>
        /// <returns>Returns a list of all the UI_Elements that are children to the specified UI element.</returns>
        public List<UI_Element> GetChildren()
        {
            return children;
        }
        /// <summary>
        /// Adds a child element to the UI
        /// </summary>
        /// <param name="ChildElement">Child UI element to be added</param>
        public void AddChild(UI_Element ChildElement)
        {
            children.Add(ChildElement);
        }

        /// <summary>
        /// Load logic for class
        /// <param name="Content">ContentManager to manage content loading</param>
        /// </summary>
        public void Load(ContentManager Content)
        {
        }
        

        /// <summary>
        /// Updates the UI_Element and all child UI_Elements.
        /// </summary>
        /// <param name="CurrentMouseState">Up to date MouseState object</param>
        /// <param name="CurrentKeyboardState">Up to date KeyboardState object</param>
        public void Update(MouseState CurrentMouseState, KeyboardState CurrentKeyboardState)
        {
            //TODO: Events system
            foreach (var child in children)
            {
                child.Update(CurrentMouseState, CurrentKeyboardState);
            }
        }

        /// <summary>
        /// Gets the priority index for the specified UI element
        /// </summary>
        /// <returns>Returns an int containing priority index</returns>
        public int GetIndex()
        {
            return index;
        }
        
        /// <summary>
        /// Draws element.
        /// </summary>
        public void Draw(SpriteBatch CurrentSpriteBatch)
        {
            foreach (var child in children)
            {
                child.Draw(CurrentSpriteBatch);
            }
        }
        /// <summary>
        /// Inheriting classes can specify what to do in the event of being clicked.
        /// </summary>
        void OnClick()
        {
        }
        
        /// <summary>
        /// Inheriting classes can specify what to do in the event of mouse over.
        /// </summary>
        void OnMouseOver()
        {

        }

        /// <summary>
        /// Inheriting classes can specify what to do when the mouse is no longer over the object.
        /// </summary>
        void AfterMouseOver()
        {
        }

    }
}
