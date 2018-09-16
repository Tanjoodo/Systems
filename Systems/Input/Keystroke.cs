using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

namespace Systems.Input
{
    class Keystroke
    {
        public Keys Key;
        public float MillisecondsDown;

        public Keystroke(Keys Key, float MillisecondsDown)
        {
            this.Key = Key;
            this.MillisecondsDown = MillisecondsDown;
        }

        public void AddtoMillisecondsDown(float Milliseconds)
        {
            MillisecondsDown = MillisecondsDown +  Milliseconds;
        }

        public void ResetMilliecondsDown()
        {
            MillisecondsDown = 0;
        }
    }
}
