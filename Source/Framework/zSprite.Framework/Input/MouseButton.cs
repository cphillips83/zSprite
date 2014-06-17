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
using System.Collections.Generic;
#endregion Namespace Declarations

namespace zSprite
{
    public struct MouseButton : Input
    {
        private InputType type;
        private int id;
        private string displayName;
        private string name;
        private HashSet<string> identifiers;

        public MouseButton(InputType type, int id, string name, string displayName, params string[] alternateStrings)
        {
            this.type = type;
            this.id = (int)id;
            this.name = name;
            this.displayName = displayName;
            this.identifiers = new HashSet<string>(alternateStrings);
        }

        public InputType getType()
        {
            return type;
        }

        public IEnumerable<string> identifiers1()
        {
            foreach (var i in identifiers)
                yield return i;
        }

        public int getId()
        {
            return id;
        }

        public string getName()
        {
            return name;
        }

        public string getDisplayName()
        {
            return displayName;
        }
    }

}
