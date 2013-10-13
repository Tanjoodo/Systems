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
        public int MillisecondsDown;

        public Keystroke(Keys Key, float MillisecondsDown)
        {
            this.Key = Key;
            this.MillisecondsDown = (int)MillisecondsDown;
        }

        public void AddtoMillisecondsDown(float Milliseconds)
        {
            this.MillisecondsDown = this.MillisecondsDown +  (int)Milliseconds;
        }

        public void ResetMilliecondsDown()
        {
            this.MillisecondsDown = 0;
        }
    }
}
