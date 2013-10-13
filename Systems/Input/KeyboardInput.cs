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
    List<Keys> key_buffer;
    List<Keys> held_key_buffer = new List<Keys>();
	Keys[] array_of_pressed_keys;
    List<Keystroke> listen_keys;

	KeyboardState previous_keyboard_state = new KeyboardState();

    float hold_threshold;
		
	/// <summary>
	/// Initializes a new instance of KeyboardInput class
	/// </summary>
	public KeyboardInput()
	{
		key_buffer = new List<Keys>();
		listen_keys = new List<Keystroke>();
	}
		
	/// <summary>
	/// Set what keys to return if pressed. If no keys are set, all keys will be listened to.
	/// </summary>
	/// <param name="ListenKeys">A list of Xna.Framework.Input.Keys that holds a group of non-duplicated keys to return if pressed</param>
	public void SetListenKeys(Keys[] ListenKeys) //Set a custom list of keys to choose from, if no custom list is given, listen to all keys
	{
			
		if (ListenKeys.Count() == ListenKeys.Distinct<Keys>().Count()) //Checks for duplicates
		{
			foreach (Keys Key in ListenKeys)
			{
					listen_keys.Add(new Keystroke(Key, 0));
			}
		}
		else
		{
			throw new System.ArgumentException("Argument has duplicates", "ListenKeys"); //Duplicates will cause problems to happen with the key buffer.
		}

	}

    /// <summary>
    /// Sets the time threshold where a pressed button becomes a held down button
    /// </summary>
    /// <param name="Threshold">Threshold in milliseconds</param>
    public void SetHoldThreshold(float Threshold)
    {
        this.hold_threshold = Threshold;
    }
		
	/// <summary>
	/// Gets list of pressed keys, if a specific list of listen keys were specified, only keys on that list will be returned if they 
	/// were pressed.
	/// </summary>
	/// <param name="CurrentKeyboardState">A current Xna.Framework.Input.KeyboardState object.</param>
	public void UpdateInput(KeyboardState CurrentKeyboardState, float MillisecondsSinceLastFrame) //Main purpose is to be used by the update loop/method.
	{
		array_of_pressed_keys = CurrentKeyboardState.GetPressedKeys();
        List<Keys> held_key_buffer_ = new List<Keys>();
        
        for (int i = 0; i < listen_keys.Count; i++)
        {
            if (CurrentKeyboardState.IsKeyDown(listen_keys[i].Key) && previous_keyboard_state.IsKeyUp(listen_keys[i].Key))
            {
                key_buffer.Add(listen_keys[i].Key);
            }
        }

        for (int i = 0; i < listen_keys.Count; i++)
        {
            if (CurrentKeyboardState.IsKeyDown(listen_keys[i].Key) && previous_keyboard_state.IsKeyDown(listen_keys[i].Key))
            {
                listen_keys[i].AddtoMillisecondsDown(MillisecondsSinceLastFrame);
                if (listen_keys[i].MillisecondsDown >= this.hold_threshold)
                {
                    held_key_buffer_.Add(listen_keys[i].Key);
                }
            }
            else if (CurrentKeyboardState.IsKeyUp(listen_keys[i].Key) && previous_keyboard_state.IsKeyDown(listen_keys[i].Key))
            {
                listen_keys[i].ResetMilliecondsDown();
            }
        }

        
        held_key_buffer = held_key_buffer_;
		previous_keyboard_state = CurrentKeyboardState;
	}

	public List<Keys> GetPressedKeys()
	{
		if (listen_keys.Count > 0)
		{
			return key_buffer;
		}

		else return array_of_pressed_keys.ToList<Keys>();
	}

	public List<Keys> GetHeldKeys()
	{
        return held_key_buffer;
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
