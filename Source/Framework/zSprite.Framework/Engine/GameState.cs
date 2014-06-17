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

namespace zSprite
{
    public interface GameState
    {

        void init(GameEngine engine);

        void dispose();

        void handleInput(float delta);

        void update(float delta);

        void render();

        /// <summary>
        /// Determines whether the game could hibernate when it loses focus
        /// </summary>
        /// <returns> Whether the game should hibernate when it loses focus</returns>
        bool isHibernationAllowed();
    }
}
