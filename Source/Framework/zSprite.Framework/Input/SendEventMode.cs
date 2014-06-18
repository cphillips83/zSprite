#region GPLv3 License

/*
zSprite
Copyright © 2014 zSprite Project Team

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License V3
as published by the Free Software Foundation; either
version 3 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
General Public License V3 for more details.

You should have received a copy of the GNU General Public License V3
along with this library; if not, write to the Free Software
Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
*/

#endregion

#region Namespace Declarations
using System;

#endregion Namespace Declarations

namespace zSprite
{
    public abstract class test
    {
        public class ALways : test
        {

            public static override void a()
            {
                test.a();
            }
        }

        public static abstract void a();
    }

    public class SendEventMode
    {
        /// <summary>
        /// Send an event every update/frame with the current axis value
        /// </summary>
        public static readonly SendEventMode ALWAYS = new SendEventMode(new Func<float, float, bool>((oldValue, newValue) =>
        {
            return true;
        }));

        /// <summary>
        /// Sends an event every frame in which the current axis value is not zero
        /// </summary>
        public static readonly SendEventMode WHEN_NON_ZERO = new SendEventMode(new Func<float, float, bool>((oldValue, newValue) =>
        {
            return newValue != 0;
        }));

        /// <summary>
        /// Only sends an event when the value of the axis changes
        /// </summary>
        public static readonly SendEventMode WHEN_CHANGED = new SendEventMode(new Func<float, float, bool>((oldValue, newValue) =>
        {
            return newValue != oldValue;
        }));

        private SendEventMode()
        {
            shouldSendEvent = null;
        }

        private SendEventMode(Func<float, float, bool> shouldSendEvent)
        {
            this.shouldSendEvent = shouldSendEvent;
        }

        public readonly Func<float, float, bool> shouldSendEvent;
    }
}