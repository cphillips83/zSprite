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
    public class UnknownInput : Input
    {
        public static readonly string UNKNOWN_PART = "_UNKNOWN_";
        private InputType type;
        private int id;

        public UnknownInput(InputType type, int id)
        {
            this.type = type;
            this.id = id;
        }

        public static Input tryParse(string s)
        {
            foreach (var type in InputType.values())
            {
                var _type = type.ToString();
                if (s.StartsWith(_type))
                {
                    var remainder = s.Substring(_type.Length);
                    if (remainder.StartsWith(UNKNOWN_PART))
                    {
                        var hexadecimal = remainder.Substring(UNKNOWN_PART.Length);
                        try
                        {
                            var id = Convert.ToInt32(hexadecimal, 16);
                            return type.getInput(id);
                        }
                        catch //(NumberFormatException e)
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }

        public InputType getType()
        {
            return type;
        }

        public int getId()
        {
            return id;
        }

        public String getName()
        {
            return type.ToString() + UNKNOWN_PART + string.Format("{0:X}", id).ToUpperInvariant();
        }

        public String getDisplayName()
        {
            return "Unknown " + string.Format("{0:X}", id).ToUpperInvariant();
        }

        public override string ToString()
        {
            return getName();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is UnknownInput)
            {
                var other = (Input)obj;
                return other.getType() == this.getType() && this.id == other.getId();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.type.GetHashCode() ^ this.id;
        }

    }
}
