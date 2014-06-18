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
    public interface BindableAxis
    {

        /// <returns>The id of this axis</returns>
        string getId();

		/// <summary>
        /// Set the circumstance under which the axis will send events
		/// </summary>
		/// <param name="mode"></param>
        void setSendEventMode(SendEventMode mode);

        /// <returns>The circumstance under which the axis will send events</returns>
        SendEventMode getSendEventMode();

		/// <summary>
        /// Registers a direct subscriber to the axis events
		/// </summary>
		/// <param name="subscriber"></param>
        void subscribe(BindAxisSubscriber subscriber);

        /// <summary>
        /// Unregisters a direct subscriber to the axis events
        /// </summary>
        /// <param name="subscriber"></param>
        void unsubscribe(BindAxisSubscriber subscriber);

		/// <summary>
        /// The current value of the axis
		/// </summary>
		/// <returns></returns>
        float getValue();

    }
}