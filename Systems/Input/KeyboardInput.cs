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
        List<Keys> _keyBuffer;
        List<Keys> _heldKeyBuffer = new List<Keys>();
        Keys[] _pressedKeys;
        List<Keystroke> _listenKeys;

        KeyboardState prevState = new KeyboardState();

        float _holdThreshold;

        /// <summary>
        /// Keys pressed in the latest provided frame
        /// </summary>
        public List<Keys> PressedKeys
        {
            get
            {
                if (_listenKeys.Count > 0)
                {
                    return _keyBuffer;
                }

                return _pressedKeys.ToList<Keys>();
            }
        }

        /// <summary>
        /// Keys held in the latest provided frame
        /// </summary>
        public List<Keys> HeldKeys => _heldKeyBuffer;

        /// <summary>
        /// Initializes a new instance of KeyboardInput class
        /// </summary>
        public KeyboardInput()
        {
            _keyBuffer = new List<Keys>();
            _listenKeys = new List<Keystroke>();
        }

        /// <summary>
        /// Set what keys to return if pressed. If no keys are set, all keys will be listened to.
        /// </summary>
        /// <param name="listenKeys">A list of <see cref="Keys"/> that holds a group of non-duplicated keys to return if pressed</param>
        public void SetListenKeys(Keys[] listenKeys)
        {

            if (listenKeys.Count() == listenKeys.Distinct<Keys>().Count()) //Checks for duplicates
            {
                foreach (Keys Key in listenKeys)
                {
                    this._listenKeys.Add(new Keystroke(Key, 0));
                }
            }
            else
            {
                throw new ArgumentException("Argument has duplicates", "ListenKeys"); //Duplicates will cause problems to happen with the key buffer.
            }

        }

        /// <summary>
        /// Sets the time threshold where a pressed button becomes a held down button
        /// </summary>
        /// <param name="Threshold">Threshold in milliseconds</param>
        public void SetHoldThreshold(float Threshold)
        {
            _holdThreshold = Threshold;
        }

        /// <summary>
        /// Gets list of pressed keys, if a specific list of listen keys were specified, only keys on that list will be returned if they 
        /// were pressed.
        /// </summary>
        /// <param name="CurrentKeyboardState">A current <see cref="KeyboardState"/> object.</param>
        /// <param name="MillisecondsSinceLastFrame">Milliseconds since last frame</param>
        public void UpdateInput(KeyboardState CurrentKeyboardState, float MillisecondsSinceLastFrame)
        {
            _pressedKeys = CurrentKeyboardState.GetPressedKeys();
            List<Keys> heldKeyBuffer = new List<Keys>();

            for (int i = 0; i < _listenKeys.Count; i++)
            {
                if (CurrentKeyboardState.IsKeyDown(_listenKeys[i].Key) && prevState.IsKeyUp(_listenKeys[i].Key))
                {
                    _keyBuffer.Add(_listenKeys[i].Key);
                }
            }

            for (int i = 0; i < _listenKeys.Count; i++)
            {
                if (CurrentKeyboardState.IsKeyDown(_listenKeys[i].Key) && prevState.IsKeyDown(_listenKeys[i].Key))
                {
                    _listenKeys[i].AddtoMillisecondsDown(MillisecondsSinceLastFrame);
                    if (_listenKeys[i].MillisecondsDown >= this._holdThreshold)
                    {
                        heldKeyBuffer.Add(_listenKeys[i].Key);
                    }
                }
                else if (CurrentKeyboardState.IsKeyUp(_listenKeys[i].Key) && prevState.IsKeyDown(_listenKeys[i].Key))
                {
                    _listenKeys[i].ResetMilliecondsDown();
                }
            }

            _heldKeyBuffer = heldKeyBuffer;
            prevState = CurrentKeyboardState;
        }

        /// <summary>
        /// Removes the first item from the input buffer, after it has been processed.
        /// </summary>
        public void RemoveFirstFromBuffer()
        {
            _keyBuffer.RemoveAt(0); //Remove the first key in the list. To be used once the keystroke has been processed.
        }
    }
}
