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

#endregion Namespace Declarations

namespace zSprite
{
    /// <summary>
    /// This enum determines which events a button will send
    /// </summary>
    public struct ActivateMode
    {
        /// <summary>
        /// The button will only send ButtonState.DOWN events
        /// </summary>
        public static readonly ActivateMode PRESS = new ActivateMode(true, false);

		/// <summary>
        /// The button will only send ButtonState.UP events
		/// </summary>
        public static readonly ActivateMode RELEASE = new ActivateMode(false, true);

		/// <summary>
		/// The button will send all events
		/// </summary>
        public static readonly ActivateMode BOTH = new ActivateMode(true, true);

        private bool activatedOnPress;
        private bool activatedOnRelease;

        private ActivateMode(bool activatedOnPress, bool activatedOnRelease)
        {
            this.activatedOnPress = activatedOnPress;
            this.activatedOnRelease = activatedOnRelease;

        }

        public bool isActivatedOnPress()
        {
            return activatedOnPress;
        }

        public bool isActivatedOnRelease()
        {
            return activatedOnRelease;
        }
    }
}