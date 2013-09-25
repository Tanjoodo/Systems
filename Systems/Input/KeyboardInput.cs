using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
namespace Systems.Input
{
    /// <summary>
    /// Class that handles keybaord input
    /// </summary>
   public class KeyboardInput
    {
        List<Keys> key_buffer, listen_keys;
        KeyboardState previous_keyboard_state = new KeyboardState();
        
        /// <summary>
        /// Initializes a new instance of KeyboardInput class
        /// </summary>
        public KeyboardInput()
        {
            key_buffer = new List<Keys>();
            listen_keys = new List<Keys>();
        }
        
        /// <summary>
        /// Set what keys to return if pressed. If no keys are set, all keys will be listened to.
        /// </summary>
        /// <param name="ListenKeys">A list of Xna.Framework.Input.Keys that holds a group of non-duplicated keys to return if pressed</param>
        public void SetListenKeys(Keys[] ListenKeys) //Set a custom list of keys to choose from, if no custom list is given, liten to all keys
        {
            if (ListenKeys.Count() == ListenKeys.Distinct<Keys>().Count())
            {
                listen_keys = ListenKeys.ToList<Keys>();
            }
            else
            {
                throw new System.ArgumentException("Argument has duplicates", "ListenKeys"); //Duplicates will cause problems to happen with the key buffer.
            }
        }
        
        /// <summary>
        /// Gets list of pressed keys, if a specific list of listen keys were specified, only keys on that list will be returned if they 
        /// were pressed.
        /// </summary>
        /// <param name="CurrentKeyboardState">A current Xna.Framework.Input.KeyboardState object.</param>
        /// <returns>Returns a list of pressed Keys.</returns>
        public List<Keys> GetInput(KeyboardState CurrentKeyboardState) //Main purpose is to be used by the update loop/method.
        {
            
            Keys[] array_of_pressed_keys = CurrentKeyboardState.GetPressedKeys();
            if (listen_keys.Count > 0)
            {
                foreach (Keys key in listen_keys)
                {
                    if (CurrentKeyboardState.IsKeyDown(key) && !previous_keyboard_state.IsKeyDown(key))
                    { 
                        key_buffer.Add(key);
                    }                   

                }
                
                previous_keyboard_state = CurrentKeyboardState;
                return key_buffer;
            }
            else
            {
                previous_keyboard_state = CurrentKeyboardState;
                return array_of_pressed_keys.ToList<Keys>();
            }

        }
        /// <summary>
        /// Removes the first item from the input buffer, after it has been processed.
        /// </summary>
        public void RemoveFirstFromBuffer()
        {
            key_buffer.RemoveAt(0); //Remove the first key in the list. To be used once the keystroke has been processed.
        }
   }
}
